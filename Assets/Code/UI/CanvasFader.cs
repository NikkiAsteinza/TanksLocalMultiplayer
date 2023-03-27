using System.Collections;
using UnityEngine;

namespace Tanks.UI
{
    public class CanvasFader : MonoBehaviour
    {
        [SerializeField] private float _loadLerpDuration = 0.4f;
        [SerializeField] private CanvasGroup _canvasGroup;

        private AnimationCurve animationCurve;

        public int GetIntAlpha()
        {
            return Mathf.RoundToInt(_canvasGroup.alpha);
        }

        private void Awake()
        {
            _canvasGroup.alpha = 0;
        }

        internal IEnumerator LerpCanvas(int from, int to)
        {
            float startTime = Time.time;
            float endTime = Time.time + _loadLerpDuration;
            float elapsedTime = 0f;
            animationCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
            _canvasGroup.alpha = animationCurve.Evaluate(from);
            while (Time.time <= endTime)
            {
                elapsedTime = Time.time - startTime;
                float percentage = 1 / (_loadLerpDuration / elapsedTime);

                _canvasGroup.alpha = animationCurve.Evaluate(to == 0 ? 1 - percentage : percentage);
                yield return new WaitForEndOfFrame(); 
            }

            _canvasGroup.alpha = animationCurve.Evaluate(to);

            if (to == 0)
            {
                gameObject.SetActive(true);
            }
        }

        public void FadeOut()
        {            
            if(gameObject.activeInHierarchy)
                StartCoroutine(LerpCanvas(1, 0));
        }

        public void FadeIn()
        {
            if (gameObject.activeInHierarchy)
            {
                _canvasGroup.alpha = 0;
                animationCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
                StartCoroutine(LerpCanvas(0, 1));
            }
        }
    }
}