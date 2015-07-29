using atpac.webapi.Controllers;
using atpac.webapi.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace atpac.webapi.Helper
{
    public class OrganizationTypeHelper : BaseApiController
    {
        public List<OrganizationType> QueryAllOnOrganizationType()
        {
            try
            {
                this.OpenConn();
                string SQL = "SELECT * FROM Organization_Type";
                NpgsqlCommand command = new NpgsqlCommand(SQL, conn);
                NpgsqlDataReader dr = command.ExecuteReader();
                List<OrganizationType> orgTypes = new List<OrganizationType>();

                while (dr.Read())
                {
                    OrganizationType type = new OrganizationType(); 
                    type.Name = dr["name"].ToString();
                    type.Description = dr["description"].ToString();
                    orgTypes.Add(type);
                }
                this.CloseConn();
                return orgTypes;
            }
            catch (Exception ne)
            {
                Console.WriteLine("error on query table connecting to server, Error details {0}", ne.ToString());
                return null;
            }
        }

        public OrganizationType QueryOnOrganizationTypeBy(string name)
        {
            try
            {
                this.OpenConn();

                string SQL = string.Format("SELECT * FROM Organization_Type WHERE Name = '{0}'", name);
                NpgsqlCommand command = new NpgsqlCommand(SQL, conn);
                NpgsqlDataReader dr = command.ExecuteReader();
                OrganizationType org = new OrganizationType();

                while (dr.Read())
                {
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
        }

        public object CreateOrganizationType(OrganizationType obj)
        {
            Int32 rowsaffected = 0;
            try
            {
                this.OpenConn();
                string sqlQuery = string.Format("INSERT into Organization_Type (NAME, DESCRIPTION) VALUES ('{0}', '{1}')", obj.Name, obj.Description);
                // Execute command
                NpgsqlCommand command = new NpgsqlCommand(sqlQuery, conn);
                rowsaffected = command.ExecuteNonQuery();
                this.CloseConn();

            }
            catch (Exception ne)
            {
                Console.WriteLine("error inserting, Error details {0}", ne.ToString());
            }
            return rowsaffected;
        }

        public object UpdateOrganizationType(string name, OrganizationType obj)
        {
            object orgObject = new object();
            try
            {
                this.OpenConn();

                string sqlQuery = string.Format("UPDATE Organization_Type SET NAME = '{0}', DESCRIPTION = '{1}' WHERE Name = {2} RETURNING id; ", obj.Name, obj.Description, name);
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

        public List<OrganizationType> DeleteOrganizationTypeReturnList(string name)
        {
            Int32 rowsaffected = 0;
            try
            {
                this.OpenConn();
                string SQL = string.Format("DELETE FROM Organization_Type WHERE Name = '{0}'", name);
                NpgsqlCommand command = new NpgsqlCommand(SQL, conn);
                rowsaffected = command.ExecuteNonQuery();
                this.CloseConn();
                return QueryAllOnOrganizationType();
            }
            catch (Exception ne)
            {
                Console.WriteLine("error on query table connecting to server, Error details {0}", ne.ToString());
                return null;
            }
        }

        public bool DeleteOrganizationType(string name)
        {
            bool rowsaffected = false;
            try
            {
                this.OpenConn();                
                string SQL = string.Format("DELETE FROM Organization_Type WHERE Name = '{0}'", name);
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