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
Meteorologene sendte ut oransje farevarsel for flom i Innlandet og Viken. I Hallingdal har flere kommuner satt krisestab som følge av mye nedbør og økt vannstand søndag.
Kort tid seinere ble det sendt ut rødt farevarsel for flom i deler av Drammensvassdraget.
Farevarselet gjelder for kommunene Søndre Land, Gran, Ringerike, Hole, Jevnaker, Modum og Lier.
Vet ikke hva som skjer
- Nedbøren som var på Sørlandet tidligere i dag har beveget seg lenger oppover, sier vakthavende meteorolog Per Egil Haga ved Meteorologisk institutt til Dagbladet.
Farevarselet kommer etter styrtregn i helga.
- Tilfeldige byger
Nå kan det likevel bli en pause for østlendingene etter sjokkbygene.
-På mandag og tirsdag vil det bli en del sol på Østlandet, sier Haga, og opplyser:
- Likevel kan det dukke opp en og annen regnskur. Prognosene våre er veldig usikre, og vi må derfor ta det fra dag til dag.
Samtidig er det lite som tyder på store nedbørsmengder gjennom ukas to første dager.
- Slutt på godværsperioden
Til tross for en god væruke innledningsvis, må meteorologen likevel skuffe. Det er nemlig ingenting som tyder på stabil høytrykksaktivitet.
- Det er ingen store endringer i sirkulasjonen vi har hatt den siste tida, og det vil være slutt på godværsperioden for Sør-Norge fra og med onsdag.
- Dette ser ut til å fortsette inn mot helgen. Det er bare å stålsette seg, sier Haga.
Også Norges vassdrags- og energidirektorat (NVE) har forberedt seg på store vannmengder de kommende dagene. Blant annet er vannstanden i Tyrifjorden ventet å nå lignende nivå som under ekstremværet Hans.
Beboere evakueres
- Vannstanden i Randsfjorden har allerede passert rødt nivå på søndag. Tyrifjorden og Sperillen venter vi når rødt nivå i løpet av mandag, sier flomvakt Stein Beldring i Norges vassdrags- og energidirektorat (NVE) i en pressemelding.
Dårlige kort på hånda
Selv om hele området sør for Trøndelag vil være preget av lavtrykksaktivitet og potensielle regnbyger, er det enkelte områder som vil bli rammet i bredere omfang.
- Vestlendingene har ganske dårlige kort på hånda, og det blir lite å se til sola - i beste fall noen innslag. Oslo vil få penere vær enn Bergen.
Nordlendingene må imidlertid nyte mandagen for å få best utbytte av det begrensede solskinnsværet.
- Lavtrykket vil bevege seg mot Nord-Norge og Trøndelag allerede fra tirsdagen. 
Flommer over
Mer om
dagbladet er en del av Aller Media
Hvorfor ser du denne annonsen

			Nettstedet du nå besøker blir i stor del finansiert av annonseinntekter. Basert på din tidligere aktivitet hos oss, vil du få annonser vi tror kan interessere deg.
		

			Du velger selv om du ønsker å endre dine innstillinger
		

			Les mer om innstillinger
		

		Om Aller Media og annonsering
	

		Aller Media eier nettstedene Dagbladet, Sol, DinSide, KK, Se og Hør, Lommelegen, Topp og Vi
	

		Les mer om Aller Media og annonser
	
Vi bryr oss om ditt personvern

		Dagbladet er en del av Aller Media, som er ansvarlig for dine data. Vi 
		bruker informasjonskapsler (cookies) og dine data til å forbedre og tilpasse tjenestene, tilbudene og annonsene 
		våre.
	

		Vil du vite mer om hvordan du kan endre dine innstillinger, gå til
		personverninnstillinger
	
Viser framkjæresten
- Han tok formye viagra
Norske kvinner:Verdens største
Hun vilbare hjem
Vekker oppsikt:- Forferdelige scener
Får drøye tilbud:- Folk er syke
- Vil hellerdø enn å lyve
Avslørerkronisk sykdom
- Tror Gjerter skuffa
Tippe-metoden: Fullpott 237 ganger
Ekle bilder: - Huntrenger hjelp
Latterliggjøres:- Selv er jeg jomfru
Nesespray-marerittet:- Forferdelig 
Bilder vekkeroppsikt

		Utforsk våre merkevarer og logg inn på tvers av Aller Media
	
Postadresse:
Besøksadresse:
";

	}

}