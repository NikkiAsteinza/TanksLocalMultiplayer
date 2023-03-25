using Tanks.Controllers.Tank;

    public static class TankEvents
    {
        public delegate void TankDestroyed(PlayerTank tank);
        public static event TankDestroyed OnTankDestroyed;
        public static void ThrowTankDestroyed(PlayerTank tank)
        {
            OnTankDestroyed?.Invoke(tank);
        }
        
        public delegate void TankDie(PlayerTank tank);
        public static event TankDie OnTankDie;
        public static void ThrowTankDie(PlayerTank tank)
        {
            OnTankDie?.Invoke(tank);
        }
    }
