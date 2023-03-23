using Tanks.Tanks;

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
        public Tank Tank =>_tank;

        public void SetSelectedMode(InputMode inputMode)
        {
            _selectedInputMode = inputMode;
        }
        public void SetTank(Tank tank)
        {
            _tank = tank;
        }

        public InputMode GetInputMode()
        {
            return _selectedInputMode;
        }
    }
}