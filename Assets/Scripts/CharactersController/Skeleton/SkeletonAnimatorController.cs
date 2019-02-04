using System;
using UnityEngine;

public class SkeletonAnimatorController : MonoBehaviour
{
    public Animator Animator;

    private Transform _transform;
    
    private float _maxSpeed;
    private Func<float> _getCurrSpeed;
    private Func<Transform> _getTarget;
    private Action<bool> _onChangeAttackState;
    private float _attackDistance;
    private bool _initialized;

    public void Init(Func<float> velocity, Func<Transform> getTarget, Action<bool> onAttackState, float maxSpeed, float attackDistance)
    {
        _getCurrSpeed = velocity;
        _maxSpeed = maxSpeed;
        _getTarget = getTarget;
        _attackDistance = attackDistance;
        _onChangeAttackState = onAttackState;
        _initialized = true;
    }

    private void Awake() => _transform = GetComponent<Transform>();
    public void Death() => Animator.SetTrigger("Death");
    
    private void Update()
    {
        if(!_initialized) return;
        var animatorState = Animator.GetCurrentAnimatorStateInfo(0);
        
        void AttackState(bool state)
        {
            Animator.SetBool("Attack", state);
            _onChangeAttackState?.Invoke(state);
        }

        // attack control
        var target = _getTarget?.Invoke();
        if (!ReferenceEquals(target, null))
        {
            var dst = (_transform.position - target.position).magnitude;
            if(animatorState.IsName("Attack") && dst > _attackDistance) AttackState(false);
            else if(!animatorState.IsName("Attack") && dst <= _attackDistance) AttackState(true);
        }
        else if(animatorState.IsName("Attack")) AttackState(false);

        // speed animation control
        if (_getCurrSpeed.Invoke() > 0.1f)
        {
            var speedAmount = _getCurrSpeed.Invoke() / _maxSpeed;
            Animator.SetFloat("Velocity", speedAmount);    
        }
        else Animator.SetFloat("Velocity", 0);
    }
}
