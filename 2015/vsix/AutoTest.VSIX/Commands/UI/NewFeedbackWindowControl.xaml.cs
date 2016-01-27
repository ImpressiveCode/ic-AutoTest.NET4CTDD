//------------------------------------------------------------------------------
// <copyright file="MyFeedbackWindowControl.xaml.cs" company="Company">
//     Copyright (c) Company.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace AutoTest.VSIX
{
    using Core.DebugLog;
    using Core.Messaging;
    using EnvDTE;
    using EnvDTE80;
    using Messages;
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Windows;
    using System.Windows.Controls;
    using VS.Util.Navigation;
    /// <summary>
    /// Interaction logic for MyFeedbackWindowControl.
    /// </summary>
    public partial class NewFeedbackWindowControl : UserControl
    {
        private DTE2 _application;
        private IMessageBus _bus;

        private UI.RunFeedback runFeedback;

        public event EventHandler<UI.DebugTestArgs> DebugTest;
        /// <summary>
        /// Initializes a new instance of the <see cref="NewFeedbackWindowControl"/> class.
        /// </summary>
        public NewFeedbackWindowControl()
        {
            this.InitializeComponent();
            this.InitializeRunFeedback();
        }

        public void SetApplication(DTE2 application)
        {
            _application = application;
        }

        public void SetMessageBus(IMessageBus bus)
        {
            _bus = bus;
        }

        public void Consume(object message)
        {
            runFeedback.ConsumeMessage(message);
        }

        public void SetText(string text)
        {
            runFeedback.PrintMessage(new UI.RunMessages(UI.RunMessageType.Normal, text));
        }

        private void runFeedback_DebugTest(object sender, UI.DebugTestArgs e)
        {
            if (DebugTest != null)
                DebugTest(sender, e);
        }

        private void runFeedback_GoToReference(object sender, UI.GoToReferenceArgs e)
        {
            try
            {
                var window = _application.OpenFile(EnvDTE.Constants.vsViewKindCode, e.Position.File);
                window.Activate();
                var selection = (TextSelection)_application.ActiveDocument.Selection;
                selection.MoveToDisplayColumn(e.Position.LineNumber, e.Position.Column, false);
            }
            catch
            {
            }
        }

        private void runFeedback_GoToType(object sender, UI.GoToTypeArgs e)
        {
            try
            {
                new TypeNavigation().GoToType(_application, e.Assembly, e.TypeName);
            }
            catch (Exception exception)
            {
                Debug.WriteException(exception);
            }
        }

        public void Clear()
        {
            runFeedback.ClearList();
        }

        public void ClearBuilds(string project) // Note: build runner will probably use it
        {
            runFeedback.ClearBuilds(project);
        }

        private void FeedbackWindow_Resize(object sender, EventArgs e)
        {
            runFeedback.Resize();
        }

        private void runFeedback_CancelRun(object sender, EventArgs e)
        {
            _bus.Publish(new AbortMessage(""));
        }


        private void InitializeRunFeedback()
        {
            this.runFeedback = new AutoTest.UI.RunFeedback();
            // 
            // runFeedback
            // 
            this.runFeedback.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right);
            this.runFeedback.CanDebug = true;
            this.runFeedback.CanGoToTypes = true;
            this.runFeedback.ListViewWidthOffset = 0;
            this.runFeedback.Location = new System.Drawing.Point(0, 0);
            this.runFeedback.Name = "runFeedback";
            this.runFeedback.ShowIcon = true;
            this.runFeedback.ShowRunInformation = true;
            this.runFeedback.Size = new System.Drawing.Size(413, 101);
            this.runFeedback.TabIndex = 0;
            this.runFeedback.GoToReference += new System.EventHandler<AutoTest.UI.GoToReferenceArgs>(this.runFeedback_GoToReference);
            this.runFeedback.GoToType += new System.EventHandler<AutoTest.UI.GoToTypeArgs>(this.runFeedback_GoToType);
            this.runFeedback.DebugTest += new System.EventHandler<AutoTest.UI.DebugTestArgs>(this.runFeedback_DebugTest);
            this.runFeedback.CancelRun += new System.EventHandler(this.runFeedback_CancelRun);
        }

        /// <summary>
        /// Handles click on the button by displaying a message box.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event args.</param>
        [SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions", Justification = "Sample code")]
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Default event handler naming pattern")]
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(
                string.Format(System.Globalization.CultureInfo.CurrentUICulture, "Invoked '{0}'", this.ToString()),
                "MyFeedbackWindow");
        }
    }
}