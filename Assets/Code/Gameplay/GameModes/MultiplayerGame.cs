using UnityEngine;

using Tanks.Controllers.Tank;

namespace Tanks.Gameplay.Logic
{
    public class MultiplayerGame : Game
    {
        [ContextMenu("Finish game")]
        void FinishGameManually()
        {
            _playerDead = true;
        }

        private bool _playerDead;

        protected override void InitialSetup()
        {
            base.InitialSetup();
            TankEvents.OnTankDie += PlayerIsDead;
        }

        protected override void GameLoopLogic()
        {
            if (_playerDead)
            {
                SwitchGameToTargetState(GameState.Finished);
            }
        }

        private void PlayerIsDead(PlayerTank attakingTank, PlayerTank killedTank)
        {
            _playerDead = true;
        }
    }
}