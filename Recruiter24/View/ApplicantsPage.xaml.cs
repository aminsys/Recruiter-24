using Recruiter24.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http.Json;

namespace Recruiter24.View;

public partial class ApplicantsPage : ContentPage
{
    public ObservableCollection<Applicant> Applicants = new();

	public ApplicantsPage()
	{
		InitializeComponent();
	}

    public ApplicantsPage(int yearsOfExperience, Technology technology)
    {
        InitializeComponent();
        _ = GetApplicants(technology.Guid, yearsOfExperience);
    }

    public async Task<ObservableCollection<Applicant>> GetApplicants(Guid guid, int yoe)
    {
        try
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage res = await client.GetAsync("https://app.ifs.aero/EternalBlue/api/candidates");
                Applicants = await res.Content.ReadFromJsonAsync<ObservableCollection<Applicant>>();
                var query = Applicants.Where(a => a.Experience.Any(e => e.TechnologyId == guid) && a.Experience.Any(e => e.YearsOfExperience >= yoe));
                collectionView.ItemsSource = query;
                
                return new ObservableCollection<Applicant>(query);
            }
        }
        catch (Exception e)
        {
            Debug.WriteLine("Error accorred: " + e.Message);
            return new ObservableCollection<Applicant>();
        }
        

    }

    private void Delete_Invoked(object sender, EventArgs e)
    {
        Debug.WriteLine("Delete swipe");
    }

    private void Save_Invoked(object sender, EventArgs e)
    {
        Debug.WriteLine("Save swipe");

    }
}