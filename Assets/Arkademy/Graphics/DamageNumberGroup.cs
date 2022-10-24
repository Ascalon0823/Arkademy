using System;
using System.Collections;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Arkademy.Graphics
{
    public class DamageNumberGroup : MonoBehaviour
    {
        public Transform parent;
        [SerializeField] private Transform numberRoot;
        [SerializeField] private TextMeshProUGUI damageNumberPrefab;
        public float lifeTime;
        public float spawnInterval;

        public float fadeOutTime;

        public void Setup(int[] numbers, Transform parent)
        {
            this.parent = parent;
            StopAllCoroutines();
            StartCoroutine(SpawnNumbers(numbers));
            LeanTween.moveLocalY(numberRoot.gameObject, 0.5f, lifeTime + spawnInterval * numbers.Length + fadeOutTime)
                .setEaseOutCubic().setOnComplete(_ => Destroy(gameObject));
        }

        IEnumerator SpawnNumbers(int[] numbers)
        {
            foreach (var number in numbers)
            {
                var numberObj = Instantiate(damageNumberPrefab, numberRoot);
                numberObj.text = number.ToString();
                numberObj.transform.position += new Vector3(Random.Range(-1, 1) * 0.15f, 0, 0);
                LeanTween.alphaCanvas(numberObj.GetComponent<CanvasGroup>(), 0f, fadeOutTime);
                yield return new WaitForSeconds(spawnInterval);
            }
        }

        private void Update()
        {
            if (!parent) return;
            transform.position = parent.position;
        }
    }
}