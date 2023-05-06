<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="window_bottomright.aspx.cs" Inherits="Sigesoft.Server.WebClientAdmin.UI.window.window_bottomright" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title></title>
    <style>
        .mywnd {
            top: auto !important;
            left: auto !important;
            bottom: 2px !important;
            right: 2px !important;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server" />
        <f:Button ID="btnShowInClient" Text="Show window" EnablePostBack="false"
            runat="server">
        </f:Button>
        <br />
        <br />
        <f:Button ID="btnHideInClient" Text="Hide window" EnablePostBack="false"
            runat="server">
        </f:Button>

        <f:Window ID="Window2" Width="500px" Height="200px" Icon="TagBlue" Title="Window"
            runat="server" EnableDrag="false" CssClass="mywnd"
            IsModal="false" AutoScroll="true" BodyPadding="10px">
            <Content>
                <p>
                    <a href="http://tech.163.com/special/jobsdead/" style="font-size: 18px" target="_blank"><b>Steve Jobs</b></a>
                </p>
                <p>
                    Steve Jobs was born on February 24, 1955, one of Apple's founders, who had been rated as the best CEO in the United States in recent years, and the industry commented that "Apple is Steve Jobs." 
                </p>
            </Content>
        </f:Window>
    </form>
</body>
</html>
