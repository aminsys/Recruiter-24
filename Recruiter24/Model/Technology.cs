using Recruiter24.View;

namespace Recruiter24.Model
{
    public class Technology
    {
        public string Name { get; set; }
        public Guid Guid { get; set; }

        public static string GetName(Guid guid)
        {
            return MainPage.Filter.SingleOrDefault(g => g.Guid == guid).Name;
        }
    }
}
