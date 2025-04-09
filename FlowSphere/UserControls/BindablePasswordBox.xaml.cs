using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace FlowSphere.UserControls;

public partial class BindablePasswordBox : UserControl
{
    public static readonly DependencyProperty PasswordProperty = DependencyProperty.Register("Password", typeof(string), typeof(BindablePasswordBox));
    
    public string Password
    {
        get { return (string)GetValue(PasswordProperty); }
        set { SetValue(PasswordProperty, value); }
    }

    public BindablePasswordBox()
    {
        InitializeComponent();
        PasswordTextBox.PasswordChanged += OnPasswordChanged;
    }

    private void OnPasswordChanged(object sender, RoutedEventArgs e)
    {
        Password = PasswordTextBox.Password;
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