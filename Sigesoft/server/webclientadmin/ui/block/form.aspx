<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="form.aspx.cs" Inherits="Sigesoft.Server.WebClientAdmin.UI.block.form" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title></title>
    <style>
        body.f-body {
            overflow-x: hidden;
        }

        .myblockform .f-panel-body .f-field {
            margin: 0;
        }
    </style>
</head>
<body class="f-body-bgcolor">
    <form id="_form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server" />
        <f:Panel IsFluid="true" runat="server"
            Layout="Block" BlockConfigSpace="20px" ShowBorder="false" ShowHeader="false">
            <Items>
                <f:Panel BlockMD="6"
                    Layout="Block" BlockConfigSpace="20px"
                    runat="server" ShowBorder="false" ShowHeader="false">
                    <Items>
                        <f:Panel ID="Panel1" CssClass="blockpanel myblockform" BlockMD="12" EnableCollapse="true"
                            Layout="Block" BlockConfigSpace="20px" BodyPadding="10px"
                            runat="server" ShowBorder="true" ShowHeader="true" Title="Form 1">
                            <Items>
                                <f:Label ID="Label1" BlockMD="6" runat="server" Label="Label" Text="Label value">
                                </f:Label>
                                <f:CheckBox ID="CheckBox1" BlockMD="6" runat="server" Text="CheckBox" Label="CheckBox">
                                </f:CheckBox>
                                <f:DropDownList ID="DropDownList1" BlockMD="6" runat="server" Required="true" ShowRedStar="true" AutoSelectFirstItem="false" Label="DropDownList">
                                    <f:ListItem Text="Item 1" Value="0"></f:ListItem>
                                    <f:ListItem Text="Item 2" Value="2"></f:ListItem>
                                    <f:ListItem Text="Item 3" Value="3"></f:ListItem>
                                    <f:ListItem Text="Item 4" Value="4"></f:ListItem>
                                    <f:ListItem Text="Item 5" Value="5"></f:ListItem>
                                    <f:ListItem Text="Item 6" Value="6"></f:ListItem>
                                    <f:ListItem Text="Item 7" Value="7"></f:ListItem>
                                    <f:ListItem Text="Item 8" Value="8"></f:ListItem>
                                </f:DropDownList>
                                <f:TextBox ID="TextBox1" BlockMD="6" ShowRedStar="true" runat="server" Label="TextBox" Required="true"
                                    Text="">
                                </f:TextBox>
                                <f:Button runat="server" BlockMD="12" Text="Validate this form and submit" ValidateForms="Panel1"
                                    ID="btnSubmitForm1" OnClick="btnSubmitForm1_Click">
                                </f:Button>
                            </Items>
                        </f:Panel>
                        <f:Panel ID="Panel2" CssClass="blockpanel myblockform" BlockMD="12" EnableCollapse="true"
                            Layout="Block" BlockConfigSpace="20px" BodyPadding="10px"
                            runat="server" ShowBorder="true" ShowHeader="true" Title="Form 2">
                            <Items>
                                <f:DatePicker runat="server" BlockMD="6" EnableEdit="false" Required="true" Label="Date" EmptyText="Please select a date"
                                    ID="DatePicker1" SelectedDate="2014-07-10" ShowRedStar="true">
                                </f:DatePicker>
                                <f:TimePicker ID="TimePicker1" BlockMD="6" EnableEdit="false" ShowRedStar="true" Label="Time" Increment="30"
                                    Required="true" Text="08:30" EmptyText="Please select a time" runat="server">
                                </f:TimePicker>
                                <f:CheckBox runat="server" BlockMD="12" ID="cbxAtSchool" Label="At school"></f:CheckBox>
                                <f:CheckBoxList ID="CheckBoxList1" BlockMD="12" Label="CheckBoxList" ColumnNumber="3" runat="server">
                                    <f:CheckItem Text="Item 1" Value="value1" />
                                    <f:CheckItem Text="Item 2" Value="value2" Selected="true" />
                                    <f:CheckItem Text="Item 3" Value="value3" Selected="true" />
                                    <f:CheckItem Text="Item 4" Value="value4" Selected="true" />
                                    <f:CheckItem Text="Item 5" Value="value5" Selected="true" />
                                </f:CheckBoxList>
                                <f:RadioButtonList ID="RadioButtonList2" BlockMD="12" Label="RadioButtonList" Required="true" ColumnNumber="3" runat="server">
                                    <f:RadioItem Text="Item 1" Value="value1" />
                                    <f:RadioItem Text="Item 2" Value="value2" />
                                    <f:RadioItem Text="Item 3" Value="value3" />
                                    <f:RadioItem Text="Item 4" Value="value4" />
                                    <f:RadioItem Text="Item 5" Value="value5" />
                                </f:RadioButtonList>
                            </Items>
                            <Toolbars>
                                <f:Toolbar runat="server" Position="Bottom" ToolbarAlign="Center">
                                    <Items>
                                        <f:Button ID="btnSubmitForm2" Text="Validate this form and submit" runat="server" OnClick="btnSubmitForm2_Click"
                                            ValidateForms="Panel2">
                                        </f:Button>
                                    </Items>
                                </f:Toolbar>
                            </Toolbars>
                        </f:Panel>
                    </Items>
                </f:Panel>
                <f:Panel ID="Panel3" CssClass="blockpanel myblockform" BlockMD="6" EnableCollapse="true"
                    Layout="Block" BlockConfigSpace="20px" BodyPadding="10px"
                    runat="server" ShowBorder="true" ShowHeader="true" Title="Form 3">
                    <Items>
                        <f:Label ID="Label3" BlockMD="6" Label="Phone" Text="0551-1234567" runat="server" />
                        <f:Label ID="Label16" BlockMD="6" runat="server" Label="Applicant" Text="admin">
                        </f:Label>
                        <f:Label ID="Label4" BlockMD="6" Label="Number" Text="200804170006" runat="server" />
                        <f:TextBox ID="TextBox2" BlockMD="6" Required="true" ShowRedStar="true" Label="Email" RegexPattern="EMAIL"
                            RegexMessage="Please enter a valid email address!" runat="server">
                        </f:TextBox>
                        <f:DropDownList ID="DropDownList3" BlockMD="12" Label="Approver" runat="server" Required="true" ShowRedStar="true"
                            EmptyText="Please select approver" AutoSelectFirstItem="false">
                            <f:ListItem Text="Boss A" Value="0"></f:ListItem>
                            <f:ListItem Text="Boss B" Value="1"></f:ListItem>
                            <f:ListItem Text="Boss C" Value="2"></f:ListItem>
                            <f:ListItem Text="Boss D" Value="3"></f:ListItem>
                            <f:ListItem Text="Boss E" Value="4"></f:ListItem>
                            <f:ListItem Text="Boss F" Value="5"></f:ListItem>
                        </f:DropDownList>
                        <f:NumberBox ID="NumberBox1" BlockMD="12" Label="Apply number" NoDecimal="true" NoNegative="true" MaxValue="1000" Required="true" runat="server"
                            ShowRedStar="true" />
                        <f:TextArea ID="TextArea1" BlockMD="12" runat="server" Label="Description" ShowRedStar="true" Required="true">
                        </f:TextArea>
                        <f:Panel BlockMD="12" runat="server" ShowBorder="false" ShowHeader="false">
                            <Items>
                                <f:Button ID="btnSubmitForm3" Text="Validate this form and submit" runat="server" OnClick="btnSubmitForm3_Click"
                                    ValidateForms="Panel3">
                                </f:Button>
                            </Items>
                        </f:Panel>
                    </Items>
                </f:Panel>
            </Items>
        </f:Panel>
    </form>
</body>
</html>
