using System;

namespace Stump.DofusProtocol.D2oClasses
{
	[D2OClass("TaxCollectorNames")]
	public class TaxCollectorName
	{
		private const String MODULE = "TaxCollectorNames";
		public int id;
		public uint nameId;
	}
}
