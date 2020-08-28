﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using G1ANT.Addon.Twitter.Properties;
using G1ANT.Language;

// Please remember to refresh G1ANT.Language.dll in references

namespace G1ANT.Addon.Twitter
{
    [Addon(Name = "Twitter", Tooltip = "...")]
    [Copyright(Author = "Company Ltd", Copyright = "Company Ltd", Email = "support@company.com", Website = "www.company.com")]
    [License(Type = "LGPL", ResourceName = "License.txt")]
    //[CommandGroup(Name = "", Tooltip = "")]
    public class TwitterAddon : Language.Addon
    {
        public override void LoadDlls()
        {
            UnpackDrivers();
            base.LoadDlls();
        }

        private void UnpackDrivers()
        {
            var unpackFolder = AbstractSettingsContainer.Instance.UserDocsAddonFolder.FullName;
            var embeddedResourceDictionary = new Dictionary<string, byte[]>()
            {
                { "chromedriver.exe", Resources.chromedriver },
                { "geckodriver.exe", Resources.geckodriver },
                { "IEDriverServer.exe", Resources.IEDriverServer }
            };
            foreach (var embededResource in embeddedResourceDictionary.Where(e => !DoesFileExist(unpackFolder, e.Key) || !AreFilesOfTheSameLength(e.Value.Length, unpackFolder, e.Key)))
            {
                try
                {
                    using (FileStream stream = File.Create(Path.Combine(unpackFolder, embededResource.Key)))
                    {
                        stream.Write(embededResource.Value, 0, embededResource.Value.Length);
                    }
                }
                catch (Exception ex) { RobotMessageBox.Show(ex.Message); }
            }
        }

        private bool DoesFileExist(string folder, string fileName)
        {
            return File.Exists(Path.Combine(folder, fileName));
        }

        private bool AreFilesOfTheSameLength(int length, string folder, string fileName)
        {
            return length == new FileInfo(Path.Combine(folder, fileName)).Length;
        }
    }
}