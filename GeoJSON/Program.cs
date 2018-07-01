using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GeoJSON
{
    class Program
    {
        static void Main(string[] args)
        {
            var FileName = "Path/To/Your/LatLong/File";
            var FileName1 = "Path/To/Your/Codes/File";
            string[] lines;
            string[] codes;
            string[] pairs;
            string GeoJson = "";
            string latlongs = "";
            string last_line;
            string last;
            string comma;
            string comma1;
            int counter = 0;
            List<string> elements = new List<string>();
            List<string> codeList = new List<string>();
            string text;
            string text_codes;
            using (StreamReader sr1 = new StreamReader(FileName1, Encoding.Default))
            {
                text_codes = sr1.ReadToEnd();
            }
            codes = text_codes.Split(',');
            foreach (var c in codes)
            {
                codeList.Add(c);
            }
            using (StreamReader sr = new StreamReader(FileName, Encoding.Default))
            {
                text = sr.ReadToEnd();
            }
            lines = text.Split(';');
            last_line = lines.Last();
            GeoJson = "{\"type\": \"FeatureCollection\",\"features\": [";
            foreach (var s in lines)
            {
                GeoJson += "{ \"type\": \"Feature\", \"geometry\": { \"type\": \"Polygon\", \"coordinates\": [ [";
               pairs = s.Split(',');
               foreach(var e in pairs)
                {
                    elements.Add(e);
                }
                last = elements.Last();
                foreach (var el in elements)
                {
                    comma = el == last ? "" : ",";
                    latlongs = el.TrimStart(' ');
                    latlongs = latlongs.Replace(" ", ", ");
                    GeoJson +=  "[" + latlongs + "]" + comma;
                }
                comma1 = s == last_line ? "" : ",";
                GeoJson += "] ] }, \"properties\": { \"CODE\": \"" + codeList[counter].ToString().Replace("\r", "").Replace("\n", "") + "\" } }" + comma1;
                counter++;
                elements.Clear();
            }

            GeoJson += "] }";


            System.IO.File.WriteAllText(@"Your/Output/GeoJson/File", GeoJson);
        }
    }
}
