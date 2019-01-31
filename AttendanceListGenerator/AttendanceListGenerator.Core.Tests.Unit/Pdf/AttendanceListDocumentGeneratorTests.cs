using AttendanceListGenerator.Core.Data;
using AttendanceListGenerator.Core.Pdf;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace AttendanceListGenerator.Core.Tests.Unit.Pdf
{
    class AttendanceListDocumentGeneratorTests
    {
        [Test]
        public void Constructor_PassNullIAttendanceListDataArgument_ThrowsArgumentNullException()
        {
            TestDelegate constructor = () => new AttendanceListDocumentGenerator(null, Mock.Of<ILocalizedNames>());

            Assert.That(constructor, Throws.ArgumentNullException);
        }

        [Test]
        public void Constructor_PassNullILocalizedNamesArgument_ThrowsArgumentNullException()
        {
            TestDelegate constructor = () => new AttendanceListDocumentGenerator(Mock.Of<IAttendanceListData>(), null);

            Assert.That(constructor, Throws.ArgumentNullException);
        }

        [Test]
        public void Constructor_PassNullArguments_ThrowsArgumentNullException()
        {
            TestDelegate constructor = () => new AttendanceListDocumentGenerator(null, null);

            Assert.That(constructor, Throws.ArgumentNullException);
        }

        [Test]
        public void GenerateDocument_PassValidData_GeneratesDocumentWithOneSection()
        {
            AttendanceListDocumentGenerator documentGenerator = GetAttendanceListDocumentGenerator();

            Document document = documentGenerator.GenerateDocument();

            int numberOfSections = document.Sections.Count;
            Assert.That(numberOfSections, Is.EqualTo(1));
        }

        [Test]
        public void GenerateDocument_PassValidData_GeneratesDocumentWithLandscapeSectionOrientation()
        {
            AttendanceListDocumentGenerator documentGenerator = GetAttendanceListDocumentGenerator();

            Document document = documentGenerator.GenerateDocument();

            Orientation orientation = document.Sections[0].PageSetup.Orientation;
            Assert.That(orientation, Is.EqualTo(Orientation.Landscape));
        }

        [Test]
        public void GenerateDocument_PassValidData_GeneratesParagraphWithMonthNameAndYearInTheSection()
        {
            AttendanceListDocumentGenerator documentGenerator = GetAttendanceListDocumentGenerator();

            Document document = documentGenerator.GenerateDocument();

            string content = ((Text)document.Sections[0].LastParagraph.Elements[0]).Content;
            Assert.That(content, Is.EqualTo("January 2019"));
        }

        [Test]
        public void GenerateDocument_PassValidData_GeneratesTable()
        {
            AttendanceListDocumentGenerator documentGenerator = GetAttendanceListDocumentGenerator();

            Document document = documentGenerator.GenerateDocument();

            Table table = document.Sections[0].LastTable;
            Assert.That(table, Is.Not.Null);
        }

        [Test]
        public void GenerateDocument_PassValidData_GeneratesTableWith9Columns()
        {
            AttendanceListDocumentGenerator documentGenerator = GetAttendanceListDocumentGenerator();

            Document document = documentGenerator.GenerateDocument();

            int numberOfColumns = document.Sections[0].LastTable.Columns.Count;
            Assert.That(numberOfColumns, Is.EqualTo(9));
        }

        [Test]
        public void GenerateDocument_PassValidData_TablesFirstFullnameIsEqualToPassedFullname()
        {
            AttendanceListDocumentGenerator documentGenerator = GetAttendanceListDocumentGenerator();

            Document document = documentGenerator.GenerateDocument();

            string content = ((Text)((Paragraph)document.Sections[0].LastTable.Rows[0].Cells[2].Elements[0]).Elements[0]).Content;
            Assert.That(content, Is.EqualTo("James Hunt"));
        }

        [Test]
        public void GenerateDocument_PassValidData_TablesThirdFullnameIsEqualToPassedFullname()
        {
            AttendanceListDocumentGenerator documentGenerator = GetAttendanceListDocumentGenerator();

            Document document = documentGenerator.GenerateDocument();

            string content = ((Text)((Paragraph)document.Sections[0].LastTable.Rows[0].Cells[4].Elements[0]).Elements[0]).Content;
            Assert.That(content, Is.EqualTo("Ryan Carroll"));
        }

        [Test]
        public void GenerateDocument_PassValidData_TablesFourthFullnameDoesNotExistAndThrowsException()
        {
            AttendanceListDocumentGenerator documentGenerator = GetAttendanceListDocumentGenerator();

            Document document = documentGenerator.GenerateDocument();

            string content;
            TestDelegate getContent = () => content = ((Text)((Paragraph)document.Sections[0].LastTable.Rows[0].Cells[6].Elements[0]).Elements[0]).Content;
            Assert.That(getContent, Throws.InstanceOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public void GenerateDocument_PassValidData_GeneratesTableWith32Rows()
        {
            AttendanceListDocumentGenerator documentGenerator = GetAttendanceListDocumentGenerator();

            Document document = documentGenerator.GenerateDocument();

            int numberOfRows = document.Sections[0].LastTable.Rows.Count;
            Assert.That(numberOfRows, Is.EqualTo(32));
        }

        [Test]
        public void GenerateDocument_PassValidData_TablesFourthRowsFirstColumnIsEqualTo4()
        {
            AttendanceListDocumentGenerator documentGenerator = GetAttendanceListDocumentGenerator();

            Document document = documentGenerator.GenerateDocument();

            string content = ((Text)((Paragraph)document.Sections[0].LastTable.Rows[4].Cells[0].Elements[0]).Elements[0]).Content;
            Assert.That(content, Is.EqualTo("4."));
        }

        [Test]
        public void GenerateDocument_PassValidData_TablesThirtySecondRowsFirstColumnIsEqualTo31()
        {
            AttendanceListDocumentGenerator documentGenerator = GetAttendanceListDocumentGenerator();

            Document document = documentGenerator.GenerateDocument();

            string content = ((Text)((Paragraph)document.Sections[0].LastTable.Rows[31].Cells[0].Elements[0]).Elements[0]).Content;
            Assert.That(content, Is.EqualTo("31."));
        }

        [Test]
        public void GenerateDocument_PassValidData_TablesFourthRowsSecondColumnIsEqualToFridayAbbreviation()
        {
            AttendanceListDocumentGenerator documentGenerator = GetAttendanceListDocumentGenerator();

            Document document = documentGenerator.GenerateDocument();

            string content = ((Text)((Paragraph)document.Sections[0].LastTable.Rows[4].Cells[1].Elements[0]).Elements[0]).Content;
            Assert.That(content, Is.EqualTo("Fri."));
        }

        [Test]
        public void GenerateDocument_PassValidData_TablesThirtySecondRowsSecondColumnIsEqualToThursdayAbbreviation()
        {
            AttendanceListDocumentGenerator documentGenerator = GetAttendanceListDocumentGenerator();

            Document document = documentGenerator.GenerateDocument();

            string content = ((Text)((Paragraph)document.Sections[0].LastTable.Rows[31].Cells[1].Elements[0]).Elements[0]).Content;
            Assert.That(content, Is.EqualTo("Thu."));
        }

        [Test]
        public void GenerateDocument_PassValidData_TablesHeadingBackgroundColorIsValid()
        {
            AttendanceListDocumentGenerator documentGenerator = GetAttendanceListDocumentGenerator();

            Document document = documentGenerator.GenerateDocument();

            Color backgroundColor = document.Sections[0].LastTable.Rows[0].Shading.Color;
            Assert.That(backgroundColor, Is.EqualTo(documentGenerator.FullnamesBackgroundColor));
        }

        [Test]
        public void GenerateDocument_PassValidData_TablesSunday13ThJanuaryColorIsValid()
        {
            AttendanceListDocumentGenerator documentGenerator = GetAttendanceListDocumentGenerator();

            Document document = documentGenerator.GenerateDocument();

            Color backgroundColor = document.Sections[0].LastTable.Rows[13].Shading.Color;
            Assert.That(backgroundColor, Is.EqualTo(documentGenerator.SundayBackgroundColor));
        }

        [Test]
        public void GenerateDocument_PassValidData_TablesSaturday19ThJanuaryColorIsValid()
        {
            AttendanceListDocumentGenerator documentGenerator = GetAttendanceListDocumentGenerator();

            Document document = documentGenerator.GenerateDocument();

            Color backgroundColor = document.Sections[0].LastTable.Rows[19].Shading.Color;
            Assert.That(backgroundColor, Is.EqualTo(documentGenerator.SaturdayBackgroundColor));
        }

        [Test]
        public void GenerateDocument_PassValidDataWithFalseAddColorsProperty_TablesHeadingBackgroundColorIsZero()
        {
            AttendanceListDocumentGenerator documentGenerator = GetAttendanceListDocumentGenerator();
            documentGenerator.CanAddColorsToTheDocument = false;

            Document document = documentGenerator.GenerateDocument();

            Color backgroundColor = document.Sections[0].LastTable.Rows[0].Shading.Color;
            Assert.That(backgroundColor.RGB, Is.EqualTo(0));
        }

        [Test]
        public void GenerateDocument_PassValidDataWithFalseAddColorsProperty_TablesSunday13ThJanuaryColorIsZero()
        {
            AttendanceListDocumentGenerator documentGenerator = GetAttendanceListDocumentGenerator();
            documentGenerator.CanAddColorsToTheDocument = false;

            Document document = documentGenerator.GenerateDocument();

            Color backgroundColor = document.Sections[0].LastTable.Rows[13].Shading.Color;
            Assert.That(backgroundColor.RGB, Is.EqualTo(0));
        }

        [Test]
        public void GenerateDocument_PassValidDataWithFalseAddColorsProperty_TablesSaturday19ThJanuaryColorIsZero()
        {
            AttendanceListDocumentGenerator documentGenerator = GetAttendanceListDocumentGenerator();
            documentGenerator.CanAddColorsToTheDocument = false;

            Document document = documentGenerator.GenerateDocument();

            Color backgroundColor = document.Sections[0].LastTable.Rows[19].Shading.Color;
            Assert.That(backgroundColor.RGB, Is.EqualTo(0));
        }

        [Test]
        public void GenerateDocument_PassValidDataWith_TablesSunday20thJanuaryAllColumnsHadSundayText()
        {
            AttendanceListDocumentGenerator documentGenerator = GetAttendanceListDocumentGenerator();

            Document document = documentGenerator.GenerateDocument();

            string firstColumn = ((Text)((Paragraph)document.Sections[0].LastTable.Rows[20].Cells[2].Elements[0]).Elements[0]).Content;
            string secondColumn = ((Text)((Paragraph)document.Sections[0].LastTable.Rows[20].Cells[3].Elements[0]).Elements[0]).Content;
            string thirdColumn = ((Text)((Paragraph)document.Sections[0].LastTable.Rows[20].Cells[4].Elements[0]).Elements[0]).Content;
            string fourthColumn = ((Text)((Paragraph)document.Sections[0].LastTable.Rows[20].Cells[5].Elements[0]).Elements[0]).Content;
            string fifthColumn = ((Text)((Paragraph)document.Sections[0].LastTable.Rows[20].Cells[6].Elements[0]).Elements[0]).Content;
            string sixthColumn = ((Text)((Paragraph)document.Sections[0].LastTable.Rows[20].Cells[7].Elements[0]).Elements[0]).Content;
            string seventhColumn = ((Text)((Paragraph)document.Sections[0].LastTable.Rows[20].Cells[8].Elements[0]).Elements[0]).Content;
            List<string> columnsList = new List<string> { firstColumn, secondColumn, thirdColumn, fourthColumn, fifthColumn, sixthColumn, seventhColumn };
            Assert.That(columnsList, Is.All.EqualTo("SUNDAY"));
        }

        private AttendanceListDocumentGenerator GetAttendanceListDocumentGenerator()
        {
            IList<string> fullnames = new List<string> { "James Hunt", "William Jefferson", "Ryan Carroll" };
            IList<IDay> days = new List<IDay>
            {
                Mock.Of<IDay>(d => d.DayOfMonth == 1 && d.FormattedDayOfMonth == "1." && d.DayOfWeek == DayOfWeek.Tuesday),
                Mock.Of<IDay>(d => d.DayOfMonth == 2 && d.FormattedDayOfMonth == "2." && d.DayOfWeek == DayOfWeek.Wednesday),
                Mock.Of<IDay>(d => d.DayOfMonth == 3 && d.FormattedDayOfMonth == "3." && d.DayOfWeek == DayOfWeek.Thursday),
                Mock.Of<IDay>(d => d.DayOfMonth == 4 && d.FormattedDayOfMonth == "4." && d.DayOfWeek == DayOfWeek.Friday),
                Mock.Of<IDay>(d => d.DayOfMonth == 5 && d.FormattedDayOfMonth == "5." && d.DayOfWeek == DayOfWeek.Saturday),
                Mock.Of<IDay>(d => d.DayOfMonth == 6 && d.FormattedDayOfMonth == "6." && d.DayOfWeek == DayOfWeek.Sunday),
                Mock.Of<IDay>(d => d.DayOfMonth == 7 && d.FormattedDayOfMonth == "7." && d.DayOfWeek == DayOfWeek.Monday),
                Mock.Of<IDay>(d => d.DayOfMonth == 8 && d.FormattedDayOfMonth == "8." && d.DayOfWeek == DayOfWeek.Tuesday),
                Mock.Of<IDay>(d => d.DayOfMonth == 9 && d.FormattedDayOfMonth == "9." && d.DayOfWeek == DayOfWeek.Wednesday),
                Mock.Of<IDay>(d => d.DayOfMonth == 10 && d.FormattedDayOfMonth == "10." && d.DayOfWeek == DayOfWeek.Thursday),
                Mock.Of<IDay>(d => d.DayOfMonth == 11 && d.FormattedDayOfMonth == "11." && d.DayOfWeek == DayOfWeek.Friday),
                Mock.Of<IDay>(d => d.DayOfMonth == 12 && d.FormattedDayOfMonth == "12." && d.DayOfWeek == DayOfWeek.Saturday),
                Mock.Of<IDay>(d => d.DayOfMonth == 13 && d.FormattedDayOfMonth == "13." && d.DayOfWeek == DayOfWeek.Sunday),
                Mock.Of<IDay>(d => d.DayOfMonth == 14 && d.FormattedDayOfMonth == "14." && d.DayOfWeek == DayOfWeek.Monday),
                Mock.Of<IDay>(d => d.DayOfMonth == 15 && d.FormattedDayOfMonth == "15." && d.DayOfWeek == DayOfWeek.Tuesday),
                Mock.Of<IDay>(d => d.DayOfMonth == 16 && d.FormattedDayOfMonth == "16." && d.DayOfWeek == DayOfWeek.Wednesday),
                Mock.Of<IDay>(d => d.DayOfMonth == 17 && d.FormattedDayOfMonth == "17." && d.DayOfWeek == DayOfWeek.Thursday),
                Mock.Of<IDay>(d => d.DayOfMonth == 18 && d.FormattedDayOfMonth == "18." && d.DayOfWeek == DayOfWeek.Friday),
                Mock.Of<IDay>(d => d.DayOfMonth == 19 && d.FormattedDayOfMonth == "19." && d.DayOfWeek == DayOfWeek.Saturday),
                Mock.Of<IDay>(d => d.DayOfMonth == 20 && d.FormattedDayOfMonth == "20." && d.DayOfWeek == DayOfWeek.Sunday),
                Mock.Of<IDay>(d => d.DayOfMonth == 21 && d.FormattedDayOfMonth == "21." && d.DayOfWeek == DayOfWeek.Monday),
                Mock.Of<IDay>(d => d.DayOfMonth == 22 && d.FormattedDayOfMonth == "22." && d.DayOfWeek == DayOfWeek.Tuesday),
                Mock.Of<IDay>(d => d.DayOfMonth == 23 && d.FormattedDayOfMonth == "23." && d.DayOfWeek == DayOfWeek.Wednesday),
                Mock.Of<IDay>(d => d.DayOfMonth == 24 && d.FormattedDayOfMonth == "24." && d.DayOfWeek == DayOfWeek.Thursday),
                Mock.Of<IDay>(d => d.DayOfMonth == 25 && d.FormattedDayOfMonth == "25." && d.DayOfWeek == DayOfWeek.Friday),
                Mock.Of<IDay>(d => d.DayOfMonth == 26 && d.FormattedDayOfMonth == "26." && d.DayOfWeek == DayOfWeek.Saturday),
                Mock.Of<IDay>(d => d.DayOfMonth == 27 && d.FormattedDayOfMonth == "27." && d.DayOfWeek == DayOfWeek.Sunday),
                Mock.Of<IDay>(d => d.DayOfMonth == 28 && d.FormattedDayOfMonth == "28." && d.DayOfWeek == DayOfWeek.Monday),
                Mock.Of<IDay>(d => d.DayOfMonth == 29 && d.FormattedDayOfMonth == "29." && d.DayOfWeek == DayOfWeek.Tuesday),
                Mock.Of<IDay>(d => d.DayOfMonth == 30 && d.FormattedDayOfMonth == "30." && d.DayOfWeek == DayOfWeek.Wednesday),
                Mock.Of<IDay>(d => d.DayOfMonth == 31 && d.FormattedDayOfMonth == "31." && d.DayOfWeek == DayOfWeek.Thursday),
            };

            IAttendanceListData stubAttendanceListData = Mock.Of<IAttendanceListData>
                                                       (a =>
                                                        a.Fullnames == fullnames &&
                                                        a.Days == days &&
                                                        a.Month == Month.January &&
                                                        a.Year == 2019 &&
                                                        a.MaxNumberOfFullnames == 7);

            ILocalizedNames names = Mock.Of<ILocalizedNames>
                                   (n =>
                                    n.GetDocumentTitle(Month.January, 2019) == "January 2019" &&
                                    n.GetDayOfWeekAbbreviation(DayOfWeek.Monday) == "Mon." &&
                                    n.GetDayOfWeekAbbreviation(DayOfWeek.Tuesday) == "Tue." &&
                                    n.GetDayOfWeekAbbreviation(DayOfWeek.Wednesday) == "Wed." &&
                                    n.GetDayOfWeekAbbreviation(DayOfWeek.Thursday) == "Thu." &&
                                    n.GetDayOfWeekAbbreviation(DayOfWeek.Friday) == "Fri." &&
                                    n.GetDayOfWeekAbbreviation(DayOfWeek.Saturday) == "Sat." &&
                                    n.GetDayOfWeekAbbreviation(DayOfWeek.Sunday) == "Sun." &&
                                    n.GetDayOfWeekName(DayOfWeek.Sunday) == "Sunday");

            return new AttendanceListDocumentGenerator(stubAttendanceListData, names);
        }
    }
}
