

// Generated on 12/12/2013 16:57:00
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class GameRolePlayDelayedActionMessage : Message
    {
        public const uint Id = 6153;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        public int delayedCharacterId;
        public sbyte delayTypeId;
        public double delayEndTime;
        
        public GameRolePlayDelayedActionMessage()
        {
        }
        
        public GameRolePlayDelayedActionMessage(int delayedCharacterId, sbyte delayTypeId, double delayEndTime)
        {
            this.delayedCharacterId = delayedCharacterId;
            this.delayTypeId = delayTypeId;
            this.delayEndTime = delayEndTime;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteInt(delayedCharacterId);
            writer.WriteSByte(delayTypeId);
            writer.WriteDouble(delayEndTime);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            delayedCharacterId = reader.ReadInt();
            delayTypeId = reader.ReadSByte();
            if (delayTypeId < 0)
                throw new Exception("Forbidden value on delayTypeId = " + delayTypeId + ", it doesn't respect the following condition : delayTypeId < 0");
            delayEndTime = reader.ReadDouble();
            if (delayEndTime < 0)
                throw new Exception("Forbidden value on delayEndTime = " + delayEndTime + ", it doesn't respect the following condition : delayEndTime < 0");
        }
        
        public override int GetSerializationSize()
        {
            return sizeof(int) + sizeof(sbyte) + sizeof(double);
        }
        
    }
    
}