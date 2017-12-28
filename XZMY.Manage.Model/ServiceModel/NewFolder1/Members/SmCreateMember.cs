using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using XZMY.Manage.Model.DataModel.Members;

namespace XZMY.Manage.Model.ServiceModel.Members
{
    [Serializable]
    [DataContract]
    public class SmCreateMember : IActionServiceModel2C<Member>
    {
        public Guid DataId { get; set; }

        [DataMember]
        public string AccName { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public int Type { get; set; }


        public Member CreateNewDataModel()
        {
            if (DataId == Guid.Empty) DataId = Guid.NewGuid();
            var model = new Member();
            //model.Id = Id;
            model.LoginName = AccName;
            if (AccName.IsEmail()) model.Email = AccName;
            if (AccName.IsMobile()) model.Mobile = AccName;
            model.Password = Password.ToMd5();
            model.Type = Type;
            model.State = Enum.EState.启用;
            model.RegisteredTime = DateTime.Now;
            return model;
        }
        public Student CreateNewStudentDataModel()
        {
            if (DataId == Guid.Empty) DataId = Guid.NewGuid();
            var model = new Student();
            //model.Id = Guid.NewGuid();
            model.MemberId = DataId;
            model.Name = AccName;
            if (AccName.IsEmail()) model.Email = AccName;
            if (AccName.IsMobile()) model.Mobile = AccName;
            return model;
        }
        public Parent CreateNewParentDataModel()
        {
            if (DataId == Guid.Empty) DataId = Guid.NewGuid();
            var model = new Parent();
            model.DataId = Guid.NewGuid();
            model.MemberId = DataId;
            model.Name = AccName;
            if (AccName.IsEmail()) model.Email = AccName;
            if (AccName.IsMobile()) model.Mobile = AccName;
            return model;
        }
    }
}
