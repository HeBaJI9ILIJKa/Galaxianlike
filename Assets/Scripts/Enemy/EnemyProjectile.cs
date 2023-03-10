using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : Projectile
{
    public override void OnTriggerEnter(Collider other)
    {
        var player = other.gameObject.GetComponent<Playership>();
        if (player)
        {
            player.TakeDamage();
            Destroy();
        }
    }
}
