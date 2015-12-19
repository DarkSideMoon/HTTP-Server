using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HttpServer.Library
{
    public class Quote
    {
        private string _fileName = string.Empty;
        private string _fileLocation = string.Empty;
        private string _allQuotes = string.Empty;
        private string[] _arrQuotes;

        public Quote()
        {
            string wanted_path = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));
            string _pathToFolder = wanted_path.Substring(0, 22) + "External\\";

            this._fileLocation = _pathToFolder;
            this._fileName = "Quotes.txt";
        }

        public string GetQuote()
        {
            Random random = new Random();
            string quote = string.Empty;
            try
            {
                this.ReadAllQuotes();
                string[] arr = this.FindQuotes();

                int index = random.Next(arr.Length);
                quote = arr[index];

                return quote;
            }
            catch (Exception)
            {
                return "Курс — он у нас один — правильный.";
            }
        }

        private void ReadAllQuotes()
        {
            try
            {
                using (FileStream stream = new FileStream(this._fileLocation + "\\" + this._fileName, FileMode.Open, FileAccess.Read))
                {
                    using (StreamReader _reader = new StreamReader(stream))
                    {
                        this._allQuotes = _reader.ReadToEnd();
                    }
                }
            }
            catch (ObjectDisposedException ex)
            {
                throw new ObjectDisposedException(ex.Message, ex);
            }
        }

        private string[] FindQuotes()
        {
            return this._arrQuotes = Regex.Split(this._allQuotes, "\r\n");
        }
    }
}
