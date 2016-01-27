using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoTest.Messages;

namespace AutoTest.VSIX.Listeners
{
    class FeedbackListener : IMessageForwarder
    {
        private NewFeedbackWindowControl _window;

        public FeedbackListener(NewFeedbackWindowControl window)
        {
            _window = window;
        }

        public void Forward(object message)
        {
            _window.Consume(message);
        }
    }
}
