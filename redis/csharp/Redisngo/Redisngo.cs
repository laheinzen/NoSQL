﻿using StackExchange.Redis;
using System;
using System.Collections.Generic;
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

        private byte numberOfUsers = 50;

        public void Set() {

            //Vamos gerar 50 jogadores e 50 cartelas
            for (var u = 1; u <= numberOfUsers; u++)
            {
                database.HashSet($"user:{u:00}", new HashEntry[]
                 {
                    new HashEntry("name", $"user{u:00}"),
                    new HashEntry("bcartela", $"cartela:{u:00}"),
                    new HashEntry("bscore", $"score:{0}")
                 });
            }

        }
        public void LetsPlay() {

        }


    }
}