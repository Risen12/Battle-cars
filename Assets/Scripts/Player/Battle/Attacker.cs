using System;
using UnityEngine;

namespace Player.Battle
{
    public class Attacker : MonoBehaviour
    {
        [SerializeField] private MachinegunController _machinegun;
        [SerializeField] private InputController _inputController;
        [SerializeField] private LayerMask _layerMask;

        private Ray _ray;
        private RaycastHit _hit;

        private void OnEnable()
        {
            _inputController.AttackPerformed += Attack;
        }

        private void OnDisable()
        {
            _inputController.AttackPerformed -= Attack;
        }
        
        private void Attack()
        {
            Debug.Log("Зашёл в метод атаки");
            
            _machinegun.ShowAttackEffect();

            if (Physics.Raycast(new Vector3(transform.position.x, 0.3f, transform.position.z), transform.forward, out _hit, _machinegun.ShotDistance, _layerMask))
            {
                Debug.DrawRay(new Vector3(transform.position.x, 0.3f, transform.position.z), transform.forward * _machinegun.ShotDistance, Color.red);
                if (_hit.transform.gameObject.TryGetComponent(out Enemy.Enemy enemy))
                {
                    enemy.TakeDamage(_machinegun.DamagePerShot);
                    Debug.Log(enemy.CurrentHelath);
                }
            }
            else
            {
                Debug.DrawRay(new Vector3(transform.position.x, 0.3f, transform.position.z), transform.forward * _machinegun.ShotDistance, Color.green);
            }
        }
    }
}