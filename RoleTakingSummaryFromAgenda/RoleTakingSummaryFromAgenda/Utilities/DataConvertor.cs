using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using RoleTakingSummaryFromAgenda.Domain.Entities;
using RoleTakingSummaryFromAgenda.Domain.Concrete;
using RoleTakingSummaryFromAgenda.Domain.Abstract;
using System.Reflection;

namespace RoleTakingSummaryFromAgenda.Utilities
{
    public static class DataConvertor
    {
        private static string UnifyParticipantNamesByPatternsInDictionary(this string raw, 
            Dictionary<string, string> dict)
        {
            var result = raw;
            foreach (var entry in dict)
            {
                var target = raw.Trim().ToUpper().Replace(" ", "").Replace(",", "");
                if (Regex.IsMatch(target, entry.Key, RegexOptions.IgnoreCase))
                {
                    result = entry.Value;
                    break;
                }

            }
            return result;
        }

        public static string Unify(this string raw, Dictionary<string, string> dict)
        {
            return raw.UnifyParticipantNamesByPatternsInDictionary(dict);
        }

       

        public static string ToTransposedString(this IEnumerable<RegularMeeting> meetings)
        {
            if(meetings == null || meetings.Count() == 0)
                return "";

            var colcount =  meetings.Count() + 1;

            var rowCount = 20;

            var matrix = new string [rowCount,colcount];

            matrix[0, 0] = "Roles/Meeting";
            matrix[1, 0] = "Date";
            matrix[2, 0] = "Room";
            matrix[3, 0] = "Theme";
            matrix[4, 0] = "Toastmaster";
            matrix[5, 0] = "SAA";
            matrix[6, 0] = "GE";
            matrix[7, 0] = "Timer";
            matrix[8, 0] = "Grammarian";
            matrix[9, 0] = "Ah-Counter";
            matrix[10, 0] = "Table Topics Master";
            matrix[11, 0] = "Table Topics Evaluator";
            matrix[12, 0] = "Prepared Speaker 1";
            matrix[13, 0] = "Prepared Speaker 2";
            matrix[14, 0] = "Speech Evaluator 1";
            matrix[15, 0] = "Speech Evaluator 2";
            matrix[16, 0] = "Quiz Master";
            matrix[17, 0] = "Photographer";
            matrix[18, 0] = "Video Taker";
            matrix[19, 0] = "Meeting Minutes Taker";


            var cursor = 1;
            foreach(var meeting in meetings)
            {
                matrix[0, cursor] = meeting.Times;
                matrix[1, cursor] = meeting.Date;
                matrix[2, cursor] = meeting.Room;
                matrix[3, cursor] = meeting.Theme;
                matrix[4, cursor] = meeting.Toastmaster;
                matrix[5, cursor] = meeting.SAA;
                matrix[6, cursor] = meeting.General_Evaluator;
                matrix[7, cursor] = meeting.Timer;
                matrix[8, cursor] = meeting.Grammarian;
                matrix[9, cursor] = meeting.Ah_Counter;
                matrix[10, cursor] = meeting.Table_Topic_Master;
                matrix[11, cursor] = meeting.Table_Topic_Evaluator;
                matrix[12, cursor] = meeting.Prepared_Speaker_1;
                matrix[13, cursor] = meeting.Prepared_Speaker_2;
                matrix[14, cursor] = meeting.Speech_Evaluator_1;
                matrix[15, cursor] = meeting.Speech_Evaluator_2;
                matrix[16, cursor] = meeting.Quiz_Master;
                matrix[17, cursor] = meeting.Photographer;
                matrix[18, cursor] = meeting.Video_Taker;
                matrix[19, cursor] = meeting.Meeting_Minutes_Taker;

                cursor++;
                
            }

            var sb = new StringBuilder();

            for(int i = 0; i < 20; i++)
            {
                for(int j = 0; j < colcount; j++)
                {
                    sb.Append(matrix[i, j]);
                    sb.Append("\t");
                }

                sb.Append("\r\n");
            }

            return sb.ToString();

        }

