<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="only_md.aspx.cs" Inherits="Sigesoft.Server.WebClientAdmin.UI.block.only_md" %>

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
                <f:Panel CssClass="blockpanel" BlockMD="3"
                    runat="server" ShowBorder="true" ShowHeader="true" Title="1">
                </f:Panel>
                <f:Panel CssClass="blockpanel" BlockMD="3"
                    runat="server" ShowBorder="true" ShowHeader="true" Title="2">
                </f:Panel>
                <f:Panel CssClass="blockpanel" BlockMD="3"
                    runat="server" ShowBorder="true" ShowHeader="true" Title="3">
                </f:Panel>
                <f:Panel CssClass="blockpanel" BlockMD="3"
                    runat="server" ShowBorder="true" ShowHeader="true" Title="4">
                </f:Panel>
                <f:Panel CssClass="blockpanel" BlockMD="9"
                    runat="server" ShowBorder="true" ShowHeader="true" Title="5">
                </f:Panel>
                <f:Panel CssClass="blockpanel" BlockMD="3"
                    runat="server" ShowBorder="true" ShowHeader="true" Title="6">
                </f:Panel>
                <f:Panel CssClass="blockpanel" BlockMD="6"
                    runat="server" ShowBorder="true" ShowHeader="true" Title="7">
                </f:Panel>
                <f:Panel CssClass="blockpanel" BlockMD="6"
                    runat="server" ShowBorder="true" ShowHeader="true" Title="8">
                </f:Panel>
            </Items>
        </f:Panel>
    </form>
</body>
</html>
