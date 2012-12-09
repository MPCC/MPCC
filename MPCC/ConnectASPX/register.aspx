<%@ Page Title=" Connect | Register" Language="C#" MasterPageFile="~/connect.master" AutoEventWireup="true"
    CodeBehind="register.aspx.cs" Inherits="ConnectASPX.Account.Register" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div class="account-container register">
        <div class="content clearfix">
            <asp:CreateUserWizard ID="RegisterUser" runat="server" EnableViewState="false" OnCreatedUser="RegisterUser_CreatedUser">
                <LayoutTemplate>
                    <asp:PlaceHolder ID="wizardStepPlaceholder" runat="server"></asp:PlaceHolder>
                    <asp:PlaceHolder ID="navigationPlaceholder" runat="server"></asp:PlaceHolder>
                </LayoutTemplate>
                <WizardSteps>
                    <asp:CreateUserWizardStep ID="RegisterUserWizardStep" runat="server">
                        <ContentTemplate>
                            <h1>
                                Create a New Account</h1>
                            <div class="login-social">
                                <p style="margin: 0;">
                                    <strong>Note:</strong> Passwords are required to be a minimum of
                                    <%= Membership.MinRequiredPasswordLength %>
                                    characters in length.</p>
                            </div>
                            <span class="failureNotification">
                                <asp:Literal ID="ErrorMessage" runat="server"></asp:Literal>
                            </span>
                            <asp:ValidationSummary ID="RegisterUserValidationSummary" runat="server" CssClass="failureNotification"
                                ValidationGroup="RegisterUserValidationGroup" />
                            <div class="accountInfo">
                                <fieldset class="register">
                                    <legend>Account Information</legend>
                                    <div class="login-fields">
                                        <div class="field clearfix">
                                            <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">User Name:</asp:Label>
                                            <asp:TextBox ID="UserName" runat="server" placeholder="User Name" CssClass="login"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                                CssClass="failureNotification" ErrorMessage="User Name is required." ToolTip="User Name is required."
                                                ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                                        </div>
                                        <div class="field">
                                            <asp:Label ID="EmailLabel" runat="server" AssociatedControlID="Email">E-mail:</asp:Label>
                                            <asp:TextBox ID="Email" runat="server" placeholder="E-mail" CssClass="login"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="Email"
                                                CssClass="failureNotification" ErrorMessage="E-mail is required." ToolTip="E-mail is required."
                                                ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                                        </div>
                                        <div class="field">
                                            <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password:</asp:Label>
                                            <asp:TextBox ID="Password" runat="server" placeholder="Password" CssClass="login"
                                                TextMode="Password"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                                                CssClass="failureNotification" ErrorMessage="Password is required." ToolTip="Password is required."
                                                ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                                        </div>
                                        <div class="field">
                                            <asp:Label ID="ConfirmPasswordLabel" runat="server" AssociatedControlID="ConfirmPassword">Confirm Password:</asp:Label>
                                            <asp:TextBox ID="ConfirmPassword" runat="server" placeholder="Confirm Password" CssClass="login"
                                                TextMode="Password"></asp:TextBox>
                                            <asp:RequiredFieldValidator ControlToValidate="ConfirmPassword" CssClass="failureNotification"
                                                Display="Dynamic" ErrorMessage="Confirm Password is required." ID="ConfirmPasswordRequired"
                                                runat="server" ToolTip="Confirm Password is required." ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                                            <asp:CompareValidator ID="PasswordCompare" runat="server" ControlToCompare="Password"
                                                ControlToValidate="ConfirmPassword" CssClass="failureNotification" Display="Dynamic"
                                                ErrorMessage="The Password and Confirmation Password must match." ValidationGroup="RegisterUserValidationGroup">*</asp:CompareValidator>
                                        </div>
                                    </div>
                                </fieldset>
                                <div class="login-actions">
                                    <asp:LinkButton ID="CreateUserButton" CssClass="button btn btn-warning btn-large"
                                        runat="server" CommandName="MoveNext" Text="Create User" ValidationGroup="RegisterUserValidationGroup"><i class="icon-user"></i> Register</asp:LinkButton>
                                </div>
                            </div>
                        </ContentTemplate>
                        <CustomNavigationTemplate>
                        </CustomNavigationTemplate>
                    </asp:CreateUserWizardStep>
                </WizardSteps>
            </asp:CreateUserWizard>
        </div>
    </div>
</asp:Content>
