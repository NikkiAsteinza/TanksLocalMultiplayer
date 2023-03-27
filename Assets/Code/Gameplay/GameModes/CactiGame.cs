using System.Collections.Generic;
using System.Linq;

using UnityEngine;

using Random = UnityEngine.Random;
using Tanks.Gameplay.Objects.Shootables;

namespace Tanks.Gameplay.Logic
{
    public class CactiGame : Game
    {
        [SerializeField] List<Transform> _spawnLimits;
        [Header("Gameplay configurations")]

        [SerializeField] private int _maxCacti = 5;
        [SerializeField] private ShootableGameplayObject _cactusPrefab;


        private List<ShootableGameplayObject> _cactiList;

        protected override void InitialSetup()
        {
            base.InitialSetup();
            _cactiList = new List<ShootableGameplayObject>();
            SpawnDisabledMaxCatiAmount();
            _players[0].Tank.InitCanvas(_lives, _initialAmmunition, 0, PointsToFinish, _pointsImage);
        }

        protected override void GameLoopLogic()
        {
            base.GameLoopLogic();

            int enabledCacti = _cactiList.Count(x => x.gameObject.activeInHierarchy);
            if (_debug)
                Debug.Log($"Spawned cactis: {enabledCacti}");
            SpawnCacti(enabledCacti);
        }

        private void SpawnCacti(int enabledCacti)
        {
            if (enabledCacti < _maxCacti)
            {
                Vector3 spawnPoint = GetRandomSpawnPoint();

                if (IsSpawnPointValid(spawnPoint))
                {

                    ShootableGameplayObject cactus = GetDisabledCactus();
                    cactus.transform.position = spawnPoint;
                    cactus.gameObject.SetActive(true);
                }
            }
        }


        private void SpawnDisabledMaxCatiAmount()
        {
            for (int i = 0; i < _maxCacti; i++)
            {
                _cactusPrefab.gameObject.SetActive(false);
                InstantiateCacti();
            }
        }

        private ShootableGameplayObject GetDisabledCactus()
        {
            ShootableGameplayObject disabledCactus = _cactiList.FirstOrDefault(
                x => !x.gameObject.activeInHierarchy);
            if (disabledCactus)
                return disabledCactus;
            else
                return InstantiateCacti();
        }

        private ShootableGameplayObject InstantiateCacti()
        {
            ShootableGameplayObject newCactus = Instantiate(_cactusPrefab, transform.position, Quaternion.identity);
            newCactus.SetOwner(this);
            _cactiList.Add(newCactus);
            return newCactus;
        }

        private Vector3 GetRandomSpawnPoint()
        {
            float minX = 0;
            float maxX = 0;
            float minY = 0;
            float maxY = 0;
            _spawnLimits.ForEach(x =>
            {
                minX = x.position.x < minX? x.position.x: minX;
                maxX = x.position.x > maxX ? x.position.x : maxX;
                minY = x.position.x < minY ? x.position.z : minY;
                maxY = x.position.x > maxY ? x.position.z : maxY;
            });

            return new Vector3(Random.Range(minX,maxX),0,Random.Range(minY,maxY));
        }

        private bool IsSpawnPointValid(Vector3 spawnPoint)
        {
            foreach (ShootableGameplayObject gameplayObject in _cactiList)
            {
                if (gameplayObject.gameObject.activeInHierarchy)
                {
                    float distance = Vector3.Distance(gameplayObject.transform.position, spawnPoint);
                    if (distance < 1.0f)
                    {
                        return false;
                    }
                }
            }

            Collider[] colliders = Physics.OverlapSphere(spawnPoint, 1.0f);
            foreach (Collider collider in colliders)
            {
                if (collider.CompareTag("Tank") || collider.CompareTag("Cactus") || collider.CompareTag("Props"))
                {
                    if (_debug)
                        Debug.Log("No valid spawn point");
                    return false;
                }
            }

            return true;
        }
    }
}