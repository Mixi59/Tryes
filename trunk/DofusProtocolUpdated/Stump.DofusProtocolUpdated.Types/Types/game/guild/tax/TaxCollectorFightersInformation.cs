

// Generated on 12/12/2013 16:57:34
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Types
{
    public class TaxCollectorFightersInformation
    {
        public const short Id = 169;
        public virtual short TypeId
        {
            get { return Id; }
        }
        
        public int collectorId;
        public IEnumerable<Types.CharacterMinimalPlusLookInformations> allyCharactersInformations;
        public IEnumerable<Types.CharacterMinimalPlusLookInformations> enemyCharactersInformations;
        
        public TaxCollectorFightersInformation()
        {
        }
        
        public TaxCollectorFightersInformation(int collectorId, IEnumerable<Types.CharacterMinimalPlusLookInformations> allyCharactersInformations, IEnumerable<Types.CharacterMinimalPlusLookInformations> enemyCharactersInformations)
        {
            this.collectorId = collectorId;
            this.allyCharactersInformations = allyCharactersInformations;
            this.enemyCharactersInformations = enemyCharactersInformations;
        }
        
        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteInt(collectorId);
            writer.WriteUShort((ushort)allyCharactersInformations.Count());
            foreach (var entry in allyCharactersInformations)
            {
                 writer.WriteShort(entry.TypeId);
                 entry.Serialize(writer);
            }
            writer.WriteUShort((ushort)enemyCharactersInformations.Count());
            foreach (var entry in enemyCharactersInformations)
            {
                 writer.WriteShort(entry.TypeId);
                 entry.Serialize(writer);
            }
        }
        
        public virtual void Deserialize(IDataReader reader)
        {
            collectorId = reader.ReadInt();
            var limit = reader.ReadUShort();
            allyCharactersInformations = new Types.CharacterMinimalPlusLookInformations[limit];
            for (int i = 0; i < limit; i++)
            {
                 (allyCharactersInformations as Types.CharacterMinimalPlusLookInformations[])[i] = Types.ProtocolTypeManager.GetInstance<Types.CharacterMinimalPlusLookInformations>(reader.ReadShort());
                 (allyCharactersInformations as Types.CharacterMinimalPlusLookInformations[])[i].Deserialize(reader);
            }
            limit = reader.ReadUShort();
            enemyCharactersInformations = new Types.CharacterMinimalPlusLookInformations[limit];
            for (int i = 0; i < limit; i++)
            {
                 (enemyCharactersInformations as Types.CharacterMinimalPlusLookInformations[])[i] = Types.ProtocolTypeManager.GetInstance<Types.CharacterMinimalPlusLookInformations>(reader.ReadShort());
                 (enemyCharactersInformations as Types.CharacterMinimalPlusLookInformations[])[i].Deserialize(reader);
            }
        }
        
        public virtual int GetSerializationSize()
        {
            return sizeof(int) + sizeof(short) + allyCharactersInformations.Sum(x => sizeof(short) + x.GetSerializationSize()) + sizeof(short) + enemyCharactersInformations.Sum(x => sizeof(short) + x.GetSerializationSize());
        }
        
    }
    
}