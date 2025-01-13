using Avalonia;
using Avalonia.Controls;
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
        public decimal ServiceCost {  get => this.Cost; }
        public int ServiceDuration { get => this.Durationinseconds/60; }
        public float? ServiceDiscount { get=> this.Discount; }

    }
    public List<ServicePresenter> _services { get; set; }
    public ObservableCollection<ServicePresenter> _servisecDisplay { get; set; }

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
        ServiceListBox.ItemsSource = _services;

    }

}