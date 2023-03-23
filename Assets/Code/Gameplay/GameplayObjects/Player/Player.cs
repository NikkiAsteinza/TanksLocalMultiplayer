using Tanks.Tanks;
using UnityEngine.InputSystem;

namespace Tanks.Players
{
    public enum InputMode
    {
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
        private Tank _tank;
        private InputMode _selectedInputMode;
        private Gamepad _selectedGamepad;
        public Tank Tank =>_tank;

        public void SetSelectedMode(InputMode inputMode)
        {
            _selectedInputMode = inputMode;
        }
        
        public void SetSelecteGamepad(Gamepad gamepad)
        {
            _selectedGamepad = gamepad;
        }
        public void SetTank(Tank tank)
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
    }
}