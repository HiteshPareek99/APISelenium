using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;

namespace AgdataAssignment
{

    public static class Utility
    {
        /// <summary>
        /// Launch the URL in chrome.
        /// </summary>
        /// Written by Hitesh Pareek.
        /// <param name="siteAddress">URL we want to open.</param>
        /// <returns></returns>
        public static IWebDriver LaunchWebsite(string siteAddress)
        {
            try
            {
                IWebDriver chromeDriver = new ChromeDriver(Strings.ChromeDriverPath);
                chromeDriver.Navigate().GoToUrl("https://www.agdata.com");
                chromeDriver.Manage().Window.Maximize();
                return chromeDriver;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Assert.Fail(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Waits till the xpath control appears.
        /// </summary>
        /// Written by Hitesh PAreek
        /// <param name="xPath">x path of the control we want to wait till it loads.</param>
        /// <param name="driver">xWeb driver instance.</param>
        /// <returns></returns>
        public static IWebDriver ExplicitWait(IWebDriver driver, string xPath)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromMinutes(5));
                wait.Until(d => d.FindElement(By.XPath(xPath)));
                return driver;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Assert.Fail(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Verifies the URL in address bar matches with parameter URL or not.
        /// </summary>
        /// Written by Hitesh Pareek
        /// <param name="driver">driver instance</param>
        /// <param name="URLToVerify">URL to verify</param>
        /// <returns>True if URL matches else false </returns>
        public static bool VerifyAddressLink(IWebDriver driver, string URLToVerify)
        {
            try
            {
                string url = driver.Url;
                if (url == URLToVerify)
                    return true;
                else
                    Console.WriteLine("URL verification failed expected URL is " + URLToVerify + " actual is " + url + " .");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Assert.Fail(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Finds all the available job openings and returns the list of string .
        /// Written by : Hitesh pareek.
        /// </summary>
        /// <param name="driver">driver instance</param>
        /// <param name="xPath">cpath  of job ids</param>
        /// <returns>list of job openings in string format.</returns>
        public static List<string> GetJobList(IWebDriver driver, string xPath)
        {
            try
            {
                List<string> listJobName = new List<string>();

                //All the list items are under frame ,So switching to the frame
                driver.SwitchTo().Frame(driver.FindElement(By.Id(Strings.CareersFrameID)));
                var jobsElementCollection = driver.FindElements(By.XPath(Strings.JobsID));

                foreach (IWebElement element in jobsElementCollection)
                {
                    listJobName.Add(element.Text);
                }
                return listJobName;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Assert.Fail(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Opens the nth instance of job name 
        /// Written by : Hitesh pareek.
        /// </summary>
        /// <param name="driver">driver instance</param>
        /// <param name="instance">instance number we want to open.</param>
        /// <param name="jobName">Partial job name.</param>
        public static void OpenJobDetails(IWebDriver driver, int instance, string jobName)
        {
            try
            {
                var jobsElementCollection = driver.FindElements(By.XPath(Strings.JobsID));
                int count = 0;
                foreach (IWebElement element in jobsElementCollection)
                {
                    if (element.Text.Contains(jobName))
                        count++;
                    if (instance == count)
                    {
                        element.Click();
                        return;
                    }
                }

                //If the number of job instance are less then expected then it wont do anything and fails the test case.
                Console.WriteLine("Only " + count + " instance found for the job name : " + jobName + " So can not open the job details.");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Assert.Fail(ex.Message);
            }
        }
    }
}
