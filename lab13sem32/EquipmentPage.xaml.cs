using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Maui.Controls;

namespace lab13sem32
{
    public partial class EquipmentPage : ContentPage
    {
        private readonly LocalDbService _dbService;
        private int _editEquipmentId;

        public EquipmentPage(LocalDbService dbService)
        {
            InitializeComponent();
            _dbService = dbService;
            LoadData();
        }

        private async Task LoadData()
        {
            try
            {
                var equipment = await _dbService.GetEquipment();
                await Dispatcher.DispatchAsync(() => list1View.ItemsSource = equipment);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Error loading data: {ex.Message}", "OK");
            }
        }

        private async void save1Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (_editEquipmentId == 0)
                {
                    await _dbService.CreateE(new Equipment
                    {
                        Name = name1EntryField.Text,
                        Type = type1EntryField.Text,
                    });
                }
                else
                {
                    await _dbService.UpdateE(new Equipment
                    {
                        Id = _editEquipmentId,
                        Name = name1EntryField.Text,
                        Type = type1EntryField.Text,
                    });
                    _editEquipmentId = 0;
                }

                name1EntryField.Text = string.Empty;
                type1EntryField.Text = string.Empty;

                await LoadData();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Error saving data: {ex.Message}", "OK");
            }
        }

        private async void list1View_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var equipment = (Equipment)e.Item;
            var action = await DisplayActionSheet("Action", "Cancel", null, "Edit", "Delete");
            switch (action)
            {
                case "Edit":
                    _editEquipmentId = equipment.Id;
                    name1EntryField.Text = equipment.Name;
                    type1EntryField.Text = equipment.Type;
                    break;
                case "Delete":
                    try
                    {
                        await _dbService.DeleteE(equipment);
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