using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main(string[] args)
    {

        if (args.Length != 3)
        {
            return;
        }

        try
        {
            string valuesPath = args[0];
            string testsPath = args[1];
            string reportPath = args[2];

            if (!File.Exists(valuesPath))
            {
                return;
            }
            if (!File.Exists(testsPath))
            {
                return;
            }

            string valuesJson = File.ReadAllText(valuesPath);
            JObject valuesObj = JObject.Parse(valuesJson);
            JArray valuesArray = (JArray)valuesObj["values"];

            Dictionary<int, string> valuesDict = new Dictionary<int, string>();
            foreach (JObject item in valuesArray)
            {
                int id = (int)item["id"];
                string value = (string)item["value"];
                valuesDict[id] = value;
            }

            string testsJson = File.ReadAllText(testsPath);
            JToken testsRoot = JToken.Parse(testsJson);

            FillValues(testsRoot, valuesDict);

            string reportJson = testsRoot.ToString(Formatting.Indented);
            File.WriteAllText(reportPath, reportJson);
        }
        catch
        {
            return;
        }
    }

    static void FillValues(JToken token, Dictionary<int, string> valuesDict)
    {
        if (token == null) return;

        if (token.Type == JTokenType.Object)
        {
            JObject obj = (JObject)token;

            if (obj.ContainsKey("id"))
            {
                int id = (int)obj["id"];
                if (valuesDict.ContainsKey(id))
                {
                    obj["value"] = valuesDict[id];
                }
            }

            foreach (JProperty property in obj.Properties())
            {
                FillValues(property.Value, valuesDict);
            }
        }
        else if (token.Type == JTokenType.Array)
        {
            JArray array = (JArray)token;
            foreach (JToken item in array)
            {
                FillValues(item, valuesDict);
            }
        }
    }
}