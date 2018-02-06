using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Web.Configuration;

namespace XZMY.Manage.Service.Utils.DataDictionary
{
    /// <summary>
    /// 数据字典XML操作类
    /// </summary>
    public class DataDictionaryManager
    {
        private static readonly string XmlDirectoryPath;
        private static readonly Dictionary<String, Dictionary<Guid, DataDictionaryItem>> DataCache;

        static DataDictionaryManager()
        {
            #region 初始化数据字典

            DataCache = new Dictionary<string, Dictionary<Guid, DataDictionaryItem>>();
            XmlDirectoryPath = AppDomain.CurrentDomain.BaseDirectory + "/App_Data/DataDictionary";
            if (!Directory.Exists(XmlDirectoryPath))
                Directory.CreateDirectory(XmlDirectoryPath);

            var xmlpaths = Directory.GetFiles(XmlDirectoryPath, "*.xml");
            foreach (var file in xmlpaths)
            {//读取所有分类
                try
                {
                    LoadDataFromFile(file);
                }
                catch { continue; }
            }

            #endregion
        }

        #region 私有方法

        /// <summary>
        /// 从文件中读取数据
        /// </summary>
        /// <param name="filepath"></param>
        private static void LoadDataFromFile(string filepath)
        {
            FileInfo fi = new FileInfo(filepath);

            XmlDocument xdoc = new XmlDocument();
            xdoc.Load(filepath);
            XmlNode root = xdoc.SelectSingleNode("Data");

            //获取文件名称
            String keyname = fi.Name.Substring(0, fi.Name.IndexOf('.'));

            //Console.WriteLine(keyname);

            #region 读取文件中的数据

            DataCache[keyname] = new Dictionary<Guid, DataDictionaryItem>();

            foreach (XmlNode node in root.ChildNodes)
            {
                var data = GetDataFromNode(node);
                if (data != null)
                {
                    DataCache[keyname][data.DataId] = data;
                }
            }

            #endregion
        }

