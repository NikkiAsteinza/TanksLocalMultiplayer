using UnityEngine;

namespace Tanks.Bullets
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(BoxCollider))]
    public class Bullet : MonoBehaviour
    {
        private Rigidbody rb;
        public Rigidbody Rb => rb;
        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            Destroy(this.gameObject);
        }
    }
}