using UnityEngine;

using Tanks.Controllers.Tank;
using System.Linq;
using Unity.VisualScripting;
using static UnityEditor.Experimental.GraphView.GraphView;

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

            _players.ForEach(x => x.Tank.InitPlayer(_lives, _initialAmmunition, 0, PointsToFinish, _pointsImage));
        }

        protected override void GameLoopLogic()
        {
            if (_players.Any(x => x.Tank.IsDead))
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