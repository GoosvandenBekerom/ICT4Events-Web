<%@ Page Title="Reports Admin Section" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Reports.aspx.cs" Inherits="ICT4Events_Web.Views.SocialMediaSystem.Reports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1><%:Page.Title%></h1>
    
    <div class="form-group">
        <asp:Label ID="Label1" runat="server" Text="Selecteer een bericht" AssociatedControlID="lbReportedPosts"></asp:Label>
        <asp:ListBox ID="lbReportedPosts" CssClass="form-control" AutoPostBack="True" Rows="7"
            OnSelectedIndexChanged="lbReportedPosts_OnSelectedIndexChanged" runat="server">
        
        </asp:ListBox>
    </div>

    <asp:PlaceHolder ID="phPost" runat="server">
        
    </asp:PlaceHolder>

    <div class="form-group">
        <button id="removeReportsButton" class="btn btn-primary">Verwijder Reports</button>
    </div>
    
    <script type="text/javascript">
        var messageId = <%:_messageId%>;
        if (messageId === 0) {
            $('#removeReportsButton').addClass("disabled");
        }
        $('#removeReportsButton').on("click", function () {
            var btn = $(this);

            $.ajax({
                type: "POST",
                url: "<%=VirtualPathUtility.ToAbsolute("~/Views/SocialMediaSystem/Reports.aspx/RemoveReports")%>",
                data: "{'postId':"+messageId+"}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (result.d !== "Not authorized") {
                        $("#lbReportedPosts option[value='"+messageId+"']").remove();
                        btn.addClass("disabled");
                    }
                    e.preventDefault();
                }
            });
        });
    </script>
</asp:Content>
