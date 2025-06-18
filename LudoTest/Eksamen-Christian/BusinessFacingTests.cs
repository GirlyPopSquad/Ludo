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
        public void GivenLoggedInUser_WhenUploadingADocumentToCase_ThenSuccessMessageIsShown()
        {
            // Arrange
            var user = new LoggedInUser();
            var casePage = new CaseDetailsPage(user);
            casePage.OpenCase("Den der sag der du ved");

            // Act
            casePage.UploadDocument("rapport.pdf");

            // Assert
            Assert.Equal("Dokumentet er uploadet", casePage.StatusMessage);
        }
    }
}
