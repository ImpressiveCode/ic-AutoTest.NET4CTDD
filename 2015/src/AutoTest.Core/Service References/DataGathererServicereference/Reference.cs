﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.586
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AutoTest.Core.DataGathererServicereference {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="LogData", Namespace="http://schemas.datacontract.org/2004/07/AutoTest.Net.DataGatherer")]
    [System.SerializableAttribute()]
    public partial class LogData : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool AutoTestEngineRunningField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string AutoTestNetVersionNumberField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int NumberOfBuildsFailedField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int NumberOfBuildsSucceededField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int NumberOfProjectsBuiltField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int NumberOfTestsFailedField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int NumberOfTestsIgnoredField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int NumberOfTestsPassedField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int NumberOfTestsRanField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string SolutionNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string StationNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string TestRunnerNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime TimeStampField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string UserNameField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool AutoTestEngineRunning {
            get {
                return this.AutoTestEngineRunningField;
            }
            set {
                if ((this.AutoTestEngineRunningField.Equals(value) != true)) {
                    this.AutoTestEngineRunningField = value;
                    this.RaisePropertyChanged("AutoTestEngineRunning");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string AutoTestNetVersionNumber {
            get {
                return this.AutoTestNetVersionNumberField;
            }
            set {
                if ((object.ReferenceEquals(this.AutoTestNetVersionNumberField, value) != true)) {
                    this.AutoTestNetVersionNumberField = value;
                    this.RaisePropertyChanged("AutoTestNetVersionNumber");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int NumberOfBuildsFailed {
            get {
                return this.NumberOfBuildsFailedField;
            }
            set {
                if ((this.NumberOfBuildsFailedField.Equals(value) != true)) {
                    this.NumberOfBuildsFailedField = value;
                    this.RaisePropertyChanged("NumberOfBuildsFailed");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int NumberOfBuildsSucceeded {
            get {
                return this.NumberOfBuildsSucceededField;
            }
            set {
                if ((this.NumberOfBuildsSucceededField.Equals(value) != true)) {
                    this.NumberOfBuildsSucceededField = value;
                    this.RaisePropertyChanged("NumberOfBuildsSucceeded");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int NumberOfProjectsBuilt {
            get {
                return this.NumberOfProjectsBuiltField;
            }
            set {
                if ((this.NumberOfProjectsBuiltField.Equals(value) != true)) {
                    this.NumberOfProjectsBuiltField = value;
                    this.RaisePropertyChanged("NumberOfProjectsBuilt");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int NumberOfTestsFailed {
            get {
                return this.NumberOfTestsFailedField;
            }
            set {
                if ((this.NumberOfTestsFailedField.Equals(value) != true)) {
                    this.NumberOfTestsFailedField = value;
                    this.RaisePropertyChanged("NumberOfTestsFailed");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int NumberOfTestsIgnored {
            get {
                return this.NumberOfTestsIgnoredField;
            }
            set {
                if ((this.NumberOfTestsIgnoredField.Equals(value) != true)) {
                    this.NumberOfTestsIgnoredField = value;
                    this.RaisePropertyChanged("NumberOfTestsIgnored");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int NumberOfTestsPassed {
            get {
                return this.NumberOfTestsPassedField;
            }
            set {
                if ((this.NumberOfTestsPassedField.Equals(value) != true)) {
                    this.NumberOfTestsPassedField = value;
                    this.RaisePropertyChanged("NumberOfTestsPassed");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int NumberOfTestsRan {
            get {
                return this.NumberOfTestsRanField;
            }
            set {
                if ((this.NumberOfTestsRanField.Equals(value) != true)) {
                    this.NumberOfTestsRanField = value;
                    this.RaisePropertyChanged("NumberOfTestsRan");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string SolutionName {
            get {
                return this.SolutionNameField;
            }
            set {
                if ((object.ReferenceEquals(this.SolutionNameField, value) != true)) {
                    this.SolutionNameField = value;
                    this.RaisePropertyChanged("SolutionName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string StationName {
            get {
                return this.StationNameField;
            }
            set {
                if ((object.ReferenceEquals(this.StationNameField, value) != true)) {
                    this.StationNameField = value;
                    this.RaisePropertyChanged("StationName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string TestRunnerName {
            get {
                return this.TestRunnerNameField;
            }
            set {
                if ((object.ReferenceEquals(this.TestRunnerNameField, value) != true)) {
                    this.TestRunnerNameField = value;
                    this.RaisePropertyChanged("TestRunnerName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime TimeStamp {
            get {
                return this.TimeStampField;
            }
            set {
                if ((this.TimeStampField.Equals(value) != true)) {
                    this.TimeStampField = value;
                    this.RaisePropertyChanged("TimeStamp");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string UserName {
            get {
                return this.UserNameField;
            }
            set {
                if ((object.ReferenceEquals(this.UserNameField, value) != true)) {
                    this.UserNameField = value;
                    this.RaisePropertyChanged("UserName");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="DataGathererServicereference.IService")]
    public interface IService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/AddLogDump", ReplyAction="http://tempuri.org/IService/AddLogDumpResponse")]
        void AddLogDump(AutoTest.Core.DataGathererServicereference.LogData data);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IServiceChannel : AutoTest.Core.DataGathererServicereference.IService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ServiceClient : System.ServiceModel.ClientBase<AutoTest.Core.DataGathererServicereference.IService>, AutoTest.Core.DataGathererServicereference.IService {
        
        public ServiceClient() {
        }
        
        public ServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public void AddLogDump(AutoTest.Core.DataGathererServicereference.LogData data) {
            base.Channel.AddLogDump(data);
        }
    }
}
