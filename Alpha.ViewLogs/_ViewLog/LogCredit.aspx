<%@ Page Title="" Language="C#" MasterPageFile="~/_ViewLog/ViewLog.Master" AutoEventWireup="true" CodeBehind="LogCredit.aspx.cs" Inherits="Alpha.ViewLogs._ViewLog.LogCredit" %>

<%@ Import Namespace="Sunplus.Agent.UI._ViewLog" %>
<asp:Content ID="Content1" ContentPlaceHolderID="contentData" runat="Server">
    <div id="content">
        <table width="100%" cellpadding="0" cellspacing="0" id="DataLog">
            <tr class="title">
                <th>
                    <%= Resources.Viewlog.logid%>
                </th>
                <th>
                     <%= Resources.Viewlog.logdate%>
                </th>
                <th>
                     <%= Resources.Viewlog.usernameEdited%>
                </th>
                <th>
                     <%= Resources.Viewlog.actionname%>
                </th>
                <th>
                    <%= Resources.Viewlog.OldValue%>
                </th>
                <th>
                    <%= Resources.Viewlog.NewValue%>
                </th>
                <th>
                    <%= Resources.Viewlog.actionDesc%>
                </th>
                <th>
                    <%= Resources.Viewlog.IP%>
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
                    <%= Resources.Lang.noInfo %>
                </td>
            </tr>
        </table>
    </div>
    <%--pager--%>
    <%=Pager%>
    <%--/pager--%>
</asp:Content>