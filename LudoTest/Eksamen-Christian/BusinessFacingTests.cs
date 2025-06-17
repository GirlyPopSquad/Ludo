using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudoTest.Eksamen
{
    public class BusinessFacingTests
    {
        [Fact(Skip = "reason")]
        public void User_UploadsDocumentToCase_ThenSeesSuccessMessage()
        {
            // Given
            var user = new UserSession();
            var casePage = new CaseDetailsPage(user);

            // When
            casePage.UploadDocument("sag-123", "dokument.pdf");

            // Then
            Assert.Equal("Dokumentet er uploadet.", casePage.StatusMessage);
        }
    }
}
