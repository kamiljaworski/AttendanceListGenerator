using AttendanceListGenerator.Core.Data;
using AttendanceListGenerator.Core.Pdf;
using MigraDoc.DocumentObjectModel;
using NUnit.Framework;
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
            return new AttendanceListDocumentGenerator(new AttendanceListData(fullnames, Month.January, 2019));
        }
    }
}
