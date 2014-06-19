namespace Our.Umbraco.PropertyConverters.Install
{
    using System;
    using System.Web;
    using System.Xml;

    using global::Umbraco.Core;
    using global::Umbraco.Core.Logging;

    public static class NameSpace
    {
        public static bool Add(string file, string xPath, string nameSpace)
        {
            // Set result default to false
            var result = false;

            //file = "~/Views/web.config";
            //xPath = "//configuration/system.web.webPages.razor/pages";

            // Create a new xml document
            var document = new XmlDocument { PreserveWhitespace = true };

            // Load the web.config file into the xml document
            var configFileName = VirtualPathUtility.ToAbsolute(file);
            document.Load(HttpContext.Current.Server.MapPath(configFileName));

            // Select root node in the web.config file for insert new nodes
            var rootNode = document.SelectSingleNode(xPath);

            // Check for rootNode exists
            if (rootNode == null)
            {
                return false;
            }

            // Set modified document default to false
            var modified = false;

            // Set insert node default true
            var insertNode = true;

            // Check for namespaces node
            if (rootNode.SelectSingleNode("namespaces") == null)
            {
                // Create namespaces node
                var namespacesNode = document.CreateElement("namespaces");
                rootNode.AppendChild(namespacesNode);

                // Replace root node
                rootNode = namespacesNode;

                // Mark document modified
                modified = true;
            }
            else
            {
                // Replace root node
                rootNode = rootNode.SelectSingleNode("namespaces");
            }

            // Look for existing nodes with same path like the new node
            if (rootNode != null && rootNode.HasChildNodes)
            {
                // Look for existing nodeType nodes
                var node = rootNode.SelectSingleNode(string.Format("//add[@namespace = '{0}']", nameSpace));

                // If path already exists 
                if (node != null)
                {
                    // Cancel insert node operation
                    insertNode = false;
                }
            }

            // Check for insert flag
            if (insertNode)
            {
                // Create new node with attributes
                var newNode = document.CreateElement("add");
                newNode.Attributes.Append(XmlHelper.AddAttribute(document, "namespace", nameSpace));

                // Append new node at the end of root node
                if (rootNode != null)
                {
                    rootNode.AppendChild(newNode);

                    // Mark document modified
                    modified = true;
                }
                else
                {
                    modified = true;      
                }
            }

            // Check for modified document
            if (modified)
            {
                try
                {
                    // Save the Rewrite config file with the new rewerite rule
                    document.Save(HttpContext.Current.Server.MapPath(configFileName));

                    // No errors so the result is true
                    result = true;
                }
                catch (Exception e)
                {
                    // Log error message
                    var message = "Error while adding namespace: " + e.Message;
                    LogHelper.Error(typeof(NameSpace), message, e);
                }
            }

            return result;
        }

        public static bool Remove(string file, string xPath, string nameSpace)
        {
            // Set result default to false
            var result = false;

            // Create a new xml document
            var document = new XmlDocument();

            // Keep current indentions format
            document.PreserveWhitespace = true;

            // Load the web.config file into the xml document
            var configFileName = VirtualPathUtility.ToAbsolute(file);
            document.Load(HttpContext.Current.Server.MapPath(configFileName));

            // Select root node in the web.config file for insert new nodes
            var rootNode = document.SelectSingleNode(xPath);

            // Check for rootNode exists
            if (rootNode == null)
            {
                return false;
            }

            // Set modified document default to false
            var modified = false;

            // Look for existing nodes with same path of undo attribute
            if (rootNode.HasChildNodes)
            {
                // Look for existing add nodes with attribute path
                var xmlNodeList = rootNode.SelectNodes(
                    string.Format("//add[@namespace = '{0}']", nameSpace));
                if (xmlNodeList != null)
                {
                    foreach (XmlNode existingNode in xmlNodeList)
                    {
                        // Remove existing node from root node
                        rootNode.RemoveChild(existingNode);
                        modified = true;
                    }
                }
            }

            if (modified)
            {
                try
                {
                    // Save the Rewrite config file with the new rewerite rule
                    document.Save(HttpContext.Current.Server.MapPath(configFileName));

                    // No errors so the result is true
                    result = true;
                }
                catch (Exception e)
                {
                    // Log error message
                    var message = "Error while removing namespace: " + e.Message;
                    LogHelper.Error(typeof(NameSpace), message, e);
                }
            }

            return result;
        }

        public static bool Check(string file, string xPath, string nameSpace){

            // Create a new xml document
            var document = new XmlDocument { PreserveWhitespace = true };

            // Load the web.config file into the xml document
            var configFileName = VirtualPathUtility.ToAbsolute(file);
            document.Load(HttpContext.Current.Server.MapPath(configFileName));

            // Select root node in the web.config file for insert new nodes
            var rootNode = document.SelectSingleNode(xPath);

            // Check for rootNode exists
            if (rootNode == null)
            {
                return false;
            }

            // Look for existing nodes with same path of undo attribute
            if (rootNode.HasChildNodes)
            {
                // Look for existing add nodes with attribute path
                var xmlNodeList = rootNode.SelectNodes(
                    string.Format("//add[@namespace = '{0}']", nameSpace));
                if (xmlNodeList != null)
                {
                    return true;
                }
            }

            return false;            
        }
    }
}
