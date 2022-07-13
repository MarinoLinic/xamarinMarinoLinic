using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MarinoLinicApp1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Users : ContentPage
    {

        List<User> userList = new List<User>();

        public Users()
        {
            InitializeComponent();
            usersAsync();
        }

        public async Task usersAsync()
        {
            var uri = new Uri("https://www.idt.mdh.se/personal/plt01/languide/?get=users");
            HttpClient myClient = new HttpClient();
            var response = await myClient.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                string json = content.ToString();
                var jsonObject = JObject.Parse(json);
                var jsonArray = JArray.Parse(jsonObject["data"].ToString());

                foreach (var user in jsonArray)
                {
                    User newUser = new User();

                    newUser.ID = user["id_user"].ToString();
                    newUser.Email = user["email"].ToString();
                    newUser.Time = user["create_time"].ToString();

                    userList.Add(newUser);
                }
            }

            usersListView.ItemsSource = userList;
        }

        public void ID_Clicked(object sender, System.EventArgs e)
        {
            usersListView.ItemsSource = userList;
        }

        public void Time_Clicked(object sender, System.EventArgs e)
        {
            List<User> TimeSort = userList.OrderByDescending(x => x.Time).ToList();
            usersListView.ItemsSource = TimeSort;
        }

        public void Email_Clicked(object sender, System.EventArgs e)
        {
            List<User> EmailSort = userList.OrderBy(x => x.Email).ToList();
            usersListView.ItemsSource = EmailSort;
        }
    }
}