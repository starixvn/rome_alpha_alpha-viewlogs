<%@ Page Title="" Language="C#" MasterPageFile="~/_ViewLog/ViewLog.Master" AutoEventWireup="true" CodeBehind="LogLogin.aspx.cs" Inherits="Alpha.ViewLogs._ViewLog.LogLogin" %>

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
                    <%= Resources.Viewlog.username%>
                </th>
                <th>
                    <%= Resources.Viewlog.actionname%>
                </th>
                <th>
                    <%= Resources.Viewlog.actionDesc%>
                </th>
                <th>
                    <%= Resources.Viewlog.Domain%>
                </th>
                <th>
                    <%= Resources.Viewlog.Browser%>
                </th>
                <th>
                    <%= Resources.Viewlog.country%>
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
