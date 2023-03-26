using UnityEngine;

namespace Tanks.Controllers.Tank
{
    public class TankTurret : MonoBehaviour
    {
        [SerializeField] Bullet _bulletPrefab;
        [SerializeField] float fireForce = 500;
        [SerializeField] Transform turretTransform;
        [SerializeField] Transform _bulletSpawnPointPrefab;

        PlayerTank _owner;
        Transform _spawnPoint;

        private void Start()
        {
            _spawnPoint = Instantiate(_bulletSpawnPointPrefab, turretTransform);
        }

        internal void SetOwner(PlayerTank playerTank)
        {
            _owner = playerTank;
        }

        internal void Fire() {
            Bullet tempBullet = Instantiate(_bulletPrefab);
            tempBullet.transform.SetPositionAndRotation(_spawnPoint.position,_spawnPoint.rotation);
            tempBullet.SetBulletOwner(_owner);
            tempBullet.Rb.AddForce(_spawnPoint.forward * fireForce);
        }

        internal void HandleRotation(Vector2 movementInput)
        {
            Debug.Log("Rotating");
            turretTransform.Rotate(Vector3.up * Time.deltaTime* movementInput.x *80, Space.Self);
        }
    }
}