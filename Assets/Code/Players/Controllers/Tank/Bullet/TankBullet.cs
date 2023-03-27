using UnityEngine;

namespace Tanks.Controllers.Tank.Bullet
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(BoxCollider))]
    public class TankBullet : MonoBehaviour
    {
        [SerializeField] GameObject _collisionEffect;
        private Rigidbody _rb;
        public Rigidbody Rb => _rb;
        private PlayerTank _owner;

        public PlayerTank GetParentPlayerTank => _owner;

        public void SetBulletOwner(PlayerTank tank) {
            _owner = tank;
        }
        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            HandleBulletCollision(this.transform.position);
        }

        private void HandleBulletCollision(Vector3 point)
        {
            Instantiate(_collisionEffect);
            _collisionEffect.transform.position = point;
            Destroy(this.gameObject);
        }
    }
}