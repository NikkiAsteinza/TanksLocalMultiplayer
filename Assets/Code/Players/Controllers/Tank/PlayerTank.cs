using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

using Tanks.Controllers.Tank.Bonus;
using Tanks.Gameplay.Objects;

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
        [SerializeField] protected int _lives = 3;
        [SerializeField] Camera _camera;
        [SerializeField] protected int _initialAmmunition = 10;

        [Header("Tank canvas")]
        [SerializeField] protected TankCanvas _tankCanvas;
        [Header("Tank visuals")]
        [SerializeField] protected TankVisualsController _tankVisuals;

        [Header("Tank Movement")]
        [SerializeField] protected TankInputController _inputController;
        [SerializeField] protected TankController _controller;

        [Header("Tank Bonus")]
        [SerializeField] private GameObject _shield;
        [SerializeField] private List<TankBonusFeature> _bonusFeatures;

        private bool _isDead;
        protected int _ammo;
        protected int _destroyedTanks=0;
        protected int _currentLife;

        protected Vector3 _initialPosition;
        internal int GetAmmo()
        {
            return _ammo;
        }
        internal void ApplyObjectFeature(ObjectTypes type)
        {
            switch (type)
            {
                case ObjectTypes.Shield:
                    TankBonusFeature _shieldBonus = _bonusFeatures.FirstOrDefault(x => x.GetBonusType == type);
                    if (!_shieldBonus.gameObject.activeInHierarchy)
                        _shieldBonus.gameObject.SetActive(true);
                    break;
                case ObjectTypes.Ammo:
                    AddAmmo(5);
                    break;
                case ObjectTypes.Turbo:
                    TankBonusFeature _speedBonus = _bonusFeatures.FirstOrDefault(x => x.GetBonusType == type);
                    if (!_speedBonus.gameObject.activeInHierarchy)
                        _speedBonus.gameObject.SetActive(true);
                    else
                    {
                        _speedBonus.ResetTimer();
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        internal void SetSpeed(float superSpeed)
        {
            SetTankSpeed(superSpeed);
        }

        private void SetTankSpeed(float superSpeed)
        {
            _controller.SetSpeed(superSpeed);
        }

        internal void ResetSpeed()
        {
            _controller.ResetSpeed();
        }

        internal void EnableShield(bool b)
        {
            _shield.gameObject.SetActive(b);
        }

        #region Internal methods
        internal virtual void UpdateAmmo(int updatedAmmo)
        {
            _ammo = updatedAmmo;
            _tankCanvas.SetAmmo(_ammo);
        }
        internal void SetAlternativeColor()
        {
            _tankVisuals.SetAlternativeColor();
        }
        #endregion

        #region Protected Methods
        protected virtual void Awake()
        {
            _currentLife = _lives;
            _ammo = _initialAmmunition;
            _initialPosition = transform.position;
        }
        protected virtual void HandleBulletCollision(Bullet bullet)
        {
            DestroyTank();
            UpdateLife(_currentLife - 1, bullet.Owner);
            _tankCanvas.SetLives(_currentLife);
            Debug.Log("A bullet hitted the tank -> " + gameObject.name);

            Invoke("RestoreTank", GameManager.Instance.SecondsToRestorePlayer);
        }
        protected virtual void UpdateLife(int updatedLife, PlayerTank attackingTank)
        {

            if (updatedLife == 0)
            {
                TankEvents.ThrowTankDie(attackingTank, this);
            }

            _currentLife = updatedLife;

        }
        #endregion

        private void Start()
        {
            _inputController.Init(this,_controller);
            _controller.Init(this);
            _tankCanvas.Init(_lives, _initialAmmunition, 0);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (_isDead)
                return;

            Bullet bullet = collision.collider.GetComponent<Bullet>();
            if (bullet)
            {
                HandleBulletCollision(bullet);
            }
        }

        private void AddAmmo(int ammount)
        {
            _ammo += ammount;
            UpdateAmmo(_ammo);
        }

        private void DestroyTank()
        {
            transform.position = _initialPosition;
            _isDead = true;
            _tankVisuals.SetNormalVisualsOn(false);
            _inputController.enabled = false;
        }

        private void RestoreTank()
        {
            _isDead= false;
            _tankVisuals.SetNormalVisualsOn(true);
            _inputController.enabled = true;
        }
    }
}