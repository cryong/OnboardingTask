using MarsQA_1.Helpers;
using MarsQA_1.Pages;
using NUnit.Framework;
using RelevantCodes.ExtentReports;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using TechTalk.SpecFlow;
using static MarsQA_1.Helpers.CommonMethods;

namespace MarsQA_1.Utils
{
    [Binding]
    public class Start : Driver
    {
        private readonly ScenarioContext _context;
        public Start(ScenarioContext context)
        {
            _context = context;
        }

        [BeforeScenario(Order = 1)]
        public void Setup()
        {
            // Starting test for ExtentRerpot
            CommonMethods.test = CommonMethods.Extent.StartTest(_context.ScenarioInfo.Title,
                string.Join(",", TestContext.CurrentContext.Test.Arguments));

            //launch the browser
            Initialize();
            var relativePath = Path.Combine("SpecflowTests", "Data", "Mars.xlsx");
            var baseDir = ConstantHelpers.baseDirectory.Replace("\\bin\\Debug", "");
            var absolutePath = Path.Combine(baseDir, relativePath);
            ExcelLibHelper.PopulateInCollection(absolutePath, "Credentials");
        }

        [AfterScenario(Order = 1)]
        public void CaptureTestResult()
        {
            // Capture screenshot as soon as the scenario is finished
            string img = SaveScreenShotClass.SaveScreenshot(Driver.driver, "Report");
            test.Log(LogStatus.Info, "Test Result Snapshot below: " + test.AddScreenCapture(img));
        }

        [AfterScenario]
        public void TearDown()
        {
            //Close the browser
            Close();

            // end test. (Reports)
            CommonMethods.Extent.EndTest(test);
        }

        [AfterStep]
        public void LogStep()
        {
            StepInfo stepInfo = _context.StepContext.StepInfo;
            ScenarioExecutionStatus status = _context.ScenarioExecutionStatus;
            LogStatus logStatus = LogStatus.Pass;

            switch (status)
            {
                case ScenarioExecutionStatus.OK:
                    break;
                case ScenarioExecutionStatus.Skipped:
                    logStatus = LogStatus.Skip;
                    break;
                default: // everything else is a fail
                    logStatus = LogStatus.Fail;
                    break;
            }

            if (logStatus == LogStatus.Fail)
            {
                test.Log(logStatus, stepInfo.Text, $"<pre>{_context.TestError.Message}</pre>");
            }
            else
            {
                test.Log(logStatus, stepInfo.Text, "");
            }
        }

        [BeforeTestRun]
        public static void StartReport()
        {
            CommonMethods.ExtentReports();
        }
        [AfterTestRun]
        public static void EndReport()
        {
            // calling Flush writes everything to the log file (Reports)
            CommonMethods.Extent.Flush();
        }
    }
}
