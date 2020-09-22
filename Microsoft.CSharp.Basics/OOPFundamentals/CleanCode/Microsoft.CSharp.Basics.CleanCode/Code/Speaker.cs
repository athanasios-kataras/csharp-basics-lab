using System.Collections.Generic;
using System.Linq;

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
		public int? Experiense { get; set; }
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
			//We weren't filtering out the prodigy domain so I added it.
			var domains = new List<string>() { "aol.com", "prodigy.com", "compuserve.com" };

			int? speakerId = null;
			bool isAdvancedUser = false;

			if (!string.IsNullOrWhiteSpace(FirstName))
			{
				if (!string.IsNullOrWhiteSpace(LastName))
				{
					if (!string.IsNullOrWhiteSpace(Email))
					{
						var employers = new List<string>() { "Pluralsight", "Microsoft", "Google" };

						isAdvancedUser = Experiense > 10 || HasBlog || Certifications.Count() > 3 || employers.Contains(Employer);

						if (!isAdvancedUser)
						{
							//need to get just the domain from the email
							string emailDomain = Email.Split('@').Last();

							if (!domains.Contains(emailDomain) && (!(Browser.Name == WebBrowser.BrowserName.InternetExplorer && Browser.MajorVersion < 9)))
							{
								isAdvancedUser = true;
							}
						}

						var oldTechnologies = new List<string>() { "Cobol", "Punch Cards", "Commodore", "VBScript" };
						bool isApproved = false;
						if (isAdvancedUser)
						{
							if (Sessions.Count() != 0)
							{
								foreach (var session in Sessions)
								{
									foreach (var technology in oldTechnologies)
									{
										if (session.Title.Contains(technology) || session.Description.Contains(technology))
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
							}
							else
							{
								return new RegisterResponse(RegisterError.NoSessionsProvided);
							}

							if (isApproved)
							{
								//if we got this far, the speaker is approved
								//let's go ahead and register him/her now.
								//First, let's calculate the registration fee. 
								//More experienced speakers pay a lower fee.
								if (Experiense <= 1)
								{
									RegistrationFee = 500;
								}
								else if (Experiense >= 2 && Experiense <= 3)
								{
									RegistrationFee = 250;
								}
								else if (Experiense >= 4 && Experiense <= 5)
								{
									RegistrationFee = 100;
								}
								else if (Experiense >= 6 && Experiense <= 9)
								{
									RegistrationFee = 50;
								}
								else
								{
									RegistrationFee = 0;
								}

								//Now, save the speaker and sessions to the db.
								try
								{
									speakerId = repository.SaveSpeaker(this);
								}
								catch
								{
									//in case the db call fails 
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