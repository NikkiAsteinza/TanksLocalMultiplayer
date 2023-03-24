using Tanks.Tanks;

    public static class TankEvents
    {
        public delegate void TankDestroyed(Tank tank);
        public static event TankDestroyed OnTankDestroyed;
        public static void ThrowTankDestroyed(Tank tank)
        {
            OnTankDestroyed?.Invoke(tank);
        }
        
        public delegate void TankDie(Tank tank);
        public static event TankDie OnTankDie;
        public static void ThrowTankDie(Tank tank)
        {
            OnTankDie?.Invoke(tank);
        }
    }
