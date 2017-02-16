using GeoLib.Core;
using System.Collections.Generic;

namespace GeoLib.Data
{
	public interface IZipCodeRepository : IDataRepository<ZipCode>
	{
		ZipCode GetByZip(string zip);

		IEnumerable<ZipCode> GetByState(string state);

		IEnumerable<ZipCode> GetZipsForRange(ZipCode zip, int range);
	}
}