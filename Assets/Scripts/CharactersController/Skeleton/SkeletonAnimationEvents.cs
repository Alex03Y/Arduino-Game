using System.Collections;
using System.Collections.Generic;
using Managers.SoundManager;
using UnityEngine;

public class SkeletonAnimationEvents : MonoBehaviour
{
    public Sound StepSound;
    public Sound DeathSound;
    public Sound HitSword;
    public Sound HitBrick;

    public ParticleSystem SwordHit;

    private Transform _transform;
    private void Awake() => _transform = GetComponent<Transform>();
    public void Death() => SoundManager.Instance().Play(DeathSound, _transform.position);
    public void Step() => SoundManager.Instance().Play(StepSound, _transform.position);
    public void Attack()
    {
        SoundManager.Instance().Play(HitSword, _transform.position);
        SoundManager.Instance().Play(HitBrick, _transform.position);
        SwordHit.Play();
    }
}
