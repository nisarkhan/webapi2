using atpac.webapi.Controllers;
using atpac.webapi.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace atpac.webapi.Helper
{
    public class WorkOrderRequestHelper : BaseApiController
    {        
        public List<WorkOrderRequest> QueryAllOnWorkOrderRequest()
        {
            try
            {
                DataSet wor_ds = new DataSet();
                DataTable wor_dt = new DataTable();

                this.OpenConn();
                string SQL = "SELECT * FROM Work_Order_Request";
                //NpgsqlCommand command = new NpgsqlCommand(SQL, conn);
                NpgsqlDataAdapter wor_da = new NpgsqlDataAdapter(SQL, conn);
                //NpgsqlDataReader dr = command.ExecuteReader();

                wor_ds.Reset();
                wor_da.Fill(wor_ds);
                wor_dt = wor_ds.Tables[0];
                
                List<WorkOrderRequest> worList = new List<WorkOrderRequest>();
                foreach (DataRow row in wor_dt.Rows)
                {
                    List<WorkPackage> wp_list = new List<WorkPackage>();
                    
                    List<Area> area_list = new List<Area>();
                    List<ScaffoldType> st_list = new List<ScaffoldType>();
                    List<Scaffold> s_list = new List<Scaffold>();

                    List<ProcessChainType> pct_list = new List<ProcessChainType>();
                    List<Contract> c_list = new List<Contract>();
                    List<Organization> org_list = new List<Organization>();

                    WorkOrderRequest wor = new WorkOrderRequest();

                    wor.Id = row.Field<Int64?>("id") ?? 0;
                    wor.Type = row["type"].ToString();
                    wor.ScaffoldTypeId = row.GetValueOrDefault<int>("scaffold_type_id"); //row.Field<Int64?>("scaffold_type_id") ?? 0;
                    wor.ScaffoldId = row.GetValueOrDefault<int>("scaffold_tag");  

                    wor.Type = row["type"].ToString();
                    wor.RequestedBy = row["requested_by"].ToString();                   
                    wor.RevisionDate = row.Field<DateTime?>("revision_date") ?? DateTime.MinValue;

                    wor.Status = row["status"].ToString();
                    wor.Priority = row["priority"].ToString();
                    wor.Trade = row["trade"].ToString();
                    wor.PointOfContact = row["point_of_contact"].ToString();

                    wor.RequiredDate = row.Field<DateTime?>("requested_date") ?? DateTime.MinValue;
                    wor.RequiredUntil = row.Field<DateTime?>("required_until") ?? DateTime.MinValue;

                    wor.WorkPackageId = row.Field<Int64?>("work_package_id") ?? 0;  

                    /*xcol1id = row.Field<int?>("col1id") ?? 0,
                    xcol2id = row.Field<string>("col2id") ?? String.Empty,
                    xcol3id = row.Field<decimal?>("col3id") ?? 0M*/


                    wor.AreaId = row.Field<Int64?>("area_id") ?? 0;
                    wor.ProcessSeqId = row.Field<Int64?>("process_seq") ?? 0;

                    wor.Finalized = row.Field<bool?>("finalized") ?? false;


                    wor.ContractId = row.Field<int?>("contract_id") ?? 0;

                    wor.EstimateOrganizationId = row.Field<int?>("estimate_organization_id") ?? 0;

                    //SCAFFOLD:
                    if (wor.ScaffoldId != 0)
                    {
                        DataSet s_ds = new DataSet();
                        DataTable s_dt = new DataTable();
                        string SSQL = "SELECT * FROM scaffold WHERE id = " + wor.ScaffoldId;
                        NpgsqlCommand s_cmd = new NpgsqlCommand(SSQL, conn);
                        NpgsqlDataReader s_dr = s_cmd.ExecuteReader();
                        while (s_dr.Read())
                        {
                            Scaffold s = new Scaffold();
                            s.Tag = s_dr["tag"] as Int64? ?? 0;
                            s.ContractId = s_dr["contract_id"] as int? ?? 0;
                            s.OrganizationIdInitiator = s_dr["organization_id_initiator"] as int? ?? 0;
                            s.Status = s_dr["status"].ToString();
                            s.ScaffoldTypeId = s_dr["scaffold_type_id"] as int? ?? 0;
                            s_list.Add(s);
                        }
                    }
                    //SCAFFOLD TYPE:
                    if (wor.ScaffoldTypeId != 0)
                    {
                        DataSet st_ds = new DataSet();
                        DataTable st_dt = new DataTable();
                        string STSQL = "SELECT * FROM scaffold_type WHERE id = " + wor.ScaffoldTypeId;
                        NpgsqlCommand st_cmd = new NpgsqlCommand(STSQL, conn);
                        NpgsqlDataReader st_dr = st_cmd.ExecuteReader();
                        while (st_dr.Read())
                        {
                            ScaffoldType st = new ScaffoldType();
                            st.Id = st_dr["id"] as int? ?? 0;  
                            st.Name = st_dr["name"].ToString();
                            st.Description = st_dr["description"].ToString();
                            st_list.Add(st);
                        }     
                    }
                    //WORK PACKAGE:
                    if (wor.WorkPackageId != 0)
                    {
                        DataSet wp_ds = new DataSet();
                        DataTable wp_dt = new DataTable();
                        string WPSQL = "SELECT * FROM Work_Package WHERE id = " + wor.WorkPackageId;
                        NpgsqlCommand wp_cmd = new NpgsqlCommand(WPSQL, conn);
                        NpgsqlDataReader wp_dr = wp_cmd.ExecuteReader();
                        while (wp_dr.Read())
                        {
                            WorkPackage wp = new WorkPackage();
                            wp.Id = wp_dr["tag"] as Int64? ?? 0;
                            wp.Name = wp_dr["name"].ToString();
                            wp.Description = wp_dr["description"].ToString();
                            wp.OrganizationId = wp_dr["organization_id"] as int? ?? 0;  
                            wp_list.Add(wp);
                        }                        
                    }
                    //AREA:
                    if (wor.AreaId != 0)
                    {
                        DataSet a_ds = new DataSet();
                        DataTable a_dt = new DataTable();
                        string ASQL = "SELECT * FROM Area WHERE ID = " + wor.AreaId;
                        NpgsqlCommand a_cmd = new NpgsqlCommand(ASQL, conn);
                        NpgsqlDataReader a_dr = a_cmd.ExecuteReader();
                        while (a_dr.Read())
                        {
                            Area a = new Area();
                            a.Id = a_dr["id"] as Int64? ?? 0;
                            a.ContractId = Convert.ToInt32(a_dr["contract_id"]);
                            a.Name = a_dr["name"].ToString();
                            a.Path = a_dr["path"].ToString();
                            a.Description = a_dr["description"].ToString();
                            a.Level = a_dr["level"] as int? ?? 0;
                            a.Inactive = a_dr["inactive"] as bool? ?? false; 
                            area_list.Add(a);
                        }
                    }
                    //PROCESS CHAIN TYPE
                    if(wor.ProcessSeqId != 0)
                    {
                        DataSet pct_ds = new DataSet();
                        DataTable pct_dt = new DataTable();
                        string PCTSQL = "SELECT * FROM Process_Chain_Type where seq = " + wor.ProcessSeqId;
                        NpgsqlCommand pct_cmd = new NpgsqlCommand(PCTSQL, conn);
                        NpgsqlDataReader pct_dr = pct_cmd.ExecuteReader();
                        while (pct_dr.Read())
                        {
                            ProcessChainType pct = new ProcessChainType();
                            pct.Seq = pct_dr["seq"] as int? ?? 0;
                            pct.ContractId = pct_dr["contract_id"] as int? ?? 0;  
                            pct.Description = pct_dr["description"].ToString();
                            pct_list.Add(pct);
                        }
                    }
                    //CONTRACT
                    if (wor.ContractId != 0)
                    {
                        DataSet c_ds = new DataSet();
                        DataTable c_dt = new DataTable();
                        string CSQL = "SELECT * FROM Contract WHERE id = " + wor.ContractId;
                        NpgsqlCommand c_cmd = new NpgsqlCommand(CSQL, conn);
                        NpgsqlDataReader c_dr = c_cmd.ExecuteReader();
                        while (c_dr.Read())
                        {
                            Contract c = new Contract();
                            c.Id = c_dr["id"] as Int64? ?? 0;
                            c.Name = c_dr["name"].ToString();
                            c.Description = c_dr["description"].ToString();
                            c.OrganizationIdPrincipal = c_dr["organization_id_principal"] as Int64? ?? 0; 
                            c_list.Add(c);
                        }
                    }
                    //ORGANIZATION
                    if (wor.EstimateOrganizationId != 0)
                    {
                        DataSet org_ds = new DataSet();
                        DataTable org_dt = new DataTable();
                        string ORGSQL = "SELECT * FROM Organization WHERE id = " + wor.EstimateOrganizationId;
                        NpgsqlCommand org_cmd = new NpgsqlCommand(ORGSQL, conn);
                        NpgsqlDataReader org_dr = org_cmd.ExecuteReader();
                        while (org_dr.Read())
                        {
                            Organization org = new Organization();
                            org.Id = org_dr["id"] as Int64? ?? 0;
                            org.Type = org_dr["type"].ToString();
                            org.Name = org_dr["name"].ToString();
                            org.Description = org_dr["description"].ToString();
                            org_list.Add(org);
                        }
                    }

                    wor.WorkPackage = wp_list;
                    wor.Area = area_list;
                    wor.ProcessChainType = pct_list;
                    wor.ScaffoldType = st_list;
                    wor.Scaffold = s_list;
                    wor.Contract = c_list;
                    wor.EstimateOrganization = org_list; 
                    //add
                    worList.Add(wor);
                    
                } 

                 
                this.CloseConn();
                return worList;
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

        public List<WorkOrderRequest> WorkOrderRequestById(string sql)
        {
            try
            {
                DataSet wor_ds = new DataSet();
                DataTable wor_dt = new DataTable();

                this.OpenConn();
                //string SQL = "SELECT * FROM Work_Order_Request";
                //NpgsqlCommand command = new NpgsqlCommand(SQL, conn);
                NpgsqlDataAdapter wor_da = new NpgsqlDataAdapter(sql, conn);
                //NpgsqlDataReader dr = command.ExecuteReader();

                wor_ds.Reset();
                wor_da.Fill(wor_ds);
                wor_dt = wor_ds.Tables[0];

                List<WorkOrderRequest> worList = new List<WorkOrderRequest>();
                foreach (DataRow row in wor_dt.Rows)
                {
                    List<WorkPackage> wp_list = new List<WorkPackage>();

                    List<Area> area_list = new List<Area>();
                    List<ScaffoldType> st_list = new List<ScaffoldType>();
                    List<Scaffold> s_list = new List<Scaffold>();

                    List<ProcessChainType> pct_list = new List<ProcessChainType>();
                    List<Contract> c_list = new List<Contract>();
                    List<Organization> org_list = new List<Organization>();

                    WorkOrderRequest wor = new WorkOrderRequest();

                    wor.Id = row.Field<Int64?>("id") ?? 0;
                    wor.Type = row["type"].ToString();
                    wor.ScaffoldTypeId = row.Field<int?>("scaffold_type_id") ?? 0;
                    wor.ScaffoldId = row.Field<Int64?>("scaffold_tag") ?? 0;

                    wor.Type = row["type"].ToString();
                    wor.RequestedBy = row["requested_by"].ToString();
                    wor.RevisionDate = row.Field<DateTime?>("revision_date") ?? DateTime.MinValue;

                    wor.Status = row["status"].ToString();
                    wor.Priority = row["priority"].ToString();
                    wor.Trade = row["trade"].ToString();
                    wor.PointOfContact = row["point_of_contact"].ToString();

                    wor.RequiredDate = row.Field<DateTime?>("requested_date") ?? DateTime.MinValue;
                    wor.RequiredUntil = row.Field<DateTime?>("required_until") ?? DateTime.MinValue;

                    wor.WorkPackageId = row.Field<Int64?>("work_package_id") ?? 0;

                    /*xcol1id = row.Field<int?>("col1id") ?? 0,
                    xcol2id = row.Field<string>("col2id") ?? String.Empty,
                    xcol3id = row.Field<decimal?>("col3id") ?? 0M*/


                    wor.AreaId = row.Field<Int64?>("area_id") ?? 0;
                    wor.ProcessSeqId = row.Field<Int64?>("process_seq") ?? 0;

                    wor.Finalized = row.Field<bool?>("finalized") ?? false;


                    wor.ContractId = row.Field<int?>("contract_id") ?? 0;

                    wor.EstimateOrganizationId = row.Field<int?>("estimate_organization_id") ?? 0;

                    //SCAFFOLD:
                    if (wor.ScaffoldId != 0)
                    {
                        DataSet s_ds = new DataSet();
                        DataTable s_dt = new DataTable();
                        string SSQL = "SELECT * FROM scaffold where Id = " + wor.ScaffoldId;
                        NpgsqlCommand s_cmd = new NpgsqlCommand(SSQL, conn);
                        NpgsqlDataReader s_dr = s_cmd.ExecuteReader();
                        while (s_dr.Read())
                        {
                            Scaffold s = new Scaffold();
                            s.Tag = s_dr["tag"] as int? ?? 0;
                            s.ContractId = s_dr["contract_id"] as int? ?? 0;
                            s.OrganizationIdInitiator = s_dr["organization_id_initiator"] as int? ?? 0;
                            s.Status = s_dr["status"].ToString();
                            s.ScaffoldTypeId = s_dr["scaffold_type_id"] as int? ?? 0;
                            s_list.Add(s);
                        }
                    }
                    //SCAFFOLD TYPE:
                    if (wor.ScaffoldTypeId != 0)
                    {
                        DataSet st_ds = new DataSet();
                        DataTable st_dt = new DataTable();
                        string STSQL = "SELECT * FROM scaffold_type where Id = " + wor.ScaffoldTypeId;
                        NpgsqlCommand st_cmd = new NpgsqlCommand(STSQL, conn);
                        NpgsqlDataReader st_dr = st_cmd.ExecuteReader();
                        while (st_dr.Read())
                        {
                            ScaffoldType st = new ScaffoldType();
                            st.Id = st_dr["id"] as int? ?? 0;
                            st.Name = st_dr["name"].ToString();
                            st.Description = st_dr["description"].ToString();
                            st_list.Add(st);
                        }
                    }
                    //WORK PACKAGE:
                    if (wor.WorkPackageId != 0)
                    {
                        DataSet wp_ds = new DataSet();
                        DataTable wp_dt = new DataTable();
                        string WPSQL = "SELECT * FROM Work_Package where Id = " + wor.WorkPackageId;
                        NpgsqlCommand wp_cmd = new NpgsqlCommand(WPSQL, conn);
                        NpgsqlDataReader wp_dr = wp_cmd.ExecuteReader();
                        while (wp_dr.Read())
                        {
                            WorkPackage wp = new WorkPackage();
                            wp.Id = wp_dr["tag"] as int? ?? 0;
                            wp.Name = wp_dr["name"].ToString();
                            wp.Description = wp_dr["description"].ToString();
                            wp.OrganizationId = wp_dr["organization_id"] as int? ?? 0;
                            wp_list.Add(wp);
                        }
                    }
                    //AREA:
                    if (wor.AreaId != 0)
                    {
                        DataSet a_ds = new DataSet();
                        DataTable a_dt = new DataTable();
                        string ASQL = "SELECT * FROM Area where Id = " + wor.AreaId;
                        NpgsqlCommand a_cmd = new NpgsqlCommand(ASQL, conn);
                        NpgsqlDataReader a_dr = a_cmd.ExecuteReader();
                        while (a_dr.Read())
                        {
                            Area a = new Area();
                            a.Id = a_dr["id"] as int? ?? 0;
                            a.ContractId = Convert.ToInt32(a_dr["contract_id"]);
                            a.Name = a_dr["name"].ToString();
                            a.Path = a_dr["path"].ToString();
                            a.Description = a_dr["description"].ToString();
                            a.Level = a_dr["level"] as int? ?? 0;
                            a.Inactive = a_dr["inactive"] as bool? ?? false;
                            area_list.Add(a);
                        }
                    }
                    //PROCESS CHAIN TYPE
                    if (wor.ProcessSeqId != 0)
                    {
                        DataSet pct_ds = new DataSet();
                        DataTable pct_dt = new DataTable();
                        string PCTSQL = "SELECT * FROM Process_Chain_Type where seq = " + wor.ProcessSeqId;
                        NpgsqlCommand pct_cmd = new NpgsqlCommand(PCTSQL, conn);
                        NpgsqlDataReader pct_dr = pct_cmd.ExecuteReader();
                        while (pct_dr.Read())
                        {
                            ProcessChainType pct = new ProcessChainType();
                            pct.Seq = pct_dr["seq"] as int? ?? 0;
                            pct.ContractId = pct_dr["contract_id"] as int? ?? 0;
                            pct.Description = pct_dr["description"].ToString();
                            pct_list.Add(pct);
                        }
                    }
                    //CONTRACT
                    if (wor.ContractId != 0)
                    {
                        DataSet c_ds = new DataSet();
                        DataTable c_dt = new DataTable();
                        string CSQL = "SELECT * FROM Contract where id = " + wor.ContractId;
                        NpgsqlCommand c_cmd = new NpgsqlCommand(CSQL, conn);
                        NpgsqlDataReader c_dr = c_cmd.ExecuteReader();
                        while (c_dr.Read())
                        {
                            Contract c = new Contract();
                            c.Id = c_dr["id"] as Int64? ?? 0;
                            c.Name = c_dr["name"].ToString();
                            c.Description = c_dr["description"].ToString();
                            c.OrganizationIdPrincipal = c_dr["organization_id_principal"] as Int64? ?? 0;
                            c_list.Add(c);
                        }
                    }
                    //ORGANIZATION
                    if (wor.EstimateOrganizationId != 0)
                    {
                        DataSet org_ds = new DataSet();
                        DataTable org_dt = new DataTable();
                        string ORGSQL = "SELECT * FROM Organization where id = " + wor.EstimateOrganizationId;
                        NpgsqlCommand org_cmd = new NpgsqlCommand(ORGSQL, conn);
                        NpgsqlDataReader org_dr = org_cmd.ExecuteReader();
                        while (org_dr.Read())
                        {
                            Organization org = new Organization();
                            org.Id = org_dr["id"] as int? ?? 0;
                            org.Type = org_dr["type"].ToString();
                            org.Name = org_dr["name"].ToString();
                            org.Description = org_dr["description"].ToString();
                            org_list.Add(org);
                        }
                    }

                    wor.WorkPackage = wp_list;
                    wor.Area = area_list;
                    wor.ProcessChainType = pct_list;
                    wor.ScaffoldType = st_list;
                    wor.Scaffold = s_list;
                    wor.Contract = c_list;
                    wor.EstimateOrganization = org_list;
                    //add
                    worList.Add(wor);

                }


                this.CloseConn();
                return worList;
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

        //public List<WorkOrderRequest> WorkOrderRequestById(int id)
        //{
        //    try
        //    {
        //        this.OpenConn();

        //        string SQL = "SELECT * FROM Work_Order_Request WHERE Id = " + id;
        //        NpgsqlCommand command = new NpgsqlCommand(SQL, conn);
        //        NpgsqlDataReader dr = command.ExecuteReader();
        //        List<WorkOrderRequest> worList = new List<WorkOrderRequest>();

        //        while (dr.Read())
        //        {
        //            WorkOrderRequest wor = new WorkOrderRequest();
        //            wor.Id = Convert.ToInt32(dr["id"]);
        //            wor.Type = dr["type"].ToString();
        //            worList.Add(wor);
        //        }
        //        this.CloseConn();
        //        return worList;
        //    }
        //    catch (Exception ne)
        //    {
        //        Console.WriteLine("error on query table connecting to server, Error details {0}", ne.ToString());
        //        return null;
        //    }
        //    finally
        //    {
        //        conn.Close();
        //        conn.Dispose();
        //    } 

        //}
    }
}