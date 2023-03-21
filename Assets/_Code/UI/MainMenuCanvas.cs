using UnityEngine;
using UnityEngine.UI;

namespace Tanks.MainMenu.Canvas
{
    public class MainMenuCanvas : MonoBehaviour
    {
        [SerializeField] Button singlePlayerButton;
        [SerializeField] Button multiplayerButton;
        [SerializeField] Button exitButton;

        private void Awake()
        {
            singlePlayerButton.onClick.AddListener(SetSingleMode);
            singlePlayerButton.onClick.AddListener(SetMultiplayerMode);
            singlePlayerButton.onClick.AddListener(QuitApp);
        }

        private void QuitApp()
        {
            GameManager.Instance.Quit();
        }

        private void SetMultiplayerMode()
        {
            GameManager.Instance.SetSelectedMode(GameMode.Multiplayer);
        }

        private void SetSingleMode()
        {
            GameManager.Instance.SetSelectedMode(GameMode.SinglePlayer);

        }
    }
}
