using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmoDrawer : MonoBehaviour
{
    public enum GizmoType
    {
        Sphere,
        Box,
    }

    [SerializeField] private GizmoType _type;
    [SerializeField] private float _radius;
    [SerializeField] private Color _color;
    
    void OnDrawGizmos()
    {
        switch (_type)
        {
            case GizmoType.Sphere:
                Gizmos.color = _color;
                Gizmos.DrawSphere(transform.position, _radius);
                break;
            case GizmoType.Box:
                Gizmos.color = _color;
                Gizmos.DrawCube(transform.position, new Vector3(_radius, _radius, _radius));
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
