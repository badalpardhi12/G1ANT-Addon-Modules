﻿using System;
using System.Diagnostics;
using System.Windows.Forms;
using G1ANT.Language;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Appium.MultiTouch;

namespace G1ANT.Addon.FlipkartAndroid
{
    [Command(Name = "flipkartandroid.open", Tooltip = "Opens Flipkart application instance on a connected android device.")]
    public class FlipkartAndroidOpenCommand : Language.Command
    {
        private static AndroidDriver<AndroidElement> driver;
        public class Arguments : CommandArguments
        {
            [Argument(DefaultVariable = "AppActivity", Tooltip = "AppActivity")]
            public TextStructure AppActivity { get; set; } = new TextStructure("com.flipkart.android.activity.HomeFragmentHolderActivity");

            [Argument(DefaultVariable = "AppPackage", Tooltip = "App Package")]
            public TextStructure AppPackage { get; set; } = new TextStructure("com.flipkart.android");

            [Argument(Tooltip = "Automation Name")]
            public TextStructure AutomationName { get; set; } = new TextStructure("UiAutomator2");

            [Argument(Tooltip = "Device Name")]
            public TextStructure DeviceName { get; set; } = new TextStructure("Android");

            [Argument(Tooltip = "Platform Name")]
            public TextStructure PlatformName { get; set; } = new TextStructure("Android");

            [Argument(Tooltip = "Platform version")]
            public TextStructure PlatformVersion { get; set; } = new TextStructure("");


        }

        public FlipkartAndroidOpenCommand(AbstractScripter scripter) :
            base(scripter)
        {
        }

        // Implement this method
        public void Execute(Arguments arguments)
        {
            Initialize(arguments);
        }

        private AppiumOptions CreateAppiumOptions(Arguments arguments)
        {
            var desiredCapabilities = new AppiumOptions();
            desiredCapabilities.AddAdditionalCapability(MobileCapabilityType.DeviceName, arguments.DeviceName.Value);
            desiredCapabilities.AddAdditionalCapability(AndroidMobileCapabilityType.AppPackage, arguments.AppPackage.Value);
            desiredCapabilities.AddAdditionalCapability(MobileCapabilityType.PlatformName, arguments.PlatformName.Value);
            desiredCapabilities.AddAdditionalCapability(AndroidMobileCapabilityType.AppActivity, arguments.AppActivity.Value);
            desiredCapabilities.AddAdditionalCapability(MobileCapabilityType.FullReset, false);
            desiredCapabilities.AddAdditionalCapability(MobileCapabilityType.NoReset, true);

            if (!string.IsNullOrEmpty(arguments.PlatformVersion.Value))
            {
                desiredCapabilities.AddAdditionalCapability(MobileCapabilityType.PlatformVersion, arguments.PlatformVersion.Value);
            }
            return desiredCapabilities;
        }
        private void Initialize(Arguments arguments)
        {
            try
            {
                var appiumServiceBuilder = new AppiumServiceBuilder().UsingAnyFreePort();
                var appiumOptions = CreateAppiumOptions(arguments);
                driver = new AndroidDriver<AndroidElement>(appiumServiceBuilder, appiumOptions);
            }
            catch (Exception ex)
            {
                InstallAppiumWhenExceptionOccured(ex);
            }
        }
        private void InstallAppiumWhenExceptionOccured(Exception ex)
        {
            if (ex.Message.StartsWith("Invalid"))
            {
                var result = RobotMessageBox.Show("It seems you have no Appium driver installed. Would you like to install it now?", "Error", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    Process.Start("\"C:\\Program Files\\nodejs\\npm.cmd\"", "install -g appium");
                }
            }
            else { throw ex; }
        }

        public static AndroidDriver<AndroidElement> GetDriver()
        {
            return driver;
        }
    }
}