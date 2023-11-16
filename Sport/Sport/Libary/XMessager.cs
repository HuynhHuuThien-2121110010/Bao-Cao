using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sport
{
    public class XMessenger
    {
        public string TypeMsg { get; set; }
        public string Msg { get; set; }
        public XMessenger() { }
        public XMessenger(string typeMsg, string msg) 
        {
            this.TypeMsg = typeMsg;
            this.Msg = msg;
        }
    }
}