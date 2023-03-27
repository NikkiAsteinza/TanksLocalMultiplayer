using UnityEngine;

namespace Tanks.Controllers.Tank.Bonus
{
    public class TankShield : TankBonusFeature
    {
        [SerializeField] private GameObject _shield;
        protected override void Feature()
        {
            _shield.SetActive(true);
            Invoke("ResetFeature",_bonusDuration);
        }

        protected override void ResetFeature()
        {
            _shield.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}