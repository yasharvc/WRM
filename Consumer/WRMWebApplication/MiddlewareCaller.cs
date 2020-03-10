using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
		using System.Xml;
using System.Net;
using System.IO;

namespace WRMWebApplication
{
	public class MiddlewareCaller
	{
		string URL = "http://xxxxxxxxx/Service1.asmx";
		string Action = "http://xxxxxxxx/Service1.asmx?op=HelloWorld";

		Dictionary<string, string> Values = new Dictionary<string, string>
		{
			{ "{RequestUUID}","ELX11111111111111" },
			{ "{ServiceRequestId}","executeFinacleScript" },
			{ "{ServiceRequestVersion}","10.2" },
			{ "{ChannelId}","ELX" },
			{ "{MessageDateTime}","2020-01-06T01:48:33.422" },
			{ "{requestId}","getCustomerAndAccountDetails" },
			{ "{AcctId}","1111111111111" },
			{ "{FetchType}","CA" },
			{ "{RelatedCIFDetailsFlag}","N" }
		};

		readonly string RequestXML = @"
<?xml version={0}1.0{0} encoding={0}UTF-8{0}?>
<NS1:FIXML xmlns:NS1={0}http://www.finacle.com/fixml{0} xmlns:xsd={0}http://www.w3.org/2001/XMLSchema{0} xmlns:xsi={0}http://www.w3.org/2001/XMLSchema-instance{0}>
   <NS1:Header>
      <NS1:RequestHeader>
         <NS1:MessageKey>
            <NS1:RequestUUID>{RequestUUID}</NS1:RequestUUID>
            <NS1:ServiceRequestId>{ServiceRequestId}</NS1:ServiceRequestId>
            <NS1:ServiceRequestVersion>{ServiceRequestVersion}</NS1:ServiceRequestVersion>
            <NS1:ChannelId>{ChannelId}</NS1:ChannelId>
         </NS1:MessageKey>
         <NS1:RequestMessageInfo>
            <NS1:BankId>RAK</NS1:BankId>
            <NS1:TimeZone />
            <NS1:EntityId />
            <NS1:EntityType />
            <NS1:ArmCorrelationId />
            <NS1:MessageDateTime>{MessageDateTime}</NS1:MessageDateTime>
         </NS1:RequestMessageInfo>
         <NS1:Security>
            <NS1:Token>
               <NS1:PasswordToken>
                  <NS1:UserId />
                  <NS1:Password />
               </NS1:PasswordToken>
            </NS1:Token>
            <NS1:FICertToken />
            <NS1:RealUserLoginSessionId />
            <NS1:RealUser />
            <NS1:RealUserPwd />
            <NS1:SSOTransferToken />
         </NS1:Security>
      </NS1:RequestHeader>
   </NS1:Header>
   <NS1:Body>
      <NS1:executeFinacleScriptRequest>
         <NS1:ExecuteFinacleScriptInputVO>
            <NS1:requestId>{requestId}</NS1:requestId>
         </NS1:ExecuteFinacleScriptInputVO>
         <NS1:executeFinacleScript_CustomData>
            <NS1:getCustomerAndAccountDetails_REQ>
               <NS1:AcctId>{AcctId}</NS1:AcctId>
               <NS1:FetchType>{FetchType}</NS1:FetchType>
               <NS1:RelatedCIFDetailsFlag>{RelatedCIFDetailsFlag}</NS1:RelatedCIFDetailsFlag>
            </NS1:getCustomerAndAccountDetails_REQ>
         </NS1:executeFinacleScript_CustomData>
      </NS1:executeFinacleScriptRequest>
   </NS1:Body>
</NS1:FIXML>
";

		public MiddlewareCaller()
		{
			RequestXML = string.Format(RequestXML, "\"");
		}
		public MiddlewareCaller(string url,string action)
		{
			URL = url;
			Action = action;
		}
		public string CallWebService()
		{
			XmlDocument soapEnvelopeXml = CreateSoapEnvelope();
			HttpWebRequest webRequest = CreateWebRequest(URL, Action);
			InsertSoapEnvelopeIntoWebRequest(soapEnvelopeXml, webRequest);
			IAsyncResult asyncResult = webRequest.BeginGetResponse(null, null);
			asyncResult.AsyncWaitHandle.WaitOne();
			string soapResult;
			using (WebResponse webResponse = webRequest.EndGetResponse(asyncResult))
			{
				using (StreamReader rd = new StreamReader(webResponse.GetResponseStream()))
				{
					soapResult = rd.ReadToEnd();
				}
				return soapResult;
			}
		}

		private HttpWebRequest CreateWebRequest(string url, string action)
		{
			HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
			webRequest.Headers.Add("SOAPAction", action);
			webRequest.ContentType = "text/xml;charset=\"utf-8\"";
			webRequest.Accept = "text/xml";
			webRequest.Method = "POST";
			return webRequest;
		}

		private XmlDocument CreateSoapEnvelope()
		{
			XmlDocument soapEnvelopeDocument = new XmlDocument();
			soapEnvelopeDocument.LoadXml(GenerateXML());
			return soapEnvelopeDocument;
		}

		private string GenerateXML()
		{
			var res = RequestXML;
			foreach (var item in this.Values)
			{
				res.Replace(item.Key, item.Value);
			}
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
	}
}