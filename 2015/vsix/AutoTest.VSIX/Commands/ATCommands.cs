//------------------------------------------------------------------------------
// <copyright file="ATFeedbackCommand.cs" company="Company">
//     Copyright (c) Company.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel.Design;
using System.Globalization;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using AutoTest.VS.Util.CommandHandling;
using EnvDTE80;

namespace AutoTest.VSIX
{
    /// <summary>
    /// Command handler
    /// </summary>
    internal sealed class ATCommands
    {

        /// <summary>
        /// VS Package that provides this command, not null.
        /// </summary>
        private readonly Package package;
        //private DTE2 _applicationObject;

        /// 
        private readonly CommandDispatcher _dispatchers = new CommandDispatcher();


        /// <summary>
        /// Initializes a new instance of the <see cref="ATCommands"/> class.
        /// Adds our command handlers for menu (commands must exist in the command table file)
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        private ATCommands(Package package/*, DTE2 _applicationObject*/)
        {
            if (package == null)
            {
                throw new ArgumentNullException("package");
            }

            this.package = package;
            //this._applicationObject = _applicationObject;

            OleMenuCommandService commandService = this.ServiceProvider.GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
            if (commandService != null)
            {
                var menuCommandID = new CommandID(GuidList.guidATPackageCmdSet, PackageCommands.RestartEngineCommandId);
                var menuItem = new MenuCommand(this.MenuItemCallback, menuCommandID);
                commandService.AddCommand(menuItem);
            }
        }

        /// <summary>
        /// Gets the instance of the command.
        /// </summary>
        public static ATCommands Instance
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the service provider from the owner package.
        /// </summary>
        private IServiceProvider ServiceProvider
        {
            get
            {
                return this.package;
            }
        }

        /// <summary>
        /// Initializes the singleton instance of the command.
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        public static void Initialize(Package package/*, DTE2 _applicationObject*/)
        {
            Instance = new ATCommands(package/*, _applicationObject*/);

        }

        /// <summary>
        /// This function is the callback used to execute the command when the menu item is clicked.
        /// See the constructor to see how the menu item is associated with this function using
        /// OleMenuCommandService service and MenuCommand class.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event args.</param>
        private void MenuItemCallback(object sender, EventArgs e)
        {
            string message = string.Format(CultureInfo.CurrentCulture, "Inside {0}.MenuItemCallback()", this.GetType().FullName);
            string title = "ATFeedbackCommand";

            // Show a message box to prove we were here
            VsShellUtilities.ShowMessageBox(
                this.ServiceProvider,
                message,
                title,
                OLEMSGICON.OLEMSGICON_INFO,
                OLEMSGBUTTON.OLEMSGBUTTON_OK,
                OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
            //_dispatchers.RegisterHandler(new ShowFeedbackWindow(_applicationObject, showFeedbackWindow));
        }

        private void showFeedbackWindow()
        {
            throw new NotImplementedException();
        }
    }
}
