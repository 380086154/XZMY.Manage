using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace XZMY.Manage.Model.ServiceModel.Albums
{
    [Serializable]
    [DataContract]
    public class SmAlbum
    {

        /// <summary>
        /// 
        /// </summary>
        //[EntAttributes.DBColumn("Title")] 
        //[DisplayName("")] 
        [DataMember]
        public String Title { get; set; }
        [DataMember]
        public String Detail { get; set; }
        /// <summary>
        /// 
        /// </summary>
        //[EntAttributes.DBColumn("Url")] 
        //[DisplayName("")] 
        [DataMember]
        public String Url { get; set; }
        [DataMember]
        public String Thumbnail { get; set; }


        [DataMember]
        public DateTime ModifiedTime { get; set; }


        [DataMember]
        public Guid Id { get; set; }
        
    }

}