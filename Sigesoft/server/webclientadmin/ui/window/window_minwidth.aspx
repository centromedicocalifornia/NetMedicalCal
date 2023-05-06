<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="window_minwidth.aspx.cs" Inherits="Sigesoft.Server.WebClientAdmin.UI.window.window_minwidth" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server" />
        <f:Button ID="btnShowWindow" Text="Show window" runat="server" EnablePostBack="false">
        </f:Button>
        <br />
        <br />
        <f:Button ID="btnCloseWindow" Text="Hide window" runat="server" EnablePostBack="false">
        </f:Button>
        <br />
        <br />
        <br />
        Note: This window's initial width and height is 400px, the minimum width and height is 300px, the maximum width and height is 600px, you can try to change the size of the window.
        <br />
        <f:Window ID="Window1" Width="400px" Height="400px" Icon="TagBlue" Title="Window" 
            EnableMaximize="false" runat="server" EnableResize="true"
            IsModal="false" AutoScroll="true" BodyPadding="10px"
            MinWidth="300px" MinHeight="300px" MaxHeight="600px" MaxWidth="600px">
            <Content>
                <p>
                    <a href="http://tech.163.com/special/jobsdead/" style="font-size: 18px" target="_blank"><b>Steve Jobs</b></a>
                </p>
                <p>
                    Steve Jobs was born on February 24, 1955, one of Apple's founders, who had been rated as the best CEO in the United States in recent years, and the industry commented that "Apple is Steve Jobs." 
                </p>
                <p>
                    Unfortunately, Apple's huge success is still unable to give jobs a healthy body, he was found in 2003 with pancreatic cancer and liver cancer, at stake in jobs after 8 years of fighting cancer, 3 sick, several surgeries, on August 25, 2011 formally announced the resignation of CEO post. October 6, 2011, Steve died the next day after Apple released the iphone 4S.
                </p>
            </Content>
        </f:Window>
    </form>
</body>
</html>
