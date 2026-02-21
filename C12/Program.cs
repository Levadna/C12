namespace C12
{
    public interface ICar
    {
        public const int min_speed = 0;
        public static int max_speed = 220;
        public static double GetTime(double distance, double speed)
            => distance / speed;
        public static int MaxSpeed
        {
            get => max_speed;
            set
            {
                if (max_speed > 0)
                {
                    max_speed = value;
                }
                throw new ArgumentException("Error argument max_speed");
            }
        }
        public void Move();
        public void Stop();
        public delegate void CarEventHandler(string message);
        public event CarEventHandler? CarEvent;
    }
    public class Car : ICar, IDisposable
    {
        public event ICar.CarEventHandler? CarEvent;

        public void Dispose()
        {
            Console.WriteLine("Dispose!");
        }

        public void Move() => CarEvent?.Invoke("Moving ...");

        public void Stop() => CarEvent?.Invoke("Stoping ...");
    }
    public class Program
    {
        static void Main(string[] args)
        {
            ICar car = new Car();
            car.CarEvent += message => Console.WriteLine(message);
            car.Move();
            car.Stop();
            Console.ReadKey();
        }
    }
}
