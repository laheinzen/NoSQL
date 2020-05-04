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
        private List<byte> numbersDrawn;

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
            numbersDrawn = new List<byte>();
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
            numbersDrawn.Add(LastDrawnNumber);
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
            //TODO
            //Checar se alguém tem score 15 usando o sorted set
            //Se sim, atualizar IsBingo
            //Checar também usando o hash? Colocar como comment
            //Independente disso, atualizar a string Last Round Result
        }
    }
}
