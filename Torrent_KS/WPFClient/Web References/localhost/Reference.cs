﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.42000.
// 
#pragma warning disable 1591

namespace WPFClient.localhost {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    using System.Data;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2556.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="WebServiceSoap", Namespace="http://tempuri.org/")]
    public partial class WebService : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback checkUserOperationCompleted;
        
        private System.Threading.SendOrPostCallback manageUserListOperationCompleted;
        
        private System.Threading.SendOrPostCallback SignInDetailsOperationCompleted;
        
        private System.Threading.SendOrPostCallback InsertFilesRecordOperationCompleted;
        
        private System.Threading.SendOrPostCallback ConnectionOperationCompleted;
        
        private System.Threading.SendOrPostCallback IsUserAdminOperationCompleted;
        
        private System.Threading.SendOrPostCallback getFileListOperationCompleted;
        
        private System.Threading.SendOrPostCallback getSearchResultOperationCompleted;
        
        private System.Threading.SendOrPostCallback getCountClientsOperationCompleted;
        
        private System.Threading.SendOrPostCallback getCountFilesOperationCompleted;
        
        private System.Threading.SendOrPostCallback getConnectedClientsOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public WebService() {
            this.Url = global::WPFClient.Properties.Settings.Default.WPFClient_localhost_WebService;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event checkUserCompletedEventHandler checkUserCompleted;
        
        /// <remarks/>
        public event manageUserListCompletedEventHandler manageUserListCompleted;
        
        /// <remarks/>
        public event SignInDetailsCompletedEventHandler SignInDetailsCompleted;
        
        /// <remarks/>
        public event InsertFilesRecordCompletedEventHandler InsertFilesRecordCompleted;
        
        /// <remarks/>
        public event ConnectionCompletedEventHandler ConnectionCompleted;
        
        /// <remarks/>
        public event IsUserAdminCompletedEventHandler IsUserAdminCompleted;
        
        /// <remarks/>
        public event getFileListCompletedEventHandler getFileListCompleted;
        
        /// <remarks/>
        public event getSearchResultCompletedEventHandler getSearchResultCompleted;
        
        /// <remarks/>
        public event getCountClientsCompletedEventHandler getCountClientsCompleted;
        
        /// <remarks/>
        public event getCountFilesCompletedEventHandler getCountFilesCompleted;
        
        /// <remarks/>
        public event getConnectedClientsCompletedEventHandler getConnectedClientsCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/checkUser", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public int checkUser(string UserName, string Password, string reqType) {
            object[] results = this.Invoke("checkUser", new object[] {
                        UserName,
                        Password,
                        reqType});
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public void checkUserAsync(string UserName, string Password, string reqType) {
            this.checkUserAsync(UserName, Password, reqType, null);
        }
        
        /// <remarks/>
        public void checkUserAsync(string UserName, string Password, string reqType, object userState) {
            if ((this.checkUserOperationCompleted == null)) {
                this.checkUserOperationCompleted = new System.Threading.SendOrPostCallback(this.OncheckUserOperationCompleted);
            }
            this.InvokeAsync("checkUser", new object[] {
                        UserName,
                        Password,
                        reqType}, this.checkUserOperationCompleted, userState);
        }
        
        private void OncheckUserOperationCompleted(object arg) {
            if ((this.checkUserCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.checkUserCompleted(this, new checkUserCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/manageUserList", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void manageUserList(string str1, string str2, int id, string reqType) {
            this.Invoke("manageUserList", new object[] {
                        str1,
                        str2,
                        id,
                        reqType});
        }
        
        /// <remarks/>
        public void manageUserListAsync(string str1, string str2, int id, string reqType) {
            this.manageUserListAsync(str1, str2, id, reqType, null);
        }
        
        /// <remarks/>
        public void manageUserListAsync(string str1, string str2, int id, string reqType, object userState) {
            if ((this.manageUserListOperationCompleted == null)) {
                this.manageUserListOperationCompleted = new System.Threading.SendOrPostCallback(this.OnmanageUserListOperationCompleted);
            }
            this.InvokeAsync("manageUserList", new object[] {
                        str1,
                        str2,
                        id,
                        reqType}, this.manageUserListOperationCompleted, userState);
        }
        
        private void OnmanageUserListOperationCompleted(object arg) {
            if ((this.manageUserListCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.manageUserListCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/SignInDetails", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void SignInDetails(string userName, string password, string IP, int port, string path) {
            this.Invoke("SignInDetails", new object[] {
                        userName,
                        password,
                        IP,
                        port,
                        path});
        }
        
        /// <remarks/>
        public void SignInDetailsAsync(string userName, string password, string IP, int port, string path) {
            this.SignInDetailsAsync(userName, password, IP, port, path, null);
        }
        
        /// <remarks/>
        public void SignInDetailsAsync(string userName, string password, string IP, int port, string path, object userState) {
            if ((this.SignInDetailsOperationCompleted == null)) {
                this.SignInDetailsOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSignInDetailsOperationCompleted);
            }
            this.InvokeAsync("SignInDetails", new object[] {
                        userName,
                        password,
                        IP,
                        port,
                        path}, this.SignInDetailsOperationCompleted, userState);
        }
        
        private void OnSignInDetailsOperationCompleted(object arg) {
            if ((this.SignInDetailsCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SignInDetailsCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/InsertFilesRecord", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void InsertFilesRecord(string fileName, int size, string IP, int port, string path) {
            this.Invoke("InsertFilesRecord", new object[] {
                        fileName,
                        size,
                        IP,
                        port,
                        path});
        }
        
        /// <remarks/>
        public void InsertFilesRecordAsync(string fileName, int size, string IP, int port, string path) {
            this.InsertFilesRecordAsync(fileName, size, IP, port, path, null);
        }
        
        /// <remarks/>
        public void InsertFilesRecordAsync(string fileName, int size, string IP, int port, string path, object userState) {
            if ((this.InsertFilesRecordOperationCompleted == null)) {
                this.InsertFilesRecordOperationCompleted = new System.Threading.SendOrPostCallback(this.OnInsertFilesRecordOperationCompleted);
            }
            this.InvokeAsync("InsertFilesRecord", new object[] {
                        fileName,
                        size,
                        IP,
                        port,
                        path}, this.InsertFilesRecordOperationCompleted, userState);
        }
        
        private void OnInsertFilesRecordOperationCompleted(object arg) {
            if ((this.InsertFilesRecordCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.InsertFilesRecordCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/Connection", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void Connection(string userName, string password, string status) {
            this.Invoke("Connection", new object[] {
                        userName,
                        password,
                        status});
        }
        
        /// <remarks/>
        public void ConnectionAsync(string userName, string password, string status) {
            this.ConnectionAsync(userName, password, status, null);
        }
        
        /// <remarks/>
        public void ConnectionAsync(string userName, string password, string status, object userState) {
            if ((this.ConnectionOperationCompleted == null)) {
                this.ConnectionOperationCompleted = new System.Threading.SendOrPostCallback(this.OnConnectionOperationCompleted);
            }
            this.InvokeAsync("Connection", new object[] {
                        userName,
                        password,
                        status}, this.ConnectionOperationCompleted, userState);
        }
        
        private void OnConnectionOperationCompleted(object arg) {
            if ((this.ConnectionCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ConnectionCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/IsUserAdmin", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public int IsUserAdmin(string userName, string password) {
            object[] results = this.Invoke("IsUserAdmin", new object[] {
                        userName,
                        password});
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public void IsUserAdminAsync(string userName, string password) {
            this.IsUserAdminAsync(userName, password, null);
        }
        
        /// <remarks/>
        public void IsUserAdminAsync(string userName, string password, object userState) {
            if ((this.IsUserAdminOperationCompleted == null)) {
                this.IsUserAdminOperationCompleted = new System.Threading.SendOrPostCallback(this.OnIsUserAdminOperationCompleted);
            }
            this.InvokeAsync("IsUserAdmin", new object[] {
                        userName,
                        password}, this.IsUserAdminOperationCompleted, userState);
        }
        
        private void OnIsUserAdminOperationCompleted(object arg) {
            if ((this.IsUserAdminCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.IsUserAdminCompleted(this, new IsUserAdminCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/getFileList", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataSet getFileList() {
            object[] results = this.Invoke("getFileList", new object[0]);
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        public void getFileListAsync() {
            this.getFileListAsync(null);
        }
        
        /// <remarks/>
        public void getFileListAsync(object userState) {
            if ((this.getFileListOperationCompleted == null)) {
                this.getFileListOperationCompleted = new System.Threading.SendOrPostCallback(this.OngetFileListOperationCompleted);
            }
            this.InvokeAsync("getFileList", new object[0], this.getFileListOperationCompleted, userState);
        }
        
        private void OngetFileListOperationCompleted(object arg) {
            if ((this.getFileListCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.getFileListCompleted(this, new getFileListCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/getSearchResult", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataSet getSearchResult(string name) {
            object[] results = this.Invoke("getSearchResult", new object[] {
                        name});
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        public void getSearchResultAsync(string name) {
            this.getSearchResultAsync(name, null);
        }
        
        /// <remarks/>
        public void getSearchResultAsync(string name, object userState) {
            if ((this.getSearchResultOperationCompleted == null)) {
                this.getSearchResultOperationCompleted = new System.Threading.SendOrPostCallback(this.OngetSearchResultOperationCompleted);
            }
            this.InvokeAsync("getSearchResult", new object[] {
                        name}, this.getSearchResultOperationCompleted, userState);
        }
        
        private void OngetSearchResultOperationCompleted(object arg) {
            if ((this.getSearchResultCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.getSearchResultCompleted(this, new getSearchResultCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/getCountClients", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public int getCountClients() {
            object[] results = this.Invoke("getCountClients", new object[0]);
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public void getCountClientsAsync() {
            this.getCountClientsAsync(null);
        }
        
        /// <remarks/>
        public void getCountClientsAsync(object userState) {
            if ((this.getCountClientsOperationCompleted == null)) {
                this.getCountClientsOperationCompleted = new System.Threading.SendOrPostCallback(this.OngetCountClientsOperationCompleted);
            }
            this.InvokeAsync("getCountClients", new object[0], this.getCountClientsOperationCompleted, userState);
        }
        
        private void OngetCountClientsOperationCompleted(object arg) {
            if ((this.getCountClientsCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.getCountClientsCompleted(this, new getCountClientsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/getCountFiles", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public int getCountFiles() {
            object[] results = this.Invoke("getCountFiles", new object[0]);
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public void getCountFilesAsync() {
            this.getCountFilesAsync(null);
        }
        
        /// <remarks/>
        public void getCountFilesAsync(object userState) {
            if ((this.getCountFilesOperationCompleted == null)) {
                this.getCountFilesOperationCompleted = new System.Threading.SendOrPostCallback(this.OngetCountFilesOperationCompleted);
            }
            this.InvokeAsync("getCountFiles", new object[0], this.getCountFilesOperationCompleted, userState);
        }
        
        private void OngetCountFilesOperationCompleted(object arg) {
            if ((this.getCountFilesCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.getCountFilesCompleted(this, new getCountFilesCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/getConnectedClients", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public int getConnectedClients() {
            object[] results = this.Invoke("getConnectedClients", new object[0]);
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public void getConnectedClientsAsync() {
            this.getConnectedClientsAsync(null);
        }
        
        /// <remarks/>
        public void getConnectedClientsAsync(object userState) {
            if ((this.getConnectedClientsOperationCompleted == null)) {
                this.getConnectedClientsOperationCompleted = new System.Threading.SendOrPostCallback(this.OngetConnectedClientsOperationCompleted);
            }
            this.InvokeAsync("getConnectedClients", new object[0], this.getConnectedClientsOperationCompleted, userState);
        }
        
        private void OngetConnectedClientsOperationCompleted(object arg) {
            if ((this.getConnectedClientsCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.getConnectedClientsCompleted(this, new getConnectedClientsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2556.0")]
    public delegate void checkUserCompletedEventHandler(object sender, checkUserCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2556.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class checkUserCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal checkUserCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public int Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((int)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2556.0")]
    public delegate void manageUserListCompletedEventHandler(object sender, System.ComponentModel.AsyncCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2556.0")]
    public delegate void SignInDetailsCompletedEventHandler(object sender, System.ComponentModel.AsyncCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2556.0")]
    public delegate void InsertFilesRecordCompletedEventHandler(object sender, System.ComponentModel.AsyncCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2556.0")]
    public delegate void ConnectionCompletedEventHandler(object sender, System.ComponentModel.AsyncCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2556.0")]
    public delegate void IsUserAdminCompletedEventHandler(object sender, IsUserAdminCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2556.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class IsUserAdminCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal IsUserAdminCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public int Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((int)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2556.0")]
    public delegate void getFileListCompletedEventHandler(object sender, getFileListCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2556.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class getFileListCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal getFileListCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public System.Data.DataSet Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Data.DataSet)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2556.0")]
    public delegate void getSearchResultCompletedEventHandler(object sender, getSearchResultCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2556.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class getSearchResultCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal getSearchResultCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public System.Data.DataSet Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Data.DataSet)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2556.0")]
    public delegate void getCountClientsCompletedEventHandler(object sender, getCountClientsCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2556.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class getCountClientsCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal getCountClientsCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public int Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((int)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2556.0")]
    public delegate void getCountFilesCompletedEventHandler(object sender, getCountFilesCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2556.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class getCountFilesCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal getCountFilesCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public int Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((int)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2556.0")]
    public delegate void getConnectedClientsCompletedEventHandler(object sender, getConnectedClientsCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2556.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class getConnectedClientsCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal getConnectedClientsCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public int Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((int)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591