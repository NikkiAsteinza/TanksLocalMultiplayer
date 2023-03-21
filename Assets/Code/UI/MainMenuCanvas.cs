using Tanks.SceneManagement;
using Tanks.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Tanks.MainMenu.Canvas
{
    public class MainMenuCanvas : MonoBehaviour
    {
        [Header("Containers")]
        [SerializeField] GameObject gameModeSelectionPanel;
        [SerializeField] GameObject inputModeSelectionPanel;
        
        [Header("Buttons")]
        [SerializeField] Button singlePlayerButton;
        [SerializeField] Button multiplayerButton;
        [SerializeField] Button exitButton;
        [SerializeField] Button startButton;
        
        [Header("Dropdowns")]
        [SerializeField] PlayerInputSelector inputSelectorPrefab;
        private void Awake()
        {
            singlePlayerButton.onClick.AddListener(SetSingleMode);
            multiplayerButton.onClick.AddListener(SetMultiplayerMode);
            exitButton.onClick.AddListener(QuitApp);
            startButton.onClick.AddListener(StartApp);
        }
        
        private void StartApp()
        {
            ScenesManager.Instance.GoToScene(ScenesManager.Scenes.Gameplay);
        }

        private void QuitApp()
        {
            GameManager.Instance.Quit();
        }

        private void SetMultiplayerMode()
        {
            GameManager.Instance.SetSelectedMode(GameMode.Multiplayer);
            gameModeSelectionPanel.SetActive(false);
            inputModeSelectionPanel.SetActive(true);
            startButton.gameObject.SetActive(true);
            GeneratePlayerInputModeSelectors();
        }

        private void GeneratePlayerInputModeSelectors()
        {
            for (int i = 0; i < GameManager.Instance.totalPlayers; i++)
            {
                PlayerInputSelector selector = Instantiate(inputSelectorPrefab, inputModeSelectionPanel.transform);
                selector.SetOwner(i);
            }
        }

        private void SetSingleMode()
        {
            GameManager.Instance.SetSelectedMode(GameMode.SinglePlayer);
            gameModeSelectionPanel.SetActive(false);
            inputModeSelectionPanel.SetActive(true);
            startButton.gameObject.SetActive(true);
            GeneratePlayerInputModeSelectors();
        }
    }
}
