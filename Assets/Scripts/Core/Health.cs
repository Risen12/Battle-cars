using System;

namespace Core
{
    public class Health
    {
        private float _value;
        
        public Health(float maxValue)
        {
            _value = maxValue;
        }

        public event Action<float> ValueChanged;
        
        public float Value => _value;

        public void TakeDamage(float damage)
        {
            if (_value - damage < 0)
            {
                _value = 0;
            }
            else
            {
                _value -= damage;
            }

            ValueChanged?.Invoke(_value);
        }
    }
}