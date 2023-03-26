using UnityEngine;

namespace Tanks.Controllers.Tank
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(BoxCollider))]

    public class TankController : MonoBehaviour
    {
        [Header("Tank Turret")]
        [SerializeField] TankTurret _turret;
        [Header("Movement Properties")]
        [SerializeField] private float _tankSpeed = 80f;
        [SerializeField] private float _tankRotationSpeed = 20f;
        private float _tankInitialSpeed;
        private CharacterController rb;
        private Vector3 targetPosition;
        private PlayerTank _owner;
        internal void Init(PlayerTank owner)
        {
            rb = GetComponent<CharacterController>();
            _tankInitialSpeed = _tankSpeed;
            _owner = owner;
            _turret.SetOwner(owner);
        }

        internal void HandleMovement(Vector2 movementInput)
        {

            targetPosition = movementInput.y * transform.forward;
            SetGravity();
            rb.Move(targetPosition);

            Quaternion targetRotation = transform.rotation * Quaternion.Euler(Vector3.up * movementInput.x * _tankRotationSpeed * Time.fixedDeltaTime);
            transform.Rotate(Vector3.up,movementInput.x * _tankRotationSpeed);
            
        }

        private void SetGravity()
        {
            targetPosition.y = -9.8f * Time.deltaTime;
        }

        public void ResetSpeed()
        {
            SetSpeed(_tankInitialSpeed);
        }

        public void SetSpeed(float superSpeed)
        {
            _tankSpeed = superSpeed;
        }

        internal void Fire()
        {
            _turret.Fire();
        }

        internal void HandleTurretRotation(Vector2 rotationInput)
        {
            _turret.HandleRotation(rotationInput);
        }
    }
}