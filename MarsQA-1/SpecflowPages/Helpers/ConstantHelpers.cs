using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsQA_1.Helpers
{
    public class ConstantHelpers
    {
        //Base Url
        public static string Url = "http://localhost:5000";

        public static string SellerProfileUrl = Url + "/Account/Profile";

        public static string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

        //ScreenshotPath
        public static string ScreenshotPath = baseDirectory + @"..\..\TestReports\Screenshots\";

        //ExtentReportsPath
        public static string ReportsPath = baseDirectory + @"..\..\TestReports\TestReport.html";

        //ReportXML Path
        public static string ReportXMLPath = baseDirectory + @"..\..\SpecflowTests\ReportConfig\html-config.xml";
    }
}