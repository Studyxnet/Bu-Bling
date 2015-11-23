using System;
using System.Xml.Serialization;

namespace bubling.Droid
{
    [XmlRoot(ElementName = "string", Namespace = "http://cpro21201.publiccloud.com.br/bu-bling/ws/")]
    public class XMLString
    {
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }

        [XmlText]
        public string Text { get; set; }
    }
}

