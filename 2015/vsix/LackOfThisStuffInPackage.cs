using System;
using System.Linq;
using Extensibility;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.CommandBars;
using System.Resources;
using System.Globalization;
using System.Reflection;
using AutoTest.Core.DebugLog;
using AutoTest.VS.Util.Menues;
using AutoTest.VS.Util.CommandHandling;
using AutoTest.VSAddin.CommandHandling;
using System.IO;
using AutoTest.VS.Util.Builds;
using AutoTest.Messages;
using AutoTest.Core.Configuration;
using AutoTest.Core.FileSystem;
using AutoTest.Messages.FileStorage;

namespace AutoTest.VSAddin
{
    public class LackOfThisStuffInPackage
    {
        private readonly CommandDispatcher _dispatchers = new CommandDispatcher();
        private static Action _onCompletedOnDemandRun = null; // Used for resetting on demand runner and resuming engine when in auto mode

        private static bool buildManually();

        public LackOfThisStuffInPackage()
        {


        }
    }
}