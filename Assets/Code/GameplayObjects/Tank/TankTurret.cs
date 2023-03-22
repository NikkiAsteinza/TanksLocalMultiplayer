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
            tempBullet.transform.position = _spawnPoint.position;
            tempBullet.Rb.AddForce(_spawnPoint.forward * fireForce);
        }

        internal void HandleTurret(Vector3 reticlePosition)
        {
            Vector3 lookAtDir = reticlePosition - turretTransform.position;
            lookAtDir.y = 0;
            turretTransform.rotation= Quaternion.LookRotation(lookAtDir);
        }
    }
}