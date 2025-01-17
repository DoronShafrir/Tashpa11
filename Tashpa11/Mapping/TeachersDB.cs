using Microsoft.Data.SqlClient;
using Tashpa11.Model;

namespace Tashpa11.Mapping
{
    public class TeachersDB
    {
        public string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Physics.mdf;Integrated Security=True";

        public string SelectAll()
        {

            string command = "SELECT Person.Name, Courses.CourseName FROM Teacher INNER JOIN Person ON  TId=Person.PId JOIN  Courses ON Teacher.CourseId = Courses.CId;";

            return command;
        }

        public string RenderAll()
        {
            string SQLStr = SelectAll();
            string RenderTable = Helper.FetchTable(SQLStr, connectionString);

            return RenderTable;
        }

        public int Insert(Teacher teacher)
        {
            int records = 0;
            int arg1 = CheckName(teacher.Name);
            int arg2 = CheckCourse(teacher.CourseName);
            if (arg1 != 0 && arg2 != 0)
            {

                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = new SqlCommand($"INSERT INTO Teacher VALUES('{arg1}','{arg2}')", connection))

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
            }

            return records;
        }

        public int Delete(Teacher teacher)
        {
            int records = 0;
            int arg1 = CheckName(teacher.Name);
            int arg2 = CheckCourse(teacher.CourseName); ;
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand($"DELETE FROM Teacher WHERE TId ='{arg1}' AND CourseId = '{arg2}' ;", connection))

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
        //checkmname() checks if teh anme exist in the people table if yes return the PId and if not return 0
        public int CheckName(string item)
        {
            int recordId = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand($"SELECT PId FROM Person WHERE Name = '{item}';", connection))

                try
                {
                    connection.Open();
                    recordId = command.ExecuteScalar() == null ? 0 : (int)command.ExecuteScalar();
                }
                catch (Exception e)
                {
                }
                finally
                {
                    connection.Close();
                }
            return recordId;
        }
        public int CheckCourse(string item)
        {
            int recordId = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand($"SELECT CId FROM Courses WHERE CourseName = '{item}';", connection))

                try
                {
                    connection.Open();
                    recordId = command.ExecuteScalar() == null ? 0 : (int)command.ExecuteScalar();
                }
                catch (Exception e)
                {
                }
                finally
                {
                    connection.Close();
                }
            return recordId;
        }
    }
}
