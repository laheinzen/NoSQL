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

        public Redisngo(string hostAndPort = "localhost:6379", bool flushAll = false) {
            connection = ConnectionMultiplexer.Connect(hostAndPort + ",allowAdmin=true");
            database = connection.GetDatabase();
            //connection.GetServer(hostAndPort).FlushAllDatabases();
            //database = connection.GetDatabase();
            Console.WriteLine("Cuidado... Nós vamos dar um flush no Redis como um todo. Se não é isso que você quer, dê um CTRL+C");
            Console.ReadKey();
            connection.GetServer(hostAndPort).FlushAllDatabases();
        }

       public void Set() {
            
            byte numberOfUsers = 50;

            //Primeiro guardamos os possíveis números (1 a 99)
            var oneToNinetyNineForRedis = Enumerable.Range(1, 99).Select (n => (RedisValue)n).ToArray();
            database.SetAdd("possibleCardMembers", oneToNinetyNineForRedis);

            //Agora vamos gerar 50 jogadores e 50 cartelas
            for (var u = 1; u <= numberOfUsers; u++)
            {
                //Primeiros os dados do jogador
                var hashName = new HashEntry("name", $"user{u:00}");
                var hashCard = new HashEntry("bcartela", $"cartela:{u:00}");
                var hashScore = new HashEntry("bscore", $"score:{0}");

                var hashEntries = new HashEntry[3];
                hashEntries[0] = hashName;
                hashEntries[1] = hashCard;
                hashEntries[2] = hashScore;

                //Guardamos o hash do jogador/user
                database.HashSet($"user:{u:00}", hashEntries);

                //Agora vamos gerar a cartela
                var cardNumber01 = database.SetRandomMember("possibleCardNumbers");
                var cardNumber02 = database.SetRandomMember("possibleCardNumbers");
                var cardNumber03 = database.SetRandomMember("possibleCardNumbers");
                var cardNumber04 = database.SetRandomMember("possibleCardNumbers");
                var cardNumber05 = database.SetRandomMember("possibleCardNumbers");
                var cardNumber06 = database.SetRandomMember("possibleCardNumbers");
                var cardNumber07 = database.SetRandomMember("possibleCardNumbers");
                var cardNumber08 = database.SetRandomMember("possibleCardNumbers");
                var cardNumber09 = database.SetRandomMember("possibleCardNumbers");
                var cardNumber10 = database.SetRandomMember("possibleCardNumbers");
                var cardNumber11 = database.SetRandomMember("possibleCardNumbers");
                var cardNumber12 = database.SetRandomMember("possibleCardNumbers");
                var cardNumber13 = database.SetRandomMember("possibleCardNumbers");
                var cardNumber14 = database.SetRandomMember("possibleCardNumbers");
                var cardNumber15 = database.SetRandomMember("possibleCardNumbers");


            }

        }
        public void LetsPlay() {

        }


    }
}
