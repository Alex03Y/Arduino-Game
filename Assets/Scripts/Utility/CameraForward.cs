using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraForward : MonoBehaviour
{
    public float ForwardSpeed;
    public Transform Target;

    private Transform _transform;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
    }

    private void Update()
    {
        _transform.position = Vector3.Lerp(_transform.position, Target.position, ForwardSpeed * Time.deltaTime);
    }
}
