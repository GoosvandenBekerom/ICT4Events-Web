using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Oracle.DataAccess.Client;
using SharedModels.Data.ContextInterfaces;
using SharedModels.Models;

namespace SharedModels.Data.OracleContexts
{
    public class ReservationOracleContext : EntityOracleContext<Reservation>, IReservationContext
    {
        public List<Reservation> GetAll()
        {
            var query = "P_RESERVERING.getAll";

            var parameters = new List<OracleParameter>
            {
                new OracleParameter("Return_Value", OracleDbType.RefCursor, ParameterDirection.ReturnValue)
            };

            var res = Database.ExecuteReader(query, parameters);
            return res.Select(GetEntityFromRecord).ToList();
        }

        public Reservation GetLastAdded()
        {
            var query = "P_RESERVERING.lastAdded";

            var parameters = new List<OracleParameter>
            {
                new OracleParameter("Return_Value", OracleDbType.RefCursor, ParameterDirection.ReturnValue)
            };

            return GetEntityFromRecord(Database.ExecuteReader(query, parameters).First());
        }

        public Reservation GetById(int id)
        {
            var query = "P_RESERVERING.getById";

            var parameters = new List<OracleParameter>
                {
                    new OracleParameter("Return_Value", OracleDbType.RefCursor, ParameterDirection.ReturnValue),
                    new OracleParameter("reservatieId", id)
                };

            return GetEntityFromRecord(Database.ExecuteReader(query, parameters).First());
        }

        public bool Insert(Reservation reservation)
        {
            var query =
                "P_RESERVERING.insertReservering";

            var parameters = new List<OracleParameter>
            {
                 new OracleParameter("Return_Value", OracleDbType.Int32, ParameterDirection.ReturnValue),
                new OracleParameter("r_persoon_id", reservation.PersonId),
                new OracleParameter("r_datumStart", reservation.DateStart),
                new OracleParameter("r_datumEinde", reservation.DateEnd),
                new OracleParameter("r_betaald", Convert.ToInt32(reservation.Paid))
            };

            return Database.ExecuteNonQuery(query, parameters);
        }

        public bool Update(Reservation user)
        {
            var query = "p_account.updateAccount";

            var parameters = new List<OracleParameter>
                {
                    new OracleParameter("Return_Value", OracleDbType.Int32, ParameterDirection.ReturnValue),
                    //new OracleParameter("p_accId", user.ID),
                    //new OracleParameter("p_gebruikersnaam", user.Username),
                    //new OracleParameter("p_email", user.Email),
                    //new OracleParameter("p_activatiehash", user.ActivationHash),
                    //new OracleParameter("p_geactiveerd", Convert.ToInt32(user.Activated))
                };

            return Database.ExecuteNonQuery(query, parameters);
        }

        public bool Delete(Reservation user)
        {
            var query = "p_account.deleteAccount";

            var parameters = new List<OracleParameter>
                {
                    new OracleParameter("Return_Value", OracleDbType.Int32, ParameterDirection.ReturnValue),
                    new OracleParameter("p_accId", user.ID)
                };
            return Database.ExecuteNonQuery(query, parameters);
        }

        protected override Reservation GetEntityFromRecord(List<string> record)
        {
            return new Reservation(Convert.ToInt32(record[0]), Convert.ToInt32(record[1]), DateTime.Parse(record[2]), DateTime.Parse(record[3]), Convert.ToBoolean(Convert.ToInt32(record[4])));
        }
    }
}
