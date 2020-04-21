using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarsQA_1.Helpers;
using MarsQA_1.Pages;
using TechTalk.SpecFlow;

namespace MarsQA_1.StepDefinitions
{
    [Binding]
    public class Login
    {

        [Given(@"I login to the website")]
        public void GivenILoginToTheWebsite()
        {
            SignIn.SigninStep();
        }
    }
}
