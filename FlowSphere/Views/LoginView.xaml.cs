using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using FlowSphere.ViewModels;

namespace FlowSphere.Views;

public partial class LoginView : UserControl
{
    public LoginView(MainWindowViewModel mainVM)
    {
        LoginViewModel vm = new LoginViewModel(mainVM);
        DataContext = vm;
        InitializeComponent();
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