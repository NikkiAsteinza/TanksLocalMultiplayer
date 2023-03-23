using System;
using Tanks.Bullets;
using Tanks.Gameplay.Objects;
using Tanks.Players;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

namespace Tanks.Tanks {
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(PlayerInput))]
    public class Tank : MonoBehaviour
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

        [Header("Tank canvas")]
        [SerializeField] private TMP_Text _lifeIndicator;
        [SerializeField] private TMP_Text _ammoIndicator;
        [Header("Tank Sounds")]
        [SerializeField] private AudioClip idleSound;
        [SerializeField] private AudioClip movingSound;
        [SerializeField] private AudioClip dieSound;

        [Header("Tank Visuals")]
        [SerializeField] int _lives = 3;
        [SerializeField] Camera _camera;
        [SerializeField] GameObject _tankModel;
        [SerializeField] GameObject _destroyedTankModel;

        [Header("Tank Turret")]
        [SerializeField] int _initialAmmunition = 10;
        [SerializeField] TankTurret _turret;
        [SerializeField] Bullet _bulletPrefab;

        [Header("Tank Reticle")]
        [SerializeField] TankReticle _reticle;

        [Header("Tank Movement")]
        [SerializeField] TankController _controller;

        private Vector2 movementInput = Vector2.zero;

        private AudioSource _audioSource;
        private PlayerInput playerInput;
        private int _ammo;
        private bool _isDestroyed;

        private void Awake()
        {
            UpdateLife(_lives);
            UpdateAmmo(_initialAmmunition);
            _destroyedTankModel.SetActive(false);
            playerInput = GetComponent<PlayerInput>();
            _audioSource = GetComponent<AudioSource>();
        }

        private void UpdateAmmo(int updatedAmmo)
        {
            _ammo = updatedAmmo;
            _ammoIndicator.text = updatedAmmo.ToString();
        }

        private void UpdateLife(int updatedLife)
        {
            _lives = updatedLife;
            _lifeIndicator.text = updatedLife.ToString();
        }

        private void Start()
        {
            SubscribeToPlayerInputs(true);

            _reticle.Init(_camera, _turret);
            _ammo = _initialAmmunition;
        }

        private void SubscribeToPlayerInputs(bool subscribe)
        {
            if (subscribe){
                playerInput.actions["move"].performed += OnMove;
                playerInput.actions["fire"].performed += OnFire;
            }
            else {
                playerInput.actions["move"].performed -= OnMove;
                playerInput.actions["fire"].performed -= OnFire;
            }

        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.GetComponent<Bullet>())
            {
                UpdateLife(_lives-1);
                Debug.Log("A bullet hitted the tank -> " + gameObject.name);
                SetNormalVisualsOn(false);
                SubscribeToPlayerInputs(false);
            }
        }
        public void AddAmmo(int ammount) {
            int tempAmmo = ammount + _ammo;
            UpdateAmmo(tempAmmo > 15 ? 15 : _ammo);
        }
        public void OnMove(InputAction.CallbackContext context)
        {
            movementInput = context.ReadValue<Vector2>();
        }

        public void OnFire(InputAction.CallbackContext context)
        {
            if (_ammo > 0)
            {
                _turret.Fire(_bulletPrefab);
                UpdateAmmo(_ammo-1);
            }
            else {
                Debug.Log("Not ammo");
            }
        }


        // Fixed rate for physics.
        void FixedUpdate()
        {
            if (movementInput != Vector2.zero)
            {
                _controller.HandleMovement(movementInput);
            }
        }
        private void DestroyTank()
        {
            SetNormalVisualsOn(false);
            SubscribeToPlayerInputs(false);
            _isDestroyed = false;
        }


        private void RestoreTank()
        {
            SetNormalVisualsOn(true);
            SubscribeToPlayerInputs(true);
            _isDestroyed = true;
        }

        private void SetNormalVisualsOn(bool enable)
        {
            _tankModel.SetActive(enable);
            _destroyedTankModel.SetActive(!enable);
        }

        public void ApplyObjectFeature(ObjectTypes type)
        {
            switch (type)
            {
                case ObjectTypes.Shield:
                    
                    break;
                case ObjectTypes.Ammo:
                    AddAmmo(10);
                    break;
                case ObjectTypes.Turbo:
                    break;
                case ObjectTypes.Cacti:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        public void SetInputDevice(InputMode getPlayerInputMode)
        {
           Debug.Log(playerInput.devices.ToString());
            playerInput.actions.bindingMask = new InputBinding {groups = getPlayerInputMode == 0 ? "Keyboard" : "Gamepad"};
        }
    }
}

