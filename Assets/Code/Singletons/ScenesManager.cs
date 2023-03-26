using System;
using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;

using Tanks.UI;

namespace Tanks.SceneManagement
{
    public class ScenesManager: SingletonBehaviour<ScenesManager>
    {
        public enum Scenes
        {
            MainMenu,
            Gameplay,
        }

        [SerializeField] private AppCanvas _loading;
        [SerializeField] SceneReference mainMenu;
        [SerializeField] SceneReference gameplayScene;

        public void GoToScene(Scenes targetScene)
        {
            switch (targetScene)
            {
                case Scenes.MainMenu:
                    StartCoroutine(LoadSceneCoroutine(mainMenu));
                    break;
                case Scenes.Gameplay:
                    StartCoroutine(LoadSceneCoroutine(gameplayScene));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(targetScene), targetScene, null);
            }
        }

        private IEnumerator LoadSceneCoroutine(SceneReference sceneToLoad)
        {
            _loading.gameObject.SetActive(true);
            _loading.FadeInCanvas();
            yield return new WaitUntil(() => _loading.GetIntAlpha() == 1);
            AsyncOperation load = SceneManager.LoadSceneAsync(sceneToLoad.ScenePath);
            yield return new WaitUntil(() => load.isDone == true);
            _loading.FadeOutCanvas();
        }

        internal void GoToMainMenu(int delay)
        {
            StartCoroutine(GoToMainManuInTimeCoroutine(delay));
        }

        private IEnumerator GoToMainManuInTimeCoroutine(int delay)
        {
            yield return new WaitForSeconds(delay);
            GoToScene(Scenes.MainMenu);
        }
    }
}