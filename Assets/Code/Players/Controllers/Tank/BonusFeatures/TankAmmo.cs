using Tanks.Controllers.Tank.Bonus;
using UnityEngine;

namespace Tanks.Controllers.Tank.BonusFeatures
{
    public class TankAmmo : TankBonusFeature
    {
        [SerializeField] private int _points = 5;
        protected override void Feature()
        {
            _tank.AddAmmo(_points);
        }
    }
}