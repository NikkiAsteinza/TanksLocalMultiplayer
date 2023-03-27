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
        [SerializeField] private Button _backButton;

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
            if(!string.IsNullOrEmpty(message))
                _finishMessage.text = message;
            _finishMessage.gameObject.SetActive(true);
            _messageContainer.SetActive(true);
        }

         void ResetInputDeviceSelectors(){
            _inputModeSelectionPanel.gameObject.SetActive(false);
            _gameModeSelectionPanel.gameObject.SetActive(true);
            _backButton.gameObject.SetActive(false);
        }

        private void Awake()
        {
            _inputModeSelectionPanel.gameObject.SetActive(false);
            _backButton.gameObject.SetActive(false);

            _singlePlayerButton.onClick.AddListener(SetSingleMode);
            _multiplayerButton.onClick.AddListener(SetMultiplayerMode);
            _exitButton.onClick.AddListener(QuitApp);
            _startButton.onClick.AddListener(StartApp);
            _backButton.onClick.AddListener(ResetInputDeviceSelectors);
        }


        private void Start()
        {
            if (_fadeinOnStart)
                FadeInCanvas();
        }

        private void StartApp()
        {
            Debug.Log("Start app clicked");
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
            _backButton.gameObject.SetActive(true);
        }

        private void CreateSelectors()
        {
            if (_inputModeSelectionPanel.transform.childCount>0)
                return;

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
            _backButton.gameObject.SetActive(true);
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
    }
}