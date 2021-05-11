﻿using System;
using CfStreamUploader.Core.Models;
using Newtonsoft.Json;
using System.IO;
using Xunit;

namespace CfStreamUploader.Core.Test
{
    public class ConfigManagerTest
    {

         private ConfigManager ConfigManager { get; } = new ConfigManager();
        
         private readonly string solutionDir =
             Path.GetDirectoryName(Path.GetDirectoryName(Environment.CurrentDirectory));

         private const string configPath = "Config.json";

         [Theory]
         [InlineData("ReadConfig TestToken", true, "ReadConfig TestToken")]
         [InlineData("ReadConfig TestToken", false, "")]
         public void ReadConfigTest(string token, bool writeFile, string result)
         {
             #region Assign
             
             SetUp();

             var config = new Config(token);
        
             if (writeFile)
             {
                 var jsonString = JsonConvert.SerializeObject(config, Formatting.Indented);
                 File.WriteAllText(Path.Combine(solutionDir, configPath), jsonString);
             }
        
             #endregion
        
             #region Act
        
             this.ConfigManager.ReadConfig();
        
             #endregion
        
             #region Assert
        
             Equals(result, this.ConfigManager.Config.CfToken);
             TearDown();
             #endregion
         }
        
         [Fact]
         public void UpdateConfigTest()
         {
             #region Assign
             
             SetUp();

             #endregion
        
             #region Act
        
             this.ConfigManager.UpdateConfig("testToken");
        
             #endregion
        
             #region Assert
        
             Equals(File.Exists(Path.Combine(this.solutionDir + configPath)), Equals(true));
             TearDown();
        
             #endregion
         }
        
         private void SetUp()
         {
             this.ConfigManager.CfStreamUploaderPath = this.solutionDir;
             DeletePath();
         }
        
         private void TearDown()
         {
             DeletePath();
         }
        
         private void DeletePath()
         {
             if (File.Exists(Path.Combine(this.solutionDir, configPath)))
                 File.Delete(Path.Combine(this.solutionDir, configPath));
         }
    }
}