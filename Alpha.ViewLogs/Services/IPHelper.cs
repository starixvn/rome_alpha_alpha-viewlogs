namespace Alpha.ViewLogs.Services
{
	using Models;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text.RegularExpressions;

	public static class IPHelper
	{
		public static bool IsVnIp(string ip)
		{
			if (string.IsNullOrEmpty(ip))
			{
				return false;
			}

			double ipNumber = ConvertIpToNumber(ip);
			List<IPRangeEntity> ips = (List<IPRangeEntity>)new IPService().GetIPVN();
			if (ips == null)
			{
				return false;
			}

			var ipRange = from q in ips where q.From <= ipNumber && q.To >= ipNumber select q;

			if (ipRange.Any())
			{
				return true;
			}

			return false;
		}

		private static double ConvertIpToNumber(string ip)
		{
			double num = 0.0;
			if (IsValidIp(ip))
			{
				string[] strArray = ip.Split(new char[] { '.' });
				num = (((double.Parse(strArray[0]) * 16777216.0) + (double.Parse(strArray[1]) * 65536.0)) + (double.Parse(strArray[2]) * 256.0)) + double.Parse(strArray[3]);
			}

			return num;
		}

		private static bool IsValidIp(string ip)
		{
			if (string.IsNullOrEmpty(ip))
			{
				return false;
			}

			var regexCharacters =
				@"(?<First>2[0-4]\d|25[0-5]|[01]?\d\d?)\.(?<Second>2[0-4]\d|25[0-5]|[01]?\d\d?)\.(?<Third>2[0-4]\d|25[0-5]|[01]?\d\d?)\.(?<Fourth>2[0-4]\d|25[0-5]|[01]?\d\d?)";
			Regex regex = new Regex(regexCharacters);

			return regex.IsMatch(ip, 0);
		}
	}
}