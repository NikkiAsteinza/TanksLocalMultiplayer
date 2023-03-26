using System.Collections;

using UnityEngine;

namespace Tanks.Gameplay.Objects
{
    public class Cactus : GameplayObject
    {
        private void Awake()
        {
            _visuals.SetActive(false);
        }

        private void OnDisable()
        {
            _visuals.SetActive(false);
        }
        
        private void OnCollisionEnter(Collision coll)
        {
            if (coll.gameObject.CompareTag("Bullet"))
            {
                StartCoroutine(DestructionCoroutine());
            }
        }

        private IEnumerator DestructionCoroutine()
        {
            GameModeOwner.OnGameplayObjectDisabled(this);
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
