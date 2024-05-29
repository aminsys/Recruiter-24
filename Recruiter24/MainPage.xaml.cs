using Recruiter24.Models;
using Recruiter24.View;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http.Json;

namespace Recruiter24;

public partial class MainPage : ContentPage
{
	public static ObservableCollection<Technology> Filter { get; set; } = new();

	public MainPage()
	{
        InitializeComponent();
    }

	protected override async void OnAppearing()
	{
        Filter = await GetFilterCriteria();
        picker.ItemsSource = Filter;
        picker.ItemDisplayBinding = new Binding("Name");

    }

    public async Task<ObservableCollection<Technology>> GetFilterCriteria()
	{
        ObservableCollection<Technology> filter = null;
		try
		{
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage res = await client.GetAsync("https://app.ifs.aero/EternalBlue/api/technologies");
                res.EnsureSuccessStatusCode();
                filter = await res.Content.ReadFromJsonAsync<ObservableCollection<Technology>>();
            }
			return filter;
        }
		catch (Exception e)
		{
			Debug.WriteLine("Error accorred: " + e.Message);
			return new ObservableCollection<Technology>();
        }		
    }

	private async void OnFilterClicked(object sender, EventArgs e)
	{
		var selectedTechnology = picker.SelectedItem as Technology;
	
		await Navigation.PushAsync(new ApplicantsPage(Int32.Parse(experience.Text), selectedTechnology));
	}

}

