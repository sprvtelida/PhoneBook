using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using PhoneNumberFrame.Models;
using Dapper;

namespace PhoneNumberFrame.Repositories
{
    class PeopleRepo
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public List<MService> GetPeople()
        {
            using(IDbConnection db = new SqlConnection(connectionString))
            {
                string sql = "exec ShowPeople";
                return db.Query<MService>(sql).ToList();
            }
        }

        public List<MPosition> GetPositions()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sql = @"SELECT Id
                                     ,Name
                                FROM Positions";
                return db.Query<MPosition>(sql).ToList();
            }
        }

        public List<MService> SearchByName(string name)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sql = "select a.*, b.Name as Position " +
                    "from People a, Positions b " +
                    "where a.PositionId = b.Id " +
                    "and a.Name like '%'+ @name + '%'";
                return db.Query<MService>(sql, new { name=name }).ToList();
            }
        }

        public List<MService> Search(string q)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sql = "exec search";
                return db.Query<MService>(sql, new { q  }).ToList();
            }
        }

        internal List<MService> SearchByFullname(string fullname)
        {
            fullname = fullname.Trim();
            string[] fullnameArray = fullname.Split(' ');
            if ((fullname == "") || (fullnameArray.Length < 2)) return new List<MService>(); ;
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sql = "select a.*, b.Name as Position " +
                    "from People a, Positions b " +
                    "where a.PositionId = b.Id " +
                    "and a.Name like '%'+ @Name + '%'" +
                    "and a.Surname like '%'+ @Surname + '%'";
                return db.Query<MService>(sql, new { Name = fullnameArray[0], Surname = fullnameArray[1] }).ToList();
            }
        }

        internal List<MService> SearchBySurname(string Surname)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sql = "select a.*, b.Name as Position " +
                    "from People a, Positions b " +
                    "where a.PositionId = b.Id " +
                    "and a.Surname like '%'+ @Surname + '%'";
                return db.Query<MService>(sql, new { Surname }).ToList();
            }
        }

        internal List<MService> SearchByWorkPhoneNumber(string workPhoneNumber)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sql = @"select * FROM People
                                where WorkPhoneNum = @workPhoneNumber";
                return db.Query<MService>(sql, new { workPhoneNumber }).ToList();
            }
        }

        internal List<MService> SearchById(string id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sql = @"select * FROM People
                                where Id = @id";
                return db.Query<MService>(sql, new { id }).ToList();
            }
        }

        internal List<MService> SearchByPosition(int pos)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sql = @"SELECT *
                                FROM People
                                WHERE PositionId = @pos";
                return db.Query<MService>(sql, new { pos }).ToList();
            }
        }

        public void AddPerson(MService person)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sql = @"INSERT INTO [dbo].[People]
                                ([Name]
                                ,[Surname]
                                ,[Birthdate]
                                ,[Gender]
                                ,[WorkPhoneNum]
                                ,[PhoneNum]
                                ,[Address]
                                ,[Email]
                                ,[WorkBegin]
                                ,[WorkEnd]
                                ,[PositionId])
                            VALUES(
                                 @Name
                                ,@SurName
                                ,@Birthdate
                                ,@Gender
                                ,@WorkPhoneNum
                                ,@PhoneNum
                                ,@Address
                                ,@Email
                                ,@WorkBegin
                                ,@WorkEnd
                                ,@PositionId)";
                db.Execute(sql, new {   Name = person.Name, 
                                        Surname = person.Surname, 
                                        Birthdate = person.Birthdate,
                                        Gender = person.Gender, 
                                        WorkPhoneNum = person.WorkPhoneNum,
                                        PhoneNum = person.PhoneNum,
                                        Address = person.Address,
                                        Email = person.Email,
                                        WorkBegin = person.WorkBegin,
                                        WorkEnd = person.WorkEnd,
                                        PositionId = person.PositionId,
                                        });
            }
        }


        public void DeletePerson(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sql = @"DELETE FROM People
                        WHERE @id = Id";
                db.Execute(sql, new { id });
            }
            
        }

        public void DeletePerson(string fullname)
        {
            fullname = fullname.Trim();
            string[] fullnameArray = fullname.Split(' ');
            if ((fullname == "") || (fullnameArray.Length < 2)) return;
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                string sql = @"DELETE FROM People
                        WHERE @firstName = Name and @lastName = Surname";
                db.Execute(sql, new { firstName = fullnameArray[0], lastName = fullnameArray[1] });
            }
        }
    }
}
