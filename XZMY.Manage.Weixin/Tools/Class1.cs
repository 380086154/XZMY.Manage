using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace XZMY.Manage.Weixin.Tools
{
    public static class XMLHelper
    {
        public static SortedDictionary<string, object> FromXml(string xml)
        {
            var m_values = new SortedDictionary<string, object>();
            if (string.IsNullOrEmpty(xml))
                return m_values;
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);
            XmlNode xmlNode = xmlDoc.FirstChild;//获取到根节点<xml>
            XmlNodeList nodes = xmlNode.ChildNodes;
            foreach (XmlNode xn in nodes)
            {
                XmlElement xe = (XmlElement)xn;
                m_values[xe.Name] = xe.InnerText;
            }
            return m_values;
        }
    }
}
