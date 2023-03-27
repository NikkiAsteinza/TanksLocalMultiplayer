using TMPro;
using UnityEngine;
using UnityEngine.UI;

using Tanks.SceneManagement;

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

        [Header("Dropdowns")]
        [SerializeField] private PlayerInputSelector _inputSelectorPrefab;

        private void Awake()
        {
            _inputModeSelectionPanel.gameObject.SetActive(false);

            _singlePlayerButton.onClick.AddListener(SetSingleMode);
            _multiplayerButton.onClick.AddListener(SetMultiplayerMode);
            _exitButton.onClick.AddListener(QuitApp);
            _startButton.onClick.AddListener(StartApp);
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
            _gameModeSelectionPanel.SetActive(false);
            _inputModeSelectionPanel.SetActive(true);
            CreateSelectors();
        }

        private void CreateSelectors()
        {
            if (_inputModeSelectionPanel.transform.childCount == GameManager.Instance.MaxPlayers)
                return;
            for (int i = 0; i < GameManager.Instance.MaxPlayers; i++)
            {
                PlayerInputSelector selector = Instantiate(_inputSelectorPrefab, _inputModeSelectionPanel.transform);
                selector.SetOwner(i);
            }
        }

        private void SetSingleMode()
        {
            GameManager.Instance.SetSelectedMode(GameMode.SinglePlayer);
            _gameModeSelectionPanel.SetActive(false);
            _inputModeSelectionPanel.SetActive(true);
            CreateSelectors();
            _inputModeSelectionPanel.transform.GetChild(1).gameObject.SetActive(false);
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
    }
}