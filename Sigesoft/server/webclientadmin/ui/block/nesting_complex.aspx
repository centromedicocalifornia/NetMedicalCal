<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="nesting_complex.aspx.cs" Inherits="Sigesoft.Server.WebClientAdmin.UI.block.nesting_complex" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title></title>
    <style>
        body.f-body {
            overflow-x: hidden;
        }
    </style>
</head>
<body class="f-body-bgcolor">
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server" />
        <f:Panel IsFluid="true" runat="server"
            Layout="Block" BlockConfigSpace="20px" ShowBorder="false" ShowHeader="false">
            <Items>
                <f:Panel runat="server" BlockMD="12" BlockLG="4"
                    Layout="Block" BlockConfigSpace="20px" ShowBorder="false" ShowHeader="false">
                    <Items>
                        <f:Panel CssClass="blockpanel" BlockMD="6" BlockLG="12" Height="200px"
                            runat="server" ShowBorder="true" ShowHeader="true" Title="1">
                        </f:Panel>
                        <f:Panel CssClass="blockpanel" BlockMD="6" BlockLG="12" Height="200px"
                            runat="server" ShowBorder="true" ShowHeader="true" Title="2">
                        </f:Panel>
                    </Items>
                </f:Panel>
                <f:Panel runat="server" BlockMD="12" BlockLG="8"
                    Layout="Block" BlockConfigSpace="20px" ShowBorder="false" ShowHeader="false">
                    <Items>
                        <f:Panel CssClass="blockpanel" BlockSM="6" BlockMD="3" Height="150px"
                            runat="server" ShowBorder="true" ShowHeader="true" Title="3">
                        </f:Panel>
                        <f:Panel CssClass="blockpanel" BlockSM="6" BlockMD="3" Height="150px"
                            runat="server" ShowBorder="true" ShowHeader="true" Title="4">
                        </f:Panel>
                        <f:Panel CssClass="blockpanel" BlockSM="6" BlockMD="3" Height="150px"
                            runat="server" ShowBorder="true" ShowHeader="true" Title="5">
                        </f:Panel>
                        <f:Panel CssClass="blockpanel" BlockSM="6" BlockMD="3" Height="150px"
                            runat="server" ShowBorder="true" ShowHeader="true" Title="6">
                        </f:Panel>
                        <f:Panel CssClass="blockpanel" Block="12" Height="250px"
                            runat="server" ShowBorder="true" ShowHeader="true" Title="7">
                        </f:Panel>
                    </Items>
                </f:Panel>
                <f:Panel runat="server" BlockMD="12"
                    Layout="Block" BlockConfigSpace="20px" ShowBorder="false" ShowHeader="false">
                    <Items>
                        <f:Panel CssClass="blockpanel" BlockMD="4" Height="200px"
                            runat="server" ShowBorder="true" ShowHeader="true" Title="8">
                        </f:Panel>
                        <f:Panel CssClass="blockpanel" BlockMD="4" Height="200px"
                            runat="server" ShowBorder="true" ShowHeader="true" Title="9">
                        </f:Panel>
                        <f:Panel CssClass="blockpanel" BlockMD="4" Height="200px"
                            runat="server" ShowBorder="true" ShowHeader="true" Title="10">
                        </f:Panel>
                        <f:Panel CssClass="blockpanel" Block="12" Height="300px"
                            runat="server" ShowBorder="true" ShowHeader="true" Title="11">
                        </f:Panel>
                    </Items>
                </f:Panel>
            </Items>
        </f:Panel>
    </form>
</body>
</html>
