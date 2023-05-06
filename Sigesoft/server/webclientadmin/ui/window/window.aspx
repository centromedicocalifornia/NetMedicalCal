<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="window.aspx.cs" Inherits="Sigesoft.Server.WebClientAdmin.UI.window.window" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server" />
        <f:Button ID="btnShowInServer" CssClass="marginr" Text="Show window (server-side code)" runat="server"
            OnClick="btnShowInServer_Click">
        </f:Button>
        <f:Button ID="btnHideInServer" Text="Hide window (server-side code)" runat="server" OnClick="btnHideInServer_Click">
        </f:Button>
        <br />
        <br />
        <f:Button ID="btnShowInClient" CssClass="marginr" Text="Show window (client-side code)" EnablePostBack="false"
            runat="server">
        </f:Button>
        <f:Button ID="btnHideInClient" CssClass="marginr" Text="Hide window (client-side code)" EnablePostBack="false"
            runat="server">
        </f:Button>
        <f:Button ID="btnHideInClient2" Text="Hide window, with postback parameters (client-side code)" EnablePostBack="false"
            runat="server">
        </f:Button>
        <br />
        <br />
        <f:Window ID="Window2" Width="650px" Height="300px" Icon="TagBlue" Title="Window"
            EnableMaximize="true" EnableCollapse="true" runat="server" EnableResize="true"
            IsModal="false" CloseAction="HidePostBack" OnClose="Window2_Close" AutoScroll="true" BodyPadding="10px">
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
            <Listeners>
                <f:Listener Event="resize" Handler="onWindowResize" />
            </Listeners>
        </f:Window>
    </form>
    <script>

        // Client event for window size changes
        function onWindowResize() {
            F.notify("window size is changed, width: " + this.width + " height: " + this.height);
        }

    </script>
</body>
</html>
