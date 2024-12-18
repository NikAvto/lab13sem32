using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Maui.Controls;

namespace lab13sem32
{
    public partial class ScientistPage : ContentPage
    {
        private readonly LocalDbService _dbService;
        private int _editScientistsId;

        public ScientistPage(LocalDbService dbService)
        {
            InitializeComponent();
            _dbService = dbService;
            LoadData();
        }

        private async Task LoadData()
        {
            try
            {
                var scien = await _dbService.GetScientists();
                await Dispatcher.DispatchAsync(() => list2View.ItemsSource = scien);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Error loading data: {ex.Message}", "OK");
            }
        }

        private async void save2Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (_editScientistsId == 0)
                {
                    await _dbService.CreateS(new Scientists
                    {
                        Name = name2EntryField.Text,
                        FieldOfStudy = fieldsofstudyEntryField.Text,
                    });
                }
                else
                {
                    await _dbService.UpdateS(new Scientists
                    {
                        Id = _editScientistsId,
                        Name = name2EntryField.Text,
                        FieldOfStudy = fieldsofstudyEntryField.Text,
                    });
                    _editScientistsId = 0;
                }

                name2EntryField.Text = string.Empty;
                fieldsofstudyEntryField.Text = string.Empty;

                await LoadData();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Error saving data: {ex.Message}", "OK");
            }
        }

        private async void list2View_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var scientist = (Scientists)e.Item;
            var action = await DisplayActionSheet("Action", "Cancel", null, "Edit", "Delete");
            switch (action)
            {
                case "Edit":
                    _editScientistsId = scientist.Id;
                    name2EntryField.Text = scientist.Name;
                    fieldsofstudyEntryField.Text = scientist.FieldOfStudy;
                    break;
                case "Delete":
                    try
                    {
                        await _dbService.DeleteSc(scientist);
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