using Tanks.Gameplay.Objects;

namespace Tanks.Gameplay
{
    public interface IGame
    {
        public void InitGame();
        public void RestartGame();
        public void OnGameplayObjectDisabled(GameplayObject gameplayObject);

        public void OnGamePlayObjectEnabled(GameplayObject gameplayObject);
    }
}