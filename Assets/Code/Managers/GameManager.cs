using System;
using System.Collections.Generic;
using Tanks.Players;
using Tanks.Tanks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Tanks
{
    public enum GameMode
    {
        SinglePlayer,
        Multiplayer,
    }

    public class GameManager : SingletonBehaviour<GameManager>
    {
        [SerializeField] internal Tank _singlePlayerTank;
        [SerializeField] internal Tank _multiPlayerTank;
        [SerializeField] internal int _playerLives = 3;
        [SerializeField] internal int _initialAmmunition = 10;
        
        [SerializeField] private List<GameplayScriptable> _gameplayObjectList;

        [Header("Debug purposes, no need assignment")] [SerializeField]
        GameMode _selectedGameMode;

        [SerializeField] private List<Player> _players;
        public GameMode gameMode => _selectedGameMode;
        public int totalPlayers => _players.Count;
        public List<GameplayScriptable> gameplayObjects => _gameplayObjectList;

        private void Awake()
        {
            _players = new List<Player>();
        }

        public void SetSelectedMode(GameMode selectedMode)
        {
            _selectedGameMode = selectedMode;
            HandlePlayersCreation(selectedMode);
        }

        private void HandlePlayersCreation(GameMode selectedMode)
        {
            switch (selectedMode)
            {
                case GameMode.SinglePlayer:
                    Player player = CreatePlayer();
                    _players.Add(player);
                    break;
                case GameMode.Multiplayer:
                    Player player1 = CreatePlayer();
                    _players.Add(player1);
                    Player player2 = CreatePlayer();
                    _players.Add(player2);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(selectedMode), selectedMode, null);
            }

            Debug.Log("Created players: " + _players.Count);
        }

        private static Player CreatePlayer()
        {
            Player player = new Player();
            return player;
        }

        public void Quit()
        {
            Application.Quit();
        }

        public void SetSelectedInputToPlayer(int owner, int selectedInputMode)
        {
            _players[owner].SetSelectedMode((InputMode)selectedInputMode);
            Debug.Log($"Player {owner} selected input: " + (InputMode)selectedInputMode);
        }

        public Tank GetPrefabToUse()
        {
            switch (gameMode)
            {
                case GameMode.SinglePlayer:
                    return _singlePlayerTank;
                    break;
                case GameMode.Multiplayer:
                    return _multiPlayerTank;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public InputMode GetPlayerInputMode(int i)
        {
            return _players[i].GetInputMode();
        }

        public Player GetPlayerByIndex(int i)
        {
            return _players[i];
        }
    }
}