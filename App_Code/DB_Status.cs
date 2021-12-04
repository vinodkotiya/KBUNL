using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

/// <summary>
/// Summary description for DB_Status
/// </summary>
/// 

[DataContract]
public class DB_Status
{
    public enum Status
    {
        Success=1, Error=2, Null=0
    }

    #region Attribute
    Status res_status;
    string res_Title;
    string res_Description;
    string res_Location;
    string res_SubLocation;
    string res_MessageCode;
    string res_MessageType;
    string res_HelpMessage;
    string res_SingleResult;
    System.Data.DataSet res_ResultDataSet;
    #endregion

    #region Property
    [DataMember]
    public Status OperationStatus
    {
        get { return res_status; }
        set { res_status = value; }
    }
    [DataMember]
    public string Title
    {
        get { return res_Title; }
        set { res_Title = value; }
    }
    [DataMember]
    public string Description
    {
        get { return res_Description; }
        set { res_Description = value; }
    }
    [DataMember]
    public string Location
    {
        get { return res_Location; }
        set { res_Location = value; }
    }
    [DataMember]
    public string SubLocation
    {
        get { return res_SubLocation; }
        set { res_SubLocation = value; }
    }
    [DataMember]
    public string MessageCode
    {
        get { return res_MessageCode; }
        set { res_MessageCode = value; }
    }
    [DataMember]
    public string MessageType
    {
        get { return res_MessageType; }
        set { res_MessageType = value; }
    }
    [DataMember]
    public string HelpMessage
    {
        get { return res_HelpMessage; }
        set { res_HelpMessage = value; }
    }
    [DataMember]
    public string SingleResult
    {
        get { return res_SingleResult; }
        set { res_SingleResult = value; }
    }
    [DataMember]
    public System.Data.DataSet ResultDataSet
    {
        get { return res_ResultDataSet; }
        set { res_ResultDataSet = value; }
    }
    #endregion

    #region Methods
    public DB_Status()
	{
        this.res_status = 0;
        this.res_Title = "";
        this.res_Description = "";
        this.res_Location = "";
        this.res_SubLocation = "";
        this.res_MessageCode = "";
        this.res_MessageType = "";
        this.res_HelpMessage = "";
        this.res_SingleResult = "";
        res_ResultDataSet = new System.Data.DataSet();
    }
    public void SetMessage(Status status, string Title, string Description, string Location, string SubLocation, string MessageCode, string MessageType, string HelpMessage)
    {
        res_status = status;
        res_Title = Title;
        res_Description = Description;
        res_Location = Location;
        res_SubLocation = SubLocation;
        res_MessageCode = MessageCode;
        res_MessageType = MessageType;
        res_HelpMessage = HelpMessage;
    }
    public void SetMessage(Status status, string Title, string Description, string Location, string SubLocation, string MessageCode, string MessageType, string HelpMessage, string SingleResult)
    {
        res_status = status;
        res_Title = Title;
        res_Description = Description;
        res_Location = Location;
        res_SubLocation = SubLocation;
        res_MessageCode = MessageCode;
        res_MessageType = MessageType;
        res_HelpMessage = HelpMessage;
        res_SingleResult = SingleResult;
    }
    public void SetMessage(Status status, string Title, string Description, string Location, string SubLocation, string MessageCode, string MessageType, string HelpMessage, System.Data.DataSet ResultDataSet)
    {
        res_status = status;
        res_Title = Title;
        res_Description = Description;
        res_Location = Location;
        res_SubLocation = SubLocation;
        res_MessageCode = MessageCode;
        res_MessageType = MessageType;
        res_HelpMessage = HelpMessage;
        res_ResultDataSet = ResultDataSet;
    }
    #endregion

}