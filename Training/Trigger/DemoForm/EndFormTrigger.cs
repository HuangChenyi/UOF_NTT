using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Ede.Uof.Utility.Log;
using Ede.Uof.WKF.ExternalUtility;
using Training.PO;
using Training.UCO;

namespace Training.Trigger.DemoForm
{
    public class EndFormTrigger : ICallbackTriggerPlugin
    {
        public void Finally()
        {
            //  throw new NotImplementedException();
        }

        public string GetFormResult(ApplyTask applyTask)
        {
            // throw new NotImplementedException();

            //<Form formVersionId="30d33f52-802f-49b3-933e-f93a9c5d61cb">
            //  <FormFieldValue>
            //    <FieldItem fieldId="NO" fieldValue="" realValue="" />
            //    <FieldItem fieldId="A01" fieldValue="xxx" realValue="" fillerName="黃建龍" fillerUserGuid="07a00c72-270e-403e-b9df-20b530ba45e8" fillerAccount="Howard_Huang" fillSiteId="" />
            //    <FieldItem fieldId="A02" fieldValue="3" realValue="" fillerName="黃建龍" fillerUserGuid="07a00c72-270e-403e-b9df-20b530ba45e8" fillerAccount="Howard_Huang" fillSiteId="" />
            //    <FieldItem fieldId="A03" fieldValue="4" realValue="" fillerName="黃建龍" fillerUserGuid="07a00c72-270e-403e-b9df-20b530ba45e8" fillerAccount="Howard_Huang" fillSiteId="" />
            //    <FieldItem fieldId="A04" fieldValue="222" realValue="" fillerName="黃建龍" fillerUserGuid="07a00c72-270e-403e-b9df-20b530ba45e8" fillerAccount="Howard_Huang" fillSiteId="" />
            //  </FormFieldValue>
            //</Form>

            DemoUCO uco = new DemoUCO();
            string docNbr = applyTask.FormNumber;
            string signStatus = applyTask.FormResult.ToString();

            uco.UpdateFormResult(docNbr, signStatus);


            if (applyTask.FormResult == Ede.Uof.WKF.Engine.ApplyResult.Adopt)
            {
                //    <Form formVersionId="1004107e-0993-4b83-945e-539d9fb42910" urgentLevel="2">
                //     <Applicant account="Tony" groupId="" jobTitleId="">
                //      <Comment />
                //     </Applicant>
                //      <FormFieldValue>
                //       <FieldItem fieldId="NO" fieldValue="" realValue="" enableSearch="True" IsNeedAutoNbr="false" />
                //       <FieldItem fieldId="RefForm" ConditionValue="" realValue="" fillerName="Tony" fillerUserGuid="c496e32b-0968-4de5-95fc-acf7e5a561c0" fillerAccount="Tony" fillSiteId="">
                //        <FormChooseInfo taskGuid="d0d0248c-5966-4890-9dab-24b6e55f71b6" />
                //       </FieldItem>
                //       <FieldItem fieldId="type" fieldValue="A" realValue="" enableSearch="True" customValue="@null" fillerName="Tony" fillerUserGuid="c496e32b-0968-4de5-95fc-acf7e5a561c0" fillerAccount="Tony" fillSiteId="" />
                //       <FieldItem fieldId="item" fieldValue="AAA" realValue="" enableSearch="True" fillerName="Tony" fillerUserGuid="c496e32b-0968-4de5-95fc-acf7e5a561c0" fillerAccount="Tony" fillSiteId="" />
                //       <FieldItem fieldId="amount" fieldValue="111" realValue="" enableSearch="True" fillerName="Tony" fillerUserGuid="c496e32b-0968-4de5-95fc-acf7e5a561c0" fillerAccount="Tony" fillSiteId="" />
                //      </FormFieldValue>
                //    </Form>
                //}

                string formInfo = GetFormInfo(applyTask);

                Ede.Uof.WKF.Utility.TaskUtilityUCO taskUtilityUCO = new Ede.Uof.WKF.Utility.TaskUtilityUCO();

                string result = taskUtilityUCO.WebService_CreateTask(formInfo);

                XElement resultXE = XElement.Parse(result);
                Logger.Write("NTTForm", formInfo);
                if(resultXE.Element("Status").Value =="0")
                {
                    string message= resultXE.Element("Exception").Element("Message").Value;

                    throw new Exception(message);

                }

            }


            return "";
        }

        private string GetFormInfo(ApplyTask applyTask)
        {
            var appicantInfo = applyTask.Task.Applicant;
            var fields = applyTask.Task.CurrentDocument.Fields;

            DemoPO po = new DemoPO();
            string usingVersionId = po.GetUsingVersionId("LabForm");


            XElement formXE = new XElement("Form",
     new XAttribute("formVersionId", usingVersionId),
     new XAttribute("urgentLevel", "2"));

            XElement applicantXE = new XElement("Applicant",
                new XAttribute("account",appicantInfo.Account),
                new XAttribute("groupId", ""),
                new XAttribute("jobTitleId", ""));

            XElement commentXE = new XElement("Comment", "");

            XElement formFieldValueXE = new XElement("FormFieldValue");

            XElement noXE = new XElement("FieldItem",
                               new XAttribute("fieldId", "NO"),
                                new XAttribute("fieldValue", ""),
                                new XAttribute("IsNeedAutoNbr", "false"));

            XElement typeXE = new XElement("FieldItem",
                               new XAttribute("fieldId", "type"),
                                new XAttribute("fieldValue", ""));

            XElement itemXE = new XElement("FieldItem",
                    new XAttribute("fieldId", "item"),
                     new XAttribute("fieldValue", fields["A01"].FieldValue));

            XElement amountXE = new XElement("FieldItem",
                    new XAttribute("fieldId", "amount"),
                     new XAttribute("fieldValue", fields["A02"].FieldValue));

            //       <FieldItem fieldId="RefForm" ConditionValue="" realValue="" fillerName="Tony" fillerUserGuid="c496e32b-0968-4de5-95fc-acf7e5a561c0" fillerAccount="Tony" fillSiteId="">
            //        <FormChooseInfo taskGuid="d0d0248c-5966-4890-9dab-24b6e55f71b6" />
            //       </FieldItem>
            XElement refFormXE= new XElement("FieldItem",
                                new XAttribute("fieldId", "RefForm"),
                                new XAttribute("ConditionValue", ""),
                                new XAttribute("realValue", ""));

            XElement formChooseInfoXE = new XElement("FormChooseInfo",
                        new XAttribute("taskGuid", applyTask.TaskId));
            refFormXE.Add(formChooseInfoXE);
            formXE.Add(applicantXE, formFieldValueXE);
            applicantXE.Add(commentXE);

            formFieldValueXE.Add(noXE, typeXE, itemXE, amountXE, refFormXE);


            return formXE.ToString();
        }

        public void OnError(Exception errorException)
        {
            //  throw new NotImplementedException();
        }
    }
}
