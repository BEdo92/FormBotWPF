using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace FormBotWPF;

/// <summary>
/// Represents the operations that can be performed on the UI (in the specified web page at the Chrome browser).
/// </summary>
class UIService
{
    private IWebDriver? _driver;

    /// <summary>
    /// Creates an instance of the UIService class and opens the Chrome browser at a specified URL.
    /// </summary>
    /// <param name="url">The URL were the browser will open.</param>
    public UIService(string url)
    {
        OpenBrowser(url);
    }

    /// <summary>
    /// Clicks the button at the specified path or id.
    /// </summary>
    public void ClickButton(string xpath)
    {
        if (xpath.StartsWith("/html/"))
        {
            _driver?.FindElement(By.XPath(xpath)).Click();
        }
        else
        {
            _driver?.FindElement(By.Id(xpath)).Click();
        }
    }

    /// <summary>
    /// Sets the specified text to the specified input.
    /// </summary>
    public void SetText(string xpath, string text)
    {
        if (xpath.StartsWith("/html/"))
        {
            _driver?.FindElement(By.XPath(xpath)).SendKeys(text);
        }
        else
        {
            _driver?.FindElement(By.Id(xpath)).SendKeys(text);
        }
    }

    /// <summary>
    /// Checks the specified check box.
    /// </summary>
    public void SetCheckbox(string xpath)
    {
        if (xpath.StartsWith("/html/"))
        {
            // NOTE: The label of the check boxes contain URLs and Selenium finds that.
            _driver?.FindElement(By.XPath(xpath)).Click();
        }
        else
        {
            IWebElement checkbox = _driver?.FindElement(By.Id(xpath));
            IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
            js.ExecuteScript("arguments[0].click();", checkbox);
        }
    }

    private void OpenBrowser(string url)
    {
        try
        {
            ChromeOptions co = new();
            _driver = new ChromeDriver(co);
            _driver.Manage().Window.Maximize();
            _driver.Navigate().GoToUrl(url);
        }
        catch (Exception e)
        {
            throw new Exception("The browser could not open.", e);
        }
    }
}
