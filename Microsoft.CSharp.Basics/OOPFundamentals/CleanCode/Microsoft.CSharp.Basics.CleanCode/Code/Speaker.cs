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
		public int? ExperienceLevel { get; set; }
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

		public RegisterResponse Register(IRepository repository)
		{
			if (!string.IsNullOrWhiteSpace(FirstName))
			{
				if (!string.IsNullOrWhiteSpace(LastName))
				{
					if (!string.IsNullOrWhiteSpace(Email))
					{

						int? speakerId = null;
						bool isHighlyExperienced = false;
						bool hasApprovedSessions = false;
						var technologies = new List<string>() { "Cobol", "Punch Cards", "Commodore", "VBScript" };
						var emailDomains = new List<string>() { "aol.com", "prodigy.com", "compuserve.com" };
						var employers = new List<string>() { "Pluralsight", "Microsoft", "Google" };

						isHighlyExperienced = ExperienceLevel > 10 || HasBlog || Certifications.Count() > 3 || employers.Contains(Employer);

						if (!isHighlyExperienced)
						{
							
							string emailDomain = Email.Split('@').Last();

							if (!emailDomains.Contains(emailDomain) && (!(Browser.Name == WebBrowser.BrowserName.InternetExplorer && Browser.MajorVersion < 9)))
							{
								isHighlyExperienced = true;
							}
						}

						if (isHighlyExperienced)
						{
							if (Sessions.Count() != 0)
							{
								foreach (var session in Sessions)
								{
									foreach (var tech in technologies)
									{
										if (session.Title.Contains(tech) || session.Description.Contains(tech))
										{
											session.Approved = false;
											break;
										}
										else
										{
											session.Approved = true;
											hasApprovedSessions = true;
										}
									}
								}
							}
							else
							{
								return new RegisterResponse(RegisterError.NoSessionsProvided);
							}

							if (hasApprovedSessions)
							{

								if (ExperienceLevel <= 1)
								{
									RegistrationFee = 500;
								}
								else if (ExperienceLevel >= 2 && ExperienceLevel <= 3)
								{
									RegistrationFee = 250;
								}
								else if (ExperienceLevel >= 4 && ExperienceLevel <= 5)
								{
									RegistrationFee = 100;
								}
								else if (ExperienceLevel >= 6 && ExperienceLevel <= 9)
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