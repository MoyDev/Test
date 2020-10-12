﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:4.0.30319.42000
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On

Imports System.Data

Namespace SRNService
    
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0"),  _
     System.ServiceModel.ServiceContractAttribute([Namespace]:="http://frontstep.com/IDOWebService", ConfigurationName:="SRNService.IDOWebServiceSoap")>  _
    Public Interface IDOWebServiceSoap
        
        <System.ServiceModel.OperationContractAttribute(Action:="http://frontstep.com/IDOWebService/GetConfigurationNames", ReplyAction:="*"),  _
         System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults:=true)>  _
        Function GetConfigurationNames() As String()
        
        <System.ServiceModel.OperationContractAttribute(Action:="http://frontstep.com/IDOWebService/CreateSessionToken", ReplyAction:="*"),  _
         System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults:=true)>  _
        Function CreateSessionToken(ByVal strUserId As String, ByVal strPswd As String, ByVal strConfig As String) As String
        
        <System.ServiceModel.OperationContractAttribute(Action:="http://frontstep.com/IDOWebService/LoadDataSet", ReplyAction:="*"),  _
         System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults:=true)>  _
        Function LoadDataSet(ByVal strSessionToken As String, ByVal strIDOName As String, ByVal strPropertyList As String, ByVal strFilter As String, ByVal strOrderBy As String, ByVal strPostQueryMethod As String, ByVal iRecordCap As Integer) As System.Data.DataSet
        
        <System.ServiceModel.OperationContractAttribute(Action:="http://frontstep.com/IDOWebService/SaveDataSet", ReplyAction:="*"),  _
         System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults:=true)>  _
        Function SaveDataSet(ByVal strSessionToken As String, ByVal updateDataSet As System.Data.DataSet, ByVal refreshAfterSave As Boolean, ByVal strCustomInsert As String, ByVal strCustomUpdate As String, ByVal strCustomDelete As String) As System.Data.DataSet
        
        <System.ServiceModel.OperationContractAttribute(Action:="http://frontstep.com/IDOWebService/CallMethod", ReplyAction:="*"),  _
         System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults:=true)>  _
        Function CallMethod(ByVal strSessionToken As String, ByVal strIDOName As String, ByVal strMethodName As String, ByRef strMethodParameters As String) As Object
        
        <System.ServiceModel.OperationContractAttribute(Action:="http://frontstep.com/IDOWebService/LoadJson", ReplyAction:="*"),  _
         System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults:=true)>  _
        Function LoadJson(ByVal strSessionToken As String, ByVal strIDOName As String, ByVal strPropertyList As String, ByVal strFilter As String, ByVal strOrderBy As String, ByVal strPostQueryMethod As String, ByVal iRecordCap As Integer) As String
        
        <System.ServiceModel.OperationContractAttribute(Action:="http://frontstep.com/IDOWebService/SaveJson", ReplyAction:="*"),  _
         System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults:=true)>  _
        Function SaveJson(ByVal strSessionToken As String, ByVal updateJsonObject As String, ByVal strCustomInsert As String, ByVal strCustomUpdate As String, ByVal strCustomDelete As String) As String
    End Interface
    
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")>  _
    Public Interface IDOWebServiceSoapChannel
        Inherits SRNService.IDOWebServiceSoap, System.ServiceModel.IClientChannel
    End Interface
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")>  _
    Partial Public Class DOWebServiceSoapClient
        Inherits System.ServiceModel.ClientBase(Of SRNService.IDOWebServiceSoap)
        Implements SRNService.IDOWebServiceSoap
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Public Sub New(ByVal endpointConfigurationName As String)
            MyBase.New(endpointConfigurationName)
        End Sub
        
        Public Sub New(ByVal endpointConfigurationName As String, ByVal remoteAddress As String)
            MyBase.New(endpointConfigurationName, remoteAddress)
        End Sub
        
        Public Sub New(ByVal endpointConfigurationName As String, ByVal remoteAddress As System.ServiceModel.EndpointAddress)
            MyBase.New(endpointConfigurationName, remoteAddress)
        End Sub
        
        Public Sub New(ByVal binding As System.ServiceModel.Channels.Binding, ByVal remoteAddress As System.ServiceModel.EndpointAddress)
            MyBase.New(binding, remoteAddress)
        End Sub
        
        Public Function GetConfigurationNames() As String() Implements SRNService.IDOWebServiceSoap.GetConfigurationNames
            Return MyBase.Channel.GetConfigurationNames
        End Function
        
        Public Function CreateSessionToken(ByVal strUserId As String, ByVal strPswd As String, ByVal strConfig As String) As String Implements SRNService.IDOWebServiceSoap.CreateSessionToken
            Return MyBase.Channel.CreateSessionToken(strUserId, strPswd, strConfig)
        End Function
        
        Public Function LoadDataSet(ByVal strSessionToken As String, ByVal strIDOName As String, ByVal strPropertyList As String, ByVal strFilter As String, ByVal strOrderBy As String, ByVal strPostQueryMethod As String, ByVal iRecordCap As Integer) As System.Data.DataSet Implements SRNService.IDOWebServiceSoap.LoadDataSet
            Return MyBase.Channel.LoadDataSet(strSessionToken, strIDOName, strPropertyList, strFilter, strOrderBy, strPostQueryMethod, iRecordCap)
        End Function
        
        Public Function SaveDataSet(ByVal strSessionToken As String, ByVal updateDataSet As System.Data.DataSet, ByVal refreshAfterSave As Boolean, ByVal strCustomInsert As String, ByVal strCustomUpdate As String, ByVal strCustomDelete As String) As System.Data.DataSet Implements SRNService.IDOWebServiceSoap.SaveDataSet
            Return MyBase.Channel.SaveDataSet(strSessionToken, updateDataSet, refreshAfterSave, strCustomInsert, strCustomUpdate, strCustomDelete)
        End Function
        
        Public Function CallMethod(ByVal strSessionToken As String, ByVal strIDOName As String, ByVal strMethodName As String, ByRef strMethodParameters As String) As Object Implements SRNService.IDOWebServiceSoap.CallMethod
            Return MyBase.Channel.CallMethod(strSessionToken, strIDOName, strMethodName, strMethodParameters)
        End Function
        
        Public Function LoadJson(ByVal strSessionToken As String, ByVal strIDOName As String, ByVal strPropertyList As String, ByVal strFilter As String, ByVal strOrderBy As String, ByVal strPostQueryMethod As String, ByVal iRecordCap As Integer) As String Implements SRNService.IDOWebServiceSoap.LoadJson
            Return MyBase.Channel.LoadJson(strSessionToken, strIDOName, strPropertyList, strFilter, strOrderBy, strPostQueryMethod, iRecordCap)
        End Function
        
        Public Function SaveJson(ByVal strSessionToken As String, ByVal updateJsonObject As String, ByVal strCustomInsert As String, ByVal strCustomUpdate As String, ByVal strCustomDelete As String) As String Implements SRNService.IDOWebServiceSoap.SaveJson
            Return MyBase.Channel.SaveJson(strSessionToken, updateJsonObject, strCustomInsert, strCustomUpdate, strCustomDelete)
        End Function
    End Class
End Namespace
