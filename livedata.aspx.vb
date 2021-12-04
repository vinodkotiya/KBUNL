Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Net
Imports System.Net.NetworkInformation
Imports System.Xml
Imports System.Drawing
Imports System.IO 




Partial Class _Default
    Inherits System.Web.UI.Page
  ''  Dim con As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("ConnectionString").ToString)
    Dim conn As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("EMS_DataConnectionString").ToString)
    Dim connect As New SqlConnection(ConfigurationManager.ConnectionStrings("EMS_NetworkConnectionString").ToString)
    Dim cs_dcsg As New SqlConnection(ConfigurationManager.ConnectionStrings("EMS_DCSGConnectionString").ToString)

    Dim da As SqlDataAdapter
    Dim cmd As SqlCommand
    Dim sqlReader As SqlDataReader

    Dim da1 As SqlDataAdapter
    Dim cmd1 As SqlCommand



    'Unit-1
    Dim LoadArraylist As New ArrayList()
    Dim FreqArrayList As New ArrayList()
    Dim GlobalTimeStamp As [String] = ""
    Dim Global_i As Integer = 100, Global_j As Integer = 0
    'Unit 2
    Dim LoadArraylist1 As New ArrayList()
    Dim FreqArrayList1 As New ArrayList()
    Dim GlobalTimeStamp1 As [String] = ""
    Dim Global_i1 As Integer = 100, Global_j1 As Integer = 0
Dim total_MW As Double = 0.0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session("myIP") = Request.UserHostAddress

        If Not IsPostBack Then

            'vik_message_linkbutton_ModalPopupExtender.Show()

            GenerationSample(0)
            GenerationSample1(0)
            Unit3Gendata(0)
            Unit4Gendata(0)
   lbTotal.Text = total_MW.ToString() + " MW"


            'Dim dt As DateTime = DateTime.Now
            'Dim Nowdate As String = dt.ToString("dd-MMMM-yyyy HH:mm")
            'Label5.Text = Nowdate.ToString()
             If not  Request.Params("xml") Is Nothing Then
                GenerationXML()
             End if
              If not  Request.Params("v") Is Nothing Then
               divVert.Visible = True
               divHori.Visible = False
             End if
        End If


    End Sub

    Public Sub GenerationXML()
    '' Dim dt As New DataTable()
     Dim dt As DataTable
