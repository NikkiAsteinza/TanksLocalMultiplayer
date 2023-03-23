using System.Collections.Generic;
using Tanks.Gameplay.Logic;
using Tanks.Players;
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
            CreateTankBySelectedInputMode(0);
            if (gameMode == GameMode.Multiplayer)
            {
                CreateTankBySelectedInputMode(1);
                InitMultiplayerGame();
            }
            InitSinglePlayerGame();
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
            // Spawn players with specific devices.
            var p1 = PlayerInput.Instantiate(
                GameManager.Instance.GetPrefabToUse().gameObject,
                controlScheme: selectedMode.ToString(),
                pairWithDevice:  selectedMode  == InputMode.Gamepad? Gamepad.all[0] : InputSystem.devices[0]);

            Transform randomSpawnPoint = _spawnPoints[playerIndex];
            p1.transform.SetPositionAndRotation(randomSpawnPoint.position, randomSpawnPoint.rotation);
        }
    }
}

