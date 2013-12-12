

// Generated on 12/12/2013 16:57:05
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class PartyInvitationDungeonMessage : PartyInvitationMessage
    {
        public const uint Id = 6244;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        public short dungeonId;
        
        public PartyInvitationDungeonMessage()
        {
        }
        
        public PartyInvitationDungeonMessage(int partyId, sbyte partyType, sbyte maxParticipants, int fromId, string fromName, int toId, short dungeonId)
         : base(partyId, partyType, maxParticipants, fromId, fromName, toId)
        {
            this.dungeonId = dungeonId;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteShort(dungeonId);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            dungeonId = reader.ReadShort();
            if (dungeonId < 0)
                throw new Exception("Forbidden value on dungeonId = " + dungeonId + ", it doesn't respect the following condition : dungeonId < 0");
        }
        
        public override int GetSerializationSize()
        {
            return base.GetSerializationSize() + sizeof(short);
        }
        
    }
    
}