using AttendanceListGenerator.Core.Data;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AttendanceListGenerator.Core.Tests.Unit.Data
{
    class DaysOffTests
    {
        [TestCase(1899)]
        [TestCase(1800)]
        [TestCase(1600)]
        [TestCase(2101)]
        [TestCase(2110)]
        [TestCase(2500)]
        public void Constructor_PassYearLessThan1900OrAbove2100_ThrowArgumentOutOfRangeException(int year)
        {
            TestDelegate constructor = () => new DaysOffData(year);

            Assert.That(constructor, Throws.InstanceOf<ArgumentOutOfRangeException>());
        }

        [TestCase(1900)]
        [TestCase(2100)]
        public void Constructor_PassYear1900And2100_DoNotThrowArgumentOutOfRangeException(int year)
        {
            TestDelegate constructor = () => new DaysOffData(year);

            Assert.That(constructor, Throws.Nothing);
        }

        [Test]
        public void Constructor_Year2019_CreateListOf13DaysOff()
        {
            DaysOffData daysOffList = new DaysOffData(2019);

            Assert.That(daysOffList.DaysOff.Count, Is.EqualTo(13));
        }

        [Test]
        public void Constructor_Year2019_NewYearsDayDateIs_01_01_2019()
        {
            DaysOffData daysOffList = new DaysOffData(2019);
            IDayOff newYearsDay = daysOffList.DaysOff.FirstOrDefault(x => x.Holiday == Holiday.NewYearsDay);

            DateTime expectedDate = new DateTime(2019, 1, 1);
            Assert.That(newYearsDay.Date, Is.EqualTo(expectedDate));
        }

        [Test]
        public void Constructor_Year2019_EpiphanyDateIs_06_01_2019()
        {
            DaysOffData daysOffList = new DaysOffData(2019);
            IDayOff epiphany = daysOffList.DaysOff.FirstOrDefault(x => x.Holiday == Holiday.Epiphany);

            DateTime expectedDate = new DateTime(2019, 1, 6);
            Assert.That(epiphany.Date, Is.EqualTo(expectedDate));
        }

        [Test]
        public void Constructor_Year2019_EasterSundayDateIs_21_04_2019()
        {
            DaysOffData daysOffList = new DaysOffData(2019);
            IDayOff easterSunday = daysOffList.DaysOff.FirstOrDefault(x => x.Holiday == Holiday.EasterSunday);

            DateTime expectedDate = new DateTime(2019, 4, 21);
            Assert.That(easterSunday.Date, Is.EqualTo(expectedDate));
        }

        [Test]
        public void Constructor_Year2019_EasterMondayDateIs_22_04_2019()
        {
            DaysOffData daysOffList = new DaysOffData(2019);
            IDayOff easterMonday = daysOffList.DaysOff.FirstOrDefault(x => x.Holiday == Holiday.EasterMonday);

            DateTime expectedDate = new DateTime(2019, 4, 22);
            Assert.That(easterMonday.Date, Is.EqualTo(expectedDate));
        }

        [Test]
        public void Constructor_Year2019_LabourDayDateIs_01_05_2019()
        {
            DaysOffData daysOffList = new DaysOffData(2019);
            IDayOff labourDay = daysOffList.DaysOff.FirstOrDefault(x => x.Holiday == Holiday.LabourDay);

            DateTime expectedDate = new DateTime(2019, 5, 1);
            Assert.That(labourDay.Date, Is.EqualTo(expectedDate));
        }

        [Test]
        public void Constructor_Year2019_ConstitutionDayDateIs_03_05_2019()
        {
            DaysOffData daysOffList = new DaysOffData(2019);
            IDayOff constitutionDay = daysOffList.DaysOff.FirstOrDefault(x => x.Holiday == Holiday.ConstitutionDay);

            DateTime expectedDate = new DateTime(2019, 5, 3);
            Assert.That(constitutionDay.Date, Is.EqualTo(expectedDate));
        }

        [Test]
        public void Constructor_Year2019_DescendOfTheHolySpiritDateIs_09_06_2019()
        {
            DaysOffData daysOffList = new DaysOffData(2019);
            IDayOff descendOfTheHolySpirit = daysOffList.DaysOff.FirstOrDefault(x => x.Holiday == Holiday.DescendOfTheHolySpirit);

            DateTime expectedDate = new DateTime(2019, 6, 9);
            Assert.That(descendOfTheHolySpirit.Date, Is.EqualTo(expectedDate));
        }

        [Test]
        public void Constructor_Year2019_CorpusChristiDayDateIs_20_06_2019()
        {
            DaysOffData daysOffList = new DaysOffData(2019);
            IDayOff corpusChristiDay = daysOffList.DaysOff.FirstOrDefault(x => x.Holiday == Holiday.CorpusChristiDay);

            DateTime expectedDate = new DateTime(2019, 6, 20);
            Assert.That(corpusChristiDay.Date, Is.EqualTo(expectedDate));
        }

        [Test]
        public void Constructor_Year2019_ArmedForcesDayDateIs_15_08_2019()
        {
            DaysOffData daysOffList = new DaysOffData(2019);
            IDayOff armedForcesDay = daysOffList.DaysOff.FirstOrDefault(x => x.Holiday == Holiday.ArmedForcesDay);

            DateTime expectedDate = new DateTime(2019, 8, 15);
            Assert.That(armedForcesDay.Date, Is.EqualTo(expectedDate));
        }

        [Test]
        public void Constructor_Year2019_AllSaintsDayDateIs_01_11_2019()
        {
            DaysOffData daysOffList = new DaysOffData(2019);
            IDayOff allSaintsDay = daysOffList.DaysOff.FirstOrDefault(x => x.Holiday == Holiday.AllSaintsDay);

            DateTime expectedDate = new DateTime(2019, 11, 1);
            Assert.That(allSaintsDay.Date, Is.EqualTo(expectedDate));
        }

        [Test]
        public void Constructor_Year2019_IndependenceDayDateIs_11_11_2019()
        {
            DaysOffData daysOffList = new DaysOffData(2019);
            IDayOff independenceDay = daysOffList.DaysOff.FirstOrDefault(x => x.Holiday == Holiday.IndependenceDay);

            DateTime expectedDate = new DateTime(2019, 11, 11);
            Assert.That(independenceDay.Date, Is.EqualTo(expectedDate));
        }

        [Test]
        public void Constructor_Year2019_FirstChristmasDayDateIs_25_12_2019()
        {
            DaysOffData daysOffList = new DaysOffData(2019);
            IDayOff christmas = daysOffList.DaysOff.FirstOrDefault(x => x.Holiday == Holiday.Christmas);

            DateTime expectedDate = new DateTime(2019, 12, 25);
            Assert.That(christmas.Date, Is.EqualTo(expectedDate));
        }

        [Test]
        public void Constructor_Year2019_SecondChristmasDayDateIs_26_12_2019()
        {
            DaysOffData daysOffList = new DaysOffData(2019);
            IDayOff christmas = daysOffList.DaysOff.LastOrDefault(x => x.Holiday == Holiday.Christmas);

            DateTime expectedDate = new DateTime(2019, 12, 26);
            Assert.That(christmas.Date, Is.EqualTo(expectedDate));
        }

        [Test]
        public void Constructor_Year2018_EasterSundayDateIs_01_04_2018()
        {
            DaysOffData daysOffList = new DaysOffData(2018);
            IDayOff easterSunday = daysOffList.DaysOff.FirstOrDefault(x => x.Holiday == Holiday.EasterSunday);

            DateTime expectedDate = new DateTime(2018, 4, 1);
            Assert.That(easterSunday.Date, Is.EqualTo(expectedDate));
        }

        [Test]
        public void Constructor_Year2018_EasterMondayDateIs_02_04_2018()
        {
            DaysOffData daysOffList = new DaysOffData(2018);
            IDayOff easterMonday = daysOffList.DaysOff.FirstOrDefault(x => x.Holiday == Holiday.EasterMonday);

            DateTime expectedDate = new DateTime(2018, 4, 2);
            Assert.That(easterMonday.Date, Is.EqualTo(expectedDate));
        }

        [Test]
        public void Constructor_Year2018_DescendOfTheHolySpiritDateIs_20_05_2018()
        {
            DaysOffData daysOffList = new DaysOffData(2018);
            IDayOff descendOfTheHolySpirit = daysOffList.DaysOff.FirstOrDefault(x => x.Holiday == Holiday.DescendOfTheHolySpirit);

            DateTime expectedDate = new DateTime(2018, 5, 20);
            Assert.That(descendOfTheHolySpirit.Date, Is.EqualTo(expectedDate));
        }

        [Test]
        public void Constructor_Year2018_CorpusChristiDayDateIs_31_05_2018()
        {
            DaysOffData daysOffList = new DaysOffData(2018);
            IDayOff corpusChristiDay = daysOffList.DaysOff.FirstOrDefault(x => x.Holiday == Holiday.CorpusChristiDay);

            DateTime expectedDate = new DateTime(2018, 5, 31);
            Assert.That(corpusChristiDay.Date, Is.EqualTo(expectedDate));
        }

        [Test]
        public void GetDaysOff_None2019_ThrowsArgumentException()
        {
            DaysOffData daysOffList = new DaysOffData(2019);

            TestDelegate getList = () => daysOffList.GetDaysOff(Month.None);

            Assert.That(getList, Throws.ArgumentException);
        }

        [Test]
        public void GetDaysOff_January2019_ReturnsListOf2Elements()
        {
            DaysOffData daysOffList = new DaysOffData(2019);

            IList<IDayOff> list = daysOffList.GetDaysOff(Month.January);

            Assert.That(list.Count, Is.EqualTo(2));
        }

        [Test]
        public void GetDaysOff_January2019_ReturnsListWithNewYearsDayAndEpiphany()
        {
            DaysOffData daysOffList = new DaysOffData(2019);

            IList<IDayOff> list = daysOffList.GetDaysOff(Month.January);

            var result = list.Where(d => d.Holiday == Holiday.NewYearsDay || d.Holiday == Holiday.Epiphany);
            Assert.That(result.Count, Is.EqualTo(2));
        }

        [Test]
        public void GetDaysOff_June2019_ReturnsListOf2Elements()
        {
            DaysOffData daysOffList = new DaysOffData(2019);

            IList<IDayOff> list = daysOffList.GetDaysOff(Month.June);

            Assert.That(list.Count, Is.EqualTo(2));
        }

        [Test]
        public void GetDaysOff_June2019_ReturnsListWithNewYearsDayAndEpiphany()
        {
            DaysOffData daysOffList = new DaysOffData(2019);

            IList<IDayOff> list = daysOffList.GetDaysOff(Month.June);

            var result = list.Where(d => d.Holiday == Holiday.DescendOfTheHolySpirit || d.Holiday == Holiday.CorpusChristiDay);
            Assert.That(result.Count, Is.EqualTo(2));
        }
    }
}
