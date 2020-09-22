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
            bool goodSpeaker = false;

            if (!string.IsNullOrWhiteSpace(FirstName))
            {
                if (!string.IsNullOrWhiteSpace(LastName))
                {
                    if (!string.IsNullOrWhiteSpace(Email))
                    {
                        var employers = new List<string>() { "Pluralsight", "Microsoft", "Google" };

                        goodSpeaker = Experience > 10 || HasBlog || Certifications.Count() > 3 || employers.Contains(Employer);

                        if (!goodSpeaker)
                        {
                            //need to get just the domain from the email
                            string emailDomain = Email.Split('@').Last();
                            var domains = new List<string>() { "aol.com", "prodigy.com", "compuserve.com" };
                            if (!domains.Contains(emailDomain) && (!(Browser.Name == WebBrowser.BrowserName.InternetExplorer && Browser.MajorVersion < 9)))
                            {
                                goodSpeaker = true;
                            }
                        }

                        if (goodSpeaker)
                        {
                            bool approved = false;

                            if (Sessions.Count() != 0)
                            {
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
                                            approved = true;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                return new RegisterResponse(RegisterError.NoSessionsProvided);
                            }

                            if (approved)
                            {
                                //if we got this far, the speaker is approved
                                //let's go ahead and register him/her now.
                                //First, let's calculate the registration fee. 
                                //More experienced speakers pay a lower fee.
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

                                try
                                {
                                    speakerId = repository.SaveSpeaker(this);
                                }
                                catch
                                {
                                }
                            }
                            else
                            {
                                return new RegisterResponse(RegisterError.NoSessionsApproved);
                            }
                        }
                        else
                        {
                            return new RegisterResponse(RegisterError.SpeakerDoesNotMeetStandards);
                        }
                    }
                    else
                    {
                        return new RegisterResponse(RegisterError.EmailRequired);
                    }
                }
                else
                {
                    return new RegisterResponse(RegisterError.LastNameRequired);
                }
            }
            else
            {
                return new RegisterResponse(RegisterError.FirstNameRequired);
            }

            return new RegisterResponse((int)speakerId);
        }
    }
}