using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAfterTargetWithOffset : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _offset;
    void LateUpdate()
    {
        transform.position = _target.position + _offset;
    }
}
