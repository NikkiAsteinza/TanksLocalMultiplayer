using Tanks.Tanks;
using UnityEngine;

namespace Tanks.GameplayObjects
{
    public enum ObjectTypes
    {
        Shield,
        Ammo,
        Turbo,
    }

    [RequireComponent(typeof(BoxCollider))]
    [RequireComponent(typeof(AudioSource))]
    public class GameplayObject : MonoBehaviour{
    
        protected ObjectTypes _type;
        protected AudioSource _audioSource;
        private AudioClip _soundEffect;
        private bool _isApplied = false;
        
        public void Init (AudioClip clip, ObjectTypes type)
        {
            _type = type;
            _soundEffect = clip;
            _audioSource = GetComponent<AudioSource>();
        }

        public void OnTriggerEnter(Collider collider)
        {
            bool isTank = collider.GetComponent<Tank>() != null;
            if (isTank && !_isApplied)
            {
                Tank tank = collider.GetComponent<Tank>();
                tank.ApplyObjectFeature(_type);
                _isApplied = true;
            }

            _audioSource.PlayOneShot(_soundEffect);
        }
    }
}
