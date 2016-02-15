namespace AutoTest.VSIX
{
    #region Usings
    using System;
    using System.ComponentModel.Design;
    using AutoTest.Core.DebugLog;
    using EnvDTE;

    #endregion

    public partial class AtPackage
    {
        private Events events;

        private BuildEvents buildEvents;

        private SolutionEvents solutionEvents;

        private void BindEvents()
        {
            this.events = _applicationObject.Events;

            this.BindBuildEvents();
            this.BindSolutionEvents();
        }

    
        private void BindBuildEvents()
        {
            this.buildEvents = this.events.BuildEvents;
            this.buildEvents.OnBuildDone += _buildEvents_OnBuildDone;
        }

        private void BindSolutionEvents()
        {
            this.solutionEvents = this.events.SolutionEvents;

            this.solutionEvents.Opened += this.SolutionEvents_Opened;
            this.solutionEvents.AfterClosing += SolutionEvents_AfterClosing;

            // TODO CF from NL: implement based on ConnectEvents.cs
        }
       
        private void _buildEvents_OnBuildDone(vsBuildScope scope, vsBuildAction action)
        {
            _buildRunner.PusblishBuildErrors();
        }

        private void SolutionEvents_Opened()
        {
            try
            {
                StartFeedbackWindow();

                _WatchToken = _applicationObject.Solution.FullName;
                SetEngine();
                _engine.Bootstrap(_WatchToken);

                if (_engine.IsRunning)
                {
                    _control.SetText("Engine is running and waiting for changes");
                }
                else
                {
                    _control.SetText("Engine is paused and will not detect changes");
                }
            }
            catch (Exception exception)
            {
                Debug.WriteException(exception);
            }
        }

        private void StartFeedbackWindow()
        {
            var commandId = new CommandID(GuidList.guidATPackageCmdSet, PackageCommands.FeedbackWindowCommandId);
            MenuCommandService.GlobalInvoke(commandId);
        }

        private static void SolutionEvents_AfterClosing()
        {
            try
            {
                _engine.Shutdown();
            }
            catch (Exception exception)
            {
                Debug.WriteException(exception);
            }
        }
    }
}