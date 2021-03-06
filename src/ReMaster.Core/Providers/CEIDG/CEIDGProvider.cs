﻿using System;
using System.Collections.Generic;
using System.IO;
using System.ServiceModel;
using CEIDGApi;
using Microsoft.Extensions.Configuration;
using NLog.Fluent;
using ReMaster.EntityFramework.Model;
using ReMaster.Utilities.Tools;
using System.Xml;
using System.Xml.Linq;
using ReMaster.BusinessLogic.Company;
using ReMaster.BusinessLogic;
using static NewCEIDGApiProd.NewDataStoreProviderClient;

namespace ReMaster.Core.Providers.CEIDG
{
	public class CEIDGProvider
	{
		#region Voivodeships
		private static List<string> Voivodeships = new List<string>() {
			"dolnośląskie",
			"kujawsko-pomorskie",
			"lubelskie",
			"lubuskie",
			"łódzkie",
			"małopolskie",
			"mazowieckie",
			"opolskie",
			"podkarpackie",
			"podlaskie",
			"pomorskie",
			"śląskie",
			"świętokrzyskie",
			"warmińsko-mazurskie",
			"wielkopolskie",
			"zachodniopomorskie"
		}; 
		#endregion

		#region ImportClients()

		public static async void ImportClients(ICompanyAppService companyService, IProjectMetaDataService metaDataService)
		{
			var newClient = new NewCEIDGApiProd.NewDataStoreProviderClient(EndpointConfiguration.BasicHttpBinding_INewDataStoreProvider);

			try
			{
				var configuration = ConfigurationHelper.Get(Directory.GetCurrentDirectory(), null, true);
				var dateOfLastImport = metaDataService.GetDateOfLastImport(ProviderType.CEIDG);

				newClient.InnerChannel.OperationTimeout = TimeSpan.MaxValue;

				var result = await newClient.GetMigrationDataExtendedAddressInfoAsync(
					configuration.GetValue<string>("CEIDGApiKey"), //key
					null, //nip
					null, //regon 
					null, //nip_sc
					null, //regon_sc
					null, //name
					Voivodeships, // provinces,
					null, //county
					null, //commune
					null, //city
					null, //street
					null, //postcode
					null, //date from
					null, //date to
					null, //pkd
					new List<int> { 1 }, //status
					null, //unique id
					dateOfLastImport == DateTime.MinValue ? (DateTime?)null : dateOfLastImport, //migration date from
					null); //migration date to

				var importedCompanies = ParseCompanies(result);

				companyService.AddCompanies(importedCompanies, dateOfLastImport == DateTime.MinValue);

				metaDataService.SetDateOfLastImport(ProviderType.CEIDG, DateTime.Now);
			}
			catch (Exception ex)
			{
				ErrorLog.Save(ex);
				throw;
			}
			finally
			{
				if (newClient.State == CommunicationState.Faulted)
				{
					newClient.Abort();
				}
				else
				{
					await newClient.CloseAsync();
				}
			}
		} 

		#endregion

		#region ParseCompanies()

