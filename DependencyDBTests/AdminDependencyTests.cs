﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DBConnection;
using DBConnectionTests.Properties;
using System.Data.SqlClient;
using System.Collections.Generic;
using Microsoft.SqlServer.Server;

namespace DBConnectionTests
{
    [TestClass]
    public class AdminDependencyTests
    {
        AdminDependencyDB AdminDependencyDBInstance = new AdminDependencyDB(CommonTestsValues.AdminConnectionString);

        [TestMethod]
        public void AdminInstall()
        {
            string slqCommandText;
            SqlCommand sqlCommand;
            // CleanUp DB
            slqCommandText = string.Format(
                Resources.AdminInstall_Cleanup,
                CommonTestsValues.MainServiceName
                );
            sqlCommand = new SqlCommand(slqCommandText);
            AdminDependencyDBInstance.AccessDBInstance.SQLRunNonQueryProcedure(sqlCommand);

            // Try install
            AdminDependencyDBInstance.AdminInstall(CommonTestsValues.DefaultDBName, CommonTestsValues.LoginPass, CommonTestsValues.MainServiceName);

            // Test install
            slqCommandText = string.Format(
                Resources.AdminInstall_Test,
                CommonTestsValues.MainServiceName
                );
            sqlCommand = new SqlCommand(slqCommandText);
            List<Tuple<int>> testResult = AdminDependencyDBInstance.AccessDBInstance.SQLRunQueryProcedure<Tuple<int>>(sqlCommand);
            if (testResult.Count != 1 && testResult[0].Item1 != 1)
            {
                Assert.Fail("Not all objects were instaled properly when instaling from clean DB.");
            }

            // try instal with existing objects
            AdminDependencyDBInstance.AdminInstall(CommonTestsValues.DefaultDBName, CommonTestsValues.LoginPass, CommonTestsValues.MainServiceName);

            // Test install2
            slqCommandText = string.Format(
                Resources.AdminInstall_Test,
                CommonTestsValues.MainServiceName
                );
            sqlCommand = new SqlCommand(slqCommandText);
            List<Tuple<int>> testResult2 = AdminDependencyDBInstance.AccessDBInstance.SQLRunQueryProcedure<Tuple<int>>(sqlCommand);
            if (testResult2.Count != 1 && testResult2[0].Item1 != 1)
            {
                Assert.Fail("Not all objects were instaled properly when instaling from already populated DB.");
            }
        }

        /// <summary>
        /// Do not run this after AdminUninstall
        /// </summary>
        [TestMethod]
        public void AdminInstallObservedShema()
        {
            string slqCommandText;
            SqlCommand sqlCommand;
            AccessDB serviceAccessDB = new AccessDB(CommonTestsValues.ServiceConnectionString);

            // Try
            AdminDependencyDBInstance.AdminInstallObservedShema("dbo", CommonTestsValues.MainServiceName, false);

            // Test
            slqCommandText = string.Format(
                Resources.AdminInstallObservedShema_Test,
                CommonTestsValues.MainServiceName
                );
            sqlCommand = new SqlCommand(slqCommandText);
            try
            {
                List<Tuple<int>> testResult = serviceAccessDB.SQLRunQueryProcedure<Tuple<int>>(sqlCommand);
                if (testResult.Count != 1 && testResult[0].Item1 != 0)
                {
                    Assert.Fail("Can select from granted table after revoke provilages.");
                }
            }
            catch (Exception ex)
            {
                if (!ex.InnerException.Message.Contains("SELECT permission was denied on the object "))
                {
                    throw ex;
                }
            }

            // Try
            AdminDependencyDBInstance.AdminInstallObservedShema("dbo", CommonTestsValues.MainServiceName);

            // Test
            slqCommandText = string.Format(
                Resources.AdminInstallObservedShema_Test,
                CommonTestsValues.MainServiceName
                );
            sqlCommand = new SqlCommand(slqCommandText);
            List<Tuple<int>> testResult2 = serviceAccessDB.SQLRunQueryProcedure<Tuple<int>>(sqlCommand);
            if (testResult2.Count != 1 && testResult2[0].Item1 != 1)
            {
                Assert.Fail("Cannot select from granted table after grant provilages.");
            }

        }

        [TestMethod]
        public void AdminUnInstall()
        {
            string slqCommandText;
            SqlCommand sqlCommand;
            
            // Try install
            AdminDependencyDBInstance.AdminUnInstall(CommonTestsValues.MainServiceName);

            // Test install
            slqCommandText = string.Format(
                Resources.AdminUnInstall_Test,
                CommonTestsValues.MainServiceName
                );
            sqlCommand = new SqlCommand(slqCommandText);
            List<Tuple<int>> testResult = AdminDependencyDBInstance.AccessDBInstance.SQLRunQueryProcedure<Tuple<int>>(sqlCommand);
            if (testResult.Count != 1 && testResult[0].Item1 != 1)
            {
                Assert.Fail("Some objects still exists after uninstall.");
            }

            // try instal again
            AdminDependencyDBInstance.AdminInstall(CommonTestsValues.DefaultDBName, CommonTestsValues.LoginPass, CommonTestsValues.MainServiceName);

            // Test install2
            slqCommandText = string.Format(
                Resources.AdminInstall_Test,
                CommonTestsValues.MainServiceName
                );
            sqlCommand = new SqlCommand(slqCommandText);
            List<Tuple<int>> testResult2 = AdminDependencyDBInstance.AccessDBInstance.SQLRunQueryProcedure<Tuple<int>>(sqlCommand);
            if (testResult2.Count != 1 && testResult2[0].Item1 != 1)
            {
                Assert.Fail("Some objects were not re-instaled properly after uninstall.");
            }
        }
    }
}
