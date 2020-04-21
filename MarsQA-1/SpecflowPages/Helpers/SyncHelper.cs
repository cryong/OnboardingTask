using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
namespace MarsQA_1.Helpers
{
    public static class SyncHelper
    {
        public static void ClickElement(this IWebDriver driver, By locator, int seconds = 0)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(seconds))
            {
                Message = "Unable to wait for an element " + locator + " to be clickable"
            };
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(locator)).Click();
        }

        public static IWebElement FindElement(this IWebDriver driver, By locator, int seconds = 0)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(seconds))
            {
                Message = "Unable to wait for an element " + locator
            };
            //SeleniumExtras.WaitHelpers.ExpectedConditions.
            return wait.Until(ExpectedConditions.ElementIsVisible(locator));
        }

        public static void EnterField(this IWebDriver driver, By locator, string text, int seconds = 0)
        {
            FindElement(driver, locator, seconds).SendKeys(text);
        }

        public static void SelectOptionByValue(this IWebDriver driver, By locator, string value, int seconds = 0)
        {
            var dropdown = new SelectElement(FindElement(driver, locator, seconds));
            dropdown.SelectByValue(value);
        }
    }
}