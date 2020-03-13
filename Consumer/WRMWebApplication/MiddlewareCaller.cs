using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
		using System.Xml;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;

namespace WRMWebApplication
{
	public class MiddlewareCaller
	{
		string URL = "http://10.15.14.219:7080/";
		Random random = new Random();
		bool isAccountNumber = false;

		Dictionary<string, string> Values = new Dictionary<string, string>
		{
			{ "{RND}","" },
			{ "{TIME}","2020-01-06T01:48:33.422" },
			{ "{AcctId}","" },
			{ "{CreditCard}","" }
		};

		private string GenerateNumber()
		{
			return new string(Enumerable.Repeat("0123456789", 15)
			  .Select(s => s[random.Next(s.Length)]).ToArray());
		}

		public MiddlewareCaller()
		{
			accountNumberRequestXML = accountNumberRequestXML.Replace("{0}", "\"");
			creditCardRequestXML = creditCardRequestXML.Replace("{0}", "\"");
		}
		public MiddlewareCaller(string url) : this()
		{
			URL = url;
		}
		public string CallWebService(bool accountNumber, string value)
		{
			HttpWebRequest webRequest = CreateWebRequest(URL);
			using (var streamWriter = new StreamWriter(webRequest.GetRequestStream()))
			{
				streamWriter.Write(GenerateXML(accountNumber, value));
				streamWriter.Flush();
				streamWriter.Close();
			}
			var httpResponse = (HttpWebResponse)webRequest.GetResponse();
			var result = "";
			using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
				result = streamReader.ReadToEnd();
			return result;
		}

		public async Task<string> CallWebService2Async(bool accountNumber, string value)
		{
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(URL);
				var content = new StringContent(GenerateXML(accountNumber, value), Encoding.UTF8, "application/xml");
				var result = await client.PostAsync(URL, content);
				return await result.Content.ReadAsStringAsync();
			}
		}

		private HttpWebRequest CreateWebRequest(string url)
		{
			var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
			httpWebRequest.ContentType = "application/xml";
			httpWebRequest.Method = "POST";
			//ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
			return httpWebRequest;
		}

		private XmlDocument CreateSoapEnvelope(bool accountNumber, string value)
		{
			XmlDocument soapEnvelopeDocument = new XmlDocument();
			var xml = GenerateXML(accountNumber, value);
			soapEnvelopeDocument.LoadXml(xml);
			return soapEnvelopeDocument;
		}

