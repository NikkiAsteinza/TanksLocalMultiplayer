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

        private Rigidbody rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        internal void HandleMovement(Vector2 movementInput)
        {
            Vector3 targetPosition = transform.position + (transform.forward * movementInput.y * _tankSpeed * Time.fixedDeltaTime);
            rb.MovePosition(targetPosition);

            Quaternion targetRotation = transform.rotation * Quaternion.Euler(Vector3.up * movementInput.x * _tankRotationSpeed * Time.fixedDeltaTime);
            rb.MoveRotation(targetRotation);
        }
    }
}
