using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoleTakingSummaryFromAgenda.Domain.Entities
{
    public class RegularMeeting
    {
        public string Times { get; set; }
        public string Date { get; set; }
        public string Room {get;set;}
        public string Theme { get; set; }
        public string Toastmaster { get; set; }
        public string SAA { get; set; }
        public string General_Evaluator { get; set; }
        public string Timer { get; set; }
        public string Grammarian { get; set; }
        public string Ah_Counter { get; set; }
        public string Table_Topic_Master { get; set; }
        public string Table_Topic_Evaluator { get; set; }
        public string Prepared_Speaker_1 { get; set; }
        public string Prepared_Speaker_2 { get; set; }
        public string Speech_Evaluator_1 { get; set; }
        public string Speech_Evaluator_2 { get; set; }
        public string Quiz_Master { get; set; }
        public string Photographer { get; set; }
        public string Video_Taker { get; set; }
        public string Meeting_Minutes_Taker { get; set; }
    }
}
