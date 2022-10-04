using System;
using Newtonsoft.Json;

namespace MyWeatherApp.Data
{
    public class DistrictResponse
    {
        public string status { get; set; }
        public string info { get; set; }
        public string infocode { get; set; }
        public string count { get; set; }
        public Suggestion suggestion { get; set; }
        public List<Province> districts { get; set; }
    }

    public class Suggestion
    {
        public List<string> keywords;
        public List<string> cities;
    }

    public class Province
    {
        public List<string> citycode { get; set; }
        public string adcode { get; set; }
        public string name { get; set; }
        public string center { get; set; }
        public string level { get; set; }
        public List<District> districts { get; set; }
    }

    public class District
    {
        public string citycode { get; set; }
        public string adcode { get; set; }
        public string name { get; set; }
        public string center { get; set; }
        public string level { get; set; }
        public List<District> districts { get; set; }
    }

    public class DistrictLookupService
	{
		public async Task<DistrictResponse> LookUpDistricts(string keyword)
        {
            string key = "315f705f52615604772b35432034b150";

            var url = $"https://restapi.amap.com/v3/config/district?key={key}&keywords={keyword}&subdistrict=1&extensions=base";
            var client = new HttpClient();
            var res = await client.GetAsync(url);
            var json = await res.Content.ReadAsStringAsync();
            var districtResponse = JsonConvert.DeserializeObject<DistrictResponse>(json);

            return districtResponse;
        }
	}
}

