using Newtonsoft.Json;
using System.Collections.Generic;
using System;

namespace AppCRM.Models
{
    public class InitFilter
    {
        [JsonProperty("PositionDLL")]
        public List<LookupItem> PositionDLL { get;set;}
        [JsonProperty("QualificationDLL")]
        public List<LookupItem> QualificationDLL { get;set;}
        [JsonProperty("JobTypeDLL")]
        public List<LookupItem> JobTypeDLL { get;set;}
        [JsonProperty("LocationDDL")]
        public List<LookupItem> LocationDDL { get;set;}
        [JsonProperty("ClassificationDLL")]
        public List<LookupItem> ClassificationDLL { get;set;}
        [JsonProperty("TicketsDLL")]
        public List<LookupItem> TicketsDLL { get;set;}
        [JsonProperty("SkillsDLL")]
        public List<LookupItem> SkillsDLL { get;set;}
    }
}
