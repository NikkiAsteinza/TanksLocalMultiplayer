using System.Collections.Generic;
using System.Linq;
using Tanks.Tanks;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Tanks.Gameplay
{

    [RequireComponent(typeof(PlayerInputManager))]
    public class GameplayManager : MonoBehaviour
    {
        [SerializeField] Tank _tankPrefab;
        [SerializeField] List<Transform> _spawnPoints;

        private PlayerInputManager _pim;

        private void Start()
        {
            InitGame(GameManager.Instance.GameMode);
        }
        internal void InitGame(GameMode gameMode)
        {
            _pim = GetComponent<PlayerInputManager>();
            int _randomSpawnPointIndex = Random.Range(0, _spawnPoints.Count());
            if (gameMode == GameMode.SinglePlayer)
            {
                Transform randomSpawnPoint = _spawnPoints[_randomSpawnPointIndex];
                Tank tank = Instantiate(_tankPrefab, randomSpawnPoint);
            }
            else
            {
                Transform randomSpawnPoint = _spawnPoints[_randomSpawnPointIndex];
                Tank tank = Instantiate(_tankPrefab, randomSpawnPoint);

                _spawnPoints.RemoveAt(_randomSpawnPointIndex);
                _randomSpawnPointIndex = Random.Range(0, _spawnPoints.Count());
                Transform randomSpawnPoint2 = _spawnPoints[_randomSpawnPointIndex];
                Tank tank2 = Instantiate(_tankPrefab, randomSpawnPoint2);
            }
        }
    }
}

