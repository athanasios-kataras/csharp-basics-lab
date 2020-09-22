using System.Collections.Generic;
using System.Linq;
using System;

namespace Microsoft.CSharp.Basics.CleanCode
{
    /// <summary>
    /// Represents a single speaker
    /// </summary>
    public class Speaker
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int? Experience { get; set; }
        public bool HasBlog { get; set; }
        public string BlogURL { get; set; }
        public WebBrowser Browser { get; set; }
        public List<string> Certifications { get; set; }
        public string Employer { get; set; }
        public int RegistrationFee { get; set; }
        public List<Session> Sessions { get; set; }

        /// <summary>
        /// Register a speaker
        /// </summary>
        /// <returns>speakerID</returns>
        public RegisterResponse Register(IRepository repository)
        {
            int? speakerId = null;

            if (string.IsNullOrWhiteSpace(FirstName))
            {
                return new RegisterResponse(RegisterError.FirstNameRequired);
            }

            if (string.IsNullOrWhiteSpace(LastName))
            {
                return new RegisterResponse(RegisterError.LastNameRequired);
            }

            if (string.IsNullOrWhiteSpace(Email))
            {
                return new RegisterResponse(RegisterError.EmailRequired);
            }

            if (!IsGoodSpeaker())
            {
                return new RegisterResponse(RegisterError.SpeakerDoesNotMeetStandards);
            }

            if (Sessions.Count() == 0)
            {
                return new RegisterResponse(RegisterError.NoSessionsProvided);
            }

            if (!isApprovedSpeaker())
            {
                return new RegisterResponse(RegisterError.NoSessionsApproved);
            }

            CalculateRegistrationFee();

            try
            {
                speakerId = repository.SaveSpeaker(this);
            }
            catch
            {

            }

            return new RegisterResponse((int)speakerId);
        }

        private bool IsGoodSpeaker()
        {
            var employers = new List<string>() { "Pluralsight", "Microsoft", "Google" };
            bool isGoodSpeaker = Experience > 10 || HasBlog || Certifications.Count() > 3 || employers.Contains(Employer);

            if (!isGoodSpeaker)
            {
                string emailDomain = Email.Split('@').Last();
                var domains = new List<string>() { "aol.com", "prodigy.com", "compuserve.com" };
                isGoodSpeaker = domains.Contains(emailDomain) || (Browser.Name == WebBrowser.BrowserName.InternetExplorer && Browser.MajorVersion < 9);
            }

            return isGoodSpeaker;
        }

        private bool isApprovedSpeaker()
        {
            bool isApproved = false;

            foreach (var session in Sessions)
            {
                var oldTech = new List<string>() { "Cobol", "Punch Cards", "Commodore", "VBScript" };
                foreach (var tech in oldTech)
                {
                    if (session.Title.Contains(tech) || session.Description.Contains(tech))
                    {
                        session.Approved = false;
                        break;
                    }
                    else
                    {
                        session.Approved = true;
                        isApproved = true;
                    }
                }
            }

            return isApproved;
        }

        private void CalculateRegistrationFee()
        {
            if (Experience <= 1)
            {
                RegistrationFee = 500;
            }
            else if (Experience >= 2 && Experience <= 3)
            {
                RegistrationFee = 250;
            }
            else if (Experience >= 4 && Experience <= 5)
            {
                RegistrationFee = 100;
            }
            else if (Experience >= 6 && Experience <= 9)
            {
                RegistrationFee = 50;
            }
            else
            {
                RegistrationFee = 0;
            }
        }
    }
}