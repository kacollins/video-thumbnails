<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Web.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Configuration.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.DirectoryServices.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.EnterpriseServices.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Web.RegularExpressions.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Design.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Web.ApplicationServices.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.ComponentModel.DataAnnotations.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.DirectoryServices.Protocols.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Security.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.ServiceProcess.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Web.Services.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\Microsoft.Build.Utilities.v4.0.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Runtime.Caching.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\Microsoft.Build.Framework.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\Microsoft.Build.Tasks.v4.0.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Windows.Forms.dll</Reference>
  <Namespace>System.Web</Namespace>
</Query>

void Main()
{
	string input = "APIs: The good, the bad, the ugly - Michele Titolo: 200 OK 2019"; 
	string logo = "";//Groups.okcjug.ToString().Replace('_', '-');
	string background = "code";//Groups.okcjug.ToString().Replace('_', '-');
	
	const string dash = " - ";
	const string colon = ": ";

	string title = input.Substring(0, input.Contains(dash) ? input.LastIndexOf(dash) : input.LastIndexOf(colon));
	string speaker = input.Contains(dash) && input.Contains(colon) ? input.Substring(input.LastIndexOf(dash) + dash.Length, input.LastIndexOf(colon) - input.LastIndexOf(dash) - dash.Length) : "";
	string group = input.Contains(colon) ? input.Substring(input.LastIndexOf(colon) + colon.Length) : input.Contains(dash) ? input.Substring(input.LastIndexOf(dash) + dash.Length) : "";

	title.Length.Dump("title");
	speaker.Length.Dump("speaker");
	group.Length.Dump("group");
	
	title = title.Replace("%", "%25").Replace(",", "%252C").Replace("#", "%23").Replace(". ", Environment.NewLine).Replace("!", "%21");

	var thumbnail = GetLink(title, speaker.Replace(",", "%252C"), group.Replace(",", ""), logo, Types.thumbnail, background);
	thumbnail.Dump("thumbnail");

	var instagram = GetLink(title.Replace(",", ""), speaker.Replace(",", ""), group.Replace(",", ""), logo, Types.instagram, background);
	instagram.Dump("instagram");
}

Hyperlinq GetLink(string title, string speaker, string group, string logo, Types type, string background)
{
	string start = $@"http://res.cloudinary.com/dt4dhjcyy/image/upload";
	//string outline = "/co_orange,e_outline:1";
	string backgroundImage = $"l_techlahoma:backgrounds:{background}";
	string blur = $"/c_scale,e_blur:300,fl_relative,g_north,h_0.7,{backgroundImage},w_1.0,c_fill";

	string techlahomaLogoSettings = GetTechlahomaLogoSettings(type);
	string groupLogoSettings = GetGroupLogoSettings(logo, type);

	string titleSettings = GetTitleSettings(title, type);
	string speakerSettings = GetSpeakerSettings(speaker, type);
	string groupSettings = GetGroupSettings(group, type);
	string canvas = "/background_nixfbg";

	return new Hyperlinq($@"{start}{blur}{techlahomaLogoSettings}{groupLogoSettings}{titleSettings}{speakerSettings}{groupSettings}{canvas}");
}

int GetTitleFontSizeForThumbnail(string title)
{
	int fontSize = (int)(185 - 1.3 * title.Length);
	fontSize.Dump("fontSize");
	return fontSize;
}

enum Types
{
	thumbnail,
	instagram
}

enum Groups
{
	Big_Data_in_Oklahoma_City,
	cocoaheads_okc,
	devopsokc,
	freecodecampokc,
	okc_analytics,
	okc_design_tech,
	okc_fp,
	okc_js,
	okc_osh,
	okc_ruby,
	okc_sharp,
	okcjug,
	okcpython,
	okcsql,
	Oklahoma_Game_Developers,
	ProductTank_OKC,
	shecodesokc,
	thunderplains
}

