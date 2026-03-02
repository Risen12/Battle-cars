using System;
using UnityEngine;

namespace Player.Battle
{
    public class Attacker : MonoBehaviour
    {
        [SerializeField] private MachinegunController _machinegun;
        [SerializeField] private InputController _inputController;

        private Ray _ray;
        private RaycastHit _hit;

        private void onEnable()
        {
            _inputController.AttackPerformed += Attack;
        }

        private void OnDisable()
        {
            _inputController.AttackPerformed -= Attack;
        }
        
        private void Attack()
        {
            _machinegun.ShowAttackEffect();

            if (Physics.Raycast(new Vector3(transform.position.x, 0.5f, transform.position.z), transform.forward, out _hit, _machinegun.ShotDistance))
            {
                Debug.DrawRay(new Vector3(transform.position.x, 0.5f, transform.position.z), transform.forward * _machinegun.ShotDistance, Color.red);
                if (_hit.transform.gameObject.TryGetComponent(out Enemy.Enemy enemy))
                {
                    enemy.TakeDamage(_machinegun.DamagePerShot);
                    Debug.Log(enemy.CurrentHelath);
                }
            }
        }
    }
}