dt = New DataTable("KBUNLGenData")
            Dim Freq As [String] = "", Load As [String] = "", LocalTimeStamp As [String] = ""
            Dim str As String = "SELECT 'Unit-1' as 'unit-number','Stage-1' as 'stage-name', [HZ] as 'Freq',  [kW] / 1000 as 'load', replace(convert(varchar, Date_Time, 120), '-','/') as Timestamp, CURRENT_TIMESTAMP as CurrentDateTime FROM [EMS_Network].[dbo].[L4REALTIME] where TimeStampID= (select MAX(TimeStampID) from [EMS_Network].[dbo].[L4REALTIME] where Meter_Name like 'GEN1') and Meter_Name like 'GEN1' " & 
             " union SELECT 'Unit-2' as 'unit-number','Stage-1' as 'stage-name', [HZ] as 'Freq',  [kW] / 1000 as 'load', replace(convert(varchar, Date_Time, 120), '-','/') as Timestamp, CURRENT_TIMESTAMP as CurrentDateTime FROM [EMS_Network].[dbo].[L4REALTIME] where TimeStampID= (select MAX(TimeStampID) from [EMS_Network].[dbo].[L4REALTIME] where Meter_Name like 'GEN2') and Meter_Name like 'GEN2'" & 
             " union SELECT 'Unit-3' as 'unit-number','Stage-2' as 'stage-name', [HZ] as 'Freq',  [kW] / 1000 as 'load', replace(convert(varchar, Date_Time, 120), '-','/') as Timestamp, CURRENT_TIMESTAMP as CurrentDateTime FROM [EMS_Network].[dbo].[L4REALTIME] where TimeStampID= (select MAX(TimeStampID) from [EMS_Network].[dbo].[L4REALTIME] where Meter_Name like 'GEN3') and Meter_Name like 'GEN3'" &
             " union SELECT 'Unit-4' as 'unit-number','Stage-2' as 'stage-name', [HZ] as 'Freq',  [kW] / 1000 as 'load', replace(convert(varchar, Date_Time, 120), '-','/') as Timestamp, CURRENT_TIMESTAMP as CurrentDateTime FROM [EMS_Network].[dbo].[L4REALTIME] where TimeStampID= (select MAX(TimeStampID) from [EMS_Network].[dbo].[L4REALTIME] where Meter_Name like 'GEN4') and Meter_Name like 'GEN4'"
            cmd = New SqlCommand(str, connect)
            da = New SqlDataAdapter(cmd)
            da.Fill(dt)
             gvStatus.DataSource =dt
              gvStatus.DataBind()
         gvStatus.Visible = True
         Dim str1 As New MemoryStream()
        dt.WriteXml(str1, True)
        str1.Seek(0, SeekOrigin.Begin)
        Dim sr As New StreamReader(str1)
        Dim xmlstr As String
        xmlstr = sr.ReadToEnd()
      '  Response.Write (xmlstr)
       Dim path As String = HttpContext.Current.Server.MapPath("~")
        Dim fileNameWitPath As String = path + "kbunl_livegen_new.xml"

         If System.IO.File.Exists(fileNameWitPath) Then
            System.IO.File.Delete(fileNameWitPath)   
         End If
        Dim file As System.IO.StreamWriter
        file = My.Computer.FileSystem.OpenTextFileWriter(fileNameWitPath, True)
            file.WriteLine(xmlstr)
            file.Close()
            Timer24.Enabled = True
    End Sub

    Private Sub Timer24_Tick(sender As Object, e As EventArgs) Handles Timer24.Tick
         GenerationXML()
    End Sub


   

    
    Public Sub GenerationSample(ByVal WhatToDo As Integer)
        Try
            Dim dt As New DataTable()
            Dim Freq As [String] = "", Load As [String] = "", LocalTimeStamp As [String] = ""
            Dim str As String = "SELECT [Meter_Name],[TimeStampID], replace(convert(varchar, Date_Time, 120), '-','/') as Date_Time, [kW] / 1000, [HZ] FROM [EMS_Network].[dbo].[L4REALTIME] where TimeStampID= (select MAX(TimeStampID) from [EMS_Network].[dbo].[L4REALTIME] where Meter_Name='GEN1') and Meter_Name='GEN1'"
            cmd = New SqlCommand(str, connect)
            da = New SqlDataAdapter(cmd)
            da.Fill(dt)
        
            'DateTime datetimes = Convert.ToDateTime(dt.Rows[0][2]);
            Dim strdatetimes As String = dt.Rows(0)(2).ToString()
            Dim departs1 As [String]() = strdatetimes.Split(New [String]() {"."}, StringSplitOptions.None)
            Dim date_time As [String]() = departs1(0).Split(New [String]() {" "}, StringSplitOptions.None)
            Dim date_parts As [String]() = date_time(0).Split(New [String]() {"/"}, StringSplitOptions.None)

            'Label3.Text = strdatetimes.ToString();
            'string strdatetimes = dt.Rows[0][2].ToString();
            'Label3.Text = date_parts[2] + "/" + date_parts[1] + "/" + date_parts[0] + " " + date_time[1];
            Dim currentDate As String = ((date_parts(2) + "/" + date_parts(1) & "/") + date_parts(0) & " ") + date_time(1)
            ViewState("CurrentDate") = currentDate.ToString()
            ViewState("CDT_U1") = currentDate.ToString()
            Load = dt.Rows(0)(3).ToString().Trim()
            ' Generation Data.
            Freq = dt.Rows(0)(4).ToString().Trim()
            ' Frequency Data.
            LocalTimeStamp = dt.Rows(0)(1).ToString().Trim()
            ' TimeStamp
            ViewState("Max") = LocalTimeStamp.ToString()
            ViewState("CTS_U1") = LocalTimeStamp.ToString()

            If WhatToDo = 0 Then
                Label19.Text = Math.Round(Single.Parse(Load), 2).ToString() & " MW"
                 Label19v.Text = Math.Round(Single.Parse(Load), 2).ToString() & " MW"
                ' Label18.Text = Math.Round(Single.Parse(Freq), 2).ToString() & " Hz"
                'Label8.Text = Math.Round(Single.Parse(Freq1), 2).ToString() & " Hz"
                if Single.Parse(Load) < 1 Then
                Label19.BackColor = Color.Pink
                 Label19v.BackColor = Color.Pink
                Else
                Label19.BackColor = Color.Lime
                Label19v.BackColor = Color.Lime
                End If
              total_MW = total_MW + Math.Round(Single.Parse(Load), 2)
            Else
                LoadArraylist.Add(Load)
                FreqArrayList.Add(Freq)
                GlobalTimeStamp = LocalTimeStamp
                'Label3.Text = Label3.Text + "[" + LoadArraylist.Count+ "]" + Math.Round(float.Parse(Load), 2) + "," + Math.Round(float.Parse(Freq), 2) + " |";
            End If
        Catch ex As Exception

        End Try
    End Sub
    Public Sub GenerationSample1(ByVal WhatToDo As Integer)
        Try
            Dim dt1 As New DataTable()
            Dim Freq1 As [String] = "", Load1 As [String] = "", LocalTimeStamp1 As [String] = ""
            Dim str1 As String = "SELECT [Meter_Name],[TimeStampID], replace(convert(varchar, Date_Time, 120), '-','/') as Date_Time, [kW] / 1000, [HZ] FROM [EMS_Network].[dbo].[L4REALTIME] where TimeStampID= (select MAX(TimeStampID) from [EMS_Network].[dbo].[L4REALTIME] where Meter_Name='GEN2') and Meter_Name='GEN2'"
            cmd1 = New SqlCommand(str1, connect)
            da1 = New SqlDataAdapter(cmd1)
            da1.Fill(dt1)
            'DateTime datetimes = Convert.ToDateTime(dt.Rows[0][2]);
            Dim strdatetimes1 As String = dt1.Rows(0)(2).ToString()
            Dim departs11 As [String]() = strdatetimes1.Split(New [String]() {"."}, StringSplitOptions.None)
            Dim date_time1 As [String]() = departs11(0).Split(New [String]() {" "}, StringSplitOptions.None)
            Dim date_parts1 As [String]() = date_time1(0).Split(New [String]() {"/"}, StringSplitOptions.None)

            'Label3.Text = strdatetimes.ToString();
            'string strdatetimes = dt.Rows[0][2].ToString();
            'Label3.Text = date_parts[2] + "/" + date_parts[1] + "/" + date_parts[0] + " " + date_time[1];
            Dim currentDate1 As String = ((date_parts1(2) + "/" + date_parts1(1) & "/") + date_parts1(0) & " ") + date_time1(1)
            ViewState("CurrentDate") = currentDate1.ToString()
            ViewState("CDT_U2") = currentDate1.ToString()
            Load1 = dt1.Rows(0)(3).ToString().Trim()
            ' Generation Data.
            Freq1 = dt1.Rows(0)(4).ToString().Trim()
            ' Frequency Data.
            LocalTimeStamp1 = dt1.Rows(0)(1).ToString().Trim()
            ' TimeStamp
            ViewState("Max") = LocalTimeStamp1.ToString()
            ViewState("CTS_U2") = LocalTimeStamp1.ToString()

            If WhatToDo = 0 Then
                Label14.Text = Math.Round(Single.Parse(Load1), 2).ToString() & " MW"
                 Label14v.Text = Math.Round(Single.Parse(Load1), 2).ToString() & " MW"
                if Single.Parse(Load1) < 1 Then
                Label14.BackColor = Color.Pink
                 Label14v.BackColor = Color.Pink
                Else
                Label14.BackColor = Color.Lime
                Label14v.BackColor = Color.Lime
                End If
                ' Label12.Text = Math.Round(Single.Parse(Freq1), 2).ToString() & " Hz"
                Label8.Text = Math.Round(Single.Parse(Freq1), 2).ToString() & " Hz"
                 Label8v.Text = Math.Round(Single.Parse(Freq1), 2).ToString() & " Hz"
		total_MW = total_MW + Math.Round(Single.Parse(Load1), 2)
            Else
                LoadArraylist.Add(Load1)
                FreqArrayList.Add(Freq1)
                GlobalTimeStamp = LocalTimeStamp1
                'Label3.Text = Label3.Text + "[" + LoadArraylist.Count+ "]" + Math.Round(float.Parse(Load), 2) + "," + Math.Round(float.Parse(Freq), 2) + " |";
            End If
        Catch ex As Exception
      
        End Try
    End Sub

    'Unit-3 Data Star

    Public Sub Unit3Gendata(ByVal WhatToDo As Integer)
        Try
            Dim dt1 As New DataTable()
            Dim Freq1 As [String] = "", Load1 As [String] = "", LocalTimeStamp1 As [String] = ""
            Dim str1 As String = "SELECT [Meter_Name],[TimeStampID], replace(convert(varchar, Date_Time, 120), '-','/') as Date_Time, [kW] / 1000, [HZ] FROM [EMS_Network].[dbo].[L4REALTIME] where TimeStampID= (select MAX(TimeStampID) from [EMS_Network].[dbo].[L4REALTIME] where Meter_Name='GEN3') and Meter_Name='GEN3'"
            cmd1 = New SqlCommand(str1, connect)
            da1 = New SqlDataAdapter(cmd1)
            da1.Fill(dt1)
            'DateTime datetimes = Convert.ToDateTime(dt.Rows[0][2]);
            Dim strdatetimes1 As String = dt1.Rows(0)(2).ToString()
            Dim departs11 As [String]() = strdatetimes1.Split(New [String]() {"."}, StringSplitOptions.None)
            Dim date_time1 As [String]() = departs11(0).Split(New [String]() {" "}, StringSplitOptions.None)
            Dim date_parts1 As [String]() = date_time1(0).Split(New [String]() {"/"}, StringSplitOptions.None)

            'Label3.Text = strdatetimes.ToString();
            'string strdatetimes = dt.Rows[0][2].ToString();
            'Label3.Text = date_parts[2] + "/" + date_parts[1] + "/" + date_parts[0] + " " + date_time[1];
            Dim currentDate1 As String = ((date_parts1(2) + "/" + date_parts1(1) & "/") + date_parts1(0) & " ") + date_time1(1)
            ViewState("CurrentDate") = currentDate1.ToString()
            ViewState("CDT_U3") = currentDate1.ToString()
            Load1 = dt1.Rows(0)(3).ToString().Trim()
            ' Generation Data.
            Freq1 = dt1.Rows(0)(4).ToString().Trim()
            ' Frequency Data.
            LocalTimeStamp1 = dt1.Rows(0)(1).ToString().Trim()
            ' TimeStamp
            ViewState("Max") = LocalTimeStamp1.ToString()
            ViewState("CTS_U3") = LocalTimeStamp1.ToString()

            If WhatToDo = 0 Then
                Label6.Text = Math.Round(Single.Parse(Load1), 2).ToString() & " MW"
                  Label6v.Text = Math.Round(Single.Parse(Load1), 2).ToString() & " MW"
                if Single.Parse(Load1) < 1 Then
                Label6.BackColor = Color.Pink
                Label6v.BackColor = Color.Pink
                Else
                Label6.BackColor = Color.Lime
                Label6v.BackColor = Color.Lime
                End If
                ' Label4.Text = Math.Round(Single.Parse(Freq1), 2).ToString() & " Hz"
                'Label8.Text = Math.Round(Single.Parse(Freq1), 2).ToString() & " Hz"
 		total_MW = total_MW + Math.Round(Single.Parse(Load1), 2)
            Else
                LoadArraylist.Add(Load1)
                FreqArrayList.Add(Freq1)
                GlobalTimeStamp = LocalTimeStamp1
                'Label3.Text = Label3.Text + "[" + LoadArraylist.Count+ "]" + Math.Round(float.Parse(Load), 2) + "," + Math.Round(float.Parse(Freq), 2) + " |";
            End If
        Catch ex As Exception

        End Try
    End Sub
    'Unit-3 Data End
    'Unit-4 Data Star

    Public Sub Unit4Gendata(ByVal WhatToDo As Integer)
        Try
            Dim dt1 As New DataTable()
            Dim Freq1 As [String] = "", Load1 As [String] = "", LocalTimeStamp1 As [String] = ""
            'Dim str1 As String = "SELECT [Meter_Name],[TimeStampID], replace(convert(varchar, Date_Time, 120), '-','/') as Date_Time, [kW] / 1000, [HZ] FROM [EMS_Network].[dbo].[L4REALTIME] where TimeStampID= (select MAX(TimeStampID) from [EMS_Network].[dbo].[L4REALTIME] where Meter_Name='GEN4') and Meter_Name='GEN4'"
            Dim str1 As String = "SELECT [Meter_Name],[TimeStampID], replace(convert(varchar, Date_Time, 120), '-','/') as Date_Time, [kW] / 1000, [HZ] FROM [EMS_Network].[dbo].[L4REALTIME] where TimeStampID= (select MAX(TimeStampID) from [EMS_Network].[dbo].[L4REALTIME] where Meter_Name='GEN4') and Meter_Name='GEN4'"
            cmd1 = New SqlCommand(str1, connect)
            da1 = New SqlDataAdapter(cmd1)
            da1.Fill(dt1)
            'DateTime datetimes = Convert.ToDateTime(dt.Rows[0][2]);
            Dim strdatetimes1 As String = dt1.Rows(0)(2).ToString()
            Dim departs11 As [String]() = strdatetimes1.Split(New [String]() {"."}, StringSplitOptions.None)
            Dim date_time1 As [String]() = departs11(0).Split(New [String]() {" "}, StringSplitOptions.None)
            Dim date_parts1 As [String]() = date_time1(0).Split(New [String]() {"/"}, StringSplitOptions.None)

            'Label3.Text = strdatetimes.ToString();
            'string strdatetimes = dt.Rows[0][2].ToString();
            'Label3.Text = date_parts[2] + "/" + date_parts[1] + "/" + date_parts[0] + " " + date_time[1];
            Dim currentDate1 As String = ((date_parts1(2) + "/" + date_parts1(1) & "/") + date_parts1(0) & " ") + date_time1(1)
            ViewState("CurrentDate") = currentDate1.ToString()
            ViewState("CDT_U4") = currentDate1.ToString()
            Load1 = dt1.Rows(0)(3).ToString().Trim()
            ' Generation Data.
            Freq1 = dt1.Rows(0)(4).ToString().Trim()
            ' Frequency Data.
            LocalTimeStamp1 = dt1.Rows(0)(1).ToString().Trim()
            ' TimeStamp
            ViewState("Max") = LocalTimeStamp1.ToString()
            ViewState("CTS_U4") = LocalTimeStamp1.ToString()

            If WhatToDo = 0 Then
                Label10.Text = Math.Round(Single.Parse(Load1), 2).ToString() & " MW"
                 Label10v.Text = Math.Round(Single.Parse(Load1), 2).ToString() & " MW"
                if Single.Parse(Load1) < 1 Then
                Label10.BackColor = Color.Pink
                Label10v.BackColor = Color.Pink
                Else
                Label10.BackColor = Color.Lime
                 Label10v.BackColor = Color.Lime
                End If
                Label8.Text = Math.Round(Single.Parse(Freq1), 2).ToString() & " Hz"
                 Label8v.Text = Math.Round(Single.Parse(Freq1), 2).ToString() & " Hz"
		total_MW = total_MW + Math.Round(Single.Parse(Load1), 2)
            Else
                LoadArraylist.Add(Load1)
                FreqArrayList.Add(Freq1)
                GlobalTimeStamp = LocalTimeStamp1
                'Label3.Text = Label3.Text + "[" + LoadArraylist.Count+ "]" + Math.Round(float.Parse(Load), 2) + "," + Math.Round(float.Parse(Freq), 2) + " |";
            End If
        Catch ex As Exception

        End Try
    End Sub
    'Unit-4 Data End



    Public Sub GenerationData()


        Dim num As Integer = Convert.ToInt32(HiddenField1.Value)
        num += 1
        HiddenField1.Value = Convert.ToInt32(num).ToString()

        If num Mod 4 = 0 Then
            Dim AvgLoad As Single = 0, AvgFreq As Single = 0
            For i As Integer = 0 To LoadArraylist.Count - 1
                AvgLoad = AvgLoad + Single.Parse(LoadArraylist(i).ToString())
                AvgFreq = AvgFreq + Single.Parse(FreqArrayList(i).ToString())
            Next
            AvgLoad = AvgLoad / LoadArraylist.Count
            AvgFreq = AvgFreq / LoadArraylist.Count

            LoadArraylist.Clear()
            FreqArrayList.Clear()

            Label19.Text = Math.Round(AvgLoad, 2).ToString() & " MW"




            'Label18.Text = "49.87 Hz"
        Else
            GenerationSample(num)
        End If
    End Sub
    Public Sub GenerationData1()


        Dim num1 As Integer
        '= Convert.ToInt32(HiddenField3.Value)
        '     num1 += 1
        '    HiddenField3.Value = Convert.ToInt32(num1).ToString()

        If num1 Mod 4 = 0 Then
            Dim AvgLoad1 As Single = 0, AvgFreq1 As Single = 0
            For i As Integer = 0 To LoadArraylist1.Count - 1
                AvgLoad1 = AvgLoad1 + Single.Parse(LoadArraylist1(i).ToString())
                AvgFreq1 = AvgFreq1 + Single.Parse(FreqArrayList1(i).ToString())
            Next
            AvgLoad1 = AvgLoad1 / LoadArraylist1.Count
            AvgFreq1 = AvgFreq1 / LoadArraylist1.Count

            LoadArraylist1.Clear()
            FreqArrayList1.Clear()

            Label14.Text = Math.Round(AvgLoad1, 2).ToString() & " MW"




        Else
            GenerationSample1(num1)
        End If
    End Sub
    Public Sub DCSG()
        Dim st1_dc, st1_sg, st2_dc, st2_sg As String

        st1_dc = Get_DCSG("1")
        st1_sg = Get_DCSG("2")
        st2_dc = Get_DCSG("4")
        st2_sg = Get_DCSG("5")
        'Label22.Text = "DCSG=" + st1_dc + ", " + st1_sg + ", " + st2_dc + ", " + st2_sg


    End Sub


    Public Function Get_DCSG(ByVal paraID As String) As String
        Dim curr_datetime As DateTime = DateTime.Now
        Dim year, month, day, hour, minute, baseminute As Integer

        year = curr_datetime.Year
        month = curr_datetime.Month
        day = curr_datetime.Day
        hour = curr_datetime.Hour
        minute = curr_datetime.Minute

        If minute < 15 Then
            baseminute = 0
        ElseIf minute >= 15 And minute < 30 Then
            baseminute = 15
        ElseIf minute >= 30 And minute < 45 Then
            baseminute = 30
        Else
            baseminute = 45
        End If

        Dim dt As DateTime = New DateTime(year, month, day, hour, baseminute, 0)
        ' Dim currdate As String = dt.ToString("16-09-2018 05:30:00")
        Dim epoch As Long = (dt.ToUniversalTime().Ticks - 621355968000000000) / 10000000
        'Label22.Text = epoch
        '( dt.TimeOfDay)

        Dim dt1 As New DataTable()
        Dim query1 As String = "SELECT   distinct ParaValue FROM [EMS_DCSG].[dbo].[InData] where timestampid=" + Convert.ToString(epoch) + " and paraId=" + paraID
        cmd1 = New SqlCommand(query1, cs_dcsg)
        da1 = New SqlDataAdapter(cmd1)
        da1.Fill(dt1)
        Dim ret_val As String = "0"
        If dt1.Rows.Count > 0 Then
            ret_val = dt1.Rows(0)(0).ToString()
        End If


        Return ret_val
    End Function


    Protected Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        'DCSG()
        'frequency()
        'Generation()
        Dim dt As DateTime = DateTime.Now
        Dim currdate As String = dt.ToString("dd-MM-yyyy HH:mm:ss")
        currdate = currdate.Replace("-", "/")
        Dim frqarr() As String
        Dim loadarr() As String
        Dim frqarr1() As String
        Dim loadarr1() As String
	total_MW = 0
        GenerationSample(0)
        GenerationSample1(0)
        Unit3Gendata(0)
        Unit4Gendata(0)
        lbTotal.Text = total_MW.ToString() + " MW"
        lbTotalv.Text = total_MW.ToString() + " MW"
        'Second XML Files
        'Dim writer1 As XmlWriter = New XmlTextWriter(Server.MapPath("kbunl_livegen_new.xml"), System.Text.Encoding.UTF8)

        ''XmlWriter writer = XmlWriter.Create("File/Annuaire.xml");
        'writer1.WriteStartDocument()
        ''CreateXmlDeclaration("1.0", "UTF-8", "no");
        'writer1.WriteStartElement("KBUNLGenData")

        'writer1.WriteStartElement("unit")

        'writer1.WriteStartElement("unit-number")
        'writer1.WriteString("Unit-1")
        'writer1.WriteEndElement()

        'writer1.WriteStartElement("stage-name")
        'writer1.WriteString("Stage-1")
        'writer1.WriteEndElement()

        'writer1.WriteStartElement("Freq")
        'frqarr = Label18.Text.ToString().Split(" ")
        'writer1.WriteString(frqarr(0))
        ''writer.WriteString(Label18.Text)
        'writer1.WriteEndElement()

        'writer1.WriteStartElement("load")
        'loadarr = Label19.Text.ToString().Split(" ")
        'writer1.WriteString(loadarr(0))
        ''writer1.WriteString("89")
        'writer1.WriteEndElement()

        'writer1.WriteStartElement("Timestamp")
        'writer1.WriteString(ViewState("CDT_U1").ToString())
        'writer1.WriteEndElement()

        'writer1.WriteStartElement("CurrentDateTime")
        'writer1.WriteString(currdate.ToString())
        'writer1.WriteEndElement()

        'writer1.WriteEndElement()

        'writer1.WriteStartElement("unit")

        'writer1.WriteStartElement("unit-number")
        'writer1.WriteString("Unit-2")
        'writer1.WriteEndElement()

        'writer1.WriteStartElement("stage-name")
        'writer1.WriteString("Stage-1")
        'writer1.WriteEndElement()

        'writer1.WriteStartElement("Freq")
        'frqarr1 = Label12.Text.ToString().Split(" ")
        'writer1.WriteString(frqarr1(0))
        ''writer.WriteString(Label18.Text)
        'writer1.WriteEndElement()

        'writer1.WriteStartElement("load")
        'loadarr1 = Label14.Text.ToString().Split(" ")
        'writer1.WriteString(loadarr1(0))
        ''writer1.WriteString("51")
        'writer1.WriteEndElement()

        'writer1.WriteStartElement("Timestamp")
        'writer1.WriteString(ViewState("CDT_U2").ToString())
        'writer1.WriteEndElement()

        'writer1.WriteStartElement("CurrentDateTime")
        'writer1.WriteString(currdate.ToString())
        'writer1.WriteEndElement()

        'writer1.WriteEndElement()






        'writer1.WriteStartElement("unit")

        'writer1.WriteStartElement("unit-number")
        'writer1.WriteString("Unit-3")
        'writer1.WriteEndElement()

        'writer1.WriteStartElement("stage-name")
        'writer1.WriteString("Stage-2")
        'writer1.WriteEndElement()

        'writer1.WriteStartElement("Freq")
        'frqarr = Label4.Text.ToString().Split(" ")
        'writer1.WriteString(frqarr(0))
        ''writer1.WriteString(Label18.Text)
        'writer1.WriteEndElement()

        'writer1.WriteStartElement("load")
        'loadarr = Label6.Text.ToString().Split(" ")
        'writer1.WriteString(loadarr(0))
        ''writer.WriteString(Label19.Text)
        'writer1.WriteEndElement()

        'writer1.WriteStartElement("Timestamp")
        'writer1.WriteString(ViewState("CDT_U3").ToString())
        'writer1.WriteEndElement()

        'writer1.WriteStartElement("CurrentDateTime")
        'writer1.WriteString(currdate.ToString())
        'writer1.WriteEndElement()

        'writer1.WriteEndElement()




        'writer1.WriteStartElement("unit")

        'writer1.WriteStartElement("unit-number")
        'writer1.WriteString("Unit-4")
        'writer1.WriteEndElement()

        'writer1.WriteStartElement("stage-name")
        'writer1.WriteString("Stage-2")
        'writer1.WriteEndElement()

        'writer1.WriteStartElement("Freq")
        'frqarr = Label8.Text.ToString().Split(" ")
        'writer1.WriteString(frqarr(0))
        'writer1.WriteEndElement()

        'writer1.WriteStartElement("load")
        'loadarr = Label10.Text.ToString().Split(" ")
        'writer1.WriteString(loadarr(0))
        'writer1.WriteEndElement()

        'writer1.WriteStartElement("Timestamp")
        'writer1.WriteString(ViewState("CDT_U4").ToString())
        'writer1.WriteEndElement()

        'writer1.WriteStartElement("CurrentDateTime")
        'writer1.WriteString(currdate.ToString())
        'writer1.WriteEndElement()

        'writer1.WriteEndElement()

        'writer1.WriteStartElement("Stage")

        'writer1.WriteStartElement("Stage1-DC")
        'writer1.WriteString(Get_DCSG("1"))
        ''writer1.WriteString("150")
        'writer1.WriteEndElement()

        'writer1.WriteStartElement("Stage1-SG")
        'writer1.WriteString(Get_DCSG("2"))
        ''writer1.WriteString("140")
        'writer1.WriteEndElement()

        'writer1.WriteStartElement("Stage2-DC")
        'writer1.WriteString(Get_DCSG("4"))
        'writer1.WriteEndElement()

        'writer1.WriteStartElement("Stage2-SG")
        'writer1.WriteString(Get_DCSG("5"))
        'writer1.WriteEndElement()
        'writer1.WriteEndElement()


        'writer1.WriteEndElement()

        'writer1.Close()




    End Sub



End Class
