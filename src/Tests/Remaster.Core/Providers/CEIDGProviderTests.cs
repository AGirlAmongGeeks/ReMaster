
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Tests.Remaster.Core.Providers
{
	public class CEIDGProviderTests
	{
		string sampleXML = @"<WynikWyszukiwania><InformacjaOWpisie><IdentyfikatorWpisu>0cf97c2a5555416139f7dfc1c25d68f</IdentyfikatorWpisu><DanePodstawowe><Imie>EWA</Imie><Nazwisko>KOWALSKA</Nazwisko><NIP></NIP><REGON></REGON><Firma>KOWALSKA EWA, -</Firma></DanePodstawowe><DaneKontaktowe><AdresPocztyElektronicznej></AdresPocztyElektronicznej><AdresStronyInternetowej></AdresStronyInternetowej><Telefon></Telefon><Faks></Faks></DaneKontaktowe><DaneAdresowe><AdresGlownegoMiejscaWykonywaniaDzialalnosci><Ulica>ul. Targowa</Ulica><Budynek>3</Budynek><KodPocztowy>12-345</KodPocztowy><Poczta>Warszawa</Poczta><Powiat>Warszawa</Powiat><Wojewodztwo>MAŁOPOLSKIE</Wojewodztwo></AdresGlownegoMiejscaWykonywaniaDzialalnosci><AdresDoDoreczen><Ulica>ul. Targowa</Ulica><Budynek>3</Budynek><KodPocztowy>12-345</KodPocztowy><Poczta>Warszawa</Poczta></AdresDoDoreczen><PrzedsiebiorcaPosiadaObywatelstwaPanstw></PrzedsiebiorcaPosiadaObywatelstwaPanstw></DaneAdresowe><DaneDodatkowe><DataRozpoczeciaWykonywaniaDzialalnosciGospodarczej>1993-12-01</DataRozpoczeciaWykonywaniaDzialalnosciGospodarczej><DataZawieszeniaWykonywaniaDzialalnosciGospodarczej></DataZawieszeniaWykonywaniaDzialalnosciGospodarczej><DataWznowieniaWykonywaniaDzialalnosciGospodarczej></DataWznowieniaWykonywaniaDzialalnosciGospodarczej><DataZaprzestaniaWykonywaniaDzialalnosciGospodarczej></DataZaprzestaniaWykonywaniaDzialalnosciGospodarczej><DataWykresleniaWpisuZRejestru></DataWykresleniaWpisuZRejestru><MalzenskaWspolnoscMajatkowa>-</MalzenskaWspolnoscMajatkowa><Status>Aktywny</Status><KodyPKD></KodyPKD></DaneDodatkowe><SpolkiCywilneKtorychWspolnikiemJestPrzedsiebiorca></SpolkiCywilneKtorychWspolnikiemJestPrzedsiebiorca><Zakazy></Zakazy><InformacjeDotyczaceUpadlosciPostepowaniaNaprawczego></InformacjeDotyczaceUpadlosciPostepowaniaNaprawczego><PelnomocnicyPrzedsiebiorcy></PelnomocnicyPrzedsiebiorcy></InformacjaOWpisie><InformacjaOWpisie><IdentyfikatorWpisu>c4915c8152d057be1568166efd142cfe</IdentyfikatorWpisu><DanePodstawowe><Imie>ANDRZEJ</Imie><Nazwisko>NOWAK</Nazwisko><NIP></NIP><REGON>555654111</REGON><Firma>CHRZĄSZCZ ANDRZEJ, PIWNICA</Firma></DanePodstawowe><DaneKontaktowe><AdresPocztyElektronicznej></AdresPocztyElektronicznej><AdresStronyInternetowej></AdresStronyInternetowej><Telefon></Telefon><Faks></Faks></DaneKontaktowe><DaneAdresowe><AdresGlownegoMiejscaWykonywaniaDzialalnosci><Ulica>ul. J. Matejki</Ulica><Budynek>10</Budynek><KodPocztowy>12-345</KodPocztowy><Poczta>Warszawa</Poczta><Powiat>Warszawa</Powiat><Wojewodztwo>MAŁOPOLSKIE</Wojewodztwo></AdresGlownegoMiejscaWykonywaniaDzialalnosci><AdresDoDoreczen><Ulica>ul. Sienkiewicza</Ulica><Budynek>11</Budynek><KodPocztowy>12-345</KodPocztowy><Poczta>Warszawa</Poczta></AdresDoDoreczen><PrzedsiebiorcaPosiadaObywatelstwaPanstw></PrzedsiebiorcaPosiadaObywatelstwaPanstw></DaneAdresowe><DaneDodatkowe><DataRozpoczeciaWykonywaniaDzialalnosciGospodarczej>2001-05-14</DataRozpoczeciaWykonywaniaDzialalnosciGospodarczej><DataZawieszeniaWykonywaniaDzialalnosciGospodarczej></DataZawieszeniaWykonywaniaDzialalnosciGospodarczej><DataWznowieniaWykonywaniaDzialalnosciGospodarczej></DataWznowieniaWykonywaniaDzialalnosciGospodarczej><DataZaprzestaniaWykonywaniaDzialalnosciGospodarczej></DataZaprzestaniaWykonywaniaDzialalnosciGospodarczej><DataWykresleniaWpisuZRejestru></DataWykresleniaWpisuZRejestru><MalzenskaWspolnoscMajatkowa>-</MalzenskaWspolnoscMajatkowa><Status>Aktywny</Status><KodyPKD>0150Z</KodyPKD></DaneDodatkowe><SpolkiCywilneKtorychWspolnikiemJestPrzedsiebiorca></SpolkiCywilneKtorychWspolnikiemJestPrzedsiebiorca><Zakazy></Zakazy><InformacjeDotyczaceUpadlosciPostepowaniaNaprawczego></InformacjeDotyczaceUpadlosciPostepowaniaNaprawczego><PelnomocnicyPrzedsiebiorcy></PelnomocnicyPrzedsiebiorcy></InformacjaOWpisie></WynikWyszukiwania>";

		[Fact]
		public void ParseCompanies_ParsedCount()
		{
			var companies =  ReMaster.Core.Providers.CEIDG.CEIDGProvider.ParseCompanies(sampleXML);
			Assert.Equal(companies.Count, 2);
		}

		[Fact]
		public void ParseCompanies_CheckFieldsReadFirstNode()
		{
			var companies = ReMaster.Core.Providers.CEIDG.CEIDGProvider.ParseCompanies(sampleXML);
			Assert.Equal(companies[1].OwnerFirstName, "ANDRZEJ");
		}

		[Fact]
		public void ParseCompanies_CheckFieldsReadSecondNode()
		{
			var companies = ReMaster.Core.Providers.CEIDG.CEIDGProvider.ParseCompanies(sampleXML);
			Assert.Equal(companies[0].OwnerFirstName, "EWA");
		}
	}
}
;