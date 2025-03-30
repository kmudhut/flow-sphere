using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;

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
        Point mouseScreenPos = PointToScreen(e.GetPosition(this));
        
        Rect workArea = GetScreenWorkAreaFromPoint(mouseScreenPos);
        double relativeX = (mouseScreenPos.X - workArea.Left) / workArea.Width;
        
        parentWindow.WindowState = WindowState.Normal;
        
        parentWindow.Dispatcher.BeginInvoke(new Action(() =>
        {
            parentWindow.Width = parentWindow.RestoreBounds.Width;
            parentWindow.Height = parentWindow.RestoreBounds.Height;
            
            double newLeft = mouseScreenPos.X - (parentWindow.Width * relativeX);
            double newTop = mouseScreenPos.Y - 10; 
            newLeft = Math.Max(workArea.Left, Math.Min(newLeft, workArea.Right - parentWindow.Width));
            newTop = Math.Max(workArea.Top, Math.Min(newTop, workArea.Bottom - parentWindow.Height));
            
            parentWindow.Left = newLeft;
            parentWindow.Top = newTop;
            
            parentWindow.DragMove();
        }));
    }
    else
    {
        parentWindow.DragMove();
    }
}

private static Rect GetScreenWorkAreaFromPoint(Point point)
{
    var monitor = MonitorFromPoint(
        new Win32Point { X = (int)point.X, Y = (int)point.Y },
        MonitorDefaultToNearest);

    var info = new MONITORINFO();
    info.cbSize = Marshal.SizeOf(info);
    GetMonitorInfo(monitor, ref info);
    
    return new Rect(
        info.rcWork.Left,
        info.rcWork.Top,
        info.rcWork.Right - info.rcWork.Left,
        info.rcWork.Bottom - info.rcWork.Top);
}

private const int MonitorDefaultToNearest = 2;

[StructLayout(LayoutKind.Sequential)]
private struct Win32Point
{
    public int X;
    public int Y;
}

[StructLayout(LayoutKind.Sequential)]
private struct RECT
{
    public int Left;
    public int Top;
    public int Right;
    public int Bottom;
}

[StructLayout(LayoutKind.Sequential)]
private struct MONITORINFO
{
    public int cbSize;
    public RECT rcMonitor;
    public RECT rcWork;
    public int dwFlags;
}

[DllImport("user32.dll")]
private static extern IntPtr MonitorFromPoint(Win32Point pt, uint flags);

[DllImport("user32.dll")]
private static extern bool GetMonitorInfo(IntPtr hMonitor, ref MONITORINFO lpmi);

    private void ToggleMaximize()
    {
        Window? parentWindow = Window.GetWindow(this);
        if (parentWindow == null) return;

        parentWindow.WindowState = (parentWindow.WindowState == WindowState.Maximized)
            ? WindowState.Normal
            : WindowState.Maximized;
    }
}
