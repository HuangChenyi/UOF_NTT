using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {

            string publicKey = "PFJTQUtleVZhbHVlPjxNb2R1bHVzPnZCMDVudFVRODhkNFBOZVdKZVprb3hnQytKODFVb0NseWhOUE1PMDBmdjZ4MmY0ZmpTenVQMVNUYitiUHVQd0R5NHQ2RDZvczYwWUlLRUYvMUxGWGNPc1E4REhNak9Gb0tmSHBELzEvSXV4REUrOE4xYUl6WE5pbTdPZC9nQ05PcTNLdE1RTExLYUQzajVlMkZQMkpBa1BaT1NORXQ2aVdBdFp4eEhpQ3FVRT08L01vZHVsdXM+PEV4cG9uZW50PkFRQUI8L0V4cG9uZW50PjwvUlNBS2V5VmFsdWU+";
            Auth.Authentication auth = new Auth.Authentication();
            auth.Url = "http://localhost/UOF_NTT"+"/PublicAPI/System/Authentication.asmx";

            string token = auth.GetToken("ERP",
                RSAEncrypt(publicKey,"admin"),
                RSAEncrypt(publicKey,"123456"));

            Console.WriteLine(token);

            WKF.Wkf wkf = new WKF.Wkf();
            wkf.Url = "http://localhost/UOF_NTT"+"/PublicAPI/WKF/Wkf.asmx";

            string result = "";

           // result = wkf.GetFormStructure(token, "0418e15d-67ee-4583-968d-b55f0bcae18b");

           // Console.WriteLine(result);

            //<Form formVersionId="6fd8fa9c-ecc2-41b3-8315-7c77d9e9e676" urgentLevel="2">
            // <Applicant account="Tony" groupId="" jobTitleId="">
            //  <Comment />
            // </Applicant>
            //  <FormFieldValue>
            //   <FieldItem fieldId="NO" fieldValue="" realValue="" enableSearch="True" IsNeedAutoNbr="false" />
            //   <FieldItem fieldId="type" fieldValue="A" realValue="" enableSearch="True" customValue="@null" fillerName="Tony" fillerUserGuid="c496e32b-0968-4de5-95fc-acf7e5a561c0" fillerAccount="Tony" fillSiteId="" />
            //   <FieldItem fieldId="item" fieldValue="AAA" realValue="" enableSearch="True" fillerName="Tony" fillerUserGuid="c496e32b-0968-4de5-95fc-acf7e5a561c0" fillerAccount="Tony" fillSiteId="" />
            //   <FieldItem fieldId="amount" fieldValue="111" realValue="" enableSearch="True" fillerName="Tony" fillerUserGuid="c496e32b-0968-4de5-95fc-acf7e5a561c0" fillerAccount="Tony" fillSiteId="" />
            //  </FormFieldValue>
            //</Form>

            XElement formXE = new XElement("Form",
                new XAttribute("formVersionId", "6fd8fa9c-ecc2-41b3-8315-7c77d9e9e676"),
                new XAttribute("urgentLevel", "2"));

            XElement applicantXE = new XElement("Applicant",
                new XAttribute("account", "Tony"),
                new XAttribute("groupId", ""),
                new XAttribute("jobTitleId", ""));

            XElement commentXE = new XElement("Comment","這是意見");

            XElement formFieldValueXE=new XElement("FormFieldValue");

            XElement noXE= new XElement("FieldItem",
                               new XAttribute("fieldId", "NO"),
                                new XAttribute("fieldValue", ""),
                                new XAttribute("IsNeedAutoNbr", "false"));

            XElement typeXE = new XElement("FieldItem",
                               new XAttribute("fieldId", "type"),
                                new XAttribute("fieldValue", "A"));

            XElement itemXE = new XElement("FieldItem",
                    new XAttribute("fieldId", "item"),
                     new XAttribute("fieldValue", "A項目"));

            XElement amountXE = new XElement("FieldItem",
                    new XAttribute("fieldId", "amount"),
                     new XAttribute("fieldValue", "100"));

            formXE.Add(applicantXE,formFieldValueXE);
            applicantXE.Add(commentXE);

            formFieldValueXE.Add(noXE, typeXE, itemXE, amountXE);

            Console.WriteLine(formXE.ToString());

            result=wkf.SendForm(token, formXE.ToString());
           
           // result=wkf.GetTaskData(token, "fc47e2b0-0dbf-4e7f-974e-1c1aef45de80");
            
            Console.WriteLine(result);
        }

        /// <summary>
        /// RSA 加密
        /// </summary>
        /// <param name="privateKey"></param>
        /// <param name="crTexturlparam>
        /// <returns></returns>
        private static string RSAEncrypt(string publicKey, string crText)
        {

            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();

            byte[] base64PublicKey = Convert.FromBase64String(publicKey);
            rsa.FromXmlString(System.Text.Encoding.UTF8.GetString(base64PublicKey));


            byte[] ctTextArray = Encoding.UTF8.GetBytes(crText);


            byte[] decodeBs = rsa.Encrypt(ctTextArray, false);

            return Convert.ToBase64String(decodeBs);
        }
    }
}
