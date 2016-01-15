using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTest.VSIX
{
    public static class PackageCommands
    {
        public const int FeedbackWindowCommandId = 0x1101;
        public const int RestartEngineCommandId = 0x1102;
        public const int ResumeEngineCommandId = 0x1103;
        public const int PauseEngineCommandId = 0x1104;
        public const int BuildAndTestAllProjectsCommandId = 0x1105;

        public const int cmdidMyFeedbackWindowCommand  = 4358;

    }
}
