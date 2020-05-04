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
                Console.WriteLine("Conectando/Inicializando o Redis");
                redisngo.Ready();
                Console.WriteLine("Criando as cartelas para os jogadores");
                redisngo.Set();
                Console.WriteLine("Que começem os jogos");
                while (!redisngo.IsBingo)
                {
                    redisngo.Go();
                    Console.WriteLine($"Rodada número {redisngo.Round}. O número retirado foi o {redisngo.LastDrawNumber}");
                    Console.WriteLine($"Resultado da última rodada: {redisngo.LastRoundResult}");
                }
            }
            catch (Exception e) {
                Console.WriteLine($"Exceção do tipo: {e.GetType()} \nMensagem: {e.Message}");
            }
        }
    }
}
