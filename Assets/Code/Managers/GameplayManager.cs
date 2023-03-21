using System.Collections.Generic;
using System.Linq;
using Tanks.Tanks;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Tanks.Gameplay
{
    public class GameplayManager : MonoBehaviour
    {
        [SerializeField] List<Transform> _spawnPoints;
        
        private void Start()
        {
            InitGame(GameManager.Instance.gameMode);
        }
        internal void InitGame(GameMode gameMode)
        {
            int _randomSpawnPointIndex = Random.Range(0, _spawnPoints.Count());
            for (int i = 0; i < GameManager.Instance.totalPlayers; i++)
            {
                Tank tank = Instantiate(GameManager.Instance.GetPrefabToUse());
                Transform randomSpawnPoint = _spawnPoints[_randomSpawnPointIndex];
                tank.transform.SetPositionAndRotation(randomSpawnPoint.position, randomSpawnPoint.rotation);
                _spawnPoints.RemoveAt(_randomSpawnPointIndex);
            }
        }
    }
}

