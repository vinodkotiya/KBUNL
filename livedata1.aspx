<%@ Page Language="VB" AutoEventWireup="false" CodeFile="livedata1.aspx.vb" Inherits="_livedata1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>KBUNL Live Data</title>
    <meta http-equiv="refresh" content="10">
</head>
<body>
    <form id="form1" runat="server">
          <!--<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager> -->

          <div id="jg1" class="gauge size-1"></div>
          <div id="vinData1" runat="Server" style="display:none;">120</div>
              <div class="h-split"></div>

              <div id="jg2" class="gauge size-1"></div>
              <div id="vinData2" runat="Server" style="display:none;">120</div>
                  <div class="h-split"></div>

                  <div id="jg3" class="gauge size-1"></div>
                  <div id="vinData3" runat="Server" style="display:none;">120</div>
                      <div class="h-split"></div>

                      <div id="jg4" class="gauge size-1"></div>
                      <div id="vinData4" runat="Server" style="display:none;">120</div>
                          <div class="h-split"></div>
                          <div class="divTable">
                            <div class="divTableHeading">
                                <div class="divTableRow">
                                <div class="divTableHead">Live Generation</div>
                                </div>
                                </div>
                            <div class="divTableBody">
                                <div class="divTableRow">
                                <div class="divTableCell">Total</div>
                                <div class="divTableCell"><span id="ContentPlaceHolder1_Label19v"><asp:Label ID="lbTotal" runat="server" Text="Loading.."></asp:Label></span></div>
                                </div>
                                <div class="divTableRow">
                                    <div class="divTableCell">Freq</div>
                                    <div class="divTableCell"><span id="ContentPlaceHolder1_Label19v"><asp:Label ID="Label8" runat="server"></asp:Label></span></div>
                                    </div>
                            </div>
                   
            <div class="style1" style="display:none;">
                <div id="sidebar-d">
                
        
                            <div id="divHori" runat="Server" >
                               

                            <table>
                                <thead>
                                    <tr>
                                        <th>Unit #1</th>
                                        <th>Unit #2</th>
                                        <th>Unit #3</th>
                                        <th>Unit #4</th>
         <th>Total</th>
                                        <th>Frequency</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td><asp:Label ID="Label19" runat="server"></asp:Label></td>
                                        <td><asp:Label ID="Label14" runat="server"></asp:Label></td>
                                        <td><asp:Label ID="Label6" runat="server"></asp:Label></td>
                                        <td><asp:Label ID="Label10" runat="server"></asp:Label></td>
         
                                    </tr>
                                </tbody>
                            </table>
                           </div>
                           <div id="divVert" runat="Server" Visible = "False" >
                            <div class="divTable">
                                <div class="divTableHeading">
                                <div class="divTableRow">
                                <div class="divTableHead">Live Generation</div>
                                </div>
                                </div>
                                <div class="divTableBody">
                                <div class="divTableRow">
                                <div class="divTableCell">Unit #1</div>
                                <div class="divTableCell"><span id="ContentPlaceHolder1_Label19v"><asp:Label ID="Label19v" runat="server"></asp:Label></span></div>
                                </div>
                                <div class="divTableRow">
                                <div class="divTableCell">Unit #2</div>
                                <div class="divTableCell"><span id="ContentPlaceHolder1_Label14v"><asp:Label ID="Label14v" runat="server"></asp:Label></span></div>
                                </div>
                                <div class="divTableRow">
                                <div class="divTableCell">Unit #3</div>
                                <div class="divTableCell"><span id="ContentPlaceHolder1_Label6v"><asp:Label ID="Label6v" runat="server"></asp:Label></span></div>
                                </div>
                                <div class="divTableRow">
                                <div class="divTableCell">Unit #4</div>
                                <div class="divTableCell"><span id="ContentPlaceHolder1_lbTotaldv"></span><asp:Label ID="Label10v" runat="server"></asp:Label></span></div>
                                </div>
                                <div class="divTableRow" style = "font-weight: bold;">
                                <div class="divTableCell">Total</div>
                                <div class="divTableCell"><span id="ContentPlaceHolder1_lbTotalv"><asp:Label ID="lbTotalv" runat="server" Text="Loading.."></asp:Label></span></div>
                                </div>
                                <div class="divTableRow" style ="font-weight: bold;">
                                <div class="divTableCell">Frequency</div>
                                <div class="divTableCell"><span id="ContentPlaceHolder1_Label8v"><asp:Label ID="Label8v" runat="server"></asp:Label></span></div>
                                </div>
                                </div>
                                </div>
                           
                           </div>
                            <asp:HiddenField ID="HiddenField1" runat="server" Visible="False" Value="0" />
                            <asp:HiddenField ID="HiddenField2" runat="server" Visible="False" />
                            <asp:HiddenField ID="HiddenField3" runat="server" Visible="False" Value="0" />
                            <asp:HiddenField ID="HiddenField4" runat="server" Visible="False" />
                            <asp:HiddenField ID="HiddenField5" runat="server" Visible="False" Value="0" />
                            <asp:HiddenField ID="HiddenField6" runat="server" Visible="False" />
                                    <asp:HiddenField ID="HiddenField7" runat="server" Visible="False" Value="0" />
                                        <asp:HiddenField ID="HiddenField8" runat="server" Visible="False" />
                                   
                                        <asp:GridView ID="gvStatus" runat="server"   Visible="False" >
                                            <EmptyDataTemplate><div>No Data Available</div></EmptyDataTemplate></asp:GridView>
                       <!--   </ContentTemplate>
        
                    </asp:UpdatePanel> -->
                  
        
                </div>
        
        
            </div>
        
        
        
        
   
        

    </form>
