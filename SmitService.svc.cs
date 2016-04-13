using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Data.Odbc;
using System.Configuration;
using System.Xml.Linq;
using System.Diagnostics;

namespace SmitService.WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "SmitService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select SmitService.svc or SmitService.svc.cs at the Solution Explorer and start debugging.
    public class SmitService : ISmitService
    {

        //US 22.01.2014
        //Daten für die Kommunikation werden alle aus der Tabelle "marketingkontakt" abgefragt, da eine Zustimmung des Kunden vorliegen muss (RB 22.01.2014)
        public getCustomersResponse getCustomers(getCustomersRequest request)
        {

            Int32 kontonr;
            string anrede = "";
            string vorname = "";
            string name1 = "";
            string name2 = "";
            string name3 = "";
            string suchname = "";
            string strasse = "";
            string plz = "";
            string ort = "";
            string land = "";
            string telefonp = "";
            string mobilp = "";
            string emailp = "";
            string telefond = "";
            string mobild = "";
            string emaild = "";
            bool ok1marketing = false;
            bool ok1post = false;
            bool ok1telefonp = false;
            bool ok1mobilp = false;
            bool ok1emailp = false;
            bool ok1telefond = false;
            bool ok1mobild = false;
            bool ok1emaild = false;
            DateTime creationdate;
            string creationuser = "";
            DateTime modifieddate;
            string modifieduser = "";
            string gebDat = "";
            DateTime geburtsdatum;
            DateTime dtDefaultDB;
            dtDefaultDB = Convert.ToDateTime("01.01.1900");
            creationdate = dtDefaultDB;
            modifieddate = dtDefaultDB;
            bool privatPerson = false;
            string steuernummer = "";
            string ustidnr = "";
            string datWerkstatt = "";
            DateTime datumWerkstatt;
            string datTheke = "";
            DateTime datumTheke;
            string art = "";
            string gebiet = "";
            
            getCustomersResponse res = new getCustomersResponse();

            interfaceData iData = new interfaceData();
            customerType cust;
            customerTypeAddress custAdr;
            customerTypeName custName;
            customerTypePhoneMobil mobile;
            customerTypeEmail email;

            System.Collections.ArrayList aAdressen = new System.Collections.ArrayList();

            salesmanType sT = new salesmanType();
            sT.identifier = "fmad";
            sT.name = "Fmade";

            try
            {

                using (DBConnect myConnect = new DBConnect())
                {
                    String sqlCommand;
                    //Limitierung auf 100 Adressdatensätzen pro Abruf

                    sqlCommand = "SELECT TOP 100 START AT " + request.Body.arg0.ToString() + " a.kontonr, a.anrede, a.vorname, a.name1, a.name2, a.name3, a.suchname, a.strasse, a.plz, a.ort, a.land, ";
                    sqlCommand += " b.telefonp, b.mobilp, b.emailp, b.telefond, b.mobild, b.emaild,";
                    sqlCommand += " b.ok1marketing, b.ok1post, b.ok1telefonp, b.ok1mobilp, b.ok1emailp, b.ok1telefond, b.ok1mobild, b.ok1emaild,";
                    sqlCommand += " a.creationdate, a.creationuser, a.modifieddate, a.modifieduser, a.geburtsdatum, a.steuernummer, a.ustidnr, a.art, a.datumwerkstatt, a.datumtheke, a.gebiet";
                    sqlCommand += " FROM bungert.adresse a LEFT OUTER JOIN bungert.marketingkontakt b";
                    sqlCommand += " ON a.adressid = b.adressid WHERE aktiv = 1";
                    sqlCommand += " ORDER BY a.adressid";

                    OdbcCommand cmd = new OdbcCommand(sqlCommand, myConnect.conn);

                    myConnect.Connect();

                    OdbcDataReader reader;
                    //DataReader Objekt wird initialisiert

                    using (reader = cmd.ExecuteReader())
                    {

                        if (reader.HasRows)
                        {

                            while (reader.Read())
                            {
                                cust = new customerType();
                                custAdr = new customerTypeAddress();
                                privatPerson = false;
                                
                                kontonr = 0;

                                kontonr = reader.GetInt32(0);
                                anrede = reader.GetValue(1).ToString();
                                vorname = reader.GetValue(2).ToString();
                                name1 = reader.GetValue(3).ToString();
                                name2 = reader.GetValue(4).ToString();
                                name3 = reader.GetValue(5).ToString();
                                suchname = reader.GetValue(6).ToString();
                                strasse = reader.GetValue(7).ToString();
                                plz = reader.GetValue(8).ToString();
                                ort = reader.GetValue(9).ToString();
                                land = reader.GetValue(10).ToString();

                                telefonp = reader.GetValue(11).ToString();
                                mobilp = reader.GetValue(12).ToString();
                                emailp = reader.GetValue(13).ToString();
                                telefond = reader.GetValue(14).ToString();
                                mobild = reader.GetValue(15).ToString();
                                emaild = reader.GetValue(16).ToString();

                                ok1marketing = (String.IsNullOrWhiteSpace(reader.GetValue(17).ToString()) ? false : reader.GetBoolean(17));
                                ok1post = (String.IsNullOrWhiteSpace(reader.GetValue(18).ToString()) ? false : reader.GetBoolean(18));
                                ok1telefonp = (String.IsNullOrWhiteSpace(reader.GetValue(19).ToString()) ? false : reader.GetBoolean(19));
                                ok1mobilp = (String.IsNullOrWhiteSpace(reader.GetValue(20).ToString()) ? false : reader.GetBoolean(20));
                                ok1emailp = (String.IsNullOrWhiteSpace(reader.GetValue(21).ToString()) ? false : reader.GetBoolean(21));
                                ok1telefond = (String.IsNullOrWhiteSpace(reader.GetValue(22).ToString()) ? false : reader.GetBoolean(22));
                                ok1mobild = (String.IsNullOrWhiteSpace(reader.GetValue(23).ToString()) ? false : reader.GetBoolean(23));
                                ok1emaild = (String.IsNullOrWhiteSpace(reader.GetValue(24).ToString()) ? false : reader.GetBoolean(24));

                                creationdate = reader.GetDateTime(25);
                                creationuser = reader.GetValue(26).ToString();
                                modifieddate = reader.GetDateTime(27);
                                modifieduser = reader.GetValue(28).ToString();
                                gebDat = reader.GetValue(29).ToString();
                                steuernummer = reader.GetValue(30).ToString();
                                ustidnr = reader.GetValue(31).ToString();
                                art = reader.GetValue(32).ToString();
                                datWerkstatt = reader.GetValue(33).ToString();
                                datTheke = reader.GetValue(34).ToString();
                                gebiet = reader.GetValue(35).ToString(); //Verkäufer

                                if (String.IsNullOrWhiteSpace(gebDat))
                                {
                                    geburtsdatum = dtDefaultDB;
                                }
                                else
                                {
                                    geburtsdatum = Convert.ToDateTime(gebDat);
                                }

                                if (String.IsNullOrWhiteSpace(steuernummer))
                                {
                                    steuernummer = "";
                                }


                                if (String.IsNullOrWhiteSpace(ustidnr))
                                {
                                    ustidnr = "";
                                }

                                if (String.IsNullOrWhiteSpace(datWerkstatt))
                                {
                                    datumWerkstatt = dtDefaultDB;
                                }
                                else
                                {
                                    datumWerkstatt = Convert.ToDateTime(datWerkstatt);
                                }

                                if (String.IsNullOrWhiteSpace(datTheke))
                                {
                                    datumTheke = dtDefaultDB;
                                }
                                else
                                {
                                    datumTheke = Convert.ToDateTime(datTheke);
                                }

                                if (!String.IsNullOrWhiteSpace(gebiet))
                                {
                                    salesmanType salesman = new salesmanType();
                                    salesman.name = gebiet;
                                    salesman.identifier = gebiet;
                                    cust.salesman = salesman;
                                }

                                custAdr.city = ort;
                                custAdr.country = land;
                                custAdr.street = strasse;
                                custAdr.zip = plz;

                                cust.address = custAdr;

                                salesmanType sMT = new salesmanType();

                                sMT.identifier = creationuser;
                                sMT.name = creationuser;

                                customerTypeCreation custCr = new customerTypeCreation();
                                custCr.date = creationdate;
                                custCr.dateSpecified = true;
                                custCr.location = 1;
                                custCr.locationSpecified = true;
                                custCr.salesman = sMT;

                                cust.creation = custCr;

                                sMT = new salesmanType();
                                sMT.identifier = modifieduser;
                                sMT.name = modifieduser;

                                customerTypeLastChange custLC = new customerTypeLastChange();
                                custLC.date = modifieddate;
                                custLC.dateSpecified = true;
                                custLC.location = 1;
                                custLC.locationSpecified = true;
                                custLC.salesman = sMT;

                                cust.lastChange = custLC;

                                customerIdentificationType custID = new customerIdentificationType();

                                //Testdaten "location" muss noch mit fmade geklärt werden
                                custID.location = 1;
                                custID.number = kontonr;

                                cust.customerNumber = custID;

                                cust.matchcode = String.IsNullOrWhiteSpace(suchname) ? "" : suchname;
                                cust.firstName = String.IsNullOrWhiteSpace(vorname) ? "" : vorname;

                                //cust.fsalesNumber = 2;

                                cust.salutationTypeSpecified = false;

                                // Salutatuon 1 = Herr / 2 = Frau
                                if (anrede.ToLower().Contains("herr"))
                                {
                                    cust.salutation = 1;
                                    cust.salutationSpecified = true;
                                    customerTypeSalutationLetter custSL = new customerTypeSalutationLetter();
                                    custSL.id = 1;
                                    custSL.description = String.IsNullOrWhiteSpace(name1) ? "" : name1;
                                    custSL.idSpecified = true;
                                    cust.salutationLetter = custSL;
                                    privatPerson = true;
                                }
                                else if (anrede.ToLower().Contains("frau"))
                                {
                                    cust.salutation = 2;
                                    cust.salutationSpecified = true;
                                    customerTypeSalutationLetter custSL = new customerTypeSalutationLetter();
                                    custSL.id = 2;
                                    custSL.description = String.IsNullOrWhiteSpace(name1) ? "" : name1;
                                    custSL.idSpecified = true;
                                    cust.salutationLetter = custSL;
                                    privatPerson = true;
                                }
                                else if (anrede.ToLower().Contains("firma"))
                                {
                                    cust.salutation = 9;
                                    cust.salutationSpecified = true;
                                    customerTypeSalutationLetter custSL = new customerTypeSalutationLetter();
                                    custSL.id = 9;
                                    custSL.idSpecified = true;
                                    cust.salutationLetter = custSL;
                                    privatPerson = false;
                                }
                                else //keine Anrede vorhanden
                                {
                                    cust.salutation = 0;
                                    cust.salutationSpecified = true;
                                    customerTypeSalutationLetter custSL = new customerTypeSalutationLetter();
                                    custSL.id = 0;
                                    custSL.idSpecified = true;
                                    cust.salutationLetter = custSL;
                                    privatPerson = true;
                                }

                                customerTypeName[] custNames = new customerTypeName[3];
                                //List<customerTypeName> custNames = new List<customerTypeName>();

                                custName = new customerTypeName();
                                custName.sequence = 1;

                                if (privatPerson)
                                {
                                    cust.lastName = name1;
                                    name1 = vorname + " " + name1;
                                }

                                custName.name = name1;
                                custNames[0] = custName;

                                custName = new customerTypeName();
                                custName.sequence = 2;
                                custName.name = name2;
                                custNames[1] = custName;

                                custName = new customerTypeName();
                                custName.sequence = 3;
                                custName.name = name3;
                                custNames[2] = custName;

                                cust.name = custNames;

                                cust.birthDateSpecified = false;

                                if (geburtsdatum.Date > dtDefaultDB.Date)
                                {
                                    cust.birthDate = geburtsdatum;
                                    cust.birthDateSpecified = true;
                                }

                                customerTypeInvoice inv = new customerTypeInvoice();

                                customerTypeInvoiceLastPart lastPart = new customerTypeInvoiceLastPart();
                                customerTypeInvoiceLastWokshop lastWorkShop = new customerTypeInvoiceLastWokshop();

                                lastPart.dateSpecified = false;
                                lastWorkShop.dateSpecified = false;

                                if (datumWerkstatt.Date > dtDefaultDB)
                                {
                                    lastWorkShop.date = datumWerkstatt;
                                    lastWorkShop.dateSpecified = true;
                                }

                                if (datumTheke.Date > dtDefaultDB)
                                {
                                    lastPart.date = datumTheke;
                                    lastPart.dateSpecified = true;
                                }

                                inv.lastPart = lastPart;
                                inv.lastWokshop = lastWorkShop;

                                cust.invoice = inv;

                                if (art.ToLower().Trim() == "k")
                                {
                                    cust.customerType1 = customerTypeCustomerType.customer; //Kunde
                                    cust.customerType1Specified = true;
                                }
                                else if (art.ToLower().Trim() == "i")
                                {
                                    cust.customerType1 = customerTypeCustomerType.prospect; //Interessent
                                    cust.customerType1Specified = true;
                                }


                                customerTypeMarketing custMarketing = new customerTypeMarketing();
                                custMarketing.postWanted = ok1post;
                                custMarketing.postWantedSpecified = true;
                                custMarketing.emailWanted = ok1emailp;
                                custMarketing.emailWantedSpecified = true;
                                custMarketing.phoneWanted = ok1telefonp;
                                custMarketing.phoneWantedSpecified = true;
                                custMarketing.useData = ok1marketing;
                                custMarketing.useDataSpecified = true;
                                custMarketing.smsWanted = ok1mobilp;
                                custMarketing.smsWantedSpecified = true;
                                custMarketing.useData = ok1marketing;
                                custMarketing.useDataSpecified = true;

                                customerTypePhone custPhone = new customerTypePhone();
                                custPhone.@private = telefonp;
                                custPhone.business = telefond;

                                customerTypePhoneMobil[] mobiles = new customerTypePhoneMobil[2];
                                //List<customerTypePhoneMobil> mobiles = new List<customerTypePhoneMobil>();

                                mobile = new customerTypePhoneMobil();
                                mobile.number = mobilp;
                                mobile.sequence = 1;

                                mobiles[0] = mobile;

                                mobile = new customerTypePhoneMobil();
                                mobile.sequence = 2;
                                mobile.number = mobild;

                                mobiles[1] = mobile;

                                custPhone.mobil = mobiles;
                                cust.phone = custPhone;

                                customerTypeEmail[] emails = new customerTypeEmail[2];
                                //List<customerTypeEmail> emails = new List<customerTypeEmail>();

                                email = new customerTypeEmail();
                                email.address = emailp;
                                email.sequence = 1;

                                emails[0] = email;

                                email = new customerTypeEmail();
                                email.address = emaild;
                                email.sequence = 2;

                                emails[1] = email;

                                cust.email = emails;

                                cust.marketing = custMarketing;

                                customerTypeNumbers cTN = new customerTypeNumbers();
                                cTN.vatNumber = ustidnr;
                                cTN.taxNumber = steuernummer;

                                cust.numbers = cTN;

                                aAdressen.Add(cust);

                            }

                        }
                        reader.Close();
                    }
                }

                #region testdaten
                //custAdr.city = "Linz";
                //custAdr.country = "AT";
                //custAdr.street = "Teststraße";
                //custAdr.zip = "4020";

                //cust.address = custAdr;

                //salesmanType sT = new salesmanType();
                //sT.identifier = "fmad";
                //sT.name = "Fmade";

                //customerTypeCreation custCr = new customerTypeCreation();
                //custCr.date = DateTime.Today;
                //custCr.dateSpecified = true;
                //custCr.location = 1;
                //custCr.locationSpecified = true;
                //custCr.salesman = sT;

                //cust.creation = custCr;

                //customerTypeLastChange custLC = new customerTypeLastChange();
                //custLC.date = DateTime.Today;
                //custLC.dateSpecified = true;
                //custLC.location = 1;
                //custLC.locationSpecified = true;

                //custLC.salesman = sT;

                //cust.lastChange = custLC;

                //customerIdentificationType custID = new customerIdentificationType();
                //custID.location = 1;
                //custID.number = 2;

                //cust.customerNumber = custID;

                //cust.firstName = "Uwe";
                //cust.fsalesNumber = 2;
                //cust.lastName = "Schwan";

                //customerTypeName[] custNames = new customerTypeName[1];
                //customerTypeName custName = new customerTypeName();

                //custName.sequence = 1;
                //custName.name = "Uwe Schwan";

                //custNames[0] = custName;

                //cust.name = custNames;

                //customerTypeMarketing custMarketing = new customerTypeMarketing();
                //custMarketing.emailWanted = true;
                //custMarketing.emailWantedSpecified = true;
                //custMarketing.phoneWanted = true;
                //custMarketing.phoneWantedSpecified = true;
                //custMarketing.useData = true;
                //custMarketing.useDataSpecified = true;

                //cust.marketing = custMarketing;

                //cust.salutation = 1;
                //cust.salutationSpecified = true;


                //customer[0] = cust;
                #endregion

                customerType[] aCustomers = new customerType[aAdressen.Count];
                //List<customerType> aCustomers = new List<customerType>();
                for (int i = 0; i < aAdressen.Count; i++)
                {
                    customerType customer = (customerType)aAdressen[i];
                    aCustomers[i] = customer;
                }

                iData.customer = aCustomers;
                iData.interfaceVersion = 1;
                iData.dataProvider = "STANDARD_INTERFACE";

                iData.callingUser = sT;
                iData.transmissionReason = "";

                System.IO.StringWriter stringWriter = new System.IO.StringWriter();

                var serializer = new XmlSerializer(typeof(interfaceData));
                using (var xw = XmlWriter.Create(stringWriter, new XmlWriterSettings { Encoding = new UTF8Encoding() }))
                {
                    serializer.Serialize(xw, iData);

                }

                #region xml encoding
                //string result;
                //using (MemoryStream memoryStream = new MemoryStream())
                //{
                //    XmlSerializer xs = new XmlSerializer(typeof(interfaceData));
                //    XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
                //    xs.Serialize(xmlTextWriter, iData);
                //    result = Encoding.UTF8.GetString(memoryStream.ToArray());
                //}
                //if (String.IsNullOrEmpty(result))
                //    result = "";
                #endregion

                getCustomersResponseBody resBody = new getCustomersResponseBody();

                resBody.@return = stringWriter.ToString();
                res.Body = resBody;

                SmitConfig sc = new SmitConfig();
                sc = ReadXML();
                sc.changedCustomerDate = DateTime.Now.ToString("yyyy-MM-dd");

                WriteXML(sc);
            }
            catch (System.Exception ex)
            {
                Trace.WriteLine(DateTime.Now.ToString() + " - " + ex.Message, "SmitService");
                res.Body.@return = "-1";
            }

            return res;
        }


        public getCustomerCountResponse getCustomerCount(getCustomerCountRequest request)
        {

            getCustomerCountResponse res = new getCustomerCountResponse();
            Int32 anzahlAdressen;
            anzahlAdressen = 0;

            try
            {
                using (DBConnect myConnect = new DBConnect())
                {
                    String sqlCommand;
                    sqlCommand = "SELECT count(kontonr)";
                    sqlCommand += " FROM bungert.adresse";
                    sqlCommand += " WHERE adresse.aktiv = 1";
                    OdbcCommand cmd = new OdbcCommand(sqlCommand, myConnect.conn);

                    myConnect.Connect();

                    OdbcDataReader reader;
                    //DataReader Objekt wird initialisiert

                    using (reader = cmd.ExecuteReader())
                    {

                        if (reader.HasRows)
                        {
                            anzahlAdressen = reader.GetInt32(0);
                        }

                        reader.Close();
                    }
                }

                res.@return = anzahlAdressen;

            }
            catch (System.Exception ex)
            {
                Trace.WriteLine(DateTime.Now.ToString() + " - " + ex.Message, "SmitService");
                res.@return = -1;
            }


            //Test Anzahl Adressen auf 1 beschränkt US 16.01.2014
            //res.@return = 1;
            return res;
        }


        public getCustomerVehiclesResponse getCustomerVehicles(getCustomerVehiclesRequest request)
        {
            Int32 kontonr;
            Int32 fahrzeugid;
            string fahrgestellnr = "";
            string zulassungskz = "";
            string marke = "";
            string verkaufstyp = "";
            string bezeichnung = "";
            string motorseriennr = "";
            DateTime datumerstzulassung;
            string datErstzulassung = "";
            Int32 kilometerstand;
            string farbe = "";
            string innenausstattung = "";
            Int32 kw;
            DateTime datumbesuch;
            string datBesuch = "";
            DateTime creationdate;
            string creationuser = "";
            DateTime modifieddate;
            string modifieduser = "";
            DateTime datumzulassungkunde;
            string datZulassungkunde = "";
            DateTime datumhu;
            string datHU = "";
            DateTime datumau;
            string datAU = "";
            string zuendschluesselnr = "";
            string tuerschluesselnr = "";
            string radiocode = "";
            Int32 hubraum;
            string kbahsnr = "";
            string kbatsnr = "";
            string motortyp = "";
            string kfzbriefnr = "";
            string treibstoff = "";
            string info = "";
            string kfzversicherung = "";
            Int32 modelljahr;
            bool aktiv = false;
            string strVerkaeufer = "";
            string strKW = "";
            string strHubraum = "";
            string strModelljahr = "";
            string strKilometerstand = "";

            DateTime dtDefaultDB;
            dtDefaultDB = Convert.ToDateTime("01.01.1900");
            creationdate = dtDefaultDB;
            modifieddate = dtDefaultDB;
            datumzulassungkunde = dtDefaultDB;
            datumhu = dtDefaultDB;
            datumau = dtDefaultDB;
            datumbesuch = dtDefaultDB;
            datumerstzulassung = dtDefaultDB;

            getCustomerVehiclesResponse res = new getCustomerVehiclesResponse();
           
            interfaceData iData = new interfaceData();
            customerVehicleType cV;
           
            System.Collections.ArrayList aFahrzeuge = new System.Collections.ArrayList();

            salesmanType sT = new salesmanType();
            sT.identifier = "fmad";
            sT.name = "Fmade";

            using (DBConnect myConnect = new DBConnect())
            {
                String sqlCommand;
                //Limitierung auf 100 Adressdatensätzen pro Abruf
                sqlCommand = "SELECT TOP 100 START AT " + request.Body.arg0.ToString() + " a.kontonr, b.fahrzeugid, b.fahrgestellnr, b.zulassungskz, b.marke, b.verkaufstyp, b.bezeichnung, ";
                //sqlCommand = "SELECT  a.kontonr, b.fahrzeugid, b.fahrgestellnr, b.zulassungskz, b.marke, b.verkaufstyp, b.bezeichnung, ";
                sqlCommand += "b.motorseriennr, b.datumerstzulassung, b.kilometerstand, b.farbe, b.innenausstattung, b.kw, b.verkaeufer, b.datumbesuch, b.creationdate, b.creationuser, b.modifieddate, b.modifieduser, ";
                sqlCommand += "b.datumzulassungkunde, b.datumhu, b.datumau, b.zuendschluesselnr, b.tuerschluesselnr, b.radiocode, b.hubraum, b.kbahsnr, b.motortyp, b.kfzbriefnr, b.treibstoff, b.info, b.kfzversicherung, b.modelljahr, ";
                sqlCommand += " b.aktiv, b.kbatsnr";
                sqlCommand += " FROM bungert.adresse a, bungert.fahrzeug b";
                sqlCommand += " WHERE  a.adressid = b.kundenadrid";
                //sqlCommand += " AND b.zulassungskz = 'B-AR 6700'";
                sqlCommand += " ORDER BY b.fahrzeugid";
                OdbcCommand cmd = new OdbcCommand(sqlCommand, myConnect.conn);
                //cmd.Parameters.Add(":fahrgestellnr", OdbcType.VarChar, 17).Value = vin;

                myConnect.Connect();

                OdbcDataReader reader;
                //DataReader Objekt wird initialisiert

                using (reader = cmd.ExecuteReader())
                {

                    if (reader.HasRows)
                    {

                        while (reader.Read())
                        {
                            cV = new customerVehicleType();
                                                       
                            kontonr = 0;

                            kontonr = reader.GetInt32(0);
                            fahrzeugid = reader.GetInt32(1);
                            fahrgestellnr = reader.GetValue(2).ToString();
                            zulassungskz = reader.GetValue(3).ToString();
                            marke = reader.GetValue(4).ToString();
                            verkaufstyp = reader.GetValue(5).ToString();
                            bezeichnung = reader.GetValue(6).ToString();
                            motorseriennr = reader.GetValue(7).ToString();
                            datErstzulassung = reader.GetValue(8).ToString();
                            strKilometerstand = reader.GetValue(9).ToString();
                            farbe = reader.GetValue(10).ToString();
                            innenausstattung = reader.GetValue(11).ToString();
                            strKW = reader.GetValue(12).ToString();
                            strVerkaeufer = reader.GetValue(13).ToString();
                            datBesuch = reader.GetValue(14).ToString();
                            creationdate = reader.GetDateTime(15);
                            creationuser = reader.GetValue(16).ToString();
                            modifieddate = reader.GetDateTime(17);
                            modifieduser = reader.GetValue(18).ToString();
                            datZulassungkunde = reader.GetValue(19).ToString();
                            datHU = reader.GetValue(20).ToString();
                            datAU = reader.GetValue(21).ToString();
                            zuendschluesselnr = reader.GetValue(22).ToString();
                            tuerschluesselnr = reader.GetValue(23).ToString();
                            radiocode = reader.GetValue(24).ToString();
                            strHubraum = reader.GetValue(25).ToString();
                            kbahsnr = reader.GetValue(26).ToString();
                            motortyp = reader.GetValue(27).ToString();
                            kfzbriefnr = reader.GetValue(28).ToString();
                            treibstoff = reader.GetValue(29).ToString();
                            info = reader.GetValue(30).ToString();
                            kfzversicherung = reader.GetValue(31).ToString();
                            strModelljahr = reader.GetValue(32).ToString();
                            aktiv = reader.GetBoolean(33);
                            kbatsnr = reader.GetValue(34).ToString();

                            //Datum Erstzulassung
                            if (String.IsNullOrWhiteSpace(datErstzulassung))
                            {
                                datumerstzulassung = dtDefaultDB;
                            }
                            else
                            {
                                datumerstzulassung = Convert.ToDateTime(datErstzulassung);
                            }

                            //Datum Besuch
                            if (String.IsNullOrWhiteSpace(datBesuch))
                            {
                                datumbesuch = dtDefaultDB;
                            }
                            else
                            {
                                datumbesuch = Convert.ToDateTime(datBesuch);
                            }

                            //Datum Zulassungkunde
                            if (String.IsNullOrWhiteSpace(datZulassungkunde))
                            {
                                datumzulassungkunde = dtDefaultDB;
                            }
                            else
                            {
                                datumzulassungkunde = Convert.ToDateTime(datZulassungkunde);
                            }

                            //Datum HU
                            if (String.IsNullOrWhiteSpace(datHU))
                            {
                                datumhu = dtDefaultDB;
                            }
                            else
                            {
                                datumhu = Convert.ToDateTime(datHU);
                            }

                            //Datum AU
                            if (String.IsNullOrWhiteSpace(datAU))
                            {
                                datumau = dtDefaultDB;
                            }
                            else
                            {
                                datumau = Convert.ToDateTime(datAU);
                            }


                            customerIdentificationType custID = new customerIdentificationType();
                            custID.number = kontonr;

                            //US 10.02.2014
                            //Standort muss zum Verkaeufer passen - standardmäßig "location=1"
                            custID.location = 1;
                                                        
                            cV.customerNumber = custID;
                            cV.vehicleNumber = fahrzeugid;
                            cV.vin = fahrgestellnr;

                            if (aktiv)
                            {
                                cV.deleted = false;
                            }
                            else
                            {
                                cV.deleted = true;
                            }

                            cV.licensePlate = zulassungskz;
                            cV.brand = marke;
                            cV.typeCode = verkaufstyp;
                            cV.type = bezeichnung;

                            if (String.IsNullOrWhiteSpace(strModelljahr))
                            {
                                modelljahr = 0;
                            }
                            else 
                            {
                                modelljahr = Convert.ToInt32(strModelljahr);
                            }

                            cV.modelYear = Convert.ToDateTime("01.01." + modelljahr.ToString());
                            cV.modelYearSpecified = true;
                            cV.motorNumber = motorseriennr;
                            cV.firstRegistration = datumerstzulassung;
                            cV.firstRegistrationSpecified = true;

                            if (String.IsNullOrWhiteSpace(strKilometerstand))
                            {
                                kilometerstand = 0;
                            }
                            else
                            {
                                kilometerstand = Convert.ToInt32(strKilometerstand);
                            }

                            cV.milage = kilometerstand;
                            cV.milageSpecified = true;
                            cV.exteriorColor = farbe;
                            cV.interiorColor = innenausstattung;

                            if (String.IsNullOrWhiteSpace(strKW))
                            {
                                kw = 0;
                            }
                            else
                            {
                                kw = Convert.ToInt32(strKW);
                            }

                            cV.kw = kw;

                            customerVehicleTypeLastInvoice custLI = new customerVehicleTypeLastInvoice();
                            custLI.date = datumbesuch;
                            custLI.dateSpecified = true;
                            cV.lastInvoice = custLI;

                            customerVehicleTypeCreation custCreate = new customerVehicleTypeCreation();
                            custCreate.date = creationdate;
                            custCreate.dateSpecified = true;

                            cV.creation = custCreate;

                            salesmanType custST = new salesmanType();
                            custST.identifier = creationuser;
                            custST.name = creationuser;

                            custCreate.salesman = custST;

                            customerVehicleTypeLastChange custLS = new customerVehicleTypeLastChange();
                            custLS.date = modifieddate;
                            custLS.dateSpecified = true;

                            custST = new salesmanType();
                            custST.identifier = modifieduser;
                            custST.name = modifieduser;

                            custLS.salesman = custST;

                            cV.lastChange = custLS;

                            //Datum Zulassungkunde
                            cV.lastRegistrationSpecified = false;

                            if (datumzulassungkunde.Date > dtDefaultDB.Date)
                            {
                                cV.lastRegistration = datumzulassungkunde;
                                cV.lastRegistrationSpecified = true;
                            }

                            //Datum HU
                            cV.testDate1Specified = false;

                            if (datumhu.Date > dtDefaultDB.Date)
                            {
                                cV.testDate1 = datumhu;
                                cV.testDate1Specified = true;
                            }

                            //Datum AU
                            cV.testDate2Specified = false;

                            if (datumau.Date > dtDefaultDB.Date)
                            {
                                cV.testDate2 = datumau;
                                cV.testDate2Specified = true;
                            }
                            
                            cV.keyCode1 = zuendschluesselnr;
                            cV.keyCode2 = tuerschluesselnr;
                            cV.radioCode = radiocode;

                            if (String.IsNullOrWhiteSpace(strHubraum))
                            {
                                hubraum = 0;
                            }
                            else
                            {
                                hubraum = Convert.ToInt32(strHubraum);
                            }

                            if (String.IsNullOrWhiteSpace(kbahsnr)) kbahsnr = "";
                            if (String.IsNullOrWhiteSpace(kbatsnr)) kbatsnr = "";
                            cV.kbaNumber = kbahsnr + "/" + kbatsnr;
                            cV.capacity = hubraum;
                            cV.capacitySpecified = true;
                            cV.motorCode = motortyp;
                            cV.carsLetter = kfzbriefnr;
                            cV.fuelDescription = treibstoff;
                            cV.remark = info;

                            customerVehicleTypeInsurance custInsurance = new customerVehicleTypeInsurance();
                            custInsurance.policyNumber = kfzversicherung;

                            cV.insurance = custInsurance;

                            aFahrzeuge.Add(cV);

                        }

                    }
                    reader.Close();
                }
            }

            customerVehicleType[] aCustVehicle = new customerVehicleType[aFahrzeuge.Count];
            //List<customerVehicleType> aCustVehicle = new List<customerVehicleType>();
            for (int i = 0; i < aFahrzeuge.Count; i++)
            {
                customerVehicleType custV = (customerVehicleType)aFahrzeuge[i];
                aCustVehicle[i] = custV;
            }

            iData.customerVehicle = aCustVehicle;
            iData.interfaceVersion = 1;
            iData.dataProvider = "STANDARD_INTERFACE";

            iData.callingUser = sT;
            iData.transmissionReason = "";

            System.IO.StringWriter stringWriter = new System.IO.StringWriter();

            var serializer = new XmlSerializer(typeof(interfaceData));
            using (var xw = XmlWriter.Create(stringWriter, new XmlWriterSettings { Encoding = new UTF8Encoding() }))
            {
                serializer.Serialize(xw, iData);

            }

            #region xml encoding
            //string result;
            //using (MemoryStream memoryStream = new MemoryStream())
            //{
            //    XmlSerializer xs = new XmlSerializer(typeof(interfaceData));
            //    XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
            //    xs.Serialize(xmlTextWriter, iData);
            //    result = Encoding.UTF8.GetString(memoryStream.ToArray());
            //}
            //if (String.IsNullOrEmpty(result))
            //    result = "";
            #endregion

            getCustomerVehiclesResponseBody resBody = new getCustomerVehiclesResponseBody();

            resBody.@return = stringWriter.ToString();

            res.Body = resBody;

            SmitConfig sc = new SmitConfig();
            sc = ReadXML();
            sc.changedCustomerVehicleDate = DateTime.Now.ToString("yyyy-MM-dd");

            WriteXML(sc);

            return res;
        }

        public getCustomerVehicleCountResponse getCustomerVehicleCount(getCustomerVehicleCountRequest request)
        {
            getCustomerVehicleCountResponse res = new getCustomerVehicleCountResponse();
            Int32 anzahlFahrzeuge;
            anzahlFahrzeuge = 0;

            using (DBConnect myConnect = new DBConnect())
            {
                String sqlCommand;
                sqlCommand = "SELECT count(fahrzeugid)";
                sqlCommand += " FROM bungert.fahrzeug";
                //sqlCommand += " WHERE fahrzeug.aktiv = 1";
                OdbcCommand cmd = new OdbcCommand(sqlCommand, myConnect.conn);

                myConnect.Connect();

                OdbcDataReader reader;
                //DataReader Objekt wird initialisiert

                using (reader = cmd.ExecuteReader())
                {

                    if (reader.HasRows)
                    {
                        anzahlFahrzeuge = reader.GetInt32(0);
                    }

                    reader.Close();
                }
            }

            res.@return = anzahlFahrzeuge;
            //Test Anzahl Fahrzeug auf 1 beschränkt US 29.01.2014
            //res.@return = 1;
            return res;
        }

        //US 25.02.2014
        //Suchparameter könen noch nicht ausgewertet werden, da die XML Deserialisierung mit "dataTransfer" noch nicht funktioniert
        public searchSalesVehiclesResponse searchSalesVehicles(searchSalesVehiclesRequest request)
        {
            Int32 fahrzeugid;
            string fahrzeugstatus = "";
            string auftragstatusverkauf = "";
            string strStandtage = "";
            Int32 standtage;
            string zulassungskz = "";
            string marke = "";
            string verkaufstyp = "";
            string bezeichnung = "";
            DateTime datumerstzulassung;
            string datErstzulassung = "";
            DateTime datumausgang;
            string datAusgang = "";
            Int32 kilometerstand;
            string farbe = "";
            Int32 kw;
            string strKW = "";
            string strKilometerstand = "";
            decimal vkempfohlenbrutto;
            decimal vkgeplantbrutto;
            string fahrzeugart = "";
            string strvke = "";
            string strvkg = "";

            DateTime dtDefaultDB;
            dtDefaultDB = Convert.ToDateTime("01.01.1900");
            
            datumerstzulassung = dtDefaultDB;

            searchSalesVehiclesResponse res = new searchSalesVehiclesResponse();

            interfaceData iData = new interfaceData();
            salesVehicleType sV;

            System.Collections.ArrayList aFahrzeuge = new System.Collections.ArrayList();

            salesmanType sT = new salesmanType();
            sT.identifier = "fmad";
            sT.name = "Fmade";
           
            System.Xml.Serialization.XmlSerializer xmlReader = new System.Xml.Serialization.XmlSerializer(typeof(Bungert.interfaceData));
            TextReader txtReader = new StringReader(request.Body.arg0);
            Bungert.interfaceData data = new Bungert.interfaceData();
            data = (Bungert.interfaceData)xmlReader.Deserialize(txtReader);

            //Filter wird verarbeitet
            string vinShort = "";
            string licensePlate = "";
            string matchCode = "";
            string bodywork = "";
            string brand = "";
            string color = "";
            string fueltype = "";
            Bungert.interfaceDataSearchSalesVehicleFirstRegistration[] fR;
            string fRfrom = "";
            string fRto = "";
            Bungert.interfaceDataSearchSalesVehicleMilage[] mile;
            string mileFrom = "";
            string mileTo = "";
            string model = "";
            Bungert.interfaceDataSearchSalesVehiclePrice[] price;
            string priceFrom = "";
            string priceTo = "";
            Bungert.interfaceDataSearchSalesVehiclePS[] ps;
            string psFrom = "";
            string psTo = "";
            string salesState = "";
            Bungert.interfaceDataSearchSalesVehicleStandingTime[] standingT;
            string standingTfrom = "";
            string standingTto = "";

            string stockState = "";
            Bungert.interfaceDataSearchSalesVehicleVehicleTypesVehicleType[] vehicleTypes;
            string vehicleT = "";
            string sqlWhere = "";

            //verkürzte Fahrgestellnr
            if (!String.IsNullOrWhiteSpace(vinShort = data.Items[0].vinShort))
                sqlWhere += " AND lower(a.fahrgestellnr) like '%" + vinShort.ToLower() + "%'";
            //ZulassungsKZ
            if (!String.IsNullOrWhiteSpace(licensePlate = data.Items[0].licensePlate))
                sqlWhere += " AND replace(lower(a.zulassungskz), ' ', '') like '%" + licensePlate.ToLower().Replace(" ","") + "%'";
            //Bezeichnung
            if (!String.IsNullOrWhiteSpace(matchCode = data.Items[0].matchcode))
                sqlWhere += " AND lower(a.bezeichnung) like '%" + matchCode.ToLower() + "%'";


            //Ausführung (Limousine/Kombi usw.)
            //bodywork = data.Items[0].bodywork;
            //Marke
            if(!String.IsNullOrWhiteSpace(brand = data.Items[0].brand))
                sqlWhere += " AND lower(a.marke) = '" + brand.ToLower() + "'";
            //Farbe
             if(!String.IsNullOrWhiteSpace(color = data.Items[0].color))
                sqlWhere += " AND lower(a.farbe) like '%" + color.ToLower() + "%'";

            //Datum Erstzulassung
            fR =  data.Items[0].firstRegistration;
            if (fR != null)
            {
                if(!String.IsNullOrWhiteSpace(fRfrom = fR[0].from))
                    sqlWhere += " AND a.datumerstzulassung >= '" + fRfrom.Substring(0,4) + "-" + fRfrom.Substring(4,2) + "-" + fRfrom.Substring(6,2) + "'";
                if(!String.IsNullOrWhiteSpace(fRto = fR[0].to))
                    sqlWhere += " AND a.datumerstzulassung <= '" + fRto.Substring(0, 4) + "-" + fRto.Substring(4, 2) + "-" + fRto.Substring(6, 2) + "'";
            }

            //Treibstoff
            if (!String.IsNullOrWhiteSpace(fueltype = data.Items[0].fuelType))
                sqlWhere += " AND Upper(a.treibstoff) like '" + fueltype + "%'";

            //Kilometerstand
            mile = data.Items[0].milage;
            if (mile != null)
            {
                if (!String.IsNullOrWhiteSpace(mileFrom = mile[0].from))
                    sqlWhere += " AND a.kilometerstand >= " + mileFrom;
                if (!String.IsNullOrWhiteSpace(mileTo = mile[0].to))
                    sqlWhere += " AND a.kilometerstand <= " + mileTo;
            }
           
            //Verkaufstyp
            if (!String.IsNullOrWhiteSpace(model = data.Items[0].model))
            {
                sqlWhere += " AND (trim(a.verkaufstyp) like '" + model.Trim().Replace(" ", "") + "%'";
                sqlWhere += " OR trim(a.bezeichnung) like '" + model.Trim().Replace(" ", "") + "%')";
            }

            //VK empfohlen brutto
            price = data.Items[0].price;
            if (price != null)
            {
                if (!String.IsNullOrWhiteSpace(priceFrom = price[0].from))
                    sqlWhere += " AND b.vkempfohlenbrutto >= " + priceFrom;
                if (!String.IsNullOrWhiteSpace(priceTo = price[0].to))
                    sqlWhere += " AND b.vkempfohlenbrutto <= " + priceTo;
            }
                    
            //PS (Umrechung auf KW) Faktor 1KW = 1,36PS
            ps = data.Items[0].ps;
            if (ps != null)
            {
                if (!String.IsNullOrWhiteSpace(psFrom = ps[0].from))
                    sqlWhere += " AND a.kw >= " + psFrom + "/1.36";
                if (!String.IsNullOrWhiteSpace(psTo = ps[0].to))
                    sqlWhere += " AND a.kw <= " + psTo + "/1.36";
            }
           
            //salesState = data.Items[0].salesState;

            //Standzeit
            //standingT = data.Items[0].standingTime;
            //standingTfrom = standingT[0].from;
            //standingTto = standingT[0].to;

            //stockState = data.Items[0].stockState;


            vehicleTypes = data.Items[0].vehicleTypes;

            if (vehicleTypes != null)
            {
                foreach (Bungert.interfaceDataSearchSalesVehicleVehicleTypesVehicleType vT in vehicleTypes)
                {
                    switch (vT.Value)
                    {
                        case "demo":
                            vehicleT += "'vorführwagen',";
                            break;
                        case "used":
                            vehicleT += "'gebrauchtwagen',";
                            break;
                        case "new":
                            vehicleT += "'neuwagen',";
                            break;
                        //Wird in P2 nicht verwendet
                        //case "agency":
                        //    vehicleT += "'agency'";
                        //    break;

                    }
                }
            }

            if (!String.IsNullOrWhiteSpace(vehicleT))
            {
                vehicleT = "(" + vehicleT.Substring(0, vehicleT.Length - 1) + ")";
                sqlWhere += " AND lower(b.fahrzeugart) IN " + vehicleT;
            }
            using (DBConnect myConnect = new DBConnect())
            {
                String sqlCommand;
               
                //SQL Standtage US 23.05.2014
                //if datumausgang is null then
                //    datediff(day,datumeingang,today(*))
                //    else
                //datediff(day,datumeingang,datumausgang)
                //endif as Standtage,

                sqlCommand = "SELECT b.fahrzeugid, b.fahrzeugstatus, a.marke, a.verkaufstyp, a.bezeichnung, ";
                sqlCommand += "a.kw, a.farbe, a.datumerstzulassung, a.kilometerstand, b.vkempfohlenbrutto, b.vkgeplantbrutto, b.fahrzeugart, a.zulassungskz, ";
                sqlCommand += "b.auftragstatusverkauf, if b.datumausgang is null then datediff(day,b.datumeingang,today(*)) else datediff(day,b.datumeingang,b.datumausgang) endif, b.datumausgang";
                sqlCommand += " FROM bungert.fahrzeug a, bungert.fahrzeugauftrag b";
                sqlCommand += " WHERE  a.fahrzeugid = b.fahrzeugid";
                //sqlCommand += " AND b.zulassungskz = 'B-AR 6700'";
                if(!String.IsNullOrWhiteSpace(sqlWhere))
                    sqlCommand += sqlWhere;
                sqlCommand += " ORDER BY b.fahrzeugid";
                OdbcCommand cmd = new OdbcCommand(sqlCommand, myConnect.conn);
                
                myConnect.Connect();

                OdbcDataReader reader;
                //DataReader Objekt wird initialisiert

                using (reader = cmd.ExecuteReader())
                {

                    if (reader.HasRows)
                    {

                        while (reader.Read())
                        {
                            sV = new salesVehicleType();

                            fahrzeugid = reader.GetInt32(0);
                            fahrzeugstatus = reader.GetValue(1).ToString();
                            marke = reader.GetValue(2).ToString();
                            verkaufstyp = reader.GetValue(3).ToString();
                            bezeichnung = reader.GetValue(4).ToString();
                            strKW = reader.GetValue(5).ToString();
                            farbe = reader.GetValue(6).ToString();
                            datErstzulassung = reader.GetValue(7).ToString();
                            strKilometerstand = reader.GetValue(8).ToString();
                            //US 12.02.2014 - Umsetzung: Fehler bei Nullwerten während der Konvertierung zu Decimal
                            //vkempfohlenbrutto = reader.GetDecimal(9);
                            strvke = reader.GetValue(9).ToString();
                            strvkg = reader.GetValue(10).ToString();
                            //vkgeplantbrutto = reader.GetDecimal(10);
                            fahrzeugart = reader.GetValue(11).ToString();
                            zulassungskz = reader.GetValue(12).ToString();
                            auftragstatusverkauf = reader.GetValue(13).ToString();
                            strStandtage = reader.GetValue(14).ToString();
                            datAusgang = reader.GetValue(15).ToString();

                            Decimal.TryParse(strvke, out vkempfohlenbrutto);
                            Decimal.TryParse(strvkg, out vkgeplantbrutto);                            

                            //Datum Erstzulassung
                            if (String.IsNullOrWhiteSpace(datErstzulassung))
                            {
                                datumerstzulassung = dtDefaultDB;
                            }
                            else
                            {
                                datumerstzulassung = Convert.ToDateTime(datErstzulassung);
                            }

                            //US 10.02.2014
                            //Standort muss zum Verkaeufer passen - standardmäßig "location=1"
                            sV.location = 1;

                            sV.vehicleNumber =  fahrzeugid;

                            salesVehicleTypeHeader sVTH = new salesVehicleTypeHeader();

                            sVTH.licensePlate = zulassungskz;
                            sVTH.brand = marke;
                            sVTH.modelShort = verkaufstyp;
                            sVTH.model = bezeichnung;
                            sVTH.modelLong = bezeichnung;
                           
                            if (String.IsNullOrWhiteSpace(strKW))
                            {
                                kw = 0;
                            }
                            else
                            {
                                kw = Convert.ToInt32(strKW);
                            }

                            sVTH.kw = kw;
                            sVTH.kwSpecified = true;
                                                       

                            if (String.IsNullOrWhiteSpace(strKilometerstand))
                            {
                                kilometerstand = 0;
                            }
                            else
                            {
                                kilometerstand = Convert.ToInt32(strKilometerstand);
                            }

                            sVTH.milage = kilometerstand;
                            sVTH.milageSpecified = true;

                            sVTH.listPrice = Convert.ToDouble(vkempfohlenbrutto);
                            sVTH.listPriceSpecified = true;

                            sVTH.actualSalesPrice = Convert.ToDouble(vkgeplantbrutto);
                            sVTH.actualSalesPriceSpecified = true;

                            //Datum Ausgang
                            if (String.IsNullOrWhiteSpace(datAusgang))
                            {
                                datumausgang = DateTime.MaxValue;
                            }
                            else
                            {
                                datumausgang = Convert.ToDateTime(datAusgang);
                            }

                            //Lagerstatus
                            switch (fahrzeugstatus.ToUpper())
                            {
                                case "NG":
                                    sVTH.stockState = StockState.bestellt;
                                    sVTH.salesState = SalesState.bestellt;
                                    break;
                                default: //IN, VF, VK
                                    sVTH.stockState = StockState.lagernd;
                                    break;
                            }

                            if (datumausgang <= DateTime.Today)
                                sVTH.stockState = StockState.ausgeliefert;

                            //Verkaufsstatus
                            if (fahrzeugstatus.ToUpper() == "NG")
                                sVTH.salesState = SalesState.bestellt;

                            switch (auftragstatusverkauf.ToUpper())
                            {
                                case "ER":
                                    sVTH.salesState = SalesState.reserviert;
                                    break;
                                case "FA":
                                    sVTH.salesState = SalesState.reserviert;
                                    break;
                                case "AB":
                                    sVTH.salesState = SalesState.reserviert;
                                    break;
                                case "NV":
                                    sVTH.salesState = SalesState.verfügbar;
                                    break;
                            }

                            if (String.IsNullOrWhiteSpace(strStandtage))
                            {
                                standtage = 0;
                            }
                            else
                            {
                                standtage = Convert.ToInt32(strStandtage);
                            }

                            sVTH.standingTime = standtage;

                            sV.header = sVTH;
                            
                            aFahrzeuge.Add(sV);

                        }

                    }
                    reader.Close();
                }
            }

            salesVehicleType[] aSalesVehicle = new salesVehicleType[aFahrzeuge.Count];
            //List<salesVehicleType> aSalesVehicle = new List<salesVehicleType>();
            for (int i = 0; i < aFahrzeuge.Count; i++)
            {
                salesVehicleType salesV = (salesVehicleType)aFahrzeuge[i];
                aSalesVehicle[i] = salesV;
            }

            iData.salesVehicle = aSalesVehicle;
            iData.interfaceVersion = 1;
            iData.dataProvider = "STANDARD_INTERFACE";

            iData.callingUser = sT;
            iData.transmissionReason = "";

            System.IO.StringWriter stringWriter = new System.IO.StringWriter();

            var serializer = new XmlSerializer(typeof(interfaceData));
            using (var xw = XmlWriter.Create(stringWriter, new XmlWriterSettings { Encoding = new UTF8Encoding() }))
            {
                serializer.Serialize(xw, iData);

            }

            #region xml encoding
            //string result;
            //using (MemoryStream memoryStream = new MemoryStream())
            //{
            //    XmlSerializer xs = new XmlSerializer(typeof(interfaceData));
            //    XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
            //    xs.Serialize(xmlTextWriter, iData);
            //    result = Encoding.UTF8.GetString(memoryStream.ToArray());
            //}
            //if (String.IsNullOrEmpty(result))
            //    result = "";
            #endregion

            searchSalesVehiclesResponseBody resBody = new searchSalesVehiclesResponseBody();

            resBody.@return = stringWriter.ToString();

            res.Body = resBody;

            return res;
        }


        //US 26.02.2014
        public getSalesVehicleDetailResponse getSalesVehicleDetail(getSalesVehicleDetailRequest request)
        {
            Int32 verkaueferid;
            string strVerkaeuferid = "";
            Int32 kundenkontonr;
            string strKundenkontonr = "";
            string kundensuchname = "";
            string fahrgestellnr = "";
            Int32 modelljahr;
            string strModelljahr = "";
            string innenausstattung = "";
            Int32 hubraum;
            string strHubraum = "";
            string treibstoff = "";
            string zuendschluesselnr = "";
            string tuerschluesselnr = "";
            string radiocode = "";
            string zulassungskz = "";
            DateTime datumabmeldung;
            string strDatumabmeldung = "";
            Int32 anzahlvorbesitzer;
            Int32 vorbesitzeradrid;
            string kbahsnr = "";
            string kbatsnr = "";
            DateTime datumzulassungkunde;
            string strDatumzulassungkunde = "";
            DateTime datumhu;
            string strDatumhu = "";
            DateTime datumau;
            string strDatumau = "";
            Boolean unfallfahrzeug;
            string kfzbriefnr = "";
            string kfzbriefort = "";
            DateTime datumausgang;
            string strDatumausgang = "";
            DateTime datumverkauf;
            string strDatumverkauf = "";
            Boolean differenzbesteuerung;
            Int32 einkaeufer;
            DateTime datumeingang;
            string strDatumeingang = "";
            Int32 gutschriftnr;
            string zusatztext = "";
            string aktenzeichen = "";
            decimal optionenwerkvk;
            string strOWVK;
            decimal zubehoervk;
            string strZVK;
            decimal optionenhaendlervk;
            string strOHVK;
            decimal fahrzeugek;
            string strFEK;
            decimal optionenwerkek;
            string strOWEK;
            decimal optionenhaendlerek;
            string strOHEK;
            decimal zubehoerek;
            string strZEK;
            decimal aufbereitungek;
            string strAEK;
            string motorseriennr = "";
            Int32 vehicleNumber = 0;
            Int32 auftragnr = 0;

            DateTime dtDefaultDB;
            dtDefaultDB = Convert.ToDateTime("01.01.1900");

            datumabmeldung = dtDefaultDB;
            datumzulassungkunde = dtDefaultDB;
            unfallfahrzeug = false;
            datumausgang = dtDefaultDB;
            datumverkauf = dtDefaultDB;
            differenzbesteuerung = false;

            getSalesVehicleDetailResponse res = new getSalesVehicleDetailResponse();

            interfaceData iData = new interfaceData();
            salesVehicleTypeDetailsBasis sVDB;

            System.Collections.ArrayList aFahrzeuge = new System.Collections.ArrayList();

            salesmanType sT = new salesmanType();
            sT.identifier = "fmad";
            sT.name = "Fmade";

            salesVehicleType sV = new salesVehicleType();

            sV.location = 1;

            if (String.IsNullOrWhiteSpace(request.Body.arg0))
            {
                vehicleNumber = 0;
            }
            else
            {
                vehicleNumber = Convert.ToInt32(request.Body.arg0);
            }


            using (DBConnect myConnect = new DBConnect())
            {
                String sqlCommand;

                //US 27.02.2014
                //Optionen aus der Tabelle "fahrzeugauftragoptionen" müssen noch abgerufen werden

                sqlCommand = "SELECT b.verkaeufer, b.kundenkontonr, b.kundensuchname, a.fahrgestellnr, a.modelljahr, a.innenausstattung, a.hubraum, a.treibstoff, a.zuendschluesselnr, a.tuerschluesselnr, a.radiocode,";
                sqlCommand += " a.zulassungskz, b.datumabmeldung, b.anzahlvorbesitzer, b.vorbesitzeradrid, a.kbahsnr, a.kbatsnr, a.datumzulassungkunde, a.datumhu, a.datumau, b.unfallfahrzeug, a.kfzbriefnr, b.kfzbriefort, ";
                sqlCommand += " b.datumausgang, b.datumverkauf, b.differenzbesteuerung, b.einkaeufer, b.datumeingang, b.gutschriftnr, b.zusatztext, b.aktenzeichen, b.optionenwerkvk, b.zubehoervk, b.optionenhaendlervk, ";
                sqlCommand += " b.fahrzeugek, b.optionenwerkek, b.optionenhaendlerek, b.zubehoerek, b.aufbereitungek, a.motorseriennr, b.auftragnr ";
                sqlCommand += " FROM bungert.fahrzeug a, bungert.fahrzeugauftrag b";
                sqlCommand += " WHERE  a.fahrzeugid = b.fahrzeugid";
                sqlCommand += " AND b.fahrzeugid = ";  
                sqlCommand +=  String.IsNullOrWhiteSpace(request.Body.arg0.ToString()) ? "0" : request.Body.arg0.ToString() ;
                sqlCommand += " ORDER BY b.fahrzeugid";
                OdbcCommand cmd = new OdbcCommand(sqlCommand, myConnect.conn);

                myConnect.Connect();

                OdbcDataReader reader;
                //DataReader Objekt wird initialisiert

                using (reader = cmd.ExecuteReader())
                {

                    if (reader.HasRows)
                    {

                        while (reader.Read())
                        {
                            sVDB = new salesVehicleTypeDetailsBasis();

                            strVerkaeuferid = reader.GetValue(0).ToString();
                            strKundenkontonr = reader.GetValue(1).ToString();
                            kundensuchname = reader.GetValue(2).ToString();
                            fahrgestellnr = reader.GetValue(3).ToString();
                            strModelljahr = reader.GetValue(4).ToString();
                            innenausstattung = reader.GetValue(5).ToString();
                            strHubraum = reader.GetValue(6).ToString();
                            treibstoff = reader.GetValue(7).ToString();
                            zuendschluesselnr = reader.GetValue(8).ToString();
                            tuerschluesselnr = reader.GetValue(9).ToString();
                            radiocode = reader.GetValue(10).ToString();
                            zulassungskz = reader.GetValue(11).ToString();
                            strDatumabmeldung = reader.GetValue(12).ToString();
                            anzahlvorbesitzer = reader.GetInt32(13);
                            vorbesitzeradrid = reader.GetInt32(14);
                            kbahsnr = reader.GetValue(15).ToString();
                            kbatsnr = reader.GetValue(16).ToString();
                            strDatumzulassungkunde = reader.GetValue(17).ToString();
                            strDatumhu = reader.GetValue(18).ToString();
                            strDatumau = reader.GetValue(19).ToString();
                            unfallfahrzeug = reader.GetBoolean(20);
                            kfzbriefnr = reader.GetValue(21).ToString();
                            kfzbriefort = reader.GetValue(22).ToString();
                            strDatumausgang = reader.GetValue(23).ToString();
                            strDatumverkauf = reader.GetValue(24).ToString();
                            differenzbesteuerung = reader.GetBoolean(25);
                            einkaeufer = reader.GetInt32(26);
                            strDatumeingang = reader.GetValue(27).ToString();
                            gutschriftnr = reader.GetInt32(28);
                            zusatztext = reader.GetValue(29).ToString();
                            aktenzeichen = reader.GetValue(30).ToString();
                            //US 13.03.2014 - Umsetzung: Fehler bei Nullwerten während der Konvertierung zu Decimal
                            strOWVK = reader.GetValue(31).ToString();
                            strZVK = reader.GetValue(32).ToString();
                            strOHVK = reader.GetValue(33).ToString();
                            strFEK = reader.GetValue(34).ToString();
                            strOWEK = reader.GetValue(35).ToString();
                            strOHEK = reader.GetValue(36).ToString();
                            strZEK = reader.GetValue(37).ToString();
                            strAEK = reader.GetValue(38).ToString();
                            motorseriennr = reader.GetValue(39).ToString();
                            auftragnr = reader.GetInt32(40);

                            Decimal.TryParse(strOWVK, out optionenwerkvk);
                            Decimal.TryParse(strZVK, out zubehoervk);
                            Decimal.TryParse(strOHVK, out optionenhaendlervk);
                            Decimal.TryParse(strFEK, out fahrzeugek);
                            Decimal.TryParse(strOWEK, out optionenwerkek);
                            Decimal.TryParse(strOHEK, out optionenhaendlerek);
                            Decimal.TryParse(strZEK, out zubehoerek);
                            Decimal.TryParse(strAEK, out aufbereitungek);

                            salesVehicleTypeDetailsBasisBuyer byer = new salesVehicleTypeDetailsBasisBuyer();

                            salesmanType salesman = new salesmanType();

                            if (String.IsNullOrWhiteSpace(strVerkaeuferid))
                            {
                                verkaueferid = 0;
                            }
                            else
                            {
                                verkaueferid = Convert.ToInt32(strVerkaeuferid);
                            }

                            if (String.IsNullOrWhiteSpace(strKundenkontonr))
                            {
                                kundenkontonr = 0;
                            }
                            else
                            {
                                kundenkontonr = Convert.ToInt32(strKundenkontonr);
                            }

                            salesman = getSalesman(verkaueferid, myConnect);

                            salesVehicleTypeDetailsBasisSalesman sVTDBS = new salesVehicleTypeDetailsBasisSalesman();
                            sVTDBS.id = verkaueferid.ToString();
                            sVTDBS.name = salesman.name;
                            
                            sVDB.salesman = sVTDBS;

                            byer.id = kundenkontonr;
                            byer.idSpecified = true;
                            byer.name = kundensuchname;

                            sVDB.buyer = byer;
                            sVDB.vin = fahrgestellnr;

                            sVDB.yearOfModel = strModelljahr;

                            sVDB.interiorColor = innenausstattung;

                            if (String.IsNullOrWhiteSpace(strHubraum))
                            {
                                hubraum = 0;
                            }
                            else
                            {
                                hubraum = Convert.ToInt32(strHubraum);
                            }

                            sVDB.cubicCapacity = hubraum;
                            sVDB.cubicCapacitySpecified = true;
                            sVDB.fuelType = treibstoff;
                            sVDB.engineNumber = motorseriennr;
                            sVDB.keyNumber1 = zuendschluesselnr;
                            sVDB.keyNumber2 = tuerschluesselnr;
                            sVDB.radioCode = radiocode;
                            sVDB.licensePlate = zulassungskz;

                            //Datum Abmeldung
                            if (String.IsNullOrWhiteSpace(strDatumabmeldung))
                            {
                                datumabmeldung = dtDefaultDB;
                            }
                            else
                            {
                                datumabmeldung = Convert.ToDateTime(strDatumabmeldung);
                            }

                            sVDB.deRegistrationDate = datumabmeldung;
                            sVDB.numberPreOwners = anzahlvorbesitzer;
                            //TODO US 18.03.2014
                            //sVDB.namePrevOwner = vorbesitzeradrid;

                            sVDB.kbaHsn = kbahsnr;
                            sVDB.kbaTsn = kbatsnr;

                            //Datum Zulassung Kunde
                            if (String.IsNullOrWhiteSpace(strDatumzulassungkunde))
                            {
                                datumzulassungkunde = dtDefaultDB;
                            }
                            else
                            {
                                datumzulassungkunde = Convert.ToDateTime(strDatumzulassungkunde);
                            }

                            sVDB.lastRegistrationDate = datumzulassungkunde;
                            sVDB.lastRegistrationDateSpecified = true;

                            //Datum HU
                            if (String.IsNullOrWhiteSpace(strDatumhu))
                            {
                                datumhu = dtDefaultDB;
                            }
                            else
                            {
                                datumhu = Convert.ToDateTime(strDatumhu);
                            }

                            sVDB.testDate1 = datumhu;
                            sVDB.testDate1Specified = true;

                            //Datum AU
                            if (String.IsNullOrWhiteSpace(strDatumau))
                            {
                                datumau = dtDefaultDB;
                            }
                            else
                            {
                                datumau = Convert.ToDateTime(strDatumau);
                            }

                            sVDB.testDate2 = datumau;
                            sVDB.testDate2Specified = true;
                            sVDB.wrack = unfallfahrzeug;
                            sVDB.wrackSpecified = true;
                            sVDB.carLetter = kfzbriefnr;
                            sVDB.carLetterLocation = kfzbriefort;

                            //Datum Ausgang
                            if (String.IsNullOrWhiteSpace(strDatumausgang))
                            {
                                datumausgang = dtDefaultDB;
                            }
                            else
                            {
                                datumausgang = Convert.ToDateTime(strDatumausgang);
                            }

                            sVDB.deliveryDate = datumausgang;
                            sVDB.deliveryDateSpecified = true;

                            //Datum Verkauf
                            if (String.IsNullOrWhiteSpace(strDatumverkauf))
                            {
                                datumverkauf = dtDefaultDB;
                            }
                            else
                            {
                                datumverkauf = Convert.ToDateTime(strDatumverkauf);
                            }

                            sVDB.salesContractDate = datumverkauf;
                            sVDB.salesContractDateSpecified = true;

                            //Datum Eingang
                            if (String.IsNullOrWhiteSpace(strDatumeingang))
                            {
                                datumeingang = dtDefaultDB;
                            }
                            else
                            {
                                datumeingang = Convert.ToDateTime(strDatumeingang);
                            }


                            salesVehicleTypeDetailsBasisIncomeOfVehicle incomeV = new salesVehicleTypeDetailsBasisIncomeOfVehicle();
                            incomeV.vat = differenzbesteuerung;
                            incomeV.date = datumeingang;
                            incomeV.dateSpecified = true;
                            incomeV.invoiceNumber = gutschriftnr.ToString();
                            //TODO US 18.03.2014
                            //Salesman

                            sVDB.incomeOfVehicle = incomeV;
                            sVDB.remarks = zusatztext;
                            sVDB.matchcode = aktenzeichen;

                            salesVehicleTypeDetails details = new salesVehicleTypeDetails();
                            details.basis = sVDB;

                            salesVehicleTypeDetailsPrices prices = new salesVehicleTypeDetailsPrices();
                            prices.optionsSpecified = (prices.options = Convert.ToDouble(optionenwerkvk)) > 0;
                            prices.equipmentSpecified = (prices.equipment = Convert.ToDouble(zubehoervk)) > 0;
                            prices.othersSpecified = (prices.others = Convert.ToDouble(optionenhaendlervk)) > 0;

                            salesVehicleTypeDetailsPricesPurchase purchase = new salesVehicleTypeDetailsPricesPurchase();

                            purchase.vehicleSpecified = (purchase.vehicle = Convert.ToDouble(fahrzeugek)) > 0;
                            purchase.optionsSpecified = (purchase.options = Convert.ToDouble(optionenwerkek)) > 0;
                            purchase.equipmentSpecified = (purchase.equipment = Convert.ToDouble(optionenhaendlerek)) > 0;
                            purchase.othersSpecified = (purchase.others = Convert.ToDouble(zubehoerek)) > 0;

                            prices.purchase = purchase;

                            salesVehicleTypeDetailsPricesEfforts efforts = new salesVehicleTypeDetailsPricesEfforts();
                            efforts.internEffort1Specified = (efforts.internEffort1 = Convert.ToDouble(aufbereitungek)) > 0;

                            prices.efforts = efforts;

                            details.prices = prices;
                            details.options = getOptions(auftragnr, myConnect);
                            sV.details = details;

                            aFahrzeuge.Add(sV);
                        }

                    }
                    reader.Close();
                }
            }

            salesVehicleType[] aSalesVehicle = new salesVehicleType[aFahrzeuge.Count];
            //List<salesVehicleType> aSalesVehicle = new List<salesVehicleType>();
            for (int i = 0; i < aFahrzeuge.Count; i++)
            {
                salesVehicleType salesV = (salesVehicleType)aFahrzeuge[i];
                aSalesVehicle[i] = salesV;
            }

            iData.salesVehicle = aSalesVehicle;
            iData.interfaceVersion = 1;
            iData.dataProvider = "STANDARD_INTERFACE";

            iData.callingUser = sT;
            iData.transmissionReason = "";

            System.IO.StringWriter stringWriter = new System.IO.StringWriter();

            var serializer = new XmlSerializer(typeof(interfaceData));
            using (var xw = XmlWriter.Create(stringWriter, new XmlWriterSettings { Encoding = new UTF8Encoding() }))
            {
                serializer.Serialize(xw, iData);

            }

            #region xml encoding
            //string result;
            //using (MemoryStream memoryStream = new MemoryStream())
            //{
            //    XmlSerializer xs = new XmlSerializer(typeof(interfaceData));
            //    XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
            //    xs.Serialize(xmlTextWriter, iData);
            //    result = Encoding.UTF8.GetString(memoryStream.ToArray());
            //}
            //if (String.IsNullOrEmpty(result))
            //    result = "";
            #endregion

            getSalesVehicleDetailResponseBody resBody = new getSalesVehicleDetailResponseBody();

            resBody.@return = stringWriter.ToString();

            res.Body = resBody;

            return res;
        }

        //US 19.03.2014 - Suchname auf Tabelle "mitarbeiter" 
        public salesmanType getSalesman(Int32 aID, DBConnect aConnect)
        {
            String strSuchname = "";
            String strKuerzel = "";
            String strSQL;
            OdbcDataReader reader;
            salesmanType sT = new salesmanType();
            
            strSQL = "SELECT suchname, kuerzel FROM bungert.mitarbeiter WHERE mitarbeiter_id = " + aID.ToString();
           
            OdbcCommand cmd = new OdbcCommand(strSQL, aConnect.conn);
          
            using (reader = cmd.ExecuteReader())
            {
                
                if (reader.HasRows)
                {
                    strSuchname = reader.GetValue(0).ToString();
                    strKuerzel = reader.GetValue(1).ToString();
                    sT.identifier = strKuerzel != null ? strKuerzel : ""; 
                    sT.name = strSuchname != null ? strSuchname : ""; 
                }
                    
            }

            return sT;
        }

        //US 21.03.2014 - Fahrzeugauftragoptionen
        public Option[] getOptions(Int32 aNr, DBConnect aConnect)
        {
            String strNummer = "";
            String strBezeichnung = "";
            String strSQL;
            OdbcDataReader reader;
            Option opt;

            strSQL = "SELECT nummer, bezeichnung FROM bungert.fahrzeugauftragoption WHERE (trim(bezeichnung) <> '' and bezeichnung is not null) and auftragnr = " + aNr.ToString();

            OdbcCommand cmd = new OdbcCommand(strSQL, aConnect.conn);

            System.Collections.ArrayList aOptionen = new System.Collections.ArrayList();

            using (reader = cmd.ExecuteReader())
            {

                if (reader.HasRows)
                {

                    while (reader.Read())
                    {
                        opt = new Option();
                        strNummer = reader.GetValue(0).ToString();
                        strBezeichnung = reader.GetValue(1).ToString();
                        opt.code = strNummer != null ? strNummer : "";
                        opt.text = strBezeichnung != null ? strBezeichnung : "";
                        aOptionen.Add(opt);
                    }
                }

            }

            Option[] aOptions = new Option[aOptionen.Count];
            for (int i = 0; i < aOptionen.Count; i++)
            {
                Option option = (Option)aOptionen[i];
                aOptions[i] = option;
            }

            return aOptions;
        }

        //US 21.03.2014 - Fahrzeugauftragoptionen
        //public List<Option> getOptions(Int32 aNr, DBConnect aConnect)
        //{
        //    String strNummer = "";
        //    String strBezeichnung = "";
        //    String strSQL;
        //    OdbcDataReader reader;
        //    Option opt;

        //    strSQL = "SELECT nummer, bezeichnung FROM bungert.fahrzeugauftragoption WHERE auftragnr = " + aNr.ToString();

        //    OdbcCommand cmd = new OdbcCommand(strSQL, aConnect.conn);

        //    System.Collections.ArrayList aOptionen = new System.Collections.ArrayList();

        //    using (reader = cmd.ExecuteReader())
        //    {

        //        if (reader.HasRows)
        //        {

        //            while (reader.Read())
        //            {
        //                opt = new Option();
        //                strNummer = reader.GetValue(0).ToString();
        //                strBezeichnung = reader.GetValue(1).ToString();
        //                opt.code = strNummer != null ? strNummer : "";
        //                opt.text = strBezeichnung != null ? strBezeichnung : "";
        //                aOptionen.Add(opt);
        //            }
        //        }

        //    }

        //    List<Option> aOptions = new List<Option>();
        //    for (int i = 0; i < aOptionen.Count; i++)
        //    {
        //        Option option = (Option)aOptionen[i];
        //        aOptions[i] = option;
        //    }

        //    return aOptions;
        //}

        //US 25.03.2014
        //Auftrag: Rechnung Extern/Intern/Garantie - Fahrzeugauftrag: Rechnung
        public getAllInvoiceCountResponse getAllInvoiceCount(getAllInvoiceCountRequest request)
        {
            getAllInvoiceCountResponse res = new getAllInvoiceCountResponse();
            Int32 anzahlRechnungen;
            anzahlRechnungen = 0;

            using (DBConnect myConnect = new DBConnect())
            {
                String sqlCommand;
                sqlCommand = "SELECT count(rechnungsnr)";
                sqlCommand += " FROM bungert.auftrag";
                sqlCommand += " WHERE rechnungsnr is not null and rechnungsnr <> 0";
                //Test Übergabe 03.04.2014
                OdbcCommand cmd = new OdbcCommand(sqlCommand, myConnect.conn);

                myConnect.Connect();

                OdbcDataReader reader;
                //DataReader Objekt wird initialisiert

                using (reader = cmd.ExecuteReader())
                {

                    if (reader.HasRows)
                    {
                        anzahlRechnungen= reader.GetInt32(0);
                    }

                    reader.Close();
                }
            }

            res.@return = anzahlRechnungen;
            //res.@return = 100;
            return res;
        }

        public getAllInvoicesResponse getAllInvoices(getAllInvoicesRequest request)
        {
            Int32 rechnungsNr;
            Int32 kontoNr;
            string strKontoNr = "";
            string kundenName1 = "";
            string kundenName2 = "";
            string kundenName3 = "";
            string kundenVorname = "";
            string kundenStrasse = "";
            string kundenPLZ = "";
            string kundenOrt = "";
            string kundenLand = "";
            Int32 rechnungsAdrID;
            string strRechnungsAdrID = "";
            string rechnungName1 = "";
            string rechnungName2 = "";
            string rechnungName3 = "";
            string rechnungVorname = "";
            string rechnungStrasse = "";
            string rechnungPLZ = "";
            string rechnungOrt = "";
            string rechnungLand = "";
            Int32 fahrzeugID;
            string fahrgestellNr = "";
            string marke = ""; 
            string bezeichnung = "";
            string zulassungsKZ = "";
            Int32 kilometerstand;
            string strKilometerstand = "";
            string motorserienNr = "";
            DateTime rechnungsDatum;
            string strRechnungsDatum = "";
            string strRechnungsNr = "";
            DateTime datumAuftrag;
            string strDatumAuftrag = "";
            string auftragNr = "";
            Int32 annehmer;
            string strAnnehmer = "";
            decimal sumVkGesamt;
            string strSumVkGesamt = "";
            decimal sumMwst;
            string strSumMwst = "";
            decimal posAnzahl;
            string strPosAnzahl = "";
            decimal posVkGesamt;
            string strPosVkGesamt = "";
            decimal posRabattBetrag;
            string strPosRabattBetrag = "";
            Int32 mwstCode;
            string kundenAnrede = "";
            string rechnungAnrede = "";
            string lastOrderNumber = "";

            DateTime dtDefaultDB;
            dtDefaultDB = Convert.ToDateTime("01.01.1900");

            rechnungsDatum = dtDefaultDB;
            datumAuftrag = dtDefaultDB;
           
            getAllInvoicesResponse res = new getAllInvoicesResponse();

            interfaceData iData = new interfaceData();

            invoiceType iT;
            invoiceTypeHeader iTH;
            invoiceCustomerTypeName[] aICTN;
            //List<invoiceCustomerTypeName> aICTN;
            invoiceCustomerTypeAddress iCTA;
            invoiceTypeHeaderVehicle iTHV;
            customerIdentificationType cIT;

            System.Collections.ArrayList aRechnungen = new System.Collections.ArrayList();

            salesmanType sT = new salesmanType();
            sT.identifier = "fmad";
            sT.name = "Fmade";

           
            using (DBConnect myConnect = new DBConnect())
            {

                String sqlCommand;
                //Limitierung auf 100 Auftragdatensätzen pro Abruf
                sqlCommand = "SELECT TOP 100 START AT " + request.Body.arg0.ToString() + " rechnungsnr, kontonr, kundenname1, kundenname2, kundenname3, kundenvorname, kundenstrasse, kundenplz, kundenort, kundenland, ";
                sqlCommand += " rechnungadrid, rechnungname1, rechnungname2, rechnungname3, rechnungvorname, rechnungstrasse, rechnungplz, rechnungort, rechnungland,";
                sqlCommand += " a.fahrzeugid, c.fahrgestellnr, c.marke, c.bezeichnung, c.zulassungskz, c.kilometerstand, c.motorseriennr, rechnungsdatum, rechnungsnr, datumauftrag, auftragnr, annehmer, sumvkgesamt, ";
                sqlCommand += " ifnull(summwst1, 0, summwst1) + ifnull(summwst2,0,summwst2) + ifnull(summwstat, 0, summwstat)  , kundenanrede, rechnunganrede ";
                //sqlCommand += " FROM bungert.auftrag a, bungert.auftragposition b, bungert.fahrzeug c";
                //sqlCommand += " WHERE a.auftragnr = b.auftragnr AND a.fahrzeugid = c.fahrzeugid";
                sqlCommand += " FROM bungert.auftrag a, bungert.fahrzeug c";
                sqlCommand += " WHERE a.fahrzeugid = c.fahrzeugid and a.rechnungsnr is not null and a.rechnungsnr <> 0";
                //Test Übergabe 03.04.2014
                //sqlCommand += " AND c.zulassungskz = 'B-AR 6700'";
                sqlCommand += " ORDER BY a.auftragnr";
                OdbcCommand cmd = new OdbcCommand(sqlCommand, myConnect.conn);

                myConnect.Connect();

                OdbcDataReader reader;
                //DataReader Objekt wird initialisiert

                using (reader = cmd.ExecuteReader())
                {

                    if (reader.HasRows)
                    {

                        while (reader.Read())
                        {
                            iT = new invoiceType();
                            iTH = new invoiceTypeHeader();

                            rechnungsNr = reader.GetInt32(0);
                            strKontoNr = reader.GetValue(1).ToString();
                            kundenName1 = reader.GetValue(2).ToString();
                            kundenName2 = reader.GetValue(3).ToString();
                            kundenName3 = reader.GetValue(4).ToString();
                            kundenVorname = reader.GetValue(5).ToString();
                            kundenStrasse = reader.GetValue(6).ToString();
                            kundenPLZ = reader.GetValue(7).ToString();
                            kundenOrt = reader.GetValue(8).ToString();
                            kundenLand = reader.GetValue(9).ToString();
                            strRechnungsAdrID = reader.GetValue(10).ToString();
                            rechnungName1 = reader.GetValue(11).ToString();
                            rechnungName2 = reader.GetValue(12).ToString();
                            rechnungName3 = reader.GetValue(13).ToString();
                            rechnungVorname = reader.GetValue(14).ToString();
                            rechnungStrasse = reader.GetValue(15).ToString();
                            rechnungPLZ = reader.GetValue(16).ToString();
                            rechnungOrt = reader.GetValue(17).ToString();
                            rechnungLand = reader.GetValue(18).ToString();
                            fahrzeugID = reader.GetInt32(19);
                            fahrgestellNr = reader.GetValue(20).ToString();
                            marke = reader.GetValue(21).ToString();
                            bezeichnung = reader.GetValue(22).ToString();
                            zulassungsKZ = reader.GetValue(23).ToString();
                            strKilometerstand = reader.GetValue(24).ToString();
                            motorserienNr = reader.GetValue(25).ToString();
                            strRechnungsDatum = reader.GetValue(26).ToString();
                            strDatumAuftrag = reader.GetValue(28).ToString();
                            auftragNr = reader.GetValue(29).ToString();
                            strAnnehmer = reader.GetValue(30).ToString();
                            strSumVkGesamt = reader.GetValue(31).ToString();
                            strSumMwst = reader.GetValue(32).ToString();
                            kundenAnrede = reader.GetValue(33).ToString();
                            rechnungAnrede = reader.GetValue(34).ToString();

                            Decimal.TryParse(strSumVkGesamt, out sumVkGesamt);
                            Decimal.TryParse(strSumMwst, out sumMwst);
                            Decimal.TryParse(strPosAnzahl, out posAnzahl);
                            Decimal.TryParse(strPosRabattBetrag, out posRabattBetrag);
                            Decimal.TryParse(strPosVkGesamt, out posVkGesamt);

                            iT.invoiceID = rechnungsNr;

                            iTH.location = 1;
                            iTH.kindOfInvoice = invoiceTypeHeaderKindOfInvoice.workshop;

                            //Kunde
                            //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
                            invoiceCustomerType iCT = new invoiceCustomerType();

                            // Salutatuon 1 = Herr / 2 = Frau
                            if (kundenAnrede.ToLower().Contains("herr"))
                            {
                                iCT.salutation = 1;
                            }
                            else if (kundenAnrede.ToLower().Contains("frau"))
                            {
                                iCT.salutation = 2;
                            }
                            else if (kundenAnrede.ToLower().Contains("firma"))
                            {
                                iCT.salutation = 9;
                            }

                            aICTN = new invoiceCustomerTypeName[4];
                            invoiceCustomerTypeName iCTN;

                            if (iCT.salutation == 1 || iCT.salutation == 2)
                            {
                                kundenName1 = kundenVorname + " " + kundenName1;
                                kundenVorname = "";
                            }

                            iCTN = new invoiceCustomerTypeName();
                            iCTN.name = kundenName1;
                            iCTN.sequence = 1;
                            aICTN[0] = iCTN;

                            iCTN = new invoiceCustomerTypeName();
                            iCTN.name = kundenName2;
                            iCTN.sequence = 2;
                            aICTN[1] = iCTN;

                            iCTN = new invoiceCustomerTypeName();
                            iCTN.name = kundenName3;
                            iCTN.sequence = 3;
                            aICTN[2] = iCTN;

                            iCTN = new invoiceCustomerTypeName();
                            iCTN.name = kundenVorname;
                            iCTN.sequence = 4;
                            aICTN[3] = iCTN;

                            iCTA = new invoiceCustomerTypeAddress();

                            iCTA.city = kundenOrt;
                            iCTA.country = kundenLand;
                            iCTA.street = kundenStrasse;
                            iCTA.zip = kundenPLZ;
                            
                            iCT.address = iCTA;

                            //KontoNr
                            if (String.IsNullOrWhiteSpace(strKontoNr))
                            {
                                kontoNr = 0;
                            }
                            else
                            {
                                kontoNr = Convert.ToInt32(strKontoNr);
                            }

                            //RechnungsAdrID
                            if (String.IsNullOrWhiteSpace(strRechnungsAdrID))
                            {
                                rechnungsAdrID = 0;
                            }
                            else
                            {
                                rechnungsAdrID = Convert.ToInt32(strRechnungsAdrID);
                            }

                            cIT = new customerIdentificationType();
                            cIT.location = 1;
                            cIT.number = kontoNr;

                            iCT.customerNumber = cIT;
                            
                            iCT.name = aICTN;
                                                        
                            iTH.customer = iCT;
                            //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<


                            //Abweichender Rechnungsempfänger
                            //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

                            iCT = new invoiceCustomerType();

                            // Salutatuon 1 = Herr / 2 = Frau
                            if (rechnungAnrede.ToLower().Contains("herr"))
                            {
                                iCT.salutation = 1;
                            }
                            else if (rechnungAnrede.ToLower().Contains("frau"))
                            {
                                iCT.salutation = 2;
                            }
                            else if (rechnungAnrede.ToLower().Contains("firma"))
                            {
                                iCT.salutation = 9;
                            }

                            aICTN = new invoiceCustomerTypeName[4];
                            //aICTN = new List<invoiceCustomerTypeName>();
                            
                            if (iCT.salutation == 1 || iCT.salutation == 2)
                            {
                                rechnungName1 = rechnungVorname + " " + rechnungName1;
                                rechnungVorname = "";
                            }

                            iCTN = new invoiceCustomerTypeName();
                            iCTN.name = rechnungName1;
                            iCTN.sequence = 1;
                            aICTN[0] = iCTN;

                            iCTN = new invoiceCustomerTypeName();
                            iCTN.name = rechnungName2;
                            iCTN.sequence = 2;
                            aICTN[1] = iCTN;

                            iCTN = new invoiceCustomerTypeName();
                            iCTN.name = rechnungName3;
                            iCTN.sequence = 3;
                            aICTN[2] = iCTN;

                            iCTN = new invoiceCustomerTypeName();
                            iCTN.name = rechnungVorname;
                            iCTN.sequence = 4;
                            aICTN[3] = iCTN;

                            iCTA = new invoiceCustomerTypeAddress();

                            iCTA.city = rechnungOrt;
                            iCTA.country = rechnungLand;
                            iCTA.street = rechnungStrasse;
                            iCTA.zip = rechnungPLZ;
                            
                            iCT.address = iCTA;

                            //RechnungsAdrID
                            if (String.IsNullOrWhiteSpace(strRechnungsAdrID))
                            {
                                rechnungsAdrID = 0;
                            }
                            else
                            {
                                rechnungsAdrID = Convert.ToInt32(strRechnungsAdrID);
                            }

                            
                            cIT = new customerIdentificationType();
                            cIT.location = 1;
                            cIT.number = getKontNr(rechnungsAdrID, myConnect);

                            iCT.customerNumber = cIT;
                            
                            iCT.name = aICTN;

                            iTH.differentInvoiceReceiver = iCT;
                            //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

                            iTHV = new invoiceTypeHeaderVehicle();

                            iTHV.brand = marke;
                            iTHV.licensePlate = zulassungsKZ;
                           

                            //Kilometerstand
                            if (String.IsNullOrWhiteSpace(strKilometerstand))
                            {
                                kilometerstand = 0;
                            }
                            else
                            {
                                kilometerstand = Convert.ToInt32(strKilometerstand);
                            }
                            
                            iTHV.milage = kilometerstand;
                            iTHV.motorNumber = motorserienNr;
                            iTHV.type = bezeichnung;
                            iTHV.vehicleNumber = fahrzeugID;
                            //iTHV.vehicleType = 
                            iTHV.vin = fahrgestellNr;

                            iTH.vehicle = iTHV;

                            //Rechnungsdatum
                            if (String.IsNullOrWhiteSpace(strRechnungsDatum))
                            {
                                rechnungsDatum = dtDefaultDB;
                            }
                            else
                            {
                                rechnungsDatum = Convert.ToDateTime(strRechnungsDatum);
                            }

                            iTH.invoiceDate = rechnungsDatum;
                            iTH.invoiceNumber = rechnungsNr.ToString();
                            //iTH.kindOfInvoice = 

                            //Datum Auftrag
                            if (String.IsNullOrWhiteSpace(strDatumAuftrag))
                            {
                                datumAuftrag = dtDefaultDB;
                            }
                            else
                            {
                                datumAuftrag = Convert.ToDateTime(strDatumAuftrag);
                            }

                            iTH.orderDate = datumAuftrag;
                            iTH.orderNumber = auftragNr.ToString();

                            //Annehmer
                            if (String.IsNullOrWhiteSpace(strAnnehmer))
                            {
                                annehmer = 0;
                            }
                            else
                            {
                                annehmer = Convert.ToInt32(strAnnehmer);
                            }

                            iTH.acceptor = getSalesman(annehmer, myConnect);

                            iTH.sumGross = Convert.ToDouble(sumVkGesamt);
                            iTH.sumNet = Convert.ToDouble(sumVkGesamt - sumMwst);

                            //iTH.vat = mw

                            iT.positions = getPostionen(auftragNr, myConnect);

                            iT.header = iTH;

                            //salesVehicleTypeDetailsPricesEfforts efforts = new salesVehicleTypeDetailsPricesEfforts();
                            //efforts.internEffort1Specified = (efforts.internEffort1 = Convert.ToDouble(aufbereitungek)) > 0;

                            if(!String.IsNullOrWhiteSpace(auftragNr)) 
                                aRechnungen.Add(iT);

                        }

                    }
                    reader.Close();
                }
            }


            invoiceType[] aInvoice = new invoiceType[aRechnungen.Count];
            //List<invoiceType> aInvoice = new List<invoiceType>();
            for (int i = 0; i < aRechnungen.Count; i++)
            {
                invoiceType iType = (invoiceType)aRechnungen[i];
                lastOrderNumber = iType.header.orderNumber;
                aInvoice[i] = iType;
            }
            

            iData.invoice = aInvoice;
            iData.interfaceVersion = 1;
            iData.dataProvider = "STANDARD_INTERFACE";

            iData.callingUser = sT;
            iData.transmissionReason = "";

            System.IO.StringWriter stringWriter = new System.IO.StringWriter();

            var serializer = new XmlSerializer(typeof(interfaceData));
            using (var xw = XmlWriter.Create(stringWriter, new XmlWriterSettings { Encoding = new UTF8Encoding() }))
            {
                serializer.Serialize(xw, iData);
            }


            SmitConfig sc = new SmitConfig();

            if (!String.IsNullOrWhiteSpace(lastOrderNumber))
            {
                sc = ReadXML();
                sc.lastOrderNumber = lastOrderNumber;

                WriteXML(sc);
            }
            

            getAllInvoicesResponseBody resBody = new getAllInvoicesResponseBody();

            resBody.@return = stringWriter.ToString();

            res.Body = resBody;

            return res;
        }

        invoiceTypePosition[] getPostionen(string aNr, DBConnect aConnect)
        {
            string strSortierfeld = "";
            Int32 sortierFeld;
            string strNummer = "";
            string strBezeichnung = "";
            string strAnzahl = "";
            double anzahl;
            string strVKGesamt = "";
            double vkgesamt;
            string strRabattbetrag = "";
            double rabattbetrag;
            string strMwstSatz = "";
            double mwstSatz;
            string strZeit = "";
            double zeit;
            String strSQL;
            OdbcDataReader reader;
            invoiceTypePosition iTP;

            strSQL = "SELECT sortierfeld * 1000, nummer, bezeichnung, anzahl, vkgesamt, rabattbetrag, a.mwstcode, b.mwstsatz, a.zeit ";
            strSQL += "FROM bungert.auftragposition a, bungert.mehrwertsteuer b WHERE ifnull(a.mwstcode, 0, a.mwstcode) = b.mwstcode AND a.art in ('PA', 'OP') AND auftragnr = " + aNr;

            OdbcCommand cmd = new OdbcCommand(strSQL, aConnect.conn);

            System.Collections.ArrayList aPositionen = new System.Collections.ArrayList();

            using (reader = cmd.ExecuteReader())
            {

                if (reader.HasRows)
                {

                    while (reader.Read())
                    {
                        iTP = new invoiceTypePosition();

                        sortierFeld = reader.GetInt32(0);
                        strNummer = reader.GetValue(1).ToString();
                        strBezeichnung = reader.GetValue(2).ToString();
                        strAnzahl = reader.GetValue(3).ToString();
                        strVKGesamt = reader.GetValue(4).ToString();
                        strRabattbetrag = reader.GetValue(5).ToString();
                        strMwstSatz = reader.GetValue(7).ToString();
                        strZeit = reader.GetValue(8).ToString();

                        Double.TryParse(strAnzahl, out anzahl);
                        Double.TryParse(strVKGesamt, out vkgesamt);
                        Double.TryParse(strRabattbetrag, out rabattbetrag);
                        Double.TryParse(strMwstSatz, out mwstSatz);
                        Double.TryParse(strZeit, out zeit);

                        if (zeit != null && zeit > 0)
                            anzahl = anzahl * zeit;

                        //opt.code = strNummer != null ? strNummer : "";
                        //opt.text = strBezeichnung != null ? strBezeichnung : "";

                        iTP.positionNumber = sortierFeld;
                        iTP.number = strNummer;
                        iTP.title = strBezeichnung;
                        iTP.amount = anzahl;
                        iTP.sumBeforeDiscount = vkgesamt - rabattbetrag;
                        iTP.discount = rabattbetrag;
                        iTP.sumAfterDiscount = vkgesamt;
                        iTP.vatRate = mwstSatz;

                        aPositionen.Add(iTP);
                    }
                }

            }


            invoiceTypePosition[] aPos = new invoiceTypePosition[aPositionen.Count];
            for (int i = 0; i < aPositionen.Count; i++)
            {
                invoiceTypePosition position = (invoiceTypePosition)aPositionen[i];
                aPos[i] = position;
            }

            return aPos;
        }

        //List<invoiceTypePosition> getPostionen(string aNr, DBConnect aConnect)
        //{
        //    string strSortierfeld = "";
        //    Int32 sortierFeld;
        //    string strNummer = "";
        //    string strBezeichnung = "";
        //    string strAnzahl = "";
        //    double anzahl;
        //    string strVKGesamt = "";
        //    double vkgesamt;
        //    string strRabattbetrag = "";
        //    double rabattbetrag;
        //    string strMwstSatz = "";
        //    double mwstSatz;
        //    string strZeit = "";
        //    double zeit;
        //    String strSQL;
        //    OdbcDataReader reader;
        //    invoiceTypePosition iTP;

        //    strSQL = "SELECT sortierfeld * 1000, nummer, bezeichnung, anzahl, vkgesamt, rabattbetrag, a.mwstcode, b.mwstsatz, a.zeit ";
        //    strSQL += "FROM bungert.auftragposition a, bungert.mehrwertsteuer b WHERE ifnull(a.mwstcode, 0, a.mwstcode) = b.mwstcode AND a.art in ('PA', 'OP') AND auftragnr = " + aNr;

        //    OdbcCommand cmd = new OdbcCommand(strSQL, aConnect.conn);

        //    System.Collections.ArrayList aPositionen = new System.Collections.ArrayList();

        //    using (reader = cmd.ExecuteReader())
        //    {

        //        if (reader.HasRows)
        //        {

        //            while (reader.Read())
        //            {
        //                iTP = new invoiceTypePosition();

        //                sortierFeld = reader.GetInt32(0);
        //                strNummer = reader.GetValue(1).ToString();
        //                strBezeichnung = reader.GetValue(2).ToString();
        //                strAnzahl = reader.GetValue(3).ToString();
        //                strVKGesamt = reader.GetValue(4).ToString();
        //                strRabattbetrag = reader.GetValue(5).ToString();
        //                strMwstSatz = reader.GetValue(7).ToString();
        //                strZeit = reader.GetValue(8).ToString();

        //                Double.TryParse(strAnzahl, out anzahl);
        //                Double.TryParse(strVKGesamt, out vkgesamt);
        //                Double.TryParse(strRabattbetrag, out rabattbetrag);
        //                Double.TryParse(strMwstSatz, out mwstSatz);
        //                Double.TryParse(strZeit, out zeit);

        //                if (zeit != null && zeit > 0)
        //                    anzahl = anzahl * zeit;

        //                //opt.code = strNummer != null ? strNummer : "";
        //                //opt.text = strBezeichnung != null ? strBezeichnung : "";

        //                iTP.positionNumber = sortierFeld;
        //                iTP.number = strNummer;
        //                iTP.title = strBezeichnung;
        //                iTP.amount = anzahl;
        //                iTP.sumBeforeDiscount = vkgesamt - rabattbetrag;
        //                iTP.discount = rabattbetrag;
        //                iTP.sumAfterDiscount = vkgesamt;
        //                iTP.vatRate = mwstSatz;

        //                aPositionen.Add(iTP);
        //            }
        //        }

        //    }


        //    List<invoiceTypePosition> aPos = new List<invoiceTypePosition>();
        //    for (int i = 0; i < aPositionen.Count; i++)
        //    {
        //        invoiceTypePosition position = (invoiceTypePosition)aPositionen[i];
        //        aPos[i] = position;
        //    }

        //    return aPos;
        //}

        //US 07.04.2014 - KontoNr aus Tabelle "adresse" 
        public Int32 getKontNr(Int32 aID, DBConnect aConnect)
        {
            Int32 kontoNr = 0;
            String strSQL;
            OdbcDataReader reader;
            salesmanType sT = new salesmanType();

            strSQL = "SELECT kontonr FROM bungert.adresse WHERE adressid = " + aID.ToString();

            OdbcCommand cmd = new OdbcCommand(strSQL, aConnect.conn);

            using (reader = cmd.ExecuteReader())
            {

                if (reader.HasRows)
                {
                    kontoNr = reader.GetInt32(0);
                }

            }

            if (kontoNr == null)
                kontoNr = 0;

            return kontoNr;
        }

        //US 13.05.2014 - AdressID aus Tabelle "adresse" 
        public Int32 getAdressID(Int32 aKontoNr, DBConnect aConnect)
        {
            Int32 adressID = 0;
            String strSQL;
            OdbcDataReader reader;
            
            strSQL = "SELECT MAX(adressid) FROM bungert.adresse WHERE kontonr = " + aKontoNr.ToString();

            OdbcCommand cmd = new OdbcCommand(strSQL, aConnect.conn);

            using (reader = cmd.ExecuteReader())
            {

                if (reader.HasRows)
                {
                    adressID = reader.GetInt32(0);
                }

            }

            if (adressID == null)
                adressID = 0;

            return adressID;
        }


        //US 07.04.2014 - MwstSatz aus Tabelle "mehrwertsteuer" 
        public Decimal getMwstSatz(Int32 aCode, DBConnect aConnect)
        {
            Decimal mwstSatz = 0;
            String strSQL;
            OdbcDataReader reader;
            salesmanType sT = new salesmanType();

            strSQL = "SELECT mwstsatz FROM bungert.mehrwertsteuer WHERE mwstcode = " + aCode.ToString();

            OdbcCommand cmd = new OdbcCommand(strSQL, aConnect.conn);

            using (reader = cmd.ExecuteReader())
            {

                if (reader.HasRows)
                {
                    mwstSatz = reader.GetDecimal(0);
                }

            }

            if (mwstSatz == null)
                mwstSatz = 0;

            return mwstSatz;
        }

        public class SmitConfig
        {
            public string changedCustomerDate;
            public string changedCustomerVehicleDate;
            public string lastOrderNumber;
        }

        public SmitConfig ReadXML()
        {
            SmitConfig conf = new SmitConfig();
            System.Xml.Serialization.XmlSerializer xmlReader = new System.Xml.Serialization.XmlSerializer(typeof(SmitConfig));

            using (System.IO.StreamReader fileIn = new System.IO.StreamReader(
                AppDomain.CurrentDomain.BaseDirectory + "SmitConfig.xml"))
            {
                conf = (SmitConfig)xmlReader.Deserialize(fileIn);
            }
            
            return conf;
        }

        public bool WriteXML(SmitConfig conf)
        {
            System.Xml.Serialization.XmlSerializer writer =
                 new System.Xml.Serialization.XmlSerializer(typeof(SmitConfig));

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(
                AppDomain.CurrentDomain.BaseDirectory + "SmitConfig.xml"))
            {
                writer.Serialize(file, conf);
                file.Close();
            }
            return true;
        }


        public getCustomerCountResponse getChangedCustomerCount(getCustomerCountRequest request)
        {
            getCustomerCountResponse res = new getCustomerCountResponse();
            Int32 anzahlAdressen;
            anzahlAdressen = 0;

            SmitConfig sc = new SmitConfig();
            sc = ReadXML();
            
            using (DBConnect myConnect = new DBConnect())
            {
                String sqlCommand;
                sqlCommand = "SELECT count(kontonr)";
                sqlCommand += " FROM bungert.adresse a LEFT OUTER JOIN bungert.marketingkontakt b";
                sqlCommand += " ON a.adressid = b.adressid WHERE a.aktiv = 1";
                sqlCommand += " AND (a.modifieddate >= '" + sc.changedCustomerDate + "' OR b.modifieddate >= '" + sc.changedCustomerDate + "')";
                OdbcCommand cmd = new OdbcCommand(sqlCommand, myConnect.conn);

                myConnect.Connect();

                OdbcDataReader reader;
                //DataReader Objekt wird initialisiert

                using (reader = cmd.ExecuteReader())
                {

                    if (reader.HasRows)
                    {
                        anzahlAdressen = reader.GetInt32(0);
                    }

                    reader.Close();
                }
            }

            res.@return = anzahlAdressen;
            //Test Anzahl Adressen auf 1 beschränkt US 16.01.2014
            //res.@return = 1;
            return res;
        }

        public getCustomersResponse getChangedCustomers(getCustomersRequest request)
        {
            Int32 kontonr;
            string anrede = "";
            string vorname = "";
            string name1 = "";
            string name2 = "";
            string name3 = "";
            string suchname = "";
            string strasse = "";
            string plz = "";
            string ort = "";
            string land = "";
            string telefonp = "";
            string mobilp = "";
            string emailp = "";
            string telefond = "";
            string mobild = "";
            string emaild = "";
            bool ok1marketing = false;
            bool ok1post = false;
            bool ok1telefonp = false;
            bool ok1mobilp = false;
            bool ok1emailp = false;
            bool ok1telefond = false;
            bool ok1mobild = false;
            bool ok1emaild = false;
            DateTime creationdate;
            string creationuser = "";
            DateTime modifieddate;
            string modifieduser = "";
            string gebDat = "";
            DateTime geburtsdatum;
            DateTime dtDefaultDB;
            dtDefaultDB = Convert.ToDateTime("01.01.1900");
            creationdate = dtDefaultDB;
            modifieddate = dtDefaultDB;
            bool privatPerson = false;
            string steuernummer = "";
            string ustidnr = "";
            string datWerkstatt = "";
            DateTime datumWerkstatt;
            string datTheke = "";
            DateTime datumTheke;
            string art = "";
            string gebiet = "";

            getCustomersResponse res = new getCustomersResponse();

            interfaceData iData = new interfaceData();
            customerType cust;
            customerTypeAddress custAdr;
            customerTypeName custName;
            customerTypePhoneMobil mobile;
            customerTypeEmail email;

            System.Collections.ArrayList aAdressen = new System.Collections.ArrayList();

            salesmanType sT = new salesmanType();
            sT.identifier = "fmad";
            sT.name = "Fmade";

            SmitConfig sc = new SmitConfig();
            sc = ReadXML();

            using (DBConnect myConnect = new DBConnect())
            {
                String sqlCommand;
                //Limitierung auf 100 Adressdatensätzen pro Abruf
                sqlCommand = "SELECT TOP 100 START AT " + request.Body.arg0.ToString() + " kontonr, anrede, vorname, name1, name2, name3, suchname, strasse, plz, ort, land, ";
                sqlCommand += " b.telefonp, b.mobilp, b.emailp, b.telefond, b.mobild, b.emaild,";
                sqlCommand += " b.ok1marketing, b.ok1post, b.ok1telefonp, b.ok1mobilp, b.ok1emailp, b.ok1telefond, b.ok1mobild, b.ok1emaild,";
                sqlCommand += " a.creationdate, a.creationuser, a.modifieddate, a.modifieduser, a.geburtsdatum, a.steuernummer, a.ustidnr, a.art, a.datumwerkstatt, a.datumtheke, a.gebiet";
                sqlCommand += " FROM bungert.adresse a LEFT OUTER JOIN bungert.marketingkontakt b";
                sqlCommand += " ON a.adressid = b.adressid WHERE aktiv = 1";
                sqlCommand += " AND (a.modifieddate >= '" + sc.changedCustomerDate + "' OR b.modifieddate >= '" + sc.changedCustomerDate + "')"; 
                sqlCommand += " ORDER BY a.adressid";
                OdbcCommand cmd = new OdbcCommand(sqlCommand, myConnect.conn);
                //cmd.Parameters.Add(":fahrgestellnr", OdbcType.VarChar, 17).Value = vin;

                myConnect.Connect();

                OdbcDataReader reader;
                //DataReader Objekt wird initialisiert
           
                using (reader = cmd.ExecuteReader())
                {

                    if (reader.HasRows)
                    {

                        while (reader.Read())
                        {
                            cust = new customerType();
                            custAdr = new customerTypeAddress();
                            privatPerson = false;
                                
                            kontonr = 0;

                            kontonr = reader.GetInt32(0);
                            anrede = reader.GetValue(1).ToString();
                            vorname = reader.GetValue(2).ToString();
                            name1 = reader.GetValue(3).ToString();
                            name2 = reader.GetValue(4).ToString();
                            name3 = reader.GetValue(5).ToString();
                            suchname = reader.GetValue(6).ToString();
                            strasse = reader.GetValue(7).ToString();
                            plz = reader.GetValue(8).ToString();
                            ort = reader.GetValue(9).ToString();
                            land = reader.GetValue(10).ToString();

                            telefonp = reader.GetValue(11).ToString();
                            mobilp = reader.GetValue(12).ToString();
                            emailp = reader.GetValue(13).ToString();
                            telefond = reader.GetValue(14).ToString();
                            mobild = reader.GetValue(15).ToString();
                            emaild = reader.GetValue(16).ToString();

                            ok1marketing = (String.IsNullOrWhiteSpace(reader.GetValue(17).ToString()) ? false : reader.GetBoolean(17));
                            ok1post = (String.IsNullOrWhiteSpace(reader.GetValue(18).ToString()) ? false : reader.GetBoolean(18));
                            ok1telefonp = (String.IsNullOrWhiteSpace(reader.GetValue(19).ToString()) ? false : reader.GetBoolean(19));
                            ok1mobilp = (String.IsNullOrWhiteSpace(reader.GetValue(20).ToString()) ? false : reader.GetBoolean(20));
                            ok1emailp = (String.IsNullOrWhiteSpace(reader.GetValue(21).ToString()) ? false : reader.GetBoolean(21));
                            ok1telefond = (String.IsNullOrWhiteSpace(reader.GetValue(22).ToString()) ? false : reader.GetBoolean(22));
                            ok1mobild = (String.IsNullOrWhiteSpace(reader.GetValue(23).ToString()) ? false : reader.GetBoolean(23));
                            ok1emaild = (String.IsNullOrWhiteSpace(reader.GetValue(24).ToString()) ? false : reader.GetBoolean(24));

                            creationdate = reader.GetDateTime(25);
                            creationuser = reader.GetValue(26).ToString();
                            modifieddate = reader.GetDateTime(27);
                            modifieduser = reader.GetValue(28).ToString();
                            gebDat = reader.GetValue(29).ToString();
                            steuernummer = reader.GetValue(30).ToString();
                            ustidnr = reader.GetValue(31).ToString();
                            art = reader.GetValue(32).ToString();
                            datWerkstatt = reader.GetValue(33).ToString();
                            datTheke = reader.GetValue(34).ToString();
                            gebiet = reader.GetValue(35).ToString(); //Verkäufer

                            if (String.IsNullOrWhiteSpace(gebDat))
                            {
                                geburtsdatum = dtDefaultDB;
                            }
                            else
                            {
                                geburtsdatum = Convert.ToDateTime(gebDat);
                            }

                            if (String.IsNullOrWhiteSpace(steuernummer))
                            {
                                steuernummer = "";
                            }


                            if (String.IsNullOrWhiteSpace(ustidnr))
                            {
                                ustidnr = "";
                            }

                            if (String.IsNullOrWhiteSpace(datWerkstatt))
                            {
                                datumWerkstatt = dtDefaultDB;
                            }
                            else
                            {
                                datumWerkstatt = Convert.ToDateTime(datWerkstatt);
                            }

                            if (String.IsNullOrWhiteSpace(datTheke))
                            {
                                datumTheke = dtDefaultDB;
                            }
                            else
                            {
                                datumTheke = Convert.ToDateTime(datTheke);
                            }

                            if (!String.IsNullOrWhiteSpace(gebiet))
                            {
                                salesmanType salesman = new salesmanType();
                                salesman.name = gebiet;
                                salesman.identifier = gebiet;
                                cust.salesman = salesman;
                            }

                            custAdr.city = ort;
                            custAdr.country = land;
                            custAdr.street = strasse;
                            custAdr.zip = plz;

                            cust.address = custAdr;

                            salesmanType sMT = new salesmanType();

                            sMT.identifier = creationuser;
                            sMT.name = creationuser;

                            customerTypeCreation custCr = new customerTypeCreation();
                            custCr.date = creationdate;
                            custCr.dateSpecified = true;
                            custCr.location = 1;
                            custCr.locationSpecified = true;
                            custCr.salesman = sMT;

                            cust.creation = custCr;

                            sMT = new salesmanType();
                            sMT.identifier = modifieduser;
                            sMT.name = modifieduser;

                            customerTypeLastChange custLC = new customerTypeLastChange();
                            custLC.date = modifieddate;
                            custLC.dateSpecified = true;
                            custLC.location = 1;
                            custLC.locationSpecified = true;
                            custLC.salesman = sMT;

                            cust.lastChange = custLC;

                            customerIdentificationType custID = new customerIdentificationType();

                            //Testdaten "location" muss noch mit fmade geklärt werden
                            custID.location = 1;
                            custID.number = kontonr;

                            cust.customerNumber = custID;

                            cust.matchcode = String.IsNullOrWhiteSpace(suchname) ? "" : suchname;
                            cust.firstName = String.IsNullOrWhiteSpace(vorname) ? "" : vorname;

                            //cust.fsalesNumber = 2;

                            cust.salutationTypeSpecified = false;

                            // Salutatuon 1 = Herr / 2 = Frau
                            if (anrede.ToLower().Contains("herr"))
                            {
                                cust.salutation = 1;
                                cust.salutationSpecified = true;
                                customerTypeSalutationLetter custSL = new customerTypeSalutationLetter();
                                custSL.id = 1;
                                custSL.description = String.IsNullOrWhiteSpace(name1) ? "" : name1;
                                custSL.idSpecified = true;
                                cust.salutationLetter = custSL;
                                privatPerson = true;
                            }
                            else if (anrede.ToLower().Contains("frau"))
                            {
                                cust.salutation = 2;
                                cust.salutationSpecified = true;
                                customerTypeSalutationLetter custSL = new customerTypeSalutationLetter();
                                custSL.id = 2;
                                custSL.description = String.IsNullOrWhiteSpace(name1) ? "" : name1;
                                custSL.idSpecified = true;
                                cust.salutationLetter = custSL;
                                privatPerson = true;
                            }
                            else if (anrede.ToLower().Contains("firma"))
                            {
                                cust.salutation = 9;
                                cust.salutationSpecified = true;
                                customerTypeSalutationLetter custSL = new customerTypeSalutationLetter();
                                custSL.id = 9;
                                custSL.idSpecified = true;
                                cust.salutationLetter = custSL;
                                privatPerson = false;
                            }
                            else //keine Anrede vorhanden
                            {
                                cust.salutation = 0;
                                cust.salutationSpecified = true;
                                customerTypeSalutationLetter custSL = new customerTypeSalutationLetter();
                                custSL.id = 0;
                                custSL.idSpecified = true;
                                cust.salutationLetter = custSL;
                                privatPerson = true;
                            }

                            customerTypeName[] custNames = new customerTypeName[3];
                            //List<customerTypeName> custNames = new List<customerTypeName>();

                            custName = new customerTypeName();
                            custName.sequence = 1;

                            if (privatPerson)
                            {
                                cust.lastName = name1;
                                name1 = vorname + " " + name1;
                            }

                            custName.name = name1;
                            custNames[0] = custName;

                            custName = new customerTypeName();
                            custName.sequence = 2;
                            custName.name = name2;
                            custNames[1] = custName;

                            custName = new customerTypeName();
                            custName.sequence = 3;
                            custName.name = name3;
                            custNames[2] = custName;

                            cust.name = custNames;

                            cust.birthDateSpecified = false;

                            if (geburtsdatum.Date > dtDefaultDB.Date)
                            {
                                cust.birthDate = geburtsdatum;
                                cust.birthDateSpecified = true;
                            }

                            customerTypeInvoice inv = new customerTypeInvoice();

                            customerTypeInvoiceLastPart lastPart = new customerTypeInvoiceLastPart();
                            customerTypeInvoiceLastWokshop lastWorkShop = new customerTypeInvoiceLastWokshop();

                            lastPart.dateSpecified = false;
                            lastWorkShop.dateSpecified = false;

                            if (datumWerkstatt.Date > dtDefaultDB)
                            {
                                lastWorkShop.date = datumWerkstatt;
                                lastWorkShop.dateSpecified = true;
                            }

                            if (datumTheke.Date > dtDefaultDB)
                            {
                                lastPart.date = datumTheke;
                                lastPart.dateSpecified = true;
                            }

                            inv.lastPart = lastPart;
                            inv.lastWokshop = lastWorkShop;

                            cust.invoice = inv;

                            if (art.ToLower().Trim() == "k")
                            {
                                cust.customerType1 = customerTypeCustomerType.customer; //Kunde
                                cust.customerType1Specified = true;
                            }
                            else if (art.ToLower().Trim() == "i")
                            {
                                cust.customerType1 = customerTypeCustomerType.prospect; //Interessent
                                cust.customerType1Specified = true;
                            }


                            customerTypeMarketing custMarketing = new customerTypeMarketing();
                            custMarketing.postWanted = ok1post;
                            custMarketing.postWantedSpecified = true;
                            custMarketing.emailWanted = ok1emailp;
                            custMarketing.emailWantedSpecified = true;
                            custMarketing.phoneWanted = ok1telefonp;
                            custMarketing.phoneWantedSpecified = true;
                            custMarketing.useData = ok1marketing;
                            custMarketing.useDataSpecified = true;
                            custMarketing.smsWanted = ok1mobilp;
                            custMarketing.smsWantedSpecified = true;
                            custMarketing.useData = ok1marketing;
                            custMarketing.useDataSpecified = true;

                            customerTypePhone custPhone = new customerTypePhone();
                            custPhone.@private = telefonp;
                            custPhone.business = telefond;

                            customerTypePhoneMobil[] mobiles = new customerTypePhoneMobil[2];
                            //List<customerTypePhoneMobil> mobiles = new List<customerTypePhoneMobil>();

                            mobile = new customerTypePhoneMobil();
                            mobile.number = mobilp;
                            mobile.sequence = 1;

                            mobiles[0] = mobile;

                            mobile = new customerTypePhoneMobil();
                            mobile.sequence = 2;
                            mobile.number = mobild;

                            mobiles[1] = mobile;

                            custPhone.mobil = mobiles;
                            cust.phone = custPhone;

                            customerTypeEmail[] emails = new customerTypeEmail[2];
                            //List<customerTypeEmail> emails = new List<customerTypeEmail>();

                            email = new customerTypeEmail();
                            email.address = emailp;
                            email.sequence = 1;

                            emails[0] = email;

                            email = new customerTypeEmail();
                            email.address = emaild;
                            email.sequence = 2;

                            emails[1] = email;

                            cust.email = emails;

                            cust.marketing = custMarketing;

                            customerTypeNumbers cTN = new customerTypeNumbers();
                            cTN.vatNumber = ustidnr;
                            cTN.taxNumber = steuernummer;

                            cust.numbers = cTN;

                            aAdressen.Add(cust);

                        }

                    }
                    reader.Close();
                }
              
            }
                       
            customerType[] aCustomers = new customerType[aAdressen.Count];
            //List<customerType> aCustomers = new List<customerType>();
            for (int i = 0; i < aAdressen.Count; i++)
            {
                customerType customer = (customerType)aAdressen[i];
                aCustomers[i] = customer;
                
            }

            iData.customer = aCustomers;
            iData.interfaceVersion = 1;
            iData.dataProvider = "STANDARD_INTERFACE";

            iData.callingUser = sT;
            iData.transmissionReason = "";

            System.IO.StringWriter stringWriter = new System.IO.StringWriter();

            var serializer = new XmlSerializer(typeof(interfaceData));
            using (var xw = XmlWriter.Create(stringWriter, new XmlWriterSettings { Encoding = new UTF8Encoding() }))
            {
                serializer.Serialize(xw, iData);

            }

            getCustomersResponseBody resBody = new getCustomersResponseBody();

            resBody.@return = stringWriter.ToString();

            res.Body = resBody;
            
            sc.changedCustomerDate = DateTime.Now.ToString("yyyy-MM-dd");

            WriteXML(sc);

            return res;
        }


        public getCustomerVehicleCountResponse getChangedCustomerVehicleCount(getCustomerVehicleCountRequest request)
        {
            getCustomerVehicleCountResponse res = new getCustomerVehicleCountResponse();
            Int32 anzahlFahrzeuge;
            anzahlFahrzeuge = 0;

            SmitConfig sc = new SmitConfig();
            sc = ReadXML();

            using (DBConnect myConnect = new DBConnect())
            {
                String sqlCommand;
                sqlCommand = "SELECT count(fahrzeugid)";
                sqlCommand += " FROM bungert.fahrzeug";
                //sqlCommand += " WHERE fahrzeug.aktiv = 1";
                sqlCommand += " WHERE fahrzeug.modifieddate >= '" + sc.changedCustomerVehicleDate + "'";
                OdbcCommand cmd = new OdbcCommand(sqlCommand, myConnect.conn);

                myConnect.Connect();

                OdbcDataReader reader;
                //DataReader Objekt wird initialisiert

                using (reader = cmd.ExecuteReader())
                {

                    if (reader.HasRows)
                    {
                        anzahlFahrzeuge = reader.GetInt32(0);
                    }

                    reader.Close();
                }
            }

            res.@return = anzahlFahrzeuge;
            //Test Anzahl Fahrzeug auf 1 beschränkt US 29.01.2014
            //res.@return = 1;
            return res;
        }

        public getCustomerVehiclesResponse getChangedCustomerVehicles(getCustomerVehiclesRequest request)
        {
            Int32 kontonr;
            Int32 fahrzeugid;
            string fahrgestellnr = "";
            string zulassungskz = "";
            string marke = "";
            string verkaufstyp = "";
            string bezeichnung = "";
            string motorseriennr = "";
            DateTime datumerstzulassung;
            string datErstzulassung = "";
            Int32 kilometerstand;
            string farbe = "";
            string innenausstattung = "";
            Int32 kw;
            DateTime datumbesuch;
            string datBesuch = "";
            DateTime creationdate;
            string creationuser = "";
            DateTime modifieddate;
            string modifieduser = "";
            DateTime datumzulassungkunde;
            string datZulassungkunde = "";
            DateTime datumhu;
            string datHU = "";
            DateTime datumau;
            string datAU = "";
            string zuendschluesselnr = "";
            string tuerschluesselnr = "";
            string radiocode = "";
            Int32 hubraum;
            string kbahsnr = "";
            string kbatsnr = "";
            string motortyp = "";
            string kfzbriefnr = "";
            string treibstoff = "";
            string info = "";
            string kfzversicherung = "";
            Int32 modelljahr;
            bool aktiv = false;
            string strVerkaeufer = "";
            string strKW = "";
            string strHubraum = "";
            string strModelljahr = "";
            string strKilometerstand = "";

            DateTime dtDefaultDB;
            dtDefaultDB = Convert.ToDateTime("01.01.1900");
            creationdate = dtDefaultDB;
            modifieddate = dtDefaultDB;
            datumzulassungkunde = dtDefaultDB;
            datumhu = dtDefaultDB;
            datumau = dtDefaultDB;
            datumbesuch = dtDefaultDB;
            datumerstzulassung = dtDefaultDB;

            getCustomerVehiclesResponse res = new getCustomerVehiclesResponse();

            interfaceData iData = new interfaceData();
            customerVehicleType cV;

            System.Collections.ArrayList aFahrzeuge = new System.Collections.ArrayList();

            salesmanType sT = new salesmanType();
            sT.identifier = "fmad";
            sT.name = "Fmade";

            SmitConfig sc = new SmitConfig();
            sc = ReadXML();

            using (DBConnect myConnect = new DBConnect())
            {
                String sqlCommand;
                //Limitierung auf 100 Adressdatensätzen pro Abruf
                sqlCommand = "SELECT TOP 100 START AT " + request.Body.arg0.ToString() + " a.kontonr, b.fahrzeugid, b.fahrgestellnr, b.zulassungskz, b.marke, b.verkaufstyp, b.bezeichnung, ";
                sqlCommand += "b.motorseriennr, b.datumerstzulassung, b.kilometerstand, b.farbe, b.innenausstattung, b.kw, b.verkaeufer, b.datumbesuch, b.creationdate, b.creationuser, b.modifieddate, b.modifieduser, ";
                sqlCommand += "b.datumzulassungkunde, b.datumhu, b.datumau, b.zuendschluesselnr, b.tuerschluesselnr, b.radiocode, b.hubraum, b.kbahsnr, b.motortyp, b.kfzbriefnr, b.treibstoff, b.info, b.kfzversicherung, b.modelljahr, ";
                sqlCommand += " b.aktiv, b.kbatsnr";
                sqlCommand += " FROM bungert.adresse a, bungert.fahrzeug b";
                sqlCommand += " WHERE  a.adressid = b.kundenadrid";
                sqlCommand += " AND b.modifieddate >= '" + sc.changedCustomerVehicleDate + "'";
                sqlCommand += " ORDER BY b.fahrzeugid";
                OdbcCommand cmd = new OdbcCommand(sqlCommand, myConnect.conn);
                //cmd.Parameters.Add(":fahrgestellnr", OdbcType.VarChar, 17).Value = vin;

                myConnect.Connect();

                OdbcDataReader reader;
                //DataReader Objekt wird initialisiert

                using (reader = cmd.ExecuteReader())
                {

                    if (reader.HasRows)
                    {

                        while (reader.Read())
                        {
                            cV = new customerVehicleType();

                            kontonr = 0;

                            kontonr = reader.GetInt32(0);
                            fahrzeugid = reader.GetInt32(1);
                            fahrgestellnr = reader.GetValue(2).ToString();
                            zulassungskz = reader.GetValue(3).ToString();
                            marke = reader.GetValue(4).ToString();
                            verkaufstyp = reader.GetValue(5).ToString();
                            bezeichnung = reader.GetValue(6).ToString();
                            motorseriennr = reader.GetValue(7).ToString();
                            datErstzulassung = reader.GetValue(8).ToString();
                            strKilometerstand = reader.GetValue(9).ToString();
                            farbe = reader.GetValue(10).ToString();
                            innenausstattung = reader.GetValue(11).ToString();
                            strKW = reader.GetValue(12).ToString();
                            strVerkaeufer = reader.GetValue(13).ToString();
                            datBesuch = reader.GetValue(14).ToString();
                            creationdate = reader.GetDateTime(15);
                            creationuser = reader.GetValue(16).ToString();
                            modifieddate = reader.GetDateTime(17);
                            modifieduser = reader.GetValue(18).ToString();
                            datZulassungkunde = reader.GetValue(19).ToString();
                            datHU = reader.GetValue(20).ToString();
                            datAU = reader.GetValue(21).ToString();
                            zuendschluesselnr = reader.GetValue(22).ToString();
                            tuerschluesselnr = reader.GetValue(23).ToString();
                            radiocode = reader.GetValue(24).ToString();
                            strHubraum = reader.GetValue(25).ToString();
                            kbahsnr = reader.GetValue(26).ToString();
                            motortyp = reader.GetValue(27).ToString();
                            kfzbriefnr = reader.GetValue(28).ToString();
                            treibstoff = reader.GetValue(29).ToString();
                            info = reader.GetValue(30).ToString();
                            kfzversicherung = reader.GetValue(31).ToString();
                            strModelljahr = reader.GetValue(32).ToString();
                            aktiv = reader.GetBoolean(33);
                            kbatsnr = reader.GetValue(34).ToString();

                            //Datum Erstzulassung
                            if (String.IsNullOrWhiteSpace(datErstzulassung))
                            {
                                datumerstzulassung = dtDefaultDB;
                            }
                            else
                            {
                                datumerstzulassung = Convert.ToDateTime(datErstzulassung);
                            }

                            //Datum Besuch
                            if (String.IsNullOrWhiteSpace(datBesuch))
                            {
                                datumbesuch = dtDefaultDB;
                            }
                            else
                            {
                                datumbesuch = Convert.ToDateTime(datBesuch);
                            }

                            //Datum Zulassungkunde
                            if (String.IsNullOrWhiteSpace(datZulassungkunde))
                            {
                                datumzulassungkunde = dtDefaultDB;
                            }
                            else
                            {
                                datumzulassungkunde = Convert.ToDateTime(datZulassungkunde);
                            }

                            //Datum HU
                            if (String.IsNullOrWhiteSpace(datHU))
                            {
                                datumhu = dtDefaultDB;
                            }
                            else
                            {
                                datumhu = Convert.ToDateTime(datHU);
                            }

                            //Datum AU
                            if (String.IsNullOrWhiteSpace(datAU))
                            {
                                datumau = dtDefaultDB;
                            }
                            else
                            {
                                datumau = Convert.ToDateTime(datAU);
                            }


                            customerIdentificationType custID = new customerIdentificationType();
                            custID.number = kontonr;

                            //US 10.02.2014
                            //Standort muss zum Verkaeufer passen - standardmäßig "location=1"
                            custID.location = 1;

                            cV.customerNumber = custID;
                            cV.vehicleNumber = fahrzeugid;
                            cV.vin = fahrgestellnr;

                            if (aktiv)
                            {
                                cV.deleted = false;
                            }
                            else
                            {
                                cV.deleted = true;
                            }

                            cV.licensePlate = zulassungskz;
                            cV.brand = marke;
                            cV.typeCode = verkaufstyp;
                            cV.type = bezeichnung;

                            if (String.IsNullOrWhiteSpace(strModelljahr))
                            {
                                modelljahr = 0;
                            }
                            else
                            {
                                modelljahr = Convert.ToInt32(strModelljahr);
                            }

                            cV.modelYear = Convert.ToDateTime("01.01." + modelljahr.ToString());
                            cV.modelYearSpecified = true;
                            cV.motorNumber = motorseriennr;
                            cV.firstRegistration = datumerstzulassung;
                            cV.firstRegistrationSpecified = true;

                            if (String.IsNullOrWhiteSpace(strKilometerstand))
                            {
                                kilometerstand = 0;
                            }
                            else
                            {
                                kilometerstand = Convert.ToInt32(strKilometerstand);
                            }

                            cV.milage = kilometerstand;
                            cV.milageSpecified = true;
                            cV.exteriorColor = farbe;
                            cV.interiorColor = innenausstattung;

                            if (String.IsNullOrWhiteSpace(strKW))
                            {
                                kw = 0;
                            }
                            else
                            {
                                kw = Convert.ToInt32(strKW);
                            }

                            cV.kw = kw;

                            customerVehicleTypeLastInvoice custLI = new customerVehicleTypeLastInvoice();
                            custLI.date = datumbesuch;
                            custLI.dateSpecified = true;
                            cV.lastInvoice = custLI;

                            customerVehicleTypeCreation custCreate = new customerVehicleTypeCreation();
                            custCreate.date = creationdate;
                            custCreate.dateSpecified = true;

                            cV.creation = custCreate;

                            salesmanType custST = new salesmanType();
                            custST.identifier = creationuser;
                            custST.name = creationuser;

                            custCreate.salesman = custST;

                            customerVehicleTypeLastChange custLS = new customerVehicleTypeLastChange();
                            custLS.date = modifieddate;
                            custLS.dateSpecified = true;

                            custST = new salesmanType();
                            custST.identifier = modifieduser;
                            custST.name = modifieduser;

                            custLS.salesman = custST;

                            cV.lastChange = custLS;

                            //Datum Zulassungkunde
                            cV.lastRegistrationSpecified = false;

                            if (datumzulassungkunde.Date > dtDefaultDB.Date)
                            {
                                cV.lastRegistration = datumzulassungkunde;
                                cV.lastRegistrationSpecified = true;
                            }

                            //Datum HU
                            cV.testDate1Specified = false;

                            if (datumhu.Date > dtDefaultDB.Date)
                            {
                                cV.testDate1 = datumhu;
                                cV.testDate1Specified = true;
                            }

                            //Datum AU
                            cV.testDate2Specified = false;

                            if (datumau.Date > dtDefaultDB.Date)
                            {
                                cV.testDate2 = datumau;
                                cV.testDate2Specified = true;
                            }

                            cV.keyCode1 = zuendschluesselnr;
                            cV.keyCode2 = tuerschluesselnr;
                            cV.radioCode = radiocode;

                            if (String.IsNullOrWhiteSpace(strHubraum))
                            {
                                hubraum = 0;
                            }
                            else
                            {
                                hubraum = Convert.ToInt32(strHubraum);
                            }

                            cV.capacity = hubraum;
                            cV.capacitySpecified = true;

                            if (String.IsNullOrWhiteSpace(kbahsnr)) kbahsnr = "";
                            if (String.IsNullOrWhiteSpace(kbatsnr)) kbatsnr = "";
                            cV.kbaNumber = kbahsnr + "/" + kbatsnr;

                            cV.motorCode = motortyp;
                            cV.carsLetter = kfzbriefnr;
                            cV.fuelDescription = treibstoff;
                            cV.remark = info;

                            customerVehicleTypeInsurance custInsurance = new customerVehicleTypeInsurance();
                            custInsurance.policyNumber = kfzversicherung;

                            cV.insurance = custInsurance;

                            aFahrzeuge.Add(cV);
                        }

                    }
                    reader.Close();
                }
            }

            customerVehicleType[] aCustVehicle = new customerVehicleType[aFahrzeuge.Count];
            //List<customerVehicleType> aCustVehicle = new List<customerVehicleType>();
            for (int i = 0; i < aFahrzeuge.Count; i++)
            {
                customerVehicleType custV = (customerVehicleType)aFahrzeuge[i];
                aCustVehicle[i] = custV;
            }

            iData.customerVehicle = aCustVehicle;
            iData.interfaceVersion = 1;
            iData.dataProvider = "STANDARD_INTERFACE";

            iData.callingUser = sT;
            iData.transmissionReason = "";

            System.IO.StringWriter stringWriter = new System.IO.StringWriter();

            var serializer = new XmlSerializer(typeof(interfaceData));
            using (var xw = XmlWriter.Create(stringWriter, new XmlWriterSettings { Encoding = new UTF8Encoding() }))
            {
                serializer.Serialize(xw, iData);

            }

            getCustomerVehiclesResponseBody resBody = new getCustomerVehiclesResponseBody();

            resBody.@return = stringWriter.ToString();
            res.Body = resBody;

            sc.changedCustomerVehicleDate = DateTime.Now.ToString("yyyy-MM-dd");
            WriteXML(sc);

            return res;
        }
        
        public createNewCustomerResponse createNewCustomer(createNewCustomerRequest request)
        {
            Int32 kontonr;
            string anrede = "";
            string briefanrede = "";
            string vorname = "";
            string nachname = "";
            string name1 = "";
            string name2 = "";
            string name3 = "";
            string suchname = "";
            string strasse = "";
            string plz = "";
            string ort = "";
            string land = "";
            DateTime modifieddate;
            string modifieduser = "";
            DateTime geburtsdatum;
            string steuernummer = "";
            string ustidnr = "";
            string telefonp = "";
            string mobilp = "";
            string emailp = "";
            string telefond = "";
            string mobild = "";
            string emaild = "";
            string ok1marketing = "";
            string ok1post = "";
            string ok1telefonp = "";
            string ok1emailp = "";
            string ok1sms = "";
            string email = "";
            Int32 rows = 0;
            string sqlCommand = "";
            string sqlCommandValues = "";
            string gebiet = "";

            createNewCustomerResponse res = new createNewCustomerResponse();

            Bungert.message m = new Bungert.message();
            customerType cust = new customerType();
            customerTypeAddress custAdr;

            try
            {
                //StreamReader sr = new StreamReader(@"D:\XML_newCustomer.txt");
                //string xmlNewCustomer = sr.ReadToEnd();
                //sr.Close();
                string xmlArg0 = "";
                xmlArg0 = request.Body.arg0.Replace("<postWanted/>", "").Replace("<emailWanted/>", "").Replace("<faxWanted/>", "").Replace("<phoneWanted/>", "").Replace("<smsWanted/>", "").Replace("<useData/>", "").Replace("<preferredMarketing/>", "");

                System.Xml.Serialization.XmlSerializer xmlReader = new System.Xml.Serialization.XmlSerializer(typeof(interfaceData));
                TextReader txtReader = new StringReader(xmlArg0);
                //TextReader txtReader = new StringReader(xmlNewCustomer);

                interfaceData data = new interfaceData();
                data = (interfaceData)xmlReader.Deserialize(txtReader);

                modifieddate = Convert.ToDateTime("01.01.1900");

                cust = data.customer[0];
                custAdr = cust.address;

                ort = custAdr.city;
                land = custAdr.country;
                strasse = custAdr.street;
                plz = custAdr.zip;

                customerIdentificationType custID;
                custID = cust.customerNumber;

                kontonr = custID.number;
                suchname = cust.matchcode;
                vorname = cust.firstName;

                if (!String.IsNullOrEmpty(cust.salesman.identifier))
                    gebiet = cust.salesman.identifier;

                switch (cust.salutation)
                {
                    case 1:
                        anrede = "Herr";
                        briefanrede = "Sehr geehrter Herr";
                        break;
                    case 2:
                        anrede = "Frau";
                        briefanrede = "Sehr geehrte Frau";
                        break;
                    case 9:
                        anrede = "Firma";
                        briefanrede = "Sehr geehrte Damen und Herren";
                        break;
                    default:
                        anrede = "";
                        briefanrede = "";
                        break;
                }

                nachname = cust.lastName;

                customerTypeName[] custNames = cust.name;
                //List<customerTypeName> custNames = cust.name;

                if (custNames != null)
                {
                    foreach (customerTypeName c in custNames)
                    {
                        switch (c.sequence)
                        {
                            case 1:
                                name1 = c.name;
                                break;
                            case 2:
                                name2 = c.name;
                                break;
                            case 3:
                                name3 = c.name;
                                break;
                        }
                    }
                }

                if (!String.IsNullOrWhiteSpace(nachname))
                    name1 = nachname;

                geburtsdatum = cust.birthDate;

                if (cust.numbers != null)
                {
                    ustidnr = cust.numbers.vatNumber;
                    steuernummer = cust.numbers.taxNumber;
                }

                if (cust.creation != null)
                {
                    modifieddate = cust.creation.date;

                    if (cust.creation.salesman != null)
                        modifieduser = cust.creation.salesman.name;
                }
                customerTypePhone phone = cust.phone;

                telefonp = phone.@private;
                telefond = phone.business;

                customerTypePhoneMobil[] mobiles = phone.mobil;
                //List<customerTypePhoneMobil> mobiles = phone.mobil;
                //teleonP = mobiles[0].number
                customerTypeEmail[] emails = cust.email;
                //List<customerTypeEmail> emails = cust.email;

                if (mobiles != null)
                {
                    foreach (customerTypePhoneMobil pM in mobiles)
                    {
                        switch (pM.sequence)
                        {
                            case 1:
                                mobilp = pM.number;
                                break;
                            case 2:
                                mobild = pM.number;
                                break;
                        }
                    }
                }

                if (emails != null)
                {
                    foreach (customerTypeEmail e in emails)
                    {
                        switch (e.sequence)
                        {
                            case 1:
                                emailp = e.address;
                                break;
                            case 2:
                                emaild = e.address;
                                break;
                        }
                    }
                }

                //letzte "kontonr" wird aus Tabelle "keylookup" gezogen
                using (DBConnect myConnect = new DBConnect())
                {
                    String sqlKontoNr;

                    sqlKontoNr = "SELECT keyvalue FROM bungert.keylookup WHERE tablename = 'adresse' AND fieldname = 'kontonr'";

                    OdbcCommand cmd = new OdbcCommand(sqlKontoNr, myConnect.conn);

                    myConnect.Connect();

                    OdbcDataReader reader;
                    //DataReader Objekt wird initialisiert

                    using (reader = cmd.ExecuteReader())
                    {

                        if (reader.HasRows)
                        {

                            while (reader.Read())
                            {
                                kontonr = reader.GetInt32(0);
                                kontonr++;
                            }
                        }
                    }

                    if (kontonr > 0)
                    {

                        //Einfügen Tabelle "adresse"

                        sqlCommand = "INSERT INTO bungert.adresse (kontonr, ";
                        sqlCommandValues += kontonr.ToString() + ", ";

                        if (!String.IsNullOrEmpty(anrede))
                        {
                            sqlCommand += "anrede, ";
                            sqlCommandValues += "'" + anrede + "', ";
                        }
                        if (!String.IsNullOrEmpty(briefanrede))
                        {
                            sqlCommand += "briefanrede, ";
                            sqlCommandValues += "'" + briefanrede + "', ";
                        }
                        if (!String.IsNullOrEmpty(vorname))
                        {
                            sqlCommand += "vorname, ";
                            sqlCommandValues += "'" + vorname + "', ";
                        }
                        if (!String.IsNullOrEmpty(name1))
                        {
                            sqlCommand += "name1, ";
                            sqlCommandValues += "'" + name1 + "', ";
                        }
                        if (!String.IsNullOrEmpty(name2))
                        {
                            sqlCommand += "name2, ";
                            sqlCommandValues += "'" + name2 + "', ";
                        }
                        if (!String.IsNullOrEmpty(name3))
                        {
                            sqlCommand += "name3, ";
                            sqlCommandValues += "'" + name3 + "', ";
                        }
                        if (!String.IsNullOrEmpty(suchname))
                        {
                            sqlCommand += "suchname, ";
                            sqlCommandValues += "'" + suchname + "', ";
                        }
                        if (!String.IsNullOrEmpty(strasse))
                        {
                            sqlCommand += "strasse, ";
                            sqlCommandValues += "'" + strasse + "', ";
                        }
                        if (!String.IsNullOrEmpty(plz))
                        {
                            sqlCommand += "plz, ";
                            sqlCommandValues += "'" + plz + "', ";
                        }
                        if (!String.IsNullOrEmpty(ort))
                        {
                            sqlCommand += "ort, ";
                            sqlCommandValues += "'" + ort + "', ";
                        }
                        if (!String.IsNullOrEmpty(land))
                        {
                            sqlCommand += "land, ";
                            sqlCommandValues += "'" + land + "', ";
                        }
                        if (modifieddate > Convert.ToDateTime("01.01.1900"))
                        {
                            sqlCommand += "modifieddate, ";
                            sqlCommandValues += "'" + modifieddate.ToString("yyyy-MM-dd") + "', ";
                        }
                        if (!String.IsNullOrEmpty(modifieduser))
                        {
                            sqlCommand += "modifieduser, ";
                            sqlCommandValues += "'" + modifieduser + "', ";
                        }

                        if (geburtsdatum > Convert.ToDateTime("01.01.1900"))
                        {
                            sqlCommand += "geburtsdatum, ";
                            sqlCommandValues += "'" + geburtsdatum.ToString("yyyy-MM-dd") + "', ";
                        }
                        if (!String.IsNullOrEmpty(steuernummer))
                        {
                            sqlCommand += "steuernummer, ";
                            sqlCommandValues += "'" + steuernummer + "', ";
                        }
                        if (!String.IsNullOrEmpty(ustidnr))
                        {
                            sqlCommand += "ustidnr, ";
                            sqlCommandValues += "'" + ustidnr + "', ";
                        }
                        if (!String.IsNullOrEmpty(telefonp))
                        {
                            sqlCommand += "komm1art, komm1nr, ";
                            sqlCommandValues += "'Telefon P', '" + telefonp + "', ";
                        }
                        if (!String.IsNullOrEmpty(telefond))
                        {
                            sqlCommand += "komm2art, komm2nr, ";
                            sqlCommandValues += "'Telefon D', '" + telefond + "', ";
                        }
                        if (!String.IsNullOrEmpty(mobilp))
                        {
                            sqlCommand += "komm3art, komm3nr, ";
                            sqlCommandValues += "'Mobil', '" + mobilp + "', ";
                        }
                        if (!String.IsNullOrEmpty(mobild))
                        {
                            sqlCommand += "komm4art, komm4nr, ";
                            sqlCommandValues += "'Mobil', '" + mobild + "', ";
                        }
                        if (!String.IsNullOrEmpty(emailp))
                        {
                            sqlCommand += "komm5art, komm5nr, ";
                            sqlCommandValues += "'E-Mail', '" + emailp + "', ";
                        }

                        if (!String.IsNullOrEmpty(gebiet))
                        {
                            sqlCommand += "gebiet, ";
                            sqlCommandValues += "'" + gebiet + "', ";
                        }

                        sqlCommand = sqlCommand.Substring(0, sqlCommand.Length - 2) + ")";
                        sqlCommandValues = sqlCommandValues.Substring(0, sqlCommandValues.Length - 2) + ")";

                        sqlCommand += " values (";
                        sqlCommand += sqlCommandValues;

                        //using (DBConnect myConnect = new DBConnect())
                        //{

                        cmd = new OdbcCommand(sqlCommand, myConnect.conn);

                        //myConnect.Connect();

                        rows = cmd.ExecuteNonQuery();

                        //Update der Tabelle "keylookup" - Wert für "kontonr" in Tabelle "adresse" wird  gesetzt
                        if (rows == 1)
                        {
                            string sqlUpdateKontoNr = "";
                            sqlUpdateKontoNr = "UPDATE bungert.keylookup SET keyvalue = " + kontonr + " WHERE tablename = 'adresse' AND fieldname = 'kontonr'";
                            cmd = new OdbcCommand(sqlUpdateKontoNr, myConnect.conn);

                            //myConnect.Connect();

                            rows = cmd.ExecuteNonQuery();
                        }
                        //}

                        customerTypeMarketing custMarketing = cust.marketing;
                        if (custMarketing != null)
                        {
                            ok1post = custMarketing.phoneWanted != null ? Convert.ToInt32(custMarketing.phoneWanted.Value).ToString() : null;
                            ok1emailp = custMarketing.emailWanted != null ? Convert.ToInt32(custMarketing.emailWanted.Value).ToString() : null;
                            ok1telefonp = custMarketing.phoneWanted != null ? Convert.ToInt32(custMarketing.phoneWanted.Value).ToString() : null;
                            ok1marketing = custMarketing.useData != null ? Convert.ToInt32(custMarketing.useData.Value).ToString() : null;
                            ok1sms = custMarketing.smsWanted != null ? Convert.ToInt32(custMarketing.smsWanted.Value).ToString() : null;
                           
                        }

                        //Einfügen Tabelle "adresse"

                        Int32 adressID = getAdressID(kontonr, myConnect);

                        if (adressID > 0)
                        {

                            sqlCommand = "INSERT INTO bungert.marketingkontakt (adressid,bearbeiter,auftragnr, ";
                            sqlCommandValues = adressID.ToString() + ",0,0, ";

                            if (!String.IsNullOrEmpty(telefonp))
                            {
                                sqlCommand += "telefonp, ";
                                sqlCommandValues += "'" + telefonp + "', ";
                            }
                            if (!String.IsNullOrEmpty(mobilp))
                            {
                                sqlCommand += "mobilp, ";
                                sqlCommand += "smsp, ";
                                sqlCommandValues += "'" + mobilp + "', ";
                                sqlCommandValues += "'" + mobilp + "', ";
                            }
                            if (!String.IsNullOrEmpty(emailp))
                            {
                                sqlCommand += "emailp, ";
                                sqlCommandValues += "'" + emailp + "', ";
                            }
                            if (!String.IsNullOrEmpty(telefond))
                            {
                                sqlCommand += "telefond, ";
                                sqlCommandValues += "'" + telefond + "', ";
                            }
                            if (!String.IsNullOrEmpty(mobild))
                            {
                                sqlCommand += "mobild, ";
                                sqlCommand += "smsd, ";
                                sqlCommandValues += "'" + mobild + "', ";
                                sqlCommandValues += "'" + mobild + "', ";
                            }
                            if (!String.IsNullOrEmpty(emaild))
                            {
                                sqlCommand += "emaild, ";
                                sqlCommandValues += "'" + emaild + "', ";
                            }
                            if (!String.IsNullOrEmpty(ok1post))
                            {
                                sqlCommand += "ok1post, ";
                                sqlCommand += "ok2post, ";
                                sqlCommandValues += "'" + ok1post + "', ";
                                sqlCommandValues += "'" + ok1post + "', ";
                            }
                            if (!String.IsNullOrEmpty(ok1emailp))
                            {
                                sqlCommand += "ok1emailp, ";
                                sqlCommand += "ok1emaild, ";
                                sqlCommand += "ok2emailp, ";
                                sqlCommand += "ok2emaild, ";
                                sqlCommandValues += "'" + ok1emailp + "', ";
                                sqlCommandValues += "'" + ok1emailp + "', ";
                                sqlCommandValues += "'" + ok1emailp + "', ";
                                sqlCommandValues += "'" + ok1emailp + "', ";
                            }
                            if (!String.IsNullOrEmpty(ok1telefonp))
                            {
                                sqlCommand += "ok1telefonp, ";
                                sqlCommand += "ok1telefond, ";
                                sqlCommand += "ok2telefonp, ";
                                sqlCommand += "ok2telefond, ";
                                sqlCommand += "ok1mobilp, ";
                                sqlCommand += "ok1mobild, ";
                                sqlCommand += "ok2mobilp, ";
                                sqlCommand += "ok2mobild, ";
                                sqlCommandValues += "'" + ok1telefonp + "', ";
                                sqlCommandValues += "'" + ok1telefonp + "', ";
                                sqlCommandValues += "'" + ok1telefonp + "', ";
                                sqlCommandValues += "'" + ok1telefonp + "', ";
                                sqlCommandValues += "'" + ok1telefonp + "', ";
                                sqlCommandValues += "'" + ok1telefonp + "', ";
                                sqlCommandValues += "'" + ok1telefonp + "', ";
                                sqlCommandValues += "'" + ok1telefonp + "', ";
                            }
                            if (!String.IsNullOrEmpty(ok1marketing))
                            {
                                sqlCommand += "ok1marketing, ";
                                sqlCommand += "ok2marketing, ";
                                sqlCommandValues += "'" + ok1marketing + "', ";
                                sqlCommandValues += "'" + ok1marketing + "', ";
                            }

                            if (!String.IsNullOrEmpty(ok1sms))
                            {
                                sqlCommand += "ok1smsp, ";
                                sqlCommand += "ok1smsd, ";
                                sqlCommand += "ok2smsp, ";
                                sqlCommand += "ok2smsd, ";
                                sqlCommandValues += "'" + ok1sms + "', ";
                                sqlCommandValues += "'" + ok1sms + "', ";
                                sqlCommandValues += "'" + ok1sms + "', ";
                                sqlCommandValues += "'" + ok1sms + "', ";
                            }

                            sqlCommand = sqlCommand.Substring(0, sqlCommand.Length - 2) + ")";
                            sqlCommandValues = sqlCommandValues.Substring(0, sqlCommandValues.Length - 2) + ")";

                            sqlCommand += " values (";
                            sqlCommand += sqlCommandValues;

                            cmd = new OdbcCommand(sqlCommand, myConnect.conn);

                            rows = cmd.ExecuteNonQuery();
                        }

                        Bungert.responseType r = new Bungert.responseType();
                        r.code = "0";

                        Bungert.customerIdentificationType cIT = new Bungert.customerIdentificationType();
                        cIT.location = 1;
                        cIT.number = kontonr;

                        r.customerNumber = cIT;
                        r.message = "Kunde wurde erfolgreich in P2 angelegt.";

                        m.success = r;
                    }
                    else
                    {
                        Bungert.responseType[] r = new Bungert.responseType[1];
                        r[0].code = "-1";

                        r[0].message = "Der Kunde konnte in P2 nicht angelegt werden. Es konnte keine KontoNr generiert werden.";

                        m.error = r;
                    }
                }


            }
            catch (System.Exception ex)
            {
                string strInner = ex.InnerException.ToString();
            }

            m.interfaceVersion = 1;
            m.dataProvider = "STANDARD_INTERFACE";
           
            //UFT-8 Encoding
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Bungert.message));
            MemoryStream memStrm = new MemoryStream();
            UTF8Encoding utf8e = new UTF8Encoding();
            XmlTextWriter xmlSink = new XmlTextWriter(memStrm, utf8e);
            xmlSerializer.Serialize(xmlSink, m);
            byte[] utf8EncodedData = memStrm.ToArray();
            string strMessage = utf8e.GetString(utf8EncodedData);

            createNewCustomerResponseBody resBody = new createNewCustomerResponseBody();
            resBody.@return = strMessage;
            res.Body = resBody;

            return res;
        }

        public changeCustomerResponse changeCustomer(changeCustomerRequest request)
        {
            changeCustomerResponse res = new changeCustomerResponse();

            System.Xml.Serialization.XmlSerializer xmlReader = new System.Xml.Serialization.XmlSerializer(typeof(interfaceData));
            TextReader txtReader = new StringReader(request.Body.arg0);
            interfaceData data = new interfaceData();
            data = (interfaceData)xmlReader.Deserialize(txtReader);
            
            Int32 kontonr;
            string anrede = "";
            string briefanrede = "";
            string vorname = "";
            string nachname = "";
            string name1 = "";
            string name2 = "";
            string name3 = "";
            string suchname = "";
            string strasse = "";
            string plz = "";
            string ort = "";
            string land = "";
            DateTime modifieddate;
            string modifieduser = "";
            DateTime geburtsdatum;
            string steuernummer = "";
            string ustidnr = "";

            string telefonp = "";
            string mobilp = "";
            string emailp = "";
            string telefond = "";
            string mobild = "";
            string emaild = "";
            string ok1marketing = "";
            string ok1post = "";
            string ok1telefonp = "";
            string ok1emailp = "";
            string ok1sms = "";
            string email = "";
            string gebiet = "";

            Bungert.message m = new Bungert.message();
            customerType cust = new customerType();
            customerTypeAddress custAdr;

            modifieddate = Convert.ToDateTime("01.01.1900");

            cust = data.customer[0];
            custAdr = cust.address;

            ort = custAdr.city;
            land = custAdr.country;
            strasse = custAdr.street;
            plz = custAdr.zip;

            customerIdentificationType custID;
            custID = cust.customerNumber;
            
            kontonr = custID.number;
            suchname = cust.matchcode;
            vorname = cust.firstName;

            if((cust.salesman != null))
            {
                if (!String.IsNullOrEmpty(cust.salesman.identifier))
                    gebiet = cust.salesman.identifier;
            }
                

            switch(cust.salutation)
            {
                case 1:
                    anrede = "Herr";
                    briefanrede = "Sehr geehrter Herr";
                    break;
                case 2:
                    anrede = "Frau";
                    briefanrede = "Sehr geehrte Frau";
                    break;
                case 9:
                    anrede = "Firma";
                    briefanrede = "Sehr geehrte Damen und Herren";
                    break;
                default:
                    anrede = "";
                    briefanrede = "";
                    break;
            }

            nachname = cust.lastName;

            customerTypeName[] custNames = cust.name;
            //List<customerTypeName> custNames = cust.name;

            if (custNames != null)
            {
                foreach (customerTypeName c in custNames)
                {
                    switch (c.sequence)
                    {
                        case 1:
                            name1 = c.name;
                            break;
                        case 2:
                            name2 = c.name;
                            break;
                        case 3:
                            name3 = c.name;
                            break;
                    }
                }
            }

            if (!String.IsNullOrEmpty(nachname))
                name1 = nachname;

            geburtsdatum = cust.birthDate;

            if (cust.numbers != null)
            {
                ustidnr = cust.numbers.vatNumber;
                steuernummer = cust.numbers.taxNumber;
            }
            
            if(cust.lastChange != null)
                modifieddate = cust.lastChange.date;

            if (cust.lastChange.salesman != null)
                modifieduser = cust.lastChange.salesman.name;

            if (cust.phone != null)
            {
                customerTypePhone phone = cust.phone;

                telefonp = phone.@private;
                telefond = phone.business;

                customerTypePhoneMobil[] mobiles = phone.mobil;
                //List<customerTypePhoneMobil> mobiles = phone.mobil;
                //teleonP = mobiles[0].number
                

                if (mobiles != null)
                {
                    foreach (customerTypePhoneMobil pM in mobiles)
                    {
                        switch (pM.sequence)
                        {
                            case 1:
                                mobilp = pM.number;
                                break;
                            case 2:
                                mobild = pM.number;
                                break;

                        }
                    }
                }
            }

            if (cust.email != null)
            {
                customerTypeEmail[] emails = cust.email;
                //List<customerTypeEmail> emails = cust.email;

                if (emails != null)
                {
                    foreach (customerTypeEmail e in emails)
                    {
                        switch (e.sequence)
                        {
                            case 1:
                                emailp = e.address;
                                break;
                            case 2:
                                emaild = e.address;
                                break;

                        }
                    }
                }
            }
          
            Int32 rows = 0;

            //Update Tabelle "adresse"
            string sqlCommand;

            sqlCommand = "Update bungert.adresse set ";

            if (!String.IsNullOrEmpty(anrede))
                sqlCommand += "anrede = '" + anrede + "', ";
            if (!String.IsNullOrEmpty(briefanrede))
                sqlCommand += "briefanrede = '" + briefanrede + "', ";
            if (!String.IsNullOrEmpty(vorname))
                sqlCommand += "vorname = '" + vorname + "', ";
            if (!String.IsNullOrEmpty(name1))
                sqlCommand += "name1 = '" + name1 + "', ";
            if (!String.IsNullOrEmpty(name2))
                sqlCommand += "name2 = '" + name2 + "', ";
            if (!String.IsNullOrEmpty(name3))
                sqlCommand += "name3 = '" + name3 + "', ";
            if (!String.IsNullOrEmpty(suchname))
                sqlCommand += "suchname = '" + suchname + "', ";
            if (!String.IsNullOrEmpty(strasse))
                sqlCommand += "strasse = '" + strasse + "', ";
            if (!String.IsNullOrEmpty(plz))
                sqlCommand += "plz = '" + plz + "', ";
            if (!String.IsNullOrEmpty(ort))
                sqlCommand += "ort = '" + ort + "', ";
            if (!String.IsNullOrEmpty(land))
                sqlCommand += "land = '" + land + "', ";
            if (modifieddate > Convert.ToDateTime("01.01.1900"))
                sqlCommand += "modifieddate = '" + modifieddate.ToString("yyyy-MM-dd") + "', ";
            if (!String.IsNullOrEmpty(modifieduser))
                sqlCommand += "modifieduser = '" + modifieduser + "', ";
            if (geburtsdatum > Convert.ToDateTime("01.01.1900"))
                sqlCommand += "geburtsdatum = '" + geburtsdatum.ToString("yyyy-MM-dd") + "', ";
            if (!String.IsNullOrEmpty(steuernummer))
                sqlCommand += "steuernummer = '" + steuernummer + "', ";
            if (!String.IsNullOrEmpty(ustidnr))
                sqlCommand += "ustidnr = '" + ustidnr + "', ";
            if (!String.IsNullOrEmpty(telefonp))
                sqlCommand += "komm1art = 'Telefon P', komm1nr = '" + telefonp + "', ";
            if (!String.IsNullOrEmpty(telefond))
                sqlCommand += "komm2art = 'Telefon D', komm2nr = '" + telefond + "', ";
            if (!String.IsNullOrEmpty(mobilp))
                sqlCommand += "komm3art = 'Mobil', komm3nr = '" + mobilp + "', ";
            if (!String.IsNullOrEmpty(mobild))
                sqlCommand += "komm4art = 'Mobil', komm4nr = '" + mobild + "', ";
            if (!String.IsNullOrEmpty(emailp))
                email = "komm5art = 'E-Mail', komm5nr = '" + emaild + "', ";
            if (!String.IsNullOrEmpty(emailp))
                email = "komm5art = 'E-Mail', komm5nr = '" + emailp + "', ";
            if (!String.IsNullOrEmpty(email))
                sqlCommand += email;
            if (!String.IsNullOrEmpty(gebiet))
                sqlCommand += "gebiet = '" + gebiet + "', ";
            
            sqlCommand = sqlCommand.Substring(0, sqlCommand.Length - 2);

            sqlCommand += " WHERE kontonr = " + kontonr.ToString();

            using (DBConnect myConnect = new DBConnect())
            {
                OdbcCommand cmd = new OdbcCommand(sqlCommand, myConnect.conn);
                myConnect.Connect();
                rows = cmd.ExecuteNonQuery();
            }

            customerTypeMarketing custMarketing = cust.marketing;
            if (custMarketing != null)
            {
                ok1post = custMarketing.phoneWanted != null ? Convert.ToInt32(custMarketing.phoneWanted.Value).ToString() : null;
                ok1emailp = custMarketing.emailWanted != null ? Convert.ToInt32(custMarketing.emailWanted.Value).ToString() : null;
                ok1telefonp = custMarketing.phoneWanted != null ? Convert.ToInt32(custMarketing.phoneWanted.Value).ToString() : null;
                ok1marketing = custMarketing.useData != null ? Convert.ToInt32(custMarketing.useData.Value).ToString() : null;
                ok1sms = custMarketing.smsWanted != null ? Convert.ToInt32(custMarketing.smsWanted.Value).ToString() : null;
            }
            
            //Update Tabelle "marketingkontakt"
            sqlCommand = "";

            if (!String.IsNullOrEmpty(telefonp))
                sqlCommand += "telefonp = '" + telefonp + "', ";
            if (!String.IsNullOrEmpty(mobilp))
            {
                sqlCommand += "mobilp = '" + mobilp + "', ";
                sqlCommand += "smsp = '" + mobilp + "', ";
            }
            if (!String.IsNullOrEmpty(emailp))
                sqlCommand += "emailp = '" + emailp + "', ";
            if (!String.IsNullOrEmpty(telefond))
                sqlCommand += "telefond = '" + telefond + "', ";
            if (!String.IsNullOrEmpty(mobild))
            {
                sqlCommand += "mobild = '" + mobild + "', ";
                sqlCommand += "smsd = '" + mobild + "', ";
            }
            if (!String.IsNullOrEmpty(emaild))
                sqlCommand += "emaild = '" + emaild + "', ";
            if (!String.IsNullOrEmpty(ok1post))
            {
                sqlCommand += "ok1post = " + ok1post + ", ";
                sqlCommand += "ok2post = " + ok1post + ", ";
            }
            if (!String.IsNullOrEmpty(ok1emailp))
            {
                sqlCommand += "ok1emailp = " + ok1emailp + ", ";
                sqlCommand += "ok1emaild = " + ok1emailp + ", ";
                sqlCommand += "ok2emailp = " + ok1emailp + ", ";
                sqlCommand += "ok2emaild = " + ok1emailp + ", ";
            }
            if (!String.IsNullOrEmpty(ok1telefonp))
            {
                sqlCommand += "ok1telefonp = " + ok1telefonp + ", ";
                sqlCommand += "ok1telefond = " + ok1telefonp + ", ";
                sqlCommand += "ok1mobilp = " + ok1telefonp + ", ";
                sqlCommand += "ok1mobild = " + ok1telefonp + ", ";
                sqlCommand += "ok2telefonp = " + ok1telefonp + ", ";
                sqlCommand += "ok2telefond = " + ok1telefonp + ", ";
                sqlCommand += "ok2mobilp = " + ok1telefonp + ", ";
                sqlCommand += "ok2mobild = " + ok1telefonp + ", ";
            }
            if (!String.IsNullOrEmpty(ok1marketing))
            {
                sqlCommand += "ok1marketing = " + ok1marketing + ", ";
                sqlCommand += "ok2marketing = " + ok1marketing + ", ";
            }
            
            if (!String.IsNullOrEmpty(ok1sms))
            {
                sqlCommand += "ok1smsp = " + ok1sms + ", ";
                sqlCommand += "ok1smsd = " + ok1sms + ", ";
                sqlCommand += "ok2smsp = " + ok1sms + ", ";
                sqlCommand += "ok2smsd = " + ok1sms + ", ";
            }

            if (!String.IsNullOrWhiteSpace(sqlCommand))
            {
                sqlCommand = "Update bungert.marketingkontakt a set " + sqlCommand;
                sqlCommand = sqlCommand.Substring(0, sqlCommand.Length - 2);
                sqlCommand += " FROM bungert.adresse b";
                sqlCommand += " WHERE a.adressid = b.adressid AND b.kontonr = " + kontonr.ToString();

                using (DBConnect myConnect = new DBConnect())
                {

                    OdbcCommand cmd = new OdbcCommand(sqlCommand, myConnect.conn);

                    myConnect.Connect();

                    rows = cmd.ExecuteNonQuery();
                }
            }

            m.dataProvider = "STANDARD_INTERFACE";
            Bungert.responseType r = new Bungert.responseType();
            r.code = "0";

            Bungert.customerIdentificationType cIT = new Bungert.customerIdentificationType();
            cIT.location = 1;
            cIT.number = kontonr;

            r.customerNumber = cIT;
            r.message = "Kundendaten wurden erfolgreich in P2 geändert.";
           
            m.success = r;
            m.interfaceVersion = 1;
                        
            //UFT-8 Encoding
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Bungert.message));
            MemoryStream memStrm = new MemoryStream();
            UTF8Encoding utf8e = new UTF8Encoding();
            XmlTextWriter xmlSink = new XmlTextWriter(memStrm, utf8e);
            xmlSerializer.Serialize(xmlSink, m);
            byte[] utf8EncodedData = memStrm.ToArray();
            string strMessage = utf8e.GetString(utf8EncodedData);

            changeCustomerResponseBody resBody = new changeCustomerResponseBody();
            resBody.@return = strMessage;
            res.Body = resBody;

            return res;

        }

        public deleteCustomerResponse deleteCustomer(deleteCustomerRequest request)
        {
            Int32 kontonr;
            Int32 rows;
            string sqlCommand = "";
            deleteCustomerResponse res = new deleteCustomerResponse();
           
            System.Xml.Serialization.XmlSerializer xmlReader = new System.Xml.Serialization.XmlSerializer(typeof(interfaceData));
            TextReader txtReader = new StringReader(request.Body.arg0);
            interfaceData data = new interfaceData();
            data = (interfaceData)xmlReader.Deserialize(txtReader);

            Bungert.message m = new Bungert.message();
            customerType cust = new customerType();

            cust = data.customer[0];

            customerIdentificationType custID;
            custID = cust.customerNumber;

            kontonr = custID.number;

            sqlCommand += "UPDATE bungert.adresse set aktiv = 0 WHERE kontonr = " + kontonr.ToString();

            using (DBConnect myConnect = new DBConnect())
            {

                OdbcCommand cmd = new OdbcCommand(sqlCommand, myConnect.conn);

                myConnect.Connect();

                rows = cmd.ExecuteNonQuery();
            }


            m.dataProvider = "STANDARD_INTERFACE";
            Bungert.responseType r = new Bungert.responseType();
            r.code = "0";

            Bungert.customerIdentificationType cIT = new Bungert.customerIdentificationType();
            cIT.location = 1;
            cIT.number = kontonr;

            r.customerNumber = cIT;
            r.message = "Kunde wurde in P2 auf inaktiv gesetzt.";

            m.success = r;
            m.interfaceVersion = 1;

            //System.IO.StringWriter strw = new System.IO.StringWriter();

            //UFT-8 Encoding
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Bungert.message));
            MemoryStream memStrm = new MemoryStream();
            UTF8Encoding utf8e = new UTF8Encoding();
            XmlTextWriter xmlSink = new XmlTextWriter(memStrm, utf8e);
            xmlSerializer.Serialize(xmlSink, m);
            byte[] utf8EncodedData = memStrm.ToArray();
            string strMessage = utf8e.GetString(utf8EncodedData);

            deleteCustomerResponseBody resBody = new deleteCustomerResponseBody();
            resBody.@return = strMessage;
            res.Body = resBody;

            return res;
        }


        public gotNewInvoicesResponse gotNewInvoices(gotNewInvoicesRequest request)
        {
            throw new NotImplementedException();
        }

        public getNewInvoiceResponse getNewInvoices(getNewInvoiceRequest request)
        {
            Int32 rechnungsNr;
            Int32 kontoNr;
            string strKontoNr = "";
            string kundenName1 = "";
            string kundenName2 = "";
            string kundenName3 = "";
            string kundenVorname = "";
            string kundenStrasse = "";
            string kundenPLZ = "";
            string kundenOrt = "";
            string kundenLand = "";
            Int32 rechnungsAdrID;
            string strRechnungsAdrID = "";
            string rechnungName1 = "";
            string rechnungName2 = "";
            string rechnungName3 = "";
            string rechnungVorname = "";
            string rechnungStrasse = "";
            string rechnungPLZ = "";
            string rechnungOrt = "";
            string rechnungLand = "";
            Int32 fahrzeugID;
            string fahrgestellNr = "";
            string marke = "";
            string bezeichnung = "";
            string zulassungsKZ = "";
            Int32 kilometerstand;
            string strKilometerstand = "";
            string motorserienNr = "";
            DateTime rechnungsDatum;
            string strRechnungsDatum = "";
            string strRechnungsNr = "";
            DateTime datumAuftrag;
            string strDatumAuftrag = "";
            string auftragNr = "";
            Int32 annehmer;
            string strAnnehmer = "";
            decimal sumVkGesamt;
            string strSumVkGesamt = "";
            decimal sumMwst;
            string strSumMwst = "";
            decimal posAnzahl;
            string strPosAnzahl = "";
            decimal posVkGesamt;
            string strPosVkGesamt = "";
            decimal posRabattBetrag;
            string strPosRabattBetrag = "";
            Int32 mwstCode;
            string kundenAnrede = "";
            string rechnungAnrede = "";

            DateTime dtDefaultDB;
            dtDefaultDB = Convert.ToDateTime("01.01.1900");

            rechnungsDatum = dtDefaultDB;
            datumAuftrag = dtDefaultDB;

            getNewInvoiceResponse res = new getNewInvoiceResponse();

            interfaceData iData = new interfaceData();

            invoiceType iT;
            invoiceTypeHeader iTH;
            invoiceCustomerTypeName[] aICTN;
            //List<invoiceCustomerTypeName> aICTN;
            invoiceCustomerTypeAddress iCTA;
            invoiceTypeHeaderVehicle iTHV;
            customerIdentificationType cIT;

            System.Collections.ArrayList aRechnungen = new System.Collections.ArrayList();

            salesmanType sT = new salesmanType();
            sT.identifier = "fmad";
            sT.name = "Fmade";

            SmitConfig sc = new SmitConfig();
            sc = ReadXML();

            using (DBConnect myConnect = new DBConnect())
            {

                String sqlCommand;
                //Limitierung auf 100 Auftragdatensätzen pro Abruf
                sqlCommand = "SELECT TOP 100 START AT " + request.Body.arg0.ToString() + " rechnungsnr, kontonr, kundenname1, kundenname2, kundenname3, kundenvorname, kundenstrasse, kundenplz, kundenort, kundenland, ";
                sqlCommand += " rechnungadrid, rechnungname1, rechnungname2, rechnungname3, rechnungvorname, rechnungstrasse, rechnungplz, rechnungort, rechnungland,";
                sqlCommand += " a.fahrzeugid, c.fahrgestellnr, c.marke, c.bezeichnung, c.zulassungskz, c.kilometerstand, c.motorseriennr, rechnungsdatum, rechnungsnr, datumauftrag, auftragnr, annehmer, sumvkgesamt, ";
                sqlCommand += " ifnull(summwst1, 0, summwst1) + ifnull(summwst2,0,summwst2) + ifnull(summwstat, 0, summwstat)  , kundenanrede, rechnunganrede ";
                sqlCommand += " FROM bungert.auftrag a, bungert.fahrzeug c";
                sqlCommand += " WHERE a.fahrzeugid = c.fahrzeugid and a.rechnungsnr is not null and a.rechnungsnr <> 0";
                sqlCommand += " AND a.auftragnr > " + (String.IsNullOrWhiteSpace(sc.lastOrderNumber) ? Int32.MaxValue.ToString() : sc.lastOrderNumber);
                sqlCommand += " ORDER BY a.auftragnr";
                OdbcCommand cmd = new OdbcCommand(sqlCommand, myConnect.conn);

                myConnect.Connect();

                OdbcDataReader reader;
                //DataReader Objekt wird initialisiert

                using (reader = cmd.ExecuteReader())
                {

                    if (reader.HasRows)
                    {

                        while (reader.Read())
                        {
                            iT = new invoiceType();
                            iTH = new invoiceTypeHeader();

                            rechnungsNr = reader.GetInt32(0);
                            strKontoNr = reader.GetValue(1).ToString();
                            kundenName1 = reader.GetValue(2).ToString();
                            kundenName2 = reader.GetValue(3).ToString();
                            kundenName3 = reader.GetValue(4).ToString();
                            kundenVorname = reader.GetValue(5).ToString();
                            kundenStrasse = reader.GetValue(6).ToString();
                            kundenPLZ = reader.GetValue(7).ToString();
                            kundenOrt = reader.GetValue(8).ToString();
                            kundenLand = reader.GetValue(9).ToString();
                            strRechnungsAdrID = reader.GetValue(10).ToString();
                            rechnungName1 = reader.GetValue(11).ToString();
                            rechnungName2 = reader.GetValue(12).ToString();
                            rechnungName3 = reader.GetValue(13).ToString();
                            rechnungVorname = reader.GetValue(14).ToString();
                            rechnungStrasse = reader.GetValue(15).ToString();
                            rechnungPLZ = reader.GetValue(16).ToString();
                            rechnungOrt = reader.GetValue(17).ToString();
                            rechnungLand = reader.GetValue(18).ToString();
                            fahrzeugID = reader.GetInt32(19);
                            fahrgestellNr = reader.GetValue(20).ToString();
                            marke = reader.GetValue(21).ToString();
                            bezeichnung = reader.GetValue(22).ToString();
                            zulassungsKZ = reader.GetValue(23).ToString();
                            strKilometerstand = reader.GetValue(24).ToString();
                            motorserienNr = reader.GetValue(25).ToString();
                            strRechnungsDatum = reader.GetValue(26).ToString();
                            strDatumAuftrag = reader.GetValue(28).ToString();
                            auftragNr = reader.GetValue(29).ToString();
                            strAnnehmer = reader.GetValue(30).ToString();
                            strSumVkGesamt = reader.GetValue(31).ToString();
                            strSumMwst = reader.GetValue(32).ToString();
                            kundenAnrede = reader.GetValue(33).ToString();
                            rechnungAnrede = reader.GetValue(34).ToString();

                            Decimal.TryParse(strSumVkGesamt, out sumVkGesamt);
                            Decimal.TryParse(strSumMwst, out sumMwst);
                            Decimal.TryParse(strPosAnzahl, out posAnzahl);
                            Decimal.TryParse(strPosRabattBetrag, out posRabattBetrag);
                            Decimal.TryParse(strPosVkGesamt, out posVkGesamt);

                            iT.invoiceID = rechnungsNr;

                            iTH.location = 1;
                            iTH.kindOfInvoice = invoiceTypeHeaderKindOfInvoice.workshop;

                            //Kunde
                            //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
                            invoiceCustomerType iCT = new invoiceCustomerType();

                            // Salutatuon 1 = Herr / 2 = Frau
                            if (kundenAnrede.ToLower().Contains("herr"))
                            {
                                iCT.salutation = 1;
                            }
                            else if (kundenAnrede.ToLower().Contains("frau"))
                            {
                                iCT.salutation = 2;
                            }
                            else if (kundenAnrede.ToLower().Contains("firma"))
                            {
                                iCT.salutation = 9;
                            }

                            aICTN = new invoiceCustomerTypeName[4];
                            //aICTN = new List<invoiceCustomerTypeName>();
                            invoiceCustomerTypeName iCTN;

                            if (iCT.salutation == 1 || iCT.salutation == 2)
                            {
                                kundenName1 = kundenVorname + " " + kundenName1;
                                kundenVorname = "";
                            }

                            iCTN = new invoiceCustomerTypeName();
                            iCTN.name = kundenName1;
                            iCTN.sequence = 1;
                            aICTN[0] = iCTN;

                            iCTN = new invoiceCustomerTypeName();
                            iCTN.name = kundenName2;
                            iCTN.sequence = 2;
                            aICTN[1] = iCTN;

                            iCTN = new invoiceCustomerTypeName();
                            iCTN.name = kundenName3;
                            iCTN.sequence = 3;
                            aICTN[2] = iCTN;

                            iCTN = new invoiceCustomerTypeName();
                            iCTN.name = kundenVorname;
                            iCTN.sequence = 4;
                            aICTN[3] = iCTN;

                            iCTA = new invoiceCustomerTypeAddress();

                            iCTA.city = kundenOrt;
                            iCTA.country = kundenLand;
                            iCTA.street = kundenStrasse;
                            iCTA.zip = kundenPLZ;

                            iCT.address = iCTA;

                            //KontoNr
                            if (String.IsNullOrWhiteSpace(strKontoNr))
                            {
                                kontoNr = 0;
                            }
                            else
                            {
                                kontoNr = Convert.ToInt32(strKontoNr);
                            }

                            //RechnungsAdrID
                            if (String.IsNullOrWhiteSpace(strRechnungsAdrID))
                            {
                                rechnungsAdrID = 0;
                            }
                            else
                            {
                                rechnungsAdrID = Convert.ToInt32(strRechnungsAdrID);
                            }

                            cIT = new customerIdentificationType();
                            cIT.location = 1;
                            cIT.number = kontoNr;

                            iCT.customerNumber = cIT;

                            iCT.name = aICTN;

                            iTH.customer = iCT;
                            //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<


                            //Abweichender Rechnungsempfänger
                            //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

                            iCT = new invoiceCustomerType();

                            // Salutatuon 1 = Herr / 2 = Frau
                            if (rechnungAnrede.ToLower().Contains("herr"))
                            {
                                iCT.salutation = 1;
                            }
                            else if (rechnungAnrede.ToLower().Contains("frau"))
                            {
                                iCT.salutation = 2;
                            }
                            else if (rechnungAnrede.ToLower().Contains("firma"))
                            {
                                iCT.salutation = 9;
                            }

                            aICTN = new invoiceCustomerTypeName[4];
                            //aICTN = new List<invoiceCustomerTypeName>();

                            if (iCT.salutation == 1 || iCT.salutation == 2)
                            {
                                rechnungName1 = rechnungVorname + " " + rechnungName1;
                                rechnungVorname = "";
                            }

                            iCTN = new invoiceCustomerTypeName();
                            iCTN.name = rechnungName1;
                            iCTN.sequence = 1;
                            aICTN[0] = iCTN;

                            iCTN = new invoiceCustomerTypeName();
                            iCTN.name = rechnungName2;
                            iCTN.sequence = 2;
                            aICTN[1] = iCTN;

                            iCTN = new invoiceCustomerTypeName();
                            iCTN.name = rechnungName3;
                            iCTN.sequence = 3;
                            aICTN[2] = iCTN;

                            iCTN = new invoiceCustomerTypeName();
                            iCTN.name = rechnungVorname;
                            iCTN.sequence = 4;
                            aICTN[3] = iCTN;

                            iCTA = new invoiceCustomerTypeAddress();

                            iCTA.city = rechnungOrt;
                            iCTA.country = rechnungLand;
                            iCTA.street = rechnungStrasse;
                            iCTA.zip = rechnungPLZ;

                            iCT.address = iCTA;

                            //RechnungsAdrID
                            if (String.IsNullOrWhiteSpace(strRechnungsAdrID))
                            {
                                rechnungsAdrID = 0;
                            }
                            else
                            {
                                rechnungsAdrID = Convert.ToInt32(strRechnungsAdrID);
                            }


                            cIT = new customerIdentificationType();
                            cIT.location = 1;
                            cIT.number = getKontNr(rechnungsAdrID, myConnect);

                            iCT.customerNumber = cIT;

                            iCT.name = aICTN;

                            iTH.differentInvoiceReceiver = iCT;
                            //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

                            iTHV = new invoiceTypeHeaderVehicle();

                            iTHV.brand = marke;
                            iTHV.licensePlate = zulassungsKZ;

                            //Kilometerstand
                            if (String.IsNullOrWhiteSpace(strKilometerstand))
                            {
                                kilometerstand = 0;
                            }
                            else
                            {
                                kilometerstand = Convert.ToInt32(strKilometerstand);
                            }

                            iTHV.milage = kilometerstand;
                            iTHV.motorNumber = motorserienNr;
                            iTHV.type = bezeichnung;
                            iTHV.vehicleNumber = fahrzeugID;
                            //iTHV.vehicleType = 
                            iTHV.vin = fahrgestellNr;

                            iTH.vehicle = iTHV;

                            //Rechnungsdatum
                            if (String.IsNullOrWhiteSpace(strRechnungsDatum))
                            {
                                rechnungsDatum = dtDefaultDB;
                            }
                            else
                            {
                                rechnungsDatum = Convert.ToDateTime(strRechnungsDatum);
                            }

                            iTH.invoiceDate = rechnungsDatum;
                            iTH.invoiceNumber = rechnungsNr.ToString();
                            //iTH.kindOfInvoice = 

                            //Datum Auftrag
                            if (String.IsNullOrWhiteSpace(strDatumAuftrag))
                            {
                                datumAuftrag = dtDefaultDB;
                            }
                            else
                            {
                                datumAuftrag = Convert.ToDateTime(strDatumAuftrag);
                            }

                            iTH.orderDate = datumAuftrag;
                            iTH.orderNumber = auftragNr.ToString();

                            //Annehmer
                            if (String.IsNullOrWhiteSpace(strAnnehmer))
                            {
                                annehmer = 0;
                            }
                            else
                            {
                                annehmer = Convert.ToInt32(strAnnehmer);
                            }

                            iTH.acceptor = getSalesman(annehmer, myConnect);

                            iTH.sumGross = Convert.ToDouble(sumVkGesamt);
                            iTH.sumNet = Convert.ToDouble(sumVkGesamt - sumMwst);

                            //iTH.vat = mw

                            iT.positions = getPostionen(auftragNr, myConnect);

                            iT.header = iTH;

                            //salesVehicleTypeDetailsPricesEfforts efforts = new salesVehicleTypeDetailsPricesEfforts();
                            //efforts.internEffort1Specified = (efforts.internEffort1 = Convert.ToDouble(aufbereitungek)) > 0;

                            if(!String.IsNullOrWhiteSpace(auftragNr))
                                aRechnungen.Add(iT);

                        }

                    }
                    reader.Close();
                }
            }

            invoiceType[] aInvoice = new invoiceType[aRechnungen.Count];
            //List<invoiceType> aInvoice = new List<invoiceType>();
            for (int i = 0; i < aRechnungen.Count; i++)
            {
                invoiceType iType = (invoiceType)aRechnungen[i];
                aInvoice[i] = iType;
            }


            iData.invoice = aInvoice;
            iData.interfaceVersion = 1;
            iData.dataProvider = "STANDARD_INTERFACE";

            iData.callingUser = sT;
            iData.transmissionReason = "";

            System.IO.StringWriter stringWriter = new System.IO.StringWriter();

            var serializer = new XmlSerializer(typeof(interfaceData));
            using (var xw = XmlWriter.Create(stringWriter, new XmlWriterSettings { Encoding = new UTF8Encoding() }))
            {
                serializer.Serialize(xw, iData);
            }

            if (!String.IsNullOrWhiteSpace(auftragNr))
            {
                sc.lastOrderNumber = auftragNr;
                WriteXML(sc);
            }
           

            getNewInvoiceResponseBody resBody = new getNewInvoiceResponseBody();

            resBody.@return = stringWriter.ToString();

            res.Body = resBody;

            return res;
        }

        public getNewInvoicesCountResponse getNewInvoicesCount(getNewInvoicesCountRequest request)
        {
            getNewInvoicesCountResponse res = new getNewInvoicesCountResponse();
            Int32 anzahlRechnungen;
            anzahlRechnungen = 0;
            string rechnungNr = "";

            SmitConfig sc = new SmitConfig();
            sc = ReadXML();
            rechnungNr = String.IsNullOrWhiteSpace(sc.lastOrderNumber) ? Int32.MaxValue.ToString() : sc.lastOrderNumber;
            using (DBConnect myConnect = new DBConnect())
            {
                String sqlCommand;
                sqlCommand = "SELECT count(rechnungsnr)";
                sqlCommand += " FROM bungert.auftrag";
                sqlCommand += " WHERE rechnungsnr is not null and rechnungsnr <> 0";
                sqlCommand += " AND auftragnr > " + rechnungNr;
          
                //Test Übergabe 03.04.2014
                OdbcCommand cmd = new OdbcCommand(sqlCommand, myConnect.conn);

                myConnect.Connect();

                OdbcDataReader reader;
                //DataReader Objekt wird initialisiert

                using (reader = cmd.ExecuteReader())
                {

                    if (reader.HasRows)
                    {
                        anzahlRechnungen = reader.GetInt32(0);
                    }

                    reader.Close();
                }
            }

            res.@return = anzahlRechnungen;
            //res.@return = 100;
            return res;
        }


        public changeCustomerVehicleResponse changeCustomerVehicle(changeCustomerVehicleRequest request)
        {
            Int32 kontonr;
            Int32 fahrzeugid;
            string fahrgestellnr = "";
            string zulassungskz = "";
            string marke = "";
            string verkaufstyp = "";
            string bezeichnung = "";
            string motorseriennr = "";
            DateTime datumerstzulassung;
            Int32 kilometerstand;
            string farbe = "";
            string innenausstattung = "";
            Int32 kw;
            DateTime datumbesuch;
            DateTime modifieddate;
            string modifieduser = "";
            DateTime datumzulassungkunde;
            DateTime datumhu;
            DateTime datumau;
            string zuendschluesselnr = "";
            string tuerschluesselnr = "";
            string radiocode = "";
            Int32 hubraum;
            string kbahsnr = "";
            string motortyp = "";
            string kfzbriefnr = "";
            string treibstoff = "";
            string info = "";
            string kfzversicherung = "";
            Int32 modelljahr;

            DateTime dtDefaultDB;
            dtDefaultDB = Convert.ToDateTime("01.01.1900");
            modifieddate = dtDefaultDB;
            datumzulassungkunde = dtDefaultDB;
            datumhu = dtDefaultDB;
            datumau = dtDefaultDB;
            datumbesuch = dtDefaultDB;
            datumerstzulassung = dtDefaultDB;

            changeCustomerVehicleResponse res = new changeCustomerVehicleResponse();
            Bungert.message m = new Bungert.message();

            System.Xml.Serialization.XmlSerializer xmlReader = new System.Xml.Serialization.XmlSerializer(typeof(interfaceData));
            TextReader txtReader = new StringReader(request.Body.arg0);
            interfaceData data = new interfaceData();
            data = (interfaceData)xmlReader.Deserialize(txtReader);

            customerVehicleType cV = data.customerVehicle[0];

            kontonr = 0;

            customerIdentificationType custID = cV.customerNumber;
            if(custID != null)
                kontonr = custID.number;

            fahrzeugid = cV.vehicleNumber;
            fahrgestellnr = cV.vin;
            zulassungskz = cV.licensePlate;
            marke = cV.brand;
            verkaufstyp = cV.typeCode;
            bezeichnung = cV.type;

            modelljahr = 0;

            if (cV.modelYear != null)
                modelljahr = cV.modelYear.Year;
            
            motorseriennr = cV.motorNumber;

            if (cV.firstRegistration != null)
                datumerstzulassung = cV.firstRegistration;
            
            kilometerstand = cV.milage;
            farbe = cV.exteriorColor;
            innenausstattung = cV.interiorColor;
            kw = cV.kw;

            customerVehicleTypeLastInvoice custLI = cV.lastInvoice;

            if(custLI != null)
                datumbesuch = custLI.date; // 01.01.0001 = fehlendes Datum         
            
            customerVehicleTypeLastChange custLS = cV.lastChange;

            if(custLS != null)
                modifieddate = custLS.date;

            salesmanType custST = custLS.salesman;

            if(custST != null)
                modifieduser =  custST.name;
           
            datumzulassungkunde = cV.lastRegistration;   
            datumhu = cV.testDate1;
            datumau = cV.testDate2;
            zuendschluesselnr = cV.keyCode1;
            tuerschluesselnr =  cV.keyCode2;
            radiocode =  cV.radioCode;
            hubraum = cV.capacity;
            kbahsnr = cV.kbaNumber;
            motortyp = cV.motorCode;
            kfzbriefnr = cV.carsLetter;
            treibstoff = cV.fuelDescription;
            info = cV.remark;
            
            customerVehicleTypeInsurance custInsurance = cV.insurance;

            if(custInsurance != null)
                kfzversicherung = custInsurance.policyNumber;

            Int32 rows = 0;

            //Update Tabelle "fahrzeug"
            string sqlCommand;

            sqlCommand = "Update bungert.fahrzeug set ";
        
            if (!String.IsNullOrEmpty(fahrgestellnr))
                sqlCommand += "fahrgestellnr = '" + fahrgestellnr + "', ";
            if (!String.IsNullOrEmpty(zulassungskz))
                sqlCommand += "zulassungskz = '" + zulassungskz + "', ";
            if (!String.IsNullOrEmpty(marke))
                sqlCommand += "marke = '" + marke + "', ";
            if (!String.IsNullOrEmpty(verkaufstyp))
                sqlCommand += "verkaufstyp = '" + verkaufstyp + "', ";
            if (!String.IsNullOrEmpty(bezeichnung))
                sqlCommand += "bezeichnung = '" + bezeichnung + "', ";
            if (!String.IsNullOrEmpty(motorseriennr))
                sqlCommand += "motorseriennr = '" + motorseriennr + "', ";
            if (datumerstzulassung > Convert.ToDateTime("01.01.1900"))
                sqlCommand += "datumerstzulassung = '" + datumerstzulassung.ToString("yyyy-MM-dd") + "', ";
            if (kilometerstand != null && kilometerstand != 0)
                sqlCommand += "kilometerstand = " + kilometerstand.ToString() + ", ";
            if (!String.IsNullOrEmpty(farbe))
                sqlCommand += "farbe = '" + farbe + "', ";
            if (!String.IsNullOrEmpty(innenausstattung))
                sqlCommand += "innenausstattung = '" + innenausstattung + "', ";
            if (kw != null && kw != 0)
                sqlCommand += "kw = " + kw.ToString() + ", ";
            if (datumbesuch > Convert.ToDateTime("01.01.1900"))
                sqlCommand += "datumbesuch = '" + datumbesuch.ToString("yyyy-MM-dd") + "', ";
            if (modifieddate > Convert.ToDateTime("01.01.1900"))
                sqlCommand += "modifieddate = '" + modifieddate.ToString("yyyy-MM-dd") + "', ";
            if (!String.IsNullOrEmpty(modifieduser))
                sqlCommand += "modifieduser = '" + modifieduser + "', ";
            if (datumzulassungkunde > Convert.ToDateTime("01.01.1900"))
                sqlCommand += "datumzulassungkunde = '" + datumzulassungkunde.ToString("yyyy-MM-dd") + "', ";
            if (datumhu > Convert.ToDateTime("01.01.1900"))
                sqlCommand += "datumhu = '" + datumhu.ToString("yyyy-MM-dd") + "', ";
            if (datumau > Convert.ToDateTime("01.01.1900"))
                sqlCommand += "datumau = '" + datumau.ToString("yyyy-MM-dd") + "', ";
            if (!String.IsNullOrEmpty(zuendschluesselnr))
                sqlCommand += "zuendschluesselnr = '" + zuendschluesselnr + "', ";
            if (!String.IsNullOrEmpty(tuerschluesselnr))
                sqlCommand += "tuerschluesselnr = '" + tuerschluesselnr + "', ";
            if (!String.IsNullOrEmpty(radiocode))
                sqlCommand += "radiocode = '" + radiocode + "', ";
            if (hubraum != null && hubraum != 0)
                sqlCommand += "hubraum = " + hubraum.ToString() + ", ";
            if (!String.IsNullOrEmpty(kbahsnr))
                sqlCommand += "kbahsnr = '" + kbahsnr + "', ";
            if (!String.IsNullOrEmpty(motortyp))
                sqlCommand += "motortyp = '" + motortyp + "', ";
            if (!String.IsNullOrEmpty(kfzbriefnr))
                sqlCommand += "kfzbriefnr = '" + kfzbriefnr + "', ";
            if (!String.IsNullOrEmpty(treibstoff))
                sqlCommand += "treibstoff = '" + treibstoff + "', ";
            if (!String.IsNullOrEmpty(info))
                sqlCommand += "info = '" + info + "', ";
            if (!String.IsNullOrEmpty(kfzversicherung))
                sqlCommand += "kfzversicherung = '" + kfzversicherung + "', ";
            if (modelljahr != null && modelljahr > 1)
                sqlCommand += "modelljahr = " + modelljahr.ToString() + ", ";

            sqlCommand = sqlCommand.Substring(0, sqlCommand.Length - 2);

            sqlCommand += " WHERE fahrzeugid = " + fahrzeugid.ToString();

            using (DBConnect myConnect = new DBConnect())
            {
                OdbcCommand cmd = new OdbcCommand(sqlCommand, myConnect.conn);
                myConnect.Connect();
                rows = cmd.ExecuteNonQuery();
            }

            m.dataProvider = "STANDARD_INTERFACE";
            Bungert.responseType r = new Bungert.responseType();
            r.code = "0";

            if (kontonr != null && kontonr != 0)
            {
                Bungert.customerIdentificationType cIT = new Bungert.customerIdentificationType();
                cIT.location = 1;
                cIT.number = kontonr;
                r.customerNumber = cIT;
            }

            r.vehicleNumber = fahrzeugid;
            r.vehicleNumberSpecified = true;
            r.message = "Fahrzeugdaten wurden erfolgreich in P2 geändert.";

            m.success = r;
            m.interfaceVersion = 1;

            //UFT-8 Encoding
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Bungert.message));
            MemoryStream memStrm = new MemoryStream();
            UTF8Encoding utf8e = new UTF8Encoding();
            XmlTextWriter xmlSink = new XmlTextWriter(memStrm, utf8e);
            xmlSerializer.Serialize(xmlSink, m);
            byte[] utf8EncodedData = memStrm.ToArray();
            string strMessage = utf8e.GetString(utf8EncodedData);

            changeCustomerVehicleResponseBody resBody = new changeCustomerVehicleResponseBody();
            resBody.@return = strMessage;
            res.Body = resBody;

            return res;
        }


        public deleteCustomerVehicleResponse deleteCustomerVehicle(deleteCustomerVehicleRequest request)
        {
            Int32 kontonr;
            Int32 rows;
            Int32 fahrzeugid;
            string sqlCommand = "";
            deleteCustomerVehicleResponse res = new deleteCustomerVehicleResponse();

            System.Xml.Serialization.XmlSerializer xmlReader = new System.Xml.Serialization.XmlSerializer(typeof(interfaceData));
            TextReader txtReader = new StringReader(request.Body.arg0);
            interfaceData data = new interfaceData();
            data = (interfaceData)xmlReader.Deserialize(txtReader);

            Bungert.message m = new Bungert.message();
            customerVehicleType cV = new customerVehicleType();

            cV = data.customerVehicle[0];

            customerIdentificationType custID;
            custID = cV.customerNumber;

            kontonr = 0;

            if (custID != null)
            {
                kontonr = custID.number;
            }

            fahrzeugid = cV.vehicleNumber;

            sqlCommand += "UPDATE bungert.fahrzeug set aktiv = 0 WHERE fahrzeugid = " + fahrzeugid.ToString();

            using (DBConnect myConnect = new DBConnect())
            {

                OdbcCommand cmd = new OdbcCommand(sqlCommand, myConnect.conn);

                myConnect.Connect();

                rows = cmd.ExecuteNonQuery();
            }

            m.dataProvider = "STANDARD_INTERFACE";
            Bungert.responseType r = new Bungert.responseType();
            r.code = "0";

            if (kontonr > 0)
            {
                Bungert.customerIdentificationType cIT = new Bungert.customerIdentificationType();
                cIT.location = 1;
                cIT.number = kontonr;
                r.customerNumber = cIT;
            }
            r.message = "Fahrzeug wurde in P2 auf inaktiv gesetzt.";

            m.success = r;
            m.interfaceVersion = 1;
         
            //UFT-8 Encoding
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Bungert.message));
            MemoryStream memStrm = new MemoryStream();
            UTF8Encoding utf8e = new UTF8Encoding();
            XmlTextWriter xmlSink = new XmlTextWriter(memStrm, utf8e);
            xmlSerializer.Serialize(xmlSink, m);
            byte[] utf8EncodedData = memStrm.ToArray();
            string strMessage = utf8e.GetString(utf8EncodedData);

            deleteCustomerVehicleResponseBody resBody = new deleteCustomerVehicleResponseBody();
            resBody.@return = strMessage;
            res.Body = resBody;

            return res;
        }
    }

    
}