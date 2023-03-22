using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Tanks.Gameplay.Objects;
using Tanks.UI;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Tanks.Gameplay.Logic
{
    public class Game : MonoBehaviour, IGame
    {
        public enum GameState
        {
            Idle,
            Started,
            Finished,
            Restarting
        }

        [Header("Gameplay configurations")] [SerializeField]
        protected GameState DefaultInitstate = GameState.Idle;

        [SerializeField] protected int PointsToFinish = 10;
        [SerializeField] protected int GameLoopRefreshInterval = 2;
        [SerializeField] private float spawnInterval = 5f;
        [SerializeField] private int maxGameplayObjectsToInstantiate;
        [SerializeField] protected string GameFinishedMessage;

        [Header("Required references")] [SerializeField]
        protected Timer Timer;

        [SerializeField] protected AppCanvas GameFinishedCanvas;
        [SerializeField] protected TMP_Text PointsIndicator;


        [SerializeField] protected List<GameplayObject> GameplayObjects;
        [SerializeField] protected List<Transform> GameplayObjectsSpawnPoints;

        private GameState _state;
        private int _points = 0;
        private bool _gameOver;

        public void InitGame()
        {
            StartCoroutine(SpawnGameplayObjects());
            SwitchGameToTargetState(GameState.Started);
        }

        IEnumerator SpawnGameplayObjects()
        {
            while (true)
            {
                GameplayObject objectToSpawn = GameplayObjects[UnityEngine.Random.Range(0, GameplayObjects.Count)];
                Transform spawnPoint = GameplayObjectsSpawnPoints[UnityEngine.Random.Range(0, GameplayObjectsSpawnPoints.Count)];
                if (spawnPoint.childCount == 0)
                {
                    Instantiate(objectToSpawn, spawnPoint.position, Quaternion.identity, spawnPoint);
                }
                
                yield return new WaitForSeconds(spawnInterval);
            }
        }
        public void RestartGame()
        {
            SwitchGameToTargetState(GameState.Restarting);
        }

        private void Awake()
        {
            SwitchGameToTargetState(DefaultInitstate);
        }

        private void SwitchGameToTargetState(GameState state)
        {
            if (_state == state)
                return;
            switch (state)
            {
                case GameState.Idle:
                    InitialSetup();
                    break;
                case GameState.Started:
                    StartCoroutine(GameLoopCoroutine());
                    break;
                case GameState.Finished:
                    OnGameFinished();
                    break;
                case GameState.Restarting:
                    Restart();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(_state), _state, null);
            }

           
            _state = state;
            Debug.Log("New state_ " + _state);
        }

        #region Protected methods

        protected virtual void InitialSetup()
        {
            GameFinishedCanvas.SetOwner(this);
            GameFinishedCanvas.FadeInCanvas();
        }

        protected virtual void OnGameFinished()
        {
            Timer.Stop();
            GameFinishedCanvas.gameObject.SetActive(true);
            GameFinishedCanvas.SetMessageText(GameFinishedMessage);
        }

        protected virtual void Restart()
        {
            GameFinishedCanvas.FadeOutCanvas();
            Timer.Reset();
            UpdatePoints(true);
            DisableAllGameplayObjects();
        }

        #region Not implemented methods on base

        protected virtual void GameLoopLogic()
        {
            throw new System.NotImplementedException();
        }

        public virtual  void OnGameplayObjectDisabled(GameplayObject gameplayObject)
        {
            throw new System.NotImplementedException();
        }

        public virtual void OnGamePlayObjectEnabled(GameplayObject gameplayObject)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #endregion

        #region Private Methods
        
        private IEnumerator GameLoopCoroutine()
        {
            while (!_gameOver)
             {
                 _gameOver = _points == PointsToFinish;
                 if (_gameOver)
                 {
                     SwitchGameToTargetState(GameState.Finished);
                     yield break;
                 }

                GameLoopLogic();
                yield return new WaitForSeconds(GameLoopRefreshInterval);
            }
        }

        protected void UpdatePoints(bool reset)
        {
            _points = reset ? 0 : _points + 1;
            PointsIndicator.text = _points.ToString();
        }

        private void DisableAllGameplayObjects()
        {
            GameplayObjects.ForEach(x => x.gameObject.SetActive(false));
        }

        protected Transform GetRandomSpawnPoint()
        {
            // Select a random spawn point from the list
            int index = Random.Range(0, GameplayObjectsSpawnPoints.Count);
            Transform spawnPoint = GameplayObjectsSpawnPoints[index];
            return spawnPoint;
        }

        protected bool IsSpawnPointValid(Transform spawnPoint)
        {
            // Check if the selected spawn point is too close to another enabled cactus
            foreach (GameplayObject gameplayObject in GameplayObjects)
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
                if (collider.CompareTag("Tank") || collider.CompareTag("Cactus"))
                {
                    return false;
                }
            }

            return true;
        }
        #endregion
    }
}