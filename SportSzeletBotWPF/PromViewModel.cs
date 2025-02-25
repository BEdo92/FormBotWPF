using FormBotWPF.Models;
using System.Windows;

namespace FormBotWPF;

class PromViewModel : BindableBase
{
    private string _webPage;
    private string _lastName;
    private string _firstName;
    private string _phone;
    private string _mail;
    private string _mail2;
    private string _promCodes;
    private PromService _service;

    public PromViewModel()
    {
        FillFormCommand = new DelegateCommand<object>(Send, CanSend)
            .ObservesProperty(() => WebPage)
            .ObservesProperty(() => LastName)
            .ObservesProperty(() => FirstName)
            .ObservesProperty(() => Phone)
            .ObservesProperty(() => Mail)
            .ObservesProperty(() => Mail2)
            .ObservesProperty(() => PromCode);

        SaveFormDataCommand = new DelegateCommand<object>(SaveFormData);
        LoadFormDataCommand = new DelegateCommand<object>(LoadFormData);

        _service = new PromService();
    }

    public DelegateCommand<object> FillFormCommand { get; private set; }

    public DelegateCommand<object> SaveFormDataCommand { get; private set; }

    public DelegateCommand<object> LoadFormDataCommand { get; private set; }

    public string WebPage
    {
        get => _webPage;
        set => SetProperty(ref _webPage, value);
    }

    public string LastName
    {
        get => _lastName;
        set => SetProperty(ref _lastName, value);
    }

    public string FirstName
    {
        get => _firstName;
        set => SetProperty(ref _firstName, value);
    }

    public string Phone
    {
        get => _phone;
        set => SetProperty(ref _phone, value);
    }

    public string Mail
    {
        get => _mail;
        set => SetProperty(ref _mail, value);
    }

    public string Mail2
    {
        get => _mail2;
        set => SetProperty(ref _mail2, value);
    }

    public string PromCode
    {
        get => _promCodes;
        set => SetProperty(ref _promCodes, value);
    }

    private void Send(object parameter)
    {
        _service.Fill(new PromModel
        {
            FirstName = FirstName,
            LastName = LastName,
            Mail = Mail,
            Phone = Phone,
            PromCode = PromCode
        });
    }

    private bool CanSend(object parameter)
    {
        return !string.IsNullOrWhiteSpace(LastName)
            && !string.IsNullOrWhiteSpace(FirstName)
            && !string.IsNullOrWhiteSpace(Phone)
            && !string.IsNullOrWhiteSpace(Mail)
            && !string.IsNullOrWhiteSpace(Mail2)
            && Mail.Equals(Mail2)
            && !string.IsNullOrWhiteSpace(PromCode);
    }

    private void SaveFormData(object parameter)
    {
        _service.SaveFormData(new PromModel
        {
            FirstName = FirstName,
            LastName = LastName,
            Mail = Mail,
            Phone = Phone,
            PromCode = PromCode
        });
    }

    private void LoadFormData(object parameter)
    {
        try
        {
            PromModel? data = _service.LoadFormData();
            FirstName = data.FirstName;
            LastName = data.LastName;
            Mail = data.Mail;
            Mail2 = data.Mail;
            Phone = data.Phone;
            PromCode = data.PromCode;
        }
        catch (Exception e)
        {
            // TODO: Find a better solution for this.
            MessageBox.Show(e.Message);
        }
    }
}
