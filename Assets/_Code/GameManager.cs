using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Tanks
{
    public enum GameMode { SinglePlayer, Multiplayer}
    public enum InputMode { Keyboard, Gamepad}

    public class GameManager : SingletonBehaviour<GameManager>
    {
        [SerializeField] float loadLerpDuration = 0.4f;
        [SerializeField] GameObject loadCanvas;
        [SerializeField] CanvasGroup canvasGroup;
        [SerializeField] SceneReference mainMenu;
        [SerializeField] SceneReference gameplayScene;

        [SerializeField] GameMode selectedGameMode;
        [SerializeField] InputMode player1_SelectedInputMode;
        [SerializeField] InputMode player2_SelectedInputMode;

        private AnimationCurve animationCurve;

        private void Awake()
        {
            loadCanvas.SetActive(false);
            canvasGroup.alpha = 0;
            animationCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
        }
        public void SetSelectedMode(GameMode selectedMode)
        {
            selectedGameMode = selectedMode;
        }

        public void SetPlayer1InputMode(InputMode selectedMode)
        {
            player1_SelectedInputMode = selectedMode;
        }
        public void SetPlayer2InputMode(InputMode selectedMode)
        {
            player2_SelectedInputMode = selectedMode;
        }
        public void InitGame()
        {
            StartCoroutine(LoadSceneCoroutine(gameplayScene));
        }
        public void GoToMainMenu()
        {
            StartCoroutine(LoadSceneCoroutine(mainMenu));
        }

        public void Quit()
        {
            Application.Quit();
        }

        private IEnumerator LoadSceneCoroutine(SceneReference sceneToLoad)
        {
            loadCanvas.SetActive(true);
            StartCoroutine(LerpCanvas(0, 1));
            yield return new WaitUntil(() => canvasGroup.alpha == 1);
            AsyncOperation load = SceneManager.LoadSceneAsync(sceneToLoad.ScenePath);
            yield return new WaitUntil(() => load.isDone == true);
            StartCoroutine(LerpCanvas(1, 0));
            yield return new WaitUntil(() => canvasGroup.alpha == 0);
            loadCanvas.SetActive(false);
        }
        private IEnumerator LerpCanvas(int from, int to)
        {
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
        }
    }
}
