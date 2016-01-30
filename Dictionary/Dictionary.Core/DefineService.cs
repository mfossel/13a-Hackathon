using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using unirest_net;
using unirest_net.http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Net;
using Dictionary.Core.Domains;

namespace Dictionary.Core
{
    public class DefineService
    {
        public static string YodaInput = "";
        public static string GetDef(string word)
        {
            string def;
            string definition = "";
            WebRequest request = WebRequest.Create($"https://montanaflynn-dictionary.p.mashape.com/define?word={word}");
            request.Headers.Add("X-Mashape-Key", "s9SQZi4fYCmshpkI3i0IYEYg4m8hp13rW2DjsnALWln7XkT8Ez");

            WebResponse response = request.GetResponse();
            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                def = streamReader.ReadToEnd();
            }
            var o = JObject.Parse(def);
            string s = o["definitions"].ToString();

            List<Definition> deflist = new List<Definition>();
            deflist = JsonConvert.DeserializeObject<List<Definition>>(s);

            for (int i = 0; i < 4; i++)
            {
                definition += "Definition: " + deflist[i].text + "\n\n";
                definition += "Attribution: " + deflist[i].attribution + "\n\n";
            }


            return definition;

        }

        public static string GetUrbanDef(string word)
        {
            string def;
            string definition = "";
            WebRequest request = WebRequest.Create($"https://mashape-community-urban-dictionary.p.mashape.com/define?term={word}");
            request.Headers.Add("X-Mashape-Key", "s9SQZi4fYCmshpkI3i0IYEYg4m8hp13rW2DjsnALWln7XkT8Ez");

            WebResponse response = request.GetResponse();
            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                def = streamReader.ReadToEnd();
            }
            var o = JObject.Parse(def);
            string s = o["list"].ToString();

            List<UrbanDef> deflist = new List<UrbanDef>();
            deflist = JsonConvert.DeserializeObject<List<UrbanDef>>(s);

            YodaInput = deflist[0].example;
            for (int i = 0; i < 4; i++)
            {
                definition += "Definition: " + deflist[i].definition + "\n\n";
                definition += "Example: " + deflist[i].example + "\n\n";
            }


            return definition;

        }

        public static string YodaTalk(string sent)
        {
            string Yoda = "";

            WebRequest request = WebRequest.Create($"https://yoda.p.mashape.com/yoda?sentence={sent}");
            request.Headers.Add("X-Mashape-Key", "s9SQZi4fYCmshpkI3i0IYEYg4m8hp13rW2DjsnALWln7XkT8Ez");

            WebResponse response = request.GetResponse();
            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                Yoda = streamReader.ReadToEnd();
            }
            return Yoda;
        }
    }
}