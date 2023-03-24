using System.Collections.Generic;
using System.Linq;
using Tanks.Gameplay.Objects;
using TMPro;
using UnityEngine;

namespace Tanks.Gameplay.Logic
{
    public class SinglePlayerGame : Game
    {
        [SerializeField] protected int PointsToFinish = 20;
        [SerializeField] private readonly int _maxCacti = 5;
        [SerializeField] private Cactus _cactusPrefab;
        [SerializeField] private Transform[] _cactiSpawnPoints;
        [SerializeField] protected TMP_Text PointsIndicator;
        [SerializeField] protected TMP_Text GoalPoints;

        private List<Cactus> _cactiList;
        private int _points = 0;

        protected override void InitialSetup()
        {
            base.InitialSetup();
            if (GoalPoints)
                GoalPoints.text = PointsToFinish.ToString();
            _cactiList = new List<Cactus>();
            SpawnDisabledMaxCatiAmount();
        }

        protected override void GameLoopLogic()
        {
            int enabledCacti = _cactiList.Count(x => x.gameObject.activeInHierarchy);
            Debug.Log($"Spawned cactis: {enabledCacti}");
            if (enabledCacti < _maxCacti)
            {
                Transform spawnPoint = GetRandomSpawnPoint();
                bool isValid = IsSpawnPointValid(spawnPoint);
                if (isValid)
                {
                    Cactus cactus = GetDisabledCactus();
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

        private void SpawnDisabledMaxCatiAmount()
        {
            for (int i = 0; i < _maxCacti; i++)
            {
                _cactusPrefab.gameObject.SetActive(false);
                InstantiateCacti();
            }
        }

        Cactus GetDisabledCactus()
        {
            Cactus disabledCactus = _cactiList.FirstOrDefault(
                x => !x.gameObject.activeInHierarchy);
            if (disabledCactus)
                return disabledCactus;
            else
                return InstantiateCacti();
        }

        private Cactus InstantiateCacti()
        {
            Cactus newCactus = Instantiate(_cactusPrefab, transform.position, Quaternion.identity);
            newCactus.SetOwner(this);
            _cactiList.Add(newCactus);
            return newCactus;
        }

        private void DisableAllGameplayObjects()
        {
            _cactiList.ForEach(x => x.gameObject.SetActive(false));
        }

        private Transform GetRandomSpawnPoint()
        {
            int index = Random.Range(0, _cactiSpawnPoints.Length);
            Transform spawnPoint = _cactiSpawnPoints[index];
            return spawnPoint;
        }

        private bool IsSpawnPointValid(Transform spawnPoint)
        {
            foreach (Cactus gameplayObject in _cactiList)
            {
                if (gameplayObject.gameObject.activeInHierarchy)
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
                if (collider.CompareTag("Tank") || collider.CompareTag("Cactus"))
                {
                    Debug.Log("No valid spawn point");
                    return false;
                }
            }

            return true;
        }

        private void UpdatePoints(bool reset)
        {
            _points = reset ? 0 : _points + 1;
            if (PointsIndicator)
                PointsIndicator.text = _points.ToString();
        }
    }
}