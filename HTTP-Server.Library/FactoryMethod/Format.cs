using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer.Library.FactoryMethod
{
    public enum TypeFormat
    {
        Html,
        Json,
        XML,
    }

    public abstract class Format
    {
        public string Name { get; set; }
        public bool IsSave { get; set; }
        public TypeFormat TypeResponse { get; set; }
        public int Length { get; set; }
        public string Content { get; set; }

        public abstract void Create(string name, Format myFormat);
        public abstract string GetInfoFormat();
    }
}
