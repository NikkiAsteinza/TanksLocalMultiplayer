using DG.Tweening;
using UnityEngine;

namespace Tanks.Gameplay.Objects.Generics
{
    public class GenericGameplayObject : GameplayObject
    {
        [SerializeField] private Transform _shieldObject;
        [SerializeField] private float _shieldRotation;
        [SerializeField] private Ease _ease;
        [SerializeField] private float _sequenceDuration;
        private void Start()
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Append(_shieldObject.DORotate(new Vector3(0,_shieldRotation,0),_sequenceDuration));
            sequence.SetLoops(-1, LoopType.Incremental);
            sequence.SetEase(_ease);
            sequence.Play();
        }
    }
}
