using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using demo1301.Helpers;
using demo1301.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace demo1301;

public partial class MenuWindow : Window
{
    public bool IsAdmin = false;
    public class ServicePresenter : Service
    {
        public Bitmap? ServicePhoto
        {
            get
            {
                if(ChangeImage != null)
                {
                    return ChangeImage;
                }
                if (!string.IsNullOrEmpty(this.Mainimagepath))
                {
                    return ImageHelper.LoadFromResource(new Uri($"avares://demo1301/Assets/{this.Mainimagepath}"));
                }
                return null;
            }
        }
        public string ServiceTitle { get => this.Title; }
        public string ServiceCost { get => string.Format("{0} ������", this.Cost); }
        public string ServiceDuration { get => string.Format("�� {0} ������",this.Durationinseconds); }
        public string ServiceDiscount { get => string.Format("{0}%", this.Discount); }
        public string? ServiceDescription { get=>this.Description; }
        public float? DiscountAsFloat { get => this.Discount*100; }
        public decimal CostAsDecimal { get=>this.Cost; }
        public Bitmap ChangeImage { get; set; } = null;

    }
    public List<ServicePresenter> _services { get; set; }
    public ObservableCollection<ServicePresenter> _servisecDisplay { get; set; }
    private string searchWord;
    private int sortIndex;
    private ServicePresenter selectedService;
    private int filterIndex;
    private string[] sortValues = ["���","��������� �� ����.", "��������� �� ����."];
    private string[] filterValues = ["���","0-5%", "5-15%","15-30%","30-70%","70-100%"];

    public MenuWindow()
    {

        InitializeComponent();

    }
    public MenuWindow(bool isAdmin)
    {

        InitializeComponent();
        IsAdmin = isAdmin;
        if(IsAdmin == false)
        {
            AddButton.IsVisible = false;
        }

        using (var dbContext = new User3Context())
        {
            _services = dbContext.Services.Select(service => new ServicePresenter
            {
                Mainimagepath = service.Mainimagepath,
                Title = service.Title,
                Cost = service.Cost,
                Durationinseconds = service.Durationinseconds,
                Discount = service.Discount,

            }).ToList();
        }

        _servisecDisplay = new ObservableCollection<ServicePresenter>(_services);
        ServiceListBox.ItemsSource = _servisecDisplay;
        SortComboBox.ItemsSource = sortValues;
        FilterComboBox.ItemsSource = filterValues;
        StatisticTextBlock.Text = string.Format("�������� {0} �� {1}", _services.Count, _services.Count);
    }

    private void DisplayService()
    {
        List<ServicePresenter>displayServiceList = new List<ServicePresenter>(_services);

        if (!string.IsNullOrEmpty(searchWord))
        {
            displayServiceList = displayServiceList.Where(service => service.Title.ToLower().Contains(searchWord.ToLower())).ToList();
        }

        switch (filterIndex)
        {
            case 1:
                displayServiceList = displayServiceList.Where(service=> service.DiscountAsFloat>=0&&service.DiscountAsFloat<5).ToList();
                break;
            case 2:
                displayServiceList = displayServiceList.Where(service => service.DiscountAsFloat >= 5 && service.DiscountAsFloat < 15).ToList();
                break;
            case 3:
                displayServiceList = displayServiceList.Where(service => service.DiscountAsFloat >= 15 && service.DiscountAsFloat < 30).ToList();
                break;
            case 4:
                displayServiceList = displayServiceList.Where(service => service.DiscountAsFloat >= 30 && service.DiscountAsFloat < 70).ToList();
                break;
            case 5:
                displayServiceList = displayServiceList.Where(service => service.DiscountAsFloat >= 70 && service.DiscountAsFloat < 100).ToList();
                break;
        }

        switch (sortIndex)
        {
            case 1:
                displayServiceList=displayServiceList.OrderByDescending(serivce=>serivce.CostAsDecimal).ToList();
                break;
            case 2:
                displayServiceList = displayServiceList.OrderBy(serivce => serivce.CostAsDecimal).ToList();
                break;
        }


        if(_servisecDisplay != null)
        {
            _servisecDisplay.Clear();
        }
        foreach(var service in displayServiceList)
        {
            _servisecDisplay.Add(service);
        }
        StatisticTextBlock.Text=string.Format("�������� {0} �� {1}",displayServiceList.Count,_services.Count);
    }

    public void SearchingTextBox(object? sender, TextChangedEventArgs e)
    {
        searchWord = (sender as TextBox).Text;
        DisplayService();
    }

    private void SortChanging(object? sender, SelectionChangedEventArgs e)
    {
        sortIndex = (sender as ComboBox).SelectedIndex;
        DisplayService();
    } 
    private void FilterChanging(object? sender, SelectionChangedEventArgs e)
    {
        filterIndex = (sender as ComboBox).SelectedIndex;
        DisplayService();
    }
    private void SelectedServiceListBox(object? sender, SelectionChangedEventArgs e)
    {
        selectedService = (ServicePresenter)(sender as ListBox).SelectedItem;
        if (IsAdmin == true) 
        {
            DeleteButton.IsVisible = true;
            EditButton.IsVisible = true;
        }
    }
    private void DeletingService(object? sender, RoutedEventArgs e)
    {
            _services.Remove(selectedService);
            DisplayService();
        
    }
    private async void EditService(object? sender, RoutedEventArgs e)
    {
        var editWindow = new AddOrEditWindow(selectedService);

        var result = await editWindow.ShowDialog<ServicePresenter>(this);
        if (result != null)
        {
            selectedService.Title = result.Title;
            selectedService.Cost = result.Cost;
            selectedService.Description = result.Description;
            selectedService.Cost = result.Cost;

            DisplayService();
        }
    }

    private async void AddService(object? sender, RoutedEventArgs e)
    {
        var addWindow = new AddOrEditWindow();

        var result = await addWindow.ShowDialog<ServicePresenter>(this);
        if (result != null)
        {
            _services.Add(result);
            DisplayService();
        }
    }

}