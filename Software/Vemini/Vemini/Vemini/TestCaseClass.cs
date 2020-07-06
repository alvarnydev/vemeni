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
            testErrand.Category = "Haushaltshilfe";
            testErrand.Title = "Dringende Hilfe im Garten benötigt";
            testErrand.Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.";
            testErrand.AddressePlz = "12343";
            testErrand.AdresseCity = "Berlin";
            testErrand.AdresseStreet = "Habsburger Platz";
            testErrand.AdresseNmbr = "12A";
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
            testErrand.Category = "Einkaufen";
            testErrand.Title = "Unterstützung von älterer Dame beim Einkaufen";
            testErrand.Description = "Ich bräuchte kurz Hilfe beim Aldi um die Ecke. Ich würde auch 5 Euro als Dank zahlen";
            testErrand.AddressePlz = "10243";
            testErrand.AdresseCity = "Berlin";
            testErrand.AdresseStreet = "Warschauer Straße";
            testErrand.AdresseNmbr = "45";
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
