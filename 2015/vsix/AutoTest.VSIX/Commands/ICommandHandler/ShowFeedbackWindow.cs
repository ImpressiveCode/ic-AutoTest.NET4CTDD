using System;
using EnvDTE;
using EnvDTE80;
using AutoTest.VS.Util.CommandHandling;

namespace AutoTest.VSIX
{
    public class ShowFeedbackWindow : ICommandHandler
    {
        private readonly DTE2 _applicationObject;
        private Action _showWindow;

        public ShowFeedbackWindow(DTE2 applicationObject, Action showWindow)
        {
            _applicationObject = applicationObject;
            _showWindow = showWindow;
        }

        public void Exec(vsCommandExecOption ExecuteOption, ref object VariantIn, ref object VariantOut, ref bool Handled)
        {
            _showWindow();
            Handled = true;
        }

        public void QueryStatus(vsCommandStatusTextWanted NeededText, ref vsCommandStatus StatusOption, ref object CommandText)
        {
            StatusOption = vsCommandStatus.vsCommandStatusSupported | vsCommandStatus.vsCommandStatusEnabled;
        }

        public string Name
        {
            get { return "AutoTest.VSAddin.Connect.AutoTestNet_FeedbackWindow"; }
        }
    }
}