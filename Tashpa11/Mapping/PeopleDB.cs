using Microsoft.Data.SqlClient;
using Tashpa11.Model;

namespace Tashpa11.Mapping
{
    public class PeopleDB
    {
        private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Physics.mdf;Integrated Security=True";

        public People SelectAll()
        {
            People people = new People();

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("SELECT * FROM Person", connection))
            {
                try
                {
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Person person = new Person
                            {
                                Name = reader["Name"].ToString(),
                                FName = reader["FName"].ToString(),
                                UserName = reader["UserName"].ToString(),
                                Password = reader["Password"].ToString(),
                                Admin = (bool)reader["Admin"],
                                Teacher = (bool)reader["Teacher"]

                            };
                            people.Add(person);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle the exception (log, throw, etc.)
                    Console.WriteLine(ex.Message);
                }
            }
            return people;
        }
        public string RenderAllPeople()
        {
            People people = SelectAll();
            string RenderTable = "";
            foreach (var item in people)
            {
                RenderTable += this.CreateTableLIne(item);
                RenderTable += "<br>";
            }
            return RenderTable;
        }
        public string CreateTableLIne(Person item)
        {
            string coursesList = "";
            coursesList += item.Name.ToString() + "  ";
            coursesList += item.FName.ToString() + "  ";
            coursesList += item.UserName.ToString() + "  ";
            coursesList += item.Password.ToString() + "  ";
            coursesList += item.Admin.ToString() + "  ";
            coursesList += item.Teacher.ToString() + "  ";

            return coursesList;

        }
        public int Insert(Person person)
        {
            int records = 0;
            string arg1 = person.Name;
            string arg2 = person.FName;
            string arg3 = person.UserName;
            string arg4 = person.Password;
            bool arg5 = person.Admin;
            bool arg6 = person.Teacher;
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand($"INSERT INTO Person VALUES('{arg1}','{arg2}', '{arg3}', '{arg4}', '{arg5}', '{arg6}')", connection))

                try
                {
                    connection.Open();
                    records = command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                }
                finally
                {
                    connection.Close();
                }
            return records;
        }

        public int DeletePerson(Person person)
        {
            int records = 0;
            string arg1 = person.Name != "" ? person.Name : "-1";
            string arg2 = person.FName != "" ? person.FName : "-1";
            string arg3 = person.UserName != "" ? person.UserName : "-1";
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand($"DELETE FROM Person WHERE Name ='{arg1}' OR Fname = '{arg2}' OR UserName = '{arg3}' ;", connection))

                try
                {
                    connection.Open();
                    records = command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                }
                finally
                {
                    connection.Close();
                }
            return records;
        }

    }
}
