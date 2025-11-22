using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary
{
    public class SqlCrud
    {
        private readonly string _connectionString;
        private SqlDataAccess db = new SqlDataAccess();
        public SqlCrud(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<BasicPersonModel> GetAllContacts()
        {
            string sql = "select Id, FirstName, Lastname from dbo.Person";

            return db.LoadData<BasicPersonModel, dynamic>(sql, new { }, _connectionString);
        }

        public void CreatePerson(FullPersonModel person)
        {
            string sql = "insert into dbo.Person (Firstname, LastName) values (@FirstName, @LastName);";
            db.SaveData(sql, new { person.BasicPerson.FirstName, person.BasicPerson.LastName }, _connectionString);
        }

        public FullPersonModel ReadPersonById(int id)
        {
            FullPersonModel person = new FullPersonModel();

            //string sql = "select FirstName from dbo.Person where Id = @Id";
            string sql = "select Id, FirstName, LastName from dbo.Person where Id = @Id;";

            person.BasicPerson = db.LoadData<BasicPersonModel, dynamic>(sql, new { Id = id }, _connectionString).FirstOrDefault();

            return person;
        }

        public void UpdatePersonName(BasicPersonModel person)
        {
            string sql = "update dbo.Person set FirstName = @FirstName, LastName = @LastName where Id = @Id;";
            db.SaveData(sql, person, _connectionString);
        }

        public void DeletePerson(int id)
        {
            string sql = "delete from dbo.Person where Id = @Id;";
            db.SaveData(sql, new { Id = id }, _connectionString);
        }
    }
}
