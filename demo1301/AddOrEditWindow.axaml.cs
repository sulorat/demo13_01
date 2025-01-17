using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using demo1301.Models;
using System.Collections.Generic;
using static demo1301.MenuWindow;

namespace demo1301;

public partial class AddOrEditWindow : Window
{

    public ServicePresenter Service = new ServicePresenter();
   
    public AddOrEditWindow()
    {
        InitializeComponent();

      
        titleOfWindow.Text = "Окно добавления";
        SaveButton.Content = "Добавить";
    }

    public AddOrEditWindow(ServicePresenter? service)
    {
        InitializeComponent();
        Service = service;
        titleOfWindow.Text = "Окно Редактирования";
        PhotoService.Source = service.ServicePhoto;
        TitleTextBox.Text = service.Title;
        DiscountTextBox.Text = service.Discount.ToString();
        DurationTextBox.Text = service.ServiceDuration.ToString();
        DescriptionTextBox.Text = service.Description;
        CostTextBox.Text = service.Cost.ToString();
    }
    private void SaveButton_Click(object? sender, RoutedEventArgs e)
    {
        Service.Title = TitleTextBox.Text;
        if (decimal.TryParse(CostTextBox.Text, out decimal cost))
        {
            Service.Cost = cost;
        }
        if (float.TryParse(DiscountTextBox.Text, out float discount))
        {
            Service.Discount = discount;
        }
        Service.Description = DescriptionTextBox.Text;
        if (int.TryParse(DurationTextBox.Text, out int duration))
        {
            Service.Durationinseconds = duration;
        }
        this.Close(Service);
    }


    private async void SelectImageButton_Click(object? sender, RoutedEventArgs e)
    {
        var dialog = new OpenFileDialog
        {
            Title = "Choose Product Image",
            Filters = new List<FileDialogFilter>
            {
                new FileDialogFilter { Name = "Image Files", Extensions = { "png", "jpg", "jpeg" } }
            }
        };
        string[] result = await dialog.ShowAsync(this);

        if (result != null && result.Length > 0)
        {
            Service.ChangeImage = new Bitmap(result[0]);
        }
    }

    


}