string GetTechlahomaLogoSettings(Types type)
{
	if (type == Types.instagram)
	{
		return "";
	}
	else
	{
		int techlahomaWidth = 640;
		int techlahomaX = 40;
		int techlahomaY = 20;
		return $"/c_scale,e_trim,g_south_east,l_techlahoma_horizontaltext_qgbkly,w_{techlahomaWidth},x_{techlahomaX},y_{techlahomaY}";
	}
}

string GetGroupLogoSettings(string logo, Types type)
{
	if (string.IsNullOrWhiteSpace(logo))
	{
		return "";
	}
	else
	{
		int logoX = type == Types.thumbnail ? -100 : 200;
		int logoY = 20;
		return $"/c_scale,g_south,h_160,l_techlahoma:logos:{logo},x_{logoX},y_{logoY}";
	}
}

string GetTitleSettings(string title, Types type)
{
	int titleFontSize = GetTitleFontSize(title, type);
	int titleWidth = type == Types.thumbnail ? 1100 : 740;
	int titleX = 640;
	int titleY = 252;

	string encodedTitle = HttpUtility.UrlPathEncode(title).Replace("?", "%3F");
	string titleSettings = $"/b_rgb:00000060,c_fit,co_white,g_xy_center,l_text:Lato_{titleFontSize}_bold_center:{encodedTitle},w_{titleWidth},x_{titleX},y_{titleY}";
	return titleSettings;
}

int GetTitleFontSize(string title, Types type)
{
	if (type == Types.thumbnail)
	{
		return GetTitleFontSizeForThumbnail(title);
	}
	else
	{
		return GetTitleFontSizeForInstagram(title);
	}
}

int GetTitleFontSizeForInstagram(string title)
{
	if (title.Length < 20)
	{
		return 150;
	}
	else if (title.Length < 25)
	{
		return 140;
	}
	else if (title.Length < 40)
	{
		return 120;
	}
	else if (title.Length < 50)
	{
		return 110;
	}
	else if (title.Length < 60)
	{
		return 100;
	}
	else if (title.Length < 65)
	{
		return 80;
	}
	else
	{
		return 70;
	}
}

string GetSpeakerSettings(string speaker, Types type)
{
	int speakerFontSize = GetSpeakerFontSize(speaker);
	int speakerWidth = 360;
	int speakerX = type == Types.thumbnail ? 40 : 300;
	int speakerY = 120;

	string speakerSettings = "";

	if (!string.IsNullOrWhiteSpace(speaker))
	{
		speakerSettings = $"/c_fit,g_south_west,l_text:Lato_{speakerFontSize}_bold_left:{HttpUtility.UrlPathEncode(speaker)},w_{speakerWidth},x_{speakerX},y_{speakerY}";
	}

	return speakerSettings;
}

int GetSpeakerFontSize(string speaker)
{
	if (speaker.Length < 11)
	{
		return 60;
	}
	else if (speaker.Length < 13)
	{
		return 55;
	}
	else if (speaker.Length < 15)
	{
		return 50;
	}
	else if (speaker.Length < 17)
	{
		return 40;
	}
	else if (speaker.Length < 20)
	{
		return 30;
	}
	else
	{
		return 25;
	}
}

string GetGroupSettings(string group, Types type)
{
	int groupFontSize = GetGroupFontSize(group);
	int groupWidth = 400;
	int groupX = type == Types.thumbnail ? 40 : 300;
	int groupY = 50;

	string groupName = HttpUtility.UrlPathEncode(group).Replace("#", "%23");

	string groupSettings = $"/c_fit,g_south_west,l_text:Lato_{groupFontSize}_left:{groupName},w_{groupWidth},x_{groupX},y_{groupY}";
	return groupSettings;
}

int GetGroupFontSize(string group)
{
	if (group.Length < 5)
	{
		return 50;
	}
	else if (group.Length < 15)
	{
		return 40;
	}
	else if (group.Length < 20)
	{
		return 35;
	}
	else
	{
		return 25;
	}
}