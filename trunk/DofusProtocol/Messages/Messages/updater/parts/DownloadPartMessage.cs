

// Generated on 07/29/2013 23:08:38
using System;
using System.Collections.Generic;
using System.Linq;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class DownloadPartMessage : Message
    {
        public const uint Id = 1503;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        public string id;
        
        public DownloadPartMessage()
        {
        }
        
        public DownloadPartMessage(string id)
        {
            this.id = id;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUTF(id);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            id = reader.ReadUTF();
        }
        
        public override int GetSerializationSize()
        {
            return sizeof(short) + id.Length;
        }
        
    }
    
}