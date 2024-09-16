using CommandLine;
using System.Reflection;

namespace NorseTechnologies.MarkdownTOC
{
	internal class Program
	{
		static void Main(string[] args)
		{
			var types = LoadVerbs();

			Parser.Default.ParseArguments(args, types)
				.WithParsed<TableOfContentOptions>(o =>
				{
					if (string.IsNullOrEmpty(o.Filename))
					{
						Console.WriteLine("Filename is required.");
						return;
					}
					MarkdownTableOfContentsGenerator.GenerateOrReplaceTableOfContents(o.Filename);
				});
		}
		private static Type[] LoadVerbs()
		{
			return Assembly.GetExecutingAssembly().GetTypes()
				.Where(t => t.GetCustomAttribute<VerbAttribute>() != null).ToArray();
		}
	}
}
