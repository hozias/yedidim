using System;
using System.Configuration;
using YedideyChabad.Models;
using MongoDB.Driver;

namespace YedideyChabad.Authentication.MongoDB
{
    public class MongoDBSettings
    {
        public void Connection()
        {
            const string conn = "mongodb://yadiduser:2wsx#EDC2wsx@ds047057.mongolab.com:047057/yedidim";

            var client = new MongoClient(conn);


            var server = client.GetServer();


            var database = server.GetDatabase("yedidim");
            
            database.CreateCollection("Users");
            var collection = database.GetCollection("Users");

            var data = collection.FindAll();
        }
    }

    //public class User
    //{
    //    public string Username { get; set; }
    //    public string Password { get; set; }
    //}
}