using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App1
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
            LoadData();
            ListView1.ItemSelected += (object sender, SelectedItemChangedEventArgs e) =>
            {
                if (((ListView)sender).SelectedItem == null) return;
                var foo = e.SelectedItem as Items;
                Next(foo);
                ((ListView)sender).SelectedItem = null;
            };
        }

        private void Next (Items item)
        {
            Navigation.PushAsync(new Task(item));
        }

        public async void LoadData()
        {
            try
            {
                HttpClient client = new HttpClient();
                var RestURL = "https://apifortest.azurewebsites.net/api/ToDoList";
                client.BaseAddress = new Uri(RestURL);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync(RestURL);
                var content = await response.Content.ReadAsStringAsync();
                var Todo = JsonConvert.DeserializeObject<List<Items>>(content);
                ListView1.ItemsSource = Todo;
            }
            catch (HttpRequestException e)
            {
                await DisplayAlert("Error", e.ToString(), "Ok");
            }
            catch (JsonSerializationException e)
            {
                await DisplayAlert("Error", e.ToString(), "Ok");
            }
        }
    }
}
