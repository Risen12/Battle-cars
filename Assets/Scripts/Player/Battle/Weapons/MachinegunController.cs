using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MachinegunController : MonoBehaviour
{
    [SerializeField] private AudioClip _sound;
    [SerializeField] private float _shotDistance;
    [SerializeField] private float _damagePerShot;
    [SerializeField] private ParticleSystem _particles;

    private AudioSource _source;
    
    public float ShotDistance => _shotDistance;
    public float DamagePerShot => _damagePerShot;

    private void Awake()
    {
        _source = GetComponent<AudioSource>();
        _source.clip = _sound;
    }

    public void ShowAttackEffect()
    {
        _source.Play();
        _particles.Play();
    }
}
