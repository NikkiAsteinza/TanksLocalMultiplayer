using System.Linq;

namespace Tanks.Gameplay.Logic
{
    public class MultiplayerGame : Game
    {
        protected override void InitialSetup()
        {
            base.InitialSetup();

            _players.ForEach(x => x.Tank.InitPlayer(_lives, _initialAmmunition, 0, PointsToFinish, _pointsImage));
        }

        protected override void GameLoopLogic()
        {
            if (_players.Any(x => x.Points == PointsToFinish))
            {
                SwitchGameToTargetState(GameState.Finished);
            }
        }

        protected override void OnGameFinished()
        {
            _players.ForEach(x =>
            {
                string message = x.Tank.IsDead ? "You loose" : "You win";
                x.Tank.ShowFinishMessage(gameOverTitle, message);
            });
        }
    }
}