<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="window_multi.aspx.cs" Inherits="Sigesoft.Server.WebClientAdmin.UI.window.window_multi" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server" />
        <table>
            <tr>
                <td>
                    <f:DropDownList runat="server" ID="DropDownList1">
                        <f:ListItem Text="Window 1" Value="1" />
                        <f:ListItem Text="Window 2" Value="2" />
                        <f:ListItem Text="Window 3" Value="3" />
                    </f:DropDownList>
                </td>
                <td>
                    <f:Button ID="btnShowWindow" Text="Show window" runat="server" OnClick="btnShowWindow_Click">
                    </f:Button>
                </td>
            </tr>
        </table>



        <br />
        <br />
        <f:Window ID="Window1" Width="650px" Height="300px" Icon="TagBlue" Title="Window 1" Hidden="true"
            EnableMaximize="true" runat="server" EnableResize="true"
            IsModal="false" AutoScroll="true" BodyPadding="10px" KeepLastPosition="true">
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
                                <f:DropDownList ID="DropDownList2" runat="server" Label="DropDownList" Required="true" ShowRedStar="true" AutoSelectFirstItem="false">
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
        </f:Window>
        <f:Window ID="Window2" Width="650px" Height="300px" Icon="TagBlue" Title="Window 2" Hidden="true"
            EnableMaximize="true" runat="server" EnableResize="true"
            IsModal="false" AutoScroll="true" BodyPadding="10px" KeepLastPosition="true">
            <Items>
                <f:Form BodyPadding="10px" ID="Form3" LabelWidth="100px" ShowBorder="false" ShowHeader="false"
                    runat="server" Title="Form 2">
                    <Rows>
                        <f:FormRow ColumnWidths="50% 50%">
                            <Items>
                                <f:Label ID="Label2" runat="server" Label="Label" Text="Label value">
                                </f:Label>
                                <f:CheckBox ID="CheckBox2" runat="server" Text="CheckBox" Label="CheckBox">
                                </f:CheckBox>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ColumnWidths="50% 50%">
                            <Items>
                                <f:DropDownList ID="DropDownList3" runat="server" Label="DropDownList" Required="true" ShowRedStar="true" AutoSelectFirstItem="false">
                                    <f:ListItem Text="Item 1" Value="Value1" />
                                    <f:ListItem Text="Item 2 (Unselectable)" Value="Value2" EnableSelect="false" />
                                    <f:ListItem Text="Item 3 (Unselectable)" Value="Value3" EnableSelect="false" />
                                    <f:ListItem Text="Item 4" Value="Value4" />
                                    <f:ListItem Text="Item 5" Value="Value5" />
                                    <f:ListItem Text="Item 6" Value="Value6" />
                                    <f:ListItem Text="Optional item 7" Value="Value7" />
                                    <f:ListItem Text="Optional item 8" Value="Value8" />
                                </f:DropDownList>
                                <f:TextBox ID="TextBox2" ShowRedStar="true" runat="server" Label="TextBox" Required="true"
                                    Text="">
                                </f:TextBox>
                            </Items>
                        </f:FormRow>
                    </Rows>
                </f:Form>
            </Items>
        </f:Window>
        <f:Window ID="Window3" Width="650px" Height="300px" Icon="TagBlue" Title="Window 3" 
            EnableMaximize="true" runat="server" EnableResize="true"
            IsModal="false" AutoScroll="true" BodyPadding="10px" KeepLastPosition="true">
            <Items>
                <f:Form BodyPadding="10px" ID="Form4" LabelWidth="100px" ShowBorder="false" ShowHeader="false"
                    runat="server" Title="Form 3">
                    <Rows>
                        <f:FormRow ColumnWidths="50% 50%">
                            <Items>
                                <f:Label ID="Label3" runat="server" Label="Label" Text="Label value">
                                </f:Label>
                                <f:CheckBox ID="CheckBox3" runat="server" Text="CheckBox" Label="CheckBox">
                                </f:CheckBox>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ColumnWidths="50% 50%">
                            <Items>
                                <f:DropDownList ID="DropDownList4" runat="server" Label="DropDownList" Required="true" ShowRedStar="true" AutoSelectFirstItem="false">
                                    <f:ListItem Text="Item 1" Value="Value1" />
                                    <f:ListItem Text="Item 2 (Unselectable)" Value="Value2" EnableSelect="false" />
                                    <f:ListItem Text="Item 3 (Unselectable)" Value="Value3" EnableSelect="false" />
                                    <f:ListItem Text="Item 4" Value="Value4" />
                                    <f:ListItem Text="Item 5" Value="Value5" />
                                    <f:ListItem Text="Item 6" Value="Value6" />
                                    <f:ListItem Text="Optional item 7" Value="Value7" />
                                    <f:ListItem Text="Optional item 8" Value="Value8" />
                                </f:DropDownList>
                                <f:TextBox ID="TextBox3" ShowRedStar="true" runat="server" Label="TextBox" Required="true"
                                    Text="">
                                </f:TextBox>
                            </Items>
                        </f:FormRow>
                    </Rows>
                </f:Form>
            </Items>
        </f:Window>
    </form>
</body>
</html>
