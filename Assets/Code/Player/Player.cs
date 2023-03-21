using Tanks.Tanks;

namespace Tanks.Players
{
    public enum InputMode
    {
        Keyboard,
        Gamepad,
    }

    public class Player
    {
        private int _lives = 3;
        private Tank _tank;
        private InputMode _selectedInputMode;

        public void SetSelectedMode(InputMode inputMode)
        {
            _selectedInputMode = inputMode;
        }
        public void SetTank(Tank tank)
        {
            _tank = tank;
        }
    }
}