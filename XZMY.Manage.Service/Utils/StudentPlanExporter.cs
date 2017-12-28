
using NPOI.OpenXmlFormats.Wordprocessing;
using NPOI.XWPF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XZMY.Manage.Model.ViewModel.Planners;

namespace XZMY.Manage.Service.Utils
{
    public class StudentPlanExporter
    {
        public MemoryStream ExportWordFileStream(PlanTemplate data, string templatePath)
        {
            using (var stream = File.OpenRead(templatePath))
            {
                var doc = new XWPFDocument(stream);
                
                var dic = GetSimpleKeys(data);
                ReplaceSimpleKeys(doc, dic);
                if (data.PlannerImage != null)
                {
                    SetPlannerImage(doc, data.PlannerImage, templatePath);
                }
                //SetPlannerQuality(doc, data.PlannerQuality);
                //SetPlannerHonor(doc, data.PlannerHonor);

                SetSchools(doc, data.Schools);

                //SetEnglishScore(doc, data.listEnglishIndex);
                //SetLearnScore(doc, data.listLearnIndex);
                //SetQualityScore(doc, data.listQualityIndex);

                SetPlanningPath(doc, data.PlanningPath);



                var newstream = new MemoryStream();
                doc.Write(newstream);
                return newstream;
                
            }
        }

        private void SetPlanningPath(XWPFDocument doc, PlanningPath planningPath)
        {
            if (planningPath == null || planningPath.Points == null || planningPath.Points.Count == 0) return;
            var pi = FindParagraphIndex(doc, "{#PlanningPath}");
            if (pi == -1) return;
            doc.Paragraphs[pi].ReplaceText("{#PlanningPath}", "");
            var i = 0;
            foreach (var item in planningPath.Points)
            {
                XWPFParagraph np = CreateSimpleParagraph(doc, item.Name, 20);
                doc.SetParagraph(np, pi + i);
                #region s删除u
                foreach (var mR in np.Runs)
                {
                    doc.RemoveBodyElement(mR.GetTextPosition());
                }
                #endregion
                i++;

                foreach (var p in item.Projects)
                {
                    np = CreateSimpleParagraph(doc, p, 40);
                    doc.SetParagraph(np, pi + i);
                    #region s删除u
                    foreach (var mR in np.Runs)
                    {
                        doc.RemoveBodyElement(mR.GetTextPosition());
                    }
                    #endregion
                    i++;
                }

            }

        }

        private void SetQualityScore(XWPFDocument doc, List<AchievementIndex> listQualityIndex)
        {
            if (listQualityIndex == null || listQualityIndex.Count == 0) return;
            var pi = FindParagraphIndex(doc, "{#QualityScores}");
            if (pi == -1) return;
            doc.Paragraphs[pi].ReplaceText("{#QualityScores}", "");
            var i = 0;
            foreach (var item in listQualityIndex)
            {
                XWPFParagraph np = CreateSimpleParagraph(doc, String.Format("{0}:{1}({2})", item.Name, item.Score, item.IsPass ? "通过 " : "未通过"), 20);
                doc.SetParagraph(np, pi + i);
                i++;
            }
        }

        private void SetLearnScore(XWPFDocument doc, List<AchievementIndex> listLearnIndex)
        {
            if (listLearnIndex == null || listLearnIndex.Count == 0) return;
            var pi = FindParagraphIndex(doc, "{#LearnScores}");
            if (pi == -1) return;
            doc.Paragraphs[pi].ReplaceText("{#LearnScores}", "");
            var i = 0;
            foreach (var item in listLearnIndex)
            {
                XWPFParagraph np = CreateSimpleParagraph(doc, String.Format("{0}:{1}({2})", item.Name, item.Score, item.IsPass ? "通过 " : "未通过"), 20);
                doc.SetParagraph(np, pi + i);
                i++;
            }
        }

        private void SetEnglishScore(XWPFDocument doc, List<AchievementIndex> listEnglishIndex)
        {
            if (listEnglishIndex == null || listEnglishIndex.Count == 0) return;
            var pi = FindParagraphIndex(doc, "{#EnglishScores}");
            if (pi == -1) return;
            doc.Paragraphs[pi].ReplaceText("{#EnglishScores}", "");
            var i = 0;
            foreach (var item in listEnglishIndex)
            {
                XWPFParagraph np = CreateSimpleParagraph(doc, String.Format("{0}:{1}({2})", item.Name, item.Score, item.IsPass ? "通过 " : "未通过"), 20);
                doc.SetParagraph(np, pi + i);
                i++;
            }

        }

