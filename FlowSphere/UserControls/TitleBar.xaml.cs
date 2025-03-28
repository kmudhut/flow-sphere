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

    // private void DragMoveParentWindow(object sender, MouseButtonEventArgs e)
    // {
    //     Window? parentWindow = Window.GetWindow(this);
    //     parentWindow?.DragMove();
    // }
    //
    // private void MaximizeParentWindowIfDoubleClicked(object sender, MouseButtonEventArgs e)
    // {
    //     if (e.ClickCount == 2)
    //     {
    //         MaximizeParentWindow(sender, e);
    //     }
    // }

    private void TitleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        Window? parentWindow = Window.GetWindow(this);
        if (parentWindow == null) return;

        if (e.ClickCount == 2)
        {
            ToggleMaximize();
            return;
        }

        if (parentWindow.WindowState == WindowState.Maximized)
        {
            Point mousePosition = PointToScreen(e.GetPosition(this));
            
            double previousWidth = parentWindow.RestoreBounds.Width;
            double previousHeight = parentWindow.RestoreBounds.Height;
            
            double ratioX = mousePosition.X / parentWindow.ActualWidth;
            
            parentWindow.WindowState = WindowState.Normal;
            
            parentWindow.Dispatcher.BeginInvoke(new Action(() =>
            {
                parentWindow.Width = previousWidth;
                parentWindow.Height = previousHeight;
                
                parentWindow.Left = mousePosition.X - (parentWindow.Width * ratioX);
                parentWindow.Top = mousePosition.Y - 10; // Małe przesunięcie w dół dla lepszego UX
                
                parentWindow.DragMove();
            }));
        }
        else
        {
            parentWindow.DragMove();
        }
    }

    private void ToggleMaximize()
    {
        Window? parentWindow = Window.GetWindow(this);
        if (parentWindow == null) return;

        parentWindow.WindowState = (parentWindow.WindowState == WindowState.Maximized)
            ? WindowState.Normal
            : WindowState.Maximized;
    }
}
