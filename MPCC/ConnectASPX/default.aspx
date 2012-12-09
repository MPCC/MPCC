<%@ Page Title="Connect | Log In" Language="C#" MasterPageFile="~/connect.master"
    AutoEventWireup="true" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:Login ID="LoginUser" runat="server" EnableViewState="false" RenderOuterTable="false">
        <LayoutTemplate>
            <span class="failureNotification">
                <asp:Literal ID="FailureText" runat="server"></asp:Literal>
            </span>
            <div class="account-container">
                <div class="content clearfix">
                    <h1>
                        Sign In</h1>
                    <div class="login-fields">
                        <p>
                            Sign in using your registered account:</p>
                        <div class="alert alert-error hidden">
                            <asp:ValidationSummary ID="LoginUserValidationSummary" runat="server" CssClass="alert alert-error hidden"
                                ValidationGroup="LoginUserValidationGroup" />
                        </div>
                        <div class="field">
                            <label for="MainContent_LoginUser_UserNameRequired">
                                Username:</label>
                            <asp:TextBox ID="UserName" runat="server" placeholder="Username" CssClass="login username-field"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                CssClass="failureNotification" ErrorMessage="User Name is required." ToolTip="User Name is required."
                                ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                        </div>
                        <div class="field">
                            <label for="MainContent_LoginUser_Password">
                                Password:</label>
                            <asp:TextBox ID="Password" runat="server" CssClass="login password-field" placeholder="Password"
                                TextMode="Password"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                                CssClass="failureNotification" ErrorMessage="Password is required." ToolTip="Password is required."
                                ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="login-actions">
                        <span class="login-checkbox">
                            <input type="checkbox" id="RememberMe" name="RememberMe" class="field login-checkbox" />
                            <label for="RememberMe" class="choice">
                                Keep me logged in</label>
                        </span>
                        <asp:LinkButton ID="LoginButton" CssClass="button btn btn-warning btn-large" runat="server"
                            CommandName="Login" ValidationGroup="LoginUserValidationGroup"><i class="icon-signin"></i> Sign In</asp:LinkButton>
                    </div>
                </div>
            </div>
        </LayoutTemplate>
    </asp:Login>
</asp:Content>
