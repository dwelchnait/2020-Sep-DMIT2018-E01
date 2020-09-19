<%@ Page Title="Filter Search" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SearchByDDL.aspx.cs" Inherits="WebApp.SamplePages.SearchByDDL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Search Albums by Artist</h1>
    <div class="row">
        <asp:Label ID="Label1" runat="server" 
            Text="Select an artist"></asp:Label>&nbsp;&nbsp;
        <asp:DropDownList ID="ArtistList" runat="server"></asp:DropDownList>&nbsp;&nbsp;
        <asp:LinkButton ID="SearchAlbums" runat="server" OnClick="SearchAlbums_Click">Search for albums</asp:LinkButton>
    </div>
    <br /><br />
    <div class="row">
        <asp:Label ID="MessageLabel" runat="server"></asp:Label>
    </div>
     <br /><br />
    <asp:GridView ID="AlbumArtistList" runat="server" 
        AutoGenerateColumns="False"
         CssClass="table table-striped"
         GridLines="Horizontal" BorderStyle="None">

        <Columns>
            <asp:TemplateField HeaderText="ID">
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" 
                        Text='<%# Eval("AlbumId") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Right"></ItemStyle>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Title">
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" 
                        Text='<%# Eval("Title") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Left"></ItemStyle>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Artist">
                <ItemTemplate>
                    <asp:Label ID="Label4" runat="server" 
                        Text='<%# Eval("ArtistId") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Right"></ItemStyle>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Year">
                <ItemTemplate>
                    <asp:Label ID="Label5" runat="server" 
                        Text='<%# Eval("ReleaseYear") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Label">
                <ItemTemplate>
                    <asp:Label ID="Label6" runat="server" 
                        Text='<%# Eval("ReleaseLabel") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Left"></ItemStyle>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
