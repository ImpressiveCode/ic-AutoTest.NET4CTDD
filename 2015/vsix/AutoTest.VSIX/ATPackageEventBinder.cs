using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnvDTE80;
using EnvDTE;
using AutoTest.Core.DebugLog;
using AutoTest.VS.Util.Builds;
using System.Windows.Forms;

namespace AutoTest.VSIX
{
    public partial class ATPackage
    {
        private Events _events;
        private DTEEvents _dteEvents;
        private BuildEvents _buildEvents;
        private SolutionEvents _solutionEvents;

        private void bindEvents()
        {
            _events = _applicationObject.Events;

            bindDTEEvents();
            bindBuildEvents();
            bindSolutionEvents();
        }

        private void bindDTEEvents()
        {
            _dteEvents = _events.DTEEvents;
            _dteEvents.OnStartupComplete += _dteEvents_OnStartupComplete;
        }

        private void bindBuildEvents()
        {
            _buildEvents = _events.BuildEvents;
            _buildEvents.OnBuildDone += _buildEvents_OnBuildDone;
        }

        private void bindSolutionEvents()
        {
            _solutionEvents = _events.SolutionEvents;

            _solutionEvents.Opened += _solutionEvents_Opened;
            _solutionEvents.AfterClosing += _solutionEvents_AfterClosing;

            // TODO CF from NL: implement based on ConnectEvents.cs
        }


        private void _dteEvents_OnStartupComplete()
        {
            //MessageBox.Show("_dteEvents_OnStartupComplete handler");
            //throw new NotImplementedException();
            // the event such as Connect.onConnection()
        }

        private void _buildEvents_OnBuildDone(vsBuildScope Scope, vsBuildAction Action)
        {
            var succeeded = _applicationObject.Solution.SolutionBuild.LastBuildInfo == 0;
            //if (_buildRunner != null)
            //{
            _buildRunner.PusblishBuildErrors();
            //}
            // TODO CF from NL: fix testing

        }

        private void _solutionEvents_Opened()
        {
            try
            {
                StartFeedbackWindow();

                _WatchToken = _applicationObject.Solution.FullName;
                SetEngine();
                _engine.Bootstrap(_WatchToken);

                if (_engine.IsRunning)
                    _control.SetText("Engine is running and waiting for changes");
                else
                    _control.SetText("Engine is paused and will not detect changes");
            }
            catch (Exception exception)
            {
                Debug.WriteException(exception);
            }
        }

        private void StartFeedbackWindow()
        {
            var commandId = new System.ComponentModel.Design.CommandID(GuidList.guidATPackageCmdSet, (int)PackageCommands.FeedbackWindowCommandId);
            MenuCommandService.GlobalInvoke(commandId);
        }

        private void _solutionEvents_AfterClosing()
        {
            try
            {
                _engine.Shutdown();
            }
            catch (Exception exception)
            {
                Debug.WriteException(exception);
            }
            // TODO CF from NL: Check if this works
        }
    }
}
