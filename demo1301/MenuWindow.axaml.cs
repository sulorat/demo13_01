using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using demo1301.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace demo1301;

public partial class MenuWindow : Window
{
    public class ServicePresenter : Service
    {
        public string ServiceTitle { get => this.Title; }
        public string ServiceCost { get => string.Format("{0} рублей", this.Cost); }
        public string ServiceDuration { get => string.Format("за {0} секунд",this.Durationinseconds); }
        public string ServiceDiscount { get => string.Format("{0}%", this.Discount); }
        public float? DiscountAsFloat { get => this.Discount*100; }
        public decimal CostAsDecimal { get=>this.Cost; } 

    }
    public List<ServicePresenter> _services { get; set; }
    public ObservableCollection<ServicePresenter> _servisecDisplay { get; set; }
    private string searchWord;
    private int sortIndex;
    private ServicePresenter selectedService;
    private int filterIndex;
    private string[] sortValues = ["Все","Стоимость по убыв.", "Стоимость по возр."];
    private string[] filterValues = ["Все","0-5%", "5-15%","15-30%","30-70%","70-100%"];

    public MenuWindow()
    {

        InitializeComponent();

        using(var dbContext = new User3Context())
        {
            _services = dbContext.Services.Select(service=>new ServicePresenter
            {
                Title= service.Title,
                Cost= service.Cost,
                Durationinseconds= service.Durationinseconds,
                Discount= service.Discount,

            }).ToList();
        }
        _servisecDisplay = new ObservableCollection<ServicePresenter>(_services);
        ServiceListBox.ItemsSource = _servisecDisplay;
        SortComboBox.ItemsSource=sortValues;
        FilterComboBox.ItemsSource=filterValues;
        StatisticTextBlock.Text = string.Format("Показано {0} из {1}", _services.Count, _services.Count);
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
        StatisticTextBlock.Text=string.Format("Показано {0} из {1}",displayServiceList.Count,_services.Count);
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
        DeleteButton.IsVisible = true;
    }
    private void DeletingService(object? sender, RoutedEventArgs e)
    {
        _services.Remove(selectedService);
        DisplayService();
    }

}