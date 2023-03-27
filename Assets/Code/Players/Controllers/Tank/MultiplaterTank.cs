using UnityEngine;

namespace Tanks.Controllers.Tank
{
    public class MultiplaterTank : PlayerTank
    {
        protected override void Awake()
        {
            base.Awake();
            TankEvents.OnTankDestroyed += HandleOtherPlayerDies;
            TankEvents.OnTankDie += OnPlayerDie;
        }
        protected override void HandleBulletCollision(Bullet bullet)
        {
            base.HandleBulletCollision(bullet);
            TankEvents.ThrowTankDestroyed(bullet.Owner, this);
        }

        protected virtual void Updatelife(int updatedLife, PlayerTank attackingTank)
        {
            base.UpdateLife(updatedLife,attackingTank);
            _tankCanvas.SetLives(updatedLife);
        }

        private void HandleOtherPlayerDies(PlayerTank attakingTank, PlayerTank damagedTank)
        {
            if (attakingTank == this && damagedTank != null)
            {
                this._destroyedTanks = _destroyedTanks + 1;
                Debug.Log($"{gameObject.name} destroyed {_destroyedTanks} tanks");
                _tankCanvas.SetDestryoedTanks(_destroyedTanks);
            }
        }

        private void OnPlayerDie(PlayerTank attakingTank, PlayerTank killedTank)
        {
            if (killedTank != this)
            {
                _tankCanvas.MultiplayerShowCanvasFinished("Winner");
            }
            else
            {
                _tankCanvas.MultiplayerShowCanvasFinished("Looser");
            }
        }
    }
}