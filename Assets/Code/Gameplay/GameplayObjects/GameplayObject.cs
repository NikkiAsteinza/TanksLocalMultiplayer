using UnityEngine;

namespace Tanks.Gameplay.Objects
{
    public enum ObjectTypes
    {
        Shield,
        Ammo,
        Turbo,
        Shootable
    }

    [RequireComponent(typeof(BoxCollider))]
    [RequireComponent(typeof(AudioSource))]
    public class GameplayObject : MonoBehaviour{

        [SerializeField] protected ObjectTypes _type;
        [SerializeField] protected GameObject _visuals;
        [SerializeField] protected AudioClip _soundEffect;

        protected BoxCollider BoxCollider;
        protected IGame GameModeOwner;
        protected AudioSource AudioSource;

        public ObjectTypes GetObjectType() => _type;
        public void SetOwner(IGame game)
        {
            GameModeOwner = game;
        }
        private void Awake()
        {
            BoxCollider = GetComponent<BoxCollider>();
            AudioSource = GetComponent<AudioSource>();
            AudioSource.playOnAwake = false;
        }
    }
}