using System.Collections;
using System.Xml;
using UnityEngine;

namespace Tanks.UI
{
    public class CanvasFader : MonoBehaviour
    {
        [SerializeField] float loadLerpDuration = 0.4f;
        [SerializeField] CanvasGroup canvasGroup;

        private AnimationCurve animationCurve;

        public int GetIntAlpha()
        {
            return Mathf.RoundToInt(canvasGroup.alpha);
        }

        private void Start()
        {
            FadeIn();
        }

        private void OnEnable()
        {
            FadeIn();
        }

        internal IEnumerator LerpCanvas(int from, int to)
        {
            var startTime = Time.time;
            var endTime = Time.time + loadLerpDuration;
            var elapsedTime = 0f;

            canvasGroup.alpha = animationCurve.Evaluate(from);
            while (Time.time <= endTime)
            {
                elapsedTime = Time.time - startTime;
                var percentage = 1 / (loadLerpDuration / elapsedTime);

                canvasGroup.alpha = animationCurve.Evaluate(to == 0 ? 1 - percentage : percentage);
                yield return new WaitForEndOfFrame(); 
            }

            canvasGroup.alpha = animationCurve.Evaluate(to);
            if(to == 0)
                gameObject.SetActive(false);
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
                canvasGroup.alpha = 0;
                animationCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
                StartCoroutine(LerpCanvas(0, 1));
            }
        }
    }
}
