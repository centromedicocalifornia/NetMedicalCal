<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="window_percent.aspx.cs" Inherits="Sigesoft.Server.WebClientAdmin.UI.window.window_percent" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title></title>
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

        <f:Window ID="Window2" Icon="TagBlue" Title="Window (PercentWidth=50% PercentHeight=50%)" PercentWidth="50%" PercentHeight="50%"
            runat="server" IsModal="false" AutoScroll="true" BodyPadding="10px">
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
