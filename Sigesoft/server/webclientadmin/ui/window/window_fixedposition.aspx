<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="window_fixedposition.aspx.cs" Inherits="Sigesoft.Server.WebClientAdmin.UI.window.window_fixedposition" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title></title>
    <style>
        body {
            background: url(../res/images/bg/small/3.jpg) !important;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server" />
        <div style="height: 1200px;">
        </div>

        <f:Window ID="Window1" Width="200px" Height="80px" PositionX="Left" PositionY="Top"
            runat="server" IsModal="false" AutoScroll="true" BodyPadding="10px" ShowHeader="false" FixedPosition="true">
            <Content>
                PositionX="Left" PositionY="Top"
            </Content>
        </f:Window>

        <f:Window ID="Window2" Width="200px" Height="80px" PositionX="Left" PositionY="Center"
            runat="server" IsModal="false" AutoScroll="true" BodyPadding="10px" ShowHeader="false" FixedPosition="true">
            <Content>
                PositionX="Left" PositionY="Center"
            </Content>
        </f:Window>

        <f:Window ID="Window3" Width="200px" Height="80px" PositionX="Left" PositionY="Bottom"
            runat="server" IsModal="false" AutoScroll="true" BodyPadding="10px" ShowHeader="false" FixedPosition="true">
            <Content>
                PositionX="Left" PositionY="Bottom"
            </Content>
        </f:Window>


        <f:Window ID="Window4" Width="200px" Height="80px" PositionX="Center" PositionY="Top"
            runat="server" IsModal="false" AutoScroll="true" BodyPadding="10px" ShowHeader="false" FixedPosition="true">
            <Content>
                PositionX="Center" PositionY="Top"
            </Content>
        </f:Window>

        <f:Window ID="Window5" Width="200px" Height="80px" PositionX="Center" PositionY="Center"
            runat="server" IsModal="false" AutoScroll="true" BodyPadding="10px" ShowHeader="false" FixedPosition="true">
            <Content>
                PositionX="Center" PositionY="Center"
            </Content>
        </f:Window>

        <f:Window ID="Window6" Width="200px" Height="80px" PositionX="Center" PositionY="Bottom"
            runat="server" IsModal="false" AutoScroll="true" BodyPadding="10px" ShowHeader="false" FixedPosition="true">
            <Content>
                PositionX="Center" PositionY="Bottom"
            </Content>
        </f:Window>

        <f:Window ID="Window7" Width="200px" Height="80px" PositionX="Right" PositionY="Top"
            runat="server" IsModal="false" AutoScroll="true" BodyPadding="10px" ShowHeader="false" FixedPosition="true">
            <Content>
                PositionX="Right" PositionY="Top"
            </Content>
        </f:Window>

        <f:Window ID="Window8" Width="200px" Height="80px" PositionX="Right" PositionY="Center"
            runat="server" IsModal="false" AutoScroll="true" BodyPadding="10px" ShowHeader="false" FixedPosition="true">
            <Content>
                PositionX="Right" PositionY="Center"
            </Content>
        </f:Window>

        <f:Window ID="Window9" Width="200px" Height="80px" PositionX="Right" PositionY="Bottom"
            runat="server" IsModal="false" AutoScroll="true" BodyPadding="10px" ShowHeader="false" FixedPosition="true">
            <Content>
                PositionX="Right" PositionY="Bottom"
            </Content>
        </f:Window>


    </form>
</body>
</html>
