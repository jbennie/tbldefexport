<%@ Control Language="C#" CodeBehind="ForeignKey.ascx.cs" Inherits="land.DataContext.ForeignKeyField" %>

<asp:HyperLink ID="HyperLink1" runat="server"
    Text="<%# GetDisplayString() %>"
    NavigateUrl="<%# GetNavigateUrl() %>"  />

