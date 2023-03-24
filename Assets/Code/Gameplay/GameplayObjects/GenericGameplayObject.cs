using System;
using DG.Tweening;
using Tanks.Gameplay.Logic;
using Tanks.Tanks;
using UnityEngine;

namespace Tanks.Gameplay.Objects.Generics
{
    public enum InteractionState
    {
        Ready,
        Disabled
    }

    public class GenericGameplayObject : GameplayObject
    {
        [ContextMenu("Disable and restart timer")]
        void DisableAndRestartTimer()
        {
            SwitchInteractionState(InteractionState.Disabled);
        }
        
        [SerializeField] private GenericGameplayObjectsSpawner _spawner;
        [SerializeField] private Transform _body;
        [SerializeField] protected GameObject _visuals;
        [SerializeField] protected AudioClip _soundEffect;
        [SerializeField] private Vector3 _objectRotationVector;
        [SerializeField] private Ease _ease;
        [SerializeField] private float _sequenceDuration;

        private InteractionState currentState;
        private void OnEnable()
        {
            SwitchInteractionState(InteractionState.Ready);
            Sequence sequence = DOTween.Sequence();
            sequence.Append(_body.DORotate(_objectRotationVector,_sequenceDuration));
            sequence.SetLoops(-1, LoopType.Incremental);
            sequence.SetEase(_ease);
            sequence.Play();
        }

        private void SwitchInteractionState(InteractionState ready)
        {
            switch (ready)
            {
                case InteractionState.Ready:
                    
                    _visuals.gameObject.SetActive(false);
                    break;
                case InteractionState.Disabled:
                    _spawner.ResetTimer();
                    gameObject.SetActive(false);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(ready), ready, null);
            }
        }

        public void SetSpawner(GenericGameplayObjectsSpawner genericGameplayObjectsSpawner)
        {
            _spawner = genericGameplayObjectsSpawner;
        }
        
        private void OnTriggerEnter(Collider collider)
        {
            TriggerEnterHandler(collider);
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
            
            Invoke("SetDisabledState",2);
            
        }

        public void SetDisabledState()
        {
            SwitchInteractionState(InteractionState.Disabled);
        }
    }
}
