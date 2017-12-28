using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using XZMY.Manage;
using XZMY.Manage.Web.Controllers;
using XZMY.Manage.Service.Auth.Data.SqlServer;
using Newtonsoft.Json;
using System.Linq.Expressions;
using XZMY.Manage.Web.Utils;
using System.IO;
using NPOI.XWPF.UserModel;
using Microsoft.Office.Interop.Word;
using XZMY.Manage.Web.Controllers.Planners;

namespace XZMY.Manage.Web.Content.Code
{
    public class PlanRecordWord
    {
        /// <summary>
        /// 规划导出为WORD
        /// </summary>
        /// <param name="PlanRecordId"></param>
        public void ToWord(Guid PlanRecordId)
        {
            PlanController bllPlan = new PlanController();
            var modelPlanRecord = bllPlan.GetSmPlanRecord(PlanRecordId);

            var path = AppDomain.CurrentDomain.BaseDirectory + "/plantemplate.docx";
            using (var stream = File.OpenRead(path))
            {
                var doc = new XWPFDocument(stream);
                using (var newstream = File.Create(AppDomain.CurrentDomain.BaseDirectory + "/poutput.docx"))
                {
                    doc.Write(newstream);
                    newstream.Flush();
                }
                var dic = new Dictionary<string, string> { };
                dic.Add("{$StudentName}", modelPlanRecord.StudentName);

                var np = doc.CreateParagraph();
                var run = np.CreateRun();
                run.SetText("324234234234");
                run = np.CreateRun();
                run.SetText("\r\n");
                run = np.CreateRun();
                run.SetText("324234234234");

                doc.SetParagraph(np, 1);


                foreach (var para in doc.Paragraphs)
                {
                    ReplaceKey(para, dic);
                }
                var tables = doc.Tables;
                foreach (var table in tables)
                {
                    foreach (var row in table.Rows)
                    {
                        foreach (var cell in row.GetTableCells())
                        {
                            foreach (var para in cell.Paragraphs)
                            {
                                ReplaceKey(para, dic);
                            }
                        }
                    }
                }
                
                using (var newstream = File.Create(AppDomain.CurrentDomain.BaseDirectory + "/output.docx"))
                {
                    
                    doc.Write(newstream);
                    newstream.Flush();
                 
                }
            }
        }
        private void ReplaceKey(XWPFParagraph para, IDictionary<string, string> redic)
        {

            string text = para.ParagraphText;
            foreach (var kv in redic)
            {
                if (text.Contains(kv.Key))
                {
                    para.ReplaceText(kv.Key, kv.Value);
                }

            }
            var runs = para.Runs;
            string styleid = para.Style;
            for (int i = 0; i < runs.Count; i++)
            {
                var run = runs[i];
                text = run.ToString();
                foreach (var kv in redic)
                {
                    if (text.Contains(kv.Key))
                    {
                        text = text.Replace(kv.Key, kv.Value);
                    }

                }

                runs[i].SetText(text, 0);
            }
        }


        public bool SaveAsWord(string fileName, string pFileName)
        {
            bool ret = false;
            object missing = System.Reflection.Missing.Value;
            object readOnly = false;
            object isVisible = false;
            object file1 = fileName;
            object html1 = pFileName;
            Microsoft.Office.Interop.Word.Application oWordApp = null;
            Microsoft.Office.Interop.Word.Document oWordDoc = null;
            try
            {



                object format = WdSaveFormat.wdFormatDocument;
                
                oWordApp = new ApplicationClass();
                oWordApp.Visible = false;


                oWordDoc = oWordApp.Documents.Open(ref file1, ref isVisible, ref readOnly, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref format, ref missing, ref isVisible, ref missing, ref missing, ref missing, ref missing);
                oWordDoc.SaveAs(ref html1, ref format, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing);



                ret = true;
            }
            catch (Exception ex)
            {
                ret = false;
            }
            finally
            {
                if (oWordDoc != null)
                {
                    oWordDoc.Close(ref missing, ref missing, ref missing);
                    oWordDoc = null;
                }
                if (oWordApp != null)
                {
                    oWordApp.Application.Quit(ref missing, ref missing, ref missing);
                    oWordApp = null;
                }
            }
            return ret;

        }
    }
}