using System;
using System.Collections.Generic;
using System.Linq;
using MarsQA_1.Helpers;
using MarsQA_1.SpecflowTests.DataObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace MarsQA_1.SpecflowPages.Pages
{
    public class ProfilePage
    {
        // tab butons
        private readonly By languagesTab = By.XPath("//form/div[contains(@class, 'menu')]/a[.='Languages']");
        private readonly By skillsTab = By.XPath("//form/div[contains(@class, 'menu')]/a[.='Skills']");
        private readonly By educationTab = By.XPath("//form/div[contains(@class, 'menu')]/a[.='Education']");
        private readonly By certificationTab = By.XPath("//form/div[contains(@class, 'menu')]/a[.='Certifications']");

        private readonly By addNewButton = By.XPath("//div[contains(@class, 'active')]/div/div/div/table/thead/tr/th/div[. = 'Add New']"); // common
        private readonly By saveButton = By.XPath("//form/div[contains(@class, 'active')]/div/div/div[@class='form-wrapper']/div/descendant::*/input[@type='button' and @value='Add']");

        // languages tab
        private readonly By languageNameField = By.XPath("//form/div[contains(@class, 'active')]/div/div/div[@class='form-wrapper']/div/div/input[@name='name']");
        private readonly By languageLevelField = By.XPath("//form/div[contains(@class, 'active')]/div/div/div[@class='form-wrapper']/div/div/select[@name='level']");

        // degrees tab
        private readonly By instituteNameField = By.Name("instituteName");
        private readonly By countryField = By.Name("country");
        private readonly By degreeTitleField = By.Name("title");
        private readonly By degreeNameField = By.Name("degree");
        private readonly By graudationYearField = By.Name("yearOfGraduation");

        // skills tab
        private readonly By skillNameField = By.XPath("//div[contains(@class,'active')]/div/div/div/div[@class='fields']/div/input[@name='name']");
        private readonly By skillLevelField = By.XPath("//div[contains(@class,'active')]/div/div/div/div[@class='fields']/div/select[@name='level']");

        // certification tab
        private readonly By certificationNameField = By.Name("certificationName");
        private readonly By certificationOrgField = By.Name("certificationFrom");
        private readonly By certificationYearField = By.Name("certificationYear");

        private readonly By successPopUp = By.XPath("//div[contains(@class, 'ns-type-success')]");
        private readonly By errorPopUp = By.XPath("//div[contains(@class, 'ns-type-error')]");

        // active table rows
        private readonly By tableDataRows = By.XPath("//div[contains(@class, 'active')]/div[@class='row']/div[contains(@class, 'scrollTable')]/div/table/tbody/tr");

        public enum SellerDetailType
        {
            Language,
            Skill,
            Education,
            Certification
        }

        public string getSuccessPopUpMessage(IWebDriver driver)
        {
            return driver.FindElement(successPopUp, 3).Text;
        }

        public string getErrorPopUpMessage(IWebDriver driver)
        {
            return driver.FindElement(errorPopUp, 3).Text;
        }

        public void ClickTab(IWebDriver driver, SellerDetailType text)
        {
            By location;
            switch (text)
            {
                case SellerDetailType.Language:
                    location = languagesTab;
                    break;
                case SellerDetailType.Skill:
                    location = skillsTab;
                    break;
                case SellerDetailType.Education:
                    location = educationTab;
                    break;
                case SellerDetailType.Certification:
                    location = certificationTab;
                    break;
                default:
                    throw new ArgumentException("Illegal argument was passed '" + text.ToString("G") + "'");
            }
            driver.ClickElement(location, 3);
        }


        public void SaveProfileDetail(IWebDriver driver, SearchableItem item)
        {
            if (item is Language language)
            {
                EnterLanguageDetails(driver, language);
            }
            else if (item is Skill skill)
            {
                EnterSkillDetails(driver, skill);
            }
            else if (item is Education education)
            {
                EnterEducationDetails(driver, education);
            }
            else if (item is Certification certification)
            {
                EnterCertificationDetails(driver, certification);
            }
            else
            {
                throw new ArgumentException("Unknown item type : " + item.GetType().ToString() + " with its values : " + item.ToString());
            }
        }

        public void SaveProfileDetails(IWebDriver driver, IEnumerable<SearchableItem> items)
        {
            foreach (var item in items)
            {
                SaveProfileDetail(driver, item);
            }
        }

        public void EnterLanguageDetails(IWebDriver driver, Language language)
        {
            ClickTab(driver, SellerDetailType.Language);
            driver.ClickElement(addNewButton, 3);
            driver.EnterField(languageNameField, language.Name, 3);
            driver.SelectOptionByValue(languageLevelField, language.Level, 3);
            // handle exception in the test if such option does not exist
            driver.ClickElement(saveButton, 3);
        }

        public void EnterEducationDetails(IWebDriver driver, Education education)
        {
            ClickTab(driver, SellerDetailType.Education);
            driver.ClickElement(addNewButton, 3);
            driver.EnterField(instituteNameField, education.InstituteName, 3);
            driver.SelectOptionByValue(countryField, education.Country, 3);
            driver.SelectOptionByValue(degreeTitleField, education.DegreeTitle, 3);
            driver.EnterField(degreeNameField, education.DegreeName, 3);
            driver.SelectOptionByValue(graudationYearField, education.YearOfGraduation, 3);
            // handle exception in the test if such option does not exist
            driver.ClickElement(saveButton, 3);
        }

        public void EnterSkillDetails(IWebDriver driver, Skill skill)
        {
            ClickTab(driver, SellerDetailType.Skill);
            driver.ClickElement(addNewButton, 3);
            driver.EnterField(skillNameField, skill.Name, 3);
            driver.SelectOptionByValue(skillLevelField, skill.Level, 3);
            driver.ClickElement(saveButton, 3);
        }

        public void EnterCertificationDetails(IWebDriver driver, Certification certification)
        {
            ClickTab(driver, SellerDetailType.Certification);
            driver.ClickElement(addNewButton, 3);
            driver.EnterField(certificationNameField, certification.Name, 3);
            driver.EnterField(certificationOrgField, certification.Organisation, 3);
            driver.SelectOptionByValue(certificationYearField,certification.Year, 3);
            driver.ClickElement(saveButton, 3);
        }

        public bool IsAddRowButtonEnbaled(IWebDriver driver)
        {
            try
            {
                if (driver.FindElement(saveButton, 1).Enabled)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool SearchForRow<T>(IWebDriver driver, T row) where T : SearchableItem
        {
            Type rowType = row.GetType();
            Enum.TryParse(rowType.Name, out SellerDetailType detailType);
            ClickTab(driver, detailType);

            foreach (var rowCandidate in driver.FindElements(tableDataRows))
            {
                // get all data cells except last one for buttons
                // order of the cells and constructor parameters are VERY CRUCIAL for this to work
                var cellData = rowCandidate.FindElements(By.XPath("td[position() <last()]"));
                var rowToCompare = rowType.GetConstructor(cellData.Select(i => typeof(string)).ToArray()).Invoke(cellData.Select(j => j.Text).ToArray());
                if (rowToCompare.Equals(row))
                {
                    return true;
                }
            }
            return false;
        }

        public void DeleteAllTableData(IWebDriver driver)
        {
            // refresh the page first
            driver.Navigate().Refresh();
            foreach (SellerDetailType detailType in Enum.GetValues(typeof(SellerDetailType)))
            {
                ClickTab(driver, detailType);
                // deleting from bottom to top to avoid StaleElementReferenceException
                foreach (var rowCandidate in driver.FindElements(tableDataRows).Reverse())
                {
                    // click delete button
                    rowCandidate.FindElement(By.XPath("td[last()]/span/i[contains(@class, 'remove')]")).Click();
                }
            }
        }

    }
}

