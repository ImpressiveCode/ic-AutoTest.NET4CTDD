//------------------------------------------------------------------------------
// <copyright file="ATPackage.cs" company="Company">
//     Copyright (c) Company.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel.Design;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using EnvDTE80;
using AutoTest.Core.DebugLog;
using AutoTest.VS.Util.Builds;
using EnvDTE;

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
    [ProvideToolWindow(typeof(AutoTest.VSIX.FeedbackWindowToolPane))]
    [ProvideAutoLoad(VSConstants.UICONTEXT.NoSolution_string)]
    [ProvideAutoLoad(VSConstants.UICONTEXT.SolutionExists_string)]
    public sealed partial class ATPackage : Package
    {
        private DTE2 _dte;

        private OleMenuCommandService _menuCommandService;
        private NewFeedbackWindowControl _toolWindow;
        private FeedbackWindow _control;
        private VSBuildRunner _buildRunner;
        private ToolWindowPane _window;

        public static string _WatchToken = null;
        public static ATEngine.Engine _engine = null;

        private DTE2 _applicationObject
        {
            get { return _dte ?? (_dte = GetService(typeof(DTE)) as DTE2); }
        }

        public OleMenuCommandService MenuCommandService
        {
            get { return _menuCommandService ?? (_menuCommandService = ServiceProvider.GetService(typeof(IMenuCommandService)) as OleMenuCommandService); }
        }

        private System.IServiceProvider ServiceProvider
        {
            get
            {
                return this;
            }
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
            try
            {
                this.SetEngine();
                this.bindEvents();
                this.InitializeCommands();
                // TODO CF from NL: init buildRunner!!!
            }
            catch (Exception exception)
            {
                Debug.WriteException(exception);
            }
        }

        private void SetEngine()        // TODO CF from NL: fix throwing exception every time during the first load.
        {
            try
            {
                if (_control != null)
                {
                    _engine = new ATEngine.Engine(_control, _applicationObject);
                }
                else _engine = new ATEngine.Engine(null, _applicationObject);
            }
            catch (Exception exception)
            {
                Debug.WriteException(exception);
            }
        }

        private void InitializeCommands()
        {

            if (MenuCommandService == null)
            {
                return;
            }

            // TODO CF from NL: VSBuildRunner ??
            // TODO CF from NL: CommandDispatchers RegisterHandler ??

            MenuCommandService.AddCommand(CreateMenuCommand(this.ShowOrHideToolWindow, PackageCommands.FeedbackWindowCommandId));
            MenuCommandService.AddCommand(CreateMenuCommand(this.ResumeEngineCallback, PackageCommands.ResumeEngineCommandId));
            MenuCommandService.AddCommand(CreateMenuCommand(this.PauseEngineCallback, PackageCommands.PauseEngineCommandId));
            MenuCommandService.AddCommand(CreateMenuCommand(this.RestartEngineCallback, PackageCommands.RestartEngineCommandId));
            MenuCommandService.AddCommand(CreateMenuCommand(this.BuildAndTestAllProjectsCallback, PackageCommands.BuildAndTestAllProjectsCommandId));

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

            if (command == null)
            {
                return;
            }

            // set visibility here
            switch (command.CommandID.ID)
            {
                case PackageCommands.ResumeEngineCommandId:
                    break;

                case PackageCommands.PauseEngineCommandId:
                    break;

                case PackageCommands.BuildAndTestAllProjectsCommandId:
                    break;
            }

        }

        #region Callback Methods

        /// <summary>
        /// Shows the tool window when the menu item is clicked.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event args.</param>
        private void ShowOrHideToolWindow(object sender, EventArgs e)
        {
            _window = this.FindToolWindow(typeof(FeedbackWindowToolPane), 0, true);

            if ((null == _window) || (null == _window.Frame))
            {
                throw new NotSupportedException("Cannot create tool window");
            }

            IVsWindowFrame windowFrame = (IVsWindowFrame)_window.Frame;

            if (windowFrame.IsVisible() == VSConstants.S_OK)
            {
                Microsoft.VisualStudio.ErrorHandler.ThrowOnFailure(windowFrame.Hide());
            }
            else
            {
                Microsoft.VisualStudio.ErrorHandler.ThrowOnFailure(windowFrame.Show());
            }

            if (_window.Content != null)
            {
                _toolWindow = _window.Content as NewFeedbackWindowControl;
                _control = _toolWindow.WindowsFormsHost.Child as FeedbackWindow;
            }       
        }
        
        private void PauseEngineCallback(object sender, EventArgs e)
        {
            _engine.Pause();
        }

        private void ResumeEngineCallback(object sender, EventArgs e)
        {
            _engine.Resume();
        }

        private void RestartEngineCallback(object sender, EventArgs e)
        {
            _engine.Shutdown();
            _engine.Resume();
        }

        private void BuildAndTestAllProjectsCallback(object sender, EventArgs e)
        {
            _engine.BuildAndTestAll();
        }

        #endregion

        #endregion
    }
}
