using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace ConsoleOutput
{
	public class XMLTest
	{
		XmlElement current = null;
		XmlDocument xd = new XmlDocument();
		public XMLTest()
		{
			xml = string.Format(xml, "\"");
			xsds.Add(File.ReadAllText(@"C:\Users\YasarAbbas\Desktop\CustomerAndAccountDetails\RAKHeader.xsd"));
			xsds.Add(File.ReadAllText(@"C:\Users\YasarAbbas\Desktop\CustomerAndAccountDetails\CustomerAndAccountDetails_v1_resp.xsd"));
		}

		public void GotoPath(string path)
		{
			xd.Load(GenerateStreamFromString(xml));
			var parts = path.Split('/');
			current = GotoElementTemporary(path);
		}

		private XmlElement GotoElementTemporary(string path)
		{
			var parts = path.Split('/');
			XmlElement element = current;
			foreach (var part in parts)
			{
				if (element == null)
					element = xd.GetElementsByTagName(part).Item(0) as XmlElement;
				else
					element = element.GetElementsByTagName(part).Item(0) as XmlElement;
			}
			return element;
		}

		public string GetInnerText()
		{
			return current.InnerText;
		}

		public string GetInnerTextOf(string path)
		{
			return GotoElementTemporary(path).InnerText;
		}

		public Stream GenerateStreamFromString(string s)
		{
			return new MemoryStream(Encoding.UTF8.GetBytes(s));
		}
		List<string> xsds = new List<string>();
		string xml = @"
<FIXML xmlns = {0}http://www.finacle.com/fixml{0} xmlns:xsi={0}http://www.w3.org/2001/XMLSchema-instance{0} xsi:schemaLocation={0}http://www.finacle.com/fixml executeFinacleScript.xsd{0}>
   <Header>
      <ResponseHeader>
         <RequestMessageKey>
            <RequestUUID>ELX1111111111111111111</RequestUUID>
            <ServiceRequestId>executeFinacleScript</ServiceRequestId>
            <ServiceRequestVersion>10.2</ServiceRequestVersion>
            <ChannelId>ELX</ChannelId>
         </RequestMessageKey>
         <ResponseMessageInfo>
            <BankId>RAK</BankId>
            <TimeZone />
            <MessageDateTime>2020-01-06T09:48:34.833</MessageDateTime>
         </ResponseMessageInfo>
         <UBUSTransaction>
            <Id />
            <Status />
         </UBUSTransaction>
         <HostTransaction>
            <Id />
            <Status>SUCCESS</Status>
         </HostTransaction>
         <HostParentTransaction>
            <Id />
            <Status />
         </HostParentTransaction>
         <CustomInfo />
      </ResponseHeader>
   </Header>
   <Body>
      <executeFinacleScriptResponse>
         <ExecuteFinacleScriptOutputVO />
         <executeFinacleScript_CustomData>
            <getCustomerAndAccountDetails_RES>
               <CustDet>
                  <CustId>1111111</CustId>
                  <ProdProcessor>FINACLECORE,CAPS</ProdProcessor>
                  <Title>MRS.</Title>
                  <FirstName>FIRST NAME FOR    1111111</FirstName>
                  <LastName>LAST NAME FOR    1111111</LastName>
                  <FullName>ACCOUNT NAME FOR         0111111XXXXXXXXXXXXX</FullName>
                  <MinorFlg>N</MinorFlg>
                  <CustStat>ACTVE</CustStat>
                  <CustDormant>N</CustDormant>
                  <Gender>F</Gender>
                  <CustTypeCode>EB</CustTypeCode>
                  <IsRetailCust>Y</IsRetailCust>
                  <CreditRiskGrade>P2</CreditRiskGrade>
                  <NonResidentInd>N</NonResidentInd>
                  <Nationality>IN</Nationality>
                  <Addr1>12345</Addr1>
                  <Addr2>PREMISE NAME FOR   1111111</Addr2>
                  <Addr3>STREET NAME FOR        1111111</Addr3>
                  <Addr4>LOCALITY NAME FOR       1111111</Addr4>
                  <City>DXB</City>
                  <PostalCode>12345</PostalCode>
                  <Country>AE</Country>
                  <AddrType>RESIDENCE</AddrType>
                  <PhnDet>
                     <PhnType>CELLPH1</PhnType>
                     <PhnPrefFlag>Y</PhnPrefFlag>
                     <PhnCountryCode>00971</PhnCountryCode>
                     <PhnLocalCode>111111111</PhnLocalCode>
                     <PhoneNo>+00971()111111111</PhoneNo>
                  </PhnDet>
                  <PhnDet>
                     <PhnType>HOMEPH1</PhnType>
                     <PhnPrefFlag>N</PhnPrefFlag>
                     <PhnCountryCode>00971</PhnCountryCode>
                     <PhnLocalCode>111111111</PhnLocalCode>
                     <PhoneNo>+00971()111111111</PhoneNo>
                  </PhnDet>
                  <PhnDet>
                     <PhnType>OFFCPH1</PhnType>
                     <PhnPrefFlag>N</PhnPrefFlag>
                     <PhnCountryCode>00971</PhnCountryCode>
                     <PhnLocalCode>111111111</PhnLocalCode>
                     <PhoneNo>+00971()111111111</PhoneNo>
                  </PhnDet>
                  <EmlDet>
                     <EmlType>ELML1</EmlType>
                     <EmlPrefFlag>N</EmlPrefFlag>
                     <Email>1234@RAKBANK.AE</Email>
                  </EmlDet>
                  <EmlDet>
                     <EmlType>ELML2</EmlType>
                     <EmlPrefFlag>N</EmlPrefFlag>
                     <Email>1234@RAKBANK.AE</Email>
                  </EmlDet>
                  <EmlDet>
                     <EmlType>HOMEEML</EmlType>
                     <EmlPrefFlag>Y</EmlPrefFlag>
                     <Email>1234@RAKBANK.AE</Email>
                  </EmlDet>
                  <EmailId>1234@RAKBANK.AE</EmailId>
                  <ElixirPrimaryEmailId>1234@RAKBANK.AE</ElixirPrimaryEmailId>
                  <ElixirSecEmailId>1234@RAKBANK.AE</ElixirSecEmailId>
                  <TelephoneOffice>00971111111111</TelephoneOffice>
                  <TelephoneResidence>0097111111111</TelephoneResidence>
                  <MobileNum1>00971501111111</MobileNum1>
                  <CustSegment>PBD</CustSegment>
                  <CustSubSegment>PAM</CustSubSegment>
                  <ECRNNum>1111111111</ECRNNum>
                  <ReturnMailStat>N</ReturnMailStat>
                  <HoldMailInd>N</HoldMailInd>
                  <HoldMailReason />
                  <HoldMailBusinessCenter />
                  <IndustrySegment>TRADE</IndustrySegment>
                  <IndustrySubSegment>RO</IndustrySubSegment>
                  <DOB>01-01-1988</DOB>
                  <MothersMaidenName>MOTHERSMAIDENNAME   1111111</MothersMaidenName>
                  <DeclrdSal>0</DeclrdSal>
                  <custTypeDesc />
                  <MaritalStatDesc>M</MaritalStatDesc>
                  <mNumCity />
                  <mNumCntry>00971</mNumCntry>
                  <mNumPhone>501111111</mNumPhone>
                  <CountryDesc>UNITED ARAB EMIRATES</CountryDesc>
                  <IsStaff>N</IsStaff>
                  <IsFinacleRelation>Y</IsFinacleRelation>
                  <IsPremium>Y</IsPremium>
                  <IsVVIP>N</IsVVIP>
                  <IsPhyVrfnDone>X</IsPhyVrfnDone>
                  <IsScrnDone>X</IsScrnDone>
                  <IsBlacklisted>N</IsBlacklisted>
                  <isARMVirtual>N</isARMVirtual>
                  <ARMName>XXXXXXXX XXXXXXXX</ARMName>
                  <ARMPhone>971511111111</ARMPhone>
                  <SecondaryContactName>N</SecondaryContactName>
                  <AECBConsentHeld>Y</AECBConsentHeld>
                  <SrcBranch>018</SrcBranch>
                  <YearsSinceInUAE>01-01-2000</YearsSinceInUAE>
                  <ResCntry>AE</ResCntry>
                  <FatcaDet>
                     <USRelation>O</USRelation>
                     <DocumentsCollected>ID DOC!SELF-ATTEST FORM</DocumentsCollected>
                  </FatcaDet>
                  <EmpDet>
                     <EmployerID>1111111</EmployerID>
                     <EmployerName>EMPLOYER NAME FOR         1111111</EmployerName>
                     <DepartmentName>MANAGEMENT</DepartmentName>
                     <EmploymentType>Self employed</EmploymentType>
                     <EmployeeStatus>2</EmployeeStatus>
                     <Desig>PARTNER</Desig>
                     <Occupation>PRTR</Occupation>
                     <TotEmpYrs>11</TotEmpYrs>
                     <BusDuration>11</BusDuration>
                     <DOJ>01-04-2006</DOJ>
                  </EmpDet>
                  <KYCDet>
                     <KYCHeld>Y</KYCHeld>
                     <KYCReviewDate>02-08-2020</KYCReviewDate>
                  </KYCDet>
                  <OECDDet>
                     <CityOfBirth>CALCUTTA WB</CityOfBirth>
                     <CountryOfBirth>IN</CountryOfBirth>
                     <CRSUnDocFlg>Y</CRSUnDocFlg>
                     <CRSUndocFlgReason>CIF UPDATE WITHIN UAE</CRSUndocFlgReason>
                     <ReporCntryDet>
                        <CntryOfTaxRes>AE</CntryOfTaxRes>
                        <NoTINReason>A-NOT ISSUED</NoTINReason>
                        <MiscellaneousID>11111111</MiscellaneousID>
                     </ReporCntryDet>
                  </OECDDet>
                  <ChannelRegSt>
                     <IB>Y</IB>
                     <RakDirect>Y</RakDirect>
                     <RakConnect>N</RakConnect>
                     <ATMEReceipt>N</ATMEReceipt>
                  </ChannelRegSt>
                  <EStatementRegSt>
                     <Core>Y</Core>
                     <RLS>N</RLS>
                     <CPPS>Y</CPPS>
                     <Deposits>Y</Deposits>
                     <Investments>Y</Investments>
                     <Remittances>Y</Remittances>
                     <FutureService1>N</FutureService1>
                     <FutureService2>N</FutureService2>
                     <FutureService3>N</FutureService3>
                  </EStatementRegSt>
                  <DocDet>
                     <DocType>VISA</DocType>
                     <DocTypeDesc>VISA FILE NUMBER</DocTypeDesc>
                     <DocNo>11111111111111</DocNo>
                     <DocExpDate>15-07-2018</DocExpDate>
                     <DocExpStatus>Y</DocExpStatus>
                  </DocDet>
                  <DocDet>
                     <DocType>EMID</DocType>
                     <DocTypeDesc>Emirates Id</DocTypeDesc>
                     <DocNo>11111111111111</DocNo>
                     <DocExpDate>10-07-2021</DocExpDate>
                     <DocExpStatus>N</DocExpStatus>
                  </DocDet>
                  <DocDet>
                     <DocType>PPT</DocType>
                     <DocTypeDesc>PASSPORT</DocTypeDesc>
                     <DocNo>AE1111111</DocNo>
                     <DocExpDate>31-12-2025</DocExpDate>
                     <DocExpStatus>N</DocExpStatus>
                     <IsDocVerified>Y</IsDocVerified>
                  </DocDet>
               </CustDet>
               <AcctDet>
                  <AcctId>1111111111111</AcctId>
                  <AcctCurr>GRM</AcctCurr>
                  <IBANNumber>AE640400001111111111111</IBANNumber>
                  <AccountStat>A</AccountStat>
                  <AcctBal>
                     <BalType>LEDGER</BalType>
                     <amountValue>20.000000</amountValue>
                     <currencyCode>GRM</currencyCode>
                  </AcctBal>
                  <AcctBal>
                     <BalType>AVAIL</BalType>
                     <amountValue>20</amountValue>
                     <currencyCode>GRM</currencyCode>
                  </AcctBal>
                  <AcctBal>
                     <BalType>EFFAVL</BalType>
                     <amountValue>20.0000</amountValue>
                     <currencyCode>GRM</currencyCode>
                  </AcctBal>
                  <AcctBal>
                     <BalType>FLOAT</BalType>
                     <amountValue>0</amountValue>
                     <currencyCode>GRM</currencyCode>
                  </AcctBal>
                  <AcctBal>
                     <BalType>SHADOW</BalType>
                     <amountValue>0.000000</amountValue>
                     <currencyCode>GRM</currencyCode>
                  </AcctBal>
                  <AcctBal>
                     <BalType>LEIN</BalType>
                     <amountValue>0</amountValue>
                     <currencyCode>GRM</currencyCode>
                  </AcctBal>
                  <AcctBal>
                     <BalType>DRWPWR</BalType>
                     <amountValue>0</amountValue>
                     <currencyCode>GRM</currencyCode>
                  </AcctBal>
                  <AcctBal>
                     <BalType>FULLEFFAVLBAL</BalType>
                     <amountValue>20.0000</amountValue>
                     <currencyCode>GRM</currencyCode>
                  </AcctBal>
                  <TODAmt>0.00</TODAmt>
                  <SchmType>SBA</SchmType>
                  <SchmCode>AXAP1</SchmCode>
                  <AcctName>ACCOUNT NAME FOR       1111111111111</AcctName>
                  <AcctShortName>ST   11111</AcctShortName>
                  <AcctChargeOff>N</AcctChargeOff>
                  <JointAcctInd>N</JointAcctInd>
                  <AcctSegment>PBD</AcctSegment>
                  <AcctSubSegment>PAM</AcctSubSegment>
                  <BranchId>018</BranchId>
                  <AcctOpnDt>04-12-2017</AcctOpnDt>
                  <ARMCode>XXXXXXXXXXX</ARMCode>
                  <StatemntFrq>M</StatemntFrq>
                  <LstStPrntDt>09-11-2019</LstStPrntDt>
                  <nxtStPrntDt>09-12-2019</nxtStPrntDt>
                  <NonLocalCrncyFlg>Y</NonLocalCrncyFlg>
                  <ChqAlwdFlg>N</ChqAlwdFlg>
                  <ModeOfOperation>100</ModeOfOperation>
                  <acctCrncyNum>123</acctCrncyNum>
               </AcctDet>
            </getCustomerAndAccountDetails_RES>
         </executeFinacleScript_CustomData>
      </executeFinacleScriptResponse>
   </Body>
</FIXML>
";
	}
}
