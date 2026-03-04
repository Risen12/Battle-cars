using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Health
{
    [RequireComponent(typeof(Slider))]
    public class HealthShower : MonoBehaviour
    {
        [SerializeField] private float _smoothIndex;

        private Coroutine _changeSliderValueSmooth;
        private Slider _slider;
        private Core.Health _health;

        private void Awake()
        {
            _slider = GetComponent<Slider>();
        }

        private void OnDisable()
        {
            if (_health != null)
            {
                _health.ValueChanged -= OnHealthChanged;
            }
        }

        private void Update()
        {
            transform.LookAt(Camera.main.transform);
        }

        public void SetHealthComponent(Core.Health health)
        {
            _health = health;
            _slider.maxValue = health.Value;
            _slider.value = _health.Value;

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