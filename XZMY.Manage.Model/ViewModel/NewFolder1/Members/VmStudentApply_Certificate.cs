using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XZMY.Manage.Model.DataModel.Members;
using XZMY.Manage.Model.Utils;

namespace XZMY.Manage.Model.ViewModel.Members
{
    /// <summary>
    /// 学生申请证书
    /// </summary>
    [Serializable]
    public class VmStudentApply_Certificate :ViewBase, IActionViewModel<StudentApply_Certificate>
    {
        public Guid DataId { get; set; }
        /// <summary>
        /// 学生ID
        /// </summary>
        public Guid StudentId { get; set; }
        /// <summary>
        /// 证书标题
        /// </summary>
        public String Title { get; set; }
        /// <summary>
        /// 证书级别
        /// </summary>
        public String LevelValue { get; set; }
        /// <summary>
        /// 年级
        /// </summary>
        public String Grade { get; set; }
        /// <summary>
        /// 证书照片
        /// </summary>
        public String Pictures { get; set; }
        /// <summary>
        /// 带域名的照片
        /// </summary>
        public String UrlPictures
        {
            get
            {
                string strPictures = Pictures;
                if (!String.IsNullOrEmpty(Pictures))
                {
                    var listP = Pictures.Split(",");
                    if (listP.Length > 0)
                    {
                        strPictures = string.Format("{0}{1}", System.Web.Configuration.WebConfigurationManager.AppSettings["WebSiteUrl"], listP[0]);
                    }
                }

                return strPictures;
            }
        }

        #region Extendsions

        public StudentApply_Certificate CreateNewDataModel()
        {
            var model = new StudentApply_Certificate();
            if (DataId == Guid.Empty) DataId = Guid.NewGuid();
            model.Grade = Grade;
            model.LevelValue = LevelValue;
            model.Pictures = Pictures;
            model.StudentId = StudentId;
            model.Title = Title;
            return model;
        }
        public StudentApply_Certificate MergeDataModel(StudentApply_Certificate model) {
            model.Grade = Grade;
            model.LevelValue = LevelValue;
            model.Pictures = Pictures;
            model.StudentId = StudentId;
            model.Title = Title;
            return model;
        }
        #endregion
    }
}
