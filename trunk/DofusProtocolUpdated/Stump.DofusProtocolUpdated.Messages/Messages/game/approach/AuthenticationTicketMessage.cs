

// Generated on 12/12/2013 16:56:50
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class AuthenticationTicketMessage : Message
    {
        public const uint Id = 110;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        public string lang;
        public string ticket;
        
        public AuthenticationTicketMessage()
        {
        }
        
        public AuthenticationTicketMessage(string lang, string ticket)
        {
            this.lang = lang;
            this.ticket = ticket;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUTF(lang);
            writer.WriteUTF(ticket);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            lang = reader.ReadUTF();
            ticket = reader.ReadUTF();
        }
        
        public override int GetSerializationSize()
        {
            return sizeof(short) + Encoding.UTF8.GetByteCount(lang) + sizeof(short) + Encoding.UTF8.GetByteCount(ticket);
        }
        
    }
    
}