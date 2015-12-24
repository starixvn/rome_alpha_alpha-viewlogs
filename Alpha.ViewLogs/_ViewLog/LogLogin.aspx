<%@ Page Title="" Language="C#" MasterPageFile="~/_ViewLog/ViewLog.Master" AutoEventWireup="true" CodeBehind="LogLogin.aspx.cs" Inherits="Alpha.ViewLogs._ViewLog.LogLogin" %>

<%@ Import Namespace="Sunplus.Agent.UI._ViewLog" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentData" runat="Server">
    <div id="content">
        <table width="100%" cellpadding="0" cellspacing="0" id="DataLog">
            <tr class="title">
                <th>
                    <%= Alpha.ViewLogs.LocalResources.Viewlog.logid%>
                </th>
                <th>
                    <%= Alpha.ViewLogs.LocalResources.Viewlog.logdate%>
                </th>
                <th>
                    <%= Alpha.ViewLogs.LocalResources.Viewlog.username%>
                </th>
                <th>
                    <%= Alpha.ViewLogs.LocalResources.Viewlog.actionname%>
                </th>
                <th>
                    <%= Alpha.ViewLogs.LocalResources.Viewlog.actionDesc%>
                </th>
                <th>
                    <%= Alpha.ViewLogs.LocalResources.Viewlog.Domain%>
                </th>
                <th>
                    <%= Alpha.ViewLogs.LocalResources.Viewlog.Browser%>
                </th>
                <th>
                    <%= Alpha.ViewLogs.LocalResources.Viewlog.country%>
                </th>
                <th>
                    <%= Alpha.ViewLogs.LocalResources.Viewlog.IP%>
                </th>
            </tr>
            <asp:Repeater ID="rptDataLog" runat="server" EnableViewState="false">
                <ItemTemplate>
                    <tr class="even">
                        <td>
                            <%#Eval("LogID")%>
                        </td>
                        <td class="datetimeInlinde">
                            <%#Eval("LogDate", "{0:d} <br/> {0:hh:mm:ss tt}")%>
                        </td>
                        <td class="EditedBy">
                            <%#Eval("UserName")%>
                        </td>
                        <td class="ActionName">
                            <%#Eval("ActionName")%>
                        </td>
                        <td class="actionDesc">
                            <%#Eval("Doing")%>
                        </td>
                        <td>
                            <%#Eval("Domain")%>
                        </td>
                        <td class="Browser">
                            <%#Eval("Browser")%>
                        </td>
                        <td>
                            <%#XsltLogFormat.FormatCountryCode(Eval("IP") as string, Eval("CountryCode") as string)%>
                        </td>
                        <td class="Ip">
                            <%# XsltLogFormat.FormatIp(Eval("IP") as string)%>
                        </td>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr class="odd">
                        <td>
                            <%#Eval("LogID")%>
                        </td>
                        <td class="datetimeInlinde">
                            <%#Eval("LogDate", "{0:d} <br/> {0:hh:mm:ss tt}")%>
                        </td>
                        <td class="EditedBy">
                            <%#Eval("UserName")%>
                        </td>
                        <td class="ActionName">
                            <%#Eval("ActionName")%>
                        </td>
                        <td class="actionDesc">
                            <%#Eval("Doing")%>
                        </td>
                        <td>
                            <%#Eval("Domain")%>
                        </td>
                        <td class="Browser">
                            <%#Eval("Browser")%>
                        </td>
                        <td>
                            <%#XsltLogFormat.FormatCountryCode(Eval("IP") as string, Eval("CountryCode") as string)%>
                        </td>
                        <td class="Ip">
                            <%# XsltLogFormat.FormatIp(Eval("IP") as string)%>
                        </td>
                    </tr>
                </AlternatingItemTemplate>
            </asp:Repeater>
            <tr class="noInfo" id="ltrNoInfo" runat="server">
                <td colspan="10">
                    <%=GetStringFromGlobalResource("noInfo")%>
                </td>
            </tr>
        </table>
    </div>
    <%--pager--%>
    <%=Pager%>
    <%--/pager--%>
</asp:Content>
