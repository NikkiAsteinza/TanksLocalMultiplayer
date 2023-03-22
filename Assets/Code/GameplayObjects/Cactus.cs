using System.Collections;
using UnityEngine;
namespace Tanks.GameplayObjects
{
    public class Cactus : GameplayObject
    {
        override protected void TriggerEnterHandler(Collider other)
        {
            if (other.CompareTag("Bullet"))
            {
                StartCoroutine(DestructionCoroutine());
            }
        }

        private IEnumerator DestructionCoroutine()
        {
            if (_audioSource != null && _soundEffect != null)
            {
                _audioSource.PlayOneShot(_soundEffect);
            }
            yield return new WaitForSeconds(0.03f);
            if (_visuals != null)
            {
                _visuals.SetActive(true);
            }
            yield return new WaitForSeconds(4f);

            gameObject.SetActive(false);
            _owner.OnGameplayObjectDestroyed(this);
        }
    }
}
