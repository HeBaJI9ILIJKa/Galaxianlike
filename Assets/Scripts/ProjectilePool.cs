using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ProjectilePool : MonoBehaviour
{
    [SerializeField] private int _poolSize = 70;
    [SerializeField] private Projectile _projectilePrefab;

    private ObjectPool<Projectile> _projectilePool;

    private void Start()
    {
        CreatePool(_projectilePrefab, _poolSize);
    }

    private void CreatePool(Projectile projectilePrefab, int poolSize)
    {
        _projectilePool = new ObjectPool<Projectile>(createFunc: () =>  CreateProjectile(projectilePrefab), actionOnGet: (obj) => obj.gameObject.SetActive(true), actionOnRelease: (obj) => obj.gameObject.SetActive(false), actionOnDestroy: (obj) => Destroy(obj), false, defaultCapacity: poolSize);
        
        FillPool(_projectilePool, projectilePrefab, poolSize);
    }

    private Projectile CreateProjectile(Projectile projectilePrefab)
    {
        Projectile projectile = Instantiate(projectilePrefab);
        projectile.transform.SetParent(this.gameObject.transform);
        projectile.OnDestroy += ReturnProjectileToPool;
        return projectile;
    }

    private void FillPool(ObjectPool<Projectile> pool, Projectile projectilePrefab, int poolSize)
    {
        for (int i = 0; i < poolSize; i++)
        {
            Projectile projectile = CreateProjectile(projectilePrefab);
            pool.Release(projectile);
        }
    }

    public Projectile GetProjectileFromPool()
    {
        return _projectilePool.Get();
    }

    public void ReturnProjectileToPool(Projectile projectile)
    {
        projectile.Clean();
        _projectilePool.Release(projectile);
    }

}
