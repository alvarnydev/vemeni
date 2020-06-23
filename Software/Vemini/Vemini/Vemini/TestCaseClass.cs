﻿using System;
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
            testErrand.Category = 0;
            testErrand.Title = " Ich bin ein Test von Benji";
            testErrand.Description = "Hey ich teste von Benji";
            testErrand.LocationLat = 10;
            testErrand.LocationLon = 20;
            testErrand.Date = DateTime.Now;
            testErrand.Status = 0;
            testErrand.AcceptedBy = 2;

            ErrandService.AddErrand(testErrand);
        }
    }
}
