using atpac.webapi.Controllers;
using atpac.webapi.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace atpac.webapi.Helper
{
    public class OrganizationHelper : BaseApiController
    {
        public List<Organization> QueryAllOnOrganization()
        {
            try
            {
                this.OpenConn();
                string SQL = "SELECT * FROM Organization";
                NpgsqlCommand command = new NpgsqlCommand(SQL, conn);
                NpgsqlDataReader dr = command.ExecuteReader();
                List<Organization> orgs = new List<Organization>();

                while (dr.Read())
                {
                    Organization org = new Organization();
                    org.Id = Convert.ToInt32(dr["id"]);
                    org.Type = dr["type"].ToString();
                    org.Name = dr["name"].ToString();
                    org.Description = dr["description"].ToString();
                    orgs.Add(org);
                }
                this.CloseConn();
                return orgs;
            }
            catch (Exception ne)
            {
                Console.WriteLine("error on query table connecting to server, Error details {0}", ne.ToString());
                return null;
            }
        }

        public Organization QueryOnOrganizationBy(Int32 id)
        {
            try
            {
                this.OpenConn();

                string SQL = "SELECT * FROM Organization WHERE Id = " + id;
                NpgsqlCommand command = new NpgsqlCommand(SQL, conn);
                NpgsqlDataReader dr = command.ExecuteReader();
                Organization org = new Organization();

                while (dr.Read())
                {
                    org.Id = Convert.ToInt32(dr["id"]);
                    org.Type = dr["type"].ToString();
                    org.Name = dr["name"].ToString();
                    org.Description = dr["description"].ToString();
                    return org;
                }
                this.CloseConn();
                return org;
            }
            catch (Exception ne)
            {
                Console.WriteLine("error on query table connecting to server, Error details {0}", ne.ToString());
                return null;
            }
            finally
            { 
                conn.Close();
                conn.Dispose();
            }    
        }

        public object CreateOrganization(Organization obj)
        {             
            object orgObject = new object();
            try
            {
                this.OpenConn();
                string sqlQuery = string.Format("INSERT into Organization (TYPE, NAME, DESCRIPTION) VALUES ('{0}', '{1}','{2}') RETURNING id;", obj.Type, obj.Name, obj.Description);
                // Execute command
                NpgsqlCommand command = new NpgsqlCommand(sqlQuery, conn);
                orgObject = command.ExecuteScalar();
                this.CloseConn();

            }
            catch (Exception ne)
            {
                Console.WriteLine("error inserting, Error details {0}", ne.ToString());
            }
            return orgObject;
        }

        public object UpdateOrganization(int id, Organization obj)
        {
            object orgObject = new object();
            try
            {
                this.OpenConn();

                string sqlQuery = string.Format("UPDATE Organization SET TYPE = '{0}', NAME = '{1}', DESCRIPTION = '{2}' WHERE ID = {3} RETURNING id; ", obj.Type, obj.Name, obj.Description, id);
                // Execute command
                NpgsqlCommand command = new NpgsqlCommand(sqlQuery, conn);
                orgObject = command.ExecuteScalar();
                this.CloseConn();

            }
            catch (Exception ne)
            {
                Console.WriteLine("error inserting, Error details {0}", ne.ToString());
            }
            return orgObject;
        }

        public List<Organization> DeleteOrganizationReturnList(Int32 id)
        {
            Int32 rowsaffected = 0;
            try
            {
                this.OpenConn();

                string SQL = "DELETE FROM Organization WHERE Id = " + id;
                NpgsqlCommand command = new NpgsqlCommand(SQL, conn);
                rowsaffected = command.ExecuteNonQuery(); 
                this.CloseConn();
                return QueryAllOnOrganization();
            }
            catch (Exception ne)
            {
                Console.WriteLine("error on query table connecting to server, Error details {0}", ne.ToString());
                return null;
            }
        }

        public bool DeleteOrganization(Int32 id)
        {
            bool rowsaffected = false;
            try
            {
                this.OpenConn();

                string SQL = "DELETE FROM Organization WHERE Id = " + id;
                NpgsqlCommand command = new NpgsqlCommand(SQL, conn);                
                rowsaffected = command.ExecuteNonQuery() != 0;
                this.CloseConn();
                
            }
            catch (Exception ne)
            {
                Console.WriteLine("error on query table connecting to server, Error details {0}", ne.ToString());                 
            }
            return rowsaffected;
        }

    }
}