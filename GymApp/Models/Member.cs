namespace GymApp.Models
{
    public class Member
    {
        public int Id { get; set; }
        public string TcNumber { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MembershipPackage { get; set; }
        public DateTime MembershipEndDate { get; set; }
        public ICollection<LessonRegistration> Registrations { get; set; } = new List<LessonRegistration>();

    }
}
