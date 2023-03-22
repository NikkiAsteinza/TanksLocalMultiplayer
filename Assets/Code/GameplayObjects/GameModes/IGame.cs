using Tanks.GameplayObjects;

public interface IGame
{
    public void StartGame();
    public void GameLoop();
    public void OnGameplayObjectDestroyed(GameplayObject gameplayObject);

    public void OnGamePlayObjectEnabled(GameplayObject gameplayObject);

    public void OnGameFinished();

    public void Restart();

}