﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using System;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        // URL where the windows appication driver is available
        protected const string WindowsApplicationDriverUrl = "http://127.0.0.1:4723";

        // Folder that contains the build output of the WPF application previously created
        private const string WpfAppId = @"C:\Users\ruthwaiganjo\source\Repos\WpfApp\WpfApp\bin\Debug\netcoreapp3.1\WpfApp.exe";

        // Static property to store the session of type WindowsDriver, classes coming from the Appium SDK
        protected static WindowsDriver<WindowsElement> session;

        // Executed before each test
        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            // The session object provides multiple methods to find elements in the window
            // FindElementByTagName() and FindElementByCssSelector() methods are dedicated to web applications
            // For a desktop application is called FindElementByAccessibilityId()
            if (session == null)
            {
                // Appium supports multiple options handled in AppiumOptions
                var appiumOptions = new AppiumOptions();

                // Only required parameter,contains the identifier of the application we are testing
                appiumOptions.AddAdditionalCapability("app", WpfAppId);

                // Tells the framework which platform our application is targeting
                appiumOptions.AddAdditionalCapability("deviceName", "WindowsPC");

                // In the end, a new session is created using the appium options created and the windows app driver url
                session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appiumOptions);

            }

        }

        // Used by MSTest to identify which actual tests to execute
        [TestMethod]
        public void AddNameToTextBox()
        {
            var txtName = session.FindElementByAccessibilityId("txtName");
            txtName.SendKeys("Ruth");
            session.FindElementByAccessibilityId("sayHelloButton").Click();
            var txtResult = session.FindElementByAccessibilityId("txtResult");
            Assert.AreEqual(txtResult.Text, $"Hello {txtName.Text}");
        }
    }
}