        public static IEnumerable<RegularMeeting> ToRegularMeetings(
            this IEnumerable<string> agenda_files, string checklist)
        {
            return from agenda in agenda_files select agenda.ToRegularMeeting(checklist);

        }

        public static RegularMeeting ToRegularMeeting(this string agenda, string checklist)
        {
            return new AATMCMeeting(agenda, checklist).GetRegularMeetingInfo();
        }

        private static bool IsRoleProperty(this PropertyInfo p)
        {
            if (p.Name == "Times")
                return false;

            if (p.Name == "Date")
                return false;

            if (p.Name == "Room")
                return false;

            if (p.Name == "Theme")
                return false;

            return true;
        }

        public static IEnumerable<RoleTakingRecord> ToRoleTakingRecords(this RegularMeeting meeting)
        {
            if (meeting == null) return null;

            var times = meeting.Times;
            var date = meeting.Date;

            var properties = typeof(RegularMeeting).GetProperties();

            var result = from property in properties
                         where property.IsRoleProperty()
                         select new RoleTakingRecord
                         {
                             Role_Name = property.Name.ReplaceUnderscoreWithSpace(),
                             Role_Taker_Name = property.GetValue(meeting).ToString(),
                             Times = times,
                             Date = date
                         };

            return result;

        }

        private static bool IsValidRoleTaker(this string roleTakerName)
        {
            if (String.IsNullOrEmpty(roleTakerName)) return false;

            if (String.IsNullOrEmpty(roleTakerName.Trim())) return false;

            if (roleTakerName.Trim() == "N/A") return false;

            if (roleTakerName.Trim().Contains("(~")) return false;

            return true;
        }

        public static IEnumerable<RoleTakingCount> ToRoleTakingCounts(this IEnumerable<RoleTakingRecord> roleTakingRecords)
        {
            if (roleTakingRecords == null)
                return null;

            if (roleTakingRecords.Count() == 0)
                return null;

            var raw = from roletakingrecord in roleTakingRecords
                       where roletakingrecord.Role_Taker_Name.IsValidRoleTaker()
                       group roletakingrecord by new { roletakingrecord.Role_Taker_Name, roletakingrecord.Role_Name } into g
                       select new RoleTakingCount
                       {
                           Role_Taker_Name = g.Key.Role_Taker_Name,
                           Role_Name = g.Key.Role_Name,
                           Count = g.Count()
                       };

            var ordered = raw
                .OrderBy(record => record.Role_Taker_Name)
                .ThenByDescending(record => record.Count);


            return ordered;
        }

        public static IEnumerable<RoleTakingCount> ToRoleTakingCountsGroupedByRole(this IEnumerable<RoleTakingRecord> roleTakingRecords)
        {
            if (roleTakingRecords == null)
                return null;

            if (roleTakingRecords.Count() == 0)
                return null;

            var raw = from roletakingrecord in roleTakingRecords
                      where roletakingrecord.Role_Taker_Name.IsValidRoleTaker()
                      group roletakingrecord by new { roletakingrecord.Role_Name, roletakingrecord.Role_Taker_Name } into g
                      select new RoleTakingCount
                      {
                          Role_Name = g.Key.Role_Name,
                          Role_Taker_Name = g.Key.Role_Taker_Name,
                          Count = g.Count()
                      };

            var ordered = raw
                .OrderBy(record => record.Role_Name)
                .ThenByDescending(record => record.Count);


            return ordered;
        }

        public static IEnumerable<RoleTakingSummaryCount> ToRoleTakingSummaryCounts(
            this IEnumerable<RoleTakingRecord> records)
        {
            if (records == null)
                return null;

            if (records.Count() == 0)
                return null;

            var raw = from record in records
                      where record.Role_Taker_Name.IsValidRoleTaker()
                      group record by record.Role_Taker_Name into g
                      select new RoleTakingSummaryCount
                      {
                          Role_Taker_Name = g.Key,
                          Count = g.Count()
                      };

            var ordered = raw
                .OrderByDescending(record => record.Count);

            return ordered;

        }


        

        

    }
}
