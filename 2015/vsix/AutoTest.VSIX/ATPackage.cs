namespace AutoTest.VSIX
{
    #region Usings
    using System;
    using System.ComponentModel.Design;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Runtime.InteropServices;
    using AutoTest.Core.DebugLog;
    using AutoTest.VS.Util.Builds;
    using EnvDTE;
    using EnvDTE80;
    using Microsoft.VisualStudio;
    using Microsoft.VisualStudio.Shell;
    using Microsoft.VisualStudio.Shell.Interop;
    using Engine = AutoTest.VSIX.ATEngine.Engine;
    using Thread = System.Threading.Thread;

    #endregion

    [PackageRegistration(UseManagedResourcesOnly = true)]
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)]
    [Guid(GuidList.guidATPackageString)]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "pkgdef, VS and vsixmanifest are valid VS terms")]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [ProvideToolWindow(typeof(FeedbackWindowToolPane))]
    [ProvideToolWindowVisibility(typeof(FeedbackWindowToolPane), UIContextGuids80.SolutionExists)]
    [ProvideAutoLoad(VSConstants.UICONTEXT.SolutionExists_string)]
    public sealed partial class AtPackage : Package
    {
        private DTE2 _dte;

        private OleMenuCommandService _menuCommandService;

        private NewFeedbackWindowControl _toolWindow;

        private FeedbackWindow _control;

        private VSBuildRunner _buildRunner;

        private ToolWindowPane _window;

        public static string _WatchToken;

        public static Engine _engine;

        private DTE2 _applicationObject
        {
            get
            {
                return _dte ?? (_dte = GetService(typeof(DTE)) as DTE2);
            }
        }

        public OleMenuCommandService MenuCommandService
        {
            get
            {
                return _menuCommandService ?? (_menuCommandService = ServiceProvider.GetService(typeof(IMenuCommandService)) as OleMenuCommandService);
            }
        }

        private IServiceProvider ServiceProvider
        {
            get
            {
                return this;
            }
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

                this.BindEvents();
                this.InitializeCommands();

                _buildRunner = new VSBuildRunner(_applicationObject, () => { return !_engine.IsRunning; }, (s) => _engine.SetCustomOutputPath(s), (o) => _control.Consume(o), (s) => _control.ClearBuilds(s));
            }
            catch (Exception exception)
            {
                Debug.WriteException(exception);
            }
        }

        private void SetEngine()
        {
            try
            {
                if (_control != null)
                {
                    _engine = new Engine(_control, _applicationObject);
                }
                else
                {
                    _engine = new Engine(null, _applicationObject);
                }
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

            MenuCommandService.AddCommand(CreateMenuCommand(this.ShowOrHideToolWindow, PackageCommands.FeedbackWindowCommandId));
            MenuCommandService.AddCommand(CreateMenuCommand(ResumeEngineCallback, PackageCommands.ResumeEngineCommandId));
            MenuCommandService.AddCommand(CreateMenuCommand(PauseEngineCallback, PackageCommands.PauseEngineCommandId));
            MenuCommandService.AddCommand(CreateMenuCommand(this.RestartEngineCallback, PackageCommands.RestartEngineCommandId));
            MenuCommandService.AddCommand(CreateMenuCommand(BuildAndTestAllProjectsCallback, PackageCommands.BuildAndTestAllProjectsCommandId));
        }

        private OleMenuCommand CreateMenuCommand(EventHandler hanlder, uint cmdId)
        {
            var menuCommandID = new CommandID(GuidList.guidATPackageCmdSet, (int)cmdId);
            var menuItem = new OleMenuCommand(hanlder, menuCommandID);
            menuItem.BeforeQueryStatus += MenuItem_ControlCommandVisibility;
            return menuItem;
        }

        private void MenuItem_ControlCommandVisibility(object sender, EventArgs e)
        {
            var command = sender as OleMenuCommand;

            if (command == null)
            {
                return;
            }

            if (_applicationObject.Solution.IsOpen)
            {
                // solution open
                switch (command.CommandID.ID)
                {
                    case PackageCommands.RestartEngineCommandId:
                    case PackageCommands.BuildAndTestAllProjectsCommandId:
                        command.Enabled = true;
                        break;

                    case PackageCommands.ResumeEngineCommandId:
                        if (!_engine.IsRunning)
                        {
                            command.Enabled = true;
                        }

                        break;

                    case PackageCommands.PauseEngineCommandId:
                        if (_engine.IsRunning)
                        {
                            command.Enabled = true;
                        }

                        break;
                }
            }
            else if (command.CommandID.ID != PackageCommands.FeedbackWindowCommandId)
            {
                // not feedback 
                // disable command
                command.Enabled = false;
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
                ErrorHandler.ThrowOnFailure(windowFrame.Hide());
            }
            else
            {
                ErrorHandler.ThrowOnFailure(windowFrame.Show());
            }

            if (_window.Content != null)
            {
                _toolWindow = _window.Content as NewFeedbackWindowControl;

                if (_toolWindow != null)
                {
                    _control = _toolWindow.WindowsFormsHost.Child as FeedbackWindow;
                }
            }
        }

        private static void PauseEngineCallback(object sender, EventArgs e)
        {
            if (_engine.IsRunning)
            {
                _engine.Pause();
            }
        }

        private static void ResumeEngineCallback(object sender, EventArgs e)
        {
            if (!_engine.IsRunning)
            {
                _engine.Resume();
            }
        }

        private void RestartEngineCallback(object sender, EventArgs e)
        {
            if (Directory.Exists(GetWatchDirectory()))
            {
                SolutionEvents_AfterClosing();
                Thread.Sleep(500);
                this.SolutionEvents_Opened();
            }
        }

        private static void BuildAndTestAllProjectsCallback(object sender, EventArgs e)
        {
            if (_engine.IsRunning)
            {
                _engine.BuildAndTestAll();
            }
        }

        #endregion

        private static string GetWatchDirectory()
        {
            if (_WatchToken == null)
            {
                return string.Empty;
            }

            return Path.GetDirectoryName(_WatchToken);
        }

        #endregion
    }
}