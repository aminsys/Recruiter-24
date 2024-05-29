using Recruiter24.Models;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http.Json;

namespace Recruiter24.View;

public partial class ApplicantsPage : ContentPage
{
    public ObservableCollection<Applicant> Applicants = new();
    private int yoe;
    private Technology technology;

	public ApplicantsPage()
	{
		InitializeComponent();
	}

    public ApplicantsPage(int yearsOfExperience, Technology technology)
    {
        InitializeComponent();
        this.yoe = yearsOfExperience;
        this.technology = technology;
    }

    protected override async void OnAppearing()
    {
        await GetApplicants(this.technology.Guid, yoe);
    }

    public async Task GetApplicants(Guid guid, int yoe)
    {
        try
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage res = await client.GetAsync("https://app.ifs.aero/EternalBlue/api/candidates");
                var json = await res.Content.ReadFromJsonAsync<ObservableCollection<Applicant>>();
                var query = json.Where(a => a.Experience.Any(e => e.TechnologyId == guid) && a.Experience.Any(e => e.YearsOfExperience >= yoe));
                Applicants = new ObservableCollection<Applicant>(query);
                collectionView.ItemsSource = Applicants;
            }
        }
        catch (Exception e)
        {
            Debug.WriteLine("Error accorred: " + e.Message);
        }
    }

    private void Delete_Invoked(object sender, SwipeItems e)
    {
        // Applicant a = sender as Applicant; 
        Debug.WriteLine("Delete swipe: " + e.ToString());
    }

    private void Save_Invoked(object sender, SwipeItem e)
    {
        Debug.WriteLine("Save swipe: " + e.ToString());

    }
}