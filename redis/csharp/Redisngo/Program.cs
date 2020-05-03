using System;

namespace Redisngo
{
    class Program
    {
        static void Main(string[] args)
        {
            try {
                Console.WriteLine("Hello World!");
                var redis = new Redis();
            }
            catch (Exception e) {
                Console.WriteLine($"Exceção do tipo: {e.GetType()} \nMensagem: {e.Message}");
            }
        }
    }
}