		public static List<Company> ParseCompanies(string importedText)
		{
			List<Company> importedCompanies = new List<Company>();
			try
			{
				using (Stream stream = GenerateStreamFromString(importedText))
				{
					using (XmlReader reader = XmlReader.Create(stream))
					{
						reader.MoveToContent();
						reader.Read();
						while (!reader.EOF)
						{
							if (reader.NodeType == XmlNodeType.Element && reader.Name == "InformacjaOWpisie")
							{
								Company company = new Company();

								XElement companyElement = XNode.ReadFrom(reader) as XElement;

								if (companyElement != null)
								{
									#region Reading node "IdentyfikatorWpisu"
									var ceidgIdElement = companyElement.Element("IdentyfikatorWpisu");
									if (ceidgIdElement != null)
									{
										company.CeidgId = ceidgIdElement.Value;
									}
									#endregion

									#region Reading node "DanePodstawowe"
									var basicDataElement = companyElement.Element("DanePodstawowe");
									if (basicDataElement != null)
									{
										company.OwnerFirstName = basicDataElement.Element("Imie") != null ? basicDataElement.Element("Imie").Value : string.Empty;
										company.OwnerLastName = basicDataElement.Element("Nazwisko") != null ? basicDataElement.Element("Nazwisko").Value : string.Empty;
										company.NIP = basicDataElement.Element("NIP") != null ? basicDataElement.Element("NIP").Value : string.Empty;
										company.Name = basicDataElement.Element("Firma") != null ? basicDataElement.Element("Firma").Value : string.Empty;
										company.Regon = basicDataElement.Element("REGON") != null ? basicDataElement.Element("REGON").Value : string.Empty;
									}
									#endregion

									#region Reading node "DaneKontaktowe"
									var contactDataElement = companyElement.Element("DaneKontaktowe");
									if (contactDataElement != null)
									{
										company.Website = contactDataElement.Element("AdresStronyInternetowej") != null ? contactDataElement.Element("AdresStronyInternetowej").Value : string.Empty;
										company.Email = contactDataElement.Element("AdresPocztyElektronicznej") != null ? contactDataElement.Element("AdresPocztyElektronicznej").Value : string.Empty;
										company.Phone = contactDataElement.Element("Telefon") != null ? contactDataElement.Element("Telefon").Value : string.Empty;
										company.Fax = contactDataElement.Element("Faks") != null ? contactDataElement.Element("Faks").Value : string.Empty;
									}
									#endregion

									#region Reading node "DaneAdresowe"
									var addressDataElement = companyElement.Element("DaneAdresowe");
									if (addressDataElement != null)
									{
										var mainAddressDataElement = addressDataElement.Element("AdresGlownegoMiejscaWykonywaniaDzialalnosci");
										if (mainAddressDataElement != null)
										{
											company.City = mainAddressDataElement.Element("Miejscowosc") != null ? mainAddressDataElement.Element("Miejscowosc").Value : string.Empty;
											company.BuildingNo = mainAddressDataElement.Element("Budynek") != null ? mainAddressDataElement.Element("Budynek").Value : string.Empty;
											company.PostalCode = mainAddressDataElement.Element("KodPocztowy") != null ? mainAddressDataElement.Element("KodPocztowy").Value : string.Empty;
											company.PostalCity = mainAddressDataElement.Element("Poczta") != null ? mainAddressDataElement.Element("Poczta").Value : string.Empty;
											company.District = mainAddressDataElement.Element("Powiat") != null ? mainAddressDataElement.Element("Powiat").Value : string.Empty;
											company.Voivodeship = mainAddressDataElement.Element("Wojewodztwo") != null ? mainAddressDataElement.Element("Wojewodztwo").Value : string.Empty;
											company.AddessAnomaly = mainAddressDataElement.Element("OpisNietypowegoMiejscaLokalizacji") != null ? mainAddressDataElement.Element("OpisNietypowegoMiejscaLokalizacji").Value : string.Empty;
										}
									}
									#endregion

									#region Reading node "DaneDodatkowe"
									var additionalDataElement = companyElement.Element("DaneDodatkowe");
									if (additionalDataElement != null)
									{
										company.Status = additionalDataElement.Element("Status") != null ? additionalDataElement.Element("Status").Value : string.Empty;
										company.PKDCodes = additionalDataElement.Element("KodyPKD") != null ? additionalDataElement.Element("KodyPKD").Value : string.Empty;
									}
									#endregion

									importedCompanies.Add(company);
								}
							}
							else
							{
								reader.Read();
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				ErrorLog.Save(ex);
				throw;
			}
			finally
			{
			}

			return importedCompanies;
		} 

		#endregion

		#region GenerateStreamFromString()
		public static Stream GenerateStreamFromString(string s)
		{
			MemoryStream stream = new MemoryStream();
			StreamWriter writer = new StreamWriter(stream);
			writer.Write(s);
			writer.Flush();
			stream.Position = 0;
			return stream;
		} 
		#endregion
	}
}