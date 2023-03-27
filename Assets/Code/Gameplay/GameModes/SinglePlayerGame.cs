using System.Collections.Generic;
using System.Linq;

using TMPro;
using UnityEngine;

using Tanks.Gameplay.Objects;
using System;
using Random = UnityEngine.Random;
using UnityEngine.AI;

namespace Tanks.Gameplay.Logic
{
    public class SinglePlayerGame : Game
    {
        [SerializeField] List<Transform> _spawnLimits;
        [Header("Gameplay configurations")]
        [SerializeField] protected int PointsToFinish = 20;
        [SerializeField] private int _maxCacti = 5;
        [SerializeField] private Cactus _cactusPrefab;

        [Header("Gameplay canvas references")]
        [SerializeField] private TMP_Text _pointsIndicator;
        [SerializeField] private TMP_Text _goalPoints;

        private List<Cactus> _cactiList;
        private int _points = 0;

        protected override void InitialSetup()
        {
            base.InitialSetup();
            if (_goalPoints)
                _goalPoints.text = PointsToFinish.ToString();
            _cactiList = new List<Cactus>();
            SpawnDisabledMaxCatiAmount();
        }

        protected override void GameLoopLogic()
        {
            if (_points == PointsToFinish)
                SwitchGameToTargetState(GameState.Finished);

            int enabledCacti = _cactiList.Count(x => x.gameObject.activeInHierarchy);
            if (_debug)
                Debug.Log($"Spawned cactis: {enabledCacti}");
            if (enabledCacti < _maxCacti)
            {
                Vector3 spawnPoint = GetRandomSpawnPoint();

                if (IsSpawnPointValid(spawnPoint))
                {
                    Cactus cactus = GetDisabledCactus();
                    cactus.transform.position = spawnPoint;
                    cactus.gameObject.SetActive(true);
                }
            }
        }
        override protected void OnGameFinished()
        {
            Timer.Stop();
            GameplayCanvas.SetMessageText(gameOverTitle, Math.Round(Timer.Time,2).ToString());
            GameplayCanvas.FadeInCanvas();
        }


        public override void OnGameplayObjectDisabled(GameplayObject gameplayObject)
        {
            if (_debug)
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

        private Cactus GetDisabledCactus()
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
            foreach (Cactus gameplayObject in _cactiList)
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

        private void UpdatePoints(bool reset)
        {
            _points = reset ? 0 : _points + 1;
            if (_pointsIndicator)
                _pointsIndicator.text = _points.ToString();
        }
    }
}