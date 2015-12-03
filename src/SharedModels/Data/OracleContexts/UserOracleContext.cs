using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.DataAccess.Client;
using SharedModels.Data.ContextInterfaces;
using SharedModels.Enums;
using SharedModels.Models;

namespace SharedModels.Data.OracleContexts
{
    public class UserOracleContext : EntityOracleContext<User>, IUserContext
    {
        public List<User> GetAll()
        {
            var query = "SELECT * FROM useraccount ORDER BY userid";
            var res = Database.ExecuteReader(query);

            return res.Select(GetEntityFromRecord).ToList();
        }

        public User GetById(object id)
        {
            var query = "SELECT * FROM useraccount WHERE userid = :userid";
            var parameters = new List<OracleParameter>
            {
                new OracleParameter("userid", Convert.ToInt32(id))
            };

            return GetEntityFromRecord(Database.ExecuteReader(query, parameters).First());
        }

        public User Insert(User user)
        {
            var query =
                "INSERT INTO useraccount (userid, username, password, firstname, surname, country, address, city, postal, phonenumber, permissionlevel) VALUES (seq_user.nextval, :username, :password, :firstname, :surname, :country, :address, :city, :postal, :phonenumber, :permissionlevel) RETURNING userid INTO :lastID";
            var parameters = new List<OracleParameter>
            {
                new OracleParameter("username", user.Username),
                new OracleParameter("password", user.Password),
                new OracleParameter("firstname", user.Name),
                new OracleParameter("surname", user.Surname),
                new OracleParameter("country", user.Country.ToString()),
                new OracleParameter("address", user.Address),
                new OracleParameter("city", user.City),
                new OracleParameter("postal", user.Postal),
                new OracleParameter("phonenumber", user.Telephone),
                new OracleParameter("permissionlevel", Convert.ToInt32(user.Permission)),
                new OracleParameter("lastID", OracleDbType.Decimal) {Direction = ParameterDirection.ReturnValue}
            };

            string newID;
            if (!Database.ExecuteNonQuery(query, out newID, parameters)) return null;
            return GetById(Convert.ToInt32(newID));
        }

        public bool Update(User user)
        {
            const string query = "UPDATE useraccount SET password = :pass, firstname = :firstname, surname = :surname, country = :country, address = :address, city = :city, postal = :postal, phonenumber = :phonenumber, permissionlevel = :permissionlevel WHERE userid = :userid";

            var parameters = new List<OracleParameter>
            {
                new OracleParameter("userid", user.ID),
                new OracleParameter("pass", user.Password),
                new OracleParameter("firstname", user.Name),
                new OracleParameter("surname", user.Surname),
                new OracleParameter("country", user.Country.ToString()),
                new OracleParameter("address", user.Address),
                new OracleParameter("city", user.City),
                new OracleParameter("postal", user.Postal),
                new OracleParameter("phonenumber", user.Telephone),
                new OracleParameter("permissionlevel", Convert.ToInt32(user.Permission))
            };

            return Database.ExecuteNonQuery(query, parameters);
        }

        public bool Delete(User user)
        {
            var query = "DELETE FROM useraccount WHERE userid = :userid";
            var parameters = new List<OracleParameter> { new OracleParameter("userid", user.ID) };

            return Database.ExecuteNonQuery(query, parameters);
        }

        public User AuthenticateUser(string username, string password)
        {
            var query = "SELECT * FROM useraccount WHERE username = :username AND password = :password AND ROWNUM <= 1";
            var parameters = new List<OracleParameter>
            {
                new OracleParameter("username", username),
                new OracleParameter("password", password),
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
            return new User(Convert.ToInt32(record[0]), record[1], record[2], record[3], record[4], (Country) Enum.Parse(typeof(Country), record[5]),
                record[7], record[8], record[6], record[9],
                DateTime.Parse(record[10]), (PermissionType) Convert.ToInt32(record[11]));
        }
    }
}
