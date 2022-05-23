using SQLite;
using System.Collections.Generic;
using System.IO;

namespace XA1_Review1
{
    class SQLiteOperations
    {
        //database path
        private readonly string dbPath = Path.Combine(
                System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "PersonDB1.db3");
        //Constructor     
        public SQLiteOperations()
        {
            //Creating database, if it doesn't already exist 
            if (!File.Exists(dbPath))
            {
                var db = new SQLiteConnection(dbPath);
                db.CreateTable<Person>();
                db.CreateTable<Address>();
            }
        }
        //  Insert users intoDB (array of users)
        //  ادخال مستخدم جديد
        public void InsertPerson(Person person)
        {
            var db = new SQLiteConnection(dbPath);
            db.Insert(person);
        }
        public void InsertAddress(Address address)
        {
            var db = new SQLiteConnection(dbPath);
            db.Insert(address);
        }
        // User Upbdate
        public void UpdatePerson(Person person)
        {
            var db = new SQLiteConnection(dbPath);
            db.Update(person);
        }
        public void UpdateAddress(Address address)
        {
            var db = new SQLiteConnection(dbPath);
            db.Update(address);
        }
        // User Delete
        public void DeletePerson(Person person)
        {
            var db = new SQLiteConnection(dbPath);
            db.Delete(person);
        }
        public void DeleteAddress(Address address)
        {
            var db = new SQLiteConnection(dbPath);
            db.Delete(address);
        }

        // Object ارجاع بيانات مستخدم واحد على شكل   
        public Person GetPerson(string user, string code)
        {
            var db = new SQLiteConnection(dbPath);
            return db.Table<Person>().Where(i => i.User == user && i.Code == code).FirstOrDefault();
        }
        public Address GetAddress(string city, string pcode)
        {
            var db = new SQLiteConnection(dbPath);
            return db.Table<Address>().Where(i => i.City == city && i.PCode == pcode).FirstOrDefault();
        }
        // Return User Record by using UId
        public Person GetPersonById(int id)
        {
            var db = new SQLiteConnection(dbPath);
            try
            {
                return db.Table<Person>().Where(i => i.Id == id).FirstOrDefault();
            }
            catch
            {
                return null;
            }
        }
        public Address GetAddressById(int aid)
        {
            var db = new SQLiteConnection(dbPath);
            try
            {
                return db.Table<Address>().Where(i => i.AId == aid).FirstOrDefault();
            }
            catch
            {
                return null;
            }
        }
        public Address GetAddressByPersonId(int id)
        {
            var db = new SQLiteConnection(dbPath);
            try
            {
                return db.Table<Address>().Where(i => i.Id == id).FirstOrDefault();
            }
            catch
            {
                return null;
            }
        }

        public List<Person> GetAllPerson()
        {
            var db = new SQLiteConnection(dbPath);
            return db.Table<Person>().ToList();
        }
        public List<Address> GetAllAddress()
        {
            var db = new SQLiteConnection(dbPath);
            return db.Table<Address>().ToList();
        }
        public Person GetPerson(string user)
        {
            var db = new SQLiteConnection(dbPath);
            return db.Table<Person>().Where(i => i.User == user).FirstOrDefault();
        }
        public List<Person> GetPersonByUser(string user)
        {
            var db = new SQLiteConnection(dbPath);
            return db.Table<Person>().Where(i => i.User == user).ToList();
        }

        [Table("Person")]
        public class Person
        {
            [PrimaryKey, AutoIncrement, Column("_id")]
            public int Id { get; set; }
            public string User { get; set; }
            public string Code { get; set; }
        }

        [Table("Address")]
        public class Address
        {
            [PrimaryKey, AutoIncrement, Column("_aid")]
            public int AId { get; set; }
            public string City { get; set; }
            public string PCode { get; set; }
            public int Id { get; set; }
        }
    }
}
