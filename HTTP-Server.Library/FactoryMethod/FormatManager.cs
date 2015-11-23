using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer.Library.FactoryMethod
{
    // Задать вопрос про Factory method
    // Можно не создавать лишние классы такие как JsonManager, TextManager, XmlManager
    public abstract class FormatManager
    {
        /// <summary>
        /// 2 method of realisation
        /// Factory method
        /// The return type will not work if does not implement the Format abstract class
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        public static Format Get(Format format)
        {
            switch (format.TypeResponse)
            {
                case TypeFormat.Html:
                    return new HtmlFormat(format.Name, format.IsSave);
                case TypeFormat.Json:
                    return new JsonFormat();
                case TypeFormat.XML:
                    return new XmlFormat();
                default:
                    return null;
            }
        }

        public abstract Format CreateNewFormat();

        /// <summary>
        /// 1 method of realisation
        /// Factory method
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        public bool Save(Format format)
        {
            if (format.IsSave)
                return false;
            // Fucktory
            // using Factory method to create new format of response page
            Format storage = this.CreateNewFormat();
            storage.Create(format.Name, format);
            return true;
        }
    }
}
