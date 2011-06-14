// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'OrientedObjectItemWithLookInRolePlay.xml' the '14/06/2011 11:32:47'
using System;
using Stump.BaseCore.Framework.IO;

namespace Stump.DofusProtocol.Types
{
	public class OrientedObjectItemWithLookInRolePlay : ObjectItemWithLookInRolePlay
	{
		public const uint Id = 199;
		public short TypeId
		{
			get
			{
				return 199;
			}
		}
		
		public byte direction;
		
		public OrientedObjectItemWithLookInRolePlay()
		{
		}
		
		public OrientedObjectItemWithLookInRolePlay(short cellId, short objectGID, Types.EntityLook entityLook, byte direction)
			 : base(cellId, objectGID, entityLook)
		{
			this.direction = direction;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			base.Serialize(writer);
			writer.WriteByte(direction);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			base.Deserialize(reader);
			direction = reader.ReadByte();
			if ( direction < 0 )
			{
				throw new Exception("Forbidden value on direction = " + direction + ", it doesn't respect the following condition : direction < 0");
			}
		}
	}
}
