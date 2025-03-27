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

namespace FlowSphere;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window, INotifyPropertyChanged
{

    public MainWindow()
    {
        DataContext = this;
        InitializeComponent();
        HeaderText = "Logowanie";
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
    private string _headerText;

    public string HeaderText
    {
        get { return _headerText; }
        set
        {
            _headerText = value;
            OnPropertyChanged();
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    private void MainWindow_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        this.DragMove();
    }
    private void AnimatePlaceholder(double from, double to, double opacity)
    {
        var anim = new DoubleAnimation(from, to, TimeSpan.FromSeconds(0.2));
        EmailPlaceholder.RenderTransform.BeginAnimation(TranslateTransform.YProperty, anim);

        var fadeAnim = new DoubleAnimation(opacity, TimeSpan.FromSeconds(0.2));
        EmailPlaceholder.BeginAnimation(OpacityProperty, fadeAnim);
    }

    private void TextBoxInput_GotFocus(object sender, RoutedEventArgs e)
    {
        if (sender is TextBox textBox || sender is PasswordBox passwordBox)
        {
            var elem = sender as FrameworkElement;

            if (elem != null && string.IsNullOrEmpty((elem as TextBox)?.Text ?? (elem as PasswordBox)?.Password))
            {
                var parent = VisualTreeHelper.GetParent(elem) as Grid;

                if (parent != null)
                {
                    foreach (var child in parent.Children)
                    {
                        if (child is TextBlock textBlock && child != elem)
                        { 
                            textBlock.RenderTransform.BeginAnimation(TranslateTransform.YProperty,
                                new DoubleAnimation(0, -22, TimeSpan.FromSeconds(0.2)));
                        }
                    }
                }
            }
        }
    }
    


    private void TextBoxInput_LostFocus(object sender, RoutedEventArgs e)
    {
        if (sender is TextBox textBox || sender is PasswordBox passwordBox)
        {
            var elem = sender as FrameworkElement;

            if (elem != null && string.IsNullOrEmpty((elem as TextBox)?.Text ?? (elem as PasswordBox)?.Password))
            {
                var parent = VisualTreeHelper.GetParent(elem) as Grid;
                
                if (parent != null)
                {
                    foreach (var child in parent.Children)
                    {
                        if (child is TextBlock textBlock && child != elem)
                        {
                            textBlock.RenderTransform.BeginAnimation(TranslateTransform.YProperty,
                                new DoubleAnimation(-22,0, TimeSpan.FromSeconds(0.2)));
                        }
                    }
                }
            }
        }
    }
}