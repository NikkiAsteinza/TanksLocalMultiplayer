using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;

using Tanks.Controllers.Tank;
using Tanks.Players;

namespace Tanks
{
    public enum GameMode
    {
        SinglePlayer,
        Multiplayer,
    }

    public class GameManager : SingletonBehaviour<GameManager>
    {
        [SerializeField] public const float _secondsToRestorePlayer = 5;
        [SerializeField] private int maxPlayers = 2;
        [SerializeField] internal PlayerTank _singlePlayerTank;
        [SerializeField] internal PlayerTank _multiPlayerTank;
        [SerializeField] internal int _playerLives = 3;
        [SerializeField] internal int _initialAmmunition = 10;

        [Header("Debug purposes, no need assignment")] [SerializeField]
        GameMode _selectedGameMode;

        [SerializeField] private List<Player> _players;
        public GameMode gameMode => _selectedGameMode;
        public int totalPlayers => _players.Count;
        public float SecondsToRestorePlayer = _secondsToRestorePlayer;
        private void Awake()
        {
            _players = new List<Player>();
        }

        public void SetSelectedMode(GameMode selectedMode)
        {
            _selectedGameMode = selectedMode;
            Debug.Log($"Selected game mode: {selectedMode}");
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
                    for (int i = 0; i < maxPlayers; i++)
                    {
                        Player player1 = CreatePlayer();
                        _players.Add(player1);
                    }
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

        public PlayerTank GetPrefabToUse()
        {
            PlayerTank targetTank;
            switch (gameMode)
            {
                case GameMode.SinglePlayer:
                    targetTank = _singlePlayerTank;
                    break;
                case GameMode.Multiplayer:
                    targetTank = _multiPlayerTank;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return targetTank;
        }

        public InputMode GetPlayerInputMode(int i)
        {
            return _players[i].GetInputMode();
        }

        public Player GetPlayerByIndex(int i)
        {
            return _players[i];
        }

        public List<Player> GetPlayers()
        {
            return _players;
        }

        public void SetSelectedGamepadToPlayer(int owner, int selectedInputMode)
        {
            Gamepad gamepad = Gamepad.all[selectedInputMode];
            if (gamepad != null)
            {
                _players[owner].SetSelecteGamepad(gamepad);
                Debug.Log($"Player {owner} selected gamepad: " + Gamepad.all[selectedInputMode]);
            }
            else
            {
                Debug.LogWarning($"There is no gamepad available for Player {owner}");
            }
        }

        public Gamepad GetPlayerGamepad(int i)
        {
            if(_players[i].GetGamepad() == null && Gamepad.all[0] !=null)
                return Gamepad.all[0];
            return _players[i].GetGamepad() ;
        }
    }
}