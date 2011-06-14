using System;

namespace Stump.DofusProtocol.D2oClasses
{
	[D2OClass("NpcMessages")]
	public class NpcMessage
	{
		private const String MODULE = "NpcMessages";
		public int id;
		public uint messageId;
		public String messageParams;
	}
}
