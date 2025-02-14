using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using Tashpa11.Model;
using Microsoft.AspNetCore.Http;
using Tashpa11.Mapping;


namespace Tashpa11.Pages.Students
{
    public class MangeMyCoursesModel : PageModel
    {
        public string active_input { get; set; }
        public string submitNewButton { get; set;}
        public string delete_input { get; set; }
        public string deleteButton { get; set; }
        public string msg { get; set; }
        public string List { get; set; }
        public string  DisplayList { get; set; }
        public string insertMSG { get; set; }

        public void OnGet()
        {
            active_input = "display:none";
            submitNewButton ="display:none";
            //List.Style.Add("display", "block");
            delete_input = "display:none";
            deleteButton = "display:none";
            //if (String.IsNullOrEmpty(HttpContext.Session.GetString("Username").ToString())) 
            msg = HttpContext.Session.GetString("Username").ToString();
            string studentName = HttpContext.Session.GetString("Username").ToString();
            IndividualStudentDB db = new IndividualStudentDB(studentName);
            List = db.RenderAllCourses();
        }

        public  void OnPostShowAddCourses()
        {
            DisplayList="display:none";
            active_input="display:block";
            submitNewButton="display:block";
            delete_input="display:none";
            deleteButton="display:none";
        }
        public void OnPostShowDeleteCourse()
        {
            DisplayList = "display:none";
            active_input = "display:none";
            submitNewButton = "display:none";
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
            if (records == 1) { insertMSG = "Course Added Successfuly"; }
            else { insertMSG = "Could Not Add Course !!!"; };
        }
    }
}
