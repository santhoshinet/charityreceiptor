<%@ Page Language="C#" CodeBehind="Default.aspx.cs" Inherits="Barcode._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>Barcode Generator 1D for ASP.net</title>
	<style type="text/css">
	*
	{
		font-family: Verdana;
		font-size: 10pt;
	}
	
	#table_barcode
	{
		width: 500px;
		border: 1px solid #841414;
		border-collapse: collapse;
	}

	#table_barcode td
	{
		text-align: left;
		border: 1px solid #a73e3e;
		padding: 4px;
	}
	
	.table_header
	{
		background-color: #993535;
		color: White;
		text-decoration: underline;
	}
	
	.table_section
	{
		background-color: #c35d5d;
		color: white;
	}
	
	.row1
	{
		background-color: #f8d3d3;
	}
	
	.row2
	{
		background-color: #e1a6a6;
	}
	
	.empty
	{
		border-collapse: collapse;
	}
	
	.empty td
	{
		padding: 0px !important;
		border: 0px !important;
	}
	
	.footer, .footer a, .footer span
	{
		font-size: 10px;
	}
	</style>
</head>
<body>

	<form id="form1" runat="server">
	<asp:ScriptManager runat="server" ID="scriptManager"></asp:ScriptManager>
	
	<div style="text-align: center;">

	<table id="table_barcode" style="margin: auto;">
		<tr><td colspan="2" style="text-align: center;" class="table_header">Config for <asp:Label runat="server" ID="configFor" /></td></tr>
		<tr><td colspan="2" style="text-align: center;" class="table_section">General Configs</td></tr>
		<tr class="row1"><td>Type</td><td><asp:DropDownList runat="server" ID="general_type" AutoPostBack="true" OnSelectedIndexChanged="BarcodeTypeChanged"></asp:DropDownList></td></tr>
		<tr class="row2"><td>Output</td><td><asp:DropDownList runat="server" ID="general_output"></asp:DropDownList></td></tr>
		<tr class="row1"><td>Thickness</td><td><asp:TextBox runat="server" ID="general_thickness"></asp:TextBox></td></tr>
		<tr class="row2"><td>Resolution</td><td><table class="empty" style="width: 100%"><tr><td><asp:RadioButton runat="server" ID="general_res1" GroupName="resolution" Text="1" /></td><td><asp:RadioButton runat="server" ID="general_res2" GroupName="resolution" Text="2" /></td><td><asp:RadioButton runat="server" ID="general_res3" GroupName="resolution" Text="3" /></td></tr></table></td></tr>
		<tr class="row1"><td>Font</td><td><asp:DropDownList runat="server" ID="general_font"></asp:DropDownList> <asp:TextBox runat="server" ID="general_fontsize" Columns="3" /></td></tr>
		<tr class="row2"><td>Text</td><td><asp:TextBox runat="server" ID="general_Text"></asp:TextBox></td></tr>
		<tr class="row1"><td colspan="2" style="text-align: center;"><asp:Button runat="server" ID="generate" Text="Generate" OnClick="GenerateClick" /></td></tr>
		<tr runat="server" id="imagerow"><td colspan="2" style="text-align: center;"><asp:Image runat="server" ID="barcodeimage" AlternateText="Barcode Image"></asp:Image></td></tr>
		<tr><td colspan="2" style="text-align: center;" class="table_section">Specific Configs</td></tr>
		<tr class="row1"><td>
			<asp:UpdatePanel runat="server" ID="specificConfigData1">
				<ContentTemplate>
					<asp:Label runat="server" ID="data1"></asp:Label>
				</ContentTemplate>
				<Triggers><asp:AsyncPostBackTrigger ControlID="general_type" EventName="SelectedIndexChanged" /></Triggers>
			</asp:UpdatePanel>
		</td><td>
			<asp:UpdatePanel runat="server" ID="specificConfigData2">
				<ContentTemplate>
					<asp:Label runat="server" ID="data2"></asp:Label>
				</ContentTemplate>
				<Triggers><asp:AsyncPostBackTrigger ControlID="general_type" EventName="SelectedIndexChanged" /></Triggers>
			</asp:UpdatePanel>
		</td></tr>
		<tr class="row2"><td>Keys</td><td>
			<asp:UpdatePanel runat="server" ID="specificConfigKeys">
				<ContentTemplate>
					<asp:Label runat="server" ID="keys"></asp:Label>
				</ContentTemplate>
				<Triggers><asp:AsyncPostBackTrigger ControlID="general_type" EventName="SelectedIndexChanged" /></Triggers>
			</asp:UpdatePanel>
		</td></tr>
		<tr class="row1"><td>Explanation</td><td>
			<asp:UpdatePanel runat="server" ID="specificConfigExplanation">
				<ContentTemplate>
					<asp:Panel runat="server" ID="explanation"></asp:Panel>
				</ContentTemplate>
				<Triggers><asp:AsyncPostBackTrigger ControlID="general_type" EventName="SelectedIndexChanged" /></Triggers>
			</asp:UpdatePanel>
		</td></tr>
	</table>

	<p class="footer">
	All Rights Reserved &copy; 2009 <a href="http://www.barcodeasp.com" target="_blank">Barcode Generator</a> ASP.net-v<asp:Label runat="server" ID="versionLabel" />
	<br />by Jean-Sébastien Goupil
	</p>

	</div>
	</form>
</body>
</html>
