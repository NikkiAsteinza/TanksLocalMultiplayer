using DG.Tweening;
using Tanks.Gameplay.Logic;
using Tanks.Tanks;
using UnityEngine;

namespace Tanks.Gameplay.Objects.Generics
{
    public class GenericGameplayObject : GameplayObject
    {
        [SerializeField] private GenericGameplayObjectsSpawner _spawner;
        [SerializeField] private Transform _body;
        [SerializeField] protected GameObject _visuals;
        [SerializeField] protected AudioClip _soundEffect;
        [SerializeField] private Vector3 _objectRotationVector;
        [SerializeField] private Ease _ease;
        [SerializeField] private float _sequenceDuration;

        private bool _isApplied = false;
        private void Start()
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Append(_body.DORotate(_objectRotationVector,_sequenceDuration));
            sequence.SetLoops(-1, LoopType.Incremental);
            sequence.SetEase(_ease);
            sequence.Play();
        }

        public void SetSpawner(GenericGameplayObjectsSpawner genericGameplayObjectsSpawner)
        {
            _spawner = genericGameplayObjectsSpawner;
        }
        
        private void OnTriggerEnter(Collider collider)
        {
            if (!_isApplied)
            {
                TriggerEnterHandler(collider);
                _spawner.ResetTimer();
                _isApplied = true;
            }
        }

        protected virtual void TriggerEnterHandler(Collider collider)
        {
            bool isTank = collider.GetComponent<Tank>() != null;
            if (isTank)
            {
                Tank tank = collider.GetComponent<Tank>();
                tank.ApplyObjectFeature(_type);
               
            }
            _visuals.gameObject.SetActive(true);
            AudioSource.PlayOneShot(_soundEffect);
            
            Invoke("DisableObject",2);
            
        }
    }
}
