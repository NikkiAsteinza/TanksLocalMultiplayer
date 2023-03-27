namespace Tanks.Controllers.Tank.Events
{
    internal static class TankEvents
    {
        internal delegate void TankDestroyed(PlayerTank tank, PlayerTank destroyedTank);
        internal static event TankDestroyed OnTankDestroyed;
        internal static void ThrowTankDestroyed(PlayerTank tank, PlayerTank destroyedTank)
        {
            OnTankDestroyed?.Invoke(tank, destroyedTank);
        }
    }
}