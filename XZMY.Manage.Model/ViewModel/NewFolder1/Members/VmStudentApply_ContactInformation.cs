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
    /// 学生申请集联系方式
    /// </summary>
    [Serializable]
    public class VmStudentApply_ContactInformation :ViewBase, IActionViewModel<StudentApply_ContactInformation>
    {
        public Guid DataId { get; set; }
        /// <summary>
        /// 学生ID
        /// </summary>
        public Guid StudentId { get; set; }

        /// <summary>
        /// 当前所在国家ID
        /// </summary>
        public Guid CurrentCountryId { get; set; }
        /// <summary>
        /// 当前所在国家名称
        /// </summary>
        public String CurrentCountry { get; set; }
        /// <summary>
        /// 当前所在省份
        /// </summary>
        public String CurrentProvince { get; set; }
        /// <summary>
        /// 当前所在城市
        /// </summary>
        public String CurrentCity { get; set; }
        /// <summary>
        /// 当前所在的邮政编码
        /// </summary>
        public String CurrentZipCode { get; set; }
        /// <summary>
        /// 当前所在地址1
        /// </summary>
        public String CurrentAddress { get; set; }
        /// <summary>
        /// 当前所在地址2
        /// </summary>
        public String CurrentAddress2 { get; set; }
        /// <summary>
        /// 永久居住国家ID
        /// </summary>
        public Guid PermanentCountryId { get; set; }
        /// <summary>
        /// 永久居住国家名称
        /// </summary>
        public String PermanentCountry { get; set; }
        /// <summary>
        /// 永久居住省份
        /// </summary>
        public String PermanentProvince { get; set; }
        /// <summary>
        /// 永久居住城市
        /// </summary>
        public String PermanentCity { get; set; }
        /// <summary>
        /// 永久居住地邮政编码
        /// </summary>
        public String PermanentZipCode { get; set; }
        /// <summary>
        /// 永久居住地地址1
        /// </summary>
        public String PermanentAddress { get; set; }
        /// <summary>
        /// 永久居住地地址2
        /// </summary>
        public String PermanentAddress2 { get; set; }
        /// <summary>
        /// 居留权国家ID
        /// </summary>
        public Guid ResidenceCountryId { get; set; }
        /// <summary>
        /// 居留权国家名称
        /// </summary>
        public String ResidenceCountry { get; set; }

        #region Extendsions

        public StudentApply_ContactInformation CreateNewDataModel()
        {
            var model = new StudentApply_ContactInformation();
            if (DataId == Guid.Empty) DataId = Guid.NewGuid();
            model.StudentId = StudentId;
            model.CurrentCountryId = CurrentCountryId;
            model.CurrentCountry = CurrentCountry;
            model.CurrentProvince = CurrentProvince;
            model.CurrentCity = CurrentCity;
            model.CurrentZipCode = CurrentZipCode;
            model.CurrentAddress = CurrentAddress;
            model.CurrentAddress2 = CurrentAddress2;
            model.PermanentCountryId = PermanentCountryId;
            model.PermanentCountry = PermanentCountry;
            model.PermanentProvince = PermanentProvince;
            model.PermanentCity = PermanentCity;
            model.PermanentZipCode = PermanentZipCode;
            model.PermanentAddress = PermanentAddress;
            model.PermanentAddress2 = PermanentAddress2;
            model.ResidenceCountryId = ResidenceCountryId;
            model.ResidenceCountry = ResidenceCountry;
            return model;
        }
        public StudentApply_ContactInformation MergeDataModel(StudentApply_ContactInformation model)
        {
            model.StudentId = StudentId;
            model.CurrentCountryId = CurrentCountryId;
            model.CurrentCountry = CurrentCountry;
            model.CurrentProvince = CurrentProvince;
            model.CurrentCity = CurrentCity;
            model.CurrentZipCode = CurrentZipCode;
            model.CurrentAddress = CurrentAddress;
            model.CurrentAddress2 = CurrentAddress2;
            model.PermanentCountryId = PermanentCountryId;
            model.PermanentCountry = PermanentCountry;
            model.PermanentProvince = PermanentProvince;
            model.PermanentCity = PermanentCity;
            model.PermanentZipCode = PermanentZipCode;
            model.PermanentAddress = PermanentAddress;
            model.PermanentAddress2 = PermanentAddress2;
            model.ResidenceCountryId = ResidenceCountryId;
            model.ResidenceCountry = ResidenceCountry;
            return model;
        }
        #endregion
    }
}
