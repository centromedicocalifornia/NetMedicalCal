<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="window_height_autosize.aspx.cs" Inherits="Sigesoft.Server.WebClientAdmin.UI.window.window_height_autosize" %>

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
        Note: Change the page size and observe the height of the window.
        <ul>
            <li>Set window property KeepLastSize=true, so that change it height is still valid when the window is hidden</li>
        </ul>
        <br />
        <br />
        <f:Window ID="Window1" Width="650px" Icon="TagBlue" Title="Window" Hidden="true"
            EnableMaximize="false" EnableCollapse="false" runat="server" EnableResize="false" FixedPosition="true"
            IsModal="false" AutoScroll="true" BodyPadding="10px" KeepLastSize="true">
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
    <script>

        var window1ClientID = '<%= Window1.ClientID %>';

        F.ready(function () {

            var window1 = F(window1ClientID);

            function resetWindow1Height() {

                // Empty upper left corner positioning
                window1.top = undefined;
                window1.left = undefined;

                // Change height
                window1.setHeight($(window).height() - 50);

            }

            // When the page is loaded, firstly set the height of the window
            resetWindow1Height();

            // Page size change event
            F.windowResize(function () {

                resetWindow1Height();

            });

        });

    </script>
</body>
</html>
