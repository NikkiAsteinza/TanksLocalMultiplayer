using System.Collections.Generic;
using Tanks.Gameplay.Objects;
using UnityEngine;

namespace Tanks.Controllers.Tank.Bonus
{
    public class TankBonusFeature : MonoBehaviour
    {
        [SerializeField] private ObjectTypes _type;
        [SerializeField] protected PlayerTank _tank;
        [SerializeField] private List<GameObject> _objectsToActivate;
        [SerializeField] protected float _bonusDuration;

        private float _timer;
        
        public ObjectTypes GetBonusType => _type;
        protected virtual  void OnEnable()
        {
            _timer = _bonusDuration;
            if(_objectsToActivate.Count > 0 )
                _objectsToActivate.ForEach(x => x.SetActive(true));
            Feature();
        }

        private void Update()
        {
            _timer -= Time.deltaTime;
                if (_timer <= 0)
                {
                    Debug.Log("Bonus feature timer");
                    ResetFeature();
                }
        }

        protected virtual  void OnDisable()
        {
            if(_objectsToActivate.Count > 0 )
                _objectsToActivate.ForEach(x => x.SetActive(true));
        }

        protected virtual void Feature()
        {
            throw new System.NotImplementedException();
        }

        protected virtual void ResetFeature()
        {
            throw new System.NotImplementedException();
        }

        public void ResetTimer()
        {
            _timer = _bonusDuration;
        }

    }
}