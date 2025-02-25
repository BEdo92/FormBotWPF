using FormBotWPF.Models;
using System.IO;
using System.Text.Json;

namespace FormBotWPF;

/// <summary>
/// Provides methods to fill the form in the browser, or handle form data at the desktop UI of this app.
/// </summary>
class PromService
{
    private XPathModel _XPaths;
    private UIService? _uiService;

    public PromService()
    {
        _XPaths = GetXPaths();
    }

    /// <summary>
    /// Fills the form with data, so the current prom code can be sent. First time, it opens the browser, later
    /// it uses the existing instance of it.
    /// </summary>
    /// <param name="promData"></param>
    /// <exception cref="Exception"></exception>
    internal void Fill(PromModel promData)
    {
        try
        {
            _uiService ??= new UIService(_XPaths.URL);

            _uiService.ClickButton(_XPaths.NavigationButton);

            Thread.Sleep(500);

            _uiService.SetText(_XPaths.LastName, promData.LastName);
            _uiService.SetText(_XPaths.FirstName, promData.FirstName);
            _uiService.SetText(_XPaths.Phone, promData.Phone);
            _uiService.SetText(_XPaths.Mail, promData.Mail);
            _uiService.SetText(_XPaths.MailRepeat, promData.Mail);
            _uiService.SetText(_XPaths.PromCode, promData.PromCode);

            Thread.Sleep(1_000);

            _uiService.SetCheckbox(_XPaths.AcceptPrivacyPolicyInput);
            _uiService.SetCheckbox(_XPaths.AcceptRulesInput);
        }
        catch (Exception e)
        {
            throw new Exception("Error occured while filling the form.", e);
        }
    }

    /// <summary>
    /// Saved the data of the desktop form to a file.
    /// </summary>
    internal void SaveFormData(PromModel promModel)
    {
        try
        {
            string json = JsonSerializer.Serialize(promModel);
            File.WriteAllText("FormData.Json", json);
        }
        catch (Exception e)
        {
            throw new Exception("Could not save the data of the form to file!", e);
        }
    }

    /// <summary>
    /// Loads the previously saved data.
    /// </summary>
    internal PromModel? LoadFormData()
    {
        try
        {
            string json = File.ReadAllText("FormData.Json");
            PromModel promModel = JsonSerializer.Deserialize<PromModel>(json);
            return promModel;
        }
        catch (Exception e)
        {
            throw new Exception("Could not save the data of the form to file!", e);
        }
    }

    private static XPathModel GetXPaths()
    {
        try
        {
            string json = File.ReadAllText(@"Configs\XPaths.json");
            return JsonSerializer.Deserialize<XPathModel>(json);
        }
        catch (Exception e)
        {
            throw new Exception("Cannot get the XPaths.", e);
        }
    }
}
