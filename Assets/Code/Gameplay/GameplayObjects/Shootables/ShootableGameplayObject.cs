using System.Collections;

using UnityEngine;

using Tanks.Controllers.Tank.Bullet;
using Tanks.Controllers.Tank;

namespace Tanks.Gameplay.Objects.Shootables
{
    public class ShootableGameplayObject : GameplayObject
    {
        [SerializeField] private int _points;
        private bool _isHitted = false;
        public int Points => _points;
        
        private void OnEnable()
        {
            _visuals.SetActive(false);
            _isHitted = false;
        }

        private void OnDisable()
        {
            _visuals.SetActive(false);
        }

        private void OnCollisionEnter(Collision coll)
        {
            if (!_isHitted && coll.collider.gameObject.CompareTag("Bullet"))
            {
                _isHitted = true;
                TankBullet bullet = coll.gameObject.GetComponent<TankBullet>();
                PlayerTank tank = bullet.GetParentPlayerTank;
                Player player =tank.GetPlayerOwner;

                if (player != null)
                {
                    player.AddPoints(_points);
                    Debug.Log($"Player points added: {_points}");
                }
                StartCoroutine(DestructionCoroutine(coll));
            }
        }

        private IEnumerator DestructionCoroutine(Collision coll)
        {
            

            if (AudioSource != null && _soundEffect != null)
            {
                AudioSource.PlayOneShot(_soundEffect);
            }

            yield return new WaitForSeconds(0.03f);

            if (_visuals != null)
            {
                _visuals.SetActive(true);
            }
            yield return new WaitForSeconds(4f);
            gameObject.SetActive(false);
        }
    }
}