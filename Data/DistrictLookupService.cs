using System;
using Newtonsoft.Json;

namespace MyWeatherApp.Data
{
    public class City
    {
        public string name { get; set; }
        public string adcode { get; set; }
    }

    public class DistrictLookupService
	{
		public async Task<List<City>> LookUpCitiesAsync(string keyword)
        {
            string key = "315f705f52615604772b35432034b150";

            var url = $"https://restapi.amap.com/v3/config/district?key={key}&keywords={keyword}&subdistrict=1&extensions=base";
            var client = new HttpClient();
            var res = await client.GetAsync(url);
            var json = await res.Content.ReadAsStringAsync();
            var districtResponse = JsonConvert.DeserializeObject<dynamic>(json);

            return getCities(districtResponse.districts);
        }

        private List<City> getCities(dynamic districts)
        {
            var cities = new List<City>();

            foreach (var district in districts)
            {
                switch ((string)district.level)
                {
                    case "city":
                        cities.Add(new City() { name = district.name, adcode = district.adcode });
                        break;
                    case "province":
                        cities.AddRange(getCities(district.districts));
                        break;
                }
            }

            return cities;
        }
    }
}

