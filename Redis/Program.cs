using StackExchange.Redis;
using System;
using Xunit;

namespace Redis
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var conn = ConnectionMultiplexer.Connect("localhost");
            var redis = conn.GetDatabase();

            string myKey = "myValueString";
            string myValue = "myValueString";

            redis.StringSet(myKey, myValue);

            redis.StringSet(myKey, myValue, TimeSpan.FromSeconds(50));
            var fetchedKey = redis.StringGet(myKey);
            
        }



    }
}
