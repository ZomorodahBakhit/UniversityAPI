using University.Core.Entities.Identity;

namespace University.Core.Entities
{
    public class Student
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime CreatedTime { get; private set; }
        public DateTime LastUpdatedTime { get; private set; }

        public int UserId { get; set; }
        public User User { get; set; }
        public void SetCreated() => CreatedTime = DateTime.Now;
        public void SetUpdated() => LastUpdatedTime = DateTime.Now;
    }
}
