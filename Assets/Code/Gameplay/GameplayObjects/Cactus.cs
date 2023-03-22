using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
namespace Tanks.Gameplay.Objects
{
    public class Cactus : GameplayObject
    {
        private void Start()
        {
            BoxCollider.isTrigger = false;
        }

        public void OnCollisionEnter(Collision coll)
        {
            if (coll.gameObject.CompareTag("Bullet"))
            {
                StartCoroutine(DestructionCoroutine());
            }
        }

        override protected void TriggerEnterHandler(Collider other)
        {
            // if (other.CompareTag("Bullet"))
            // {
            //     StartCoroutine(DestructionCoroutine());
            // }
        }

        private IEnumerator DestructionCoroutine()
        {
            Owner.OnGameplayObjectDisabled(this);
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
