namespace University.Core.Entities
{
    public class Course
    {

        public int Id { get; set; }
        public string CourseName { get; set; }
        public string Description { get; set; }
        public DateTime CreatedTime { get; private set; }
        public DateTime LastUpdatedTime { get; private set; }

        public void SetCreated() => CreatedTime = DateTime.Now;
        public void SetUpdated() => LastUpdatedTime = DateTime.Now;
    }
}
