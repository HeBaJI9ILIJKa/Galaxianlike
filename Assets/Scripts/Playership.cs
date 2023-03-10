using System;
using System.Collections.Generic;
using UnityEngine;


public class Playership : BaseUnit
{
    [SerializeField] private LayerMask _groundMask;

    [SerializeField] private float _turningSpeed = 1f;

    [SerializeField] private Vector3 _forwardVelocity= new Vector3(0, 0, 3);

    [SerializeField] private Transform _projectileLaunchPosition;

    [SerializeField] private ProjectilePool _projectilePool;

    public Vector3 Position => transform.position;

    public static event Action OnPlayerKilled;

    private Rigidbody _rigidbody;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();

        //Vector3 moveVector = new Vector3(0, 0, 5);
        //_rigidbody.AddForce(moveVector, ForceMode.VelocityChange);
    }

    void Update()
    {
        Shoot();


    }

    private void FixedUpdate()
    {
        Rotation();

        Movement();
    }

    private void Rotation()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, Mathf.Infinity))
        {
            var hitPosition = new Vector3(raycastHit.point.x, transform.position.y, raycastHit.point.z);

            var direction = (hitPosition - transform.position).normalized;
            var rotation = Quaternion.LookRotation(direction, Vector3.up);
            rotation = Quaternion.Euler(90, rotation.eulerAngles.y, 0);
            transform.rotation = rotation;
        }
    }

    private void Movement()
    {
        Vector3 movementVector = Vector3.zero;

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            movementVector = Vector3.right;
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            movementVector = Vector3.left;

        _rigidbody.velocity += movementVector.normalized * _turningSpeed;
        _rigidbody.velocity += _forwardVelocity;
    }

    public override void Shoot()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            var projectile = _projectilePool.GetProjectileFromPool();
            projectile.transform.position = _projectileLaunchPosition.position;
            projectile.transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
            projectile.Launch();
        }
    }

    public override void TakeDamage()
    {
        OnPlayerKilled?.Invoke();
    }

}
