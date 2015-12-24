namespace Alpha.ViewLogs.Services
{
	using Models;
	using System.Collections.Generic;

	public interface IIPService
	{
		IList<IPRangeEntity> GetIPVN();
	}
}
