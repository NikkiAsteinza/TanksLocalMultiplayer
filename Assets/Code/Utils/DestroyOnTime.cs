using UnityEngine;

namespace Tanks
{
    public class DestroyOnTime : MonoBehaviour
    {
        [SerializeField] float time = 0.5f;
        void Start()
        {
            Destroy(gameObject, time);
        }
    }
}