		private string GenerateXML(bool accountNumber, string value)
		{
			var res = "";
			res = accountNumber ? accountNumberRequestXML : creditCardRequestXML;
			res = res.Replace(accountNumber ? "{AcctId}" : "{CreditCard}", value);
			res = res.Replace("{RND}", GenerateNumber());
			//2020-03-11 05:54:01.070
			res = res.Replace("{TIME}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ".000");
			return res;
		}

		private void InsertSoapEnvelopeIntoWebRequest(XmlDocument soapEnvelopeXml, HttpWebRequest webRequest)
		{
			using (Stream stream = webRequest.GetRequestStream())
			{
				soapEnvelopeXml.Save(stream);
			}
		}
		public string this[string index]
		{
			get { return Values[index]; }
			set { if (Values.ContainsKey(index)) Values[index] = value; }
		}


		readonly string accountNumberRequestXML = @"<envelope encodingStyle={0}http://www.altova.com{0}
      xsi:schemaLocation={0}urn:CustomerAndAccountDetails_v1_req CustomerAndAccountDetails_v1_req.xsd{0}
      xmlns={0}urn:CustomerAndAccountDetails_v1_req{0} xmlns:xsi={0}http://www.w3.org/2001/XMLSchema-instance{0}
      xmlns:RAKHeader={0}urn:RAKBankHeader{0}>
      <header>
            <RAKHeader:ServiceId>RBS_CustomerAndAccountDetails_v1
			</RAKHeader:ServiceId>
            <RAKHeader:ServiceType>
                  <RAKHeader:type>CustomerAndAccountDetails</RAKHeader:type>
                  <subtype xmlns = {0}{0} >
						< RAKHeader:type>String</RAKHeader:type>
                  </subtype>
            </RAKHeader:ServiceType>
            <RAKHeader:ServiceRequestorId>ELX</RAKHeader:ServiceRequestorId>
            <RAKHeader:ServiceProviderId>String</RAKHeader:ServiceProviderId>
            <RAKHeader:ServiceChannelId>ELX</RAKHeader:ServiceChannelId>
            <RAKHeader:RequestID>ELX{RND}</RAKHeader:RequestID>
            <RAKHeader:IsResponseRequired>String</RAKHeader:IsResponseRequired>
            <RAKHeader:ServiceExpirySecond>String</RAKHeader:ServiceExpirySecond>
            <RAKHeader:SecurityInfo>String</RAKHeader:SecurityInfo>
            <RAKHeader:Language>E</RAKHeader:Language>
            <RAKHeader:TimeStampyyyymmddhhmmsss>{TIME}
            </RAKHeader:TimeStampyyyymmddhhmmsss>
            <RAKHeader:RequestLifeCycleStage>CustomerAndAccountDetailsRequest
			</RAKHeader:RequestLifeCycleStage>
            <RAKHeader:MessageStatus>String</RAKHeader:MessageStatus>
            <RAKHeader:NarrationList>
                  <RAKHeader:Narration>String</RAKHeader:Narration>
            </RAKHeader:NarrationList>
      </header>
      <body>
            <AcctInfo>
                  <AcctId>{AcctId}</AcctId>
            </AcctInfo>
            <InquiryType>CustomerAndAccount</InquiryType>
            <CIFDetailsFlag>N</CIFDetailsFlag>
            <RefInfo>
                  <RefType>String</RefType>
                  <RefValue>String</RefValue>
            </RefInfo>
            <Narration>String</Narration>
      </body>
</envelope>

";

		readonly string creditCardRequestXML = @"
<envelope encodingStyle={0}http://www.altova.com{0}
      xsi:schemaLocation={0}urn:CustomerAndAccountDetails_v1_req CustomerAndAccountDetails_v1_req.xsd{0}
      xmlns={0}urn:CustomerAndAccountDetails_v1_req{0} xmlns:xsi={0}http://www.w3.org/2001/XMLSchema-instance{0}
      xmlns:RAKHeader={0}urn:RAKBankHeader{0}>
      <header>
            <RAKHeader:ServiceId>RBS_CustomerAndAccountDetails_v1
			</RAKHeader:ServiceId>
            <RAKHeader:ServiceType>
                  <RAKHeader:type>CustomerAndAccountDetails</RAKHeader:type>
                  <subtype xmlns = {0}{0} >

						< RAKHeader:type>String</RAKHeader:type>
                  </subtype>
            </RAKHeader:ServiceType>
            <RAKHeader:ServiceRequestorId>ELX</RAKHeader:ServiceRequestorId>
            <RAKHeader:ServiceProviderId>String</RAKHeader:ServiceProviderId>
            <RAKHeader:ServiceChannelId>ELX</RAKHeader:ServiceChannelId>
            <RAKHeader:RequestID>ELX{RND}</RAKHeader:RequestID>
            <RAKHeader:IsResponseRequired>String</RAKHeader:IsResponseRequired>
            <RAKHeader:ServiceExpirySecond>String</RAKHeader:ServiceExpirySecond>
            <RAKHeader:SecurityInfo>String</RAKHeader:SecurityInfo>
            <RAKHeader:Language>E</RAKHeader:Language>
            <RAKHeader:TimeStampyyyymmddhhmmsss>{TIME}
            </RAKHeader:TimeStampyyyymmddhhmmsss>
            <RAKHeader:RequestLifeCycleStage>CustomerAndAccountDetailsRequest
			</RAKHeader:RequestLifeCycleStage>
            <RAKHeader:MessageStatus>String</RAKHeader:MessageStatus>
            <RAKHeader:NarrationList>
                  <RAKHeader:Narration>String</RAKHeader:Narration>
            </RAKHeader:NarrationList>
      </header>
      <body>
            <CardLogicalData>
                  <CardType>Credit Card</CardType>
                  <CardEmbossNum>{CreditCard}</CardEmbossNum>
            </CardLogicalData>
            <InquiryType>CustomerAndAccount</InquiryType>
            <CIFDetailsFlag>N</CIFDetailsFlag>
            <RefInfo>
                  <RefType>String</RefType>
                  <RefValue>String</RefValue>
            </RefInfo>
            <Narration>String</Narration>
      </body>
</envelope>

";
	}
}