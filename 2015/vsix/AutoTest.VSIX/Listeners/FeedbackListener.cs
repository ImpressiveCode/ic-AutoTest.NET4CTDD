using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoTest.Messages;

namespace AutoTest.VSIX.Listeners
{
    class FeedbackListener : IMessageForwarder
    {
        private FeedbackWindow _control;

        public FeedbackListener(FeedbackWindow control)
        {
            _control = control;
        }

        public void Forward(object message)
        {
            _control.Consume(message);
        }
    }
}
