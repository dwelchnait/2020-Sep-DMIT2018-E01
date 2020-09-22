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
         GridLines="Horizontal" BorderStyle="None" 
        OnSelectedIndexChanged="AlbumArtistList_SelectedIndexChanged">

        <Columns>
            <asp:CommandField SelectText="View" ShowSelectButton="True"></asp:CommandField>
            <asp:TemplateField Visible="False">
                <ItemTemplate>
                    <asp:Label ID="AlbumId" runat="server" 
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
                
                    <asp:DropDownList ID="ArtistListGV" runat="server" 
                        DataSourceID="ArtistListGVODS" 
                        DataTextField="DisplayText" 
                        DataValueField="ValueId"
                         selectedvalue='<%# Eval("ArtistId") %>'
                         Enabled="false"
                         Width="350px">
                    </asp:DropDownList>
                </ItemTemplate>
                <ItemStyle ></ItemStyle>
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
    <asp:ObjectDataSource ID="ArtistListGVODS" runat="server" 
        OldValuesParameterFormatString="original_{0}" 
        SelectMethod="Artists_List" 
        TypeName="ChinookSystem.BLL.ArtistController">
    </asp:ObjectDataSource>
</asp:Content>
