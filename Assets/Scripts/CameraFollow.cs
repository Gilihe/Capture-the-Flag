using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset = new Vector3(0, 1.2f, -2.5f);

    private void LateUpdate()
    {
        transform.position = target.TransformPoint(offset);
        transform.LookAt(target);
    }
}
