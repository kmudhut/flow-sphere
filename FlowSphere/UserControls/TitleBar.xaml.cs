using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FlowSphere.UserControls;

public partial class TitleBar : UserControl
{
    public TitleBar()
    {
        InitializeComponent();
    }

    private void CloseParentWindow(object sender, RoutedEventArgs e)
    {
        Window? parentWindow = Window.GetWindow(this);
        parentWindow?.Close();
    }
    
    private void MinimizeParentWindow(object sender, RoutedEventArgs e)
    {
        Window? parentWindow = Window.GetWindow(this);
        parentWindow.WindowState = WindowState.Minimized;
    }
    private void MaximizeParentWindow(object sender, RoutedEventArgs e)
    {
        Window? parentWindow = Window.GetWindow(this);
        if (parentWindow?.WindowState == WindowState.Maximized)
        {
            parentWindow.WindowState = WindowState.Normal;
        }
        else
        {
            parentWindow.WindowState = WindowState.Maximized;
        }
    }

    private void DragMoveParentWindow(object sender, MouseButtonEventArgs e)
    {
        Window? parentWindow = Window.GetWindow(this);
        parentWindow?.DragMove();
    }
    private void MaximizeParentWindowIfDoubleClicked(object sender, MouseButtonEventArgs e)
    {
        if (e.ClickCount == 2) 
        {
            MaximizeParentWindow(sender,e);
        }
    }
}