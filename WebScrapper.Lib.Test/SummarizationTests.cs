using Azure;
using Azure.AI.TextAnalytics;
using System.Diagnostics;
using Webscrapper.Lib;
using Xunit.Abstractions;

namespace WebScrapper.Lib.Test
{

	public class SummarizationTests
	{
		private readonly ITestOutputHelper _testOutputHelper;
		private readonly TextAnalyticsClient _client;
		ISummarizationUtil _summarizationUtil;

        public SummarizationTests(ITestOutputHelper testOutputHelper)
        {
			_summarizationUtil = new SummarizationUtil();
			_testOutputHelper = testOutputHelper;
			string? uri = Environment.GetEnvironmentVariable("AZURE_COGNITIVE_SERVICE_ENDPOINT", EnvironmentVariableTarget.Machine);
			string? key = Environment.GetEnvironmentVariable("AZURE_COGNITIVE_SERVICE_KEY", EnvironmentVariableTarget.Machine);
			if (uri == null)
			{
				throw new ArgumentNullException(nameof(uri), "Could not get system environment variable named 'AZURE_COGNITIVE_SERVICE_ENDPOINT' Set this variable first.");
			}
			if (uri == null)
			{
				throw new ArgumentNullException(nameof(uri), "Could not get system environment variable named 'AZURE_COGNITIVE_SERVICE_KEY' Set this variable first.");
			}
			_client = new TextAnalyticsClient(new Uri(uri!), new AzureKeyCredential(key!));
		}

        [Theory]
		[InlineData(_samplePageText)]
		public async Task Summarize_Of_Page_Does_Not_Fail(string pageText)
		{
			string? summarySentences = await _summarizationUtil.GetExtractiveSummarizeSentectesResult(pageText, _client);
			summarySentences.Should().NotBeNullOrWhiteSpace();
            _testOutputHelper.WriteLine(summarySentences);
        }

		[Theory]
		[InlineData(_samplePageText)]
		public async Task SummarizeResult_Of_Page_Does_Not_Fail(string pageText)
		{
			List<ExtractiveSummarizeResult> summarySentences = await _summarizationUtil.GetExtractiveSummarizeResult(pageText, _client);
			summarySentences.Should().NotBeNullOrEmpty();
			_testOutputHelper.WriteLine(string.Join(Environment.NewLine, summarySentences.Select(x => x.GetExtractiveSummarizeResultInfo())));
		}

		private const string _samplePageText = @"
Snur etter sjokkbygene
Regnet tar en liten pause i starten av uka. Likevel har meteorologene kjedelige nyheter.
Meteorologene sendte ut oransje farevarsel for flom i Innlandet og Viken. I Hallingdal har flere kommuner satt krisestab som f�lge av mye nedb�r og �kt vannstand s�ndag.
Kort tid seinere ble det sendt ut r�dt farevarsel for flom i deler av Drammensvassdraget.
Farevarselet gjelder for kommunene S�ndre Land, Gran, Ringerike, Hole, Jevnaker, Modum og Lier.
Vet ikke hva som skjer
- Nedb�ren som var p� S�rlandet tidligere i dag har beveget seg lenger oppover, sier vakthavende meteorolog Per Egil Haga ved Meteorologisk institutt til Dagbladet.
Farevarselet kommer etter styrtregn i helga.
- Tilfeldige byger
N� kan det likevel bli en pause for �stlendingene etter sjokkbygene.
-P� mandag og tirsdag vil det bli en del sol p� �stlandet, sier Haga, og opplyser:
- Likevel kan det dukke opp en og annen regnskur. Prognosene v�re er veldig usikre, og vi m� derfor ta det fra dag til dag.
Samtidig er det lite som tyder p� store nedb�rsmengder gjennom ukas to f�rste dager.
- Slutt p� godv�rsperioden
Til tross for en god v�ruke innledningsvis, m� meteorologen likevel skuffe. Det er nemlig ingenting som tyder p� stabil h�ytrykksaktivitet.
- Det er ingen store endringer i sirkulasjonen vi har hatt den siste tida, og det vil v�re slutt p� godv�rsperioden for S�r-Norge fra og med onsdag.
- Dette ser ut til � fortsette inn mot helgen. Det er bare � st�lsette seg, sier Haga.
Ogs� Norges vassdrags- og energidirektorat (NVE) har forberedt seg p� store vannmengder de kommende dagene. Blant annet er vannstanden i Tyrifjorden ventet � n� lignende niv� som under ekstremv�ret Hans.
Beboere evakueres
- Vannstanden i Randsfjorden har allerede passert r�dt niv� p� s�ndag. Tyrifjorden og Sperillen venter vi n�r r�dt niv� i l�pet av mandag, sier flomvakt Stein Beldring i Norges vassdrags- og energidirektorat (NVE) i en pressemelding.
D�rlige kort p� h�nda
Selv om hele omr�det s�r for Tr�ndelag vil v�re preget av lavtrykksaktivitet og potensielle regnbyger, er det enkelte omr�der som vil bli rammet i bredere omfang.
- Vestlendingene har ganske d�rlige kort p� h�nda, og det blir lite � se til sola - i beste fall noen innslag. Oslo vil f� penere v�r enn Bergen.
Nordlendingene m� imidlertid nyte mandagen for � f� best utbytte av det begrensede solskinnsv�ret.
- Lavtrykket vil bevege seg mot Nord-Norge og Tr�ndelag allerede fra tirsdagen. 
Flommer over
Mer om
dagbladet er en del av Aller Media
Hvorfor ser du denne annonsen

			Nettstedet du n� bes�ker blir i stor del finansiert av annonseinntekter. Basert p� din tidligere aktivitet hos oss, vil du f� annonser vi tror kan interessere deg.
		

			Du velger selv om du �nsker � endre dine innstillinger
		

			Les mer om innstillinger
		

		Om Aller Media og annonsering
	

		Aller Media eier nettstedene Dagbladet, Sol, DinSide, KK, Se og H�r, Lommelegen, Topp og Vi
	

		Les mer om Aller Media og annonser
	
Vi bryr oss om ditt personvern

		Dagbladet er en del av Aller Media, som er ansvarlig for dine data. Vi 
		bruker informasjonskapsler (cookies) og dine data til � forbedre og tilpasse tjenestene, tilbudene og annonsene 
		v�re.
	

		Vil du vite mer om hvordan du kan endre dine innstillinger, g� til
		personverninnstillinger
	
Viser framkj�resten
- Han tok formye viagra
Norske kvinner:Verdens st�rste
Hun vilbare hjem
Vekker oppsikt:- Forferdelige scener
F�r dr�ye tilbud:- Folk er syke
- Vil hellerd� enn � lyve
Avsl�rerkronisk sykdom
- Tror Gjerter skuffa
Tippe-metoden: Fullpott 237 ganger
Ekle bilder: - Huntrenger hjelp
Latterliggj�res:- Selv er jeg jomfru
Nesespray-marerittet:- Forferdelig 
Bilder vekkeroppsikt

		Utforsk v�re merkevarer og logg inn p� tvers av Aller Media
	
Postadresse:
Bes�ksadresse:
";

	}

}