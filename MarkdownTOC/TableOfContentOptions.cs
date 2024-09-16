using CommandLine;

namespace NorseTechnologies.MarkdownTOC
{
	[Verb("toc", HelpText = "Create/Update Table of Contents in markdown file", Hidden = true)]
	public class TableOfContentOptions
	{
		[Option('f', "filename", Default = null, HelpText = "URL of filename to publish")]
		public string? Filename { get; set; }
	}
}
