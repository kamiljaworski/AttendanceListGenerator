using AttendanceListGenerator.Core.Data;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AttendanceListGenerator.Core.Tests.Unit.Data
{
    class AttendanceListDataTests
    {
        [TestCase(Month.February)]
        [TestCase(Month.March)]
        [TestCase(Month.July)]
        [TestCase(Month.December)]
        public void Constructor_PassValidMonth_MonthIsEqualToGivenMonth(Month month)
        {
            AttendanceListData monthData = new AttendanceListData(Mock.Of<IDaysOffData>(), GetListOfPeople(), month, 2019);

            Assert.That(monthData.Month, Is.EqualTo(month));
        }

        [TestCase(2019)]
        [TestCase(2018)]
        [TestCase(2022)]
        [TestCase(2010)]
        public void Constructor_PassValidYear_YearIsEqualToGivenYear(int year)
        {
            AttendanceListData monthData = new AttendanceListData(Mock.Of<IDaysOffData>(), GetListOfPeople(), Month.January, year);

            Assert.That(monthData.Year, Is.EqualTo(year));
        }

        [Test]
        public void Constructor_PassValidPeopleList_FullnamesListIsEqualToGivenList()
        {
            IList<IPerson> people = GetListOfPeople();

            AttendanceListData monthData = new AttendanceListData(Mock.Of<IDaysOffData>(), people, Month.January, 2019);

            Assert.That(monthData.People, Is.EqualTo(people));
        }

        [Test]
        public void Constructor_PassEmptyPeopleList_ThrowsArgumentException()
        {
            IList<IPerson> people = new List<IPerson>();

            TestDelegate constructor = () => new AttendanceListData(Mock.Of<IDaysOffData>(), people, Month.January, 2019);

            Assert.That(constructor, Throws.ArgumentException);
        }

        [Test]
        public void Constructor_PassListOf8People_ThrowsArgumentException()
        {
            IList<IPerson> people = new List<IPerson>
            {
                Mock.Of<IPerson>(),
                Mock.Of<IPerson>(),
                Mock.Of<IPerson>(),
                Mock.Of<IPerson>(),
                Mock.Of<IPerson>(),
                Mock.Of<IPerson>(),
                Mock.Of<IPerson>(),
                Mock.Of<IPerson>()
            };

            TestDelegate constructor = () => new AttendanceListData(Mock.Of<IDaysOffData>(), people, Month.January, 2019);

            Assert.That(constructor, Throws.ArgumentException);
        }

        [Test]
        public void Constructor_PassMonthNone_ThrowsArgumentException()
        {
            TestDelegate constructor = () => new AttendanceListData(Mock.Of<IDaysOffData>(), GetListOfPeople(), Month.None, 2019);

            Assert.That(constructor, Throws.ArgumentException);
        }

        [Test]
        public void Constructor_PassNullPeopleList_ThrowsArgumentException()
        {
            TestDelegate constructor = () => new AttendanceListData(Mock.Of<IDaysOffData>(), null, Month.January, 2019);

            Assert.That(constructor, Throws.ArgumentException);
        }

        [Test]
        public void Constructor_PassNullDaysOffData_ThrowsArgumentException()
        {
            TestDelegate constructor = () => new AttendanceListData(null, GetListOfPeople(), Month.January, 2019);

            Assert.That(constructor, Throws.ArgumentException);
        }

        [Test]
        public void Constructor_PassDaysOffDataWithAnotherYear_ThrowsArgumentException()
        {
            TestDelegate constructor = () => new AttendanceListData(Mock.Of<IDaysOffData>(d => d.Year == 2019), GetListOfPeople(), Month.January, 2019);

            Assert.That(constructor, Throws.ArgumentException);
        }

        [TestCase(1899)]
        [TestCase(1800)]
        [TestCase(1600)]
        [TestCase(2101)]
        [TestCase(2110)]
        [TestCase(2500)]
        public void Constructor_PassYearLessThan1900OrAbove2100_ThrowArgumentOutOfRangeException(int year)
        {
            TestDelegate constructor = () => new AttendanceListData(Mock.Of<IDaysOffData>(), GetListOfPeople(), Month.January, year);

            Assert.That(constructor, Throws.InstanceOf<ArgumentOutOfRangeException>());
        }

        [TestCase(1900)]
        [TestCase(2100)]
        public void Constructor_PassYear1900And2100_DoNotThrowArgumentOutOfRangeException(int year)
        {
            TestDelegate constructor = () => new AttendanceListData(Mock.Of<IDaysOffData>(), GetListOfPeople(), Month.January, year);

            Assert.That(constructor, Throws.Nothing);
        }

        [Test]
        public void Constructor_PassFebruary2019_CreateListOf28Days()
        {
            AttendanceListData monthData = new AttendanceListData(Mock.Of<IDaysOffData>(), GetListOfPeople(), Month.February, 2019);

            Assert.That(monthData.Days.Count, Is.EqualTo(28));
        }

        [Test]
        public void Constructor_PassFebruary2019_ListOfDaysDoNotContainAnyNull()
        {
            AttendanceListData monthData = new AttendanceListData(Mock.Of<IDaysOffData>(), GetListOfPeople(), Month.February, 2019);

            CollectionAssert.AllItemsAreNotNull(monthData.Days);
        }

        [Test]
        public void Constructor_PassFebruary2019_FirstDayInFebruaryIsFriday()
        {
            AttendanceListData monthData = new AttendanceListData(Mock.Of<IDaysOffData>(), GetListOfPeople(), Month.February, 2019);
            IDay firstDay = monthData.Days.FirstOrDefault();

            Assert.That(firstDay.DayOfWeek, Is.Not.Null.And.EqualTo(DayOfWeek.Friday));
        }

        [Test]
        public void Constructor_PassFebruary2019_LastDayInFebruaryIsThursday()
        {
            AttendanceListData monthData = new AttendanceListData(Mock.Of<IDaysOffData>(), GetListOfPeople(), Month.February, 2019);
            IDay lastDay = monthData.Days.LastOrDefault();

            Assert.That(lastDay.DayOfWeek, Is.Not.Null.And.EqualTo(DayOfWeek.Thursday));
        }

        [Test]
        public void Constructor_PassFebruary2020LeapYear_CreateListOf29Days()
        {
            AttendanceListData monthData = new AttendanceListData(Mock.Of<IDaysOffData>(), GetListOfPeople(), Month.February, 2020);

            Assert.That(monthData.Days.Count, Is.EqualTo(29));
        }

        [Test]
        public void Constructor_PassFebruary2020LeapYear_FirstDayIsSaturday()
        {
            AttendanceListData monthData = new AttendanceListData(Mock.Of<IDaysOffData>(), GetListOfPeople(), Month.February, 2020);
            IDay firstDay = monthData.Days.FirstOrDefault();

            Assert.That(firstDay.DayOfWeek, Is.Not.Null.And.EqualTo(DayOfWeek.Saturday));
        }

        [Test]
        public void Constructor_PassFebruary2020LeapYear_LastDayIsSaturday()
        {
            AttendanceListData monthData = new AttendanceListData(Mock.Of<IDaysOffData>(), GetListOfPeople(), Month.February, 2020);
            IDay lastDay = monthData.Days.LastOrDefault();

            Assert.That(lastDay.DayOfWeek, Is.Not.Null.And.EqualTo(DayOfWeek.Saturday));
        }

        [Test]
        public void Constructor_PassAugust2024_CreateListOf31Days()
        {
            AttendanceListData monthData = new AttendanceListData(Mock.Of<IDaysOffData>(), GetListOfPeople(), Month.August, 2024);

            Assert.That(monthData.Days.Count, Is.EqualTo(31));
        }

        [Test]
        public void Constructor_PassAugust2024_FirstDayIsThursday()
        {
            AttendanceListData monthData = new AttendanceListData(Mock.Of<IDaysOffData>(), GetListOfPeople(), Month.August, 2024);
            IDay firstDay = monthData.Days.FirstOrDefault();

            Assert.That(firstDay.DayOfWeek, Is.Not.Null.And.EqualTo(DayOfWeek.Thursday));
        }

        [Test]
        public void Constructor_PassAugust2024_LastDayIsSaturday()
        {
            AttendanceListData monthData = new AttendanceListData(Mock.Of<IDaysOffData>(), GetListOfPeople(), Month.August, 2024);
            IDay lastDay = monthData.Days.LastOrDefault();

            Assert.That(lastDay.DayOfWeek, Is.Not.Null.And.EqualTo(DayOfWeek.Saturday));
        }

        [Test]
        public void Constructor_January2019_FirstDayIsNewYearsDay()
        {
            IDaysOffData daysOff = Mock.Of<IDaysOffData>(d => d.GetDaysOff(Month.January) == new List<IDayOff>
                                                         {
                                                             Mock.Of<IDayOff>(x => x.Date == new DateTime(2019, 1, 1) && x.Holiday == Holiday.NewYearsDay),
                                                             Mock.Of<IDayOff>(x => x.Date == new DateTime(2019, 1, 6) && x.Holiday == Holiday.Epiphany),
                                                         });
            AttendanceListData monthData = new AttendanceListData(daysOff, GetListOfPeople(), Month.January, 2019);
            IDay day = monthData.Days[0];

            Assert.That(day.Holiday, Is.EqualTo(Holiday.NewYearsDay));
        }

        [Test]
        public void Constructor_January2019_SixthDayIsEpiphany()
        {
            IDaysOffData daysOff = Mock.Of<IDaysOffData>(d => d.GetDaysOff(Month.January) == new List<IDayOff>
                                                         {
                                                             Mock.Of<IDayOff>(x => x.Date == new DateTime(2019, 1, 1) && x.Holiday == Holiday.NewYearsDay),
                                                             Mock.Of<IDayOff>(x => x.Date == new DateTime(2019, 1, 6) && x.Holiday == Holiday.Epiphany),
                                                         });
            AttendanceListData monthData = new AttendanceListData(daysOff, GetListOfPeople(), Month.January, 2019);
            IDay day = monthData.Days[5];

            Assert.That(day.Holiday, Is.EqualTo(Holiday.Epiphany));
        }

        [Test]
        public void Constructor_June2019_NinthDayIsDescendOfTheHolySpirit()
        {
            IDaysOffData daysOff = Mock.Of<IDaysOffData>(d => d.GetDaysOff(Month.January) == new List<IDayOff>
                                                         {
                                                             Mock.Of<IDayOff>(x => x.Date == new DateTime(2019, 8, 9) && x.Holiday == Holiday.DescendOfTheHolySpirit),
                                                             Mock.Of<IDayOff>(x => x.Date == new DateTime(2019, 8, 20) && x.Holiday == Holiday.CorpusChristiDay),
                                                         });
            AttendanceListData monthData = new AttendanceListData(daysOff, GetListOfPeople(), Month.January, 2019);
            IDay day = monthData.Days[8];

            Assert.That(day.Holiday, Is.EqualTo(Holiday.DescendOfTheHolySpirit));
        }

        [Test]
        public void Constructor_June2019_TwentythDayIsCorpusChristiDay()
        {
            IDaysOffData daysOff = Mock.Of<IDaysOffData>(d => d.GetDaysOff(Month.January) == new List<IDayOff>
                                                         {
                                                             Mock.Of<IDayOff>(x => x.Date == new DateTime(2019, 8, 9) && x.Holiday == Holiday.DescendOfTheHolySpirit),
                                                             Mock.Of<IDayOff>(x => x.Date == new DateTime(2019, 8, 20) && x.Holiday == Holiday.CorpusChristiDay),
                                                         });
            AttendanceListData monthData = new AttendanceListData(daysOff, GetListOfPeople(), Month.January, 2019);
            IDay day = monthData.Days[19];

            Assert.That(day.Holiday, Is.EqualTo(Holiday.CorpusChristiDay));
        }

        [Test]
        public void Constructor_June2019_FourteenthDayIsNotAHoliday()
        {
            AttendanceListData monthData = new AttendanceListData(Mock.Of<IDaysOffData>(), GetListOfPeople(), Month.January, 2019);
            IDay day = monthData.Days[13];

            Assert.That(day.Holiday, Is.EqualTo(Holiday.None));
        }

        private IList<IPerson> GetListOfPeople()
        {
            return new List<IPerson>
            {
                Mock.Of<IPerson>(p => p.FirstName == "James" && p.LastName == "Hunt"),
                Mock.Of<IPerson>(p => p.FirstName == "William" && p.LastName == "Jefferson"),
                Mock.Of<IPerson>(p => p.FirstName == "Ryan" && p.LastName == "Carroll")
            };

        }
    }
}
