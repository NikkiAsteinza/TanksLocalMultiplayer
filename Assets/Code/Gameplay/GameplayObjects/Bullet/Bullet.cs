using UnityEngine;

namespace Tanks.Controllers.Tank.Turret.Bullets
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(BoxCollider))]
    public class Bullet : MonoBehaviour
    {
        [SerializeField] GameObject _collisionEffect;
        private Rigidbody _rb;
        private AudioSource _audioSource;
        public Rigidbody Rb => _rb;
        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _rb = GetComponent<Rigidbody>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            HandleBulletCollision(collision.contacts[0].point);
        }

        private void HandleBulletCollision(Vector3 point)
        {
            Instantiate(_collisionEffect);
            _collisionEffect.transform.position = point;
            Destroy(this.gameObject);
        }
    }
}