using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System;
using System.Windows;
using System.Windows.Interop;
using System.Runtime.InteropServices;
using FlowSphere.ViewModels;

namespace FlowSphere;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{

    public MainWindow()
    {
        MainWindowViewModel vm = new MainWindowViewModel();
        this.DataContext = vm;
        InitializeComponent();
        
    }
    
    private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        if (this.WindowState == WindowState.Maximized)
        {
            this.BorderThickness = new System.Windows.Thickness(8);
        }
        else
        {
            this.BorderThickness = new System.Windows.Thickness(0);
        }
    }
    
    // private void AnimatePlaceholder(double from, double to, double opacity)
    // {
    //     var anim = new DoubleAnimation(from, to, TimeSpan.FromSeconds(0.2));
    //     EmailPlaceholder.RenderTransform.BeginAnimation(TranslateTransform.YProperty, anim);
    //
    //     var fadeAnim = new DoubleAnimation(opacity, TimeSpan.FromSeconds(0.2));
    //     EmailPlaceholder.BeginAnimation(OpacityProperty, fadeAnim);
    // }
}