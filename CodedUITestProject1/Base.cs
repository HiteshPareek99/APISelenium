using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
namespace AgdataAssignment
{

    [CodedUITest(CodedUITestType.WindowsStore)]
    public class Base
    {
        #region Selenium Test
        public IWebDriver driver;

        [TestMethod]
        public void SeleniumAssignment()
        {
            try
            {
                //Step : 1 : Open a browser and navigate to "www.agdata.com"
                driver = Utility.LaunchWebsite("https://www.agdata.com");
                //Wait till company Menu box appears.
                Utility.ExplicitWait(driver, Strings.CompanyMenuXPath);

                //Step : 2 : On the top navigation menu click on "Company" > "Careers"
                //Open Careers page from Company dropdown.
                driver.FindElement(By.XPath(Strings.CompanyMenuXPath)).Click();
                Utility.ExplicitWait(driver, Strings.CareersXPath);
                driver.FindElement(By.XPath(Strings.CareersXPath)).Click();
                Utility.ExplicitWait(driver, Strings.OpenPositionButton);

                //Step : 3 : On the "https://www.agdata.com/company/careers/" page, get back all the jobs on the page in a LIST
                //Verify the current URL is careesrs URL or not.
                if (!Utility.VerifyAddressLink(driver, Strings.CareersPageURL))
                    Assert.Fail("URL verification failed.");
                List<string> jobsName = Utility.GetJobList(driver, Strings.JobsID);

                //Step : 4 : Click on the 2nd link in the list where to job title contains 'Manager'.
                Utility.OpenJobDetails(driver, 2, "Manager");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Assert.Fail(ex.Message);
            }
            finally
            {
                driver.Quit();
            }

        }
        #endregion

        #region API test
        [TestMethod]
        public void APITest()
        {
            try
            {
                HttpClientWrapper obj = new HttpClientWrapper();

                //GET https://jsonplaceholder.typicode.com/posts
                obj.GetTest();
                //POST https://jsonplaceholder.typicode.com/posts
                obj.PostTest();
                //PUT https://jsonplaceholder.typicode.com/posts/{postId}
                obj.PutTest();
                //DELETE https://jsonplaceholder.typicode.com/posts/{postId}
                obj.DeleteTest();
                //POST https://jsonplaceholder.typicode.com/posts/{postId}/comments
                obj.PostTest2();
                //GET https://jsonplaceholder.typicode.com/comments?postId={postId}
                obj.GetTest2();

            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                Assert.Fail(ex.Message);
            }
        }
        #endregion  
    }
}

