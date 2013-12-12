

// Generated on 12/12/2013 16:57:34
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Types
{
    public class Preset
    {
        public const short Id = 355;
        public virtual short TypeId
        {
            get { return Id; }
        }
        
        public sbyte presetId;
        public sbyte symbolId;
        public bool mount;
        public IEnumerable<Types.PresetItem> objects;
        
        public Preset()
        {
        }
        
        public Preset(sbyte presetId, sbyte symbolId, bool mount, IEnumerable<Types.PresetItem> objects)
        {
            this.presetId = presetId;
            this.symbolId = symbolId;
            this.mount = mount;
            this.objects = objects;
        }
        
        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteSByte(presetId);
            writer.WriteSByte(symbolId);
            writer.WriteBoolean(mount);
            writer.WriteUShort((ushort)objects.Count());
            foreach (var entry in objects)
            {
                 entry.Serialize(writer);
            }
        }
        
        public virtual void Deserialize(IDataReader reader)
        {
            presetId = reader.ReadSByte();
            if (presetId < 0)
                throw new Exception("Forbidden value on presetId = " + presetId + ", it doesn't respect the following condition : presetId < 0");
            symbolId = reader.ReadSByte();
            if (symbolId < 0)
                throw new Exception("Forbidden value on symbolId = " + symbolId + ", it doesn't respect the following condition : symbolId < 0");
            mount = reader.ReadBoolean();
            var limit = reader.ReadUShort();
            objects = new Types.PresetItem[limit];
            for (int i = 0; i < limit; i++)
            {
                 (objects as Types.PresetItem[])[i] = new Types.PresetItem();
                 (objects as Types.PresetItem[])[i].Deserialize(reader);
            }
        }
        
        public virtual int GetSerializationSize()
        {
            return sizeof(sbyte) + sizeof(sbyte) + sizeof(bool) + sizeof(short) + objects.Sum(x => x.GetSerializationSize());
        }
        
    }
    
}