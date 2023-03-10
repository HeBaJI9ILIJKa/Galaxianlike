using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _speed = 5f, _lifeTime = 5f;

    public event Action<Projectile> OnDestroy;

    private Rigidbody _rigidBody = null;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void OnEnable()
    {
        Invoke("Destroy", _lifeTime);
    }

    public void Destroy()
    {
        OnDestroy?.Invoke(this);
    }

    public void Clean()
    {
        _rigidBody.velocity = Vector3.zero;
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
    }

    public void Launch()
    {
        _rigidBody.AddForce(transform.forward * _speed, ForceMode.Impulse);
        _rigidBody.velocity += new Vector3(0, 0, 1);
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        var unit = other.gameObject.GetComponent<BaseUnit>();
        if (unit)
            unit.TakeDamage();

        Destroy();
    }
}