        /// <summary>
        /// 把数据保存回文件
        /// </summary>
        /// <param name="catagory"></param>
        private static void SaveDataToFile(string catagory)
        {
            if (!DataCache.ContainsKey(catagory))
                return;

            XmlDocument xdoc = new XmlDocument();
            var root = xdoc.CreateElement("Data");
            xdoc.AppendChild(root);
            foreach (var data in DataCache[catagory].Values)
            {
                var node = xdoc.CreateElement("Item");
                node.SetAttribute("DataId", data.DataId.ToString());
                node.SetAttribute("Name", data.Name);
                node.SetAttribute("EName", data.EName);
                node.SetAttribute("Value", data.Value);
                node.SetAttribute("Default", data.IsDefault.ToString());
                node.SetAttribute("Sys", data.IsSystem.ToString());
                node.SetAttribute("State", data.State.ToString());
                node.SetAttribute("Sort", data.Sort.ToString());
                node.SetAttribute("Descr", data.Descr);
                root.AppendChild(node);
            }

            xdoc.Save(XmlDirectoryPath + @"\" + catagory + ".xml");
        }

        /// <summary>
        /// 从节点中读取数据对象
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private static DataDictionaryItem GetDataFromNode(XmlNode node)
        {
            if (node.Name != "Item")
                return null;
            String id = node.Attributes["DataId"].InnerText;
            Guid gid;
            if (!(Guid.TryParse(id, out gid)))
                return null;

            var name = node.Attributes["Name"].InnerText;
            var ename = node.Attributes["EName"].InnerText;
            var value = node.Attributes["Value"].InnerText;
            var isdefaultstr = node.Attributes["Default"].InnerText;
            var issysstr = node.Attributes["Sys"].InnerText;
            var sort = node.Attributes["Sort"].InnerText;
            var state = node.Attributes["State"].InnerText;
            var descr = node.Attributes["Descr"].InnerText;
            var isdefault = false;
            var issys = false;
            Boolean.TryParse(isdefaultstr, out isdefault);
            Boolean.TryParse(issysstr, out issys);

            var data = new DataDictionaryItem()
            {
                DataId = gid,
                Name = name,
                EName = ename,
                Value = value,
                State = Int32.Parse(state),//状态 
                Sort = Int32.Parse(sort),//排序
                IsDefault = isdefault,
                IsSystem = issys,
                Descr = descr
            };

            return data;
        }

        #endregion

        /// <summary>
        /// 分类列表
        /// </summary>
        /// <returns></returns>
        public static List<string> GetCatagories()
        {
            return DataCache.Keys.ToList();
        }

        /// <summary>
        /// 获取指定分类下的所有数据（含状态为【2】(已删除)的数据）
        /// </summary>
        /// <returns></returns>
        public static List<DataDictionaryItem> GetAll()
        {
            var result = new List<DataDictionaryItem>();
            DataCache.Values.Foreach(v => result.AddRange(v.Values));
            //foreach (var datacollection in DataCache.Values)
            //{
            //    result.AddRange(datacollection.Values);
            //}
            return result;
        }
        /// <summary>
        /// 获取指定分类下的所有数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static Dictionary<Guid, DataDictionaryItem> GetCatagory(string key)
        {
            return !DataCache.ContainsKey(key) ? null
                : DataCache[key].Values.ToDictionary(m => m.DataId);
        }

        public static Dictionary<string, Dictionary<Guid, DataDictionaryItem>> GetCatagories(Func<KeyValuePair<string, Dictionary<Guid, DataDictionaryItem>>, bool> func)
        {
            var res = new Dictionary<string, Dictionary<Guid, DataDictionaryItem>>();
            foreach (var item in DataCache.Where(func))
            {
                res.Add(item.Key, item.Value);
            }
            return res;
        }

        /// <summary>
        /// 获取数据字典是否包含此分类信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static Boolean ContainsCatagory(string key)
        {
            return DataCache.ContainsKey(key);
        }

        /// <summary>
        /// 按Id查找数据
        /// </summary>
        /// <param name="key">数据字典称</param>
        /// <param name="gid">数据字典项Id</param>
        /// <returns></returns>
        public static DataDictionaryItem GetDataById(string key, Guid gid)
        {
            return !DataCache.ContainsKey(key) ? null
                : DataCache[key].Values.FirstOrDefault(m => m.DataId == gid);
        }

        /// <summary>
        /// 根据Id集合查找数据
        /// </summary>
        /// <param name="idList">Id集合</param>
        /// <returns></returns>
        public static Dictionary<Guid, DataDictionaryItem> GetDataByIdList(List<Guid> idList)
        {
            var list = GetAll();

            return list.Where(item => idList.Contains(item.DataId)).ToDictionary(item => item.DataId);
        }

        /// <summary>
        /// 按名称查找数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static List<DataDictionaryItem> GetDataByName(string key, string name)
        {
            return GetDataByName(key, name, false);
        }

        /// <summary>
        /// 按名称查找数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="name"></param>
        /// <param name="fuzzysearch"></param>
        /// <returns></returns>
        public static List<DataDictionaryItem> GetDataByName(string key, string name, bool fuzzysearch)
        {
            if (!fuzzysearch)
                return !DataCache.ContainsKey(key) ? new List<DataDictionaryItem>()
                    : DataCache[key].Values.Where(m => m.Name == name).ToList();
            return GetDataByFuzzySearchName(key, name);
        }


        /// <summary>
        /// 按名称模糊搜索数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        private static List<DataDictionaryItem> GetDataByFuzzySearchName(string key, string name)
        {
            return !DataCache.ContainsKey(key) ? new List<DataDictionaryItem>()
                : DataCache[key].Values.Where(m => m.Name.Contains(name)).ToList();
        }

        /// <summary>
        /// 新建或更新数据信息
        /// </summary>
        /// <param name="catagory">数据字典类型名称</param>
        /// <param name="data">数据字典项</param>
        /// <returns></returns>
        public static Boolean SaveOrUpdateData(string catagory, DataDictionaryItem data)
        {
            SaveOrUpdateData(catagory, new DataDictionaryItem[] { data });

            return true;
        }

        /// <summary>
        /// 新建或更新数据信息
        /// </summary>
        /// <param name="catagoryName">数据字典类型名称</param>
        /// <param name="datalist">数据字典项集合</param>
        /// <returns></returns>
        public static Boolean SaveOrUpdateData(string catagoryName, IEnumerable<DataDictionaryItem> datalist)
        {
            if (!DataCache.ContainsKey(catagoryName))
                AddCatagory(catagoryName);

            foreach (var data in datalist)
            {
                DataCache[catagoryName][data.DataId] = data;
            }
            SaveDataToFile(catagoryName);

            return true;
        }

        /// <summary>
        /// 删除数据信息
        /// </summary>
        /// <param name="catagoryName">数据字典称</param>
        /// <param name="data">数据字典项</param>
        public static void RemoveData(string catagoryName, DataDictionaryItem data)
        {
            RemoveData(catagoryName, new DataDictionaryItem[] { data });
        }

        /// <summary>
        /// 批量删除数据信息
        /// </summary>
        /// <param name="catagoryName">数据字典称</param>
        /// <param name="datalist">数据字典项集合</param>
        public static void RemoveData(String catagoryName, IEnumerable<DataDictionaryItem> datalist)
        {
            if (!DataCache.ContainsKey(catagoryName))
                return;

            foreach (var data in datalist)
            {
                DataCache[catagoryName].Remove(data.DataId);
            }
            SaveDataToFile(catagoryName);
        }

        /// <summary>
        /// 新增【数据字典】
        /// </summary>
        /// <param name="catagoryName">数据字典数据字典称</param>
        public static void AddCatagory(string catagoryName)
        {
            if (DataCache.ContainsKey(catagoryName))
                return;

            DataCache.Add(catagoryName, new Dictionary<Guid, DataDictionaryItem>());
            SaveDataToFile(catagoryName);
        }

        /// <summary>
        /// 删除【数据字典】，此方法将删除分类内所有数据
        /// </summary>
        /// <param name="catagory"></param>
        public static void RemoveCatagory(String catagory)
        {
            if (!DataCache.ContainsKey(catagory))
                return;

            DataCache.Remove(catagory);
            File.Delete(XmlDirectoryPath + @"\" + catagory + ".xml");
        }

        /// <summary>
        /// 按【数据项】模糊搜索数据
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static IList<DataDictionaryItem> SearchByName(string name)
        {
            var result = new List<DataDictionaryItem>();
            DataCache.Foreach(kv => result.AddRange(kv.Value.Where(v => v.Value.Name.Contains(name)).Select(v => v.Value)));

            return result;
        }

        /// <summary>
        /// 模糊搜索数据
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static IList<DataDictionaryItem> FuzzySearch(String name)
        {
            var result = new List<DataDictionaryItem>();

            DataCache.Foreach(kv => result.AddRange(kv.Value.Where(v => ValidFuzzySearch(v.Value, name)).Select(v => v.Value)));

            return result;
        }

        private static Boolean ValidFuzzySearch(DataDictionaryItem item, string name)
        {
            return item.Name.Contains(name) ||
                item.EName.Contains(name);
        }
    }

    public static class DataDictionaryManagerExtend
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GetName(this Dictionary<Guid, DataDictionaryItem> dict, Guid id)
        {
            return dict.ContainsKey(id) ? dict[id].Name : string.Empty;
        }
    }
}
