

// Generated on 10/28/2014 16:38:03
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Types
{
    public class GameRolePlayGroupMonsterWaveInformations : GameRolePlayGroupMonsterInformations
    {
        public const short Id = 464;
        public override short TypeId
        {
            get { return Id; }
        }
        
        public uint nbWaves;
        public IEnumerable<Types.GroupMonsterStaticInformations> alternatives;
        
        public GameRolePlayGroupMonsterWaveInformations()
        {
        }
        
        public GameRolePlayGroupMonsterWaveInformations(int contextualId, Types.EntityLook look, Types.EntityDispositionInformations disposition, bool keyRingBonus, bool hasHardcoreDrop, bool hasAVARewardToken, Types.GroupMonsterStaticInformations staticInfos, short ageBonus, sbyte lootShare, sbyte alignmentSide, uint nbWaves, IEnumerable<Types.GroupMonsterStaticInformations> alternatives)
         : base(contextualId, look, disposition, keyRingBonus, hasHardcoreDrop, hasAVARewardToken, staticInfos, ageBonus, lootShare, alignmentSide)
        {
            this.nbWaves = nbWaves;
            this.alternatives = alternatives;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteUInt(nbWaves);
            var alternatives_before = writer.Position;
            var alternatives_count = 0;
            writer.WriteUShort(0);
            foreach (var entry in alternatives)
            {
                 writer.WriteShort(entry.TypeId);
                 entry.Serialize(writer);
                 alternatives_count++;
            }
            var alternatives_after = writer.Position;
            writer.Seek((int)alternatives_before);
            writer.WriteUShort((ushort)alternatives_count);
            writer.Seek((int)alternatives_after);

        }
        
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            nbWaves = reader.ReadUInt();
            if (nbWaves < 0 || nbWaves > 4.294967295E9)
                throw new Exception("Forbidden value on nbWaves = " + nbWaves + ", it doesn't respect the following condition : nbWaves < 0 || nbWaves > 4.294967295E9");
            var limit = reader.ReadUShort();
            var alternatives_ = new Types.GroupMonsterStaticInformations[limit];
            for (int i = 0; i < limit; i++)
            {
                 alternatives_[i] = Types.ProtocolTypeManager.GetInstance<Types.GroupMonsterStaticInformations>(reader.ReadShort());
                 alternatives_[i].Deserialize(reader);
            }
            alternatives = alternatives_;
        }
        
        public override int GetSerializationSize()
        {
            return base.GetSerializationSize() + sizeof(uint) + sizeof(short) + alternatives.Sum(x => sizeof(short) + x.GetSerializationSize());
        }
        
    }
    
}