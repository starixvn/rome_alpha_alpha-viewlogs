namespace Alpha.ViewLogs.Repositories
{
	using Models;
	using System.Collections.Generic;

	public interface IIPRepository
	{
		IList<IPRangeEntity> GetVNIPs();
	}
}
