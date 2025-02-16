using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Tashpa11.Mapping;
using Tashpa11.Model;

namespace Tashpa11.Pages.Courses
{
    public class ManageCoursesModel : PageModel
    {
        public string active_input { get; set; }
        public string submitNewButton { get; set; }
        public string delete_input { get; set; }
        public string deleteButton { get; set; }
        public string msg { get; set; }
        public string DisplayList { get; set; }
        public string insertMSG { get; set; }
        public string insert_button { get; set; }
        public string deleteMSG { get; set; }
        public Coursess List { get; set; } = new Coursess(); 
        public Course course { get; set; } = new Course(); 


        public void OnGet()
        {
            active_input = "display:none";
            submitNewButton = "display:none";
            DisplayList = "display:block";
            delete_input = "display:none";
            deleteButton = "display:none";                       
            CoursesDB db = new CoursesDB();
            List = db.SelectAll();
        }
        public void OnPostRenderCourses()
        {
            active_input = "display:none";
            submitNewButton = "display:none";
            DisplayList = "display:block";
            delete_input = "display:none";
            deleteButton = "display:none";
            CoursesDB db = new CoursesDB();
            List = db.SelectAll();
        }

        public void OnPostShowAddCourses()
        {
            DisplayList = "display:none";
            active_input = "display:block";
            insert_button = "display:block";
            delete_input = "display:none";
            deleteButton = "display:none";
        }
        public void OnPostShowDeleteCourse()
        {
            DisplayList = "display:none";
            active_input = "display:none";
            insert_button = "display:none";
            delete_input = "display:block";
            deleteButton = "display:block";
        }
        public void OnPostInsertCourse(string newCourseName, string newCourseNumber, string newCourseTeacher)
        {
            Course newCourse = new Course();
            CoursesDB db = new CoursesDB();
            newCourse.CourseName = newCourseName;
            newCourse.CourseNumber = newCourseNumber;
            newCourse.Name = newCourseTeacher;

            int records = db.Insert(newCourse);
            delete_input = "display:none";
            deleteButton = "display:none";
            if (records == 1)
            {
                insert_button = "disply:none";
                insertMSG = "Course Added Successfuly";
            }
            else { insertMSG = "Could Not Add Course !!!"; };
        }
    }
}

