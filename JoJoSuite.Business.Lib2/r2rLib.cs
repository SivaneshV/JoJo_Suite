using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace JoJoSuite.Business.Lib
{
    public class r2rLib
    {
        public static int r2rid { get; set; }


        public string ConnectionString { get; set; }

        public r2rLib(string ConStr)
        {
            ConnectionString = ConStr;
        }

        public bool CheckDBConnection()
        {
            bool res = false;

            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    res = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                res = false;
            }
            return res;
        }

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
                        cmd.CommandText = "R2rInsertBotHdr";                       
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@title", Bot.Title);
                        cmd.Parameters.AddWithValue("@FKCreatedBy", Bot.CreatedBy.Id);
                        cmd.Parameters.AddWithValue("@functionality", Bot.Functionality);
                        cmd.Parameters.AddWithValue("@benefit", Bot.Benefit);
                        cmd.Parameters.AddWithValue("@FKTeam", Bot.Team.Id);
                        cmd.Parameters.AddWithValue("@type", Bot.Type);
                        cmd.Parameters.AddWithValue("@FKApproverAdmin", Bot.ApproverAdmin.Id);
                        cmd.Parameters.AddWithValue("@FKApproverManager", Bot.ApproverManager.Id);
                        cmd.Parameters.AddWithValue("@isApprovedAdmin", Bot.ApprovedByAdmin);
                        cmd.Parameters.AddWithValue("@isApprovedManager", Bot.ApprovedByManager);
                        cmd.Parameters.AddWithValue("@noOfPeople", Bot.NumberOfPeople);
                        cmd.Parameters.AddWithValue("@manualMins", Bot.ManualMinutes);
                        cmd.Parameters.AddWithValue("@tech", Bot.Technologies);
                        cmd.Parameters.AddWithValue("@apps", Bot.Applications);             
                        cmd.Parameters.AddWithValue("@isProduction", Bot.isProduction);
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

        public int AddPassword(r2rBotPassword Pwd)
        {
            int res = 0;

            try
            {

                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "INSERT INTO r2rBotPwdDtl (FKBot, PwdName, PwdValue) VALUES (@bot, @name, @value)";

                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@bot", Pwd.Bot.Id);
                        cmd.Parameters.AddWithValue("@name", Pwd.Name);
                        cmd.Parameters.AddWithValue("@value", Pwd.Password);

                        cmd.ExecuteNonQuery();

                        using (SqlCommand cmd2 = conn.CreateCommand())
                        {
                            cmd2.CommandText = "select ident_current('r2rBotPwdDtl') AS 'Id'";
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

        public bool UpdateBotRunEndTime(r2rBotRun Run, DateTime EndTime)
        {
            bool res = false;

            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "UPDATE r2rBotRunDtl SET endDate=@endTime WHERE Id=@id AND FKBot=@fkbot";
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@id", Run.Id);
                        cmd.Parameters.AddWithValue("@fkbot", Run.Bot.Id);
                        cmd.Parameters.AddWithValue("@endTime", EndTime);

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

        public bool DeleteBot(int BotId)
        {
            bool res = false;

            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "DELETE FROM r2rBotHdr WHERE Id=@id";
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@id", BotId);

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

        public int AddBotRun(r2rBotRun Run)
        {
            int res = 0;

            try
            {

                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "INSERT INTO r2rBotRunDtl (FKBot, runDate, startDate, endDate, tranCount, status, log, testRun, FKUser) ";
                        cmd.CommandText += "VALUES (@bot, @runDate, @startDate, @endDate, 0, 0, '', @testRun, @user) ";

                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@bot", Run.Bot.Id);
                        cmd.Parameters.AddWithValue("@runDate", Run.DateRun);
                        cmd.Parameters.AddWithValue("@startDate", Run.TimeStart);
                        cmd.Parameters.AddWithValue("@endDate", Run.TimeEnd);
                        cmd.Parameters.AddWithValue("@testRun", Run.TestRun);
                        cmd.Parameters.AddWithValue("@user", Run.User.Id);

                        cmd.ExecuteNonQuery();

                        using (SqlCommand cmd2 = conn.CreateCommand())
                        {
                            cmd2.CommandText = "select ident_current('r2rBotRunDtl') AS 'Id'";
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


        public int AddUserToBot(int BotId, int UserId, int Access)
        {
            int res = 0;

            try
            {

                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "INSERT INTO r2rBotUserDtl (FKBot, FKUser, access) VALUES (@botId, @userId, @access)";

                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@botId", BotId);
                        cmd.Parameters.AddWithValue("@userId", UserId);
                        cmd.Parameters.AddWithValue("@access", Access);

                        cmd.ExecuteNonQuery();

                        using (SqlCommand cmd2 = conn.CreateCommand())
                        {
                            cmd2.CommandText = "select ident_current('r2rBotUserDtl') AS 'Id'";
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

        public int AddTeam(r2rTeam Team)
        {
            int res = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "INSERT INTO r2rTeamMst (title, FKManager, FKL2, FKL3, FKL4, FKRegion, active, createdDate, modifiedDate) VALUES ";
                        cmd.CommandText += "(@title, @manager, @l2, @l3, @l4, @region, @active, GETDATE(), GETDATE())";

                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@title", Team.Title);
                        cmd.Parameters.AddWithValue("@manager", Team.Manager.Id);
                        cmd.Parameters.AddWithValue("@l2", Team.L2.Id);
                        cmd.Parameters.AddWithValue("@l3", Team.L3.Id);
                        cmd.Parameters.AddWithValue("@l4", Team.L4.Id);
                        cmd.Parameters.AddWithValue("@region", Team.Region.Id);
                        cmd.Parameters.AddWithValue("@active", Team.Active);

                        cmd.ExecuteNonQuery();

                        using (SqlCommand cmd2 = conn.CreateCommand())
                        {
                            cmd2.CommandText = "select ident_current('r2rTeamMst') AS 'Id'";
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

        public int AddRegion(r2rRegion Region)
        {
            int res = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "INSERT INTO r2rRegionMst (title, active, createdDate, modifiedDate) VALUES (@title, @active, GETDATE(), GETDATE())";

                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@title", Region.Title);
                        cmd.Parameters.AddWithValue("@active", Region.Active);

                        cmd.ExecuteNonQuery();

                        using (SqlCommand cmd2 = conn.CreateCommand())
                        {
                            cmd2.CommandText = "select ident_current('r2rRegionMst') AS 'Id'";
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

        public int AddRole(r2rRole Role)
        {
            int res = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "INSERT INTO r2rRoleMst (title, active, createdDate, modifiedDate) VALUES (@title, @active, GETDATE(), GETDATE())";

                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@title", Role.Title);
                        cmd.Parameters.AddWithValue("@active", Role.Active);

                        cmd.ExecuteNonQuery();

                        using (SqlCommand cmd2 = conn.CreateCommand())
                        {
                            cmd2.CommandText = "select ident_current('r2rRoleMst') AS 'Id'";
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

        public bool UpdateBot(r2rBot Bot)
        {
            bool res = false;

            try
            {

                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "UPDATE r2rBotHdr SET title=@title, functionality=@functionality, benefit=@benefit, noOfPeople=@noOfPeople, manualMins=@manualMins, ";
                        cmd.CommandText += "tech=@tech, apps=@apps, type=@type,isProduction=@isProduction, modifiedDate=GETDATE() WHERE Id=@id";

                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@id", Bot.Id);

                        cmd.Parameters.AddWithValue("@title", Bot.Title);
                        cmd.Parameters.AddWithValue("@functionality", Bot.Functionality);
                        cmd.Parameters.AddWithValue("@benefit", Bot.Benefit);
                        cmd.Parameters.AddWithValue("@noOfPeople", Bot.NumberOfPeople);
                        cmd.Parameters.AddWithValue("@manualMins", Bot.ManualMinutes);
                        cmd.Parameters.AddWithValue("@tech", Bot.Technologies);
                        cmd.Parameters.AddWithValue("@apps", Bot.Applications);
                        cmd.Parameters.AddWithValue("@type", Bot.Type);
                        cmd.Parameters.AddWithValue("@isProduction", Bot.isProduction);
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

        public bool UpdatePassword(r2rBotPassword Pwd)
        {
            bool res = false;

            try
            {

                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "UPDATE r2rBotPwdDtl SET PwdValue=@pwd WHERE Id=@id";

                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@id", Pwd.Id);
                        cmd.Parameters.AddWithValue("@pwd", Pwd.Password);

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

        public bool UpdateTeam(r2rTeam Team)
        {
            bool res = false;

            try
            {

                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "UPDATE r2rTeamMst SET title=@title, FKManager=@manager, FKRegion=@region, active=@active, modifiedDate=GETDATE() WHERE Id=@id";
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@id", Team.Id);

                        cmd.Parameters.AddWithValue("@title", Team.Title);
                        cmd.Parameters.AddWithValue("@manager", Team.Manager.Id);
                        cmd.Parameters.AddWithValue("@region", Team.Region.Id);
                        cmd.Parameters.AddWithValue("@active", Team.Active);

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

        public bool UpdateRegion(r2rRegion Region)
        {
            bool res = false;

            try
            {

                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "UPDATE r2rRegionMst SET title=@title, active=@active, modifiedDate=GETDATE() WHERE Id=@id";
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@id", Region.Id);
                        cmd.Parameters.AddWithValue("@title", Region.Title);
                        cmd.Parameters.AddWithValue("@active", Region.Active);

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

        public bool RemoveUsersFromBot(int BotId,int UserId)
        {
            bool res = false;

            try
            {

                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "DELETE FROM r2rBotUserDtl WHERE FKBot=@botId and FKUser!="+UserId;
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@botId", BotId);

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

        public bool UpdateRole(r2rRole Role)
        {
            bool res = false;

            try
            {

                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "UPDATE r2rRoleMst SET title=@title, active=@active, modifiedDate=GETDATE() WHERE Id=@id";
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@id", Role.Id);
                        cmd.Parameters.AddWithValue("@title", Role.Title);
                        cmd.Parameters.AddWithValue("@active", Role.Active);

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

        public bool UpdateUser(r2rUser User)
        {
            bool res = false;

            try
            {

                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "UPDATE r2rUserMst SET name=@name, active=@active, FKRole=@role, FKTeam=@team, modifiedDate=GETDATE() WHERE Id=@id";
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@id", User.Id);
                        cmd.Parameters.AddWithValue("@name", User.Name);
                        cmd.Parameters.AddWithValue("@active", User.Active);
                        cmd.Parameters.AddWithValue("@role", User.Role.Id);
                        cmd.Parameters.AddWithValue("@team", User.Team.Id);

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

        public bool UserExist(string Email, string Pwd, out r2rUser user)
        {
            bool res = false;
            r2rUser obj = new r2rUser();

            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "r2rGetUserByEmail";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@email", Email);
                        cmd.Parameters.AddWithValue("@pwd", Pwd);

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            obj.Id = Convert.ToInt32(reader["id"].ToString());
                            obj.Name = reader["name"].ToString();
                            obj.Email = reader["email"].ToString();
                            obj.Role = new r2rRole();
                            obj.Role.Id = Convert.ToInt32(reader["FKRole"].ToString());
                            obj.Role.Title = reader["TitleRole"].ToString();
                            obj.Team = new r2rTeam();
                            obj.Team.Id = Convert.ToInt32(reader["FKTeam"].ToString());
                            obj.Team.Title = reader["TitleTeam"].ToString();
                            obj.Team.Manager = new r2rUser();
                            obj.Team.Manager.Id = Convert.ToInt32(reader["TeamManagerId"].ToString());
                            obj.Team.Manager.Name = reader["TeamManagerName"].ToString();
                            obj.Active = Convert.ToBoolean(reader["active"].ToString());

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
        

            user = obj;
            return res;
        }

        public List<r2rTheme> GetThemes()
        {
            List<r2rTheme> lst = new List<r2rTheme>();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT * FROM r2rColorMst";
                        cmd.CommandType = CommandType.Text;

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            r2rTheme obj = new r2rTheme();

                            obj.Id = Convert.ToInt32(reader["id"].ToString());
                            obj.Title = reader["title"].ToString();
                            obj.Color1 = reader["color1"].ToString();
                            obj.Color2 = reader["color2"].ToString();
                            obj.Color3 = reader["color3"].ToString();
                            obj.Color4 = reader["color4"].ToString();
                            obj.Active = Convert.ToBoolean(reader["active"].ToString());

                            lst.Add(obj);

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return lst;
        }

        public r2rTheme ActiveTheme()
        {
            r2rTheme obj = new r2rTheme();

            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT * FROM r2rColorMst WHERE Active = 1";
                        cmd.CommandType = CommandType.Text;

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {

                            obj.Id = Convert.ToInt32(reader["id"].ToString());
                            obj.Title = reader["ThemeName"].ToString();
                            obj.Color1 = reader["color1"].ToString();
                            obj.Color2 = reader["color2"].ToString();
                            obj.Color3 = reader["color3"].ToString();
                            obj.Color4 = reader["color4"].ToString();
                            obj.Active = Convert.ToBoolean(reader["active"].ToString());

                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return obj;
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
                        cmd.CommandText = "r2rGetBots";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@userId", UserId);

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            r2rBot obj = new r2rBot();

                            obj.Id = Convert.ToInt32(reader["id"].ToString());
                            obj.Title = reader["title"].ToString();
                            obj.CreatedBy = new r2rUser() { Id = UserId };
                            obj.XAML = reader["xaml"].ToString();
                            obj.Functionality = reader["functionality"].ToString();
                            obj.Benefit = reader["benefit"].ToString();
                            obj.Type = Convert.ToInt32(reader["type"].ToString());
                            obj.ApproverAdmin = new r2rUser();
                            obj.ApproverAdmin.Id = Convert.ToInt32(reader["FKApproverAdmin"].ToString());
                            obj.ApproverAdmin.Name = reader["ApproverAdminName"].ToString();
                            obj.ApproverManager = new r2rUser();
                            obj.ApproverManager.Id = Convert.ToInt32(reader["FKApproverManager"].ToString());
                            obj.ApproverManager.Name = reader["ApproverManagerName"].ToString();
                            obj.ApprovedByAdmin = Convert.ToBoolean(reader["isApprovedAdmin"].ToString());
                            obj.ApprovedByManager = Convert.ToBoolean(reader["isApprovedManager"].ToString());
                            obj.DateCreated = Convert.ToDateTime(reader["createdDate"].ToString());
                            obj.DateModified = Convert.ToDateTime(reader["modifiedDate"].ToString());
                            obj.Applications = reader["apps"].ToString();
                            obj.Technologies = reader["tech"].ToString();
                            obj.Active = Convert.ToBoolean(reader["active"].ToString());

                            int a1 = 0;
                            int a2 = 0;

                            Int32.TryParse(reader["noOfPeople"].ToString(), out a1);
                            Int32.TryParse(reader["manualMins"].ToString(), out a2);

                            obj.NumberOfPeople = a1;
                            obj.ManualMinutes = a2;

                            lstBots.Add(obj);

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

        public List<r2rBotRun> GetBotRuns(int BotId, int testRun)
        {
            //testRun = 0-all, 1-test, 2-prod
            List<r2rBotRun> lstBots = new List<r2rBotRun>();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        if (testRun == 0)
                        {
                            cmd.CommandText = "r2rGetBotRuns";
                        }
                        else
                        {
                            cmd.CommandText = "r2rGetBotRunsByTestRun";

                            if (testRun == 1)
                            {
                                cmd.Parameters.AddWithValue("@testRun", true);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@testRun", false);
                            }
                        }
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@botId", BotId);

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            r2rBotRun obj = new r2rBotRun();

                            obj.Id = Convert.ToInt32(reader["id"].ToString());
                            obj.Bot = new r2rBot();
                            obj.Bot.Id = BotId;
                            obj.Bot.Title = reader["BotTitle"].ToString();

                            obj.User = new r2rUser();
                            obj.User.Id = Convert.ToInt32(reader["FKUser"].ToString());
                            if (obj.User.Id == 0)
                            {
                                obj.User.Name = "Scheduler";
                            }
                            else
                            {
                                obj.User.Name = reader["UserName"].ToString();
                            }
                            obj.DateRun = Convert.ToDateTime(reader["runDate"].ToString());
                            obj.TimeStart = Convert.ToDateTime(reader["startDate"].ToString());
                            obj.TimeEnd = Convert.ToDateTime(reader["endDate"].ToString());
                            obj.TransactionCount = Convert.ToInt32(reader["tranCount"].ToString());
                            obj.Id = Convert.ToInt32(reader["status"].ToString());
                            obj.Log = reader["log"].ToString();
                            obj.TestRun = Convert.ToBoolean(reader["testRun"].ToString());

                            lstBots.Add(obj);
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

        public List<r2rBotPassword> GetBotPasswords(int BotId)
        {
            List<r2rBotPassword> lstBots = new List<r2rBotPassword>();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "r2rGetBotPwds";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@botId", BotId);

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            r2rBotPassword obj = new r2rBotPassword();

                            obj.Id = Convert.ToInt32(reader["id"].ToString());
                            obj.Bot = new r2rBot();
                            obj.Bot.Id = BotId;
                            obj.Bot.Title = reader["BotTitle"].ToString();

                            obj.Name = reader["PwdName"].ToString();
                            obj.Password = reader["PwdValue"].ToString();

                            lstBots.Add(obj);
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
                        cmd.CommandText = "r2rGetSharedBots";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@userId", UserId);

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            r2rBot obj = new r2rBot();

                            obj.Id = Convert.ToInt32(reader["id"].ToString());
                            obj.Title = reader["title"].ToString();
                            obj.CreatedBy = new r2rUser() { Id = Convert.ToInt32(reader["FKCreatedBy"].ToString()) };
                            obj.XAML = reader["xaml"].ToString();
                            obj.Functionality = reader["functionality"].ToString();
                            obj.Benefit = reader["benefit"].ToString();
                            obj.Type = Convert.ToInt32(reader["type"].ToString());
                            obj.ApproverAdmin = new r2rUser();
                            obj.ApproverAdmin.Id = Convert.ToInt32(reader["FKApproverAdmin"].ToString());
                            obj.ApproverAdmin.Name = reader["ApproverAdminName"].ToString();
                            obj.ApproverManager = new r2rUser();
                            obj.ApproverManager.Id = Convert.ToInt32(reader["FKApproverManager"].ToString());
                            obj.ApproverManager.Name = reader["ApproverManagerName"].ToString();
                            obj.ApprovedByAdmin = Convert.ToBoolean(reader["isApprovedAdmin"].ToString());
                            obj.ApprovedByManager = Convert.ToBoolean(reader["isApprovedManager"].ToString());
                            obj.DateCreated = Convert.ToDateTime(reader["createdDate"].ToString());
                            obj.DateModified = Convert.ToDateTime(reader["modifiedDate"].ToString());
                            obj.Active = Convert.ToBoolean(reader["active"].ToString()); ;
                            obj.Applications = reader["apps"].ToString();
                            obj.Technologies = reader["tech"].ToString();

                            int a1 = 0;
                            int a2 = 0;

                            Int32.TryParse(reader["noOfPeople"].ToString(), out a1);
                            Int32.TryParse(reader["manualMins"].ToString(), out a2);

                            obj.NumberOfPeople = a1;
                            obj.ManualMinutes = a2;

                            obj.BotAccess = Convert.ToInt32(reader["BotAccess"].ToString());

                            lstBots.Add(obj);

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
                        cmd.CommandText = "r2rGetBot";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id", Id);

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            bot.Id = Convert.ToInt32(reader["id"].ToString());
                            bot.Title = reader["title"].ToString();
                            bot.CreatedBy = new r2rUser() { Id = Convert.ToInt32(reader["FKCreatedBy"].ToString()) };
                            bot.XAML = reader["xaml"].ToString();
                            bot.Functionality = reader["functionality"].ToString();
                            bot.Benefit = reader["benefit"].ToString();
                            bot.Type = Convert.ToInt32(reader["type"].ToString());
                            bot.ApproverAdmin = new r2rUser();
                            bot.ApproverAdmin.Id = Convert.ToInt32(reader["FKApproverAdmin"].ToString());
                            bot.ApproverAdmin.Name = reader["ApproverAdminName"].ToString();
                            bot.ApproverManager = new r2rUser();
                            bot.ApproverManager.Id = Convert.ToInt32(reader["FKApproverManager"].ToString());
                            bot.ApproverManager.Name = reader["ApproverManagerName"].ToString();
                            bot.ApprovedByAdmin = Convert.ToBoolean(reader["isApprovedAdmin"].ToString());
                            bot.ApprovedByManager = Convert.ToBoolean(reader["isApprovedManager"].ToString());
                            bot.DateCreated = Convert.ToDateTime(reader["createdDate"].ToString());
                            bot.DateModified = Convert.ToDateTime(reader["modifiedDate"].ToString());

                            int a1 = 0;
                            int a2 = 0;

                            Int32.TryParse(reader["noOfPeople"].ToString(), out a1);
                            Int32.TryParse(reader["manualMins"].ToString(), out a2);

                            bot.NumberOfPeople = a1;
                            bot.ManualMinutes = a2;

                            bot.Active = Convert.ToBoolean(reader["active"].ToString());
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

        public r2rRole GetRole(int Id)
        {
            r2rRole obj = new r2rRole();

            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT * FROM r2rRoleMst WHERE Id = @Id";
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@Id", Id);

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            obj.Id = Convert.ToInt32(reader["id"].ToString());
                            obj.Title = reader["title"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return obj;
        }

        public r2rTeam GetTeam(int Id)
        {
            r2rTeam obj = new r2rTeam();

            if (Id == 0)
            {
                obj.Id = 0;
                obj.Title = "Blank";
            }
            else
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(ConnectionString))
                    {
                        conn.Open();

                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.CommandText = "r2rGetTeam";
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@id", Id);

                            SqlDataReader reader = cmd.ExecuteReader();

                            while (reader.Read())
                            {
                                obj.Id = Convert.ToInt32(reader["id"].ToString());
                                obj.Title = reader["title"].ToString();
                                obj.Manager = new r2rUser();
                                obj.Manager.Id = Convert.ToInt32(reader["FKManager"].ToString());
                                obj.Manager.Name = reader["ManagerName"].ToString();
                                obj.Region = new r2rRegion();
                                obj.Region.Id = Convert.ToInt32(reader["FKRegion"].ToString());
                                obj.Region.Title = reader["RegionTitle"].ToString();
                                obj.Active = Convert.ToBoolean(reader["active"].ToString());

                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return obj;
        }

        public List<r2rUser> GetUsers()
        {
            List<r2rUser> lstUsers = new List<r2rUser>();

            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "r2rGetUsers";
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            r2rUser obj = new r2rUser();

                            obj.Id = Convert.ToInt32(reader["id"].ToString());
                            obj.Name = reader["name"].ToString();
                            obj.Email = reader["email"].ToString();
                            obj.Role = new r2rRole();
                            obj.Role.Id = Convert.ToInt32(reader["FKRole"].ToString());
                            obj.Role.Title = reader["TitleRole"].ToString();
                            obj.Team = new r2rTeam();
                            obj.Team.Id = Convert.ToInt32(reader["FKTeam"].ToString());
                            obj.Team.Title = reader["TitleTeam"].ToString();
                            obj.Active = Convert.ToBoolean(reader["active"].ToString());
                            obj.DateCreated = Convert.ToDateTime(reader["createdDate"].ToString());
                            obj.DateModified = Convert.ToDateTime(reader["modifiedDate"].ToString());

                            lstUsers.Add(obj);

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return lstUsers;
        }

        public List<r2rUser> GetUsersByRole(int RoleId)
        {
            List<r2rUser> lstUsers = new List<r2rUser>();

            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "r2rGetUsersByRole";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@roleId", RoleId);

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            r2rUser obj = new r2rUser();

                            obj.Id = Convert.ToInt32(reader["id"].ToString());
                            obj.Name = reader["name"].ToString();
                            obj.Email = reader["email"].ToString();
                            obj.Role = new r2rRole();
                            obj.Role.Id = Convert.ToInt32(reader["FKRole"].ToString());
                            obj.Role.Title = reader["TitleRole"].ToString();
                            obj.Team = new r2rTeam();
                            obj.Team.Id = Convert.ToInt32(reader["FKTeam"].ToString());
                            obj.Team.Title = reader["TitleTeam"].ToString();
                            obj.Active = Convert.ToBoolean(reader["active"].ToString());
                            obj.DateCreated = Convert.ToDateTime(reader["createdDate"].ToString());
                            obj.DateModified = Convert.ToDateTime(reader["modifiedDate"].ToString());

                            lstUsers.Add(obj);

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return lstUsers;
        }

        public List<r2rUser> GetUsersByTeam(int TeamId)
        {
            List<r2rUser> lstUsers = new List<r2rUser>();

            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "r2rGetUsersByTeam";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@teamId", TeamId);

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            r2rUser obj = new r2rUser();

                            obj.Id = Convert.ToInt32(reader["id"].ToString());
                            obj.Name = reader["name"].ToString();
                            obj.Email = reader["email"].ToString();
                            obj.Role = new r2rRole();
                            obj.Role.Id = Convert.ToInt32(reader["FKRole"].ToString());
                            obj.Role.Title = reader["TitleRole"].ToString();
                            obj.Team = new r2rTeam();
                            obj.Team.Id = Convert.ToInt32(reader["FKTeam"].ToString());
                            obj.Team.Title = reader["TitleTeam"].ToString();
                            obj.Active = Convert.ToBoolean(reader["active"].ToString());
                            obj.DateCreated = Convert.ToDateTime(reader["createdDate"].ToString());
                            obj.DateModified = Convert.ToDateTime(reader["modifiedDate"].ToString());

                            lstUsers.Add(obj);

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return lstUsers;
        }

        public List<r2rUser> GetUsersByRoleAndTeam(int RoleId, int TeamId)
        {
            List<r2rUser> lstUsers = new List<r2rUser>();

            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "r2rGetUsersByRoleAndTeam";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@roleId", RoleId);
                        cmd.Parameters.AddWithValue("@teamId", TeamId);

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            r2rUser obj = new r2rUser();

                            obj.Id = Convert.ToInt32(reader["id"].ToString());
                            obj.Name = reader["name"].ToString();
                            obj.Email = reader["email"].ToString();
                            obj.Role = new r2rRole();
                            obj.Role.Id = Convert.ToInt32(reader["FKRole"].ToString());
                            obj.Role.Title = reader["TitleRole"].ToString();
                            obj.Team = new r2rTeam();
                            obj.Team.Id = Convert.ToInt32(reader["FKTeam"].ToString());
                            obj.Team.Title = reader["TitleTeam"].ToString();
                            obj.Active = Convert.ToBoolean(reader["active"].ToString());
                            obj.DateCreated = Convert.ToDateTime(reader["createdDate"].ToString());
                            obj.DateModified = Convert.ToDateTime(reader["modifiedDate"].ToString());

                            lstUsers.Add(obj);

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return lstUsers;
        }

        public List<r2rUser> GetUsersByBot(int BotId)
        {
            List<r2rUser> lstUsers = new List<r2rUser>();

            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "r2rGetUsersByBot";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@botId", BotId);

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            r2rUser obj = new r2rUser();

                            obj.Id = Convert.ToInt32(reader["id"].ToString());
                            obj.Name = reader["name"].ToString();
                            obj.Email = reader["email"].ToString();
                            obj.Role = new r2rRole();
                            obj.Role.Id = Convert.ToInt32(reader["FKRole"].ToString());
                            obj.Role.Title = reader["TitleRole"].ToString();
                            obj.Team = new r2rTeam();
                            obj.Team.Id = Convert.ToInt32(reader["FKTeam"].ToString());
                            obj.Team.Title = reader["TitleTeam"].ToString();
                            obj.Active = Convert.ToBoolean(reader["active"].ToString());
                            obj.DateCreated = Convert.ToDateTime(reader["createdDate"].ToString());
                            obj.DateModified = Convert.ToDateTime(reader["modifiedDate"].ToString());

                            obj.BotAccess = Convert.ToInt32(reader["BotAccess"].ToString());

                            lstUsers.Add(obj);

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return lstUsers;
        }


        public r2rUser GetUser(int Id)
        {
            r2rUser obj =  new r2rUser();
            obj.Id = 0;

            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "r2rGetUser";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id", Id);

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            obj.Id = Convert.ToInt32(reader["id"].ToString());
                            obj.Name = reader["name"].ToString();
                            obj.Email = reader["email"].ToString();
                            obj.Role = new r2rRole();
                            obj.Role.Id = Convert.ToInt32(reader["FKRole"].ToString());
                            obj.Role.Title = reader["TitleRole"].ToString();
                            obj.Team = new r2rTeam();
                            obj.Team.Id = Convert.ToInt32(reader["FKTeam"].ToString());
                            obj.Team.Title = reader["TitleTeam"].ToString();
                            obj.Active = Convert.ToBoolean(reader["active"].ToString());
                            obj.DateCreated = Convert.ToDateTime(reader["createdDate"].ToString());
                            obj.DateModified = Convert.ToDateTime(reader["modifiedDate"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return obj;
        }

        public r2rRegion GetRegion(int Id)
        {
            r2rRegion obj = new r2rRegion();

            if (Id == 0)
            {
                obj.Id = 0;
                obj.Title = "Blank";
            }
            else
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(ConnectionString))
                    {
                        conn.Open();

                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.CommandText = "SELECT * FROM r2rRegionMst WHERE id=@id";
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("@id", Id);

                            SqlDataReader reader = cmd.ExecuteReader();

                            while (reader.Read())
                            {
                                obj.Id = Convert.ToInt32(reader["id"].ToString());
                                obj.Title = reader["title"].ToString();
                                obj.Active = Convert.ToBoolean(reader["active"].ToString());
                                obj.DateCreated = Convert.ToDateTime(reader["createdDate"].ToString());
                                obj.DateModified = Convert.ToDateTime(reader["modifiedDate"].ToString());
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return obj;
        }

        public List<r2rTeam> GetTeams()
        {
            List<r2rTeam> lstTeam = new List<r2rTeam>();

            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "r2rGetTeams";
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            r2rTeam obj = new r2rTeam();

                            obj.Id = Convert.ToInt32(reader["id"].ToString());
                            obj.Title = reader["title"].ToString();

                            obj.Manager = new r2rUser();
                            obj.Manager.Id = Convert.ToInt32(reader["FKManager"].ToString());
                            obj.Manager.Name = reader["ManagerName"].ToString();

                            obj.L2 = new r2rUser();
                            obj.L2.Id = Convert.ToInt32(reader["FKL2"].ToString());
                            obj.L2.Name = reader["L2Name"].ToString();

                            obj.L3 = new r2rUser();
                            obj.L3.Id = Convert.ToInt32(reader["FKL3"].ToString());
                            obj.L3.Name = reader["L3Name"].ToString();

                            obj.L4 = new r2rUser();
                            obj.L4.Id = Convert.ToInt32(reader["FKL4"].ToString());
                            obj.L4.Name = reader["L4Name"].ToString();

                            obj.Region = new r2rRegion();
                            obj.Region.Id = Convert.ToInt32(reader["FKRegion"].ToString());
                            obj.Region.Title = reader["RegionTitle"].ToString();
                            obj.DateCreated = Convert.ToDateTime(reader["createdDate"].ToString());
                            obj.DateModified = Convert.ToDateTime(reader["modifiedDate"].ToString());
                            obj.Active = Convert.ToBoolean(reader["active"].ToString());

                            lstTeam.Add(obj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return lstTeam;
        }

        public List<r2rTeam> GetTeams(int Region)
        {
            List<r2rTeam> lstTeam = new List<r2rTeam>();

            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "r2rGetTeamsByRegion";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@regionId", Region);

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            r2rTeam obj = new r2rTeam();

                            obj.Id = Convert.ToInt32(reader["id"].ToString());
                            obj.Title = reader["title"].ToString();
                            obj.Manager = new r2rUser();
                            obj.Manager.Id = Convert.ToInt32(reader["FKManager"].ToString());
                            obj.Manager.Name = reader["ManagerName"].ToString();
                            obj.Region = new r2rRegion();
                            obj.Region.Id = Convert.ToInt32(reader["FKRegion"].ToString());
                            obj.Region.Title = reader["RegionTitle"].ToString();
                            obj.DateCreated = Convert.ToDateTime(reader["createdDate"].ToString());
                            obj.DateModified = Convert.ToDateTime(reader["modifiedDate"].ToString());
                            obj.Active = Convert.ToBoolean(reader["active"].ToString());

                            lstTeam.Add(obj);

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return lstTeam;
        }

        public List<r2rRole> GetRoles()
        {
            List<r2rRole> lstRole = new List<r2rRole>();

            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT * FROM r2rRoleMst";
                        cmd.CommandType = CommandType.Text;

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            r2rRole obj = new r2rRole();

                            obj.Id = Convert.ToInt32(reader["id"].ToString());
                            obj.Title = reader["title"].ToString();
                            obj.Active = Convert.ToBoolean(reader["active"].ToString());
                            obj.DateCreated = Convert.ToDateTime(reader["createdDate"].ToString());
                            obj.DateModified = Convert.ToDateTime(reader["modifiedDate"].ToString());

                            lstRole.Add(obj);

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return lstRole;
        }

        public List<r2rRegion> GetRegions()
        {
            List<r2rRegion> lstRegion = new List<r2rRegion>();

            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT * FROM r2rRegionMst";
                        cmd.CommandType = CommandType.Text;

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            r2rRegion obj = new r2rRegion();

                            obj.Id = Convert.ToInt32(reader["id"].ToString());
                            obj.Title = reader["title"].ToString();
                            obj.Active = Convert.ToBoolean(reader["active"].ToString());
                            obj.DateCreated = Convert.ToDateTime(reader["createdDate"].ToString());
                            obj.DateModified = Convert.ToDateTime(reader["modifiedDate"].ToString());

                            lstRegion.Add(obj);

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return lstRegion;
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
                        cmd.CommandText = "INSERT INTO r2rUserMst (name, email, active, FKRole, FKTeam, createdDate, modifiedDate) VALUES (@email, @email, 0, 0, 0, GETDATE(), GETDATE())";
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


        public int AddTask(r2rBotSchedule task,int occur)
        {
            int res = 0;

            try
            {

                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "INSERT INTO [r2rBotScheduleDetl] (	FKBot,	occurance,	startDate,	endDate,	active,	RepeatTask,	starttime,	endtime,	ForDurationmin,	ForDurationhour,";
                        cmd.CommandText += "weeklydays,monthdays,monthoccur,weeklyoccur,dailyoccur,Taskname) VALUES ( @FKBot, @occurance, @startDate, @endDate, ";
                        cmd.CommandText += "@active, @RepeatTask, @starttime, @endtime, @ForDurationmin, @ForDurationhour,@weeklydays,@monthdays,@monthoccur,@weeklyoccur,@dailyoccur,@name)";

                        cmd.CommandType = CommandType.Text;
                        if (occur==1)
                        {
                            
                            cmd.Parameters.AddWithValue("@FKBot", task.Id);
                            cmd.Parameters.AddWithValue("@occurance", occur);
                            cmd.Parameters.AddWithValue("@startDate", task.DateStart);
                            cmd.Parameters.AddWithValue("@endDate", "");
                            cmd.Parameters.AddWithValue("@active", "Y");
                            cmd.Parameters.AddWithValue("@endtime", "");
                            cmd.Parameters.AddWithValue("@RepeatTask", task.RepeatTask);
                            cmd.Parameters.AddWithValue("@starttime", task.startime);
                            cmd.Parameters.AddWithValue("@ForDurationmin", task.ForDurationmin);
                            cmd.Parameters.AddWithValue("@ForDurationhour", "");
                            cmd.Parameters.AddWithValue("@weeklydays", "");
                            cmd.Parameters.AddWithValue("@monthdays", "");
                            cmd.Parameters.AddWithValue("@monthoccur", "");
                            cmd.Parameters.AddWithValue("@weeklyoccur", "");
                            cmd.Parameters.AddWithValue("@dailyoccur", "");
                            cmd.Parameters.AddWithValue("@name", task.name);
                        }
                        if (occur == 2)
                        {

                            cmd.Parameters.AddWithValue("@FKBot", task.Id);
                            cmd.Parameters.AddWithValue("@occurance", occur);
                            cmd.Parameters.AddWithValue("@startDate", task.DateStart);
                            cmd.Parameters.AddWithValue("@endDate", task.DateEnd);
                            cmd.Parameters.AddWithValue("@active", "Y");
                            cmd.Parameters.AddWithValue("@endtime", "");
                            cmd.Parameters.AddWithValue("@RepeatTask", task.RepeatTask);
                            cmd.Parameters.AddWithValue("@starttime", task.startime);
                            cmd.Parameters.AddWithValue("@ForDurationmin", task.ForDurationmin);
                            cmd.Parameters.AddWithValue("@ForDurationhour", "");
                            cmd.Parameters.AddWithValue("@weeklydays", "");
                            cmd.Parameters.AddWithValue("@monthdays", "");
                            cmd.Parameters.AddWithValue("@monthoccur", "");
                            cmd.Parameters.AddWithValue("@weeklyoccur", "");
                            cmd.Parameters.AddWithValue("@dailyoccur", task.dailyoccur);
                            cmd.Parameters.AddWithValue("@name", task.name);
                        }
                        if (occur == 3)
                        {

                            cmd.Parameters.AddWithValue("@FKBot", task.Id);
                            cmd.Parameters.AddWithValue("@occurance", occur);
                            cmd.Parameters.AddWithValue("@startDate", task.DateStart);
                            cmd.Parameters.AddWithValue("@endDate", task.DateEnd);
                            cmd.Parameters.AddWithValue("@active", "Y");
                            cmd.Parameters.AddWithValue("@endtime", "");
                            cmd.Parameters.AddWithValue("@RepeatTask", task.RepeatTask);
                            cmd.Parameters.AddWithValue("@starttime", task.startime);
                            cmd.Parameters.AddWithValue("@ForDurationmin", task.ForDurationmin);
                            cmd.Parameters.AddWithValue("@ForDurationhour", "");
                            cmd.Parameters.AddWithValue("@weeklydays", task.weeklydays);
                            cmd.Parameters.AddWithValue("@monthdays", "");
                            cmd.Parameters.AddWithValue("@monthoccur", "");
                            cmd.Parameters.AddWithValue("@weeklyoccur", task.weeklyoccr);
                            cmd.Parameters.AddWithValue("@dailyoccur", "");
                            cmd.Parameters.AddWithValue("@name", task.name);
                        }

                        if (occur == 4)
                        {

                            cmd.Parameters.AddWithValue("@FKBot", task.Id);
                            cmd.Parameters.AddWithValue("@occurance", occur);
                            cmd.Parameters.AddWithValue("@startDate", task.DateStart);
                            cmd.Parameters.AddWithValue("@endDate", task.DateEnd);
                            cmd.Parameters.AddWithValue("@active", "Y");
                            cmd.Parameters.AddWithValue("@endtime", "");
                            cmd.Parameters.AddWithValue("@RepeatTask", task.RepeatTask);
                            cmd.Parameters.AddWithValue("@starttime", task.startime);
                            cmd.Parameters.AddWithValue("@ForDurationmin", task.ForDurationmin);
                            cmd.Parameters.AddWithValue("@ForDurationhour", "");
                            cmd.Parameters.AddWithValue("@weeklydays", "");
                            cmd.Parameters.AddWithValue("@monthdays", task.monthdays);
                            cmd.Parameters.AddWithValue("@monthoccur", task.monthoccur);
                            cmd.Parameters.AddWithValue("@weeklyoccur", "");
                            cmd.Parameters.AddWithValue("@dailyoccur", "");
                            cmd.Parameters.AddWithValue("@name", task.name);
                        }

                        using (SqlCommand cmd2 = conn.CreateCommand())
                        {
                            cmd2.CommandText = "delete [r2rBotScheduleDetl] where fkbot='" + task.Id + "' and TaskName='" + task.name +"' ";
                            cmd2.CommandType = CommandType.Text;

                            cmd2.ExecuteNonQuery();
                          
                        }


                        cmd.ExecuteNonQuery();

                        //using (SqlCommand cmd2 = conn.CreateCommand())
                        //{
                        //    cmd2.CommandText = "select ident_current('r2rBotHdr') AS 'Id'";
                        //    cmd2.CommandType = CommandType.Text;

                        //    SqlDataReader reader = cmd2.ExecuteReader();

                        //    while (reader.Read())
                        //    {
                        //        res = Convert.ToInt32(reader["id"].ToString());
                        //        break;
                        //    }
                        //}
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

        public DataTable GetTask(int id)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                using (SqlCommand cmd2 = conn.CreateCommand())
                {
                    cmd2.CommandText = "select * from [r2rBotScheduleDetl] where fkbot='" + id +"'";
                    cmd2.CommandType = CommandType.Text;

                    SqlDataAdapter da = new SqlDataAdapter(cmd2);
                    // this will query your database and return the result to your datatable
                    da.Fill(dt);
                    return dt;
                }
            }
        }

        public void updTask(int id,string active, string name)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                using (SqlCommand cmd2 = conn.CreateCommand())
                {
                    cmd2.CommandText = "update [r2rBotScheduleDetl] set active='" + active +"' where fkbot='" + id + "' and TaskName='" + name + "' ";
                    cmd2.CommandType = CommandType.Text;
                    cmd2.ExecuteNonQuery();
                }
            }
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

        //filled only when bot is supplied
        public int BotAccess { get; set; }

    }
    

    public class r2rRole
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public bool Active { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        //METHODS
        public List<r2rUser> GetRoleUsers()
        {
            List<r2rUser> lstUser = new List<r2rUser>();

            return lstUser;
        }
    }

    public class r2rRegion
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public bool Active { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        //METHODS
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

        public r2rUser L4 { get; set; }

        public r2rUser L3 { get; set; }

        public r2rUser L2 { get; set; }

        public r2rRegion Region { get; set; }

        public bool Active { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public List<r2rUser> GetTeamUsers()
        {
            List<r2rUser> lstUser = new List<r2rUser>();

            return lstUser;
        }
    }

    public  class r2rBot
    {
        public  int Id { get; set; }

        public string Title { get; set; }

        public r2rUser CreatedBy { get; set; }

        public string XAML { get; set; }

        public string Functionality { get; set; }

        public string Benefit { get; set; }

        public r2rTeam Team { get; set; }

        public int Type { get; set; }

        public int NumberOfPeople { get; set; }

        public int ManualMinutes { get; set; }

        public r2rUser ApproverAdmin { get; set; }

        public r2rUser ApproverManager { get; set; }

        public bool ApprovedByAdmin { get; set; }

        public bool ApprovedByManager { get; set; }

        public string Technologies { get; set; }

        public string Applications { get; set; }

        public bool Active { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        //used only in shared bots
        public int BotAccess { get; set; }
        public bool isProduction { get; set; }
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

        public r2rUser User { get; set; }

        public DateTime DateRun { get; set; }

        public DateTime TimeStart { get; set; }

        public DateTime TimeEnd { get; set; }

        public int TransactionCount { get; set; }
         
        public int Status { get; set; }

        public string Log { get; set; }

        public bool TestRun { get; set; }

    }

    public class r2rBotPassword
    {
        public int Id { get; set; }

        public r2rBot Bot { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public string Encrypt()
        {
            string res = "";

            res = EncryptString(Password, "ShameemAhmed");

            return res;
        }

        public string Decrypt()
        {
            string res = "";

            res = DecryptString(Password, "ShameemAhmed");

            return res;
        }

        private const string initVector = "pemgail9uzpgzl88";
        private const int keysize = 256;

        public static string EncryptString(string plainText, string passPhrase)
        {
            byte[] initVectorBytes = Encoding.UTF8.GetBytes(initVector);
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, null);
            byte[] keyBytes = password.GetBytes(keysize / 8);
            RijndaelManaged symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;
            ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
            cryptoStream.FlushFinalBlock();
            byte[] cipherTextBytes = memoryStream.ToArray();
            memoryStream.Close();
            cryptoStream.Close();
            return Convert.ToBase64String(cipherTextBytes);
        }

        public static string DecryptString(string cipherText, string passPhrase)
        {
            byte[] initVectorBytes = Encoding.UTF8.GetBytes(initVector);
            byte[] cipherTextBytes = Convert.FromBase64String(cipherText);
            PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, null);
            byte[] keyBytes = password.GetBytes(keysize / 8);
            RijndaelManaged symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;
            ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
            MemoryStream memoryStream = new MemoryStream(cipherTextBytes);
            CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            byte[] plainTextBytes = new byte[cipherTextBytes.Length];
            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            memoryStream.Close();
            cryptoStream.Close();
            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
        }
    }

    public class r2rBotSchedule
    {
        public int Id { get; set; }

        public string name { get; set; }

        public r2rBot Bot { get; set; }

        public int Occurance { get; set; }

        public string DateStart { get; set; }

        public string startime { get; set; }

        public string endtime { get; set; }

        public string DateEnd { get; set; }

        public string RepeatTask { get; set; }

        public string ForDurationmin { get; set; }

        public string ForDurationhour { get; set; }

        public string weeklyoccr { get; set; }

        public string weeklydays { get; set; }

        public string monthdays { get; set; }

        public string monthoccur { get; set; }

        public string dailyoccur { get; set; }

        public int expyear { get; set; }

        public int expdate { get; set; }

        public int expmont { get; set; }

      

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


    public class r2rTheme
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Color1 { get; set; }

        public string Color2 { get; set; }

        public string Color3 { get; set; }

        public string Color4 { get; set; }

        public bool Active { get; set; }
    }

}
