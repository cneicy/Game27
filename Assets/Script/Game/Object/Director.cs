using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Script.Game.Object
{
    public class Director : MonoBehaviour
    {
        [SerializeField] private Transform targetPoint;
        [SerializeField] private Transform startPoint;
        [SerializeField] private float targetIntensity;
        [SerializeField] private float stoppingDistance;
        [SerializeField] private float moveSpeed;
        [SerializeField] private Light2D[] light2D;
        private bool _moveTrigger;
        private bool _lightOn;
        private bool _lightOff;

        private void Awake()
        {
            foreach (var light in light2D)
            {
                light.intensity = 0;
            }
        }

        //亮度缓慢增加
        private void LightOn()
        {
            if (light2D[0].intensity < targetIntensity && _lightOn)
            {
                light2D[0].intensity += 0.02f;
                light2D[1].intensity += 0.2f;
            }
        }

        //亮度缓慢降低
        private void LightOff()
        {
            if (light2D[0].intensity > 0 && _lightOff)
            {
                light2D[0].intensity -= 0.02f;
                light2D[1].intensity -= 0.2f;
            }
        }

        //复位
        private void OnEnable()
        {
            transform.position = startPoint.position;
            _moveTrigger = false;
        }

        private void FixedUpdate()
        {
            LightOn();
            LightOff();
        }

        private void Update()
        {
            if (!_moveTrigger) return;
            var direction = targetPoint.position - transform.position;
            if (!(direction.magnitude > stoppingDistance)) return;
            var moveVector = direction.normalized * (moveSpeed * Time.deltaTime);
            transform.Translate(moveVector);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            _lightOn = true;
            _moveTrigger = true;
            StartCoroutine(Disable());
        }

        private IEnumerator Disable()
        {
            yield return new WaitForSeconds(2);
            _lightOn = false;
            _lightOff = true;
            yield return new WaitForSeconds(2);
            _lightOff = false;
            gameObject.SetActive(false);
        }
    }
}