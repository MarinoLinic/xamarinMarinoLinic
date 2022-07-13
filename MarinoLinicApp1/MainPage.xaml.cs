using Firebase.Auth;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MarinoLinicApp1
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        public string WebAPIKey = "AIzaSyCCSoFjkz-pz5ZT_gfYMGwaNr5FNUruvaI";

        async private void loginButton_Clicked(object sender, EventArgs e)
        {
            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(WebAPIKey));
            try
            {
                var auth = await authProvider.SignInWithEmailAndPasswordAsync(userEntry.Text, passEntry.Text);
                var content = await auth.GetFreshAuthAsync();
                var serializedContent = JsonConvert.SerializeObject(content);
                Preferences.Set("MyFirebaseRefreshToken", serializedContent);
                await Navigation.PushAsync(new HomePage());
                string getToken = auth.FirebaseToken;
                await App.Current.MainPage.DisplayAlert("Great!", "You have successfully logged in", "Ok");
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Alert", "Incorrect log in credentials.", "OK");
            }
        }

        /* private void loginButton_Clicked(object sender, EventArgs e)
        {
            bool isEmailEmpty = string.IsNullOrEmpty(userEntry.Text);
            bool isPasswordEmpty = string.IsNullOrEmpty(passEntry.Text);

            if (!isEmailEmpty && !isPasswordEmpty)
            {
                Navigation.PushAsync(new HomePage());
            }

            else
            {
                // do not navigate
            }
        }
        */

        private void registerButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RegistrationPage());
        }
    }
}
