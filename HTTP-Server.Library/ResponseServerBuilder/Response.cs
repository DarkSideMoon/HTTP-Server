using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer.Library.ResponseServer
{
    #region Response example #1
    //HTTP/1.1 200 OK
    //Vary: Authorization,Accept
    //Transfer-Encoding: chunked
    //Etag: "fa2ba873343ba638123b7671c8c09998"
    //Content-Type: application/vnd.bonfire+xml; charset=utf-8
    //Date: Wed, 01 Jun 2011 14:59:30 GMT
    //Server: thin 1.2.11 codename Bat-Shit Crazy
    //Allow: GET,OPTIONS,HEAD
    //Cache-Control: public, max-age=120
    //Connection: close
    #endregion

    #region Response example #2
    //HTTP/1.1 400 Bad Request
    //Date: Sun, 18 Oct 2012 10:36:20 GMT
    //Server: Apache/2.2.14 (Win32)
    //Content-Length: 230
    //Content-Type: text/html; charset=iso-8859-1
    //Connection: Closed
    #endregion

    public class Response
    {
        public DateTime DateTimeResponse { get; set; }
        public string StatusDesc { get; set; }
        public int StatusCode { get; set; }
        public Encoding Charset { get; set; }
        public int ContentLength { get; set; }
        public string ContentType { get; set; }
        public bool IsConnected { get; set; }
        public string Head { get; set; }
        public string Body { get; set; }
        public ProtocolType ProtocolType { get; set; }
        public AddressFamily AddressFamily { get; set; }
        public string Html { get; set; }
    }
}
