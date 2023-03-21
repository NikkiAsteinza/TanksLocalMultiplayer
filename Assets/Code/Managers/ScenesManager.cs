using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Tanks.SceneManagement
{
    public class ScenesManager: SingletonBehaviour<ScenesManager>
    {
        public enum Scenes
        {
            MainMenu,
            Gameplay,
        }

        [SerializeField] SceneReference mainMenu;
        [SerializeField] SceneReference gameplayScene;
        [SerializeField] private LoadCanvas _loadCanvas;

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
            StartCoroutine(_loadCanvas.LerpCanvas(0, 1));
            yield return new WaitUntil(() => _loadCanvas.GetIntAlpha() == 1);
            AsyncOperation load = SceneManager.LoadSceneAsync(sceneToLoad.ScenePath);
            yield return new WaitUntil(() => load.isDone == true);
            StartCoroutine(_loadCanvas.LerpCanvas(1, 0));
            yield return new WaitUntil(() => _loadCanvas.GetIntAlpha() == 0);
        }
    }
}