using UnityEngine;
using Tanks.Gameplay.Objects;
using Tanks.Controllers.Tank.Common;
using Tanks.Controllers.Tank.Bonus;

using Tanks.Controllers.Tank.Bullet;
using Tanks.Controllers.Tank.Events;
using Tanks.UI;

namespace Tanks.Controllers.Tank
{
    [RequireComponent(typeof(BoxCollider))]

    public class PlayerTank : MonoBehaviour
    {
        [ContextMenu("Destroy Tank Manually")]
        void DestroyTankManually()
        {
            DestroyTank();
        }

        [ContextMenu("Restore Tank Manually")]
        void RestoreTankManually()
        {
            RestoreTank();
        }

        [Header("Tank General")]
        [SerializeField] Camera _camera;

        [Header("Tank canvas")]
        [SerializeField] private TankCanvas _tankCanvas;
        [Header("Tank visuals")]
        [SerializeField] private TankVisualsController _tankVisuals;

        [Header("Tank Movement")]
        [SerializeField] private TankInputController _inputController;
        [SerializeField] private TankController _controller;

        [Header("Tank Bonus")]
        [SerializeField] private TankBonusController _bonusController;

        private bool _isDestroyed = false;
        private bool _isDead = false;

        private Player _owner;

        private int _lifes;
        private int _initialAmmunition;
        private int _ammo = 0;
        private int _destroyedTanks = 0;
        private int _currentLife = 0;


        protected Vector3 _initialPosition;
        internal bool IsDead => _isDead;
        public Player GetPlayerOwner => _owner;
        internal int GetAmmo()
        {
            return _ammo;
        }

        #region Internal methods
        internal void SetOwner(Player player)
        {
            _owner = player;
        }
        internal void AddAmmo(int ammount)
        {
            int resultingAmmo = _ammo + ammount;
            _ammo = resultingAmmo > 15? 15 : resultingAmmo;
            _tankCanvas.SetAmmo(_ammo);
        }
        internal virtual void RemoveAmmo(int ammount)
        {
            _ammo -= ammount;
            _tankCanvas.SetAmmo(_ammo);
        }
        internal void SetAlternativeColor()
        {
            _tankVisuals.SetAlternativeColor();
        }

        #endregion

        #region Unity  Methods
        private void Awake()
        {
            TankEvents.OnTankDestroyed += HandleOtherPlayerDies;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (_isDestroyed)
                return;

            TankBullet bullet = collision.collider.GetComponent<TankBullet>();
            if (bullet)
            {
                HandleBulletCollision(bullet);
            }
        }
        #endregion

        private void HandleBulletCollision(TankBullet bullet)
        {
            DestroyTank();
            UpdateLife(_currentLife - 1, bullet.GetParentPlayerTank);
            _tankCanvas.SetLives(_currentLife);
            Debug.Log("A bullet hitted the tank -> " + gameObject.name);

            Invoke("RestoreTank", GameManager.Instance.SecondsToRestorePlayer);
            TankEvents.ThrowTankDestroyed(bullet.GetParentPlayerTank, this);
        }

        private void UpdateLife(int updatedLife, PlayerTank attackingTank)
        {

            if (updatedLife == 0)
            {
                _isDead = true;
            }

            _currentLife = updatedLife;
            TankEvents.ThrowTankDestroyed(this, attackingTank);

        }

        private void DestroyTank()
        {
            transform.position = _initialPosition;
            _isDestroyed = true;
            _tankVisuals.SetNormalVisualsOn(false);
            _inputController.enabled = false;
        }

        private void RestoreTank()
        {
            _isDestroyed= false;
            _tankVisuals.SetNormalVisualsOn(true);
            _inputController.enabled = true;
        }

        internal void ApplyObjectFeature(ObjectTypes type)
        {
            _bonusController.ApplyObjectFeature(type);
        }
        private void HandleOtherPlayerDies(PlayerTank attakingTank, PlayerTank damagedTank)
        {
            if (attakingTank.GetPlayerOwner == _owner)
            {
                this._destroyedTanks = this._destroyedTanks + 1;
                Debug.Log($"{gameObject.name} destroyed {_destroyedTanks} tanks");
                this._tankCanvas.SetPoints(_destroyedTanks);
            }
        }

        internal void UpdatePoints(int points)
        {
            _tankCanvas.SetPoints(points);
        }

        internal void InitPlayer(int lives, int initialAmmunition, int points, int goalPoints, Sprite pointsImage)
        {
            _currentLife = lives;
            _ammo = initialAmmunition;
            _initialPosition = transform.position;

            _inputController.Init(this, _controller);
            _controller.Init(this);
            _bonusController.Init(this);

            _tankCanvas.Init(lives, initialAmmunition, points, goalPoints, pointsImage);
        }

        internal void ShowFinishMessage(string gameOverTitle, string message)
        {
            _tankCanvas.ShowFinalMessage(gameOverTitle, message);
        }

        internal void ShowFinalMessageTimer(string gameOverTitle)
        {
            _tankCanvas.ShowFinalMessageTimer(gameOverTitle);
        }
    }
}