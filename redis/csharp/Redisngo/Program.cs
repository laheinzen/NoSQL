using System;

namespace Redisngo
{
    class Program
    {
        static void Main(string[] args)
        {
            try {
                Console.WriteLine("Inicializando");
                var redisngo = new Redisngo();
                redisngo.Set();
            }
            catch (Exception e) {
                Console.WriteLine($"Exceção do tipo: {e.GetType()} \nMensagem: {e.Message}");
            }
        }
    }
}
