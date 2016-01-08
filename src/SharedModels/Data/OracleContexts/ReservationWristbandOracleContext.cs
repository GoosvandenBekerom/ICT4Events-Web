using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Oracle.DataAccess.Client;
using SharedModels.Data.ContextInterfaces;
using SharedModels.Models;

namespace SharedModels.Data.OracleContexts
{
    public class ReservationWristbandOracleContext : EntityOracleContext<ReservationWristband>, IReservationWristband
    {
        public List<ReservationWristband> GetAll()
        {
            var query = "P_RESERVERING_POLSBANDJE.getAll";

            var parameters = new List<OracleParameter>
            {
                new OracleParameter("Return_Value", OracleDbType.RefCursor, ParameterDirection.ReturnValue)
            };

            var res = Database.ExecuteReader(query, parameters);
            return res.Select(GetEntityFromRecord).ToList();
        }

        public List<ReservationWristband> GetReservationByUserIdAll(int id)
        {
            var query = "P_RESERVERING_POLSBANDJE.getReservationByAccountID";

            var parameters = new List<OracleParameter>
            {
                new OracleParameter("Return_Value", OracleDbType.RefCursor, ParameterDirection.ReturnValue),
                new OracleParameter("accountId", id)
            };

            var res = Database.ExecuteReader(query, parameters);
            return res.Select(GetEntityFromRecord).ToList();
        }

        public ReservationWristband GetLastAdded()
        {
            var query = "P_RESERVERING_POLSBANDJE.lastAdded";

            var parameters = new List<OracleParameter>
            {
                new OracleParameter("Return_Value", OracleDbType.RefCursor, ParameterDirection.ReturnValue)
            };

            return GetEntityFromRecord(Database.ExecuteReader(query, parameters).First());
        }

        public ReservationWristband GetById(int id)
        {
            var query = "P_RESERVERING_POLSBANDJE.getById";

            var parameters = new List<OracleParameter>
                {
                    new OracleParameter("Return_Value", OracleDbType.RefCursor, ParameterDirection.ReturnValue),
                    new OracleParameter("resId", id)
                };

            return GetEntityFromRecord(Database.ExecuteReader(query, parameters).First());
        }

        public bool Insert(ReservationWristband entity)
        {
            var query =
                "P_RESERVERING_POLSBANDJE.insertResPols";

            var parameters = new List<OracleParameter>
            {
                new OracleParameter("Return_Value", OracleDbType.Int32, ParameterDirection.ReturnValue),
                new OracleParameter("p_resId", entity.ReservationId),
                new OracleParameter("p_accountId", entity.UserId),
            };
            return Database.ExecuteNonQuery(query, parameters);
        }

        public bool Update(ReservationWristband entity)
        {
            var query = "p_person.updatePerson";

            var parameters = new List<OracleParameter>
                {
                    //new OracleParameter("Return_Value", OracleDbType.Int32, ParameterDirection.ReturnValue),
                    //new OracleParameter("p_perId", entity.ID),
                    //new OracleParameter("p_name", entity.Name),
                    //new OracleParameter("p_surname", entity.Surname),
                    //new OracleParameter("p_address", entity.Street),
                    //new OracleParameter("p_housenr", entity.HouseNr),
                    //new OracleParameter("p_city", entity.City),
                    //new OracleParameter("p_banknr", entity.IBAN)
                };

            return Database.ExecuteNonQuery(query, parameters);
        }
                          
        public bool Delete(ReservationWristband entity)
        {
            var query = "p_person.deletePerson";

            var parameters = new List<OracleParameter>
                {
                    //new OracleParameter("Return_Value", OracleDbType.Int32, ParameterDirection.ReturnValue),
                    //new OracleParameter("p_accId", entity.ID)
                };
            return Database.ExecuteNonQuery(query, parameters);
        }

        protected override ReservationWristband GetEntityFromRecord(List<string> record)
        {
            // 1	Jan		Pietersen	Rachelsmolen	1	5611MA	NL91ABNA0417164300
            return new ReservationWristband(Convert.ToInt32(record[0]), Convert.ToInt32(record[1]), Convert.ToInt32(record[3]));
        }
    }
}
