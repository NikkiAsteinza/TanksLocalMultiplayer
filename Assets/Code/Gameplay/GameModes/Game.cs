using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

using Tanks.SceneManagement;

namespace Tanks.Gameplay.Logic
{
    public class Game : MonoBehaviour, IGame
    {
        public enum GameState
        {
            Idle,
            Started,
            Finished,
        }

        [SerializeField] protected bool _debug = false;

        [Header("Gameplay configurations")]
        [SerializeField] protected GameState DefaultInitstate = GameState.Idle;
        [SerializeField] protected int _lives = 3;
        [SerializeField] protected int _initialAmmunition = 10;
        [SerializeField] protected int PointsToFinish = 20;
        [SerializeField] protected int GameLoopRefreshInterval = 2;
        [SerializeField] protected string gameOverTitle;
        [Header("Gameplay canvas adjustments")]
        [SerializeField] protected Sprite _pointsImage;

        private GameState _state;
        protected List<Player> _players;
        private bool _gameOver;

        private void Awake()
        {
            SwitchGameToTargetState(DefaultInitstate);
        }

        public void InitGame()
        {   if(_debug)
                Debug.Log("Single player game init");
            SwitchGameToTargetState(GameState.Started);
        }

        protected void SwitchGameToTargetState(GameState state)
        {
            switch (state)
            {
                case GameState.Idle:
                    InitialSetup();
                    break;
                case GameState.Started:
                    _players = GameManager.Instance.GetPlayers();
                    StartCoroutine(GameLoopCoroutine());
                    break;
                case GameState.Finished:
                    OnGameFinished();
                    ScenesManager.Instance.GoToMainMenu(5);
                    _gameOver = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(_state), _state, null);
            }

            _state = state;
            if (_debug)
                Debug.Log("New state_ " + _state);
        }

        #region Protected methods
        protected virtual void GameLoopLogic()
        {
            if (_players.Any(x=>x.Points == PointsToFinish)){
                SwitchGameToTargetState(GameState.Finished);
            }
        }

        protected virtual void InitialSetup()
        {
            _players = GameManager.Instance.GetPlayers();
        }

        #region Not implemented methods on base

        protected virtual void OnGameFinished()
        {
            throw new NotImplementedException();
        }

        void IGame.AddPoints(int pointsToAdd)
        {
            throw new NotImplementedException();
        }

        #endregion

        #endregion

        #region Private Methods

        private IEnumerator GameLoopCoroutine()
        {
            while (!_gameOver)
            {
                GameLoopLogic();
                yield return new WaitForSeconds(GameLoopRefreshInterval);
            }
        }
        #endregion
    }
}