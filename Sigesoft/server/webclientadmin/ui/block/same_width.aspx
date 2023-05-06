<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="same_width.aspx.cs" Inherits="Sigesoft.Server.WebClientAdmin.UI.block.same_width" %>

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
            Layout="Block" BlockConfigSpace="20px" ShowBorder="false" ShowHeader="false">
            <Items>
                <f:Panel CssClass="blockpanel" BlockSM="6" BlockMD="4" BlockLG="3"
                    runat="server" ShowBorder="true" ShowHeader="true" Title="1">
                </f:Panel>
                <f:Panel CssClass="blockpanel" BlockSM="6" BlockMD="4" BlockLG="3"
                    runat="server" ShowBorder="true" ShowHeader="true" Title="2">
                </f:Panel>
                <f:Panel CssClass="blockpanel" BlockSM="6" BlockMD="4" BlockLG="3"
                    runat="server" ShowBorder="true" ShowHeader="true" Title="3">
                </f:Panel>
                <f:Panel CssClass="blockpanel" BlockSM="6" BlockMD="4" BlockLG="3"
                    runat="server" ShowBorder="true" ShowHeader="true" Title="4">
                </f:Panel>
                <f:Panel CssClass="blockpanel" BlockSM="6" BlockMD="4" BlockLG="3"
                    runat="server" ShowBorder="true" ShowHeader="true" Title="5">
                </f:Panel>
                <f:Panel CssClass="blockpanel" BlockSM="6" BlockMD="4" BlockLG="3"
                    runat="server" ShowBorder="true" ShowHeader="true" Title="6">
                </f:Panel>
                <f:Panel CssClass="blockpanel" BlockSM="6" BlockMD="4" BlockLG="3"
                    runat="server" ShowBorder="true" ShowHeader="true" Title="7">
                </f:Panel>
                <f:Panel CssClass="blockpanel" BlockSM="6" BlockMD="4" BlockLG="3"
                    runat="server" ShowBorder="true" ShowHeader="true" Title="8">
                </f:Panel>
            </Items>
        </f:Panel>
    </form>
</body>
</html>
