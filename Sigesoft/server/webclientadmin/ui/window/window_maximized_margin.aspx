<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="window_maximized_margin.aspx.cs" Inherits="Sigesoft.Server.WebClientAdmin.UI.window_maximized_margin" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title></title>
    <style>
        .f-window.f-window-maximized {
            margin: 50px;
            border-width: 1px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server" />
        <f:Button ID="btnShowWindow" Text="Show window" runat="server" EnablePostBack="false">
        </f:Button>
        <br />
        <br />
        <f:Window ID="Window1" Width="650px" Height="300px" Icon="TagBlue" Title="Window (margin 50px when maximized)"
            EnableMaximize="true" runat="server" EnableResize="true"
            IsModal="true" AutoScroll="true" BodyPadding="10px" Maximized="true">
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
