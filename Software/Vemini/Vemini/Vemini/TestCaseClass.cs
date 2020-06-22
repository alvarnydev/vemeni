using System;
using System.Collections.Generic;
using System.Text;
using Vemini.Models;

namespace Vemini
{
    class TestCaseClass
    {
        public void testAddErrand()
        {
           Errand testErrand = new Errand();
            testErrand.Id = "6";
            testErrand.User = "3";
            testErrand.Type = "0";
            testErrand.Title = " Ich bin ein Test 2";
            testErrand.Description = "Hey ich teste von Benji";
            testErrand.location_lat = "10";
            testErrand.Location_lon = "20";
            testErrand.Date = "2020-06-22T09:31:16";
            testErrand.Status = "1";

        }
    }
}