        private void SetSchools(XWPFDocument doc, IList<PlanningSchool> schools)
        {
            if (schools == null || schools.Count == 0) return;
            var pi = FindParagraphIndex(doc, "{#Schools}");
            if (pi == -1) return;
            doc.Paragraphs[pi].ReplaceText("{#Schools}", "");
            var i = 0;
            var s = 1;
            foreach (var item in schools)
            {
                XWPFParagraph np = CreateSimpleParagraph(doc, "推荐Top" + s);
                doc.SetParagraph(np, pi + i);

                #region s删除u
                foreach (var mR in np.Runs)
                {
                    doc.RemoveBodyElement(mR.GetTextPosition());
                }
                #endregion
                i++;
                if (item.Images.Count > 0)
                {
                    item.Logo = item.Images[0];
                }
                XWPFParagraph gp = CreateSimpleParagraphImage(doc, item.Logo, "slogo" + s);
                doc.SetParagraph(gp, pi + i);
                #region s删除u
                foreach (var mR in np.Runs)
                {
                    doc.RemoveBodyElement(mR.GetTextPosition());
                }
                #endregion
                i++;

                np = CreateSimpleParagraph(doc, "学校基础信息：");
                doc.SetParagraph(np, pi + i);
                #region s删除u
                foreach (var mR in np.Runs)
                {
                    doc.RemoveBodyElement(mR.GetTextPosition());
                }
                #endregion
                i++;

                np = CreateSimpleParagraph(doc, "所属地区：" + item.Location, 20);
                doc.SetParagraph(np, pi + i);
                #region s删除u
                foreach (var mR in np.Runs)
                {
                    doc.RemoveBodyElement(mR.GetTextPosition());
                }
                #endregion
                i++;
                np = CreateSimpleParagraph(doc, "成立时间：" + item.CreatedTime, 20);
                doc.SetParagraph(np, pi + i);
                #region s删除u
                foreach (var mR in np.Runs)
                {
                    doc.RemoveBodyElement(mR.GetTextPosition());
                }
                #endregion
                i++;
                np = CreateSimpleParagraph(doc, "州排名：" + item.Ranking, 20);
                doc.SetParagraph(np, pi + i);
                #region s删除u
                foreach (var mR in np.Runs)
                {
                    doc.RemoveBodyElement(mR.GetTextPosition());
                }
                #endregion
                i++;


                np = CreateSimpleParagraph(doc, "学校介绍：");
                doc.SetParagraph(np, pi + i);
                #region s删除u
                foreach (var mR in np.Runs)
                {
                    doc.RemoveBodyElement(mR.GetTextPosition());
                }
                #endregion
                i++;

                //foreach (var img in item.Images)
                //{
                //    gp = CreateSimpleParagraphImage(doc, img, "simg" + i);
                //    doc.SetParagraph(gp, pi + i);
                //    i++;
                //}

                np = CreateSimpleParagraph(doc, StringHtml.KillHtml(item.Description), 20);
                doc.SetParagraph(np, pi + i);
                #region s删除u
                foreach (var mR in np.Runs)
                {
                    doc.RemoveBodyElement(mR.GetTextPosition());
                }
                #endregion

                i++;

                s++;
            }


        }

        private void SetPlannerHonor(XWPFDocument doc, IList<string> plannerHonor)
        {
            if (plannerHonor == null || plannerHonor.Count == 0) return;

            var pi = FindParagraphIndex(doc, "{#PlannerHonor}");
            if (pi == -1) return;
            doc.Paragraphs[pi].ReplaceText("{#PlannerHonor}", "");
            var i = 0;
            foreach (var item in plannerHonor)
            {
                XWPFParagraph np = CreateSimpleParagraph(doc, item, 20);
                doc.SetParagraph(np, pi + i);
                i++;
            }
        }

        private void SetPlannerQuality(XWPFDocument doc, IList<string> plannerQuality)
        {
            if (plannerQuality == null || plannerQuality.Count == 0) return;

            var pi = FindParagraphIndex(doc, "{#PlannerQuality}");
            if (pi == -1) return;
            doc.Paragraphs[pi].ReplaceText("{#PlannerQuality}", "");
            var i = 0;
            foreach (var item in plannerQuality)
            {
                XWPFParagraph np = CreateSimpleParagraph(doc, item, 20);
                doc.SetParagraph(np, pi + i);
                i++;
            }
        }

        private static XWPFParagraph CreateSimpleParagraph(XWPFDocument doc, string text, int fl = 0)
        {
            var np = doc.CreateParagraph();
            np.IndentationFirstLine = fl;
            var run = np.CreateRun();
            run.SetText(text);
            return np;
        }

        private void SetPlannerImage(XWPFDocument doc, Stream plannerImage,string templatePath)
        {
            
            if (plannerImage == null || !plannerImage.CanRead) return;
            CT_P m_p = doc.Document.body.AddNewP();
            m_p.AddNewPPr().AddNewJc().val = ST_Jc.center;//段落水平居中
            XWPFParagraph gp = new XWPFParagraph(m_p, doc);
            var pi = FindParagraphIndex(doc, "{#PlannerImage}");
            var gr = gp.CreateRun();
            
            
            gr.AddPicture(plannerImage, (int)PictureType.JPEG, "1.jpg", 5000000, 5000000);
           
         
            //XWPFParagraph gp = CreateSimpleParagraphImage(doc, plannerImage, "plannerImage");
          
            doc.SetParagraph(gp, pi);
            doc.RemoveBodyElement(gr.GetTextPosition());
            
            //using (var stream = File.OpenRead(templatePath))
            //{
            //    var doc123 = new XWPFDocument(stream);
            //    var m_p = doc123.Document.body.AddNewP();
            //    m_p.AddNewPPr().AddNewJc().val = ST_Jc.center;//段落水平居中
            //    var gp = new XWPFParagraph(m_p, doc123);
            //    var gr = gp.CreateRun();
            //    gr.AddPicture(plannerImage, (int)NPOI.XWPF.UserModel.PictureType.JPEG, "plannerImage", 5000000, 5000000);

            //    var pi = FindParagraphIndex(doc, "{#PlannerImage}");
            //    doc.SetParagraph(gp, pi);
            //}






        }

