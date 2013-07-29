

// Generated on 07/29/2013 23:08:34
using System;
using System.Collections.Generic;
using System.Linq;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class PrismWorldInformationMessage : Message
    {
        public const uint Id = 5854;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        public int nbSubOwned;
        public int subTotal;
        public int maxSub;
        public IEnumerable<Types.PrismSubAreaInformation> subAreasInformation;
        public int nbConqsOwned;
        public int conqsTotal;
        public IEnumerable<Types.VillageConquestPrismInformation> conquetesInformation;
        
        public PrismWorldInformationMessage()
        {
        }
        
        public PrismWorldInformationMessage(int nbSubOwned, int subTotal, int maxSub, IEnumerable<Types.PrismSubAreaInformation> subAreasInformation, int nbConqsOwned, int conqsTotal, IEnumerable<Types.VillageConquestPrismInformation> conquetesInformation)
        {
            this.nbSubOwned = nbSubOwned;
            this.subTotal = subTotal;
            this.maxSub = maxSub;
            this.subAreasInformation = subAreasInformation;
            this.nbConqsOwned = nbConqsOwned;
            this.conqsTotal = conqsTotal;
            this.conquetesInformation = conquetesInformation;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteInt(nbSubOwned);
            writer.WriteInt(subTotal);
            writer.WriteInt(maxSub);
            writer.WriteUShort((ushort)subAreasInformation.Count());
            foreach (var entry in subAreasInformation)
            {
                 entry.Serialize(writer);
            }
            writer.WriteInt(nbConqsOwned);
            writer.WriteInt(conqsTotal);
            writer.WriteUShort((ushort)conquetesInformation.Count());
            foreach (var entry in conquetesInformation)
            {
                 entry.Serialize(writer);
            }
        }
        
        public override void Deserialize(IDataReader reader)
        {
            nbSubOwned = reader.ReadInt();
            if (nbSubOwned < 0)
                throw new Exception("Forbidden value on nbSubOwned = " + nbSubOwned + ", it doesn't respect the following condition : nbSubOwned < 0");
            subTotal = reader.ReadInt();
            if (subTotal < 0)
                throw new Exception("Forbidden value on subTotal = " + subTotal + ", it doesn't respect the following condition : subTotal < 0");
            maxSub = reader.ReadInt();
            if (maxSub < 0)
                throw new Exception("Forbidden value on maxSub = " + maxSub + ", it doesn't respect the following condition : maxSub < 0");
            var limit = reader.ReadUShort();
            subAreasInformation = new Types.PrismSubAreaInformation[limit];
            for (int i = 0; i < limit; i++)
            {
                 (subAreasInformation as Types.PrismSubAreaInformation[])[i] = new Types.PrismSubAreaInformation();
                 (subAreasInformation as Types.PrismSubAreaInformation[])[i].Deserialize(reader);
            }
            nbConqsOwned = reader.ReadInt();
            if (nbConqsOwned < 0)
                throw new Exception("Forbidden value on nbConqsOwned = " + nbConqsOwned + ", it doesn't respect the following condition : nbConqsOwned < 0");
            conqsTotal = reader.ReadInt();
            if (conqsTotal < 0)
                throw new Exception("Forbidden value on conqsTotal = " + conqsTotal + ", it doesn't respect the following condition : conqsTotal < 0");
            limit = reader.ReadUShort();
            conquetesInformation = new Types.VillageConquestPrismInformation[limit];
            for (int i = 0; i < limit; i++)
            {
                 (conquetesInformation as Types.VillageConquestPrismInformation[])[i] = new Types.VillageConquestPrismInformation();
                 (conquetesInformation as Types.VillageConquestPrismInformation[])[i].Deserialize(reader);
            }
        }
        
        public override int GetSerializationSize()
        {
            return sizeof(int) + sizeof(int) + sizeof(int) + sizeof(short) + subAreasInformation.Sum(x => x.GetSerializationSize()) + sizeof(int) + sizeof(int) + sizeof(short) + conquetesInformation.Sum(x => x.GetSerializationSize());
        }
        
    }
    
}