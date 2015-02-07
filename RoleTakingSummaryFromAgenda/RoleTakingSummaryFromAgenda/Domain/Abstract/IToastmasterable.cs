using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoleTakingSummaryFromAgenda.Domain.Entities;

namespace RoleTakingSummaryFromAgenda.Domain.Abstract
{
    public interface IToastmasterable
    {
        RegularMeeting GetRegularMeetingInfo();
        
    }
}
