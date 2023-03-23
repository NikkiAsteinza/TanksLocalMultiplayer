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
            Debug.Log("selected game mode:"+gameMode);
            if (gameMode == GameMode.Multiplayer)
            {
                CreateTankBySelectedInputMode(0);
                CreateTankBySelectedInputMode(1);
                //InitMultiplayerGame();
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
            InputMode selectedMode = GameManager.Instance.GetPlayerInputMode(playerIndex);
            Tank prefab = GameManager.Instance.GetPrefabToUse();
            Gamepad selectedPlayerGamepad = GameManager.Instance.GetPlayerGamepad(playerIndex);
            Debug.Log($"Player {playerIndex} desired gamepad: "+selectedPlayerGamepad);
            string controlScheme = playerIndex == 0 ? "Player1" : "Player2";
            InputDevice deviceToPair = selectedMode == InputMode.Gamepad
                ? selectedPlayerGamepad
                : InputSystem.devices[0];
            Debug.Log($"Player {playerIndex} found device to pair: "+deviceToPair);
            // Spawn players with specific devices.
            // var p1 = PlayerInput.Instantiate(
            //     prefab,
            //     playerIndex,
            //     controlScheme: controlScheme,
            //     pairWithDevice: deviceToPair);
            

            Transform randomSpawnPoint = _spawnPoints[playerIndex];
            Tank tank = Instantiate(prefab); 
            tank.transform.SetPositionAndRotation(randomSpawnPoint.position, randomSpawnPoint.rotation);
            tank.SetDevice(playerIndex);
        }
    }
}

