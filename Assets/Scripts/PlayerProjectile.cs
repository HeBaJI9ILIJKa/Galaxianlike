using UnityEngine;

public class PlayerProjectile : Projectile
{
    public override void OnTriggerEnter(Collider other)
    {
        var enemy = other.gameObject.GetComponent<Enemy>();
        if (enemy)
        {
            enemy.TakeDamage();
            Destroy();
        }
    }
}
