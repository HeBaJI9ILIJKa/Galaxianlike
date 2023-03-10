using System.Collections;
using System;
using UnityEngine;

public class Enemy : BaseUnit
{
    [SerializeField] private Transform _projectileLaunchPosition;

    [SerializeField] private int _spawnChance;

    [SerializeField] private float _shootCooldown = 1f;
    
    public int SpawnChance => _spawnChance;

    public event Action<Enemy> OnDestroy;

    private Playership _target;

    private ProjectilePool _projectilePool;

    private Coroutine _shoting;

    private Rigidbody _rigidbody;

    private Vector3 _movementVector = Vector3.back;

    private float _changeMoveDirectionTime = 0f;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    public void SetTarget(Playership target)
    {
        _target = target;
    }

    public void SetProjectilePool(ProjectilePool projectilePool)
    {
        _projectilePool = projectilePool;
    }

    private void FixedUpdate()
    {
        if (Game.GameRunning == false)
            return;

        Rotation();

        Movement();
    }

    private void Rotation()
    { 
        if (_target == null)
            return;
        Vector3 targetPosition = _target.Position;
        targetPosition.z += UnityEngine.Random.Range(1f, 10f);

        Vector3 targetVector = (_target.Position - transform.position).normalized;
        targetVector.y = 0;

        if (targetVector.Equals(Vector3.zero))
            return;

        transform.rotation = Quaternion.LookRotation(targetVector);
    }
    
    private void Movement()
    {
        _changeMoveDirectionTime -= Time.fixedDeltaTime;

        if(_changeMoveDirectionTime <= 0)
        {
            _movementVector.x = UnityEngine.Random.Range(0f, 2f) - 1f;
            _changeMoveDirectionTime = UnityEngine.Random.Range(0f, 2f);
        }

        _rigidbody.AddForce(_movementVector, ForceMode.VelocityChange);
    }

    public void StartShooting()
    {
        _shoting = StartCoroutine(Shooting());
    }

    private IEnumerator Shooting()
    {
        while (Game.GameRunning)
        {   
            yield return new WaitForSeconds(_shootCooldown);
            Shoot();
        }
    }

    public override void Shoot()
    {
        var projectile = _projectilePool.GetProjectileFromPool();
        projectile.transform.position = _projectileLaunchPosition.position;
        projectile.transform.rotation = Quaternion.Euler(0, RandomAim().eulerAngles.y, 0);
        projectile.Launch();
    }

    private Quaternion RandomAim()
    {
        if (_target == null)
            return transform.rotation;

        Vector3 targetPosition = _target.Position;
        targetPosition.z += UnityEngine.Random.Range(1f, 10f);

        Vector3 targetVector = (targetPosition - transform.position).normalized;
        targetVector.y = 0;

        return Quaternion.LookRotation(targetVector);
    }

    public override void TakeDamage()
    {
        ScoreController.ScoreIncrease();
        Destroy();
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.gameObject.GetComponent<Playership>();
        if (player)
        {
            player.TakeDamage();
            Destroy();
        }
    }

    public void Destroy()
    {
        OnDestroy?.Invoke(this);
        Destroy(gameObject);
    }
}
