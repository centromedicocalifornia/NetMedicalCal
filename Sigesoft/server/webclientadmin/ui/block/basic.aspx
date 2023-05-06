<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="basic.aspx.cs" Inherits="Sigesoft.Server.WebClientAdmin.UI.block.basic" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title></title>
    <style>
        body.f-body {
            overflow-x: hidden;
        }

        .f-field {
            margin-bottom: 0 !important;
        }
    </style>
</head>
<body class="f-body-bgcolor">
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server" />
        <f:ContentPanel ID="Panel8" IsFluid="true" CssClass="blockpanel" runat="server" ShowBorder="true" EnableCollapse="false"
            BodyPadding="10px" ShowHeader="false">
            <h4>Responsive layout</h4>
            To enrich the page display, we introduced the industry's popular responsive layout.Unlike the Bootstrap CSS3 media query implementation, FineUI fully implements the responsive layout through JavaScript, not only to fit into the existing layout system, but also to be more flexible.
            <br />
            We divide the container into 12 pieces (customizable) and then define each block size in a different screen so that the sum equals 12, thus displaying a different layout on the phone, the tablet, the desktop, and the large size screen.
            <br />
            <h4>Layout rules</h4>
            <ol>
                <li>Defines the Layout=Block property for the parent container.</li>
                <li>Use BlockMD=6 to define the responsive width for each block.
                <ul>
                    <li>The suffix MD represents different screen sizes (Block, BlockSM, BlockMD, BlockLG).
                    </li>
                    <li>Property 6 represents the width (6-12) of each piece.</li>
                    <li>If the sum of the widths of multiple blocks equals 12, then occupy one row and the extra block will be on the other line.</li>
                </ul>
                </li>
            </ol>
            <br />
            <h4>Responsive rules</h4>
            <table class="result" style="width: 100%;">
                <tr>
                    <td></td>
                    <td style="font-weight: normal;">Extra small screen (mobile phone)</td>
                    <td style="font-weight: normal;">Small screen (tablet)</td>
                    <td style="font-weight: normal;">Medium screen (desktop)</td>
                    <td style="font-weight: normal;">Large screen (large size display)</td>
                </tr>
                <tr>
                    <td>Screen size</td>
                    <td>&lt; 768px</td>
                    <td>&gt;= 768px</td>
                    <td>&gt;= 992px</td>
                    <td>&gt;= 1200px</td>
                </tr>
                <tr>
                    <td>Block property</td>
                    <td>Block</td>
                    <td>BlockSM</td>
                    <td>BlockMD</td>
                    <td>BlockLG</td>
                </tr>
                <tr>
                    <td>Responsive behavior</td>
                    <td>Always arrange horizontally</td>
                    <td colspan="3">Horizontal arrangement, cascading when less than critical value</td>
                </tr>
            </table>
        </f:ContentPanel>
        <br />
        <br />
        Arrange horizontally below the medium screen and cascade below the small screen:
        <br />
        <br />
        <f:Panel ID="Panel5" IsFluid="true" CssClass="blockpanel" runat="server" ShowBorder="true" EnableCollapse="false"
            Layout="Block" BodyPadding="10px" ShowHeader="false">
            <Items>
                <f:Panel ID="Panel6" BlockMD="6"
                    runat="server" BodyPadding="10px" ShowBorder="true" ShowHeader="false">
                    <Items>
                        <f:Label ID="Label4" runat="server" EncodeText="false" Text="BlockMD=6">
                        </f:Label>
                    </Items>
                </f:Panel>
                <f:Panel ID="Panel7" BlockMD="6"
                    runat="server" BodyPadding="10px" ShowBorder="true" ShowHeader="false">
                    <Items>
                        <f:Label ID="Label5" runat="server" EncodeText="false" Text="BlockMD=6">
                        </f:Label>
                    </Items>
                </f:Panel>
            </Items>
        </f:Panel>

        <br />
        <br />
        Arranged horizontally below the large screen, displayed in two lines below the medium screen, stacked below the small screen:
        <br />
        <br />
        <f:Panel ID="Panel2" IsFluid="true" CssClass="blockpanel" runat="server" ShowBorder="true" EnableCollapse="false"
            Layout="Block" BodyPadding="10px" ShowHeader="false">
            <Items>
                <f:Panel ID="Panel1" BlockMD="6" BlockLG="4"
                    runat="server" BodyPadding="10px" ShowBorder="true" ShowHeader="false">
                    <Items>
                        <f:Label ID="Label1" runat="server" EncodeText="false" Text="BlockMD=6<br/>BlockLG=4">
                        </f:Label>
                    </Items>
                </f:Panel>
                <f:Panel ID="Panel3" BlockMD="6" BlockLG="4"
                    runat="server" BodyPadding="10px" ShowBorder="true" ShowHeader="false">
                    <Items>
                        <f:Label ID="Label2" runat="server" EncodeText="false" Text="BlockMD=6<br/>BlockLG=4">
                        </f:Label>
                    </Items>
                </f:Panel>
                <f:Panel ID="Panel4" BlockMD="12" BlockLG="4"
                    runat="server" BodyPadding="10px" ShowBorder="true" ShowHeader="false">
                    <Items>
                        <f:Label ID="Label3" runat="server" EncodeText="false" Text="BlockMD=12<br/>BlockLG=4">
                        </f:Label>
                    </Items>
                </f:Panel>
            </Items>
        </f:Panel>
        <br />
        <br />
        Use BlockConfigSpace=10 to set the spacing between blocks:
        <br />
        <br />
        <f:Panel ID="Panel9" IsFluid="true" CssClass="blockpanel" runat="server" ShowBorder="true" EnableCollapse="false"
            Layout="Block" BlockConfigSpace="10px" BodyPadding="10px" ShowHeader="false">
            <Items>
                <f:Panel ID="Panel10" BlockSM="6" BlockMD="9" BlockLG="4"
                    runat="server" BodyPadding="10px" ShowBorder="true" ShowHeader="false">
                    <Items>
                        <f:Label ID="Label7" runat="server" EncodeText="false" Text="BlockSM=6<br/>BlockMD=9<br/>BlockLG=4">
                        </f:Label>
                    </Items>
                </f:Panel>
                <f:Panel ID="Panel11" BlockSM="6" BlockMD="3" BlockLG="4"
                    runat="server" BodyPadding="10px" ShowBorder="true" ShowHeader="false">
                    <Items>
                        <f:Label ID="Label8" runat="server" EncodeText="false" Text="BlockSM=6<br/>BlockMD=3<br/>BlockLG=4">
                        </f:Label>
                    </Items>
                </f:Panel>
                <f:Panel ID="Panel12" BlockSM="12" BlockMD="12" BlockLG="4"
                    runat="server" BodyPadding="10px" ShowBorder="true" ShowHeader="false">
                    <Items>
                        <f:Label ID="Label9" runat="server" EncodeText="false" Text="BlockSM=12<br/>BlockMD=12<br/>BlockLG=4">
                        </f:Label>
                    </Items>
                </f:Panel>
            </Items>
        </f:Panel>
    </form>
</body>
</html>
