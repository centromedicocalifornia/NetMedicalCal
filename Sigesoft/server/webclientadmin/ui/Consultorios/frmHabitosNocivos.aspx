﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmHabitosNocivos.aspx.cs" Inherits="Sigesoft.Server.WebClientAdmin.UI.Consultorios.frmHabitosNocivos" %>
<%@ Register assembly="FineUI" namespace="FineUI" tagprefix="x" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <x:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <x:Panel ID="Panel1" runat="server" Layout="Fit" ShowBorder="False" ShowHeader="false" BodyPadding="5px" EnableBackgroundColor="true">
            <Toolbars>
                <x:Toolbar ID="Toolbar1" runat="server">
                    <Items>
                        <x:Button ID="btnSaveRefresh" Text="Guardar y Cerrar" runat="server" Icon="SystemSave" OnClick="btnSaveRefresh_Click" ValidateForms="SimpleForm1" TabIndex="9">
                        </x:Button>
                        <x:Button ID="btnClose" EnablePostBack="false" Text="Cancelar y Cerrar" runat="server" Icon="SystemClose" TabIndex="10">
                        </x:Button>
                    </Items>
                </x:Toolbar>
            </Toolbars>
            <Items>
            <x:Panel ID="Panel2" Layout="Fit" runat="server" ShowBorder="false" ShowHeader="false" >
                <Items>
                    <x:SimpleForm ID="SimpleForm1" ShowBorder="false" ShowHeader="false" EnableBackgroundColor="true" 
                            AutoScroll="true" BodyPadding="5px" runat="server" EnableCollapse="True">         
                        <Items>
                            <x:DropDownList ID="ddlTipoHabito"  runat="server" Label="Hábito" TabIndex="1" EnableSimulateTree="true" ShowRedStar="true" CompareType="String" CompareValue="-1"
                                CompareOperator="NotEqual" CompareMessage="Campo requerido" OnSelectedIndexChanged="ddlTipoHabito_SelectedIndexChanged" AutoPostBack="true" ></x:DropDownList>
                            <x:DropDownList ID="ddlFrecuencia"  runat="server" Label="Frecuencia" TabIndex="1" EnableSimulateTree="true" ShowRedStar="true" CompareType="String" CompareValue="-1"
                                CompareOperator="NotEqual" CompareMessage="Campo requerido" ></x:DropDownList>                                
                            <x:TextArea ID="txtComentario" Label="Comentario" runat="server" TabIndex="5" Height="55"/>                   
                        </Items>                                   
                    </x:SimpleForm>
                </Items>
            </x:Panel>            
       </Items>
        </x:Panel>
    </form>
</body>
</html>
