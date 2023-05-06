<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="window_form.aspx.cs"
    Inherits="FineUIPro.Examples.window.window_form" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server" />
        <f:Window ID="Window2" Width="650px" Height="300px" Icon="TagBlue" Title="Window"
            EnableMaximize="true"
            runat="server" EnableResize="true" BodyPadding="10px" Layout="Fit"
            IsModal="false">
            <Items>
                <f:Form BodyPadding="10px" ID="Form2" LabelWidth="100px" ShowBorder="false" ShowHeader="false"
                    runat="server" Title="Form 1">
                    <Rows>
                        <f:FormRow ColumnWidths="50% 50%">
                            <Items>
                                <f:Label ID="Label1" runat="server" Label="Label" Text="Label value">
                                </f:Label>
                                <f:CheckBox ID="CheckBox1" runat="server" Text="CheckBox" Label="CheckBox">
                                </f:CheckBox>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ColumnWidths="50% 50%">
                            <Items>
                                <f:DropDownList ID="DropDownList1" runat="server" Label="DropDownList" Required="true" ShowRedStar="true" AutoSelectFirstItem="false">
                                    <f:ListItem Text="Item 1" Value="Value1" />
                                    <f:ListItem Text="Item 2 (Unselectable)" Value="Value2" EnableSelect="false" />
                                    <f:ListItem Text="Item 3 (Unselectable)" Value="Value3" EnableSelect="false" />
                                    <f:ListItem Text="Item 4" Value="Value4" />
                                    <f:ListItem Text="Item 5" Value="Value5" />
                                    <f:ListItem Text="Item 6" Value="Value6" />
                                    <f:ListItem Text="Optional item 7" Value="Value7" />
                                    <f:ListItem Text="Optional item 8" Value="Value8" />
                                </f:DropDownList>
                                <f:TextBox ID="TextBox1" ShowRedStar="true" runat="server" Label="TextBox" Required="true"
                                    Text="">
                                </f:TextBox>
                            </Items>
                        </f:FormRow>
                    </Rows>
                </f:Form>
            </Items>
            <Toolbars>
                <f:Toolbar ID="Toolbar3" Position="Top" runat="server">
                    <Items>
                        <f:ToolbarText Text="Toolbar text 1" ID="ToolbarText3" runat="server">
                        </f:ToolbarText>
                        <f:ToolbarSeparator ID="ToolbarSeparator2" runat="server">
                        </f:ToolbarSeparator>
                        <f:ToolbarText Text="Toolbar text 2" ID="ToolbarText4" runat="server">
                        </f:ToolbarText>
                        <f:ToolbarSeparator ID="ToolbarSeparator1" runat="server">
                        </f:ToolbarSeparator>
                        <f:Button ID="Button1" runat="server" Text="Toolbar button" OnClick="Button1_Click">
                        </f:Button>
                    </Items>
                </f:Toolbar>
                <f:Toolbar runat="server" Position="Bottom" ToolbarAlign="Right">
                    <Items>
                        <f:Button runat="server" Text="Validate this form and submit" ValidateForms="Form2"
                            ID="btnSubmitForm1" OnClick="btnSubmitForm1_Click">
                        </f:Button>
                        <f:Button ID="btnClose" runat="server" OnClick="btnClose_Click" Text="Hide window">
                        </f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>
        </f:Window>
    </form>
</body>
</html>
