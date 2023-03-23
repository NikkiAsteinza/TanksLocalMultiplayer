using UnityEngine;

namespace Tanks.Tanks
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(SphereCollider))]

    public class TankController : MonoBehaviour
    {
        [Header("Movement Properties")]
        [SerializeField] private float _tankSpeed = 80f;
        [SerializeField] private float _tankRotationSpeed = 20f;
        private float _tankInitialSpeed;
        private Rigidbody rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            _tankInitialSpeed = _tankSpeed;
        }

        internal void HandleMovement(Vector2 movementInput)
        {
            Vector3 targetPosition = transform.position + (transform.forward * movementInput.y * _tankSpeed * Time.fixedDeltaTime);
            
            // IMPROVEMENT -> REALISTIC PHYSICS///////////////////////////////////
            // Vector3 force = transform.forward * movementInput.y * _tankSpeed * 10;
            //  force.y = transform.position.y > 0.25? force.y : 0;
            // rb.AddForce(force,ForceMode.Force);
             rb.MovePosition(targetPosition);

            Quaternion targetRotation = transform.rotation * Quaternion.Euler(Vector3.up * movementInput.x * _tankRotationSpeed * Time.fixedDeltaTime);
            rb.MoveRotation(targetRotation);
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
