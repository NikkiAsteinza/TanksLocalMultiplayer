using UnityEngine;

namespace Tanks.Controllers.Tank
{
    public class MultiplaterTank : PlayerTank
    {
        protected override void Awake()
        {
            base.Awake();
            _tankCanvas.Init(_lives, _initialAmmunition, 0);
            TankEvents.OnTankDestroyed += HandleOtherPlayerDies;
            TankEvents.OnTankDie += OnPlayerDie;
        }
        protected override void HandleBulletCollision(Bullet bullet)
        {
            base.HandleBulletCollision(bullet);
            TankEvents.ThrowTankDestroyed(bullet.Owner, this);
        }
        public override void RestoreAll()
        {
            base.RestoreAll();
            _tankCanvas.Init(_currentLife, _ammo, _destroyedTanks);
        }

        protected virtual void Updatelife(int updatedLife, PlayerTank attackingTank)
        {
            base.UpdateLife(updatedLife,attackingTank);
            _tankCanvas.SetLives(updatedLife);
        }

        private void HandleOtherPlayerDies(PlayerTank attakingTank, PlayerTank damagedTank)
        {
            if (attakingTank == this)
            {
                _destroyedTanks++;
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