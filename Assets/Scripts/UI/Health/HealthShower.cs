using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Health
{
    public class HealthShower : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private float _smoothIndex;
        
        private Coroutine _changeSliderValueSmooth;

        private Core.Health _health;

        private void OnDisable()
        {
            if (_health != null)
            {
                _health.ValueChanged -= OnHealthChanged;
            }
        }

        public void SetHealthComponent(Core.Health health)
        {
            _health = health;

            _health.ValueChanged += OnHealthChanged;
        }

        private void OnHealthChanged(float value)
        {
            if (_changeSliderValueSmooth != null)
            {
                StopCoroutine(_changeSliderValueSmooth);
            }
            
            StartCoroutine(ChangeSliderValueSmooth(value));
        }

        private IEnumerator ChangeSliderValueSmooth(float targetValue)
        {
            while (!Mathf.Approximately(_slider.value, targetValue))
            {
                _slider.value = Mathf.MoveTowards(_slider.value, targetValue, Time.deltaTime * _smoothIndex);
                yield return null;
            }
        }
    }
}