using System;
using Core;
using UI.Health;
using UnityEngine;

namespace Enemy
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private HealthShower _healthShower;
        
        private Health _health;

        public float CurrentHelath => _health.Value;

        private void Awake()
        {
            _health = new Health(100);
            //_healthShower.SetHealthComponent(_health);
        }

        public void TakeDamage(float damage)
        {
            _health.TakeDamage(damage);
            Debug.Log(_health.Value);
        }
    }
}