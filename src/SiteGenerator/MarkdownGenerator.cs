using Markdig;

namespace SiteGenerator;

internal class MarkdownGenerator
{
    public string GenerateHTML(string markdown)
    {
        string result = Markdown.ToHtml(markdown);
        return result;
    }
}
