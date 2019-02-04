using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualPartMovement : MonoBehaviour
{
    public Transform TargetPosition;
    public Transform TargetRotation;
    
    [Tooltip("Move speed from 'visual' to 'hide' part")]
    public float MoveSmoothTime;
    public float MaxMoveSpeed;

    [Space]
    public float RotationSmoothSpeed;
    public float MaxRotationSpeed;
    
    
    private Transform _transform;
    private Vector3 _positionRefVelocity;
    private Vector3 _rotationRefVelocity;

    private void Awake() =>  _transform = GetComponent<Transform>();
    private void FixedUpdate()
    {
        PositionControl();
        RotationControl();
    }

    private void PositionControl()
    {
        var target = ReferenceEquals(_tempTargetPosition, null) ? TargetPosition : _tempTargetPosition;
        var newPos = Vector3.SmoothDamp(_transform.position, target.position, ref _positionRefVelocity, 
                                        MoveSmoothTime, MaxMoveSpeed);
        _transform.position = newPos;
    }

    private void RotationControl()
    {
        var target = ReferenceEquals(_tempTargetRotation, null) ? TargetRotation : _tempTargetRotation;
        
        // smooth follow
        var rustDir = (target.position - _transform.position).normalized;
        var cleanDir = Vector3.ProjectOnPlane(rustDir, _transform.up);

        _transform.forward = Vector3.SmoothDamp(_transform.forward, cleanDir, ref _rotationRefVelocity,
                                                RotationSmoothSpeed, MaxRotationSpeed);
    }

    private Transform _tempTargetPosition;
    private Transform _tempTargetRotation;
    public void SetTempTargetPosition(Transform tempTargetPosition) => _tempTargetPosition = tempTargetPosition;
    public void SetTempTargetRosition(Transform tempTargetRotation) => _tempTargetRotation = tempTargetRotation;
}
