using Tanks.Bullets;
using UnityEngine;

namespace Tanks.Tanks
{
    public class TankTurret : MonoBehaviour
    {
        [SerializeField] float fireForce = 500;
        [SerializeField] Transform turretTransform;
        [SerializeField] Transform _bulletSpawnPointPrefab;

        Transform _spawnPoint;
        private void Start()
        {
            _spawnPoint = Instantiate(_bulletSpawnPointPrefab, turretTransform);
        }
        internal void Fire(Bullet bulletPrefab) {
            Bullet tempBullet = Instantiate(bulletPrefab);
            tempBullet.transform.SetPositionAndRotation(_spawnPoint.position,_spawnPoint.rotation);
            tempBullet.Rb.AddForce(_spawnPoint.forward * fireForce);
        }

        internal void HandleRotation(Vector2 movementInput)
        {
            Debug.Log("Rotating");
            turretTransform.Rotate(Vector3.up * Time.deltaTime* movementInput.x *80, Space.Self);
            
        }
    }
}