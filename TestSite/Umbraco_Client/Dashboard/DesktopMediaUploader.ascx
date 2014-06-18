﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DesktopMediaUploader.ascx.cs" Inherits="umbraco.presentation.umbraco.dashboard.DesktopMediaUploader" %>
<%@ Register Assembly="controls" Namespace="umbraco.uicontrols" TagPrefix="umb" %>
<%@ Register Assembly="ClientDependency.Core" Namespace="ClientDependency.Core.Controls" TagPrefix="umb" %>

<umb:CssInclude runat="server" FilePath="propertypane/style.css" PathNameAlias="UmbracoClient" />
<umb:JsInclude runat="server" FilePath="dashboard/scripts/swfobject.js" PathNameAlias="UmbracoRoot" />

<div class="dashboardWrapper">
    <h2>Desktop Media Uploader</h2>
    <img src="./dashboard/images/dmu.png" alt="Umbraco" class="dashboardIcon" />
    <p><strong>Desktop Media Uploader</strong> is a small desktop application that you can install on your computer which allows you to easily upload media items directly to the media section.</p>
    <p>The badge below will auto configure itself based upon whether you already have <strong>Desktop Media Uploader</strong> installed or not.</p>
    <p>Just click the <strong>Install Now / Upgrade Now / Launch Now</strong> link to perform that action.</p>
    <div class="dashboardColWrapper">
        <div class="dashboardCols">
            <div class="dashboardCol">
                <asp:Panel ID="Panel1" runat="server"></asp:Panel>
                <asp:Panel ID="Panel2" runat="server">
                    <p>
                        <div id="dmu-badge">
                            Download <a href="<%= FullyQualifiedAppPath %>umbraco/dashboard/air/desktopmediauploader.air">Desktop Media Uploader</a> now.<br /><br /><span id="Span1">This application requires Adobe&#174;&nbsp;AIR&#8482; to be installed for <a href="http://airdownload.adobe.com/air/mac/download/latest/AdobeAIR.dmg">Mac OS</a> or <a href="http://airdownload.adobe.com/air/win/download/latest/AdobeAIRInstaller.exe">Windows</a>.
                        </div>
                    </p>
                    <script type="text/javascript">
                    // <![CDATA[
                        var flashvars = {
                            appid: "org.umbraco.DesktopMediaUploader",
                            appname: "Desktop Media Uploader",
                            appversion: "v2.1.0",
                            appurl: "<%= FullyQualifiedAppPath %>umbraco/dashboard/air/desktopmediauploader.air",
                            applauncharg: "<%= AppLaunchArg %>",
                            image: "/umbraco/dashboard/images/dmu-badge.jpg?2.1.0",
                            airversion: "2.0"
                        };
                        var params = {
                            menu: "false",
                            wmode: "opaque"
                        };
                        var attributes = {
                            style: "margin-bottom:10px;"
                        };

                        swfobject.embedSWF("/umbraco/dashboard/swfs/airinstallbadge.swf", "dmu-badge", "215", "180", "9.0.115", "/umbraco/dashboard/swfs/expressinstall.swf", flashvars, params, attributes);
                    // ]]>
                    </script>
                </asp:Panel>
            </div>
        </div>
    </div>
</div>