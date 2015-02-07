using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoleTakingSummaryFromAgenda.Utilities;
using RoleTakingSummaryFromAgenda.Domain.Abstract;
using RoleTakingSummaryFromAgenda.Domain.Concrete;
using RoleTakingSummaryFromAgenda.Domain.Entities;
using System.IO;

namespace RoleTakingSummaryFromAgenda
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Job started!");
            var folder_old = Directory.GetCurrentDirectory() +  @"\old format\";
            var folder_new = Directory.GetCurrentDirectory() + @"\new format\";
            var files_old = Directory.GetFiles(folder_old);
            var files_new = Directory.GetFiles(folder_new);
            var files = files_old.Concat(files_new);
            var checklist = Directory.GetCurrentDirectory() + @"\names.txt";
            var meetings = files.ToRegularMeetings(checklist).ToList();
            var selector = new Func<RegularMeeting, IEnumerable<RoleTakingRecord>>(meeting => meeting.ToRoleTakingRecords());
            var roletakingrecords =  meetings.SelectMany(selector).ToList();
            //var roletakingcounts = roletakingrecords.ToRoleTakingCounts().ToList();

            //var meetingsStr = meetings.ToTransposedString();
            //var roletakingrecordsStr = roletakingrecords.ToStringWithPropertyNamesAndValuesByTabInLines(typeof(RoleTakingRecord));
            //var roletakingsummaryStr = roletakingcounts.ToStringWithPropertyNamesAndValuesByTabInLines(typeof(RoleTakingCount));

            //var meetings_out = Directory.GetCurrentDirectory() + @"\AATMCMeetingSummary2014.txt";
            //File.WriteAllText(meetings_out, meetingsStr);
            //var roletakingrecords_out = Directory.GetCurrentDirectory() + @"\AATMCRoleTakingDetails2014.txt";
            //File.WriteAllText(roletakingrecords_out, roletakingrecordsStr);
            //var roletakingsummary_out = Directory.GetCurrentDirectory() + @"\AATMCRoleTakingSummary2014.txt";
            //File.WriteAllText(roletakingsummary_out, roletakingsummaryStr);

            //var roletakingsummarycounts = roletakingrecords.ToRoleTakingSummaryCounts();
            //var roletakingsummarycountsStr = roletakingsummarycounts.ToStringWithPropertyNamesAndValuesByTabInLines(
            //    typeof(RoleTakingSummaryCount));
            //var roletakingsummarycounts_out = Directory.GetCurrentDirectory() + @"\AATMCRoleTakingSummaryCounts2014.txt";
            //File.WriteAllText(roletakingsummarycounts_out, roletakingsummarycountsStr);

            var roletakingcountsbyrole = roletakingrecords.ToRoleTakingCountsGroupedByRole();
            var roletakingcountsbyroleStr = roletakingcountsbyrole.ToStringWithPropertyNamesAndValuesByTabInLines(
                typeof(RoleTakingCount));
            var roletakingcounts_out = Directory.GetCurrentDirectory() + @"\AATMCRoleTakingCountsByRole2014.txt";
            File.WriteAllText(roletakingcounts_out, roletakingcountsbyroleStr);

            Console.WriteLine("Job done!");
            Console.ReadLine();
        }

        
    }
}
