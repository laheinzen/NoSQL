using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace Redisngo
{
    class Redis
    {

        private ConnectionMultiplexer connection;
        private IDatabase database;

        public Redis(string hostAndPort = "localhost:6379") {
            connection = ConnectionMultiplexer.Connect(hostAndPort);
            database = connection.GetDatabase();
        }
    }
}
