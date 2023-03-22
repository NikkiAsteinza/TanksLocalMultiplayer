using Tanks.Tanks;
using UnityEngine;

namespace Tanks.GameplayObjects
{
    public enum ObjectTypes
    {
        Shield,
        Ammo,
        Turbo,
        Cacti,
    }

    [RequireComponent(typeof(BoxCollider))]
    [RequireComponent(typeof(AudioSource))]
    public class GameplayObject : MonoBehaviour{

        [SerializeField] protected GameObject _visuals;
        [SerializeField] protected ObjectTypes _type;
        [SerializeField] protected AudioClip _soundEffect;

        protected IGame _owner;
        protected AudioSource _audioSource;
        private bool _isApplied = false;

        public void SetOwner(IGame game)
        {
            _owner = game;
        }
        private void Awake()
        {
            _visuals.SetActive(false);
            _audioSource = GetComponent<AudioSource>();
            _audioSource.playOnAwake = false;
        }

        private void OnDisable()
        {
            _visuals.SetActive(false);
        }

        private void OnTriggerEnter(Collider collider)
        {
            TriggerEnterHandler(collider);
        }

        protected virtual void TriggerEnterHandler(Collider collider)
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
