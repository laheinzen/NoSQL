using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Redisngo
{
    class Redisngo
    {

        private ConnectionMultiplexer connection;
        private IDatabase database;
        private byte numberOfPlayers = 50;
        private byte numbersInACard = 15;
        private byte lowestNumberPossible = 1;
        private byte highestNumberPossible = 99;
        private string possibleNumbersSetName = "possibleCardNumbers";
        public bool IsBingo { get; private set; }
        public string LastRoundResult { get; private set; }
        public byte LastDrawnNumber { get; private set; }
        public byte Round { get; private set; }
        private List<string> winners;
        public StringBuilder NumbersDrawn { get; private set; }

        public Redisngo() {

        }

        public void Ready(string hostAndPort = "localhost:6379", bool flushAll = false)
        {
            connection = ConnectionMultiplexer.Connect(hostAndPort + ",allowAdmin=true");
            database = connection.GetDatabase();
            //Console.WriteLine("Cuidado... Nós vamos dar um flush no Redis como um todo. Se não é isso que você quer, dê um CTRL+C");
            //Console.ReadKey();
            connection.GetServer(hostAndPort).FlushAllDatabases();
        }

        public void Set() {
            IsBingo = false;
            Round = 0;
            winners = new List<string>();
            NumbersDrawn = new StringBuilder();
            //Primeiro guardamos os possíveis números de acordo com o parâmetro (convertendo para Valores Redis
            var oneToNinetyNineForRedis = Enumerable.Range(lowestNumberPossible, highestNumberPossible).Select (n => (RedisValue)n).ToArray();
            database.SetAdd(possibleNumbersSetName, oneToNinetyNineForRedis);


            //Agora vamos gerar 50 jogadores e 50 cartelas
            for (var u = 1; u <= numberOfPlayers; u++)
            {
                //Primeiros os dados do jogador
                var hashName = new HashEntry("name", $"user{u:00}");
                var hashCard = new HashEntry("bcartela", $"cartela:{u:00}");
                //O PDF fala que deve ser armazenacomo 'score:0' mas não faz sentido. 
                var hashScore = new HashEntry("bscore", 0);

                var hashEntries = new HashEntry[3];
                hashEntries[0] = hashName;
                hashEntries[1] = hashCard; 
                hashEntries[2] = hashScore;

                //Guardamos o hash do jogador/user
                database.HashSet($"user:{u:00}", hashEntries);

                //Agora vamos gerar a cartela
                var cardNumbers = database.SetRandomMembers(possibleNumbersSetName, numbersInACard).ToArray();
                
                //Guardando a cartela
                database.SetAdd($"cartela:{u:00}", cardNumbers);

                //Guardando os scores iniciais
                database.SortedSetAdd("scores", $"user:{u:00}", 0);
            }
        }
        
        public void Go()
        {
            var drawNumber = database.SetRandomMember(possibleNumbersSetName);
            //Sou teimoso. Quero em byte, não em int.
            LastDrawnNumber = (byte)((int)(drawNumber));
            Round++;

            //Agora vamos verificar todas as cartelas
            for (var u = 1; u <= numberOfPlayers; u++)
            {
                //Primeiros os dados do jogador
                var userKey = $"user:{u:00}";
                //Eu já sei qual é a cartela né? Mas vamos seguir a hash
                var cardSet = database.HashGet(userKey, "bcartela").ToString();

                //Opa. Se tem o número, vamos aumentar o score do sortudo
                if (database.SetContains(cardSet, drawNumber)) {
                    //Primeiro usando o Sorted Set
                    database.SortedSetIncrement("scores", userKey, 1);

                    //Agora usando o Hash
                    database.HashIncrement(userKey, "bscore");
                }
            }

            checkForBingo();
        }

        private void checkForBingo() {
            //Checar se alguém tem score 15 usando o sorted set

            //Buscar scores com mais de 15;
            var winners = database.SortedSetRangeByScore("scores", 15);
            var numberOfWinners = winners.Length; 

            //Retorna não vazio? Temos ganahador(es)
            if (numberOfWinners > 0){
                IsBingo = true;
                LastRoundResult = "BINGO! ";
                if (numberOfWinners == 1) {
                    LastRoundResult += "O ganhador é: ";
                }
                else {
                    LastRoundResult += "Os ganhadors são: ";
                }

                //Sempre tem ao menos um ganhador
                LastRoundResult += winners[0];

                for (byte w = 1; w < numberOfWinners; w++) {
                    LastRoundResult += ", " + winners[w];
                }
                LastRoundResult += ".";
            } 
            else  {
                LastRoundResult = "Ainda não há vencedores.";
            }
            
            //Checar também usando o hash? Colocar como comment

            if (NumbersDrawn.Length > 0 ) {
                NumbersDrawn.Append(", ");
            }
            NumbersDrawn.Append(LastDrawnNumber);

            bool hashBingo = false;
            byte hashNumberOfWinners = 0;
            List<String> hashWinners = new List<string>();
            //Redudante, checando pelo hash
            for (byte u = 1; u <= numberOfPlayers; u++) {
                var userScore = (int)database.HashGet($"user:{u:00}", "bscore");
                if (userScore == 15) {
                    hashBingo = true;
                    hashNumberOfWinners++;
                    hashWinners.Add($"user:{u:00}");
                }
            }

            if (hashBingo) {
                Console.WriteLine($"\t\tHashSet encontrou {hashNumberOfWinners}  vencedor(es):");
                foreach (var hashWinner in hashWinners) {
                    Console.WriteLine($"\t\t\t{hashWinner}");
                }
            }
        }
    }
}
