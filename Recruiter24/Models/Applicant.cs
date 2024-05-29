
namespace Recruiter24.Models
{
    public class Applicant
    {
        public string FullName { get; set; }
        public Uri ProfilePicture { get; set; }
        public List<Experience> Experience { get; set; }
    }
}
