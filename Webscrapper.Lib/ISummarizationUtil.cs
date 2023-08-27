using Azure.AI.TextAnalytics;

namespace Webscrapper.Lib
{
	public interface ISummarizationUtil
	{
		Task<List<ExtractiveSummarizeResult>> GetExtractiveSummarizeResult(string document, TextAnalyticsClient client);
		Task<string> GetExtractiveSummarizeSentectesResult(string document, TextAnalyticsClient client);
	}
}