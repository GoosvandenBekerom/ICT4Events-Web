using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Oracle.DataAccess.Client;
using SharedModels.Data.ContextInterfaces;
using SharedModels.Models;

namespace SharedModels.Data.OracleContexts
{
    public class UserOracleContext : EntityOracleContext<User>, IUserContext
    {
        public List<User> GetAll()
        {
            var query = "p_account.getAll";

            var parameters = new List<OracleParameter>
            {
                new OracleParameter("Return_Value", OracleDbType.RefCursor, ParameterDirection.ReturnValue)
            };

            var res = Database.ExecuteReader(query, parameters);
            return res.Select(GetEntityFromRecord).ToList();
        }

        public User GetLastAdded()
        {
            var query = "p_account.lastAdded";

            var parameters = new List<OracleParameter>
            {
                new OracleParameter("Return_Value", OracleDbType.RefCursor, ParameterDirection.ReturnValue)
            };

            return GetEntityFromRecord(Database.ExecuteReader(query, parameters).First());
        }

        public User GetById(int id)
        {
            var query = "p_account.getById";

            var parameters = new List<OracleParameter>
                {
                    new OracleParameter("Return_Value", OracleDbType.RefCursor, ParameterDirection.ReturnValue),
                    new OracleParameter("accountId", id)
                };

            return GetEntityFromRecord(Database.ExecuteReader(query, parameters).First());
        }

        public bool Insert(User user)
        {
            var query =
                "p_account.insertAccount";

            var parameters = new List<OracleParameter>
                {
                    new OracleParameter("Return_Value", OracleDbType.Int32, ParameterDirection.ReturnValue),
                    new OracleParameter("p_gebruikersnaam", user.Username),
                    new OracleParameter("p_email", user.Email),
                    new OracleParameter("p_pass", user.Password),
                    new OracleParameter("p_activatiehash", user.ActivationHash),
                    new OracleParameter("p_geactiveerd", Convert.ToInt32(user.Activated)) };

            return Database.ExecuteNonQuery(query, parameters);
        }

        public bool Update(User user)
        {
            var query = "p_account.updateAccount";

            var parameters = new List<OracleParameter>
                {
                    new OracleParameter("Return_Value", OracleDbType.Int32, ParameterDirection.ReturnValue),
                    new OracleParameter("p_accId", user.ID),
                    new OracleParameter("p_gebruikersnaam", user.Username),
                    new OracleParameter("p_email", user.Email),
                    new OracleParameter("p_activatiehash", user.ActivationHash),
                    new OracleParameter("p_geactiveerd", Convert.ToInt32(user.Activated))
                };

            return Database.ExecuteNonQuery(query, parameters);
        }

        public bool Delete(User user)
        {
            var query = "p_account.deleteAccount";

            var parameters = new List<OracleParameter>
                {
                    new OracleParameter("Return_Value", OracleDbType.Int32, ParameterDirection.ReturnValue),
                    new OracleParameter("p_accId", user.ID)
                };
            return Database.ExecuteNonQuery(query, parameters);
        }

        public User AuthenticateUser(string username, string password)
        {
            var query = "p_account.authenticateUser";

            var parameters = new List<OracleParameter>
                {
                    new OracleParameter("Return_Value", OracleDbType.RefCursor, ParameterDirection.ReturnValue),
                    new OracleParameter("p_email", username),
                    new OracleParameter("p_pass", password)
                };

            return GetEntityFromRecord(Database.ExecuteReader(query, parameters).FirstOrDefault());
        }

        public User GetByUsername(string username)
        {
            var query = "SELECT * FROM useraccount WHERE username = :username";
            var parameters = new List<OracleParameter>
            {
                new OracleParameter("username", username)
            };

            return GetEntityFromRecord(Database.ExecuteReader(query, parameters).FirstOrDefault());
        }

        protected override User GetEntityFromRecord(List<string> record)
        {
            if (record == null) return null;

            // Date format: 19-10-2015 01:57:21
            return new User(Convert.ToInt32(record[0]), record[1], record[2], record[4], Convert.ToBoolean(Convert.ToInt32(record[5])), record[3], Convert.ToBoolean(Convert.ToInt32(record[6])));
            /*return new User(Convert.ToInt32(record[0]), record[1], record[2], record[3], record[4], (Country) Enum.Parse(typeof(Country), record[5]),
                record[7], record[8], record[6], record[9],
                DateTime.Parse(record[10]), (PermissionType) Convert.ToInt32(record[11]));*/
        }
    }
}
