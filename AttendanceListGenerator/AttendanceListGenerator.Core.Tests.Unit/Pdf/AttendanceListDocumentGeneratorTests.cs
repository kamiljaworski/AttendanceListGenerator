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
            AttendanceListDocumentGenerator documentGenerator = GetAttendanceListDocumentGenerator(); ;
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

            Assert.That(content, Is.EqualTo("Styczeń 2019"));
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
        public void GenerateDocument_PassValidData_GeneratesTableWith8Columns()
        {
            AttendanceListDocumentGenerator documentGenerator = GetAttendanceListDocumentGenerator();
            Document document = documentGenerator.GenerateDocument();

            int numberOfColumns = document.Sections[0].LastTable.Columns.Count;

            Assert.That(numberOfColumns, Is.EqualTo(8));
        }

        [Test]
        public void GenerateDocument_PassValidData_TablesFirstFullnameIsEqualToPassedFullname()
        {
            AttendanceListDocumentGenerator documentGenerator = GetAttendanceListDocumentGenerator();
            Document document = documentGenerator.GenerateDocument();

            string content = ((Text)((Paragraph)document.Sections[0].LastTable.Rows[0].Cells[1].Elements[0]).Elements[0]).Content;

            Assert.That(content, Is.EqualTo("James Hunt"));
        }

        [Test]
        public void GenerateDocument_PassValidData_TablesThirdFullnameIsEqualToPassedFullname()
        {
            AttendanceListDocumentGenerator documentGenerator = GetAttendanceListDocumentGenerator();
            Document document = documentGenerator.GenerateDocument();

            string content = ((Text)((Paragraph)document.Sections[0].LastTable.Rows[0].Cells[3].Elements[0]).Elements[0]).Content;

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
        public void GenerateDocument_PassValidData_TablesFourthRowsFirstColumnIsEqualTo3()
        {
            AttendanceListDocumentGenerator documentGenerator = GetAttendanceListDocumentGenerator();
            Document document = documentGenerator.GenerateDocument();

            string content;
            content = ((Text)((Paragraph)document.Sections[0].LastTable.Rows[4].Cells[0].Elements[0]).Elements[0]).Content;

            Assert.That(content, Is.EqualTo("4."));
        }

        private AttendanceListDocumentGenerator GetAttendanceListDocumentGenerator()
        {
            IList<string> fullnames = new List<string> { "James Hunt", "William Jefferson", "Ryan Carroll" };
            IList<IDay> days = new List<IDay>
            {
                Mock.Of<IDay>(d => d.DayOfMonth == 1 && d.DayOfWeek == DayOfWeek.Tuesday),
                Mock.Of<IDay>(d => d.DayOfMonth == 2 && d.DayOfWeek == DayOfWeek.Wednesday),
                Mock.Of<IDay>(d => d.DayOfMonth == 3 && d.DayOfWeek == DayOfWeek.Thursday),
                Mock.Of<IDay>(d => d.DayOfMonth == 4 && d.DayOfWeek == DayOfWeek.Friday),
                Mock.Of<IDay>(d => d.DayOfMonth == 5 && d.DayOfWeek == DayOfWeek.Saturday),
                Mock.Of<IDay>(d => d.DayOfMonth == 6 && d.DayOfWeek == DayOfWeek.Sunday),
                Mock.Of<IDay>(d => d.DayOfMonth == 7 && d.DayOfWeek == DayOfWeek.Monday),
                Mock.Of<IDay>(d => d.DayOfMonth == 8 && d.DayOfWeek == DayOfWeek.Tuesday),
                Mock.Of<IDay>(d => d.DayOfMonth == 9 && d.DayOfWeek == DayOfWeek.Wednesday),
                Mock.Of<IDay>(d => d.DayOfMonth == 10 && d.DayOfWeek == DayOfWeek.Thursday),
                Mock.Of<IDay>(d => d.DayOfMonth == 11 && d.DayOfWeek == DayOfWeek.Friday),
                Mock.Of<IDay>(d => d.DayOfMonth == 12 && d.DayOfWeek == DayOfWeek.Saturday),
                Mock.Of<IDay>(d => d.DayOfMonth == 13 && d.DayOfWeek == DayOfWeek.Sunday),
                Mock.Of<IDay>(d => d.DayOfMonth == 14 && d.DayOfWeek == DayOfWeek.Monday),
                Mock.Of<IDay>(d => d.DayOfMonth == 15 && d.DayOfWeek == DayOfWeek.Tuesday),
                Mock.Of<IDay>(d => d.DayOfMonth == 16 && d.DayOfWeek == DayOfWeek.Wednesday),
                Mock.Of<IDay>(d => d.DayOfMonth == 17 && d.DayOfWeek == DayOfWeek.Thursday),
                Mock.Of<IDay>(d => d.DayOfMonth == 18 && d.DayOfWeek == DayOfWeek.Friday),
                Mock.Of<IDay>(d => d.DayOfMonth == 19 && d.DayOfWeek == DayOfWeek.Saturday),
                Mock.Of<IDay>(d => d.DayOfMonth == 20 && d.DayOfWeek == DayOfWeek.Sunday),
                Mock.Of<IDay>(d => d.DayOfMonth == 21 && d.DayOfWeek == DayOfWeek.Monday),
                Mock.Of<IDay>(d => d.DayOfMonth == 22 && d.DayOfWeek == DayOfWeek.Tuesday),
                Mock.Of<IDay>(d => d.DayOfMonth == 23 && d.DayOfWeek == DayOfWeek.Wednesday),
                Mock.Of<IDay>(d => d.DayOfMonth == 24 && d.DayOfWeek == DayOfWeek.Thursday),
                Mock.Of<IDay>(d => d.DayOfMonth == 25 && d.DayOfWeek == DayOfWeek.Friday),
                Mock.Of<IDay>(d => d.DayOfMonth == 26 && d.DayOfWeek == DayOfWeek.Saturday),
                Mock.Of<IDay>(d => d.DayOfMonth == 27 && d.DayOfWeek == DayOfWeek.Sunday),
                Mock.Of<IDay>(d => d.DayOfMonth == 28 && d.DayOfWeek == DayOfWeek.Monday),
                Mock.Of<IDay>(d => d.DayOfMonth == 29 && d.DayOfWeek == DayOfWeek.Tuesday),
                Mock.Of<IDay>(d => d.DayOfMonth == 30 && d.DayOfWeek == DayOfWeek.Wednesday),
                Mock.Of<IDay>(d => d.DayOfMonth == 31 && d.DayOfWeek == DayOfWeek.Thursday),
            };

            IAttendanceListData stubAttendanceListData = Mock.Of<IAttendanceListData>
                                                       (a =>
                                                        a.Fullnames == fullnames &&
                                                        a.Days == days &&
                                                        a.Month == Month.January &&
                                                        a.Year == 2019);

            ILocalizedNames names = Mock.Of<ILocalizedNames>
                                   (n =>
                                    n.GetDocumentTitle(Month.January, 2019) == "Styczeń 2019");

            return new AttendanceListDocumentGenerator(stubAttendanceListData, names);
        }
    }
}
