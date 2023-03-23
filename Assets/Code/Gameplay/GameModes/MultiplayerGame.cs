using System.Collections.Generic;
using Tanks.Players;
using Tanks.Tanks;

namespace Tanks.Gameplay.Logic
{
    public class MultiplayerGame : Game
    {
        private List<Player> _players;
        protected override void InitialSetup()
        {
            base.InitialSetup();
            _players = GameManager.Instance.GetPlayers();
            foreach (Player player in _players)
            {
                player.Tank.onDie.AddListener(OnPlayerDie);
            }
        }

        private void OnPlayerDie(Tank relatedTank)
        {
            relatedTank.ShowCanvasFinished(GameLostTitle);
            foreach (Player player in _players)
            {
                if (player.Tank != relatedTank)
                {
                    player.Tank.ShowCanvasFinished(GameWonTitle);
                }
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
