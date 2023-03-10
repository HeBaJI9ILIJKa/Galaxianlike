using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var unit = other.gameObject.GetComponent<Enemy>();
        if (unit)
        {
            unit.Destroy();
            return;
        }

        var projectile = other.gameObject.GetComponent<Projectile>();
        if (projectile)
            projectile.Destroy();
    }
}
