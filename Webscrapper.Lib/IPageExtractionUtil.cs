namespace Webscrapper.Lib
{

	public interface IPageExtractionUtil
	{
		Task<string?> ExtractHtml(string url, bool includeTags);
	}

}