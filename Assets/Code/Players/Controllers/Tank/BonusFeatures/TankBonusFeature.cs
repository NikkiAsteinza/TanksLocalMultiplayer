using System.Collections.Generic;

using UnityEngine;

using Tanks.Gameplay.Objects;

namespace Tanks.Controllers.Tank.Bonus
{
    public class TankBonusFeature : MonoBehaviour
    {
        [SerializeField] private ObjectTypes _type;
        [SerializeField] protected PlayerTank _tank;
        [SerializeField] private List<GameObject> _objectsToActivate;
        [SerializeField] private bool _isTemporary = false;
        [SerializeField] protected float _bonusDuration;

        private float _timer;

        public ObjectTypes GetBonusType => _type;
        protected virtual void OnEnable()
        {
            if(_isTemporary)
                _timer = _bonusDuration;
            if (_objectsToActivate.Count > 0)
                _objectsToActivate.ForEach(x => x.SetActive(true));
            Feature();
        }

        private void Update()
        {
            if (!_isTemporary)
                return;

            _timer -= Time.deltaTime;
            if (_timer <= 0)
            {
                ResetFeature();
            }
        }

        protected virtual void OnDisable()
        {
            if (_objectsToActivate.Count > 0)
                _objectsToActivate.ForEach(x => x.SetActive(false));
        }

        protected virtual void Feature()
        {
            throw new System.NotImplementedException();
        }

        protected virtual void ResetFeature()
        {
            throw new System.NotImplementedException();
        }

        internal void ResetTimer()
        {
            _timer = _bonusDuration;
        }
    }
}