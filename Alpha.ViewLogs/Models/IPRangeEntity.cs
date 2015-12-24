namespace Alpha.ViewLogs.Models
{
	using System;

	[Serializable]
	public class IPRangeEntity
	{
		public double From { get; set; }

		public double To { get; set; }

		public bool IsManual { get; set; }
	}
}