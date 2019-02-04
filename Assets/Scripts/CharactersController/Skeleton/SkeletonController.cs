using System;
using System.Globalization;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonController : MonoBehaviour
{
    // public variables
    public NavMeshAgent Agent;
    public SkeletonAnimatorController AnimatorController;
    public VisualPartMovement VisualPartMovement;
    public Health Health;
    public float AttackDistance;

    // private variables
    private Transform _target;
    
    // incapsulation
    public Transform Target
    {
        get => _target;
        set
        {
            _target = value;
            Agent.destination = value.position;
        }
    }

    private void Awake()
    {
        float GetVelocity() => Agent.velocity.magnitude;
        Transform GetTarget() => _target;
        void OnAttack(bool x) => VisualPartMovement.SetTempTargetRosition(x ? _target : null);
        AnimatorController.Init(GetVelocity, GetTarget, OnAttack, Agent.speed, AttackDistance);
    }

    #if UNITY_EDITOR
    /*private void Update()
    {
        if (!Input.GetMouseButton(0)) return;
        if (!Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out var hit)) return;
        
        _target           = hit.collider.name.Equals("Golem") ? hit.collider.transform : null;
        Agent.destination = hit.point;
    }*/

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(AnimatorController.transform.position, AttackDistance);
    }
    #endif
}
