using System.Collections;
using UnityEngine;

namespace Tanks.SceneManagement
{
    public class LoadCanvas : MonoBehaviour
    {
        [SerializeField] float loadLerpDuration = 0.4f;
        [SerializeField] GameObject loadCanvas;
        [SerializeField] CanvasGroup canvasGroup;
        
        private AnimationCurve animationCurve;

        public int GetIntAlpha()
        {
            return Mathf.RoundToInt(canvasGroup.alpha);
        }

        private void Awake()
        {
            loadCanvas.SetActive(false);
            canvasGroup.alpha = 0;
            animationCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
        }
        
        internal IEnumerator LerpCanvas(int from, int to)
        {
            if (!loadCanvas.activeInHierarchy)
            {
                loadCanvas.SetActive(true);
            }

            var startTime = Time.time;
            var endTime = Time.time + loadLerpDuration;
            var elapsedTime = 0f;

            canvasGroup.alpha = animationCurve.Evaluate(from);
            while (Time.time <= endTime)
            {
                elapsedTime = Time.time - startTime; // update the elapsed time
                var percentage =  1 / (loadLerpDuration / elapsedTime); // calculate how far along the timeline we are

                canvasGroup.alpha = animationCurve.Evaluate(to == 0 ? 1-percentage :percentage);
                yield return new WaitForEndOfFrame(); // wait for the next frame before continuing the loop
            }
            canvasGroup.alpha = animationCurve.Evaluate(to);
            loadCanvas.SetActive(false);
        }
    }
}