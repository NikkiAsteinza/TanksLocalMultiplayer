using System.Collections.Generic;
using System.Linq;
using Tanks.Gameplay.Objects;
using UnityEngine;

namespace Tanks.Gameplay.Logic
{
    public class SinglePlayerGame : Game
    {
        [SerializeField] private readonly int _maxCacti = 5;
        [SerializeField] private Cactus _cactiPrefab;
        [SerializeField] private Transform[] _cactiSpawnPoints;
        private List<GameplayObject> _gameplayObjects;
        protected override void InitialSetup()
        {
            base.InitialSetup();
            _gameplayObjects = new List<GameplayObject>();
            SpawnCactus();
        }

        protected override void GameLoopLogic()
        {
            int enabledCacti = _gameplayObjects.Count();
            Debug.Log($"Enabled cactis: {enabledCacti}");
            if (enabledCacti < _maxCacti)
            {
                Transform spawnPoint = GetRandomSpawnPoint();
                bool isValid = IsSpawnPointValid(spawnPoint);
                if (!isValid)
                {
                   List<Transform> reducedSpawnList = _cactiSpawnPoints.ToList();
                   reducedSpawnList.Remove(spawnPoint);
                   for (int i = 0; i < 3; i++) {
                       spawnPoint = GetRandomSpawnPoint();
                       isValid = IsSpawnPointValid(spawnPoint);
                       if (!isValid)
                           break;
                   }
                }
                GameplayObject cactus = GetDisabledCactus();
                if (cactus != null)
                {
                    cactus.transform.position = spawnPoint.position;
                    cactus.gameObject.SetActive(true);
                }
            }
        }
        protected override void Restart()
        {
            base.Restart();
            DisableAllGameplayObjects();
        }
        public override void OnGameplayObjectDisabled(GameplayObject gameplayObject)
        {
            Debug.Log("Cactus destroys");
            UpdatePoints(false);
        }

        private void SpawnCactus()
        {
            for (int i = 0; i < _maxCacti ; i++)
            {
                _cactiPrefab.gameObject.SetActive(false);
                Cactus cactus = InstantiateCacti();
            }
        }

        GameplayObject GetDisabledCactus()
        {
            GameplayObject disabledCactus = _gameplayObjects.FirstOrDefault(
                x => !x.gameObject.activeInHierarchy);
            if (disabledCactus)
                return disabledCactus;
            
            return InstantiateCacti();
        }

        private Cactus InstantiateCacti()
        {
            Cactus newCactus = Instantiate(_cactiPrefab, transform.position, Quaternion.identity);
            newCactus.SetOwner(this);
            _gameplayObjects.Add(newCactus);
            return newCactus;
        }
        private void DisableAllGameplayObjects()
        {
            _gameplayObjects.ForEach(x => x.gameObject.SetActive(false));
        }

        private Transform GetRandomSpawnPoint()
        {
            int index = Random.Range(0, _cactiSpawnPoints.Length);
            Transform spawnPoint = _cactiSpawnPoints[index];
            return spawnPoint;
        }
        
        private bool IsSpawnPointValid(Transform spawnPoint)
        {
            foreach (GameplayObject gameplayObject in _gameplayObjects)
            {
                if (gameplayObject.gameObject.activeSelf)
                {
                    float distance = Vector3.Distance(gameplayObject.transform.position, spawnPoint.position);
                    if (distance < 1.0f)
                    {
                        return false;
                    }
                }
            }

            // Check if the selected spawn point is too close to a tank object
            Collider[] colliders = Physics.OverlapSphere(spawnPoint.position, 1.0f);
            foreach (Collider collider in colliders)
            {
                if (collider && !collider.CompareTag("Floor") || !collider.CompareTag("Cactus") )
                {
                    return false;
                }
            }

            return true;
        }
    }
}