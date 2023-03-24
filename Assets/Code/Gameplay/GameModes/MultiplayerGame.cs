using System.Collections.Generic;
using System.Linq;
using Tanks.Players;
namespace Tanks.Gameplay.Logic
{
    public class MultiplayerGame : Game
    {
        private List<Player> _players;
        protected override void InitialSetup()
        {
            base.InitialSetup();
            _players = GameManager.Instance.GetPlayers();
        }
        protected override void GameLoopLogic()
        {
            if (_players.Any(x=>x.Tank.GetLives() == 0))
             {
                 UpdatePoints(GoalPoints);
             }
        }

        protected override void Restart()
        {
            base.Restart();
            foreach (Player player in _players)
            {
                player.Tank.RestoreAll();
            }
        }
    }
}
