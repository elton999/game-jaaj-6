using System;
using System.IO;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace game_jaaj_6.Cutscene
{
    // <auto-generated />
    //
    // To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
    //
    //    using QuickType;
    //
    //    var castledb = Castledb.FromJson(jsonString);

    public class Dialogue
    {
        string path = @"Content/dialogue.cdb";
        public Castledb Castledb;
        
        public Dialogue(){
            if(File.Exists(path))
            {
                Console.WriteLine("reading dialogue file");
                Castledb = Castledb.FromJson(File.ReadAllText(path));
            }
        }

    }

    public partial class Castledb
    {
        [JsonProperty("sheets")]
        public Sheet[] Sheets { get; set; }

        [JsonProperty("customTypes")]
        public object[] CustomTypes { get; set; }

        [JsonProperty("compress")]
        public bool Compress { get; set; }
    }

    public partial class Sheet
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("columns")]
        public Column[] Columns { get; set; }

        [JsonProperty("lines")]
        public Line[] Lines { get; set; }

        [JsonProperty("separators")]
        public object[] Separators { get; set; }

        [JsonProperty("props")]
        public Props Props { get; set; }
    }

    public partial class Column
    {
        [JsonProperty("typeStr")]
        public string TypeStr { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public partial class Line
    {
        [JsonProperty("text", NullValueHandling = NullValueHandling.Ignore)]
        public string Text { get; set; }

        [JsonProperty("character", NullValueHandling = NullValueHandling.Ignore)]
        public string Character { get; set; }

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }
    }

    public partial class Props
    {
    }

    public partial class Castledb
    {
        public static Castledb FromJson(string json) => JsonConvert.DeserializeObject<Castledb>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this Castledb self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}