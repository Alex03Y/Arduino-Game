using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Managers.SoundManager;
using UnityEngine;

public class DragonAnimationEvents : MonoBehaviour
{
    public Animator Animator;
    public ParticleSystem BurnParticles;

    public float DamageAmount = 30f;
    public float TickTime = 0.5f;
    public Transform OverlapSpherePosition;
    public float SphereRadius;
    
    private void Start()
    {
        GetComponent<SerialReader>().OnDataReceived += i => Animator.SetInteger("Pressure", i);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
            Animator.SetInteger("Pressure", 300);
        else if(Input.GetKeyDown(KeyCode.LeftShift))
            Animator.SetInteger("Pressure", 500);

        var animState = Animator.GetCurrentAnimatorStateInfo(0);
        if (animState.IsName("AtkAction") && _attack == null)
             StartCoroutine(_attack = Attack());
        else if(_attack != null)
        {
            StopCoroutine(_attack);
            _attack = null;
        }
    }

    public Sound DragonAttack;
    public void BurnAttackStart()
    {
        BurnParticles.Play();
        SoundManager.Instance().Play(DragonAttack, transform.position);
    }

    [SerializeField] private Sound _dragonWings;
    public void DragonWings()
    {
        SoundManager.Instance().Play(_dragonWings, transform.position);
        SoundManager.Instance().Play(_dragonWings, transform.position);
    }

    public void BurnAttackEnd() => BurnParticles.Stop();        

    private IEnumerator _attack;
    private IEnumerator Attack()
    {
        while (true)
        {
            var healths = Physics.OverlapSphere(OverlapSpherePosition.position, SphereRadius)
                                   .ToList()
                                   .Select(x => x.GetComponent<Health>())
                                   .Where(x => x != null).ToList();
            healths.ForEach(x => x.RemoveHealth(DamageAmount));
            yield return new WaitForSecondsRealtime(TickTime);
        }        
    }

    private void OnDrawGizmos()
    {
        if(OverlapSpherePosition == null) return;
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(OverlapSpherePosition.position, SphereRadius);
    }
}
