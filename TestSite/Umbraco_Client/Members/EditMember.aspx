<%@ Page language="c#" MasterPageFile="../masterpages/umbracoPage.Master" 
    ValidateRequest="false" AutoEventWireup="True" Inherits="umbraco.cms.presentation.members.EditMember" %>

<asp:Content ContentPlaceHolderID="head" runat="server">
		<script type="text/javascript">
		  // Save handlers for IDataFields		
		    var saveHandlers = new Array();

		  function addSaveHandler(handler) {
		    saveHandlers[saveHandlers.length] = handler;
		  }

		  function invokeSaveHandlers() {
		    for (var i = 0; i < saveHandlers.length; i++) {
		      eval(saveHandlers[i]);
		    }
		  }
		</script>
</asp:Content>

<asp:Content ContentPlaceHolderID="body" runat="server">
			<input id="doSave" type="hidden" name="doSave" runat="server" /> <input id="doPublish" type="hidden" name="doPublish" runat="server" />
			<asp:PlaceHolder id="plc" Runat="server"></asp:PlaceHolder>
</asp:Content>