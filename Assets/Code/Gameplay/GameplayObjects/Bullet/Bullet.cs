using UnityEngine;

namespace Tanks.Controllers.Tank
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(BoxCollider))]
    public class Bullet : MonoBehaviour
    {
        [SerializeField] GameObject _collisionEffect;
        private Rigidbody _rb;
        private AudioSource _audioSource;
        public Rigidbody Rb => _rb;
        private PlayerTank _owner;

        public PlayerTank Owner => _owner;

        public void SetBulletOwner(PlayerTank tank) {
            _owner = tank;
        }
        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
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