

// Generated on 10/30/2016 16:20:31
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class JobCrafterDirectoryAddMessage : Message
    {
        public const uint Id = 5651;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        public Types.JobCrafterDirectoryListEntry listEntry;
        
        public JobCrafterDirectoryAddMessage()
        {
        }
        
        public JobCrafterDirectoryAddMessage(Types.JobCrafterDirectoryListEntry listEntry)
        {
            this.listEntry = listEntry;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            listEntry.Serialize(writer);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            listEntry = new Types.JobCrafterDirectoryListEntry();
            listEntry.Deserialize(reader);
        }
        
    }
    
}