//------------------------------------------------------------------------------
// <copyright file="ATPackage.cs" company="Company">
//     Copyright (c) Company.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.Win32;
using EnvDTE80;

namespace AutoTest.VSIX
{
    /// <summary>
    /// This is the class that implements the package exposed by this assembly.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The minimum requirement for a class to be considered a valid package for Visual Studio
    /// is to implement the IVsPackage interface and register itself with the shell.
    /// This package uses the helper classes defined inside the Managed Package Framework (MPF)
    /// to do it: it derives from the Package class that provides the implementation of the
    /// IVsPackage interface and uses the registration attributes defined in the framework to
    /// register itself and its components with the shell. These attributes tell the pkgdef creation
    /// utility what data to put into .pkgdef file.
    /// </para>
    /// <para>
    /// To get loaded into VS, the package must be referred by &lt;Asset Type="Microsoft.VisualStudio.VsPackage" ...&gt; in .vsixmanifest file.
    /// </para>
    /// </remarks>
    [PackageRegistration(UseManagedResourcesOnly = true)]
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)] // Info on this package for Help/About
    [Guid(GuidList.guidATPackageString)]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "pkgdef, VS and vsixmanifest are valid VS terms")]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [ProvideToolWindow(typeof(AutoTest.VSIX.MyFeedbackWindow))]
    public sealed class ATPackage : Package
    {
        private DTE2 _dte;
        private OleMenuCommandService _mcs;

        private DTE2 _applicationObject
        {
            get { return _dte ?? (_dte = ServiceProvider.GlobalProvider.GetService(typeof(DTE2)) as DTE2); }
        }

        public OleMenuCommandService MenuCommandService
        {
            get { return _mcs ?? (_mcs = GetService(typeof(IMenuCommandService)) as OleMenuCommandService); }
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ATPackage"/> class.
        /// </summary>
        public ATPackage()
        {
            // Inside this method you can place any initialization code that does not require
            // any Visual Studio service because at this point the package object is created but
            // not sited yet inside Visual Studio environment. The place to do all the other
            // initialization is the Initialize method.
        }

        #region Package Members

        /// <summary>
        /// Initialization of the package; this method is called right after the package is sited, so this is the place
        /// where you can put all the initialization code that rely on services provided by VisualStudio.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
            InitializeCommands();
            AutoTest.VSIX.MyFeedbackWindowCommand.Initialize(this);
        }

        private void InitializeCommands()
        {
            //AutoTest.VSIX.ATFeedbackCommand.Initialize(this/*, _applicationObject*/);
            //AutoTest.VSIX.Command1.Initialize(this);

            if (MenuCommandService == null)
            {
                return;
            }

            MenuCommandService.AddCommand(CreateMenuCommand(this.FeedbackWindowCallback, PackageCommands.FeedbackWindowCommandId));
        }


        private OleMenuCommand CreateMenuCommand(EventHandler hanlder, uint cmdId)
        {
            var menuCommandID = new CommandID(GuidList.guidATPackageCmdSet, (int)cmdId);
            var menuItem = new OleMenuCommand(hanlder, menuCommandID);
            menuItem.BeforeQueryStatus += MenuItem_BeforeQueryStatus;
            return menuItem;
        }

        private void MenuItem_BeforeQueryStatus(object sender, EventArgs e)
        {
            var command = sender as OleMenuCommand;

            if(command == null)
            {
                return;
            }

            //set visibility here
            switch (command.CommandID.ID)
            {
                case PackageCommands.ResumeEngineCommandId:
                    //if engine is paused then make active
                    break;

                case PackageCommands.PauseEngineCommandId:
                    //if engine is started then make it active
                    break;

                case PackageCommands.BuildAndTestAllProjectsCommandId:
                    //if engine is started then make it active
                    break;
            }

        }

        private void FeedbackWindowCallback(object sender, EventArgs e)
        {
            string message = string.Format(CultureInfo.CurrentCulture, "Inside {0}.MenuItemCallback()", this.GetType().FullName);
            string title = "ATFeedbackCommand";

            // Show a message box to prove we were here
            VsShellUtilities.ShowMessageBox(
                this,
                message,
                title,
                OLEMSGICON.OLEMSGICON_INFO,
                OLEMSGBUTTON.OLEMSGBUTTON_OK,
                OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
        }


        #endregion
    }
}
