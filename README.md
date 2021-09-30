# Redis-Examples
C# StackExchange Redis Client Examples


1 . After Clone Run Dotnet Test 
--------------------------------------------------------------------


Redis :)) Cli
--------------------------------------------------------------------

1  . Info -> get information of Redis machine
2  . KEYS * -> show keys
3  . SET myKey myValue EX 10 -> set expire second
4  . Persist Key -> change EX to persist 
5  . Set KEy Value
6  . Get Key   
7  . flushdb
8  . lpush myList v1 v2 v3  -> push to myList as Key
9  . llen myList -> get lenght
10 . lrange myList 0 -1 -> from 0 index to last index
11 . rpush myList x1 x2 x3
12 . HGET SMB:anton:UserInfo email
13 . HGETALL Key 
14 . GEOPOS Iran Iran
15 . Bachs  Exec
16 . Transaction Exec
17 . expireat key timestamp
18 . pexpire key milisecond
19 . pexpireat key mili-timestamp
20 . getrange key 0 - 0 
21 . del key 
22 . ttl key 
23 . if ttl = -2 means -> key become expire
24 . if ttl = -1 means -> key become persist
25 . mget key1 key2  -> multiGet
26 . getset key value -> if it is not exist then redis set  it 
27 . setex key second value 
28 . setnx key value -> check if isnot exist Set it 
29 . setRange key offset value -> ex setRange name1 5 salam 
30 . strlen key
31 . multiSet key1 value1 key2 value 2
32 . msetnx key value key value 
33 . incr key 
34 . set key 100 -> THen -> incr key  
35 . incrby key 5
36 . decr key 
37 . decrby key 5
38 . append key value 
39 . hset key fieldMemberName  value -> Hash Set
40 . hget key fieldMemberName
41 . hgetall key 
42 . hkeys Hashkey 
43 . hvals Hashkey 
44 . hdel key fieldMemberName 
45 . hlen key -> lenght of hash
46 . exists key1 key2 key3 
47 . hexists key fieldMemberName
48 . lpop key -> del last value of list
49 . lrem key count value  
50 . rpop key -> del first value of list 
51 . lset key index value -> change index value 
52 . linsert key BEFORE|AFTER val1 value 




