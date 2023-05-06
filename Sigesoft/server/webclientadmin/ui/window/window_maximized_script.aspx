<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="window_maximized_script.aspx.cs" Inherits="Sigesoft.Server.WebClientAdmin.UI.window.window_maximized_script" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server" />
        <f:Button ID="btnShowWindowMax" Text="Show window (maximize)" runat="server" EnablePostBack="false">
        </f:Button>
        <br />
        <br />
        <f:Button ID="btnShowWindow900" Text="Show window (set size to 900px * 450px)" runat="server" EnablePostBack="false">
        </f:Button>
        <br />
        <br />
        <f:Button ID="btnShowWindowLargeHeight" Text="Show window (full page height)" runat="server" EnablePostBack="false">
        </f:Button>
        <br />
        <br />
        <f:Button ID="btnShowWindow" Text="Show window (initial value)" runat="server" EnablePostBack="false">
        </f:Button>
        <br />
        <br />
        <f:Button ID="btnCloseWindow" Text="Hide window" runat="server" EnablePostBack="false">
        </f:Button>
        <br />
        <br />

        <br />
        <f:Window ID="Window1" Width="650px" Height="300px" Icon="TagBlue" Title="Window"
            runat="server" EnableResize="true"
            IsModal="false" AutoScroll="true" BodyPadding="10px" EnableMaximize="true">
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
                <br />
                <br />
                
            </Content>
        </f:Window>
    </form>
</body>
</html>
