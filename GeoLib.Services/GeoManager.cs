using GeoLib.Contracts;
using GeoLib.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoLib.Services
{
	public class GeoManager : IGeoService
	{
		IZipCodeRepository zipCodeRepository = null;
		IStateRepository stateRepository = null;

		public GeoManager()
		{
			// Empty.
		}

		public GeoManager(IZipCodeRepository zipCodeRepository)
		{
			this.zipCodeRepository = zipCodeRepository;
		}

		public GeoManager(IStateRepository stateRepository)
		{
			this.stateRepository = stateRepository;
		}

		public GeoManager(IZipCodeRepository zipCodeRepository, IStateRepository stateRepository)
		{
			this.zipCodeRepository = zipCodeRepository;
			this.stateRepository = stateRepository;
		}

		public IEnumerable<string> GetStates(bool primaryOnly)
		{
			List<string> stateData = new List<string>(10);

			IStateRepository stateRepository = this.stateRepository ?? new StateRepository();

			IEnumerable<State> states = stateRepository.Get(primaryOnly);

			if(states != null)
			{
				foreach (State state in states)
				{
					stateData.Add(state.Abbreviation);
				}
			}

			return stateData;
		}

		public ZipCodeData GetZipInfo(string zip)
		{
			ZipCodeData zipCodeData = null;

			IZipCodeRepository zipCodeRepozitory = this.zipCodeRepository ?? new ZipCodeRepository();

			ZipCode zipCodeEntity = zipCodeRepozitory.GetByZip(zip);

			if(zipCodeEntity != null)
			{
				zipCodeData = new ZipCodeData()
				{
					City = zipCodeEntity.City,
					State = zipCodeEntity.State.Abbreviation,
					ZipCode = zipCodeEntity.Zip
				};
			}

			return zipCodeData;
		}

		public IEnumerable<ZipCodeData> GetZips(string state)
		{
			List<ZipCodeData> zipCodeData = new List<ZipCodeData>(10);

			IZipCodeRepository zipCodeRepository = this.zipCodeRepository ?? new ZipCodeRepository();

			IEnumerable<ZipCode> zips = zipCodeRepository.GetByState(state);

			if(zips != null)
			{
				foreach (ZipCode zipCode in zips)
				{
					zipCodeData.Add(new ZipCodeData()
					{
						City = zipCode.City,
						State = zipCode.State.Abbreviation,
						ZipCode = zipCode.Zip
					});
				}
			}

			return zipCodeData;
		}

		public IEnumerable<ZipCodeData> GetZips(string zip, int range)
		{
			List<ZipCodeData> zipCodeData = new List<ZipCodeData>(10);

			IZipCodeRepository zipCodeRepository = this.zipCodeRepository ?? new ZipCodeRepository();

			ZipCode zipEntity = zipCodeRepository.GetByZip(zip);
			IEnumerable<ZipCode> zips = zipCodeRepository.GetZipsForRange(zipEntity, range);

			if (zips != null)
			{
				foreach (ZipCode zipCode in zips)
				{
					zipCodeData.Add(new ZipCodeData()
					{
						City = zipCode.City,
						State = zipCode.State.Abbreviation,
						ZipCode = zipCode.Zip
					});
				}
			}

			return zipCodeData;
		}
	}
}
