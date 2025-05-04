using System.Net;
using System.Security;
using System.Security.Principal;
using System.Windows.Input;
using FlowSphere.MVVM;
using System.Text.RegularExpressions;
using System.Windows;
using FlowSphere.Models;
using FlowSphere.Repositories;
using FlowSphere.Views;

namespace FlowSphere.ViewModels;

public class LoginViewModel : ViewModelBase
{
    private MainWindowViewModel _mainVM;
    
    private string _email;
    private string _password;
    private string _emailErrorMessage;
    private string _passwordErrorMessage;
    private IUserRepository _userRepository;
    public string Email
    {
        get => _email;
        set
        {
            if (value == _email) return;
            _email = value ?? throw new ArgumentNullException(nameof(value));
            EmailErrorMessage = "";
            OnPropertyChanged();
        }
    }

    public string Password
    {
        get => _password;
        set
        {
            if (Equals(value, _password)) return;
            _password = value ?? throw new ArgumentNullException(nameof(value));
            PasswordErrorMessage = "";
            OnPropertyChanged();
        }
    }

    public string EmailErrorMessage
    {
        get => _emailErrorMessage;
        set
        {
            if (value == _emailErrorMessage) return;
            _emailErrorMessage = value ?? throw new ArgumentNullException(nameof(value));
            OnPropertyChanged();
        }
    }

    public string PasswordErrorMessage
    {
        get => _passwordErrorMessage;
        set
        {
            if (value == _passwordErrorMessage) return;
            _passwordErrorMessage = value ?? throw new ArgumentNullException(nameof(value));
            OnPropertyChanged();
        }
    }
    
    public ICommand LoginCommand { get; set; }

    public LoginViewModel(MainWindowViewModel mainVM)
    {
        _mainVM = mainVM;
        _userRepository = new UserRepository();
        LoginCommand = new RelayCommand(ExecuteLoginCommand, CanExecuteLoginCommand);
    }

    private bool CanExecuteLoginCommand(object arg)
    {
        bool validEMail = !string.IsNullOrEmpty(Email) && 
                          Regex.IsMatch(Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");;
        bool validPassword = Password?.Length >= 8 &&
                             Password?.Length < 128 &&
                             Regex.IsMatch(Password, @"[a-z]") &&
                             Regex.IsMatch(Password, @"[A-Z]") &&
                             Regex.IsMatch(Password, @"\d") &&
                             Regex.IsMatch(Password, @"[\W_]");
        
        return validEMail && validPassword;
    }

    private async void ExecuteLoginCommand(object obj)
    {
        try
        {
            var isUserValid =  await _userRepository.AuthenticateUser(new NetworkCredential(Email, Password));
            
            if (isUserValid == 1) EmailErrorMessage = "Nie odnaleziono takiego adresu e-mail.";
            else if (isUserValid == 2) PasswordErrorMessage = "Niepoprawne hasło.";
            else
            {
                Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(Email), null);
                _mainVM.SwitchToMainView();
            }
        }
        catch (Exception ex)
        {
            EmailErrorMessage = "Połączenie z bazą nie pykło";
        }
    }
}