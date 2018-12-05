using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;

namespace NinPriceMobile
{
    public partial class MainPage : ContentPage
    {
        ObservableCollection<GameModel> gameObjects = new ObservableCollection<GameModel>();

        public MainPage()
        {
            InitializeComponent();

            GetData();
            listView.ItemsSource = gameObjects;
        }

        public async void GetData()
        {
            var client = new HttpClient();

            var response = await client.GetAsync("https://searching.nintendo-europe.com/ru/select?q=*&fq=type%3AGAME%20AND%20((playable_on_txt%3A%22HAC%22))%20AND%20sorting_title%3A*%20AND%20*%3A*&sort=price_discount_percentage_f%20desc%2C%20price_lowest_f%20desc&start=0&rows=200&wt=json&bf=linear(ms(priority%2CNOW%2FHOUR)%2C1.1e-11%2C0)");
            var responseString = await response.Content.ReadAsStringAsync();

            JObject games = JObject.Parse(responseString);
            IList<JToken> results = games["response"]["docs"].Children().ToList();
            IList<GameModel> gameList = new List<GameModel>();
            foreach (var result in results)
            {
                GameModel gameModel = result.ToObject<GameModel>();
                if (gameModel.price_has_discount_b)
                {
                    gameObjects.Add(gameModel);
                }
            }

            activity.IsVisible = false;
            gameListView.IsVisible = true;
        }

        void Handle_Activated(object sender, System.EventArgs e)
        {
            DisplayAlert("NinPrice Mobile 0.1b", "Приложение для просмотра текущих скидок на игры Nintendo Switch (RU регион)\n\nby RocketStorm © 2018\nhttp://rocketstorm.me\nneocreystudios@gmail.com", "OK");
        }
    }
}
