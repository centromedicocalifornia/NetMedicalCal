<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="nine_blocks.aspx.cs" Inherits="Sigesoft.Server.WebClientAdmin.UI.block.nine_blocks" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title></title>
    <style>
        body.f-body {
            overflow-x: hidden;
        }

        .blockpanel {
            text-align: center;
        }
    </style>
</head>
<body class="f-body-bgcolor">
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server" />
        <f:Panel ID="Panel1" IsFluid="true" runat="server"
            Layout="Block" BlockConfigSpace="0" ShowBorder="false" ShowHeader="false">
            <Items>
                <f:ContentPanel CssClass="blockpanel" Block="4"
                    runat="server" ShowBorder="true" ShowHeader="false">
                    1
                </f:ContentPanel>
                <f:ContentPanel CssClass="blockpanel" Block="4"
                    runat="server" ShowBorder="true" ShowHeader="false">
                    2
                </f:ContentPanel>
                <f:ContentPanel CssClass="blockpanel" Block="4"
                    runat="server" ShowBorder="true" ShowHeader="false">
                    3
                </f:ContentPanel>
                <f:ContentPanel CssClass="blockpanel" Block="4"
                    runat="server" ShowBorder="true" ShowHeader="false">
                    4
                </f:ContentPanel>
                <f:ContentPanel CssClass="blockpanel" Block="4"
                    runat="server" ShowBorder="true" ShowHeader="false">
                    5
                </f:ContentPanel>
                <f:ContentPanel CssClass="blockpanel" Block="4"
                    runat="server" ShowBorder="true" ShowHeader="false">
                    6
                </f:ContentPanel>
                <f:ContentPanel CssClass="blockpanel" Block="4"
                    runat="server" ShowBorder="true" ShowHeader="false">
                    7
                </f:ContentPanel>
                <f:ContentPanel CssClass="blockpanel" Block="4"
                    runat="server" ShowBorder="true" ShowHeader="false">
                    8
                </f:ContentPanel>
                <f:ContentPanel CssClass="blockpanel" Block="4"
                    runat="server" ShowBorder="true" ShowHeader="false">
                    9
                </f:ContentPanel>
            </Items>
        </f:Panel>
    </form>
    <script>
        var Panel1ClientID = '<%= Panel1.ClientID %>';

        function doLayoutBlock() {
            $('.blockpanel .f-panel-body').each(function () {
                var bodyEl = $(this);
                var bodyElWidth = bodyEl.width() + 'px';
                bodyEl.children(':first').css({
                    'height': bodyElWidth,
                    'line-height': bodyElWidth
                });
            });

            F(Panel1ClientID).doLayout();
        }

        F.ready(function () {

            F.windowResize(function () {
                doLayoutBlock();
            });

            doLayoutBlock();

        });
    </script>
</body>
</html>
