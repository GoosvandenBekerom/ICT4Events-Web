using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Oracle.DataAccess.Client;
using SharedModels.Data.ContextInterfaces;
using SharedModels.Models;

namespace SharedModels.Data.OracleContexts
{
    public class PersonOracleContext : EntityOracleContext<Person>, IPersonContext
    {
        public List<Person> GetAll()
        {
            var query = "p_person.getAll";

            var parameters = new List<OracleParameter>
            {
                new OracleParameter("Return_Value", OracleDbType.RefCursor, ParameterDirection.ReturnValue)
            };

            var res = Database.ExecuteReader(query, parameters);
            return res.Select(GetEntityFromRecord).ToList();
        }

        public Person GetLastAdded()
        {
            var query = "p_person.lastAdded";

            var parameters = new List<OracleParameter>
            {
                new OracleParameter("Return_Value", OracleDbType.RefCursor, ParameterDirection.ReturnValue)
            };

            return GetEntityFromRecord(Database.ExecuteReader(query, parameters).First());
        }

        public Person GetById(int id)
        {
            var query = "p_person.getById";

            var parameters = new List<OracleParameter>
                {
                    new OracleParameter("Return_Value", OracleDbType.RefCursor, ParameterDirection.ReturnValue),
                    new OracleParameter("personId", id)
                };

            return GetEntityFromRecord(Database.ExecuteReader(query, parameters).First());
        }

        public bool Insert(Person entity)
        {
            var query =
                "p_person.insertPerson";

            var parameters = new List<OracleParameter>
            {
                new OracleParameter("Return_Value", OracleDbType.Int32, ParameterDirection.ReturnValue),
                new OracleParameter("p_name", entity.Name),
                new OracleParameter("p_surname", entity.Surname),
                new OracleParameter("p_address", entity.Street),
                new OracleParameter("p_housenr", entity.HouseNr),
                new OracleParameter("p_city", entity.City),
                new OracleParameter("p_banknr", entity.IBAN)
            };
            return Database.ExecuteNonQuery(query, parameters);
        }

        public bool Update(Person entity)
        {
            var query = "p_person.updatePerson";

            var parameters = new List<OracleParameter>
                {
                    new OracleParameter("Return_Value", OracleDbType.Int32, ParameterDirection.ReturnValue),
                    new OracleParameter("p_perId", entity.ID),
                    new OracleParameter("p_name", entity.Name),
                    new OracleParameter("p_surname", entity.Surname),
                    new OracleParameter("p_address", entity.Street),
                    new OracleParameter("p_housenr", entity.HouseNr),
                    new OracleParameter("p_city", entity.City),
                    new OracleParameter("p_banknr", entity.IBAN)
                };

            return Database.ExecuteNonQuery(query, parameters);
        }

        public bool Delete(Person entity)
        {
            var query = "p_person.deletePerson";

            var parameters = new List<OracleParameter>
                {
                    new OracleParameter("Return_Value", OracleDbType.Int32, ParameterDirection.ReturnValue),
                    new OracleParameter("p_accId", entity.ID)
                };
            return Database.ExecuteNonQuery(query, parameters);
        }

        protected override Person GetEntityFromRecord(List<string> record)
        {
            // 1	Jan		Pietersen	Rachelsmolen	1	5611MA	NL91ABNA0417164300
            return new Person(Convert.ToInt32(record[0]), $"{record[1]} {record[3]}", "temp?", $"{record[4]} {record[5]}", record[6], record[7]);
        }
    }
}
