namespace Tanks.Controllers.Tank.Bonus
{
    public class TankShield : TankBonusFeature
    {
        protected override void Feature()
        {
            _tank.EnableShield(true);
            Invoke("ResetFeature",_bonusDuration);
        }

        protected override void ResetFeature()
        {
            _tank.EnableShield(false);
            gameObject.SetActive(false);
        }
    }
}
