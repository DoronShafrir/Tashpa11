using Microsoft.Data.SqlClient;
using Tashpa11.Model;
using Tashpa11.App_Code;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using Tashpa11.Mapping;
using System.Reflection.PortableExecutable;

namespace Tashpa11.Mapping
{
    public class StudentsDB
    {
        private string connectionString = Imp_Data.ConString;
        SqlConnection connection;
        SqlCommand command;
        SqlDataReader reader;

        public StudentsDB()
        {
            connection = new SqlConnection(connectionString);
            command = new SqlCommand();
            command.Connection = connection;
        }

        public Studentss SelectAll()
        {
            Studentss students = new Studentss();

            //string command = "SELECT Person.Name, Courses.CourseName FROM Student INNER JOIN Person ON  SId=Person.PId JOIN  Courses ON Student.CourseId = Courses.CId;";

            
            command.CommandText = "SELECT student_person.Name AS StudentName, Courses.CourseName, " +
    "teacher_person.Name AS TeacherName " +
    "FROM Student " +
    "JOIN Person AS student_person ON Student.SId = student_person.Id " +
    "JOIN Courses ON Student.CourseId = Courses.CId " +
    "JOIN Person AS teacher_person ON Courses.ResponsibleTeacher = teacher_person.Id;";
            try
            {
                command.Connection = connection;
                connection.Open();
                reader = command.ExecuteReader();
                Student student;
                while (reader.Read())
                {
                    student = new Student();
                    //student.SId = int.Parse(reader["SId"].ToString());
                    student.Name = reader["StudentName"].ToString();
                    student.CourseName = reader["CourseName"].ToString();
                    student.TeacherName = reader["TeacherName"].ToString();
                    
                    
                    students.Add(student);
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
            return students;
        }
        //public string RenderAllStudents()
        //{
        //    string SQLStr = SelectAll();
        //    string RenderTable = App_Code.Helper.FetchTable(SQLStr, connectionString);
          
        //    return RenderTable;
        //}

        //public string CreateTableLIne(Student item)
        //{
        //    string studentsList = "";
        //    studentsList += item.Name.ToString() + "  ";
        //    studentsList += item.CourseName.ToString() + "  ";


        //    return studentsList;

        //}
        public int Insert(int SId, int CourseId)
        {
            int records = 0;
            int arg1 = SId;
            int arg2 = CourseId;
            if (arg1 != 0 && arg2 != 0)
            {

                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = new SqlCommand($"INSERT INTO Student VALUES('{arg1}','{arg2}')", connection))

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

        public int DeleteStudent(int studentId)
        {
            int records = 0;
            //int arg1 = CheckStudent(student.Name);
            //int arg2 = CheckCourse(student.CourseName); 
            int arg1 = studentId;
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand($"DELETE FROM Student WHERE Id ='{arg1}';", connection))

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
        public int CheckStudent(string item)
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




        public string PrepareStudenstsDropDownList()
        {
            Studentss students = new Studentss();
            using (SqlConnection connection = new SqlConnection(connectionString))
            //using (SqlCommand command = new SqlCommand("SELECT Student.SId, Person.Name FROM Student, Person WHERE Student.SId=Person.PId GROUP BY  Person.Name, Student.SId ;", connection))
            using (SqlCommand command = new SqlCommand("SELECT  Person.PId, Person.Name, Person.FName FROM Person WHERE Teacher = 0;", connection))
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
                            Name = reader["Name"].ToString(),
                            FName = reader["FName"].ToString(),
                            PId = (int)reader["PId"]
                        };
                        students.Add(student);
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
            prepartion = " <select name=\"idStudentToSelect\" >";
            prepartion += "<option>Student to add a course";
            foreach (var item in students)
            {
                prepartion += $"<option MMM={item.PId}> Student Name:{item.Name} {item.FName} </ option >";
            }
            prepartion += "</select>";
            return prepartion;
        }

        public string PrepareStudenstsDropDownListToDelete()
        {
            Studentss students = new Studentss();
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("SELECT Student.Id, Person.Name, Courses.CourseName FROM Student INNER JOIN Person ON  SId=Person.PId JOIN  Courses ON Student.CourseId = Courses.CId;", connection))
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
                            Id = (int)reader["Id"],
                            Name = reader["Name"].ToString(),
                            CourseName = reader["CourseName"].ToString()
                        };
                        students.Add(student);
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
            prepartion = " <select name=\"idToDelete\" >";
            prepartion += "<option>Student to be removed from course>";
            foreach (var item in students)
            {
                prepartion += $"<option MMM={item.Id}> Name:{item.Name}   Course:{item.CourseName} </ option >";
            }
            prepartion += "</select>";
            return prepartion;
        }

    }
}
