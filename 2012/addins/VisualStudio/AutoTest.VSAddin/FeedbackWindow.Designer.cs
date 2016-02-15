﻿namespace AutoTest.VSAddin
{
    partial class FeedbackWindow
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.runFeedback = new AutoTest.UI.RunFeedback();
            this.SuspendLayout();
            // 
            // runFeedback
            // 
            this.runFeedback.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
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
            // 
            // FeedbackWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.runFeedback);
            this.Name = "FeedbackWindow";
            this.Size = new System.Drawing.Size(414, 103);
            this.Resize += new System.EventHandler(this.FeedbackWindow_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private UI.RunFeedback runFeedback;

    }
}
