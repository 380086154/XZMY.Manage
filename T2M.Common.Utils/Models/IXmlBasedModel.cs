using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace T2M.Common.Utils.Models
{
    public interface IXmlBasedModel<TId>
    {
        TId Id { get; set; }
        void LoadProperties(XmlNode node);
    }
}
