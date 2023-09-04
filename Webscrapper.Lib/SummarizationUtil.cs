using Azure.AI.TextAnalytics;
using System.Text;

namespace Webscrapper.Lib
{
	public class SummarizationUtil : ISummarizationUtil
	{

		public async Task<List<ExtractiveSummarizeResult>> GetExtractiveSummarizeResult(string document, TextAnalyticsClient client)
		{
			var batchedDocuments = new List<string>
			{
				document
			};
			var result = new List<ExtractiveSummarizeResult>();
			var options = new ExtractiveSummarizeOptions
			{
				 MaxSentenceCount = 5
			};
			var operation = await client.ExtractiveSummarizeAsync(Azure.WaitUntil.Completed, batchedDocuments, options: options);
			await foreach (ExtractiveSummarizeResultCollection documentsInPage in operation.Value)
			{
				foreach (ExtractiveSummarizeResult documentResult in documentsInPage)
				{
					result.Add(documentResult);
				}
			}
			return result;
		}

		public async Task<string> GetExtractiveSummarizeSentectesResult(string document, TextAnalyticsClient client)
		{
			List<ExtractiveSummarizeResult> summaries = await GetExtractiveSummarizeResult(document, client);
			return string.Join(Environment.NewLine, summaries.Select(s => s.Sentences).SelectMany(x => x).Select(x => x.Text));
		}

	}

}
