using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Tanks.Utils
{
    public class StartAppChecker : MonoBehaviour
    {
        [SerializeField] private GameObject _startAppButton;
        bool activateButton = false;
        List<Gamepad> gamepads;
        private int players = 0;
        private void Start()
        {
            gamepads = new List<Gamepad>();
        }

        private void Update()
        {
            List<Player> players = GameManager.Instance.GetPlayers();
            if (players != null && players.Count > 0)
            {
                GameMode gameMode = GameManager.Instance.gameMode;
                CheckInputSelectionByGameMode(gameMode);
                //CheckGamepadsNotEqual(gameMode);

                if (_startAppButton.activeInHierarchy != activateButton)
                    _startAppButton.SetActive(activateButton);
            }
        }

        private void CheckGamepadsNotEqual(GameMode gameMode)
        {
            if (gameMode != GameMode.Multiplayer)
                return;

            for (int i = 0; i < players; i++)
            {
                Gamepad playerGamepad = GameManager.Instance.GetPlayerGamepad(i);
                if (playerGamepad != null)
                {
                    gamepads.Add(playerGamepad);
                }
            }

            activateButton = gamepads.Count == gamepads.Distinct().Count();
        }

        private void CheckInputSelectionByGameMode(GameMode gameMode)
        {
            switch (gameMode)
            {
                case GameMode.SinglePlayer:
                    activateButton = CheckInputSelectionByPlayerIndex(0);
                    break;
                case GameMode.Multiplayer:
                    activateButton = CheckInputSelectionByPlayerIndex(0) &&
                                     CheckInputSelectionByPlayerIndex(1);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static bool CheckInputSelectionByPlayerIndex(int playerIndex)
        {
            bool activate = false;
            if (GameManager.Instance.GetPlayerInputMode(playerIndex) == InputMode.Keyboard)
            {
                activate = true;
            }
            else if (GameManager.Instance.GetPlayerInputMode(playerIndex) == InputMode.Gamepad)
            {
                if (GameManager.Instance.GetPlayerGamepad(0) != null)
                    activate = true;
            }

            return activate;
        }
    }
}