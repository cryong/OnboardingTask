using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MarsQA_1.Helpers;
using MarsQA_1.SpecflowPages.Pages;
using MarsQA_1.SpecflowTests.DataObjects;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using static MarsQA_1.SpecflowPages.Pages.ProfilePage;

namespace MarsQA_1.StepDefinitions
{
    [Binding]
    public sealed class ProfileSteps
    {
        private readonly ScenarioContext _context;
        private ProfilePage ProfilePage => new ProfilePage();
        public ProfileSteps(ScenarioContext injectedContext)
        {
            _context = injectedContext;
        }

        [When(@"I save (?:another )?'(.*)' details as follows:")]
        public void WhenISaveDetailsAsFollows(SellerDetailType type, Table table)
        {
            try
            {
                var contextObject = CreateObjectFromDataTable(type, table);
                _context.Set<SearchableItem>(contextObject);
                ProfilePage.SaveProfileDetail(Driver.driver, contextObject);
            }
            catch (Exception e)
            {
                DoHandleExceptions(e);
            }
        }

        // Currently does not work with 'Description'
        [Then(@"my profile page displays the newly added (?:.+?) details")]
        public void ThenMyProfilePageDisplaysTheNewlyAddedDetails()
        {
            bool isFound = false;
            try
            {
                isFound = ProfilePage.SearchForRow(Driver.driver, _context.Get<SearchableItem>());

            }
            catch (Exception e)
            {
                DoHandleExceptions(e);
            }
            Assert.IsTrue(isFound);
        }

        [Then(@"the success message ""(.*)"" is displayed")]
        public void ThenTheSuccessMessageIsDisplayed(string message)
        {
            CheckForSuccessPopUp(message);
        }

        [Given(@"I already have (?:\d+) '(.*)' details as follows:")]
        public void GivenIAlreadyHaveDetailsAsFollows(SellerDetailType type, Table table)
        {
            try
            {
                var details = CreateObjectsFromDataTable(type, table);
                _context.Set<IEnumerable<SearchableItem>>(details);
                ProfilePage.SaveProfileDetails(Driver.driver, details);
            }
            catch (Exception e)
            {
                DoHandleExceptions(e);
            }
        }

        private void DoHandleExceptions(Exception e)
        {
            StepInfo stepInfo = _context.StepContext.StepInfo;
            Assert.Fail($"Error has occurred while executing step : {stepInfo.Text}\nMessage : {e.Message}\nStackTrace : {e.StackTrace}");
        }

        [Then(@"I cannot add another '(?:.*)' detail")]
        public void ThenICannotAddAnotherDetail()
        {
            Assert.Throws<WebDriverTimeoutException>(() => ProfilePage.SaveProfileDetail(Driver.driver, _context.Get<SearchableItem>()));
        }

        [When(@"I add language detail with name '(.*)' and level '(.*)'")]
        public void WhenIAddLanguageDetailWithName(string name, string level)
        {
            try
            {
                Language language = new Language(name, level);
                _context.Set<SearchableItem>(language);
                ProfilePage.EnterLanguageDetails(Driver.driver, language);
            }
            catch (Exception e)
            {
                DoHandleExceptions(e);
            }
        }

        [Then(@"the error message '(.*)' is displayed")]
        public void ThenTheErrorMessageIsDisplayed(string message)
        {
            CheckForErrorPopUp(message);
        }

        [Then(@"the detail is not saved")]
        public void ThenTheDetailIsNotSaved()
        {
            bool isButtonEnabled = false;
            bool isRowFound = false;
            try
            {
                isButtonEnabled = ProfilePage.IsAddRowButtonEnbaled(Driver.driver);
                isRowFound = ProfilePage.SearchForRow(Driver.driver, _context.Get<SearchableItem>());
            }
            catch (Exception e)
            {
                DoHandleExceptions(e);
            }
            // To verify that it's succesfully saved
            // We need to check that button is disabled and row is found
            bool isSaved = !isButtonEnabled && isRowFound;
            Assert.IsFalse(isSaved);
        }

        [Given(@"I have opened the profile page on another tab")]
        public void GivenIHaveOpenedTheProfilePageOnAnotherTab()
        {
            try
            {
                _context.Set<String>(Driver.driver.CurrentWindowHandle);
                IJavaScriptExecutor ex = (IJavaScriptExecutor)Driver.driver;
                ex.ExecuteScript("window.open()");
                var tabs = Driver.driver.WindowHandles;
                Driver.driver.SwitchTo().Window(tabs[1]); // switching tab
                Driver.driver.Navigate().GoToUrl(ConstantHelpers.SellerProfileUrl);
            }
            catch (Exception e)
            {
                DoHandleExceptions(e);
            }
        }

        [Given(@"I switch back to the previous tab")]
        public void GivenISwitchBackToThePreviousTab()
        {
            try
            {
                Driver.driver.SwitchTo().Window(_context.Get<String>());
            }
            catch(Exception e)
            {
                DoHandleExceptions(e);
            }
        }

        [StepArgumentTransformation("''(.*)''")]
        public string TransformTableCellData(string expr)
        {
            // Special case in order to preserve whitespaces in DataTable
            // as leading & trailing whitespaces are stripped otherwise
            return expr;
        }

        [AfterScenario(Order = 2)] // Screenshot is taken before this
        public void CleanTableData()
        {
            ProfilePage.DeleteAllTableData(Driver.driver);
        }

        private void CheckForErrorPopUp(string expectedErrorMessage)
        {
            string actualMessage = "";
            try
            {
                actualMessage = ProfilePage.getErrorPopUpMessage(Driver.driver);
            }
            catch (Exception e)
            {
                DoHandleExceptions(e);
            }
            Assert.That(expectedErrorMessage, Is.EqualTo(actualMessage));
        }

        private void CheckForSuccessPopUp(string expectedSuccessMessage)
        {
            string actualMessage = "";
            try
            {
                actualMessage = ProfilePage.getSuccessPopUpMessage(Driver.driver);
            }
            catch (Exception e)
            {
                DoHandleExceptions(e);
            }
            Assert.That(expectedSuccessMessage, Is.EqualTo(actualMessage));
        }

        private SearchableItem CreateObjectFromDataTable(SellerDetailType type, Table table)
        {
            switch (type)
            {
                case SellerDetailType.Language:
                    return table.CreateInstance<Language>();
                case SellerDetailType.Skill:
                    return table.CreateInstance<Skill>();
                case SellerDetailType.Education:
                    return table.CreateInstance<Education>();
                case SellerDetailType.Certification:
                    return table.CreateInstance<Certification>();
                default:
                    throw new ArgumentException("Unknown type argument : " + type.ToString("G"));
            }
        }

        private IEnumerable<SearchableItem> CreateObjectsFromDataTable(SellerDetailType type, Table table)
        {
            switch (type)
            {
                case SellerDetailType.Language:
                    return table.CreateSet<Language>();
                case SellerDetailType.Skill:
                    return table.CreateSet<Skill>();
                case SellerDetailType.Education:
                    return table.CreateSet<Education>();
                case SellerDetailType.Certification:
                    return table.CreateSet<Certification>();
                default:
                    throw new ArgumentException("Unknown type argument : " + type.ToString("G"));
            }
        }
    }
}
