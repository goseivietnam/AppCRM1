using Newtonsoft.Json;
using System.Collections.Generic;
using System;

namespace AppCRM.Models
{
    public class InitFilter
    {
        [JsonProperty("PositionDLL")]
        public List<PickerItem> PositionDLL { get;set;}
        [JsonProperty("QualificationDLL")]
        public List<PickerItem> QualificationDLL { get;set;}
        [JsonProperty("JobTypeDLL")]
        public List<PickerItem> JobTypeDLL { get;set;}
        [JsonProperty("LocationDDL")]
        public List<PickerItem> LocationDDL { get;set;}
        [JsonProperty("ClassificationDLL")]
        public List<PickerItem> ClassificationDLL { get;set;}
        [JsonProperty("TicketsDLL")]
        public List<PickerItem> TicketsDLL { get;set;}
        [JsonProperty("SkillsDLL")]
        public List<PickerItem> SkillsDLL { get;set;}
    }
}
