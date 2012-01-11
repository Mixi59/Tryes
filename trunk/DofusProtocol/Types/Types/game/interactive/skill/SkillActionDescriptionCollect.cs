// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'SkillActionDescriptionCollect.xml' the '09/12/2011 21:48:40'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Types
{
	public class SkillActionDescriptionCollect : SkillActionDescriptionTimed
	{
		public const uint Id = 99;
		public override short TypeId
		{
			get
			{
				return 99;
			}
		}
		
		public short min;
		public short max;
		
		public SkillActionDescriptionCollect()
		{
		}
		
		public SkillActionDescriptionCollect(short skillId, byte time, short min, short max)
			 : base(skillId, time)
		{
			this.min = min;
			this.max = max;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			base.Serialize(writer);
			writer.WriteShort(min);
			writer.WriteShort(max);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			base.Deserialize(reader);
			min = reader.ReadShort();
			if ( min < 0 )
			{
				throw new Exception("Forbidden value on min = " + min + ", it doesn't respect the following condition : min < 0");
			}
			max = reader.ReadShort();
			if ( max < 0 )
			{
				throw new Exception("Forbidden value on max = " + max + ", it doesn't respect the following condition : max < 0");
			}
		}
	}
}
