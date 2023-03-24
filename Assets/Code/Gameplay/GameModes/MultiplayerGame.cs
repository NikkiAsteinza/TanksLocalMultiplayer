using System.Collections.Generic;
using System.Linq;
using Tanks.Players;
using Tanks.Tanks;
using UnityEngine;

namespace Tanks.Gameplay.Logic
{
    public class MultiplayerGame : Game
    {
        [ContextMenu("Finish game")]
        void FinishGameManually()
        {
            _playerDead = true;
        }
        private List<Player> _players;
        private bool _playerDead;
        protected override void InitialSetup()
        {
            base.InitialSetup();
            _players = GameManager.Instance.GetPlayers();
            TankEvents.OnTankDie += PlayerIsDead;
        }

        private void PlayerIsDead(Tank tank)
        {
            _playerDead = true;
        }

        protected override void GameLoopLogic()
        {
            if (_playerDead)
             {
                 SwitchGameToTargetState(GameState.Finished);
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
