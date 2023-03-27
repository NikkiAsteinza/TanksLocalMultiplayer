using UnityEngine;

using Tanks.Controllers.Tank.Common;

namespace Tanks.Controllers.Tank.Bonus
{
    public class TankSuperSpeed : TankBonusFeature
    {
        [SerializeField] private float _superSpeed = 30;
        [SerializeField] TankController _tankController;
        protected override void Feature()
        {
            _tankController.SetSpeed(_superSpeed);
            Invoke("ResetFeature",_bonusDuration);
        }

        protected override void ResetFeature()
        {
            _tankController.ResetSpeed();
            gameObject.SetActive(false);
        }
    }
}