using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace r2rStudio.Business.Lib
{
    public class r2rLib
    {
        public r2rLib(string ConStr)
        {
            ConnectionString = ConStr;
        }

        public string ConnectionString { get; set; }

        public int AddBot(r2rBot Bot)
        {
            int res = 0;

            try
            {

                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "INSERT INTO r2rBotHdr (title, FKCreatedBy, xaml, purpose, benefit, notes, FKTeam, type, FKApproverAdmin, FKApproverManager, ";
                        cmd.CommandText += "isApprovedAdmin, isApprovedManager, active, createdDate, modifiedDate) VALUES (@title, @createdBy, '', @purpose, @benefit, ";
                        cmd.CommandText += "@notes, @team, @type, @approverAdmin, @approverManager, @approvedAdmin, @approvedManager, 1, GETDATE(), GETDATE())";

                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@title", Bot.Title);
                        cmd.Parameters.AddWithValue("@createdBy", Bot.CreatedBy.Id);
                        cmd.Parameters.AddWithValue("@purpose", Bot.Purpose);
                        cmd.Parameters.AddWithValue("@benefit", Bot.Benefit);
                        cmd.Parameters.AddWithValue("@notes", Bot.Notes);
                        cmd.Parameters.AddWithValue("@team", Bot.Team.Id);
                        cmd.Parameters.AddWithValue("@type", Bot.Type);
                        cmd.Parameters.AddWithValue("@approverAdmin", Bot.ApproverAdmin.Id);
                        cmd.Parameters.AddWithValue("@approverManager", Bot.ApproverManager.Id);
                        cmd.Parameters.AddWithValue("@approvedAdmin", Bot.ApprovedByAdmin);
                        cmd.Parameters.AddWithValue("@approvedManager", Bot.ApprovedByManager);

                        cmd.ExecuteNonQuery();

                        using (SqlCommand cmd2 = conn.CreateCommand())
                        {
                            cmd2.CommandText = "select ident_current('r2rBotHdr') AS 'Id'";
                            cmd2.CommandType = CommandType.Text;

                            SqlDataReader reader = cmd2.ExecuteReader();

                            while (reader.Read())
                            {
                                res = Convert.ToInt32(reader["id"].ToString());
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                res = 0;
            }

            return res;
        }

        public bool UserExist(string Email, out r2rUser user)
        {
            bool res = false;
            r2rUser o1 = new r2rUser();

            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT * FROM r2rUserMst WHERE email = @email";
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@email", Email);

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            o1.Id = Convert.ToInt32(reader["id"].ToString());
                            o1.Name = reader["name"].ToString();
                            o1.Email = reader["email"].ToString();
                            o1.Active = Convert.ToBoolean(reader["active"].ToString());

                            res = true;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                res = false;
            }

            user = o1;
            return res;
        }

        public List<r2rBot> GetBots(int UserId)
        {
            List<r2rBot> lstBots = new List<r2rBot>();

            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT * FROM r2rBotHdr WHERE FKCreatedBy = @userId AND active = 1";
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@userId", UserId);

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            r2rBot o1 = new r2rBot();

                            o1.Id = Convert.ToInt32(reader["id"].ToString());
                            o1.Title = reader["title"].ToString();
                            o1.CreatedBy = new r2rUser() { Id = UserId };
                            o1.XAML = reader["xaml"].ToString();
                            o1.Purpose = reader["purpose"].ToString();
                            o1.Benefit = reader["benefit"].ToString();
                            o1.Notes = reader["notes"].ToString();
                            o1.Type = Convert.ToInt32(reader["type"].ToString());

                            o1.ApproverAdmin = new r2rUser() { Id = Convert.ToInt32(reader["FKApproverAdmin"].ToString()) };
                            o1.ApproverManager = new r2rUser() { Id = Convert.ToInt32(reader["FKApproverManager"].ToString()) };

                            o1.ApprovedByAdmin = Convert.ToBoolean(reader["isApprovedAdmin"].ToString());
                            o1.ApprovedByManager = Convert.ToBoolean(reader["isApprovedManager"].ToString());

                            o1.DateCreated = Convert.ToDateTime(reader["createdDate"].ToString());
                            o1.DateModified = Convert.ToDateTime(reader["modifiedDate"].ToString());

                            o1.Active = true;

                            lstBots.Add(o1);

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return lstBots;
        }

        public List<r2rBot> GetSharedBots(int UserId)
        {
            List<r2rBot> lstBots = new List<r2rBot>();

            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT * FROM r2rBotHdr WHERE active = 1 AND Id IN (SELECT FKBot FROM r2rBotUserDtl WHERE FKUser = @userId)";
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@userId", UserId);

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            r2rBot o1 = new r2rBot();

                            o1.Id = Convert.ToInt32(reader["id"].ToString());
                            o1.Title = reader["title"].ToString();
                            o1.CreatedBy = new r2rUser() { Id = UserId };
                            o1.XAML = reader["xaml"].ToString();
                            o1.Purpose = reader["purpose"].ToString();
                            o1.Benefit = reader["benefit"].ToString();
                            o1.Notes = reader["notes"].ToString();
                            o1.Type = Convert.ToInt32(reader["type"].ToString());

                            o1.ApproverAdmin = new r2rUser() { Id = Convert.ToInt32(reader["FKApproverAdmin"].ToString()) };
                            o1.ApproverManager = new r2rUser() { Id = Convert.ToInt32(reader["FKApproverManager"].ToString()) };

                            o1.ApprovedByAdmin = Convert.ToBoolean(reader["isApprovedAdmin"].ToString());
                            o1.ApprovedByManager = Convert.ToBoolean(reader["isApprovedManager"].ToString());

                            o1.DateCreated = Convert.ToDateTime(reader["createdDate"].ToString());
                            o1.DateModified = Convert.ToDateTime(reader["modifiedDate"].ToString());

                            o1.Active = true;

                            lstBots.Add(o1);

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return lstBots;
        }

        public r2rBot GetBot(int Id)
        {
            r2rBot bot = new r2rBot();

            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT * FROM r2rBotHdr WHERE Id = @Id";
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@Id", Id);

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            bot.Id = Convert.ToInt32(reader["id"].ToString());
                            bot.Title = reader["title"].ToString();
                            bot.CreatedBy = new r2rUser() { Id = Convert.ToInt32(reader["FKCreatedBy"].ToString()) };
                            bot.XAML = reader["xaml"].ToString();
                            bot.Purpose = reader["purpose"].ToString();
                            bot.Benefit = reader["benefit"].ToString();
                            bot.Notes = reader["notes"].ToString();
                            bot.Type = Convert.ToInt32(reader["type"].ToString());

                            bot.ApproverAdmin = new r2rUser() { Id = Convert.ToInt32(reader["FKApproverAdmin"].ToString()) };
                            bot.ApproverManager = new r2rUser() { Id = Convert.ToInt32(reader["FKApproverManager"].ToString()) };

                            bot.ApprovedByAdmin = Convert.ToBoolean(reader["isApprovedAdmin"].ToString());
                            bot.ApprovedByManager = Convert.ToBoolean(reader["isApprovedManager"].ToString());

                            bot.DateCreated = Convert.ToDateTime(reader["createdDate"].ToString());
                            bot.DateModified = Convert.ToDateTime(reader["modifiedDate"].ToString());

                            bot.Active = Convert.ToBoolean(reader["active"].ToString()); ;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return bot;
        }

        public bool RegisterUser(string Email)
        {
            bool res = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "INSERT INTO r2rUserMst (name, email, active, createdDate, modifiedDate) VALUES (@email, @email, 0, GETDATE(), GETDATE())";
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@email", Email);

                        cmd.ExecuteNonQuery();
                        res = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                res = false;
            }
            return res;
        }

        public bool UpdateXAML(int Id, string XAML)
        {
            bool res = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "UPDATE r2rBotHdr SET xaml = @xaml WHERE Id = @Id";
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@xaml", XAML);
                        cmd.Parameters.AddWithValue("@id", Id);
                        cmd.ExecuteNonQuery();
                        res = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                res = false;
            }
            return res;
        }
    }

    public class r2rUser
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public bool Active { get; set; }

        public r2rRole Role { get; set; }

        public r2rTeam Team { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }
    }

    public class r2rRole
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public bool Active { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public List<r2rUser> GetRoleUsers()
        {
            List<r2rUser> lstUser = new List<r2rUser>();

            return lstUser;
        }
    }

    public class r2rTeam
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public r2rUser Manager { get; set; }

        public bool Active { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public List<r2rUser> GetTeamUsers()
        {
            List<r2rUser> lstUser = new List<r2rUser>();

            return lstUser;
        }
    }

    public class r2rBot
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public r2rUser CreatedBy { get; set; }

        public string XAML { get; set; }

        public string Purpose { get; set; }

        public string Benefit { get; set; }

        public r2rTeam Team { get; set; }

        public string Notes { get; set; }

        public int Type { get; set; }

        public r2rUser ApproverAdmin { get; set; }

        public r2rUser ApproverManager { get; set; }

        public bool ApprovedByAdmin { get; set; }

        public bool ApprovedByManager { get; set; }

        public bool Active { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        //METHODS
        //
        public List<r2rBotRun> GetRunDetails()
        {
            List<r2rBotRun> lstRun = new List<r2rBotRun>();
            return lstRun;
        }



    }

    public class r2rBotRun
    {
        public int Id { get; set; }

        public r2rBot Bot { get; set; }

        public DateTime DateRun { get; set; }

        public int Status { get; set; }

        public string Log { get; set; }
    }

    public class r2rBotSchedule
    {
        public int Id { get; set; }

        public r2rBot Bot { get; set; }

        public int Occurance { get; set; }

        public DateTime DateStart { get; set; }

        public DateTime DateEnd { get; set; }

        public bool Active { get; set; }

        public List<r2rBotScheduleOccurance> GetScheduleOccurance()
        {
            List<r2rBotScheduleOccurance> lstScheduleOccurance = new List<r2rBotScheduleOccurance>();


            return lstScheduleOccurance;
        }

    }

    public class r2rBotScheduleOccurance
    {
        public int Id { get; set; }

        public r2rBotSchedule BotSchedule { get; set; }

        public int Occurance { get; set; }

        public DateTime OccurOn { get; set; }

        public bool DidOccur { get; set; }


        public int Status { get; set; }

        public string Log { get; set; }

    }
}
