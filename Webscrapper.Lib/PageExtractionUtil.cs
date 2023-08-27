using HtmlAgilityPack;
using System.Text;

namespace Webscrapper.Lib
{
	public class PageExtractionUtil : IPageExtractionUtil
	{

		public async Task<string?> ExtractHtml(string url, bool includeTags)
		{
			if (string.IsNullOrEmpty(url)) 
				return null;
			var httpClient = new HttpClient();

			string pageHtml = await httpClient.GetStringAsync(url);
			if (string.IsNullOrEmpty(pageHtml))
			{
				return null;
			}

			var htmlDoc = new HtmlDocument(); 
			htmlDoc.LoadHtml(pageHtml);
			var textNodes = htmlDoc.DocumentNode.SelectNodes("//h1|//h2|//h3|//h4|//h5|//h6|//p")
				.Where(n => !string.IsNullOrWhiteSpace(n.InnerText)).ToList();
			var sb = new StringBuilder();
			foreach (var textNode in textNodes)
			{
				var text = textNode.InnerText;
				if (includeTags)
				{
					sb.AppendLine($"<{textNode.Name}>{textNode.InnerText}</{textNode.Name}>");
				}
				else
				{
					sb.AppendLine($"{textNode.InnerText}");
				}
			}
			return sb.ToString();
		}
	}
}
