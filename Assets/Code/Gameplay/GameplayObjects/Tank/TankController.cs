using UnityEngine;

namespace Tanks.Controllers.Tank
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(BoxCollider))]

    public class TankController : MonoBehaviour
    {
        [Header("Movement Properties")]
        [SerializeField] private float _tankSpeed = 80f;
        [SerializeField] private float _tankRotationSpeed = 20f;
        private float _tankInitialSpeed;
        private CharacterController rb;
        private Vector3 targetPosition;
        private void Awake()
        {
            rb = GetComponent<CharacterController>();
            _tankInitialSpeed = _tankSpeed;
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
    }
}