using System.Diagnostics;
using Webscrapper.Lib;
using Xunit.Abstractions;

namespace WebScrapper.Lib.Test
{
	
	public class PageExtractionTests
	{
		private readonly ITestOutputHelper _testOutputHelper;
		IPageExtractionUtil _pageExtractionUtil;

        public PageExtractionTests(ITestOutputHelper testOutputHelper)
        {
			_pageExtractionUtil = new PageExtractionUtil();
			_testOutputHelper = testOutputHelper;
		}

        [Theory]
		[InlineData(@"https://www.dagbladet.no/nyheter/snur-etter-sjokkbygene/80109518", false)]
		[InlineData(@"https://www.dagbladet.no/nyheter/snur-etter-sjokkbygene/80109518", true)]
		public async Task Extract_Page_Does_Not_Fail(string url, bool includeTags)
		{
			string? html = await _pageExtractionUtil.ExtractHtml(url, includeTags);
			html.Should().NotBeNullOrWhiteSpace();
            _testOutputHelper.WriteLine(html);
        }

	}

}