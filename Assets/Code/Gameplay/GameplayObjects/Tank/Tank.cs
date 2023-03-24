using System;
using System.Collections.Generic;
using System.Linq;
using Tanks.Bullets;
using Tanks.Gameplay.Objects;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.Events;

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
        [SerializeField] private TMP_Text _finishMessage;
        [SerializeField] private GameObject _tankCanvas;
        [SerializeField] private TMP_Text _destroyedTankIndicator;
        [Header("Tank Sounds")]
        [SerializeField] private AudioClip idleSound;
        [SerializeField] private AudioClip movingSound;

        [Header("Tank Visuals")]
        [SerializeField] int _lives = 3;
        [SerializeField] Camera _camera;
        [SerializeField] GameObject _tankModel;
        [SerializeField] GameObject _destroyedTankModel;

        [Header("Tank Turret")]
        [SerializeField] int _initialAmmunition = 10;
        [SerializeField] TankTurret _turret;
        [SerializeField] Bullet _bulletPrefab;

        [Header("Tank Movement")]
        [SerializeField] TankController _controller;
        
        [Header("Tank Bonus")]
        [SerializeField] private GameObject _shield;

        [SerializeField] private List<TankBonusFeature> _bonusFeatures;

        private Vector2 movementInput = Vector2.zero;
        private Vector2 rotationInput = Vector2.zero;

        private AudioSource _audioSource;
        private PlayerInput playerInput;
        private int _ammo;
        private bool _isMuliplayer;
        private int _destroyedTanks;
        
        public void ApplyObjectFeature(ObjectTypes type)
        {
            switch (type)
            {
                case ObjectTypes.Shield:
                    TankBonusFeature _shieldBonus = _bonusFeatures.FirstOrDefault(x => x.GetType == type);
                    if(!_shieldBonus.gameObject.activeInHierarchy) 
                        _shieldBonus.gameObject.SetActive(true);
                    break;
                case ObjectTypes.Ammo:
                    AddAmmo(10);
                    break;
                case ObjectTypes.Turbo:
                    TankBonusFeature _speedBonus = _bonusFeatures.FirstOrDefault(x => x.GetType == type);
                    if(!_speedBonus.gameObject.activeInHierarchy) 
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

        public void MultiplayerShowCanvasFinished(string text)
        {
            _finishMessage.text = text;
            _finishMessage.transform.parent.gameObject.SetActive(true);
        }

        public void RestoreAll()
        {
            UpdateLife(0, true);
        }

        public void SetSpeed(float superSpeed)
        {
            SetTankSpeed(superSpeed);
        }

        private void SetTankSpeed(float superSpeed)
        {
            _controller.SetSpeed(superSpeed);
        }

        public void ResetSpeed()
        {
            _controller.ResetSpeed();
        }

        public void EnableShield(bool b)
        {
            _shield.gameObject.SetActive(b);
        }

        public void SetDevice(int owner)
        {
            Gamepad selectedPlayerGamepad = GameManager.Instance.GetPlayerGamepad(owner);
            playerInput.SwitchCurrentControlScheme(selectedPlayerGamepad);
        }

        #region Unity Methods

        private void Awake()
        {
            _isMuliplayer = GameManager.Instance.gameMode == GameMode.Multiplayer;
            
            if (_isMuliplayer)
            {
                TankEvents.OnTankDestroyed += HandleOtherPlayerDies;
                _tankCanvas.SetActive(true);
            }

            UpdateLife(_lives);
            UpdateAmmo(_initialAmmunition);
            _destroyedTankModel.SetActive(false);
            playerInput = GetComponent<PlayerInput>();
            _audioSource = GetComponent<AudioSource>();
        }
        private void Start()
        {
            SubscribeToPlayerInputs(true);
            _ammo = _initialAmmunition;
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.GetComponent<Bullet>())
            {
                UpdateLife(_lives-1);
                Debug.Log("A bullet hitted the tank -> " + gameObject.name);
                SetNormalVisualsOn(false);
                SubscribeToPlayerInputs(false);
                Invoke("RestoreTank",GameManager.Instance.SecondsToRestorePlayer);
            }
        }
        void FixedUpdate()
        {
            if (movementInput != Vector2.zero)
            {
                _controller.HandleMovement(movementInput);
                if (Mathf.Abs(movementInput.y) > 0.1 && _audioSource.clip != movingSound){
                    PLayClipIfNotPlaying(movingSound);
                }
            }
            else
                PLayClipIfNotPlaying(idleSound);
            if (rotationInput != Vector2.zero)
            {
                _turret.HandleRotation(rotationInput);

            }
        }
        #endregion
        
        private void HandleOtherPlayerDies(Tank arg0)
        {
            _destroyedTanks++;
            _destroyedTankIndicator.text = _destroyedTanks.ToString();
        }

        private void UpdateAmmo(int updatedAmmo)
        {
            _ammo = updatedAmmo;
            _ammoIndicator.text = updatedAmmo.ToString();
        }

        private void UpdateLife(int updatedLife, bool reset = false)
        {
            
            if(reset){
                _lives = 0;
                _lifeIndicator.text = "0";
                return;
            }
            if (updatedLife == 0)
            {
                TankEvents.ThrowTankDie(this);
            }
            TankEvents.ThrowTankDestroyed(this);
            _lives = updatedLife;
            _lifeIndicator.text = updatedLife.ToString();
        }
        
        private void SubscribeToPlayerInputs(bool subscribe)
        {
            if (subscribe){
                this.playerInput.actions["move"].performed += OnMove;
                this.playerInput.actions["rotate"].performed += OnRotate;
                this.playerInput.actions["fire"].performed += OnFire;
            }
            else {
                this.playerInput.actions["move"].performed -= OnMove;
                this.playerInput.actions["rotate"].performed -= OnRotate;
                this.playerInput.actions["fire"].performed -= OnFire;
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
        private void OnRotate(InputAction.CallbackContext context)
        {
            rotationInput = context.ReadValue<Vector2>();
        }
        private void PLayClipIfNotPlaying(AudioClip clip)
        {
            if (_audioSource.clip == clip)
                return;
            _audioSource.Stop();
            _audioSource.clip = clip;
            _audioSource.Play();
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
    
       
        private void DestroyTank()
        {
            SetNormalVisualsOn(false);
            SubscribeToPlayerInputs(false);
        }


        private void RestoreTank()
        {
            SetNormalVisualsOn(true);
            SubscribeToPlayerInputs(true);
        }

        private void SetNormalVisualsOn(bool enable)
        {
            _tankModel.SetActive(enable);
            _destroyedTankModel.SetActive(!enable);
        }

        public int GetLives()
        {
            return _lives;
        }
        //
        // private void OnPlayerDie(Tank relatedTank)
        // {
        //     relatedTank.MultiplayerShowCanvasFinished(GameLostMessage);
        //     foreach (Player player in _players)
        //     {
        //         if (player.Tank != relatedTank)
        //         {
        //             UpdatePoints(_goalPoints);
        //             player.Tank.MultiplayerShowCanvasFinished(GameWonMessage);
        //         }
        //     }
        // }
    }
}