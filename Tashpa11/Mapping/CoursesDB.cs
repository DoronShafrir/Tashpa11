using Microsoft.Data.SqlClient;
using System.Data;
using Tashpa11.Model;

namespace Tashpa11.Mapping
{
    public class CoursesDB
    {
        private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Physics.mdf;Integrated Security=True";
        SqlConnection connection;
        SqlCommand command;
        SqlDataReader reader;
        public CoursesDB()
        {
            connection = new SqlConnection(connectionString);
            command = new SqlCommand();
            command.Connection = connection;
        }
        public Courses SelectAll()
        {
            Courses courses = new Courses();
            command.CommandText = "SELECT Courses.CourseName, Courses.CourseNumber, Person.Name " +
                "FROM Courses INNER JOIN Person ON Courses.ResponsibleTeacher=Person.PId;";
            try
            {
                command.Connection = connection;
                connection.Open();
                reader = command.ExecuteReader();
                Course course;
                while (reader.Read())
                {
                    course = new Course();
                    course.CourseName = reader["CourseName"].ToString();
                    course.CourseNumber = reader["CourseNumber"].ToString();
                    course.Name = reader["Name"].ToString();
                    courses.Add(course);
                }
            }
            catch (Exception e)
            {
            }
            finally
            {
                if (reader != null)
                    reader.Close();

                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
            return courses;

        }

        public string RenderAllCourses()
        {
            Courses courses = SelectAll();
            string RenderTable = "";
            foreach (var item in courses)
            {
                RenderTable += this.CreateTableLIne(item);
                RenderTable += "<br>";
            }
            return RenderTable;
        }
        public string CreateTableLIne(Course item)
        {
            string coursesList = "";
            coursesList += item.CourseName.ToString() + "  ";
            coursesList += item.CourseNumber.ToString() + "  ";
            coursesList += item.Name.ToString() + "  ";
            return coursesList;
        }
        public int Insert(Course course)
        {
            int records = 0;
            string arg1 = course.CourseName;
            string arg2 = course.CourseNumber;
            int arg3 = CheckName(course.Name);
            if (!(arg3 > 0)) return 0;
            command.CommandText = "INSERT INTO Courses  " +
                $"VALUES('{arg1}','{arg2}', '{arg3}') ";
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

        public int DeleteCourse(Course course)
        {
            int records = 0;
            string arg1 = course.CourseName;
            string arg2 = course.CourseNumber;
            int arg3 = CheckName(course.Name);
            if (!(arg3 > 0)) return 0;

            command.CommandText = "DELETE FROM Courses  " +
                $"WHERE CourseName ='{arg1}' OR CourseNumber = '{arg2}' OR ResponsibleTeacher =  '{arg3}'; ";

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
        public string PrepareCoursestDropDownList()
        {
            Courses courses = new Courses();
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("SELECT Courses.CId, Courses.CourseName FROM  Courses;", connection))
            {
                try
                {
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    // Loop through the data and add <option> elements to the <select> element
                    while (reader.Read())
                    {
                        Student student = new Student
                        {
                            CId = (int)reader["CId"],
                            CourseName = reader["CourseName"].ToString()
                        };
                        courses.Add(student);
                    }

                    // Close the SqlDataReader
                    reader.Close();
                }
                catch (Exception ex)
                {
                    // Handle the exception (log, throw, etc.)
                    Console.WriteLine(ex.Message);
                }

                finally
                {
                    connection.Close();
                }
            }
            string prepartion;
            prepartion = " <select name=\"idCourseToSelect\" >";
            prepartion += "<option>Course to register the student";
            foreach (var item in courses)
            {
                prepartion += $"<option value={item.CId}> Course Name:{item.CourseName} </ option >";
            }
            prepartion += "</select>";
            return prepartion;
        }
        //checkmname() checks if teh anme exist in the people table if yes return the PId and if not return 0
        public int CheckName(string name)
        {
            int recordId = 0;
            command.CommandText = $"SELECT PId FROM Person WHERE Name = '{name}';";
            try
            {
                connection.Open();
                recordId = (int)command.ExecuteScalar();
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
