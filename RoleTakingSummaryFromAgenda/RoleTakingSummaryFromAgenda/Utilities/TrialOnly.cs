using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoleTakingSummaryFromAgenda.Domain.Entities;

namespace RoleTakingSummaryFromAgenda.Utilities
{
    public static class TrialOnly
    {
        public static string TrialOnly_DoSomething(this object o)
        {
            var rm = new RegularMeeting { Times = "T200", Date = "Jan 8th, 2015" };
            var t = typeof(RegularMeeting);
            var properties = t.GetProperties();
            var property = properties[0];
            var name = property.Name;
            var value = property.GetValue(rm);
            return "";

        }
    }
}
