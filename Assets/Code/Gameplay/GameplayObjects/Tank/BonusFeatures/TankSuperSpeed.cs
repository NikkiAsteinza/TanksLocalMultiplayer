using UnityEngine;

namespace Tanks.Controllers.Tank.Bonus
{
    public class TankSuperSpeed : TankBonusFeature
    {
        [SerializeField] private float _superSpeed = 30;
        protected override void Feature()
        {
            _tank.SetSpeed(_superSpeed);
            Invoke("ResetFeature",_bonusDuration);
        }

        protected override void ResetFeature()
        {
            _tank.ResetSpeed();
            gameObject.SetActive(false);
        }
    }
}