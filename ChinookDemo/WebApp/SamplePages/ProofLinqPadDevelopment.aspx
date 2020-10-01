<%@ Page Title="Proof LinqPad Development" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProofLinqPadDevelopment.aspx.cs" Inherits="WebApp.SamplePages.ProofLinqPadDevelopment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Proof LinqPad Development</h1>
    <div class="row">
        <asp:Label ID="Label1" runat="server" Text="Select an artist"></asp:Label>&nbsp;&nbsp;
        <asp:DropDownList ID="ArtistList" runat="server" 
            DataSourceID="ArtistListODS" 
            DataTextField="DisplayText" 
            DataValueField="DisplayText"
             AppendDataBoundItems="true">
            <asp:ListItem Value="" Text="select ...."></asp:ListItem>
        </asp:DropDownList>&nbsp;&nbsp;
        <asp:LinkButton ID="Search" runat="server">Search</asp:LinkButton>
    </div>
    <div class="row">
        <asp:GridView ID="SongList" runat="server" 
            AutoGenerateColumns="False" 
            DataSourceID="SongListODS" 
            AllowPaging="True"
             CssClass="table table-striped" GridLines="Horizontal" BorderStyle="None">

            <Columns>
                <asp:BoundField DataField="Song" HeaderText="Song" SortExpression="Song"></asp:BoundField>
                <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title"></asp:BoundField>
                <asp:BoundField DataField="Year" HeaderText="Year" SortExpression="Year"></asp:BoundField>
                <asp:BoundField DataField="Length" HeaderText="Length" SortExpression="Length"></asp:BoundField>
                <asp:BoundField DataField="Price" HeaderText="Price" SortExpression="Price"></asp:BoundField>
                <asp:BoundField DataField="Genre" HeaderText="Genre" SortExpression="Genre"></asp:BoundField>
            </Columns>
            <EmptyDataTemplate>
                there is no data available for search
            </EmptyDataTemplate>
        </asp:GridView>
    </div>
    <asp:ObjectDataSource ID="ArtistListODS" runat="server" 
        OldValuesParameterFormatString="original_{0}" 
        SelectMethod="Artists_List" 
        TypeName="ChinookSystem.BLL.ArtistController">

    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="SongListODS" runat="server" 
        OldValuesParameterFormatString="original_{0}" 
        SelectMethod="BLL_Query" 
        TypeName="ChinookSystem.BLL.TrackController">

        <SelectParameters>
            <asp:ControlParameter ControlID="ArtistList" 
                PropertyName="SelectedValue" 
                Name="artist" Type="String" DefaultValue=""></asp:ControlParameter>
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
