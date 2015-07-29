using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace atpac.webapi.Controllers
{
    public class BaseApiController : ApiController
    {
        //local db
        //public NpgsqlConnection conn = new NpgsqlConnection(String.Format("Preload Reader = true; Server={0};Port={1};" + "User Id={2};Password={3};Database={4};", "127.0.0.1", "5432", "atpac", "atpac", "atpac"));
        //heroku db
        public NpgsqlConnection conn = new NpgsqlConnection("Server=ec2-54-204-3-188.compute-1.amazonaws.com;Port=5432;User Id=kptrpgqqylqibj;Password=1JmCkEVkoU-Bzkz-S2aYRzAh3P;Database=d70mbahpcgbmd4;SSL=True;Sslmode=require;Preload Reader=True;");

        //Host:                     ec2-54-204-3-188.compute-1.amazonaws.com
        //Database:            d70mbahpcgbmd4
        //User:                     kptrpgqqylqibj
        //Port:                      5432
        //Password:           1JmCkEVkoU-Bzkz-S2aYRzAh3P
        //Psql:                      heroku pg:psql --app sms-thingtech DATABASE
        //URL:                       postgres://kptrpgqqylqibj:1JmCkEVkoU-Bzkz-S2aYRzAh3P@ec2-54-204-3-188.compute-1.amazonaws.com:5432/d70mbahpcgbmd4

        public void OpenConn()
        {
            try
            {
                conn.Open();
            }
            catch (Exception ne)
            {
                Console.WriteLine("Problem connecting to server, Error details {0}", ne.ToString());
            }
        }

        public void CloseConn()
        {
            try
            {
                conn.Close();
            }
            catch (Exception ne)
            {
                Console.WriteLine("Problem connecting to server, Error details {0}", ne.ToString());
            }
        } 
    }
}