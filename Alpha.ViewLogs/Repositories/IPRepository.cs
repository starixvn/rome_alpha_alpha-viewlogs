namespace Alpha.ViewLogs.Repositories
{
	using Fanex.Common;
	using Fanex.Data;
	using Models;
	using System.Collections.Generic;
	using System.Data;

	public class IPRepository : IIPRepository
	{
		public IList<IPRangeEntity> GetVNIPs()
		{
			var ips = new List<IPRangeEntity>();
			using (ObjectDb objDb = new ObjectDb("GetIPVN"))
			{
				IDataReader reader = objDb.ExecuteReader();
				while (reader.Read())
				{
					ips.Add(new IPRangeEntity()
					{
						From = Converter.To<double>(reader["ipfrom"]),
						To = Converter.To<double>(reader["ipto"]),
						IsManual = Converter.ToBool(reader["ismanual"])
					});
				}
			}

			return ips;
		}
	}
}