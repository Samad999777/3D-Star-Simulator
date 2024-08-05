using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Csvreader : MonoBehaviour
{
    // Struct to hold the data for each star
    public struct Star
    {
        public string star_id;
        public string star_type;
        public float radius;
        public float mass;
        public float temperature;
        public string color;
    }

    void Start()
    {
        Dictionary<string, Star> stars = ReadCSV("stars");
        foreach (var star in stars)
        {
            /*Debug.Log("Star ID: " + star.Value.star_id + ", Type: " + star.Value.star_type +
                      ", Radius: " + star.Value.radius + ", Mass: " + star.Value.mass +
                      ", Temperature: " + star.Value.temperature + ", Color: " + star.Value.color);*/
        }
    }

    public Dictionary<string, Star> ReadCSV(string fileName)
    {
        Dictionary<string, Star> stars = new Dictionary<string, Star>();

        // Load the CSV file from Resources folder
        TextAsset csvData = Resources.Load<TextAsset>(fileName);

        if (csvData == null)
        {
            Debug.LogError("Could not find the CSV file in Resources.");
            return stars;
        }

        using (StringReader reader = new StringReader(csvData.text))
        {
            bool firstLine = true;
            string line;

            while ((line = reader.ReadLine()) != null)
            {
                // Skip the header line
                if (firstLine)
                {
                    firstLine = false;
                    continue;
                }

                string[] fields = line.Split(',');

                if (fields.Length >= 6)
                {
                    Star star = new Star();
                    star.star_id = fields[0].Trim();
                    star.star_type = fields[1].Trim();
                    star.radius = float.Parse(fields[2].Trim());
                    star.mass = float.Parse(fields[3].Trim());
                    star.temperature = float.Parse(fields[4].Trim());
                    star.color = fields[5].Trim();

                    stars.Add(star.star_id, star);
                }
            }
        }

        return stars;
    }
}
