<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="window_image_resize.aspx.cs" Inherits="Sigesoft.Server.WebClientAdmin.UI.window.window_image_resize" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title></title>
    <style>
        #mylogo {
            position: absolute;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server" />
        <f:Window ID="Window2" Width="650px" Height="300px" Icon="TagBlue" Title="Window (watch the size of the picture change as the window size change)" EnableClose="false"
            EnableMaximize="true" EnableCollapse="false" runat="server" EnableResize="true"
            IsModal="false" AutoScroll="true" BodyPadding="10px">
            <Content>
                <img id="mylogo" src="../res/images/logo/logo3.png" alt="Logo" />
            </Content>
            <Listeners>
                <f:Listener Event="resize" Handler="onWindowResize" />
                <f:Listener Event="render" Handler="onWindowResize" />
            </Listeners>
        </f:Window>
    </form>
    <script>

        // Keep the image's aspect ratio
        var LOGO_WIDTH = 127, LOGO_HEIGHT = 81;

        // resize, render
        function onWindowResize() {
            var bodyWidth = this.bodyEl.width();
            var bodyHeight = this.bodyEl.height();

            var logoWidth = bodyWidth;
            var logoHeight = Math.floor(bodyWidth * LOGO_HEIGHT / LOGO_WIDTH);
            if (logoHeight > bodyHeight) {
                logoHeight = bodyHeight;
                logoWidth = Math.floor(bodyHeight * LOGO_WIDTH / LOGO_HEIGHT);
            }

            $('#mylogo').css({
                top: Math.floor((bodyHeight - logoHeight) / 2),
                left: Math.floor((bodyWidth - logoWidth) / 2),
                width: logoWidth,
                height: logoHeight
            });

        }

    </script>
</body>
</html>
