using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using Skyfield;
using Skyfield.Almanac;
using Skyfield.Api;
using Skyfield.Positions;

namespace SatelliteVisualization
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Step 1: Retrieve satellite data from the API
                string satelliteDataApiUrl = "API_URL_HERE";
                string satelliteDataJson = SendGetRequest(satelliteDataApiUrl);
                var satelliteData = JsonConvert.DeserializeObject<List<SatelliteData>>(satelliteDataJson);

                // Step 2: Parse TLE data using Skyfield
                var tleData = new List<string[]>();
                foreach (var satellite in satelliteData)
                {
                    string line1 = satellite.tle_line1;
                    string line2 = satellite.tle_line2;
                    tleData.Add(new string[] { line1, line2 });
                }

                // Step 3: Visualize satellite orbits in 3D
                var loader = new Loader(@"path_to_data_directory");
                var ephemeris = loader.Load(@"de421.bsp");
                var satellites = loader.LoadTleFile(tleData);

                var fig = plt.figure();
                var ax = fig.add_subplot(111, projection='3d');

                foreach (var satellite in satellites)
                {
                    // Calculate the satellite's position over time
                    var ts = loader.MakeTimescale();
                    var t = ts.Utc(2023, 7, 11, 0, Enumerable.Range(0, 3600).Select(x => x * 60));
                    var geocentric = satellite.At(t);
                    var subpoint = geocentric.Subpoint();

                    // Extract latitude, longitude, and altitude
                    var latitude = subpoint.Latitude.Degrees;
                    var longitude = subpoint.Longitude.Degrees;
                    var altitude = subpoint.Elevation.Km;

                    // Plot the satellite's trajectory in 3D
                    ax.plot(longitude, latitude, altitude);
                }

                ax.set_xlabel("Longitude");
                ax.set_ylabel("Latitude");
                ax.set_zlabel("Altitude (km)");

                // Step 4: Map satellites to countries using the satellite database API
                string satelliteDbApiUrl = "SATELLITE_DB_API_URL_HERE";
                string satelliteDbJson = SendGetRequest(satelliteDbApiUrl);
                var satelliteDb = JsonConvert.DeserializeObject<List<SatelliteDbEntry>>(satelliteDbJson);

                // Mapping satellite names to countries
                var satelliteCountryMap = new Dictionary<string, string>();
                foreach (var satellite in satelliteData)
                {
                    string name = satellite.name;
                    foreach (var entry in satelliteDb)
                    {
                        if (entry.name == name)
                        {
                            string country = entry.country;
                            satelliteCountryMap[name] = country;
                            break;
                        }
                    }
                }

                // Printing satellite information
                foreach (var satellite in satelliteData)
                {
                    string name = satellite.name;
                    double angle = satellite.angle;
                    string country = satelliteCountryMap.GetValueOrDefault(name, "Unknown");

                    Console.WriteLine($"Satellite Name: {name}");
                    Console.WriteLine($"Orbital Angle: {angle} degrees");
                    Console.WriteLine($"Country: {country}");
                    Console.WriteLine();
                }

                // Show the 3D plot
                plt.show();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
        }

        private static string SendGetRequest(string url)
        {
            string responseString = string.Empty;

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        responseString = reader.ReadToEnd();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error in SendGetRequest: {e.Message}");
            }

            return responseString;
        }
    }

    public class SatelliteData
    {
        public string name { get; set; }
        public string tle_line1 { get; set; }
        public string tle_line2 { get; set; }
        public double angle { get; set; }
    }

    public class SatelliteDbEntry
    {
        public string name { get; set; }
        public string country { get; set; }
    }
}
