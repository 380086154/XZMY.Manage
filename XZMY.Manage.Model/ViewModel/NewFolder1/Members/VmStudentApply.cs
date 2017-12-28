using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XZMY.Manage.Model.ViewModel.Members
{
    /// <summary>
    /// 学生申请集
    /// </summary>
    [Serializable]
    public class VmStudentApply
    {
        /// <summary>
        /// 设置 或  获取 学生个人信息
        /// </summary>
        public VmStudent modelStudent{ get; set; }
        /// <summary>
        /// 设置 或 获取 留学意向
        /// </summary>
        public VmStudentApply_Intention modelIntention { get; set; }
        /// <summary>
        /// 设置 或 获取 学生联系方式
        /// </summary>
        public VmStudentApply_ContactInformation modelContactInformation { get; set; }
        /// <summary>
        /// 设置 或 获取学生获取证书列表
        /// </summary>
        public List<VmStudentApply_Certificate> listCreificate { get; set; }
        /// <summary>
        /// 设置 或 获取 监护人家庭成员列表
        /// </summary>
        public List<VmStudentApply_Guardian> listGuardian { get; set; }
        
        /// <summary>
        /// 设置 或 获取 兴趣列表
        /// </summary>
        public List<VmStudentApply_Interest> listInterest { get; set; }

        /// <summary>
        /// 设置 或 获取 课外活动列表
        /// </summary>
        public List<VmStudentApply_Project> listProject { get; set; }

        /// <summary>
        /// 设置 或 获取 就读学校信息
        /// </summary>
        public List<VmStudentApply_SchoolInformation> listSchoolInformation { get; set; }
    }
}
