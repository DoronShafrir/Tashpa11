namespace Tashpa11.Model
{
    public class Teacher : Course
    {
        public int TId { get; set; }
        public int CourseId { get; set; }
    }

    public class Teachers : List<Teacher>
    {
        public Teachers() { }
        public Teachers(IEnumerable<Teacher> list) : base(list) { }
    }
}
