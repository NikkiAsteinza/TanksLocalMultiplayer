using UnityEngine;

namespace Tanks.Controllers.Tank
{
    public class DestroyOnTime : MonoBehaviour
    {
        [SerializeField] float time = 0.5f;
        void Start()
        {
            Destroy(this.transform, time);
        }
    }
}