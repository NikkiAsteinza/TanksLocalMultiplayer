using System.Collections.Generic;
using System.Linq;
using Tanks.Tanks;
using UnityEngine;

namespace Tanks.Gameplay
{
    public class GameplayManager : MonoBehaviour
    {
        [SerializeField] List<Transform> _spawnPoints;
        [SerializeField] SinglePlayerGame _singlePlayerGame;

        private void Start()
        {
            InitGame();
        }
        internal void InitGame()
        {
            int _randomSpawnPointIndex = Random.Range(0, _spawnPoints.Count());
            for (int i = 0; i < GameManager.Instance.totalPlayers; i++)
            {
                Tank tank = Instantiate(GameManager.Instance.GetPrefabToUse());
                Transform randomSpawnPoint = _spawnPoints[_randomSpawnPointIndex];
                tank.transform.SetPositionAndRotation(randomSpawnPoint.position, randomSpawnPoint.rotation);
                tank.SetSelectedInputMode(i);
                _spawnPoints.RemoveAt(_randomSpawnPointIndex);
            }

            SinglePlayerGame game = Instantiate(_singlePlayerGame);
            game.StartGame();
        }
    }
}

