using System;
using Tanks.Controllers.Tank;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Tanks
{
    public enum InputMode
    {
        None,
        Keyboard,
        Gamepad,
    }

    public enum PlayerState
    {
        Idle,
        Shielded,
        Destroyed
    }

    public class Player
    {
        private PlayerTank _tank;
        private InputMode _selectedInputMode;
        private Gamepad _selectedGamepad;
        private int _points = 0;

        public PlayerTank Tank =>_tank;
        public int Points => _points;
        
        public void SetSelectedMode(InputMode inputMode)
        {
            _selectedInputMode = inputMode;
        }
        
        public void SetSelecteGamepad(Gamepad gamepad)
        {
            _selectedGamepad = gamepad;
        }
        public void SetTank(PlayerTank tank)
        {
            _tank = tank;
        }

        public InputMode GetInputMode()
        {
            return _selectedInputMode;
        }

        public Gamepad GetGamepad()
        {
            return _selectedGamepad;
        }

        internal void AddPoints(int points)
        {
            _points += points;
            _tank.UpdatePoints(_points);
        }
    }
}