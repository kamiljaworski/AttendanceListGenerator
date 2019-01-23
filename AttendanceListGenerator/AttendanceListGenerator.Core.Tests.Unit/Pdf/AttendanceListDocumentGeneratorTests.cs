using AttendanceListGenerator.Core.Data;
using AttendanceListGenerator.Core.Pdf;
using MigraDoc.DocumentObjectModel;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace AttendanceListGenerator.Core.Tests.Unit.Pdf
{
    class AttendanceListDocumentGeneratorTests
    {
        [Test]
        public void GenerateDocument_PassValidData_GeneratesDocumentWithOneSection()
        {
            AttendanceListDocumentGenerator documentGenerator = GetAttendanceListDocumentGenerator(); ;

            Document document = documentGenerator.GenerateDocument();

            Assert.That(document.Sections.Count, Is.EqualTo(1));
        }

        [Test]
        public void GenerateDocument_PassValidData_GeneratesDocumentWithLandscapeSectionOrientation()
        {
            AttendanceListDocumentGenerator documentGenerator = GetAttendanceListDocumentGenerator();

            Document document = documentGenerator.GenerateDocument();

            Assert.That(document.Sections[0].PageSetup.Orientation, Is.EqualTo(Orientation.Landscape));
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

            ILocalizedNames names = Mock.Of<ILocalizedNames>();

            return new AttendanceListDocumentGenerator(stubAttendanceListData, names);
        }
    }
}
