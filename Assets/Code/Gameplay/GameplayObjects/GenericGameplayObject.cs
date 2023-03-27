using System;
using DG.Tweening;

using UnityEngine;

using Tanks.Controllers.Tank;

namespace Tanks.Gameplay.Objects
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
        [SerializeField] private Vector3 _objectRotationVector;
        [SerializeField] private Ease _ease;
        [SerializeField] private float _sequenceDuration;

        BoxCollider _boxCollider;
        bool _isApplied = false;

        public void SetSpawner(GenericGameplayObjectsSpawner genericGameplayObjectsSpawner)
        {
            _spawner = genericGameplayObjectsSpawner;
        }

        private void OnEnable()
        {
            SwitchInteractionState(InteractionState.Ready);
            Sequence sequence = DOTween.Sequence();
            sequence.Append(_body.DORotate(_objectRotationVector,_sequenceDuration));
            sequence.SetLoops(-1, LoopType.Incremental);
            sequence.SetEase(_ease);
            sequence.Play();
        }

        private void OnTriggerEnter(Collider collider)
        {
            TriggerEnterHandler(collider);
        }

        private void SwitchInteractionState(InteractionState targetInteractionState)
        {
            switch (targetInteractionState)
            {
                case InteractionState.Ready:
                    _isApplied = false;
                    _visuals.gameObject.SetActive(false);
                    break;
                case InteractionState.Disabled:
                    _spawner.ResetTimer();
                    gameObject.SetActive(false);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(targetInteractionState), targetInteractionState, null);
            }
        }

        private void TriggerEnterHandler(Collider collider)
        {
            if (_isApplied)
                return;

            _isApplied = true;
            
            PlayerTank tank = collider.GetComponent<PlayerTank>();
            if (tank != null)
            {
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