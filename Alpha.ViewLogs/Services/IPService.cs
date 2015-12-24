namespace Alpha.ViewLogs.Services
{
	using Fanex.Caching;
	using Models;
	using Repositories;
	using System;
	using System.Collections.Generic;

	public class IPService
	{
		private const string VietnamIPList = "Fanex.Razorbill.Module.Common.Constants.VNIPs";

		private readonly IIPRepository _repository;

		public IPService(IPRepository repository = null)
		{
			_repository = repository ?? new IPRepository();
		}

		public IList<IPRangeEntity> GetIPVN()
		{
			ICache cache = CacheService.CreateInstance(CacheType.MemoryCache);
			List<IPRangeEntity> ipsFromCaching = cache.Get<List<IPRangeEntity>>(VietnamIPList);
			if (ipsFromCaching != null && ipsFromCaching.Count > 0)
			{
				return ipsFromCaching;
			}

			var ipsFromDB = _repository.GetVNIPs();
			cache.Set<IList<IPRangeEntity>>(VietnamIPList, ipsFromDB, TimeSpan.FromHours(1.0));

			return ipsFromDB;
		}
	}
}