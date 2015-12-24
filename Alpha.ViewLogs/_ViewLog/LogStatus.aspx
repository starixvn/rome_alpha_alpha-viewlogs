<%@ Page Title="" Language="C#" MasterPageFile="~/_ViewLog/ViewLog.Master" AutoEventWireup="true" CodeBehind="LogStatus.aspx.cs" Inherits="Alpha.ViewLogs._ViewLog.LogStatus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentData" runat="Server">
    <div id="content">
        <table width="100%" cellpadding="0" cellspacing="0" id="DataLog">
            <tr>
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
                    <%= Resources.Viewlog.actionDesc%>
                </th>
            </tr>
            <asp:Repeater ID="rptDataLog" runat="server" EnableViewState="false">
                <ItemTemplate>
                    <tr>
                        <td class="LogId">
                            <%#Eval("LogID")%>
                        </td>
                        <td class="datetime">
                            <%#Eval("LogDate", "{0:d} <br/> {0:T}")%>
                        </td>
                        <td class="EditedBy">
                            <%#Eval("UserName")%>
                        </td>
                        <td class="ActionName">
                            <%#Eval("ActionName")%>
                        </td>
                        <td class="TrackingValue">
                            <%#GetContentHtml(Eval("TrackingValue"), (int)Eval("ActionID"), (long)Eval("LogID"))%>
                        </td>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr>
                        <td class="LogId">
                            <%#Eval("LogID")%>
                        </td>
                        <td class="datetime">
                            <%#Eval("LogDate", "{0:d} <br/> {0:hh:mm:ss tt}")%>
                        </td>
                        <td class="EditedBy">
                            <%#Eval("UserName")%>
                        </td>
                        <td class="ActionName">
                            <%#Eval("ActionName")%>
                        </td>
                        <td class="TrackingValue">
                            <%#GetContentHtml(Eval("TrackingValue"), (int)Eval("ActionID"), (long)Eval("LogID"))%>
                        </td>
                    </tr>
                </AlternatingItemTemplate>
            </asp:Repeater>
            <tr class="noInfo" id="ltrNoInfo" runat="server">
                <td colspan="5">
                    <%=GetStringFromGlobalResource("noInfo")%>
                </td>
            </tr>
        </table>
    </div>
    <%--pager--%>
    <%=Pager%>
    <%--/pager--%>
</asp:Content>
