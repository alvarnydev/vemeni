using System;
using System.Collections.Generic;
using System.Text;

using Vemini.Models;
using Vemini.Services;


namespace Vemini
{
    public class TestCaseClass
    {
        public static Errand GetExampleErrandLong()
        {
            Errand testErrand = new Errand();
            testErrand.Id = 10;
            testErrand.User = 3;
            testErrand.Type = 0;
            testErrand.Category = 0;
            testErrand.Title = "Test Benji Lang";
            testErrand.Description = "Benji daaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaasssssss ist ein laaaaaaaaaaaaaaaaaaaaaanges file";
            testErrand.LocationLat = 10;
            testErrand.LocationLon = 20;
            testErrand.Date = null;
            testErrand.Status = 0;
            testErrand.AcceptedBy = 2;

            return testErrand;
        }
        public static Errand GetExampleErrandShort()
        {
            Errand testErrand = new Errand();
            testErrand.Id = 11;
            testErrand.User = 3;
            testErrand.Type = 0;
            testErrand.Category = 0;
            testErrand.Title = "Test Benji Kurz";
            testErrand.Description = "Benji kurzes file";
            testErrand.LocationLat = 10;
            testErrand.LocationLon = 20;
            testErrand.Date = null;
            testErrand.Status = 0;
            testErrand.AcceptedBy = 2;

            return testErrand;
        }
        public static void testAddErrand()
        {
            Errand testErrand = GetExampleErrandShort();

            ErrandService.AddErrand(testErrand);
        }

        public static void testGetErrands(string city)
        {
           List<Errand> testErrandList = ErrandService.GetErrands(city);
           foreach (Errand errand in testErrandList)
           {
               Console.WriteLine(errand.Title);
           }
           
        }
    }
}
