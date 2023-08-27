using Azure.AI.TextAnalytics;
using System.Text;

namespace Webscrapper.Lib
{

	public static class SummarizationExtensions
	{

		public static string GetExtractiveSummarizeResultInfo(this ExtractiveSummarizeResult documentResults)
		{
			var sb = new StringBuilder();

			if (documentResults.HasError)
			{
				sb.AppendLine($"Error!");
				sb.AppendLine($"Document error code: {documentResults.Error.ErrorCode}.");
				sb.AppendLine($"Message: {documentResults.Error.Message}");
			}
			else
			{
				sb.AppendLine($"SUCCESS. There are no errors encountered while summarizing the document");
			}

			sb.AppendLine($"Extracted the following {documentResults.Sentences.Count} sentence(s):");
			sb.AppendLine();

			foreach (ExtractiveSummarySentence sentence in documentResults.Sentences)
			{
				sb.AppendLine($"Sentence: {sentence.Text} Offset: {sentence.Offset} Rankscore: {sentence.RankScore} Length:{sentence.Length}");
				sb.AppendLine();
			}
			return sb.ToString();
		}
	}

}
