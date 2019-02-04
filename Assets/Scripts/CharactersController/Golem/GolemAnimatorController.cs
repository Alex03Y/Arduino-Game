using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Managers.SoundManager;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class GolemAnimatorController : MonoBehaviour
{
    public Animator Animator;
    
    private List<Transform> _enemies = new List<Transform>();

    private void Update()
    {
        var animatorState = Animator.GetCurrentAnimatorStateInfo(0);

        if (_enemies.Count != 0 && animatorState.IsName("Idle"))
        {
            Animator.ResetTrigger("Attack1");
            Animator.ResetTrigger("Attack2");
            Animator.ResetTrigger("Attack3");
            Animator.ResetTrigger("Attack4");
            Animator.ResetTrigger("Attack5");
            Animator.ResetTrigger("Attack6");
            Animator.SetTrigger("Attack" + Random.Range(1, 7));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!other.transform.name.Contains("Skeleton")) return;
        _enemies.Add(other.transform);
        _enemies = _enemies.Where(x => !ReferenceEquals(x, null)).ToList();
    }

    private void OnTriggerExit(Collider other)
    {
        if(!other.transform.name.Contains("Skeleton")) return;
        var index = _enemies.FindIndex(x => x.GetInstanceID().Equals(other.transform.GetInstanceID()));
        if(index != -1) _enemies.RemoveAt(index);
        _enemies = _enemies.Where(x => !ReferenceEquals(x, null)).ToList();
    }


    [SerializeField] private Sound _golemFallDown;
    public void GolemFallDown() => SoundManager.Instance().Play(_golemFallDown, transform.position);

    [SerializeField] private Sound _golemAttack;
    [SerializeField] private Sound _golemAttackPost;
    public void GolemAttack()
    {
        SoundManager.Instance().Play(_golemAttack, transform.position);
        SoundManager.Instance().Play(_golemAttackPost, transform.position);
    }
}
