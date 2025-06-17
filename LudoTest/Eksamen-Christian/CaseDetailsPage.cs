

namespace LudoTest.Eksamen
{
    internal class CaseDetailsPage
    {
        private UserSession user;

        public CaseDetailsPage(UserSession user)
        {
            this.user = user;
        }

        public IEnumerable<char>? StatusMessage { get; internal set; }

        internal void UploadDocument(string v1, string v2)
        {
            throw new NotImplementedException();
        }
    }
}