using Tanks.Gameplay;
using Tanks.SceneManagement;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Tanks.UI
{
    public class AppCanvas : MonoBehaviour
    {
        [SerializeField]
        private bool _fadeinOnStart = false;
        [Header("Containers")] [SerializeField]
        private CanvasFader _canvasFader;

        [SerializeField] private GameObject _messageContainer;
        [SerializeField] private GameObject _gameModeSelectionPanel;
        [SerializeField] private GameObject _inputModeSelectionPanel;
        [SerializeField] private TMP_Text _finishMessage;
        [SerializeField] private TMP_Text _title;
        [Header("Buttons")] [SerializeField] Button _singlePlayerButton;
        [SerializeField] private Button _multiplayerButton;
        [SerializeField] private Button _exitButton;
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _mainMenuButton;

        [Header("Dropdowns")]
        [SerializeField] private PlayerInputSelector _inputSelectorPrefab;

        private IGame _owner;

        public void SetOwner(IGame game)
        {
            _owner = game;
        }

        public void SetMessageText(string title, string message = null)
        {
            _title.text = title;
            if(string.IsNullOrEmpty(message))
                _finishMessage.text = message;
            _finishMessage.gameObject.SetActive(true);
            _messageContainer.SetActive(true);
        }

        private void Awake()
        {
            _inputModeSelectionPanel.gameObject.SetActive(false);
            _restartButton.gameObject.SetActive(false);

            _singlePlayerButton.onClick.AddListener(SetSingleMode);
            _multiplayerButton.onClick.AddListener(SetMultiplayerMode);
            _exitButton.onClick.AddListener(QuitApp);
            _startButton.onClick.AddListener(StartApp);
            _mainMenuButton.onClick.AddListener(GotoMainMenu);
        }

        private void GotoMainMenu()
        {
            FadeOutCanvas();
            ScenesManager.Instance.GoToScene(ScenesManager.Scenes.MainMenu);
        }

        private void Start()
        {
            if (_fadeinOnStart)
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
            _gameModeSelectionPanel.SetActive(false);
            _inputModeSelectionPanel.SetActive(true);
        }

        private void CreateSelectors()
        {
            for (int i = 0; i < GameManager.Instance.totalPlayers; i++)
            {
                PlayerInputSelector selector = Instantiate(_inputSelectorPrefab, _inputModeSelectionPanel.transform);
                selector.SetOwner(i);
            }
        }

        private void SetSingleMode()
        {
            GameManager.Instance.SetSelectedMode(GameMode.SinglePlayer);
            CreateSelectors();
            _gameModeSelectionPanel.SetActive(false);
            _inputModeSelectionPanel.SetActive(true);
        }

        public void FadeInCanvas()
        {
            if (!_canvasFader)
                return;

            _canvasFader.gameObject.SetActive(true);
            _canvasFader.FadeIn();
        }

        public void FadeOutCanvas()
        {
            if (!_canvasFader)
                return;
            _canvasFader.FadeOut();
        }

        public int GetIntAlpha()
        {
            return _canvasFader.GetIntAlpha();
        }

        public void EnableEndButtons()
        {
            _restartButton.gameObject.SetActive(true);
            _mainMenuButton.gameObject.SetActive(true);
        }
    }
}