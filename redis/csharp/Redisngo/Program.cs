using System;

namespace Redisngo
{
    class Program
    {
        static void Main(string[] args)
        {
            try {
                //Inicializando
                var redisngo = new Redisngo();
                //Conectando/Inicializando o Redis
                redisngo.Ready();
                Console.WriteLine("Distribuindo as cartelas.");
                redisngo.Set();
                Console.WriteLine("Começando o Bingo. Boa sorte aos jogadores!\n");
                Console.ReadKey();

                while (!redisngo.IsBingo)
                {
                    redisngo.Go();

                    //A cada 10 rodadas a gente relembra o que já foi sorteado. 
                    if (redisngo.Round % 10 == 0) {
                        Console.WriteLine($"{redisngo.Round} números sorteados até o mommento: {redisngo.NumbersDrawn}\n");
                        Console.ReadKey();
                    }

                    Console.WriteLine($"Rodada {redisngo.Round}.");
                    Console.WriteLine($"\tNúmero retirado: { redisngo.LastDrawnNumber}.");
                    Console.WriteLine($"\t{redisngo.LastRoundResult}\n");
                }
                Console.WriteLine("Bingo encerrado. Espero que todos tenham se divertido!");
                Console.ReadKey();
            }
            catch (Exception e) {
                Console.WriteLine($"Exceção do tipo: {e.GetType()} \nMensagem: {e.Message}");
            }
        }
    }
}
