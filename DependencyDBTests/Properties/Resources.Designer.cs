﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DBConnectionTests.Properties {
    using System;
    using System.Reflection;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("DBConnectionTests.Properties.Resources", typeof(Resources).GetTypeInfo().Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to DECLARE @V_MainName sysname = &apos;{0}&apos;
        ///DECLARE @V_Cmd nvarchar(max)
        ///
        ///-- Drop Type
        ///IF EXISTS (
        ///	SELECT name 
        ///	FROM sys.types 
        ///	WHERE 
        ///		is_table_type = 1 AND 
        ///		name = &apos;SpParametersType&apos;)
        ///	BEGIN
        ///		SET @V_Cmd = &apos;
        ///			DROP TYPE &apos; + quotename(@V_MainName) + &apos;.[SpParametersType];&apos;
        ///		EXEC( @V_Cmd);
        ///	END
        ///
        ///-- Drop Route
        ///IF EXISTS (
        ///	SELECT name
        ///	FROM sys.routes
        ///	WHERE name = &apos;AutoCreatedLocal&apos;)
        ///	BEGIN
        ///		DROP ROUTE [AutoCreatedLocal];
        ///	END
        ///
        ///-- Drop shema
        ///IF EXISTS (
        ///	SELECT name  
        ///	FROM sys.s [rest of string was truncated]&quot;;.
        /// </summary>
        public static string AdminInstall_Cleanup {
            get {
                return ResourceManager.GetString("AdminInstall_Cleanup", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to DECLARE @V_MainName sysname = &apos;{0}&apos;
        ///DECLARE @V_Cmd nvarchar(max)
        ///
        ///-- Drop Type
        ///IF EXISTS (
        ///		SELECT name 
        ///		FROM sys.types 
        ///		WHERE 
        ///			is_table_type = 1 AND 
        ///			name = &apos;SpParametersType&apos;)
        ///	AND
        ///	EXISTS (
        ///		SELECT name
        ///		FROM sys.routes
        ///		WHERE name = &apos;AutoCreatedLocal&apos;)
        ///	AND
        ///	EXISTS (
        ///		SELECT name
        ///		FROM sys.database_principals
        ///		WHERE 
        ///			name = @V_MainName AND
        ///			type = &apos;S&apos;)
        ///	AND 
        ///	EXISTS (
        ///		SELECT name  
        ///		FROM sys.schemas
        ///		WHERE name = @V_MainName)
        ///	AND
        ///	EXISTS (
        ///		SELECT [rest of string was truncated]&quot;;.
        /// </summary>
        public static string AdminInstall_Test {
            get {
                return ResourceManager.GetString("AdminInstall_Test", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to DECLARE @V_MainName sysname = &apos;{0}&apos;
        ///DECLARE @V_Cmd nvarchar(max)
        ///
        ///-- Drop Type
        ///IF EXISTS (
        ///		SELECT column1 
        ///		FROM dbo.testTable)
        ///	BEGIN
        ///		SELECT 1
        ///	END
        ///ELSE
        ///	BEGIN
        ///		SELECT 0
        ///	END
        ///.
        /// </summary>
        public static string AdminInstallObservedShema_Test {
            get {
                return ResourceManager.GetString("AdminInstallObservedShema_Test", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to DECLARE @V_MainName sysname = &apos;{0}&apos;
        ///DECLARE @V_Cmd nvarchar(max)
        ///
        ///-- Drop Type
        ///IF NOT EXISTS (
        ///		SELECT name 
        ///		FROM sys.types 
        ///		WHERE 
        ///			is_table_type = 1 AND 
        ///			name = &apos;SpParametersType&apos;)
        ///	AND
        ///	NOT EXISTS (
        ///		SELECT name
        ///		FROM sys.routes
        ///		WHERE name = &apos;AutoCreatedLocal&apos;)
        ///	AND
        ///	NOT EXISTS (
        ///		SELECT name
        ///		FROM sys.database_principals
        ///		WHERE 
        ///			name = @V_MainName AND
        ///			type = &apos;S&apos;)
        ///	AND 
        ///	NOT EXISTS (
        ///		SELECT name  
        ///		FROM sys.schemas
        ///		WHERE name = @V_MainName)
        ///	AND
        ///	NO [rest of string was truncated]&quot;;.
        /// </summary>
        public static string AdminUnInstall_Test {
            get {
                return ResourceManager.GetString("AdminUnInstall_Test", resourceCulture);
            }
        }
    }
}