using System;
using System.Collections.Generic;
using System.Text;
using Vemini.Models;
using Vemini.Services;


namespace Vemini
{
    public class TestCaseClass
    {
        public static void testAddErrand()
        {
           Errand testErrand = new Errand();
            testErrand.Id = 6;
            testErrand.User = 3;
            testErrand.Type = 0;
            testErrand.Title = " Ich bin ein Test 2";
            testErrand.Description = "Hey ich teste von Benji";
            testErrand.location_lat = 10;
            testErrand.Location_lon = 20;
            testErrand.Date = DateTime.Now;
            testErrand.Status = 1;
            testErrand.Accepted_by = 2;

            ErrandService.AddErrand(testErrand);
        }
    }
}
