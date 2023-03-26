using Tanks.Controllers.Tank;

    public static class TankEvents
    {
        public delegate void TankDestroyed(PlayerTank tank, PlayerTank destroyedTank);
        public static event TankDestroyed OnTankDestroyed;
        public static void ThrowTankDestroyed(PlayerTank tank, PlayerTank destroyedTank)
        {
            OnTankDestroyed?.Invoke(tank, destroyedTank);
        }
        
        public delegate void TankDie(PlayerTank tank, PlayerTank destroyedTank);
        public static event TankDie OnTankDie;
        public static void ThrowTankDie(PlayerTank tank, PlayerTank destroyedTank)
        {
            OnTankDie?.Invoke(tank, destroyedTank);
        }
    }