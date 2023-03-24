using System.Collections.Generic;
using Tanks.Gameplay.Logic;
using Tanks.Players;
using Tanks.Tanks;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Tanks
{
    [RequireComponent(typeof(PlayerInputManager))]
    public class GameplayManager : MonoBehaviour
    {
        [SerializeField] List<Transform> _spawnPoints;
        [SerializeField] private SinglePlayerGame _singlePlayerGame;
        [SerializeField] private MultiplayerGame _multiplayerGame;

        private void Start()
        {
            InitGame(GameManager.Instance.gameMode);
        }
        private void InitGame(GameMode gameMode)
        {
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
            string controlScheme = playerIndex == 0 ? "Keyboard" : "Gamepad";

            switch (selectedMode)
            {
                case InputMode.Keyboard:
                    player = PlayerInput.Instantiate(
                        prefab,
                        playerIndex,
                        controlScheme: controlScheme);
                    break;
                case InputMode.Gamepad:
                    Gamepad selectedPlayerGamepad = GameManager.Instance.GetPlayerGamepad(playerIndex);
                    Debug.Log($"Player {playerIndex} desired gamepad: " + selectedPlayerGamepad);
                    InputDevice deviceToPair = selectedMode == InputMode.Gamepad
                ? selectedPlayerGamepad
                : InputSystem.devices[0];
                    Debug.Log($"Player {playerIndex} found device to pair: " + deviceToPair);
                    // Spawn players with specific devices.
                    player = PlayerInput.Instantiate(
                        prefab,
                        playerIndex,
                        controlScheme: controlScheme,
                        pairWithDevice: deviceToPair);
                    break;
            }

            Tank _playerTank = player.gameObject.GetComponent<Tank>();
            //Paint
            if(playerIndex == 1)
                _playerTank.SetAlternativeColor();
            // PLace
            Transform randomSpawnPoint = _spawnPoints[playerIndex];
            _playerTank.transform.SetPositionAndRotation(randomSpawnPoint.position, randomSpawnPoint.rotation);
            // Input
            _playerTank.SetDevice(playerIndex);

        }
    }
}

