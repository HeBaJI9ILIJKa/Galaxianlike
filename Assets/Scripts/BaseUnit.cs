using UnityEngine;

public interface IUnit
{
    void TakeDamage();
    void Shoot();
}

public abstract class BaseUnit : MonoBehaviour, IUnit
{
    public abstract void Shoot();

    public abstract void TakeDamage();
}
