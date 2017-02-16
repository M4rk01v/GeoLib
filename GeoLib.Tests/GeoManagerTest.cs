using GeoLib.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using GeoLib.Contracts;
using GeoLib.Services;

namespace GeoLib.Tests
{
	[TestClass]
	public class GeoManagerTest
	{
		[TestMethod]
		public void GetZipInfoTest()
		{
			Mock<IZipCodeRepository> mockZipCodeRepository = new Mock<IZipCodeRepository>();

			ZipCode zipCode = new ZipCode()
			{
				City = "NOVI SAD",
				State = new State() { Abbreviation = "NS" },
				Zip = "21000"
			};

			mockZipCodeRepository.Setup(obj => obj.GetByZip("21000")).Returns(zipCode);

			IGeoService geoService = new GeoManager(mockZipCodeRepository.Object);

			ZipCodeData data = geoService.GetZipInfo("21000");

			Assert.IsTrue(data.City.ToUpper() == "NOVI SAD");
			Assert.IsTrue(data.State == "NS");
		}

		[TestMethod]
		public void GetStatesTest()
		{
			Mock<IStateRepository> mockStateRepository = new Mock<IStateRepository>();

			IEnumerable<State> stateData = new List<State>(1)
			{
				new State()
				{
					IsPrimaryState = true,
					Abbreviation = "NS"
				}
			};

			mockStateRepository.Setup(obj => obj.Get(true)).Returns(stateData);

			IGeoService geoService = new GeoManager(mockStateRepository.Object);

			IEnumerable<string> states = geoService.GetStates(true);

			Assert.IsTrue(states.First() == "NS");
		}
	}
}
