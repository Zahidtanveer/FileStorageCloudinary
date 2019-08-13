<%@ Page Title="Home Page" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Default.aspx.vb" Inherits="_Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <br />
    <div class="row">
       <asp:FileUpload ID="fileUploadControl" runat="server" /><br />
    <asp:Button ID="btnUpload" runat="server" CssClass="btn btn-primary" Text="Upload"/>
    <asp:Label  ID="fileUploadStatus" runat="server"></asp:Label>
    </div>
    <br /><br />
    <div class="row">
        <div class="col-md-6">
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" ShowHeader="False" ShowFooter="false" BorderStyle="None">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Image ID="Image1" runat="server" ImageUrl='<%# Eval("Image") %>'  Width="100%" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                </Columns>
            </asp:GridView>
        </div>
        <div class="col-md-6">
            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="false" ShowHeader="False" ShowFooter="false" BorderStyle="None">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>

                            <video width="320" height="240" controls autoplay muted>
                                <source src="<%# Eval("Video") %>" type="video/ogg">
                                <source src="<%# Eval("Video") %>" type="video/mp4">
                                <object data="movie.mp4" width="320" height="240">
                                    <embed width="320" height="240" src='<%# Eval("Video") %> '></object></video>


                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
