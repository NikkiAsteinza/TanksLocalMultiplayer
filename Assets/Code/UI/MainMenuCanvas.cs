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
        [SerializeField] TMP_Dropdown firstPlayerDropdownInputSelector;
        [SerializeField] TMP_Dropdown secondPlayerDropdownInputSelector;
        private void Awake()
        {
            singlePlayerButton.onClick.AddListener(SetSingleMode);
            multiplayerButton.onClick.AddListener(SetMultiplayerMode);
            exitButton.onClick.AddListener(QuitApp);
            startButton.onClick.AddListener(StartApp);
            firstPlayerDropdownInputSelector.onValueChanged.AddListener(SetFirstPlayerInput);
            secondPlayerDropdownInputSelector.onValueChanged.AddListener(SetSecondPlayerInput);
        }

        private void SetSecondPlayerInput(int arg0)
        {
            GameManager.Instance.SetPlayer2InputMode((InputMode)arg0);
        }

        private void SetFirstPlayerInput(int arg0)
        {
            GameManager.Instance.SetPlayer1InputMode((InputMode)arg0);
        }

        private void StartApp()
        {
            GameManager.Instance.InitGame();
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
        }

        private void SetSingleMode()
        {
            GameManager.Instance.SetSelectedMode(GameMode.SinglePlayer);
            gameModeSelectionPanel.SetActive(false);
            secondPlayerDropdownInputSelector.gameObject.SetActive(false);
            inputModeSelectionPanel.SetActive(true);
            startButton.gameObject.SetActive(true);
        }
    }
}
