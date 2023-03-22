using Tanks.Tanks;
using UnityEngine;

namespace Tanks.Gameplay.Objects
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

        [SerializeField] private ObjectTypes _type;
        [SerializeField] protected GameObject _visuals;
        [SerializeField] protected AudioClip _soundEffect;

        protected BoxCollider BoxCollider;
        protected IGame Owner;
        protected AudioSource AudioSource;
        private bool _isApplied = false;

        public ObjectTypes GetObjectType() => _type;
        public void SetOwner(IGame game)
        {
            Owner = game;
        }
        private void Awake()
        {
            _visuals.SetActive(false);
            BoxCollider = GetComponent<BoxCollider>();
            AudioSource = GetComponent<AudioSource>();
            AudioSource.playOnAwake = false;
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
            // bool isTank = collider.GetComponent<Tank>() != null;
            // if (isTank && !_isApplied)
            // {
            //     Tank tank = collider.GetComponent<Tank>();
            //     tank.ApplyObjectFeature(_type);
            //     _isApplied = true;
            // }

            AudioSource.PlayOneShot(_soundEffect);
        }
    }
}
