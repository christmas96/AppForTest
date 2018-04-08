using System;
using System.Collections.Generic;
using System.Linq;

using System.Net.Http;
using Newtonsoft.Json;

using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http.Headers;
using System.Net;

namespace App1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Task : ContentPage
    {
        public bool temp = false;
        public int ID = 0;

        public Task(Items item)
        {
            InitializeComponent();
            ID = item.Id;
            Title.Text = item.title;
            Description.Text = item.description;
            Toggle.IsToggled = item.done;
            temp = item.done;
        }

        private void switcher_Toggled(object sender, ToggledEventArgs e)
        {
            temp = Toggle.IsToggled;
        }

        private async Task<int> Delete_Task(object sender, ToggledEventArgs e)
        {
            HttpClient client = new HttpClient();
            string uri = string.Concat("https://apifortest.azurewebsites.net/api/ToDoList/", ID);
            var response = await client.DeleteAsync(uri);
            return JsonConvert.DeserializeObject<int>(response.Content.ReadAsStringAsync().Result);
        }

        public async void Delete_Button(object sender, ToggledEventArgs e)
        {
            await DeleteTaskAsync(ID);
            var next = await DisplayAlert("", "Deleted", "", "Ok");
            await Navigation.PushAsync(new MainPage());           
        }

        public async void Update_Button(object sender, ToggledEventArgs e)
        {
            Items it = new Items(ID, Title.Text, Description.Text, temp);
            await UpdateTaskAsync(ID, it);
            var next = await DisplayAlert("", "Saved", "", "Ok");
            await Navigation.PushAsync(new MainPage());                      
        }

        public async Task<Items> UpdateTaskAsync(int id, Items item)
        {
            HttpClient client = new HttpClient();
            var data = JsonConvert.SerializeObject(item);
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await client.PutAsync(string.Concat("https://apifortest.azurewebsites.net/api/ToDoList/", id), content);
            return JsonConvert.DeserializeObject<Items>(response.Content.ReadAsStringAsync().Result);
        }

        public async Task<HttpStatusCode> DeleteTaskAsync(int itemIndex)
        {
            HttpClient client = new HttpClient();
            await client.DeleteAsync(string.Concat("https://apifortest.azurewebsites.net/api/ToDoList/", itemIndex));
            return HttpStatusCode.OK;
        }
    }
}