using System.Collections.Generic;
using System.Linq;
using Tanks.Gameplay.Logic;
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
            }
            InitSinglePlayerGame();
            //CreateTankBySelectedInputMode(0);
           
        }

        private void InitSinglePlayerGame()
        {
            SinglePlayerGame game = Instantiate(_singlePlayerGame);
            game.InitGame();
        }

        private void CreateTankBySelectedInputMode(int playerIndex)
        {
            Transform randomSpawnPoint = _spawnPoints[playerIndex];
            Tank tank = Instantiate(GameManager.Instance.GetPrefabToUse());
            tank.SetInputDevice(GameManager.Instance.GetPlayerInputMode(playerIndex));
            tank.transform.SetPositionAndRotation(randomSpawnPoint.position, randomSpawnPoint.rotation);
        }
    }
}

