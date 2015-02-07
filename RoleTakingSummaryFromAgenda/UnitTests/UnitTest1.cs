using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RoleTakingSummaryFromAgenda.Utilities;
using RoleTakingSummaryFromAgenda.Domain.Abstract;
using RoleTakingSummaryFromAgenda.Domain.Concrete;
using RoleTakingSummaryFromAgenda.Domain.Entities;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        

        [TestMethod]
        public void Can_Convert_From_RegularMeeting_To_RoleTakingCollection()
        {
            //var agenda = @"C:\Lemon\Personal\Desktop\Agenda_T101_20140704.xlsx";
            //var checklist = @"C:\Lemon\Personal\Desktop\names.txt";
            //var meeting = new AATMCMeeting(agenda, checklist).GetRegularMeetingInfo();
            //var roletakings = meeting.ToRoleTakingRecords().ToList();

            //Assert.AreEqual(16, roletakings.Count);
            //Assert.AreEqual("Toastmaster", roletakings[0].RoleName);
            //Assert.AreEqual("Meeting Minutes Taker", roletakings[15].RoleName);
            
        }

        [TestMethod]
        public void Can_Convert_From_RoleTakingRecord_To_StringWithPropertyValueByTab()
        {
            var rtr = new RoleTakingRecord
            {
                Role_Name = "Toastmaster",
                Role_Taker_Name = "Guan, Alex",
                Times = "T200",
                Date = "June 14th, 2015"
            };

            var result = rtr.ToStringWithPropertyValuesByTab(typeof(RoleTakingRecord));

            Assert.AreEqual("Toastmaster\tGuan, Alex\tT200\tJune 14th, 2015", result);
        }

        [TestMethod]
        public void Can_Convert_From_RoleTakingRecord_To_StringWithPropertyNameByTab()
        {
            var rtr = new RoleTakingRecord
            {
                Role_Name = "Toastmaster",
                Role_Taker_Name = "Guan, Alex",
                Times = "T200",
                Date = "June 14th, 2015"
            };

            var result = typeof(RoleTakingRecord).ToStringWithPropertyNamesByTab();

            Assert.AreEqual("Role Name\tRole Taker Name\tTimes\tDate",result);
        }

        [TestMethod]
        public void Can_Convert_From_RoleTakingRecordCollection_To_StringWithPropertyNamesAndValuesByTabInLines()
        {
            var rtrs = new RoleTakingRecord[]
            {
                new RoleTakingRecord
                {
                    Role_Name = "Toastmaster",
                    Role_Taker_Name = "Guan, Alex",
                    Times = "T200",
                    Date = "June 14th, 2015"
                },
                new RoleTakingRecord
                {
                    Role_Name = "SAA",
                    Role_Taker_Name = "Xiang, Cathryn",
                    Times = "T200",
                    Date = "June 14th, 2015"
                },
                new RoleTakingRecord
                {
                    Role_Name = "General Evaluator",
                    Role_Taker_Name = "Yin, Ann",
                    Times = "T200",
                    Date = "June 14th, 2015"
                }
            };

            var actual = rtrs.ToStringWithPropertyNamesAndValuesByTabInLines(typeof(RoleTakingRecord));

            var expected = "Role Name\tRole Taker Name\tTimes\tDate\r\n" +
                "Toastmaster\tGuan, Alex\tT200\tJune 14th, 2015\r\n" +
                "SAA\tXiang, Cathryn\tT200\tJune 14th, 2015\r\n" +
                "General Evaluator\tYin, Ann\tT200\tJune 14th, 2015";

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Can_Dump_Any_TypedCollection_To_String()
        {
            var rtrs = new RoleTakingRecord[]
            {
                new RoleTakingRecord { Role_Name = "Toastmaster", Role_Taker_Name = "Guan, Alex", Times = "T200"} ,
                new RoleTakingRecord { Role_Name = "SAA", Role_Taker_Name = "Xiang, Cathryn", Times = "T200"} ,
                new RoleTakingRecord { Role_Name = "General Evaluator", Role_Taker_Name = "Yin, Ann", Times = "T200"} ,
                new RoleTakingRecord { Role_Name = "Timer", Role_Taker_Name = "Su, Xindao", Times = "T200"} ,
                new RoleTakingRecord { Role_Name = "Prepared Speaker 2", Role_Taker_Name = "Wang, Nick", Times = "T200"} 
            };

            

            var selector = new Func<RoleTakingRecord, bool>(record => record.Role_Name.StartsWith("T"));

            return "";
        }
    }
}
