using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stump.Server.BaseServer.IPC.Messages
{
    [ProtoContract]
    public class TokenMessage : IPCMessage
    {
        public TokenMessage()
        {

        }

        [ProtoMember(26)]
        public int Token
        {
            get;
            set;
        }
        [ProtoMember(2)]
        public string Login
        {
            get;
            set;
        }
    }
}

