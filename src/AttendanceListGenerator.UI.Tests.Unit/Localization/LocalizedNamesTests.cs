using AttendanceListGenerator.Core.Data;
using AttendanceListGenerator.UI.Localization;
using NUnit.Framework;
using System;
using System.Globalization;

namespace AttendanceListGenerator.UI.Tests.Unit.Localization
{
    class LocalizedNamesTests
    {
        [TestCase("pl-PL")]
        [TestCase("en-US")]
        [TestCase("de-de")]
        public void DocumentAuthor_GivenCulture_ReturnsAlwaysKamilJaworski(string cultureCode)
        {
            UseCulture(cultureCode);
            LocalizedNames localizedNames = new LocalizedNames();

            string result = localizedNames.DocumentAuthor;

            Assert.That(result, Is.EqualTo("Kamil Jaworski"));
        }

        [TestCase("pl-PL", "Generator List Obecności")]
        [TestCase("en-US", "Attendance List Generator")]
        [TestCase("de-de", "Attendance List Generator")]
        public void ApplicationDirectoryName_GivenCulture_ReturnsGivenName(string cultureCode, string expectedResult)
        {
            UseCulture(cultureCode);
            LocalizedNames localizedNames = new LocalizedNames();

            string result = localizedNames.ApplicationDirectoryName;

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [TestCase("pl-PL", "Dokumenty")]
        [TestCase("en-US", "Documents")]
        [TestCase("de-de", "Documents")]
        public void DocumentsDirectoryName_GivenCulture_ReturnsGivenName(string cultureCode, string expectedResult)
        {
            UseCulture(cultureCode);
            LocalizedNames localizedNames = new LocalizedNames();

            string result = localizedNames.DocumentsDirectoryName;

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [TestCase("pl-PL", DayOfWeek.Monday, "Poniedziałek")]
        [TestCase("pl-PL", DayOfWeek.Tuesday, "Wtorek")]
        [TestCase("pl-PL", DayOfWeek.Wednesday, "Środa")]
        [TestCase("pl-PL", DayOfWeek.Thursday, "Czwartek")]
        [TestCase("pl-PL", DayOfWeek.Friday, "Piątek")]
        [TestCase("pl-PL", DayOfWeek.Saturday, "Sobota")]
        [TestCase("pl-PL", DayOfWeek.Sunday, "Niedziela")]
        [TestCase("en-US", DayOfWeek.Monday, "Monday")]
        [TestCase("en-US", DayOfWeek.Tuesday, "Tuesday")]
        [TestCase("en-US", DayOfWeek.Wednesday, "Wednesday")]
        [TestCase("en-US", DayOfWeek.Thursday, "Thursday")]
        [TestCase("en-US", DayOfWeek.Friday, "Friday")]
        [TestCase("en-US", DayOfWeek.Saturday, "Saturday")]
        [TestCase("en-US", DayOfWeek.Sunday, "Sunday")]
        public void GetDayOfWeekName_GivenCulture_ReturnsCorrectDayOfWeekName(string cultureCode, DayOfWeek dayOfWeek, string expectedResult)
        {
            UseCulture(cultureCode);
            LocalizedNames localizedNames = new LocalizedNames();

            string result = localizedNames.GetDayOfWeekName(dayOfWeek);

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [TestCase("pl-PL", Month.None, "")]
        [TestCase("pl-PL", Month.January, "Styczeń")]
        [TestCase("pl-PL", Month.February, "Luty")]
        [TestCase("pl-PL", Month.March, "Marzec")]
        [TestCase("pl-PL", Month.April, "Kwiecień")]
        [TestCase("pl-PL", Month.May, "Maj")]
        [TestCase("pl-PL", Month.June, "Czerwiec")]
        [TestCase("pl-PL", Month.July, "Lipiec")]
        [TestCase("pl-PL", Month.August, "Sierpień")]
        [TestCase("pl-PL", Month.September, "Wrzesień")]
        [TestCase("pl-PL", Month.October, "Październik")]
        [TestCase("pl-PL", Month.November, "Listopad")]
        [TestCase("pl-PL", Month.December, "Grudzień")]
        [TestCase("en-US", Month.None, "")]
        [TestCase("en-US", Month.January, "January")]
        [TestCase("en-US", Month.February, "February")]
        [TestCase("en-US", Month.March, "March")]
        [TestCase("en-US", Month.April, "April")]
        [TestCase("en-US", Month.May, "May")]
        [TestCase("en-US", Month.June, "June")]
        [TestCase("en-US", Month.July, "July")]
        [TestCase("en-US", Month.August, "August")]
        [TestCase("en-US", Month.September, "September")]
        [TestCase("en-US", Month.October, "October")]
        [TestCase("en-US", Month.November, "November")]
        [TestCase("en-US", Month.December, "December")]
        public void GetMonthName_GivenCulture_ReturnsCorrectMonthName(string cultureCode, Month month, string expectedResult)
        {
            UseCulture(cultureCode);
            LocalizedNames localizedNames = new LocalizedNames();

            string result = localizedNames.GetMonthName(month);

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [TestCase("pl-PL", Holiday.None, "")]
        [TestCase("pl-PL", Holiday.AllSaintsDay, "Wszystkich Świętych")]
        [TestCase("pl-PL", Holiday.ArmedForcesDay, "Święto Wojska Polskiego")]
        [TestCase("pl-PL", Holiday.Christmas, "Boże Narodzenie")]
        [TestCase("pl-PL", Holiday.ConstitutionDay, "Konstytucja 3 maja")]
        [TestCase("pl-PL", Holiday.CorpusChristi, "Boże Ciało")]
        [TestCase("pl-PL", Holiday.DescendOfTheHolySpirit, "Zesłanie Ducha Świetego")]
        [TestCase("pl-PL", Holiday.EasterMonday, "Poniedziałek Wielkanocny")]
        [TestCase("pl-PL", Holiday.EasterSunday, "Wielkanoc")]
        [TestCase("pl-PL", Holiday.Epiphany, "Święto Trzech Króli")]
        [TestCase("pl-PL", Holiday.IndependenceDay, "Święto Niepodległości")]
        [TestCase("pl-PL", Holiday.LabourDay, "Święto Pracy")]
        [TestCase("pl-PL", Holiday.NewYearsDay, "Nowy Rok")]
        [TestCase("en-US", Holiday.None, "")]
        [TestCase("en-US", Holiday.AllSaintsDay, "All Saints' Day")]
        [TestCase("en-US", Holiday.ArmedForcesDay, "Armed Forces Day")]
        [TestCase("en-US", Holiday.Christmas, "Christmas")]
        [TestCase("en-US", Holiday.ConstitutionDay, "Constitution Day")]
        [TestCase("en-US", Holiday.CorpusChristi, "Corpus Christi")]
        [TestCase("en-US", Holiday.DescendOfTheHolySpirit, "The Descend of the Holy Spirit")]
        [TestCase("en-US", Holiday.EasterMonday, "Easter Monday")]
        [TestCase("en-US", Holiday.EasterSunday, "Easter")]
        [TestCase("en-US", Holiday.Epiphany, "Epiphany")]
        [TestCase("en-US", Holiday.IndependenceDay, "Independence Day")]
        [TestCase("en-US", Holiday.LabourDay, "Labour Day")]
        [TestCase("en-US", Holiday.NewYearsDay, "New Years Day")]
        public void GetHolidayName_GivenCulture_ReturnsCorrectHolidayName(string cultureCode, Holiday holiday, string expectedResult)
        {
            UseCulture(cultureCode);
            LocalizedNames localizedNames = new LocalizedNames();

            string result = localizedNames.GetHolidayName(holiday);

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [TestCase("pl-PL", DayOfWeek.Monday, "pon.")]
        [TestCase("pl-PL", DayOfWeek.Tuesday, "wto.")]
        [TestCase("pl-PL", DayOfWeek.Wednesday, "śro.")]
        [TestCase("pl-PL", DayOfWeek.Thursday, "czw.")]
        [TestCase("pl-PL", DayOfWeek.Friday, "pią.")]
        [TestCase("pl-PL", DayOfWeek.Saturday, "sob.")]
        [TestCase("pl-PL", DayOfWeek.Sunday, "nie.")]
        [TestCase("en-US", DayOfWeek.Monday, "Mon.")]
        [TestCase("en-US", DayOfWeek.Tuesday, "Tue.")]
        [TestCase("en-US", DayOfWeek.Wednesday, "Wed.")]
        [TestCase("en-US", DayOfWeek.Thursday, "Thu.")]
        [TestCase("en-US", DayOfWeek.Friday, "Fri.")]
        [TestCase("en-US", DayOfWeek.Saturday, "Sat.")]
        [TestCase("en-US", DayOfWeek.Sunday, "Sun.")]
        public void GetDayOfWeekAbbreviation_GivenCulture_ReturnsCorrectDayOfWeekAbbreviation(string cultureCode, DayOfWeek dayOfWeek, string expectedResult)
        {
            UseCulture(cultureCode);
            LocalizedNames localizedNames = new LocalizedNames();

            string result = localizedNames.GetDayOfWeekAbbreviation(dayOfWeek);

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [TestCase("pl-PL", Month.January, 2018, "Styczeń 2018")]
        [TestCase("pl-PL", Month.April, 1997, "Kwiecień 1997")]
        [TestCase("pl-PL", Month.December, 2020, "Grudzień 2020")]
        [TestCase("pl-PL", Month.May, 2005, "Maj 2005")]
        [TestCase("en-US", Month.January, 2018, "January 2018")]
        [TestCase("en-US", Month.April, 1997, "April 1997")]
        [TestCase("en-US", Month.December, 2020, "December 2020")]
        [TestCase("en-US", Month.May, 2005, "May 2005")]
        public void GetDocumentTitle_GivenCulture_ReturnsCorrectDocumentTitle(string cultureCode, Month month, int year, string expectedResult)
        {
            UseCulture(cultureCode);
            LocalizedNames localizedNames = new LocalizedNames();

            string result = localizedNames.GetDocumentTitle(month, year);

            Assert.That(result, Is.EqualTo(expectedResult));
        }



        private void UseCulture(string cultureCode)
        {
            CultureInfo.CurrentCulture = new CultureInfo(cultureCode, false);
            CultureInfo.CurrentUICulture = new CultureInfo(cultureCode, false);
        }
    }
}
