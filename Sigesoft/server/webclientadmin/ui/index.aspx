<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="Sigesoft.Server.WebClientAdmin.UI.index" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>FineUI WebForms Examples - jQuery based professional ASP.NET Controls</title>
    <link rel="shortcut icon" type="image/x-icon" href="favicon.ico" />
    <meta name="Title" content="FineUI WebForms - jQuery based professional ASP.NET Controls" />
    <meta name="Description" content="FineUI WebForms - jQuery based professional ASP.NET Controls" />
    <meta name="Keywords" content="jQuery,jQueryUI,FineUI,ASP.NET,Controls,AJAX,Web2.0" />
    <link type="text/css" rel="stylesheet" id="linkIndexCSS" runat="server" href="~/res/css/index.css" />
    <link type="text/css" rel="stylesheet" id="linkMobileViewCss" runat="server" href="~/res/css/mobileview.css" />
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="mainPanel" runat="server"></f:PageManager>
        <f:Panel ID="mainPanel" Layout="Region" CssClass="mainpanel" ShowBorder="false" ShowHeader="false" runat="server">
            <Items>
                <f:Panel ID="sidebarRegion" CssClass="sidebarregion bgpanel" RegionPosition="Left"
                    ShowBorder="false" Width="260" ShowHeader="false"
                    EnableCollapse="false" Collapsed="false" Layout="VBox" runat="server"
                    RegionSplit="true" RegionSplitIcon="false" RegionSplitWidth="3" RegionSplitTransparent="true" >
                    <Items>
                        <f:ContentPanel CssClass="topregion" ShowBorder="false" ShowHeader="false" runat="server">
                            <div id="sideheader" class="f-widget-header f-mainheader">
                                <a class="logo" href="./" title="FineUI WebForms" id="logoTitle" runat="server">FineUI WebForms</a>
                            </div>
                        </f:ContentPanel>
                        <f:Panel ID="leftPanel" CssClass="leftregion" BoxFlex="1" ShowBorder="true" ShowHeader="false"
                            Layout="Fit" runat="server">
                            <Items>
                                <f:Tree ID="treeMenu" ShowBorder="false" ShowHeader="false" EnableSingleClickExpand="true" runat="server"></f:Tree>
                            </Items>
                        </f:Panel>
                    </Items>
                    <Listeners>
                        <f:Listener Event="splitdrag" Handler="onSidebarSplitDrag" />
                    </Listeners>
                </f:Panel>
                <f:Panel ID="bodyRegion" CssClass="bodyregion" RegionPosition="Center" ShowBorder="false" ShowHeader="false"
                    Layout="VBox" runat="server">
                    <Items>
                        <f:ContentPanel ID="topPanel" CssClass="topregion" ShowBorder="false" ShowHeader="false" runat="server">
                            <div id="header" class="f-widget-header f-mainheader">
                                <div class="header-left">
                                    <f:Button runat="server" ID="btnCollapseSidebar" CssClass="icononlyaction" ToolTip="Collapse/expand sidebar" IconAlign="Top" IconFont="_Fold"
                                        EnablePostBack="false" EnableDefaultState="false" EnableDefaultCorner="false" TabIndex="-1">
                                        <Listeners>
                                            <f:Listener Event="click" Handler="onFoldClick" />
                                        </Listeners>
                                    </f:Button>
                                    <div id="breadcrumb">
                                        <div class="breadcrumb-inner">
											<span class="breadcrumb-last">Home</span>
										</div>
                                        <div class="breadcrumb-icons">
                                            <a data-qtip="View Source Code" href="javascript:onToolSourceCodeClick();"><i class="f-icon f-iconfont f-iconfont-code"></i></a>
                                            <a data-qtip="Refresh Page" href="javascript:onToolRefreshClick();"><i class="f-icon f-iconfont f-iconfont-refresh"></i></a>
											<a data-qtip="Open in New Tab" href="javascript:onToolNewWindowClick();"><i class="f-icon f-iconfont f-iconfont-new-tab"></i></a>
                                        </div>
                                    </div>
                                </div>
                                <div class="header-right">
                                    <f:TwinTriggerBox ID="ttbxSearch" CssClass="searchbox collapsed" ShowLabel="false" Trigger1Icon="Clear" ShowTrigger1="false" 
										EmptyText="Search" Trigger2Icon="Search"
                                        EnableTrigger1PostBack="false" EnableTrigger2PostBack="false" runat="server" Width="24px">
                                        <Listeners>
                                            <f:Listener Event="trigger1click" Handler="onSearchTrigger1Click" />
                                            <f:Listener Event="trigger2click" Handler="onSearchTrigger2Click" />
                                            <f:Listener Event="blur" Handler="onSearchBlur" />
                                        </Listeners>
                                    </f:TwinTriggerBox>
                                    <f:Button runat="server" CssClass="icononlyaction" ID="btnThemeSelect" ToolTip="Themes" IconAlign="Top" IconFont="_Skin"
                                        EnablePostBack="false" EnableDefaultState="false" EnableDefaultCorner="false" TabIndex="-1">
                                        <Listeners>
                                            <f:Listener Event="click" Handler="onThemeSelectClick" />
                                        </Listeners>
                                    </f:Button>
                                    <f:Button runat="server" CssClass="userpicaction" Text="Sanshi Zhang" IconUrl="~/res/images/my_face_80.jpg" IconAlign="Left"
                                        EnablePostBack="false" EnableDefaultState="false" EnableDefaultCorner="false">
                                        <Menu runat="server" ID="menuSettings">
                                            <f:MenuButton runat="server" EnablePostBack="false" ID="MenuDisplayMode" Text="Display Mode">
                                                <Menu runat="server">
                                                    <Items>
														<f:MenuCheckBox Text="Compact" ID="MenuDisplayModeCompact" AttributeDataTag="compact" GroupName="MenuDisplayMode" runat="server">
                                                        </f:MenuCheckBox>
														<f:MenuCheckBox Text="Small" ID="MenuDisplayModeSmall" AttributeDataTag="small" GroupName="MenuDisplayMode" runat="server">
                                                        </f:MenuCheckBox>
                                                        <f:MenuCheckBox Text="Normal" ID="MenuDisplayModeNormal" AttributeDataTag="normal" Checked="true" GroupName="MenuDisplayMode" runat="server">
                                                        </f:MenuCheckBox>
                                                        <f:MenuCheckBox Text="Large" ID="MenuDisplayModeLarge" AttributeDataTag="large" GroupName="MenuDisplayMode" runat="server">
                                                        </f:MenuCheckBox>
                                                        <f:MenuCheckBox Text="Large Space" ID="MenuDisplayModeLargeSpace" AttributeDataTag="largeSpace" GroupName="MenuDisplayMode" runat="server">
                                                        </f:MenuCheckBox>
                                                    </Items>
                                                    <Listeners>
                                                        <f:Listener Event="checkchange" Handler="onMenuDisplayModeCheckChange" />
                                                    </Listeners>
                                                </Menu>
                                            </f:MenuButton>
                                            <f:MenuButton EnablePostBack="false" Text="Menu Style" ID="MenuStyle" runat="server">
                                                <Menu runat="server">
                                                    <Items>
                                                        <f:MenuCheckBox Text="Smart Tree Menu" ID="MenuStyleTree" AttributeDataTag="tree" Checked="true" GroupName="MenuStyle" runat="server">
                                                        </f:MenuCheckBox>
                                                        <f:MenuCheckBox Text="Tree Menu" ID="MenuStylePlainTree" AttributeDataTag="plaintree" GroupName="MenuStyle" runat="server">
                                                        </f:MenuCheckBox>
                                                    </Items>
                                                    <Listeners>
                                                        <f:Listener Event="checkchange" Handler="onMenuStyleCheckChange" />
                                                    </Listeners>
                                                </Menu>
                                            </f:MenuButton>
											<f:MenuButton EnablePostBack="false" Text="Main Tabs" ID="MenuMainTabs" runat="server">
                                                <Menu runat="server">
                                                    <Items>
                                                        <f:MenuCheckBox Text="Multi" ID="MenuMainTabsMulti" AttributeDataTag="multi" Checked="true" GroupName="MenuMainTabs" runat="server">
                                                        </f:MenuCheckBox>
                                                        <f:MenuCheckBox Text="Single" ID="MenuMainTabsSingle" AttributeDataTag="single" GroupName="MenuMainTabs" runat="server">
                                                        </f:MenuCheckBox>
                                                    </Items>
                                                    <Listeners>
                                                        <f:Listener Event="checkchange" Handler="onMenuMainTabsCheckChange" />
                                                    </Listeners>
                                                </Menu>
                                            </f:MenuButton>
                                            <f:MenuButton EnablePostBack="false" Text="Language" ID="MenuLang" runat="server">
                                                <Menu ID="Menu2" runat="server">
                                                    <Items>
                                                        <f:MenuCheckBox Text="English" ID="MenuLangEN" AttributeDataTag="en" Checked="true" GroupName="MenuLang" runat="server">
                                                        </f:MenuCheckBox>
                                                        <f:MenuCheckBox Text="简体中文" ID="MenuLangZHCN" AttributeDataTag="zh_CN" GroupName="MenuLang" runat="server">
                                                        </f:MenuCheckBox>
                                                        <f:MenuCheckBox Text="繁體中文" ID="MenuLangZHTW" AttributeDataTag="zh_TW" GroupName="MenuLang" runat="server">
                                                        </f:MenuCheckBox>
                                                        <f:MenuCheckBox Text="ئۇيغۇر تىلى" ID="MenuLangZHUEY" AttributeDataTag="zh_UEY" GroupName="MenuLang" runat="server">
                                                        </f:MenuCheckBox>
                                                    </Items>
                                                    <Listeners>
                                                        <f:Listener Event="checkchange" Handler="onMenuLangCheckChange" />
                                                    </Listeners>
                                                </Menu>
                                            </f:MenuButton>
                                            <f:MenuButton runat="server" Text="Loading" EnablePostBack="false">
                                                <Listeners>
                                                    <f:Listener Event="click" Handler="onLoadingSelectClick" />
                                                </Listeners>
                                            </f:MenuButton>
                                            <f:MenuSeparator runat="server">
                                            </f:MenuSeparator>
                                            <f:MenuHyperLink runat="server" Text="FineUI MVC Examples" NavigateUrl="http://mvc.fineui.us/" Target="_blank">
                                            </f:MenuHyperLink>
                                            <f:MenuHyperLink runat="server" Text="FineUI Core Examples" NavigateUrl="http://core.fineui.us/" Target="_blank">
                                            </f:MenuHyperLink>
                                            <f:MenuHyperLink runat="server" Text="FineUI JS Examples" NavigateUrl="http://js.fineui.us/" Target="_blank">
                                            </f:MenuHyperLink>
                                            <f:MenuSeparator runat="server">
                                            </f:MenuSeparator>
                                            <f:MenuText runat="server" ID="menuTextCopyright" HideOnClick="false" CssClass="copyright-menutext">
                                            </f:MenuText>
                                        </Menu>
                                    </f:Button>
                                </div>
                            </div>
                        </f:ContentPanel>
                        <f:TabStrip ID="mainTabStrip" CssClass="centerregion" ShowInkBar="true" InkBarPosition="Top" BoxFlex="1" ShowBorder="true" EnableTabCloseMenu="true" runat="server">
                            <Tabs>
                                <f:Tab ID="tabHomepage" Title="Home" IconFont="_Home" EnableIFrame="true" IFrameUrl="~/Common/main.aspx" runat="server">
                                </f:Tab>
                            </Tabs>
                            <Tools>
                                <f:Tool runat="server" EnablePostBack="false" IconFont="_Code" CssClass="tabtool viewcode" ToolTip="View Source Code" ID="toolSourceCode">
                                    <Listeners>
                                        <f:Listener Event="click" Handler="onToolSourceCodeClick" />
                                    </Listeners>
                                </f:Tool>
                                <f:Tool runat="server" EnablePostBack="false" IconFont="_Refresh" CssClass="tabtool" ToolTip="Refresh Page" ID="toolRefresh">
                                    <Listeners>
                                        <f:Listener Event="click" Handler="onToolRefreshClick" />
                                    </Listeners>
                                </f:Tool>
                                <f:Tool runat="server" EnablePostBack="false" IconFont="_NewTab" CssClass="tabtool" ToolTip="Open in New Tab" ID="toolNewWindow">
                                    <Listeners>
                                        <f:Listener Event="click" Handler="onToolNewWindowClick" />
                                    </Listeners>
                                </f:Tool>
                            </Tools>
                        </f:TabStrip>
                    </Items>
                </f:Panel>
            </Items>
        </f:Panel>
        <f:Window ID="windowSourceCode" IconFont="_Code" Title="Source Code" Hidden="true" EnableIFrame="true"
            runat="server" IsModal="true" Width="1000px" Height="600px" EnableClose="true"
            EnableMaximize="true" EnableResize="true">
        </f:Window>
        <f:Window ID="windowThemeRoller" Title="Themes" Hidden="true" EnableIFrame="true" IFrameUrl="./Common/themes.aspx" ClearIFrameAfterClose="false"
            runat="server" IsModal="true" Width="1020px" Height="600px" EnableClose="true"
            EnableMaximize="true" EnableResize="true">
        </f:Window>
        <f:Window ID="windowLoadingSelector" Title="Loading" Hidden="true" EnableIFrame="true" IFrameUrl="./Common/loading.aspx" ClearIFrameAfterClose="false"
            runat="server" IsModal="true" Width="1000px" Height="600px" EnableClose="true"
            EnableMaximize="true" EnableResize="true">
        </f:Window>

        <asp:XmlDataSource ID="XmlDataSource1" runat="server" EnableCaching="false" DataFile="~/res/menu.xml"></asp:XmlDataSource>

        <f:HiddenField runat="server" ID="hfExamplesCount"></f:HiddenField>
    </form>

    <script>
        var PARAMS = {
            mainPanel: '<%= mainPanel.ClientID %>',
            mainTabStrip: '<%= mainTabStrip.ClientID %>',
            treeMenu: '<%= treeMenu.ClientID %>',
            sidebarRegion: '<%= sidebarRegion.ClientID %>',
            btnCollapseSidebar: '<%= btnCollapseSidebar.ClientID %>',
            tabHomepage: '<%= tabHomepage.ClientID %>',
            windowSourceCode: '<%= windowSourceCode.ClientID %>',
            windowThemeRoller: '<%= windowThemeRoller.ClientID %>',
            windowLoadingSelector: '<%= windowLoadingSelector.ClientID %>',
            hfExamplesCount: '<%= hfExamplesCount.ClientID %>',
            sourceUrl: '<%= ResolveUrl("~/Common/source.aspx") %>',
            dashboardUrl: '<%= ResolveUrl("~/block/dashboard.aspx") %>',
            mainUrl: '<%= ResolveUrl("~/Common/main.aspx") %>',
            processNewWindowUrl: function (url) {
                return url.replace(/\/mobile\/\?file=/ig, '/mobile/');
            }
        };
    </script>
    
    <asp:Literal ID="litScriptIndex" runat="server"></asp:Literal>

</body>
</html>
