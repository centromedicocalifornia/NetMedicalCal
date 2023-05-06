<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="dashboard.aspx.cs" Inherits="Sigesoft.Server.WebClientAdmin.UI.block.dashboard" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title></title>
    <meta name="sourcefiles" content="~/res/css/dashboard.css;~/res/js/dashboard_chart.js" />
    <link href="../res/css/dashboard.css" rel="stylesheet" />
</head>
<body class="f-body-bgcolor">
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server" />
        <f:Panel IsFluid="true" runat="server"
            Layout="Block" BlockConfigSpace="20px" ShowBorder="false" ShowHeader="false">
            <Items>
                <f:ContentPanel CssClass="blockpanel" BlockMD="6" BlockLG="3"
                    runat="server" ShowBorder="true" ShowHeader="false">
                    <div class="mycard">
                        <div class="title">
                            Number of views today
                            <i class="f-icon f-iconfont f-iconfont-info infoicon"></i>
                        </div>
                        <div class="number">
                            12,000
                        </div>
                        <div class="chart chart1">
                        </div>
                        <div class="footer f-widget-content">
                            Average number of views 15,000
                        </div>
                    </div>
                    <div class="mycard" style="display: none;">
                        <div class="title">
                            Number of views today
                            <i class="f-icon f-iconfont f-iconfont-info infoicon"></i>
                        </div>
                        <div class="desc">
                            A detailed description of the number of views today.
                            <br />
                            FineUI is a professional ASP.NET WebForms/MVC/Core control library based on jQuery.
                        </div>
                    </div>
                </f:ContentPanel>
                <f:ContentPanel CssClass="blockpanel" BlockMD="6" BlockLG="3"
                    runat="server" ShowBorder="true" ShowHeader="false">
                    <div class="mycard">
                        <div class="title">
                            Number of independent IP today
                            <i class="f-icon f-iconfont f-iconfont-info infoicon"></i>
                        </div>
                        <div class="number">
                            800
                        </div>
                        <div class="chart chart2">
                        </div>
                        <div class="footer f-widget-content">
                            Average independent IP 900
                        </div>
                    </div>
                    <div class="mycard" style="display: none;">
                        <div class="title">
                            Number of independent IP today
                            <i class="f-icon f-iconfont f-iconfont-info infoicon"></i>
                        </div>
                        <div class="desc">
                            A detailed description of today's independent IP.
                            <br />
                            FineUI is a professional ASP.NET WebForms/MVC/Core control library based on jQuery.
                        </div>
                    </div>
                </f:ContentPanel>
                <f:ContentPanel CssClass="blockpanel" BlockMD="6" BlockLG="3"
                    runat="server" ShowBorder="true" ShowHeader="false">
                    <div class="mycard">
                        <div class="title">
                            Number of users today
                            <i class="f-icon f-iconfont f-iconfont-info infoicon"></i>
                        </div>
                        <div class="number">
                            900
                        </div>
                        <div class="chart chart3">
                        </div>
                        <div class="footer f-widget-content">
                            Weekly 10% <i class="f-icon f-iconfont f-iconfont-triangle-down"></i>
                            &nbsp;
                            Daily 20% <i class="f-icon f-iconfont f-iconfont-triangle-up"></i>
                        </div>
                    </div>
                    <div class="mycard" style="display: none;">
                        <div class="title">
                            Number of users today
                            <i class="f-icon f-iconfont f-iconfont-info infoicon"></i>
                        </div>
                        <div class="desc">
                            A detailed description of the number of users today.
                            <br />
                            FineUI is a professional ASP.NET WebForms/MVC/Core control library based on jQuery.
                        </div>
                    </div>
                </f:ContentPanel>
                <f:ContentPanel CssClass="blockpanel" BlockMD="6" BlockLG="3"
                    runat="server" ShowBorder="true" ShowHeader="false">
                    <div class="mycard rank">
                        <div class="title">
                            Today's ranking
                            <i class="f-icon f-iconfont f-iconfont-info infoicon"></i>
                        </div>
                        <div class="number">
                            <span class="number-subtext">Up</span> 9 <span class="number-subtext">places</span>
                        </div>
                        <div class="chart">
                            <div class="f-progressbar f-widget f-widget-content f-corner-all">
                                <div class="f-progressbar-value f-widget-header" style="width: 70%;">
                                </div>
                            </div>
                        </div>
                        <div class="footer f-widget-content">
                            Ranked top 32% <i class="f-icon f-iconfont f-iconfont-triangle-up"></i>
                        </div>
                    </div>
                    <div class="mycard" style="display: none;">
                        <div class="title">
                            Today's ranking
                            <i class="f-icon f-iconfont f-iconfont-info infoicon"></i>
                        </div>
                        <div class="desc">
                            A detailed description of today's ranking.
                            <br />
                            FineUI is a professional ASP.NET WebForms/MVC/Core control library based on jQuery.
                        </div>
                    </div>
                </f:ContentPanel>
                <f:TabStrip CssClass="blockpanel mytabstrip" BlockMD="12" ShowInkBar="true"
                    runat="server" ShowBorder="true" ActiveTabIndex="0">
                    <Tabs>
                        <f:Tab runat="server" Layout="Block" BlockConfigSpace="0" Title="Number of views">
                            <Items>
                                <f:ContentPanel BlockMD="6" BlockLG="8"
                                    runat="server" ShowBorder="false" ShowHeader="false">
                                    <div class="tabstrip-chart chart1">
                                    </div>
                                </f:ContentPanel>
                                <f:ContentPanel BlockMD="6" BlockLG="4" BodyPadding="20px"
                                    runat="server" ShowBorder="false" ShowHeader="false">
                                    <table class="mytable">
                                        <thead>
                                            <tr>
                                                <th></th>
                                                <th>Operator</th>
                                                <th>PV</th>
                                                <th>UV</th>
                                                <th>IP</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                <td><span class="mynumber round f-widget-header">1</span></td>
                                                <td>Telecom</td>
                                                <td>12,000</td>
                                                <td>9,000</td>
                                                <td>6,000</td>
                                            </tr>
                                            <tr>
                                                <td><span class="mynumber round f-widget-header">2</span></td>
                                                <td>Unicom</td>
                                                <td>10,000</td>
                                                <td>8,000</td>
                                                <td>5,000</td>
                                            </tr>
                                            <tr>
                                                <td><span class="mynumber round f-widget-header">3</span></td>
                                                <td>Mobile</td>
                                                <td>9,000</td>
                                                <td>6,000</td>
                                                <td>4,000</td>
                                            </tr>
                                            <tr>
                                                <td><span class="mynumber">4</span></td>
                                                <td>Netcom</td>
                                                <td>6,000</td>
                                                <td>5,000</td>
                                                <td>3,000</td>
                                            </tr>
                                            <tr>
                                                <td><span class="mynumber">5</span></td>
                                                <td>Railcom</td>
                                                <td>4,000</td>
                                                <td>3,000</td>
                                                <td>2,000</td>
                                            </tr>
                                            <tr>
                                                <td><span class="mynumber">6</span></td>
                                                <td>Others</td>
                                                <td>8,000</td>
                                                <td>5,000</td>
                                                <td>2,000</td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </f:ContentPanel>
                            </Items>
                        </f:Tab>
                        <f:Tab runat="server" Layout="Block" BlockConfigSpace="0" Title="Number of users">
                            <Items>
                                <f:ContentPanel BlockMD="6" BlockLG="8"
                                    runat="server" ShowBorder="false" ShowHeader="false">
                                    <div class="tabstrip-chart chart2">
                                    </div>
                                </f:ContentPanel>
                                <f:ContentPanel BlockMD="6" BlockLG="4" BodyPadding="20px"
                                    runat="server" ShowBorder="false" ShowHeader="false">
                                    <table class="mytable">
                                        <thead>
                                            <tr>
                                                <th></th>
                                                <th>Browser</th>
                                                <th>PV</th>
                                                <th>UV</th>
                                                <th>IP</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                <td>1</td>
                                                <td>Chrome</td>
                                                <td>12,600</td>
                                                <td>9,600</td>
                                                <td>6,600</td>
                                            </tr>
                                            <tr>
                                                <td>2</td>
                                                <td>Firefox</td>
                                                <td>10,600</td>
                                                <td>8,600</td>
                                                <td>5,600</td>
                                            </tr>
                                            <tr>
                                                <td>3</td>
                                                <td>Safari</td>
                                                <td>9,600</td>
                                                <td>6,600</td>
                                                <td>4,600</td>
                                            </tr>
                                            <tr>
                                                <td>4</td>
                                                <td>IE</td>
                                                <td>6,600</td>
                                                <td>5,600</td>
                                                <td>3,600</td>
                                            </tr>
                                            <tr>
                                                <td>5</td>
                                                <td>Edge</td>
                                                <td>4,600</td>
                                                <td>3,600</td>
                                                <td>2,600</td>
                                            </tr>
                                            <tr>
                                                <td>6</td>
                                                <td>Others</td>
                                                <td>8,600</td>
                                                <td>5,600</td>
                                                <td>2,600</td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </f:ContentPanel>
                            </Items>
                        </f:Tab>
                    </Tabs>
                    <Tools>
                        <f:Tool runat="server" ToolTip="Open detailed data" IconFont="_More">
                            <Listeners>
                                <f:Listener Event="click" Handler="onTabstripToolClick" />
                            </Listeners>
                        </f:Tool>
                    </Tools>
                </f:TabStrip>
                <f:Panel BlockMD="6" BlockLG="8" Layout="Block" BlockConfigSpace="20px"
                    runat="server" ShowBorder="false" ShowHeader="false">
                    <Items>
                        <f:Grid ID="Grid1" CssClass="blockpanel mygrid" BlockMD="12"
                            ShowBorder="true" ShowHeader="true" Title="Grid example"
                            EnableCheckBoxSelect="true" EnableAlternateRowColor="false" EnableRowLines="false"
                            runat="server" DataIDField="Id" DataUrl="~/griddataurl/griddataurl_paging_database.ashx"
                            AllowPaging="true" IsDatabasePaging="true" PageSize="10">
                            <Columns>
                                <f:RowNumberField></f:RowNumberField>
                                <f:RenderField Width="140px" ColumnID="Name" DataField="Name" HeaderText="Name">
                                </f:RenderField>
                                <f:RenderField Width="100px" ColumnID="EntranceYear" DataField="EntranceYear" FieldType="Int"
                                    HeaderText="Entrance year">
                                </f:RenderField>
                                <f:RenderCheckField Width="100px" ColumnID="AtSchool" DataField="AtSchool" RenderAsStaticField="true" HeaderText="At school" />
                                <f:RenderField ColumnID="Major" DataField="Major" RendererFunction="renderMajor"
                                    ExpandUnusedSpace="true" MinWidth="150px" HeaderText="Major">
                                </f:RenderField>
                            </Columns>
                        </f:Grid>
                        <f:Form CssClass="blockpanel myform" BlockMD="12" Layout="Block" BlockConfigSpace="10px" BodyPadding="20px"
                            runat="server" ShowBorder="true" ShowHeader="true" Title="Form example" LabelAlign="Right">
                            <Items>
                                <f:Label ID="Label1" BlockMD="6" runat="server" Label="Label" Text="Label value">
                                </f:Label>
                                <f:CheckBox ID="CheckBox1" BlockMD="6" runat="server" Text="CheckBox" Label="CheckBox">
                                </f:CheckBox>
                                <f:DropDownList ID="DropDownList1" BlockMD="6" runat="server" Required="true" ShowRedStar="true" AutoSelectFirstItem="false" Label="DropDownList">
                                    <f:ListItem Text="Item 1" Selected="true" Value="0"></f:ListItem>
                                    <f:ListItem Text="Item 2" Value="2"></f:ListItem>
                                    <f:ListItem Text="Item 3" Value="3"></f:ListItem>
                                </f:DropDownList>
                                <f:TextBox ID="TextBox1" BlockMD="6" ShowRedStar="true" runat="server" Label="TextBox" Required="true"
                                    Text="">
                                </f:TextBox>
                                <f:DatePicker runat="server" BlockMD="6" EnableEdit="false" Required="true" Label="Date" EmptyText="Please select a date"
                                    ID="DatePicker1" SelectedDate="2014-07-10" ShowRedStar="true">
                                </f:DatePicker>
                                <f:TimePicker ID="TimePicker1" BlockMD="6" EnableEdit="false" ShowRedStar="true" Label="Time" Increment="30"
                                    Required="true" Text="08:30" EmptyText="Please select a time" runat="server">
                                </f:TimePicker>
                                <f:CheckBoxList ID="CheckBoxList1" BlockMD="12" Label="CheckBoxList" ColumnNumber="3" runat="server">
                                    <f:CheckItem Text="Item 1" Value="value1" />
                                    <f:CheckItem Text="Item 2" Value="value2" Selected="true" />
                                    <f:CheckItem Text="Item 3" Value="value3" Selected="true" />
                                    <f:CheckItem Text="Item 4" Value="value4" Selected="true" />
                                    <f:CheckItem Text="Item 5" Value="value5" Selected="true" />
                                </f:CheckBoxList>
                                <f:TextArea ID="TextArea1" BlockMD="12" runat="server" Label="Description" ShowRedStar="true" Required="true">
                                </f:TextArea>
                            </Items>
                        </f:Form>
                    </Items>
                </f:Panel>
                <f:Panel BlockMD="6" BlockLG="4" Layout="Block" BlockConfigSpace="20px"
                    runat="server" ShowBorder="false" ShowHeader="false">
                    <Items>
                        <f:ContentPanel CssClass="blockpanel" BlockMD="12" BodyPadding="20px 20px 0 20px"
                            runat="server" ShowBorder="true" ShowHeader="true" Title="Site statistics">
                            <ul class="mysitestats">
                            </ul>
                        </f:ContentPanel>
                        <f:ContentPanel CssClass="blockpanel" BlockMD="12" BodyPadding="20px"
                            runat="server" ShowBorder="true" ShowHeader="true" Title="Access source">
                            <div class="mychart-pie"></div>
                        </f:ContentPanel>
                    </Items>
                </f:Panel>
            </Items>
        </f:Panel>
        <div class="f-widget-content" id="emptycontent"></div>
    </form>
    <script src="../res/third-party/echarts/echarts.min.js"></script>
    <script src="../res/js/dashboard_chart.js"></script>
    <script>

        function onTabstripToolClick(event) {
            parent.addExampleTab && parent.addExampleTab({
                id: 'dashboard_iframe_grid',
                iframeUrl: F.baseUrl + 'iframe/grid_iframe.aspx',
                title: 'Grid and Form',
                refreshWhenExist: true
            });
        }

        function renderMajor(value) {
            return F.formatString('<a href="http://gsa.ustc.edu.cn/search?q={0}" target="_blank" data-qtip="{1}">{1}</a>', encodeURIComponent(value), value);
        }

        F.ready(function () {

            $('.mycard .infoicon').click(function () {
                var card1 = $(this).parents('.mycard');
                var card2 = card1.siblings('.mycard');
                // Flip animation
                F.flip(card1, card2);
            });


            // Site statistics
            var siteStats = [];
            function addToSiteStats(title, content) {
                siteStats.push('<li class="f-state-default"><div class="title">' + title + '</div><div class="content">' + content + '</div></li>');
            }
            var examplesCount = parent.getExamplesCount ? parent.getExamplesCount() : '---';
            var examplesCountTitle = 'Examples count';
            addToSiteStats(examplesCountTitle, examplesCount);

            var cookieSearchText = F.cookie('SearchText');
            if (cookieSearchText) {
                addToSiteStats('Search keywords', cookieSearchText);
            }

            addToSiteStats('Display Mode', F.displayMode);
            addToSiteStats('Language', F.language);
            addToSiteStats('Theme', F.theme);
            addToSiteStats('Version', F.version);
            $('.mysitestats').html(siteStats.join(''));

        });

    </script>
</body>
</html>