</body>
<script src="raphael.min.js"></script>
    <script src="justgage.js"></script>
    <script>
        function vinshow(){
           
        }
    document.addEventListener("DOMContentLoaded", function(event) {
        var jg1;

        var defs1 = {
            label: "U#1 MW",
            value: document.getElementById("vinData1").innerHTML,
            min: 0,
            max: 110,
            decimals: 2,
            gaugeWidthScale: 0.6,
            pointer: true,
            pointerOptions: {
                toplength: 10,
                bottomlength: 10,
                bottomwidth: 2
            },
            counter: true,
            relativeGaugeSize: true
        }

       
        jg1 = new JustGage({
            id: "jg1",
            defaults: defs1
        });

        var jg2;

        var defs2 = {
            label: "U#2 MW",
            value: document.getElementById("vinData2").innerHTML,
            min: 0,
            max: 110,
            decimals: 2,
            gaugeWidthScale: 0.6,
            pointer: true,
            pointerOptions: {
                toplength: 10,
                bottomlength: 10,
                bottomwidth: 2
            },
            counter: true,
            relativeGaugeSize: true
        }

       
        jg2 = new JustGage({
            id: "jg2",
            defaults: defs2
        });

        var jg3;

        var defs3 = {
            label: "U#3 MW",
            value: document.getElementById("vinData3").innerHTML,
            min: 0,
            max: 195,
            decimals: 2,
            gaugeWidthScale: 0.6,
            pointer: true,
            pointerOptions: {
                toplength: 10,
                bottomlength: 10,
                bottomwidth: 2
            },
            counter: true,
            relativeGaugeSize: true
        }

       
        jg3 = new JustGage({
            id: "jg3",
            defaults: defs3
        });

        var jg4;

        var defs4 = {
            label: "U#4 MW",
            value: document.getElementById("vinData4").innerHTML,
            min: 0,
            max: 195,
            decimals: 2,
            gaugeWidthScale: 0.6,
            pointer: true,
            pointerOptions: {
                toplength: 10,
                bottomlength: 10,
                bottomwidth: 2
            },
            counter: true,
            relativeGaugeSize: true
        }

       
        jg4 = new JustGage({
            id: "jg4",
            defaults: defs4
        });
    });
    </script>
<style>
    .clear:before,
        .clear:after {
            content: "";
            display: table;
        }

        .clear:after {
            clear: both;
        }

        .clear {
            *zoom: 1;
        }

        .gauge {
            display: block;
            float: left;
            border: 1px solid #ddd;
            box-sizing: border-box;
            margin: 0 0 1% 0;
        }

        .size-1 {
            width: 20%;
        }

        .size-2 {
            width: 30%;
        }

        .size-3 {
            width: 48%;
        }

        .h-split {
            display: block;
            float: left;
            width: 1%;
            min-height: 100px;
        }
  
body {
  background: white;
  font: 400 14px "Calibri", "Arial";
  padding: 0px;
}

blockquote {
  color: white;
  text-align: center;
}
/* DivTable.com */
.divTable{
	display: inline;
	
  /*border: 1px solid #1C6EA4;*/
  background-color: #EEEEEE;
 
  text-align: left;
  border-collapse: collapse;
}
.divTableRow {
	display: table-row;
}
.divTableHeading {
background: #1C6EA4;
  background: -moz-linear-gradient(top, #5592bb 0%, #327cad 66%, #1C6EA4 100%);
  background: -webkit-linear-gradient(top, #5592bb 0%, #327cad 66%, #1C6EA4 100%);
  background: linear-gradient(to bottom, #5592bb 0%, #327cad 66%, #1C6EA4 100%);
  border-bottom: 2px solid #444444;
	display: table-header-group;
  font-size: 20px;
  font-weight: bold;
  color: #FFFFFF;
  border-left: 2px solid #D0E4F5;
}
.divTableCell, .divTableHead {
	
	display: table-cell;
	padding: 3px 10px;
   border: 1px solid #AAAAAA;
 font-size: 17px;
}
.divTableFoot {
	background-color: #EEE;
	display: table-footer-group;
	font-weight: bold;
}
.divTableBody {
/*	display: table-row-group; */
}
</style>
</html>
