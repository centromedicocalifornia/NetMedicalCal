<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="custom_blocks.aspx.cs" Inherits="Sigesoft.Server.WebClientAdmin.UI.block.custom_blocks" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title></title>
    <style>
        body.f-body {
            overflow-x: hidden;
        }

        .blockpanel {
            min-height: 200px;
        }
    </style>
</head>
<body class="f-body-bgcolor">
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server" />
        <f:Panel IsFluid="true" runat="server"
            Layout="Block" BlockConfigSpace="20px" BlockConfigBlockCount="20" ShowBorder="false" ShowHeader="false">
            <Items>
                <f:Panel CssClass="blockpanel" BlockSM="10" BlockMD="4"
                    runat="server" ShowBorder="true" ShowHeader="true" Title="4">
                </f:Panel>
                <f:Panel CssClass="blockpanel" BlockSM="5" BlockMD="4"
                    runat="server" ShowBorder="true" ShowHeader="true" Title="4">
                </f:Panel>
                <f:Panel CssClass="blockpanel" BlockSM="5" BlockMD="4"
                    runat="server" ShowBorder="true" ShowHeader="true" Title="4">
                </f:Panel>
                <f:Panel CssClass="blockpanel" BlockSM="10" BlockMD="4"
                    runat="server" ShowBorder="true" ShowHeader="true" Title="4">
                </f:Panel>
                <f:Panel CssClass="blockpanel" BlockSM="10" BlockMD="4"
                    runat="server" ShowBorder="true" ShowHeader="true" Title="4">
                </f:Panel>
                <f:Panel CssClass="blockpanel" BlockSM="20" BlockMD="10"
                    runat="server" ShowBorder="true" ShowHeader="true" Title="10">
                </f:Panel>
                <f:Panel CssClass="blockpanel" BlockSM="10" BlockMD="10"
                    runat="server" ShowBorder="true" ShowHeader="true" Title="10">
                </f:Panel>
                <f:Panel CssClass="blockpanel" BlockSM="10" BlockMD="20"
                    runat="server" ShowBorder="true" ShowHeader="true" Title="20">
                </f:Panel>
            </Items>
        </f:Panel>
    </form>
</body>
</html>
