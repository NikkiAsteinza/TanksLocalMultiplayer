
using Tanks.Gameplay;
using Tanks.Players;
using Tanks.SceneManagement;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Tanks.UI
{
    public class AppCanvas : MonoBehaviour
    {
        [SerializeField] private bool _fadeinOnStart = false;
        [Header("Containers")]
        [SerializeField]private CanvasFader _canvasFader;
        [SerializeField] private GameObject messageContainer;
        [SerializeField] GameObject gameModeSelectionPanel;
        [SerializeField] GameObject inputModeSelectionPanel;
        [SerializeField] TMP_Text _finishMessage;
        [Header("Buttons")] [SerializeField] Button singlePlayerButton;
        [SerializeField] Button multiplayerButton;
        [SerializeField] Button exitButton;
        [SerializeField] Button startButton;
        [SerializeField] Button restartButton;
        [SerializeField] private Button mainMenuButton;

        [Header("Dropdowns")] [SerializeField] PlayerInputSelector inputSelectorPrefab;

        private  IGame _owner;
        public void SetOwner(IGame game)
        {
            _owner = game;
        }

        public void SetMessageText(string message)
        {

            _finishMessage.text = message;
        }

        private void Awake()
        {
            startButton.gameObject.SetActive(false);
            inputModeSelectionPanel.gameObject.SetActive(false);
            restartButton.gameObject.SetActive(false);
            messageContainer.SetActive(false);
            
            singlePlayerButton.onClick.AddListener(SetSingleMode);
            multiplayerButton.onClick.AddListener(SetMultiplayerMode);
            exitButton.onClick.AddListener(QuitApp);
            startButton.onClick.AddListener(StartApp);
            mainMenuButton.onClick.AddListener(GotoMainMenu);
        }

        private void GotoMainMenu()
        {
            FadeOutCanvas();
            ScenesManager.Instance.GoToScene(ScenesManager.Scenes.MainMenu);
        }

        private void Start()
        {
            if(_fadeinOnStart)
                FadeInCanvas();

        }

        private void StartApp()
        {
            FadeOutCanvas();
            ScenesManager.Instance.GoToScene(ScenesManager.Scenes.Gameplay);
        }

        private void QuitApp()
        {
            GameManager.Instance.Quit();
        }

        private void SetMultiplayerMode()
        {
            GameManager.Instance.SetSelectedMode(GameMode.Multiplayer);
            CreateSelectors();
            gameModeSelectionPanel.SetActive(false);
            inputModeSelectionPanel.SetActive(true);
            startButton.gameObject.SetActive(true);
        }

        private void CreateSelectors()
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
            CreateSelectors();
            gameModeSelectionPanel.SetActive(false);
            inputModeSelectionPanel.SetActive(true);
            startButton.gameObject.SetActive(true);
        }

        public void FadeInCanvas()
        {
            if(!_canvasFader)
                return;
            
            _canvasFader.gameObject.SetActive(true);
            _canvasFader.FadeIn();
        }

        public void FadeOutCanvas()
        {
            if(!_canvasFader)
                return;
            _canvasFader.FadeOut();
        }

        public int GetIntAlpha()
        {
            return _canvasFader.GetIntAlpha();
        }
        
    }
}