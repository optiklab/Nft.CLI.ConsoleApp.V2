using Nft.App.Models;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Nft.App.Repository
{
    public class XmlHelpers
    {
        #region Public methods

        public static string ConvertXMLObjectToString(Type type, object response)
        {
            if (response == null)
            {
                throw new ArgumentNullException("response");
            }

            string result;

            using (var stream = new MemoryStream())
            {
                using (var textWriter = new XmlTextWriter(stream, Encoding.UTF8))
                {
                    var xmlSerializer = new XmlSerializer(type);
                    xmlSerializer.Serialize(textWriter, response);

                    using (var internalStream = (MemoryStream)textWriter.BaseStream)
                    {
                        result = Encoding.UTF8.GetString(internalStream.ToArray());
                    }
                }
            }

            return result;
        }

        public static object ConvertStringToXMLObject(Type type, string response)
        {
            object result = null;

            var xmlSerializer = new XmlSerializer(type);

            var bytes = Encoding.UTF8.GetBytes(response);

            using (var stream = new MemoryStream(bytes))
            {
                using (var textReader = new XmlTextReader(stream))
                {
                    textReader.MoveToContent();
                    result = xmlSerializer.Deserialize(textReader);
                }
            }

            return result;
        }

        #endregion
    }
}
