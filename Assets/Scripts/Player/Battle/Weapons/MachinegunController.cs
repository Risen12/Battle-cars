using UnityEngine;

public class MachinegunController : MonoBehaviour
{
    [SerializeField] private AudioSource _sound;
    [SerializeField] private float _shotDistance;
    [SerializeField] private float _damagePerShot;
    
    public float ShotDistance => _shotDistance;
    public float DamagePerShot => _damagePerShot;
    
    public void ShowAttackEffect()
    {
        
    }
}
