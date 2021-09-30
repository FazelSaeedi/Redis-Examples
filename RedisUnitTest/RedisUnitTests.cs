using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace RedisUnitTest
{
    public class RedisUnitTests
    {
        private IDatabase _redis { get; set; }

        public RedisUnitTests()
        {
            var options = ConfigurationOptions.Parse("localhost:6379");
            options.AllowAdmin = true;

            var conn = ConnectionMultiplexer.Connect(options);
            
            conn.GetServer("localhost:6379").FlushDatabase();
            _redis = conn.GetDatabase();

        }

        [Fact]
        public void Test_Connection()
        {

            var conn = ConnectionMultiplexer.Connect("localhost");
            var redis = conn.GetDatabase();

        }

        [Fact]
        public void Test_SetValue()
        {

            var conn = ConnectionMultiplexer.Connect("localhost");
            var redis = conn.GetDatabase();


            string myKey = "myValueString";
            string myValue = "myValueString";
            redis.StringSet(myKey, myValue, null , When.NotExists );

            var GetValue = redis.StringGet(myKey);

            Assert.Equal(GetValue, myValue);
            Assert.True(GetValue.HasValue);


        }

        [Fact]
        public void Test_SetValue_With_Expiration()
        {

            var conn = ConnectionMultiplexer.Connect("localhost");
            var redis = conn.GetDatabase();


            string myKey = "myValueString";
            string myValue = "myValueString";

            redis.StringSet(myKey, myValue);

            redis.StringSet(myKey, myValue, TimeSpan.FromSeconds(3));
            var fetchedKey = redis.StringGet(myKey);

            Assert.True(fetchedKey.HasValue);
              
            Thread.Sleep(3000);

         
            var fetchedKey2 = redis.StringGet(myKey);
            Assert.False(fetchedKey2.HasValue);
        }

        [Fact]
        public void Test_Set_Value_Increment()
        {

            var conn = ConnectionMultiplexer.Connect("localhost");
            var redis = conn.GetDatabase();


            string myKey = "myValueString";
            int myValue = 5 ;


            redis.StringSet(myKey, myValue);
            redis.StringIncrement(myKey);

            var result = (int)redis.StringGet(myKey);

            Assert.Equal(result , 6);

        }

        [Fact]
        public void Test_Set_List_1()
        {

            var key = "SMB:anton:Posts";

            _redis.ListLeftPush(
                key ,
                  new RedisValue[]
                  {
                      "v1" ,
                      "v2" ,
                      "v3" 
                  }); 


            var NumberOfPosts = _redis.ListLength(key);

            Assert.Equal(NumberOfPosts, 3);
        }

        [Fact]
        public void Test_Ses()
        {
            var keyAntonFollowing = "SMB:anton:following";
            _redis.SetAdd(keyAntonFollowing, new RedisValue[] { " userA" , "userB" });


            var nAntonFollowingCount = _redis.SetLength(keyAntonFollowing);
            Assert.Equal(nAntonFollowingCount, 2);


            _redis.SetAdd("SMB:userB:followers", "anton");
            _redis.SetAdd("SMB:userA:followers", "anton");

            var nUserAFollower = _redis.SetLength("SMB:userA:followers");
            Assert.Equal(nUserAFollower, 1);


            var keyAntonFollowers = "SMB:anton:followers";
            _redis.SetAdd(keyAntonFollowers, new RedisValue[] { "userC", "userD" });



            _redis.SetAdd("SMB:userC:followers", "anton");
            _redis.SetAdd("SMB:userD:followers", "anton");
        }


        [Fact]
        public  void Test_Order_Set()
        {

            var today = DateTime.UtcNow.Ticks;
            var yesterday = DateTime.UtcNow.AddDays(1).Ticks;

            var KeyAntonPosts = "SMB:anton:Posts";
            _redis.SortedSetAdd(KeyAntonPosts, new SortedSetEntry[]
                {
                    new SortedSetEntry("hellow world" , yesterday ),
                    new SortedSetEntry("hellow world today" , today ),
                });

            var KeyUserXPosts = "SMB:userx:Posts";
            _redis.SortedSetAdd(KeyUserXPosts, new SortedSetEntry[]
                {
                    new SortedSetEntry("userx say  hellow world yesterday " , yesterday+1 ),
                    new SortedSetEntry("userx say hellow world Today" , today+1 ),
                });


            _redis.SortedSetCombineAndStore(
                SetOperation.Union ,
                "homepage" ,
                KeyAntonPosts,
                KeyUserXPosts
                );

            var homePagePostCount = _redis.SortedSetLength("homepage");
            Assert.Equal(homePagePostCount, 4);


            var homePagePosts = _redis.SortedSetRangeByRank( "homepage", 0 , -1);

        }   

        [Fact]
        public void Test_Hash()
        {
            var nextID = _redis.StringIncrement("next_user_id");
            
            var keyUserList = "SMB:Users";
            _redis.HashSet(keyUserList, new HashEntry[]
                {
                    new HashEntry("anton" , nextID)
                });

            var KeyAnton = "SMB:anton:UserInformation";
            _redis.HashSet(KeyAnton, new HashEntry[]
                {
                    new HashEntry("email" , "anton@delsink.com"),
                    new HashEntry("phone" , "+989..."),
                    new HashEntry("id" , nextID),
                    new HashEntry("etc" , "etc.")
                });



        }

        [Fact]
        public void GEO()
        {
            var key = "Iran";
            _redis.GeoAdd(key, 54.53, 34.43 , "Mashhad");


            var key2 = "Iran";
            _redis.GeoAdd(key, 44.53, 24.43, "Tehran");

            var pos = _redis.GeoPosition("Iran" , "Mashhad");

            var dist = _redis
                .GeoDistance("Iran", "Mashhad", "Tehran" , GeoUnit.Kilometers);

        }
    }
}
 