using System.Windows.Controls;
using FlowSphere.ViewModels;

namespace FlowSphere.Views;

public partial class MainView : UserControl
{
    public MainView()
    {
        MainViewModel vm = new MainViewModel();
        this.DataContext = vm;
        InitializeComponent();
    }
}