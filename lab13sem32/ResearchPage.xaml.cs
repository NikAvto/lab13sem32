using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Maui.Controls;

namespace lab13sem32
{
    public partial class ResearchPage : ContentPage
    {
        private readonly LocalDbService _dbService;
        private int _editResearchId;

        public ResearchPage(LocalDbService dbService)
        {
            InitializeComponent();
            _dbService = dbService;
            LoadData();
        }

        private async Task LoadData()
        {
            try
            {
                var researches = await _dbService.GetResearches();
                await Dispatcher.DispatchAsync(() => listView.ItemsSource = researches);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Error loading data: {ex.Message}", "OK");
            }
        }

        private async void saveButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (_editResearchId == 0)
                {
                    await _dbService.Create(new Research
                    {
                        Title = titleEntryField.Text,
                        Description = descriptionEntryField.Text,
                    });
                }
                else
                {
                    await _dbService.Update(new Research
                    {
                        Id = _editResearchId,
                        Title = titleEntryField.Text,
                        Description = descriptionEntryField.Text,
                    });
                    _editResearchId = 0;
                }

                titleEntryField.Text = string.Empty;
                descriptionEntryField.Text = string.Empty;

                await LoadData();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Error saving data: {ex.Message}", "OK");
            }
        }

        private async void listView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var research = (Research)e.Item;
            var action = await DisplayActionSheet("Action", "Cancel", null, "Edit", "Delete");
            switch (action)
            {
                case "Edit":
                    _editResearchId = research.Id;
                    titleEntryField.Text = research.Title;
                    descriptionEntryField.Text = research.Description;
                    break;
                case "Delete":
                    try
                    {
                        await _dbService.Delete(research);
                        await LoadData();
                    }
                    catch (Exception ex)
                    {
                        await DisplayAlert("Error", $"Error deleting data: {ex.Message}", "OK");
                    }
                    break;
            }
        }
    }
}