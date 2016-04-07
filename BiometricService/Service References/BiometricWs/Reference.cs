﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BiometricService.BiometricWs {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://ws.umeca.com", ConfigurationName="BiometricWs.BiometricWSPortType")]
    public interface BiometricWSPortType {
        
        // CODEGEN: Generating message contract since the operation getUsersFromDB is neither RPC nor document wrapped.
        [System.ServiceModel.OperationContractAttribute(Action="urn:getUsersFromDB", ReplyAction="urn:getUsersFromDBResponse")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        BiometricService.BiometricWs.getUsersFromDBResponse getUsersFromDB(BiometricService.BiometricWs.getUsersFromDBRequest request);
        
        // CODEGEN: Parameter 'return' requires additional schema information that cannot be captured using the parameter mode. The specific attribute is 'System.Xml.Serialization.XmlElementAttribute'.
        [System.ServiceModel.OperationContractAttribute(Action="urn:updateImputedFingerPrint", ReplyAction="urn:updateImputedFingerPrintResponse")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="return")]
        BiometricService.BiometricWs.updateImputedFingerPrintResponse updateImputedFingerPrint(BiometricService.BiometricWs.updateImputedFingerPrintRequest request);
        
        // CODEGEN: Parameter 'return' requires additional schema information that cannot be captured using the parameter mode. The specific attribute is 'System.Xml.Serialization.XmlElementAttribute'.
        [System.ServiceModel.OperationContractAttribute(Action="urn:updateAttendanceLogs", ReplyAction="urn:updateAttendanceLogsResponse")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="return")]
        BiometricService.BiometricWs.updateAttendanceLogsResponse updateAttendanceLogs(BiometricService.BiometricWs.updateAttendanceLogsRequest request);
        
        // CODEGEN: Parameter 'return' requires additional schema information that cannot be captured using the parameter mode. The specific attribute is 'System.Xml.Serialization.XmlElementAttribute'.
        [System.ServiceModel.OperationContractAttribute(Action="urn:getDevices", ReplyAction="urn:getDevicesResponse")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="return")]
        BiometricService.BiometricWs.getDevicesResponse getDevices(BiometricService.BiometricWs.getDevicesRequest request);
        
        // CODEGEN: Parameter 'return' requires additional schema information that cannot be captured using the parameter mode. The specific attribute is 'System.Xml.Serialization.XmlElementAttribute'.
        [System.ServiceModel.OperationContractAttribute(Action="urn:updateUserFingerPrint", ReplyAction="urn:updateUserFingerPrintResponse")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="return")]
        BiometricService.BiometricWs.updateUserFingerPrintResponse updateUserFingerPrint(BiometricService.BiometricWs.updateUserFingerPrintRequest request);
        
        // CODEGEN: Parameter 'return' requires additional schema information that cannot be captured using the parameter mode. The specific attribute is 'System.Xml.Serialization.XmlElementAttribute'.
        [System.ServiceModel.OperationContractAttribute(Action="urn:getImputed", ReplyAction="urn:getImputedResponse")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="return")]
        BiometricService.BiometricWs.getImputedResponse getImputed(BiometricService.BiometricWs.getImputedRequest request);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://model.infrastructure.umeca.com/xsd")]
    public partial class ResponseMessage : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string dataField;
        
        private bool hasErrorField;
        
        private bool hasErrorFieldSpecified;
        
        private string messageField;
        
        private object returnDataField;
        
        private string titleField;
        
        private string urlToGoField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=0)]
        public string data {
            get {
                return this.dataField;
            }
            set {
                this.dataField = value;
                this.RaisePropertyChanged("data");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public bool hasError {
            get {
                return this.hasErrorField;
            }
            set {
                this.hasErrorField = value;
                this.RaisePropertyChanged("hasError");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool hasErrorSpecified {
            get {
                return this.hasErrorFieldSpecified;
            }
            set {
                this.hasErrorFieldSpecified = value;
                this.RaisePropertyChanged("hasErrorSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=2)]
        public string message {
            get {
                return this.messageField;
            }
            set {
                this.messageField = value;
                this.RaisePropertyChanged("message");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=3)]
        public object returnData {
            get {
                return this.returnDataField;
            }
            set {
                this.returnDataField = value;
                this.RaisePropertyChanged("returnData");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=4)]
        public string title {
            get {
                return this.titleField;
            }
            set {
                this.titleField = value;
                this.RaisePropertyChanged("title");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=5)]
        public string urlToGo {
            get {
                return this.urlToGoField;
            }
            set {
                this.urlToGoField = value;
                this.RaisePropertyChanged("urlToGo");
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
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class getUsersFromDBRequest {
        
        public getUsersFromDBRequest() {
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="getUsersFromDBResponse", WrapperNamespace="http://ws.umeca.com", IsWrapped=true)]
    public partial class getUsersFromDBResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://ws.umeca.com", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public BiometricService.BiometricWs.ResponseMessage @return;
        
        public getUsersFromDBResponse() {
        }
        
        public getUsersFromDBResponse(BiometricService.BiometricWs.ResponseMessage @return) {
            this.@return = @return;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="updateImputedFingerPrint", WrapperNamespace="http://ws.umeca.com", IsWrapped=true)]
    public partial class updateImputedFingerPrintRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://ws.umeca.com", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string enrollNumber;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://ws.umeca.com", Order=1)]
        public int finger;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://ws.umeca.com", Order=2)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string fingerPrint;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://ws.umeca.com", Order=3)]
        public int operation;
        
        public updateImputedFingerPrintRequest() {
        }
        
        public updateImputedFingerPrintRequest(string enrollNumber, int finger, string fingerPrint, int operation) {
            this.enrollNumber = enrollNumber;
            this.finger = finger;
            this.fingerPrint = fingerPrint;
            this.operation = operation;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="updateImputedFingerPrintResponse", WrapperNamespace="http://ws.umeca.com", IsWrapped=true)]
    public partial class updateImputedFingerPrintResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://ws.umeca.com", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public BiometricService.BiometricWs.ResponseMessage @return;
        
        public updateImputedFingerPrintResponse() {
        }
        
        public updateImputedFingerPrintResponse(BiometricService.BiometricWs.ResponseMessage @return) {
            this.@return = @return;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="updateAttendanceLogs", WrapperNamespace="http://ws.umeca.com", IsWrapped=true)]
    public partial class updateAttendanceLogsRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://ws.umeca.com", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string logsList;
        
        public updateAttendanceLogsRequest() {
        }
        
        public updateAttendanceLogsRequest(string logsList) {
            this.logsList = logsList;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="updateAttendanceLogsResponse", WrapperNamespace="http://ws.umeca.com", IsWrapped=true)]
    public partial class updateAttendanceLogsResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://ws.umeca.com", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public BiometricService.BiometricWs.ResponseMessage @return;
        
        public updateAttendanceLogsResponse() {
        }
        
        public updateAttendanceLogsResponse(BiometricService.BiometricWs.ResponseMessage @return) {
            this.@return = @return;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="getDevices", WrapperNamespace="http://ws.umeca.com", IsWrapped=true)]
    public partial class getDevicesRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://ws.umeca.com", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string deviceUse;
        
        public getDevicesRequest() {
        }
        
        public getDevicesRequest(string deviceUse) {
            this.deviceUse = deviceUse;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="getDevicesResponse", WrapperNamespace="http://ws.umeca.com", IsWrapped=true)]
    public partial class getDevicesResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://ws.umeca.com", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public BiometricService.BiometricWs.ResponseMessage @return;
        
        public getDevicesResponse() {
        }
        
        public getDevicesResponse(BiometricService.BiometricWs.ResponseMessage @return) {
            this.@return = @return;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="updateUserFingerPrint", WrapperNamespace="http://ws.umeca.com", IsWrapped=true)]
    public partial class updateUserFingerPrintRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://ws.umeca.com", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string enrollNumber;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://ws.umeca.com", Order=1)]
        public int finger;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://ws.umeca.com", Order=2)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public string fingerPrint;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://ws.umeca.com", Order=3)]
        public int operation;
        
        public updateUserFingerPrintRequest() {
        }
        
        public updateUserFingerPrintRequest(string enrollNumber, int finger, string fingerPrint, int operation) {
            this.enrollNumber = enrollNumber;
            this.finger = finger;
            this.fingerPrint = fingerPrint;
            this.operation = operation;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="updateUserFingerPrintResponse", WrapperNamespace="http://ws.umeca.com", IsWrapped=true)]
    public partial class updateUserFingerPrintResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://ws.umeca.com", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public BiometricService.BiometricWs.ResponseMessage @return;
        
        public updateUserFingerPrintResponse() {
        }
        
        public updateUserFingerPrintResponse(BiometricService.BiometricWs.ResponseMessage @return) {
            this.@return = @return;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="getImputed", WrapperNamespace="http://ws.umeca.com", IsWrapped=true)]
    public partial class getImputedRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://ws.umeca.com", Order=0)]
        public long imputed;
        
        public getImputedRequest() {
        }
        
        public getImputedRequest(long imputed) {
            this.imputed = imputed;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="getImputedResponse", WrapperNamespace="http://ws.umeca.com", IsWrapped=true)]
    public partial class getImputedResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://ws.umeca.com", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public BiometricService.BiometricWs.ResponseMessage @return;
        
        public getImputedResponse() {
        }
        
        public getImputedResponse(BiometricService.BiometricWs.ResponseMessage @return) {
            this.@return = @return;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface BiometricWSPortTypeChannel : BiometricService.BiometricWs.BiometricWSPortType, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class BiometricWSPortTypeClient : System.ServiceModel.ClientBase<BiometricService.BiometricWs.BiometricWSPortType>, BiometricService.BiometricWs.BiometricWSPortType {
        
        public BiometricWSPortTypeClient() {
        }
        
        public BiometricWSPortTypeClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public BiometricWSPortTypeClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public BiometricWSPortTypeClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public BiometricWSPortTypeClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        BiometricService.BiometricWs.getUsersFromDBResponse BiometricService.BiometricWs.BiometricWSPortType.getUsersFromDB(BiometricService.BiometricWs.getUsersFromDBRequest request) {
            return base.Channel.getUsersFromDB(request);
        }
        
        public BiometricService.BiometricWs.ResponseMessage getUsersFromDB() {
            BiometricService.BiometricWs.getUsersFromDBRequest inValue = new BiometricService.BiometricWs.getUsersFromDBRequest();
            BiometricService.BiometricWs.getUsersFromDBResponse retVal = ((BiometricService.BiometricWs.BiometricWSPortType)(this)).getUsersFromDB(inValue);
            return retVal.@return;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        BiometricService.BiometricWs.updateImputedFingerPrintResponse BiometricService.BiometricWs.BiometricWSPortType.updateImputedFingerPrint(BiometricService.BiometricWs.updateImputedFingerPrintRequest request) {
            return base.Channel.updateImputedFingerPrint(request);
        }
        
        public BiometricService.BiometricWs.ResponseMessage updateImputedFingerPrint(string enrollNumber, int finger, string fingerPrint, int operation) {
            BiometricService.BiometricWs.updateImputedFingerPrintRequest inValue = new BiometricService.BiometricWs.updateImputedFingerPrintRequest();
            inValue.enrollNumber = enrollNumber;
            inValue.finger = finger;
            inValue.fingerPrint = fingerPrint;
            inValue.operation = operation;
            BiometricService.BiometricWs.updateImputedFingerPrintResponse retVal = ((BiometricService.BiometricWs.BiometricWSPortType)(this)).updateImputedFingerPrint(inValue);
            return retVal.@return;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        BiometricService.BiometricWs.updateAttendanceLogsResponse BiometricService.BiometricWs.BiometricWSPortType.updateAttendanceLogs(BiometricService.BiometricWs.updateAttendanceLogsRequest request) {
            return base.Channel.updateAttendanceLogs(request);
        }
        
        public BiometricService.BiometricWs.ResponseMessage updateAttendanceLogs(string logsList) {
            BiometricService.BiometricWs.updateAttendanceLogsRequest inValue = new BiometricService.BiometricWs.updateAttendanceLogsRequest();
            inValue.logsList = logsList;
            BiometricService.BiometricWs.updateAttendanceLogsResponse retVal = ((BiometricService.BiometricWs.BiometricWSPortType)(this)).updateAttendanceLogs(inValue);
            return retVal.@return;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        BiometricService.BiometricWs.getDevicesResponse BiometricService.BiometricWs.BiometricWSPortType.getDevices(BiometricService.BiometricWs.getDevicesRequest request) {
            return base.Channel.getDevices(request);
        }
        
        public BiometricService.BiometricWs.ResponseMessage getDevices(string deviceUse) {
            BiometricService.BiometricWs.getDevicesRequest inValue = new BiometricService.BiometricWs.getDevicesRequest();
            inValue.deviceUse = deviceUse;
            BiometricService.BiometricWs.getDevicesResponse retVal = ((BiometricService.BiometricWs.BiometricWSPortType)(this)).getDevices(inValue);
            return retVal.@return;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        BiometricService.BiometricWs.updateUserFingerPrintResponse BiometricService.BiometricWs.BiometricWSPortType.updateUserFingerPrint(BiometricService.BiometricWs.updateUserFingerPrintRequest request) {
            return base.Channel.updateUserFingerPrint(request);
        }
        
        public BiometricService.BiometricWs.ResponseMessage updateUserFingerPrint(string enrollNumber, int finger, string fingerPrint, int operation) {
            BiometricService.BiometricWs.updateUserFingerPrintRequest inValue = new BiometricService.BiometricWs.updateUserFingerPrintRequest();
            inValue.enrollNumber = enrollNumber;
            inValue.finger = finger;
            inValue.fingerPrint = fingerPrint;
            inValue.operation = operation;
            BiometricService.BiometricWs.updateUserFingerPrintResponse retVal = ((BiometricService.BiometricWs.BiometricWSPortType)(this)).updateUserFingerPrint(inValue);
            return retVal.@return;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        BiometricService.BiometricWs.getImputedResponse BiometricService.BiometricWs.BiometricWSPortType.getImputed(BiometricService.BiometricWs.getImputedRequest request) {
            return base.Channel.getImputed(request);
        }
        
        public BiometricService.BiometricWs.ResponseMessage getImputed(long imputed) {
            BiometricService.BiometricWs.getImputedRequest inValue = new BiometricService.BiometricWs.getImputedRequest();
            inValue.imputed = imputed;
            BiometricService.BiometricWs.getImputedResponse retVal = ((BiometricService.BiometricWs.BiometricWSPortType)(this)).getImputed(inValue);
            return retVal.@return;
        }
    }
}
