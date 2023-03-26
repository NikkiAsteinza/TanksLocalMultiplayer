using System.Collections.Generic;
using Tanks.Gameplay.Logic;
using Tanks.Players;
using Tanks.Controllers.Tank;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Tanks
{
    [RequireComponent(typeof(PlayerInputManager))]
    public class GameplayManager : MonoBehaviour
    {
        [SerializeField] bool _debug = false;
        [SerializeField] List<Transform> _spawnPoints;
        [SerializeField] private SinglePlayerGame _singlePlayerGame;
        [SerializeField] private MultiplayerGame _multiplayerGame;

        private void Start()
        {
            InitGame(GameManager.Instance.gameMode);
        }
        private void InitGame(GameMode gameMode)
        {
            if(_debug)
                Debug.Log("selected game mode:" + gameMode);
            if (gameMode == GameMode.Multiplayer)
            {
                CreateTankBySelectedInputMode(0);
                CreateTankBySelectedInputMode(1);
                InitMultiplayerGame();
            }
            else
            {
                CreateTankBySelectedInputMode(0);
                InitSinglePlayerGame();
            }
        }

        private void InitMultiplayerGame()
        {
            MultiplayerGame game = Instantiate(_multiplayerGame);
            game.InitGame();
        }

        private void InitSinglePlayerGame()
        {
            SinglePlayerGame game = Instantiate(_singlePlayerGame);
            game.InitGame();
        }

        private void CreateTankBySelectedInputMode(int playerIndex)
        {

            PlayerInput player = null;
            InputMode selectedMode = GameManager.Instance.GetPlayerInputMode(playerIndex);
            GameObject prefab = GameManager.Instance.GetPrefabToUse().gameObject;
            string controlScheme = "Player2";
            
            switch (selectedMode)
            {
                case InputMode.Keyboard:
                    controlScheme = "Keyboard";
                    player = PlayerInput.Instantiate(
                        prefab,
                        playerIndex,
                        controlScheme: controlScheme,
                        pairWithDevice: Keyboard.current);
                    Debug.Log($" Payer {playerIndex} -> Control scheme: {controlScheme}");
                    break;
                case InputMode.Gamepad:
                    Gamepad selectedPlayerGamepad = GameManager.Instance.GetPlayerGamepad(playerIndex);
                    if (_debug)
                        Debug.Log($"Player {playerIndex} desired gamepad: {selectedPlayerGamepad}");
                    InputDevice deviceToPair = selectedMode == InputMode.Gamepad
                ? selectedPlayerGamepad
                : InputSystem.devices[0];
                    if (_debug)
                        Debug.Log($"Player {playerIndex} found device to pair: " + deviceToPair);
                    player = PlayerInput.Instantiate(
                        prefab,
                        playerIndex,
                        controlScheme: controlScheme,
                        pairWithDevice: deviceToPair);
                    break;
            }

            PlayerTank _playerTank = player.gameObject.GetComponent<PlayerTank>();
            if(playerIndex == 1)
                _playerTank.SetAlternativeColor();
            Transform randomSpawnPoint = _spawnPoints[playerIndex];
            _playerTank.transform.SetPositionAndRotation(randomSpawnPoint.position, randomSpawnPoint.rotation);
        }
    }
}