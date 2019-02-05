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
        public void Constructor_CorrectMonth_ObjectsMonthIsEqualToGivenMonth(Month month)
        {
            IDaysOffData daysOff = Mock.Of<IDaysOffData>(d => d.Year == 2019);

            AttendanceListData listData = new AttendanceListData(daysOff, GetListOfPeople(), month, 2019);

            Assert.That(listData.Month, Is.EqualTo(month));
        }

        [TestCase(2019)]
        [TestCase(2018)]
        [TestCase(2022)]
        [TestCase(2010)]
        public void Constructor_CorrectYear_ObjectsYearIsEqualToGivenYear(int year)
        {
            IDaysOffData daysOff = Mock.Of<IDaysOffData>(d => d.Year == year);

            AttendanceListData listData = new AttendanceListData(daysOff, GetListOfPeople(), Month.January, year);

            Assert.That(listData.Year, Is.EqualTo(year));
        }

        [Test]
        public void Constructor_CorrectPeopleList_ObjectsPeopleListIsEqualToGivenList()
        {
            IDaysOffData daysOff = Mock.Of<IDaysOffData>(d => d.Year == 2019);
            IList<IPerson> people = GetListOfPeople();

            AttendanceListData monthData = new AttendanceListData(daysOff, people, Month.January, 2019);

            Assert.That(monthData.People, Is.EqualTo(people));
        }

        [Test]
        public void Constructor_EmptyPeopleList_ThrowsArgumentException()
        {
            IDaysOffData daysOff = Mock.Of<IDaysOffData>(d => d.Year == 2019);
            IList<IPerson> people = new List<IPerson>();

            TestDelegate constructor = () => new AttendanceListData(daysOff, people, Month.January, 2019);

            Assert.That(constructor, Throws.ArgumentException);
        }

        [Test]
        public void Constructor_ListOf8People_ThrowsArgumentException()
        {
            IDaysOffData daysOff = Mock.Of<IDaysOffData>(d => d.Year == 2019);
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

            TestDelegate constructor = () => new AttendanceListData(daysOff, people, Month.January, 2019);

            Assert.That(constructor, Throws.ArgumentException);
        }

        [Test]
        public void Constructor_NoneMonth_ThrowsArgumentException()
        {
            IDaysOffData daysOff = Mock.Of<IDaysOffData>(d => d.Year == 2019);

            TestDelegate constructor = () => new AttendanceListData(daysOff, GetListOfPeople(), Month.None, 2019);

            Assert.That(constructor, Throws.ArgumentException);
        }

        [Test]
        public void Constructor_NullPeopleList_ThrowsArgumentException()
        {
            IDaysOffData daysOff = Mock.Of<IDaysOffData>(d => d.Year == 2019);

            TestDelegate constructor = () => new AttendanceListData(daysOff, null, Month.January, 2019);

            Assert.That(constructor, Throws.ArgumentException);
        }

        [Test]
        public void Constructor_NullIDaysOffData_ThrowsArgumentException()
        {
            TestDelegate constructor = () => new AttendanceListData(null, GetListOfPeople(), Month.January, 2019);

            Assert.That(constructor, Throws.ArgumentException);
        }

        [Test]
        public void Constructor_IDaysOffDataWithAnotherYearThanPassedInConstructor_ThrowsArgumentException()
        {
            IDaysOffData daysOff = Mock.Of<IDaysOffData>(d => d.Year == 2018);

            TestDelegate constructor = () => new AttendanceListData(daysOff, GetListOfPeople(), Month.January, 2019);

            Assert.That(constructor, Throws.ArgumentException);
        }

        [TestCase(1899)]
        [TestCase(1800)]
        [TestCase(1600)]
        [TestCase(2101)]
        [TestCase(2110)]
        [TestCase(2500)]
        public void Constructor_YearLessThan1900OrAbove2100_ThrowsArgumentOutOfRangeException(int year)
        {
            IDaysOffData daysOff = Mock.Of<IDaysOffData>(d => d.Year == year);

            TestDelegate constructor = () => new AttendanceListData(daysOff, GetListOfPeople(), Month.January, year);

            Assert.That(constructor, Throws.InstanceOf<ArgumentOutOfRangeException>());
        }

        [TestCase(1900)]
        [TestCase(2100)]
        public void Constructor_Year1900And2100_DoesNotThrowArgumentOutOfRangeException(int year)
        {
            IDaysOffData daysOff = Mock.Of<IDaysOffData>(d => d.Year == year);

            TestDelegate constructor = () => new AttendanceListData(daysOff, GetListOfPeople(), Month.January, year);

            Assert.That(constructor, Throws.Nothing);
        }

        [Test]
        public void Constructor_February2019_CreatesListOf28Days()
        {
            AttendanceListData listData = GetAttendanceListData();

            Assert.That(listData.Days.Count, Is.EqualTo(28));
        }

        [Test]
        public void Constructor_February2019_DaysListDoesNotContainAnyNull()
        {
            AttendanceListData listData = GetAttendanceListData();

            CollectionAssert.AllItemsAreNotNull(listData.Days);
        }

        [Test]
        public void Constructor_February2019_FirstDayInFebruaryIsFriday()
        {
            AttendanceListData listData = GetAttendanceListData();

            IDay firstDay = listData.Days.FirstOrDefault();

            Assert.That(firstDay.DayOfWeek, Is.Not.Null.And.EqualTo(DayOfWeek.Friday));
        }

        [Test]
        public void Constructor_February2019_LastDayInFebruaryIsThursday()
        {
            AttendanceListData listData = GetAttendanceListData();

            IDay lastDay = listData.Days.LastOrDefault();

            Assert.That(lastDay.DayOfWeek, Is.Not.Null.And.EqualTo(DayOfWeek.Thursday));
        }

        [Test]
        public void Constructor_February2020LeapYear_CreateListOf29Days()
        {
            IDaysOffData daysOff = Mock.Of<IDaysOffData>(d => d.Year == 2020);

            AttendanceListData listData = new AttendanceListData(daysOff, GetListOfPeople(), Month.February, 2020);

            Assert.That(listData.Days.Count, Is.EqualTo(29));
        }

        [Test]
        public void Constructor_February2020LeapYear_FirstDayIsSaturday()
        {
            IDaysOffData daysOff = Mock.Of<IDaysOffData>(d => d.Year == 2020);

            AttendanceListData listData = new AttendanceListData(daysOff, GetListOfPeople(), Month.February, 2020);
            IDay firstDay = listData.Days.FirstOrDefault();

            Assert.That(firstDay.DayOfWeek, Is.Not.Null.And.EqualTo(DayOfWeek.Saturday));
        }

        [Test]
        public void Constructor_February2020LeapYear_LastDayIsSaturday()
        {
            IDaysOffData daysOff = Mock.Of<IDaysOffData>(d => d.Year == 2020);

            AttendanceListData listData = new AttendanceListData(daysOff, GetListOfPeople(), Month.February, 2020);
            IDay lastDay = listData.Days.LastOrDefault();

            Assert.That(lastDay.DayOfWeek, Is.Not.Null.And.EqualTo(DayOfWeek.Saturday));
        }

        [Test]
        public void Constructor_August2024_CreatesListOf31Days()
        {
            IDaysOffData daysOff = Mock.Of<IDaysOffData>(d => d.Year == 2024);

            AttendanceListData listData = new AttendanceListData(daysOff, GetListOfPeople(), Month.August, 2024);

            Assert.That(listData.Days.Count, Is.EqualTo(31));
        }

        [Test]
        public void Constructor_August2024_FirstDayIsThursday()
        {
            IDaysOffData daysOff = Mock.Of<IDaysOffData>(d => d.Year == 2024);

            AttendanceListData listData = new AttendanceListData(daysOff, GetListOfPeople(), Month.August, 2024);
            IDay firstDay = listData.Days.FirstOrDefault();

            Assert.That(firstDay.DayOfWeek, Is.Not.Null.And.EqualTo(DayOfWeek.Thursday));
        }

        [Test]
        public void Constructor_August2024_LastDayIsSaturday()
        {
            IDaysOffData daysOff = Mock.Of<IDaysOffData>(d => d.Year == 2024);

            AttendanceListData listData = new AttendanceListData(daysOff, GetListOfPeople(), Month.August, 2024);
            IDay lastDay = listData.Days.LastOrDefault();

            Assert.That(lastDay.DayOfWeek, Is.Not.Null.And.EqualTo(DayOfWeek.Saturday));
        }

        [Test]
        public void Constructor_January2019_FirstDayIsNewYearsDay()
        {
            IDaysOffData daysOff = Mock.Of<IDaysOffData>(d => d.Year == 2019 &&
                                                         d.GetDaysOff(Month.January) == new List<IDayOff>
                                                         {
                                                             Mock.Of<IDayOff>(x => x.Date == new DateTime(2019, 1, 1) && x.Holiday == Holiday.NewYearsDay),
                                                             Mock.Of<IDayOff>(x => x.Date == new DateTime(2019, 1, 6) && x.Holiday == Holiday.Epiphany),
                                                         });
            AttendanceListData listData = new AttendanceListData(daysOff, GetListOfPeople(), Month.January, 2019);
            IDay day = listData.Days.FirstOrDefault();

            Assert.That(day.Holiday, Is.EqualTo(Holiday.NewYearsDay));
        }

        [Test]
        public void Constructor_January2019_SixthDayIsEpiphany()
        {
            IDaysOffData daysOff = Mock.Of<IDaysOffData>(d => d.Year == 2019 &&
                                                         d.GetDaysOff(Month.January) == new List<IDayOff>
                                                         {
                                                             Mock.Of<IDayOff>(x => x.Date == new DateTime(2019, 1, 1) && x.Holiday == Holiday.NewYearsDay),
                                                             Mock.Of<IDayOff>(x => x.Date == new DateTime(2019, 1, 6) && x.Holiday == Holiday.Epiphany),
                                                         });
            AttendanceListData listData = new AttendanceListData(daysOff, GetListOfPeople(), Month.January, 2019);
            IDay day = listData.Days[5];

            Assert.That(day.Holiday, Is.EqualTo(Holiday.Epiphany));
        }

        [Test]
        public void Constructor_June2019_NinthDayIsDescendOfTheHolySpirit()
        {
            IDaysOffData daysOff = Mock.Of<IDaysOffData>(d => d.Year == 2019 &&
                                                         d.GetDaysOff(Month.January) == new List<IDayOff>
                                                         {
                                                             Mock.Of<IDayOff>(x => x.Date == new DateTime(2019, 8, 9) && x.Holiday == Holiday.DescendOfTheHolySpirit),
                                                             Mock.Of<IDayOff>(x => x.Date == new DateTime(2019, 8, 20) && x.Holiday == Holiday.CorpusChristiDay),
                                                         });
            AttendanceListData listData = new AttendanceListData(daysOff, GetListOfPeople(), Month.January, 2019);
            IDay day = listData.Days[8];

            Assert.That(day.Holiday, Is.EqualTo(Holiday.DescendOfTheHolySpirit));
        }

        [Test]
        public void Constructor_June2019_TwentythDayIsCorpusChristiDay()
        {
            IDaysOffData daysOff = Mock.Of<IDaysOffData>(d => d.Year == 2019 &&
                                                         d.GetDaysOff(Month.January) == new List<IDayOff>
                                                         {
                                                             Mock.Of<IDayOff>(x => x.Date == new DateTime(2019, 8, 9) && x.Holiday == Holiday.DescendOfTheHolySpirit),
                                                             Mock.Of<IDayOff>(x => x.Date == new DateTime(2019, 8, 20) && x.Holiday == Holiday.CorpusChristiDay),
                                                         });
            AttendanceListData listData = new AttendanceListData(daysOff, GetListOfPeople(), Month.January, 2019);
            IDay day = listData.Days[19];

            Assert.That(day.Holiday, Is.EqualTo(Holiday.CorpusChristiDay));
        }

        [Test]
        public void Constructor_June2019_FourteenthDayIsNotAHoliday()
        {
            IDaysOffData daysOff = Mock.Of<IDaysOffData>(d => d.Year == 2019);

            AttendanceListData listData = new AttendanceListData(daysOff, GetListOfPeople(), Month.January, 2019);
            IDay day = listData.Days[13];

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

        private AttendanceListData GetAttendanceListData()
        {
            IDaysOffData daysOff = Mock.Of<IDaysOffData>(d => d.Year == 2019);
            return new AttendanceListData(daysOff, GetListOfPeople(), Month.February, 2019);
        }
    }
}
