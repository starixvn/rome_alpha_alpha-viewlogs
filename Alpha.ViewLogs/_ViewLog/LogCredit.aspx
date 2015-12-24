<%@ Page Title="" Language="C#" MasterPageFile="~/_ViewLog/ViewLog.Master" AutoEventWireup="true" CodeBehind="LogCredit.aspx.cs" Inherits="Alpha.ViewLogs._ViewLog.LogCredit" %>

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
                     <%= Alpha.ViewLogs.LocalResources.Viewlog.usernameEdited%>
                </th>
                <th>
                     <%= Alpha.ViewLogs.LocalResources.Viewlog.actionname%>
                </th>
                <th>
                    <%= Alpha.ViewLogs.LocalResources.Viewlog.OldValue%>
                </th>
                <th>
                    <%= Alpha.ViewLogs.LocalResources.Viewlog.NewValue%>
                </th>
                <th>
                    <%= Alpha.ViewLogs.LocalResources.Viewlog.actionDesc%>
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
                            <%#Eval("LogDate")%>
                        </td>
                        <td class="EditedBy">
                            <%#Eval("UserName")%>
                        </td>
                        <td class="ActionName">
                            <%#Eval("ActionName")%>
                        </td>
                        <td class="Value">
                            <%#Eval("OldValue", "{0:#,##0.00}")%>
                        </td>
                        <td class="Value">
                            <%#Eval("NewValue", "{0:#,##0.00}")%>
                        </td>
                        <td class="Doing">
                            <%#Eval("Doing")%>
                        </td>
                        <td class="Ip">
                            <%#XsltLogFormat.FormatIp(Eval("IP") as string)%>
                        </td>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr class="odd">
                        <td>
                            <%#Eval("LogID")%>
                        </td>
                        <td class="datetimeInlinde">
                            <%#Eval("LogDate")%>
                        </td>
                        <td class="EditedBy">
                            <%#Eval("UserName")%>
                        </td>
                        <td class="ActionName">
                            <%#Eval("ActionName")%>
                        </td>
                        <td class="Value">
                            <%#Eval("OldValue", "{0:#,##0.00}")%>
                        </td>
                        <td class="Value">
                            <%#Eval("NewValue", "{0:#,##0.00}")%>
                        </td>
                        <td class="Doing">
                            <%#Eval("Doing")%>
                        </td>
                        <td class="Ip">
                            <%#XsltLogFormat.FormatIp(Eval("IP") as string)%>
                        </td>
                    </tr>
                </AlternatingItemTemplate>
            </asp:Repeater>
            <tr class="noInfo" id="ltrNoInfo" runat="server">
                <td colspan="8">
                    <%= Alpha.ViewLogs.LocalResources.Lang.noInfo %>
                </td>
            </tr>
        </table>
    </div>
    <%--pager--%>
    <%=Pager%>
    <%--/pager--%>
</asp:Content>