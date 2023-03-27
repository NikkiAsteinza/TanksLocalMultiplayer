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

        internal delegate void TankDie(PlayerTank tank, PlayerTank destroyedTank);
        internal static event TankDie OnTankDie;
        internal static void ThrowTankDie(PlayerTank tank, PlayerTank destroyedTank)
        {
            OnTankDie?.Invoke(tank, destroyedTank);
        }
    }
}