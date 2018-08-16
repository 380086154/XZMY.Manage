using System;
using System.IO;
using System.Xml;
using System.Collections.Generic;

namespace XZMY.Manage.WindowsService
{
    /// <summary>
    /// Xml简易操作类
    /// </summary>
    public class XmlUtility
    {
        //private const String FolderName = "Log";//配置文件路径
        public string LogFileName { get; set; }
        private const string Id = "00000000-0000-0000-0000-000000000001"; //用于判断是否重新生成Xml文件
        private readonly Stream xmlStream;

        public XmlUtility()
        {

        }

        public XmlUtility(string path)
        {
            LogFileName = path + "/backuplog.xml";
            CreateFile();//文件不存在则创建

            //加载放到这里是为了处理Xml文件不存在的问题
            xmlStream = new MemoryStream(File.ReadAllBytes(LogFileName));
        }

        /// <summary>
        /// 新增数据
        /// </summary>
        /// <param name="entity">项目地址信息</param>
        public void Create(LogDto entity)
        {
            try
            {
                //新增一条记录
                var xmlDoc = new XmlDocument();
                xmlDoc.Load(LogFileName);
                var data = xmlDoc.SelectSingleNode("Data");//找到根节点

                var item = xmlDoc.CreateElement("Item");//创建子节点 User

                item.SetAttribute("Id", entity.Id.ToString());
                item.SetAttribute("FileName", entity.FileName);
                item.SetAttribute("TypeName", entity.TypeName.ToString());
                item.SetAttribute("Description", entity.Description);
                item.SetAttribute("CreatorIPv4", entity.CreatorIPv4);
                item.SetAttribute("CreatorHostName", entity.CreatorHostName);
                item.SetAttribute("CreatedTime", entity.CreatedTime);

                data.AppendChild(item);//子项添加到 Data 根节点中
                xmlDoc.Save(LogFileName);//保存

                CheckData();//检查数据上限，并删除超过的数据
            }
            catch (Exception ex)
            {

            }
        }

        //检查数据上限，并删除超过的数据
        public void CheckData()
        {
            //保留最多 166 条数据

        }

        /// <summary>
        /// 获取所有项目地址数据
        /// </summary>
        /// <returns></returns>
        public IList<LogDto> GetAll()
        {
            var list = new List<LogDto>();
            var xmlDoc = new XmlDocument();

            xmlDoc.Load(LogFileName);
            XmlNodeList xmlList = null;
            try
            {
                xmlList = xmlDoc.SelectSingleNode("Data").ChildNodes;
            }
            catch (Exception)
            {
                //这段代码后留一段时间，用来防止部分用户Xml结构不一致时使用（）
                xmlList = xmlDoc.SelectSingleNode("Data").ChildNodes;
            }

            foreach (XmlNode node in xmlList)
            {
                var entity = XmlNodeToModel(node);
                list.Add(entity);
            }
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private LogDto XmlNodeToModel(XmlNode node)
        {
            var typeName = 0;
            int.TryParse(Get(node, "TypeName"), out typeName);

            var entity = new LogDto
            {
                Id = new Guid(Get(node, "Id")),
                FileName = Get(node, "FileName"),
                TypeName = (Type)typeName,
                Description = Get(node, "Description"),
                CreatorIPv4 = Get(node, "CreatorIPv4"),
                CreatorHostName = Get(node, "CreatorHostName"),
                CreatedTime = Get(node, "CreatedTime"),
            };
            return entity;
        }

        private string Get(XmlNode node, string colume)
        {
            var attribute = node.Attributes[colume];
            if (attribute == null)
                return "";
            else
                return attribute.Value ?? "";
        }

        /// <summary>
        /// 在重新修改Xml结构之前此为依据创建Xml
        /// </summary>
        /// <returns></returns>
        public bool IsReset(string id)
        {
            if (!File.Exists(LogFileName))
                return true;

            var xmlDoc = new XmlDocument();
            xmlDoc.Load(LogFileName);
            var data = xmlDoc.SelectSingleNode(string.Format("Data[@Id='{0}']", id));
            if (data == null) return true;

            return !(data.Attributes["Id"].Value == Id);
        }

        /// <summary>
        /// 文件夹或文件不存在则创建
        /// </summary>
        private void CreateFile()
        {
            var result = IsReset(Id);//确认是否需要重建

            if (File.Exists(LogFileName) && !result) return;

            if (result && File.Exists(LogFileName))//需要重建
            {
                File.Delete(LogFileName);//首先将Xml文件删除
            }

            #region 创建默认Xml数据

            if (!File.Exists(LogFileName))
            {
                //如果文件不存在则创建
                var fs = File.Create(LogFileName);
                fs.Close();
                fs.Dispose();
            }

            var xmlDoc = new XmlDocument();
            var data = xmlDoc.CreateElement("Data");//创建根节点
            data.SetAttribute("Id", Id);//用于判断是否重新生成Xml文件 

            //var item = xmlDoc.CreateElement("Item");//创建子节点 Item
            //item.SetAttribute("Id", Guid.NewGuid().ToString());
            //item.SetAttribute("IsDefault", "");
            //item.SetAttribute("Domian", entity.Domian);//域名 Url
            //item.SetAttribute("Universe", entity.Universe);//宇宙 Url
            //item.SetAttribute("Language", entity.Language);//语言
            //item.SetAttribute("UserName", entity.UserName);
            //item.SetAttribute("Password", entity.Password);

            xmlDoc.AppendChild(data);
            xmlDoc.Save(LogFileName);//保存

            #endregion
        }
    }
}