        private static XWPFParagraph CreateSimpleParagraphImage(XWPFDocument doc, Stream image, string imagename)
        {
        
            var m_p = doc.Document.body.AddNewP();
            m_p.AddNewPPr().AddNewJc().val = ST_Jc.center;//段落水平居中
            var gp = new XWPFParagraph(m_p, doc);
            
            var gr = gp.CreateRun();
            gr.AddPicture(image, (int)NPOI.XWPF.UserModel.PictureType.JPEG, imagename, 5000000, 5000000);
            return gp; 
        }

        private Dictionary<string, string> GetSimpleKeys(PlanTemplate data)
        {
            var dic = new Dictionary<string, string> { };
            var type = typeof(PlanTemplate);
            //foreach (var p in type.GetProperties())
            //{
            //    if (p.PropertyType.IsValueType || p.PropertyType == typeof(string))
            //    {
            //        var value = p.GetValue(data);
            //        if (value == null) value = "NULL";
            //        var key = "{$" + p.Name + "}";
            //        dic.Add(key, value.ToString());
            //    }
            //}
            dic.Add("{$StudentName}", data.StudentName.ToString());
            dic.Add("{$StudentAge}", data.StudentAge.ToString());
            dic.Add("{$StudentGender}", data.StudentGender.ToString());
            dic.Add("{$PlanCreatedTime}", data.PlanCreatedTime.ToString());
            dic.Add("{$PlannerName}", data.PlannerName.ToString());
            dic.Add("{$PlannerDescrption}", StringHtml.KillHtml(data.PlannerDescription));
            if (data.PlannerQuality.Count > 0)
            {
                dic.Add("{#PlannerQuality}", data.PlannerQuality[0].ToString());
            }
            else {
                dic.Add("{#PlannerQuality}", "暂无资质");
            }
            if (data.PlannerHonor.Count > 0)
            {
                dic.Add("{#PlannerHonor}", data.PlannerQuality[0].ToString());
            }
            else
            {
                dic.Add("{#PlannerHonor}", "暂无荣誉");
            }
            dic.Add("{$TargetCountry}", data.TargetCountry);
            dic.Add("{$TargetSchoolType}", data.TargetSchoolType);
            dic.Add("{$Budget}", data.Budget.ToString());
            

            var keyEnglishScores = "{#EnglishScores}";
            dic.Add(keyEnglishScores, data.EnglishScore.ToString());

            var keyLearnScores = "{#LearnScores}";
            dic.Add(keyLearnScores, data.LearnScore.ToString());

            var keyQualityScores = "{#QualityScores}";
            dic.Add(keyQualityScores, data.QualityScore.ToString());
            return dic;
        }


        //private List<string> GetExtendKeys(PlanTemplate data)
        //{
        //    var dic = new List<string> { };
        //    var type = typeof(PlanTemplate);
        //    foreach (var p in type.GetProperties())
        //    {
        //        if (!(p.PropertyType.IsValueType || p.PropertyType == typeof(string)))
        //        {
        //            var key = "{#" + p.Name + "}";
        //            dic.Add(key);
        //        }
        //    }
        //    return dic;
        //}

        private int FindParagraphIndex(XWPFDocument doc, string keyword)
        {
            var i = 0;
            foreach (var para in doc.Paragraphs)
            {
                string text = para.ParagraphText;
                if (text.Contains(keyword))
                    return i;
                i++;
            }
            return -1;
        }

        private void ReplaceSimpleKeys(XWPFDocument doc, Dictionary<string, string> dic)
        {
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
        }

        private void ReplaceKey(XWPFParagraph para, IDictionary<string, string> redic)
        {

            foreach (var kv in redic)
            {
                ReplaceKey(para, kv.Key, kv.Value);
            }
        }

        private void ReplaceKey(XWPFParagraph para, string key, string value)
        {
            string text = para.ParagraphText;
            if (text.Contains(key))
            {
                para.ReplaceText(key, value);
            }

            var runs = para.Runs;
            string styleid = para.Style;
            for (int i = 0; i < runs.Count; i++)
            {
                var run = runs[i];
                text = run.ToString();
                if (text.Contains(key))
                {
                    text = text.Replace(key, value);
                }
                runs[i].SetText(text, 0);
            }
        }
    }
}