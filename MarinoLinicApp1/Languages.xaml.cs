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
    public partial class Languages : ContentPage
    {

        List<Language> langList = new List<Language>();

        public Languages()
        {
            InitializeComponent();
            langsAsync();
        }

        public async Task langsAsync()
        {
            var uri = new Uri("https://www.idt.mdh.se/personal/plt01/languide/?get=results&order=language");
            HttpClient myClient = new HttpClient();
            var response = await myClient.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                string json = content.ToString();
                var jsonObject = JObject.Parse(json);
                var jsonArray = JArray.Parse(jsonObject["data"].ToString());

                foreach (var langu in jsonArray)
                {
                    Language newLang = new Language();

                    newLang.Lang = langu["language"].ToString();

                    if(newLang != null)
                    {
                        langList.Add(newLang);
                    }
                    
                }
            }

            List<Language> LangSort = langList.OrderByDescending(x => x.Lang).ToList();
            langListView.ItemsSource = LangSort;
        }
    }
}