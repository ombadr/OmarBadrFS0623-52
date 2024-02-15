<%@ Page Title="" Language="C#" MasterPageFile="~/Template.Master" AutoEventWireup="true" CodeBehind="HomePage.aspx.cs" Inherits="Esercizio.HomePage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>HomePage
    </title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5">
        <div class="row">
            <div class="col">
                <h2>Scegli la tua auto</h2>
                <asp:DropDownList ID="ddlAuto" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlAuto_SelectedIndexChanged"></asp:DropDownList>
                <asp:Image ID="imgAuto" runat="server" CssClass="img-fluid mt-3" />
                <asp:Label ID="lblPrezzoDiPartenza" runat="server" CssClass="form-label mt-3"></asp:Label>
            </div>
            <div class="col">
                <h2>Optional</h2>
                <asp:CheckBoxList ID="cblOptional" runat="server" CssClass="form-check"></asp:CheckBoxList>
                <h3>Garanzia</h3>
                <asp:TextBox ID="txtAnniGaranzia" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:Button runat="server" Text="Calcola Preventivo" CssClass="btn btn-primary mt-3" OnClick="btnCalcola_Click"></asp:Button>
<%--                <asp:Button ID="btnCalcola" runat="server" Text="Calcola Preventivo" CssClass="btn btn-primary mt-3" OnClick="btnCalcola_Click" />--%>
                <h2>Totale preventivo</h2>
                
                <asp:Label ID="lblTotalePrezzoDiPartenza" runat="server" CssClass="form-label"></asp:Label>
                <asp:Label ID="lblTotaleOptionals" runat="server" CssClass="form-label"></asp:Label>
                <asp:Label ID="lblTotaleGaranzia" runat="server" CssClass="form-label"></asp:Label>
                <asp:Label ID="lblTotalePrezzo" runat="server" CssClass="form-label"></asp:Label>
            </div>
        </div>
    </div>
</asp:Content>
