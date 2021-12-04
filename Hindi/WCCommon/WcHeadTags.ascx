<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WcHeadTags.ascx.cs" Inherits="WCCommon_WcHeadTags" %>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<link href="../css/MainStyleSheet.css" rel="stylesheet" type="text/css">
		<script src="../js/jquery-1.10.1.min.js"></script>
		
		<!--Slider - Begin -->
		<script src="../js/slider/slider-1.3.min.js"></script>
		<script class="secret-source">
				jQuery(document).ready(function($) {
				  $('#ntpcbanner').jsslider({
					//animtype    : 'slide',
					height      : 555,
					width       : 1200,
					responsive  : true
				  });
				});
		</script>
		<link href="../js/slider/slider.css" rel="stylesheet" type="text/css" />
		<link href="../js/slider/slider-controls.css" rel="stylesheet" type="text/css" />
		<!--Slider - End -->
		
		<!-- Start of Top Navigation Menu Script -->
		<script type="text/javascript" src="../js/menu/ddsmoothmenu.js"></script>
		<link rel="stylesheet" type="text/css" href="../js/menu/ddsmoothmenu.css" />
		<script type="text/javascript">
		   ddsmoothmenu.init({
		   mainmenuid: "smoothmenu1", //menu DIV id
		   orientation: 'h', //Horizontal or vertical menu: Set to "h" or "v"
		   classname: 'ddsmoothmenu', //class added to menu's outer DIV
		   contentsource: "markup" //"markup" or ["container_id", "path_to_menu_file"]
		})
		</script>
		<!-- End of Top Navigation Menu Script -->
<link href="../../css/MediaQuery.css" rel="stylesheet" />