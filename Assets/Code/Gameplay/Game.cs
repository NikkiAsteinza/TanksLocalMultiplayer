using System;
using System.Collections;
using Tanks.Gameplay.Objects;
using Tanks.UI;
using TMPro;
using UnityEngine;

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

        [SerializeField] protected int GameLoopRefreshInterval = 2;
        [SerializeField] protected string gameOverTitle;

        [Header("UI references")] [SerializeField]
        protected Timer Timer;

        [SerializeField] protected AppCanvas GameplayCanvas;

        private GameState _state;

        private bool _gameOver;

        public void InitGame()
        {
            Debug.Log("Single player game init");
            SwitchGameToTargetState(GameState.Started);
        }

        public void RestartGame()
        {
            SwitchGameToTargetState(GameState.Restarting);
        }

        private void Awake()
        {
            SwitchGameToTargetState(DefaultInitstate);
        }

        protected void SwitchGameToTargetState(GameState state)
        {
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
                    _gameOver = true;
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
            GameplayCanvas.SetOwner(this);
            GameplayCanvas.FadeOutCanvas();
        }

        protected virtual void OnGameFinished()
        {
            Timer.Stop();
            GameplayCanvas.SetMessageText(gameOverTitle);
            GameplayCanvas.EnableEndButtons();
            GameplayCanvas.FadeInCanvas();
        }

        protected virtual void Restart()
        {
            _gameOver = true;
            GameplayCanvas.FadeOutCanvas();
            Timer.Reset();
          
        }

        #region Not implemented methods on base

        protected virtual void GameLoopLogic()
        {
            throw new System.NotImplementedException();
        }

        public virtual void OnGameplayObjectDisabled(GameplayObject gameplayObject)
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
                GameLoopLogic();
                yield return new WaitForSeconds(GameLoopRefreshInterval);
            }
        }
        
        #endregion
    }
}