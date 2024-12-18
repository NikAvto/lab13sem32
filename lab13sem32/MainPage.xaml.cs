using System;
using Microsoft.Maui.Controls;

namespace lab13sem32
{
    public partial class MainPage : ContentPage
    {
        private readonly LocalDbService _dbService;
        public MainPage()
        {
            InitializeComponent();
            _dbService = new LocalDbService();
        }

        private async void OnResearchClicked(object sender, EventArgs e)
        {
            /*string str = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            await DisplayAlert("N", $"{str}", "OK");*/
            await Navigation.PushModalAsync(new ResearchPage(_dbService));
        }

        private async void OnEquipmentClicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new EquipmentPage(_dbService));
        }

        private async void OnScientistClicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new ScientistPage(_dbService));
        }
    }
}