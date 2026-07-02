using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using static System.Runtime.InteropServices.JavaScript.JSType;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length < 3)
        {
            Console.WriteLine("Ошибка! Нужно передать 3 пути к файлам:");
            Console.WriteLine("Пример: Program.exe values.json tests.json report.json");
            return;
        }

        string valuesPath = "values.json";
        string testsPath = "tests.json";
        string reportPath = "report.json";

        // 1. Читаем values.json
        string valuesJson = File.ReadAllText(valuesPath);
        JObject valuesObj = JObject.Parse(valuesJson);
        JArray valuesArray = (JArray)valuesObj["values"];

        // Создаём словарь id -> value
        Dictionary<int, string> valuesDict = new Dictionary<int, string>();
        foreach (JObject item in valuesArray)
        {
            int id = (int)item["id"];
            string value = (string)item["value"];
            valuesDict[id] = value;
        }

        // 2. Читаем tests.json
        string testsJson = File.ReadAllText(testsPath);
        JToken testsRoot = JToken.Parse(testsJson);

        // 3. Заполняем значения
        FillValues(testsRoot, valuesDict);

        // 4. Записываем в report.json
        string reportJson = testsRoot.ToString(Newtonsoft.Json.Formatting.Indented);
        File.WriteAllText(reportPath, reportJson);

        Console.WriteLine("Готово! report.json создан.");
    }

    static void FillValues(JToken token, Dictionary<int, string> valuesDict)
    {
        if (token == null) return;

        // Если это объект
        if (token.Type == JTokenType.Object)
        {
            JObject obj = (JObject)token;

            // Проверяем, есть ли у объекта id
            if (obj.ContainsKey("id"))
            {
                int id = (int)obj["id"];
                if (valuesDict.ContainsKey(id))
                {
                    obj["value"] = valuesDict[id];
                }
            }

            // Обходим все свойства объекта
            foreach (JProperty property in obj.Properties())
            {
                FillValues(property.Value, valuesDict);
            }
        }
        // Если это массив
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