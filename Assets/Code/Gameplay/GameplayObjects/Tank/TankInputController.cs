using UnityEngine;
using UnityEngine.InputSystem;

namespace Tanks.Controllers.Tank
{
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(PlayerInput))]
    public class TankInputController : MonoBehaviour
    {
        [Header("Tank Sounds")]
        [SerializeField]
        private AudioClip idleSound;

        [SerializeField] private AudioClip movingSound;

        private PlayerInput _playerInput;
        private Vector2 movementInput = Vector2.zero;
        private Vector2 rotationInput = Vector2.zero;
        private AudioSource _audioSource;
        private PlayerTank _relatedTank;
        private TankController _tankController;

        private bool init = false;
        internal void Init(PlayerTank relatedTank,TankController controller)
        {
            _playerInput = GetComponent<PlayerInput>();
            _audioSource = GetComponent<AudioSource>();

            SubscribeToPlayerInputs(true);
            _tankController = controller;
            _relatedTank = relatedTank;
            init = true;
        }

        private void OnDestroy()
        {
            SubscribeToPlayerInputs(false);
        }
        void FixedUpdate()
        {
            if (!init)
                return;

            if (movementInput != Vector2.zero)
            {
                _tankController.HandleMovement(movementInput);
                if (Mathf.Abs(movementInput.y) > 0.1 && _audioSource.clip != movingSound)
                {
                    PLayClipIfNotPlaying(movingSound);
                }
            }
            else
                PLayClipIfNotPlaying(idleSound);

            if (rotationInput != Vector2.zero)
            {
                _tankController.HandleTurretRotation(rotationInput);
            }
        }

        private void SubscribeToPlayerInputs(bool subscribe)
        {
            if (subscribe)
            {
                this._playerInput.actions["move"].performed += OnMove;
                this._playerInput.actions["rotate"].performed += OnRotate;
                this._playerInput.actions["fire"].performed += OnFire;
            }
            else
            {
                this._playerInput.actions["move"].performed -= OnMove;
                this._playerInput.actions["rotate"].performed -= OnRotate;
                this._playerInput.actions["fire"].performed -= OnFire;
            }
        }


        public void OnMove(InputAction.CallbackContext context)
        {
            movementInput = context.ReadValue<Vector2>();
        }

        private void OnRotate(InputAction.CallbackContext context)
        {
            rotationInput = context.ReadValue<Vector2>();
        }

        public void OnFire(InputAction.CallbackContext context)
        {
            int tankAmmo = _relatedTank.GetAmmo();
            if (tankAmmo > 0)
            {
                _tankController.Fire();
                _relatedTank.UpdateAmmo(tankAmmo - 1);
            }
            else
            {
                Debug.Log("Not ammo");
            }
        }

        private void PLayClipIfNotPlaying(AudioClip clip)
        {
            if (_audioSource.clip == clip)
                return;
            _audioSource.Stop();
            _audioSource.clip = clip;
            _audioSource.Play();
        }
    }
}
