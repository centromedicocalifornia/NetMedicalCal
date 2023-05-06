<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="main.aspx.cs" Inherits="Sigesoft.Server.WebClientAdmin.UI.Common.main" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title></title>
    <meta name="sourcefiles" content="~/index.aspx;~/res/css/index.css;~/res/js/index.js;~/code/PageBase.cs" />
    <style>
        body {
            padding: 0 10px;
        }

        .important {
            border-style: solid;
            border-width: 3px;
            display: inline-block;
            padding: 20px;
            position: absolute;
            top: 10px;
            right: 10px;
        }

        .weixin {
            position: fixed;
            bottom: 10px;
            right: 10px;
            text-align: center;
            border: solid 1px #ddd;
            padding: 10px;
            background-color: #efefef;
        }


        ul.list {
            list-style-type: none;
            padding: 0;
            margin: 0;
        }

            ul.list li {
                margin-bottom: 5px;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server"></f:PageManager>
        <h2>FineUI WebForms</h2>
        jQuery based professional ASP.NET Controls
                                        
                                   
        <br />
        <h2>Support Browsers</h2>
        Chrome, Firefox, Safari, Edge, IE8.0+<sup>*</sup>

        <br />
        <h2>License</h2>
        Commercial
                                            
        <br />
        <h2>Related Links</h2>
        <ul class="list">
            <li>Homepage:<a target="_blank" href="http://fineui.us/">http://fineui.us/</a>
            </li>
            <li>Examples:<a target="_blank" href="http://webforms.fineui.us/">http://webforms.fineui.us/</a>
            </li>
            <%--<li>Versions: <a target="_blank" href="http://fineui.us/version_pro/">http://fineui.us/version_pro/</a>
            </li>--%>
        </ul>
        <br />
        <br />
        <div style="font-size: 11px; ">
            * Recommend Chrome, Firefox, Safari, Edge, IE11 browser to get the best performance.
            <div style="margin:10px 8px;opacity:0.6;">
                IE8.0 browser limited support, there are known issues:
                <ul style="margin-top: 2px; padding-left: 20px;">
                    <li>Worst performance compared to other modern browsers.</li>
                    <li>Round corner and other CSS3 features are not supported.</li>
                    <li>TabStrip does not support left and right title bar.</li>
                    <li>Font icon may be randomly missing when interact with IFrame.</li>
                </ul>
            </div>
        </div>
        <br />
        
    </form>
</body>
</html>
