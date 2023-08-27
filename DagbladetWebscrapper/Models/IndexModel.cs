using System.ComponentModel.DataAnnotations;

namespace DagbladetWebscrapper.Models
{
	public class IndexModel
	{
        [Required]
        public string? ArticleUrl { get; set; }

        public string SummarySentences { get; set; }

        public string ArticleText { get; set; }
    }
}
