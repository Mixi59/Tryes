// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'UpdateMountIntBoost.xml' the '09/12/2011 21:48:40'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Types
{
	public class UpdateMountIntBoost : UpdateMountBoost
	{
		public const uint Id = 357;
		public override short TypeId
		{
			get
			{
				return 357;
			}
		}
		
		public int value;
		
		public UpdateMountIntBoost()
		{
		}
		
		public UpdateMountIntBoost(sbyte type, int value)
			 : base(type)
		{
			this.value = value;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			base.Serialize(writer);
			writer.WriteInt(value);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			base.Deserialize(reader);
			value = reader.ReadInt();
		}
	}
}
