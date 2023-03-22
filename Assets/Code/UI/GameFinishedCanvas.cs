using Tanks.SceneManagement;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameFinishedCanvas : MonoBehaviour
{
    [SerializeField] TMP_Text _finishMessage;
    [SerializeField] Button _restartButton;
    [SerializeField] Button _mainMenuButton;
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
        if(_finishMessage || _restartButton || _mainMenuButton)
        {
            Debug.LogWarning("Missing reference in inspector");
        }

        _restartButton.onClick.AddListener(OnStartButtonClicked);
        _mainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);
    }

    private void OnMainMenuButtonClicked()
    {
        ScenesManager.Instance.GoToScene(ScenesManager.Scenes.MainMenu);
    }

    private void OnStartButtonClicked()
    {
        _owner.Restart();
    }
}
