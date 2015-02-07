using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using RoleTakingSummaryFromAgenda.Utilities;
using RoleTakingSummaryFromAgenda.Domain.Entities;
using RoleTakingSummaryFromAgenda.Domain.Abstract;

namespace RoleTakingSummaryFromAgenda.Domain.Concrete
{
    public class AATMCMeeting: IToastmasterable
    {
        private string agenda;
        private Dictionary<string, string> checklist;
        

        public AATMCMeeting(string agendaFileName, string list)
        {
            agenda = agendaFileName;
            checklist = list.GetCheckList();

        }

        public RegularMeeting GetRegularMeetingInfo()
        {
            var meeting = new RegularMeeting();
            using (var excelObject = new ExcelObject(agenda))
            {
                if(Regex.IsMatch(agenda, "old"))
                {
                    meeting.Date = excelObject.At(3, 2).Unify(checklist);
                    meeting.Room = excelObject.At(4, 2).Unify(checklist);
                    meeting.Theme = excelObject.At(5, 2).Unify(checklist);
                    meeting.Toastmaster = excelObject.At(6, 2).Unify(checklist);
                    meeting.SAA = excelObject.At(7, 2).Unify(checklist);
                    meeting.General_Evaluator = excelObject.At(8, 2).Unify(checklist);
                    meeting.Timer = excelObject.At(9, 2).Unify(checklist);
                    meeting.Grammarian = excelObject.At(10, 2).Unify(checklist);
                    meeting.Ah_Counter = excelObject.At(11, 2).Unify(checklist);
                    meeting.Table_Topic_Master = excelObject.At(12, 2).Unify(checklist);
                    meeting.Table_Topic_Evaluator = excelObject.At(13, 2).Unify(checklist);
                    meeting.Prepared_Speaker_1 = excelObject.At(14, 2).Unify(checklist);
                    meeting.Prepared_Speaker_2 = excelObject.At(18, 2).Unify(checklist);
                    meeting.Speech_Evaluator_1 = excelObject.At(17, 2).Unify(checklist);
                    meeting.Speech_Evaluator_2 = excelObject.At(21, 2).Unify(checklist);
                    meeting.Quiz_Master = excelObject.At(23, 2).Unify(checklist);
                    meeting.Photographer = excelObject.At(24, 2).Unify(checklist);
                    meeting.Video_Taker = excelObject.At(25, 2).Unify(checklist);
                    meeting.Meeting_Minutes_Taker = excelObject.At(26, 2).Unify(checklist);
                    meeting.Times = excelObject.At(28, 2).Unify(checklist);
                }
                else
                {
                    meeting.Date = excelObject.At(3, 2).Unify(checklist);
                    meeting.Room = excelObject.At(4, 2).Unify(checklist);
                    meeting.Theme = excelObject.At(5, 2).Unify(checklist);
                    meeting.Toastmaster = excelObject.At(7, 2).Unify(checklist);
                    meeting.SAA = excelObject.At(8, 2).Unify(checklist);
                    meeting.General_Evaluator = excelObject.At(9, 2).Unify(checklist);
                    meeting.Timer = excelObject.At(10, 2).Unify(checklist);
                    meeting.Grammarian = excelObject.At(11, 2).Unify(checklist);
                    meeting.Ah_Counter = excelObject.At(12, 2).Unify(checklist);
                    meeting.Table_Topic_Master = excelObject.At(13, 2).Unify(checklist);
                    meeting.Table_Topic_Evaluator = excelObject.At(14, 2).Unify(checklist);
                    meeting.Prepared_Speaker_1 = excelObject.At(15, 2).Unify(checklist);
                    meeting.Prepared_Speaker_2 = excelObject.At(16, 2).Unify(checklist);
                    meeting.Speech_Evaluator_1 = excelObject.At(17, 2).Unify(checklist);
                    meeting.Speech_Evaluator_2 = excelObject.At(18, 2).Unify(checklist);
                    meeting.Quiz_Master = excelObject.At(20, 2).Unify(checklist);
                    meeting.Photographer = excelObject.At(21, 2).Unify(checklist);
                    meeting.Video_Taker = excelObject.At(22, 2).Unify(checklist);
                    meeting.Meeting_Minutes_Taker = excelObject.At(23, 2).Unify(checklist);
                    meeting.Times = excelObject.At(32, 2).Unify(checklist);
                }
                

            }
            return meeting;
        }

        
    }
}
