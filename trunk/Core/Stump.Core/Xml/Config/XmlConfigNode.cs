using System;
using System.Reflection;
using System.Text;
using System.Xml;
using Stump.Core.Attributes;
using Stump.Core.Reflection;

namespace Stump.Core.Xml.Config
{
    public class XmlConfigNode
    {
        private object m_newValue;
        private string m_documentation;

        public XmlConfigNode(XmlNode node)
        {
            Node = node;

            Name = node.Attributes["name"] != null ? node.Attributes["name"].Value : "";
            Serialized = node.Attributes["serialized"] != null ? node.Attributes["serialized"].Value == "true" : false;
            ClassName = GetClassNameFromNode(node);
            Namespace = GetNamespaceFromNode(node);
        }

        public XmlConfigNode(FieldInfo field)
        {
            BindedField = field;

            Name = field.Name;
            Serialized = !field.FieldType.HasInterface(typeof (IConvertible)) || field.FieldType.IsEnum;
            ClassName = field.DeclaringType.Name;
            Namespace = field.DeclaringType.Namespace;
        }

        public XmlConfigNode(PropertyInfo property)
        {
            BindedProperty = property;

            Name = property.Name;
            Serialized = !property.PropertyType.HasInterface(typeof(IConvertible)) || property.PropertyType.IsEnum;
            ClassName = property.DeclaringType.Name;
            Namespace = property.DeclaringType.Namespace;
        }

        public XmlNode Node
        {
            get;
            private set;
        }

        /// <summary>
        /// Field namespace
        /// </summary>
        public string Namespace
        {
            get;
            private set;
        }

        public string AssemblyName
        {
            get;
            private set;
        }

        public string ClassName
        {
            get;
            private set;
        }

        /// <summary>
        /// Field name
        /// </summary>
        public string Name
        {
            get;
            private set;
        }

        public bool Serialized 
        { 
            get; 
            set;
        }

        public string Path
        {
            get { return Namespace + "." + ClassName + "." + Name; }
        }

        public string Documentation
        {
            get
            {
                if (!string.IsNullOrEmpty(m_documentation))
                    return m_documentation;

                if (Node != null && Node.PreviousSibling != null && Node.PreviousSibling.NodeType == XmlNodeType.Comment)
                    return Node.PreviousSibling.Value;

                return "";
            }
            set
            {
                m_documentation = value;
            }
        }

        public VariableAttribute Attribute
        {
            get;
            private set;
        }

        public FieldInfo BindedField
        {
            get;
            private set;
        }

        public PropertyInfo BindedProperty
        {
            get;
            private set;
        }

        public void BindToField(FieldInfo fieldInfo)
        {
            if (BindedProperty != null)
                throw new Exception(string.Format("Node already binded to a property : {0}", BindedProperty.Name));

            if (!fieldInfo.IsStatic)
                throw new Exception(string.Format("A variable field have to be static : {0} is not static", BindedField.Name));

            Attribute = fieldInfo.GetCustomAttribute<VariableAttribute>();

            if (Attribute == null)
                throw new Exception(string.Format("{0} has no variable attribute", BindedField.Name));

            BindedField = fieldInfo;
        }

        public void BindToProperty(PropertyInfo propertyInfo)
        {
            if (BindedField != null)
                throw new Exception(string.Format("Node already binded to a field : {0}", BindedField.Name));

            if (!propertyInfo.CanRead || !propertyInfo.CanWrite)
                throw new Exception(string.Format("{0} has not get and set accessors", BindedProperty.Name));

            Attribute = propertyInfo.GetCustomAttribute<VariableAttribute>();

            if (Attribute == null)
                throw new Exception(string.Format("{0} has no variable attribute", BindedProperty.Name));

            BindedProperty = propertyInfo;
        }

        /// <summary>
        /// Read the element as it should appears in the xml file
        /// </summary>
        /// <returns></returns>
        /// <remarks>The return value can be differant from the field value</remarks>
        public object GetValue()
        {
            if (BindedField != null && BindedProperty == null)
            {
                if (m_newValue != null &&
                    !Attribute.DefinableRunning)
                    return m_newValue;

                return BindedField.GetValue(null);
            }

            else if (BindedProperty != null && BindedField == null)
            {
                if (m_newValue != null &&
                    !Attribute.DefinableRunning)
                    return m_newValue;

                return BindedProperty.GetValue(null, new object[0]);
            }

            throw new Exception(string.Format("Cannot read the config node '{0}' because no member has been binded to it", Path));
        }

        public void SetValue(object value, bool alreadyRunning = false)
        {
            if (BindedField != null && BindedProperty == null)
            {
                if (m_newValue == null && !alreadyRunning)
                    BindedField.SetValue(null, value);

                else if (Attribute.DefinableRunning)
                    BindedField.SetValue(null, value);

                m_newValue = value;
            }

            else if (BindedProperty != null && BindedField == null)
            {
                if (m_newValue == null && !alreadyRunning)
                    BindedProperty.SetValue(null, value, new object[0]);

                else if (Attribute.DefinableRunning)
                    BindedProperty.SetValue(null, value, new object[0]);

                m_newValue = value;
            }
        }

        private static string GetNamespaceFromNode(XmlNode node)
        {
            var stringBuilder = new StringBuilder();

            XmlNode currentNode = node.ParentNode; // ignore the class node
            while (currentNode.ParentNode != null && currentNode.ParentNode != currentNode.OwnerDocument.DocumentElement)
            {
                stringBuilder.Insert(0, currentNode.ParentNode.Name + ".");

                currentNode = currentNode.ParentNode;
            }

            return stringBuilder.Remove(stringBuilder.Length - 1, 1).ToString(); // remove the dot at the end
        }

        private static string GetClassNameFromNode(XmlNode node)
        {
            return node.ParentNode.Name;
        }
    }
}