using NPOI.OpenXmlFormats.Dml.WordProcessing;
using NPOI.OpenXmlFormats.Wordprocessing;
using NPOI.XWPF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XZMY.Manage.Model.ViewModel.Planners;

namespace XZMY.Manage.ToWord
{
    public class ToWord111
    {
        public ToWord111() { }


        public MemoryStream ExportWordFileStream(PlanTemplate data, string templatePath)
        {
            using (var stream = File.OpenRead(templatePath))
            {
                var doc = new XWPFDocument(stream);

              
                if (data.PlannerImage != null)
                {
                    //anchor方式插图
                    CT_Anchor anchor = new CT_Anchor();
                    //图片距正文上(distT)、下(distB)、左(distL)、右(distR)的距离。114300EMUS=3.1mm
                    anchor.distT = 0u;
                    anchor.distB = 0u;
                    anchor.distL = 114300u;
                    anchor.distR = 114300u;
                    anchor.simplePos1 = false;
                    anchor.relativeHeight = 251658240u;
                    anchor.behindDoc = false;
                    anchor.locked = false;
                    anchor.layoutInCell = true;
                    anchor.allowOverlap = true;

                    CT_Positive2D simplePos = new CT_Positive2D();
                    simplePos.x = 0;
                    simplePos.y = 0;

                    CT_EffectExtent effectExtent = new CT_EffectExtent();
                    effectExtent.l = 0;
                    effectExtent.t = 0;
                    effectExtent.r = 0;
                    effectExtent.b = 0;


                    CT_PosH posH = new CT_PosH();
                    posH.relativeFrom = ST_RelFromH.column;
                    posH.posOffset = FindParagraphIndex(doc, "{$Hobby}")* 360000;//单位：EMUS,1CM=360000EMUS
                    CT_PosV posV = new CT_PosV();
                    posV.relativeFrom = ST_RelFromV.paragraph;
                    posV.posOffset = 200000;
                    CT_WrapSquare wrapSquare = new CT_WrapSquare();
                    wrapSquare.wrapText = ST_WrapText.largest;



                    //SetPlannerImage(doc, data.PlannerImage);
                    CT_P m_p = doc.Document.body.AddNewP();
                    XWPFParagraph gp = new XWPFParagraph(m_p, doc);

                    XWPFRun gr = gp.CreateRun();
                    gr = gp.CreateRun();
                    //inline方式插图
           
                    gr.AddPicture(data.PlannerImage, (int)NPOI.XWPF.UserModel.PictureType.JPEG, "1.jpg", 1000000, 1000000, posH, posV, wrapSquare, anchor, simplePos, effectExtent);


                    
                }
                //SetPlannerQuality(doc, data.PlannerQuality);
                //SetPlannerHonor(doc, data.PlannerHonor);

                //SetSchools(doc, data.Schools);

                //SetEnglishScore(doc, data.listEnglishIndex);
                //SetLearnScore(doc, data.listLearnIndex);
                //SetQualityScore(doc, data.listQualityIndex);

               // SetPlanningPath(doc, data.PlanningPath);



                var newstream = new MemoryStream();
                doc.Write(newstream);
                return newstream;

            }
        }


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
    }
}
