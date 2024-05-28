using Recruiter24.Models;
using Recruiter24.View;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http.Json;

namespace Recruiter24;

public partial class MainPage : ContentPage
{
	public Collection<Technology> Filter { get; set; } = new();

	public MainPage()
	{
        InitializeComponent();
		GetFilterCriteria();
    }

    public async void GetFilterCriteria()
	{
		try
		{
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage res = await client.GetAsync("https://app.ifs.aero/EternalBlue/api/technologies");
                res.EnsureSuccessStatusCode();
                Filter = await res.Content.ReadFromJsonAsync<Collection<Technology>>();
                picker.ItemsSource = Filter;
                picker.ItemDisplayBinding = new Binding("Name");
            }
        }
		catch (Exception e)
		{
			Debug.WriteLine("Error accorred: " + e.Message);
		}
		
    }

	private async void OnFilterClicked(object sender, EventArgs e)
	{
		Debug.WriteLine("This is a test result of clicking a button.");
		Debug.WriteLine("Experience? " + experience.Text);
		var selectedTechnology = picker.SelectedItem as Technology;
        Debug.WriteLine("Selected Item? " + selectedTechnology.Name);

		// Open a new view
		
		await Navigation.PushAsync(new ApplicantsPage(Int32.Parse(experience.Text), selectedTechnology));
	}

}

