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
        internal void InitGame(GameMode gameMode)
        {
            int _randomSpawnPointIndex = Random.Range(0, _spawnPoints.Count());
            if (gameMode == GameMode.SinglePlayer)
            {
                Transform randomSpawnPoint = _spawnPoints[_randomSpawnPointIndex];
                Tank tank = Instantiate(GameManager.Instance.GetPrefabToUse());
                tank.transform.SetPositionAndRotation(randomSpawnPoint.position, randomSpawnPoint.rotation);
            }

            else
            {
                Transform randomSpawnPoint = _spawnPoints[_randomSpawnPointIndex];
                Tank tank = Instantiate(GameManager.Instance.GetPrefabToUse());
                tank.transform.SetPositionAndRotation(randomSpawnPoint.position, randomSpawnPoint.rotation);
                _spawnPoints.RemoveAt(_randomSpawnPointIndex);
                _randomSpawnPointIndex = Random.Range(0, _spawnPoints.Count());
                Transform randomSpawnPoint2 = _spawnPoints[_randomSpawnPointIndex];
                Tank tank2 = Instantiate(GameManager.Instance.GetPrefabToUse());
                tank2.transform.SetPositionAndRotation(randomSpawnPoint2.position, randomSpawnPoint2.rotation);
            }
            
            SinglePlayerGame game = Instantiate(_singlePlayerGame);
            game.InitGame();
        }
    }
}

