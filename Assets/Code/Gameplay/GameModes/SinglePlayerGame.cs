using System.Linq;
using Tanks.Gameplay.Objects;
using UnityEngine;

namespace Tanks.Gameplay.Logic
{
    public class SinglePlayerGame : Game
    {
        [SerializeField] private Cactus _cactiPrefab;
        [SerializeField] private int _maxCacti = 5;
        [SerializeField] private Transform[] _cactiSpawnPoints;

        protected override void InitialSetup()
        {
            base.InitialSetup();
            //SpawnCactus();
        }

        protected override void GameLoopLogic()
        {
            int enabledCacti = GameplayObjects.Count(x => x.gameObject.activeInHierarchy && x.GetObjectType() == ObjectTypes.Cacti);
            Debug.Log($"Enabled cactis: {enabledCacti}");
            if (enabledCacti < _maxCacti)
            {
                Transform spawnPoint = GetRandomSpawnPoint(_cactiSpawnPoints);
                bool isValid = IsSpawnPointValid(spawnPoint);
                while (!isValid)
                {
                    spawnPoint = GetRandomSpawnPoint(_cactiSpawnPoints);
                    isValid = IsSpawnPointValid(spawnPoint);
                }

                GameplayObject cactus = GetDisabledCactus();
                if (cactus != null)
                {
                    cactus.transform.position = spawnPoint.position;
                    cactus.gameObject.SetActive(true);
                }
            }
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
            GameplayObject disabledCactus = GameplayObjects.FirstOrDefault(
                x => !x.gameObject.activeInHierarchy && x.GetObjectType() == ObjectTypes.Cacti);
            if (disabledCactus)
                return disabledCactus;
            
            return InstantiateCacti();
        }

        private Cactus InstantiateCacti()
        {
            Cactus newCactus = Instantiate(_cactiPrefab, transform.position, Quaternion.identity);
            newCactus.SetOwner(this);
            GameplayObjects.Add(newCactus);
            return newCactus;
        }
    }
}