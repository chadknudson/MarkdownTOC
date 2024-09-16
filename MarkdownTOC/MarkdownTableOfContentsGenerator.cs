using System.Text.RegularExpressions;

namespace NorseTechnologies.MarkdownTOC
{
	public class MarkdownTableOfContentsGenerator
	{
		public static void GenerateOrReplaceTableOfContents(string filePath)
		{
			if (!File.Exists(filePath))
			{
				Console.WriteLine("File not found.");
				return;
			}

			var lines = File.ReadAllLines(filePath).ToList();
			var tocLines = new List<string>();

			// Find all the lines with markdown headings and generate ToC
			foreach (var line in lines)
			{
				// Check for markdown headings (starting with #, ##, ###, etc.)
				var match = Regex.Match(line, @"^(#{2,6})\s+(.+)");
				if (match.Success)
				{
					int headingLevel = match.Groups[1].Value.Length; // Number of '#' characters
					string headingText = match.Groups[2].Value.Trim();

					// Convert heading to a markdown-friendly link
					string link = GenerateMarkdownLink(headingText);

					// Indentation based on heading level
					string indent = new string(' ', (headingLevel - 2) * 2);
					tocLines.Add($"{indent}- [{headingText}](#{link})");
				}
			}

			// Check if Table of Contents already exists
			var tocStartIndex = lines.FindIndex(line => line.Trim() == "## Table of Contents");
			var tocEndIndex = -1;

			if (tocStartIndex != -1)
			{
				// If ToC exists, find where it ends (first heading after ToC)
				tocEndIndex = lines.FindIndex(tocStartIndex + 1, line => Regex.IsMatch(line, @"^#{2,6}\s+"));
				if (tocEndIndex == -1) tocEndIndex = lines.Count; // If no heading found, end at the file's end

				// Remove the existing ToC lines
				lines.RemoveRange(tocStartIndex, tocEndIndex - tocStartIndex);
			}
			else
			{
				// Insert ToC after the first level 1 heading (#)
				var firstHeadingIndex = lines.FindIndex(line => line.StartsWith("# "));
				var firstLevel2HeadingIndex = lines.FindIndex(firstHeadingIndex + 1, line => line.StartsWith("## "));

				// Insert after first heading and before first level 2 heading
				tocStartIndex = firstLevel2HeadingIndex != -1 ? firstLevel2HeadingIndex : lines.Count;
			}

			// Insert the generated ToC
			var tocContent = new List<string> { "## Table of Contents" };
			tocContent.AddRange(tocLines);
			lines.InsertRange(tocStartIndex, tocContent);

			// Write the modified content back to the file
			File.WriteAllLines(filePath, lines);
			Console.WriteLine("Table of Contents generated and updated successfully.");
		}

		private static string GenerateMarkdownLink(string headingText)
		{
			// Convert to lowercase, replace spaces with hyphens, and remove invalid characters
			string link = headingText.ToLower()
									 .Replace(" ", "-")
									 .Replace(".", "")
									 .Replace(",", "")
									 .Replace(":", "")
									 .Replace("?", "")
									 .Replace("'", "")
									 .Replace("\"", "")
									 .Replace("(", "")
									 .Replace(")", "")
									 .Replace("!", "")
									 .Replace("[", "")
									 .Replace("]", "");

			return link;
		}

		public static void Main(string[] args)
		{
			// Example usage
			string filePath = "README.md";
			GenerateOrReplaceTableOfContents(filePath);
		}
	}
}