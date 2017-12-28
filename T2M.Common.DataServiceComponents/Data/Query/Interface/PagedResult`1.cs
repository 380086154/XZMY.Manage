using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace T2M.Common.DataServiceComponents.Data.Query.Interface
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    [DataContract]
    public class PagedResult<T>
    {      
        #region Constructors

        /// <summary>
        /// ��ʼ��һ������<see>
        ///         <cref>PagedResult</cref>
        ///     </see>
        ///     ���󣬸ù��췽��ֻ�����������
        /// </summary>
        public PagedResult()
        {
            
        }

        /// <summary>
        /// ��ʼ��һ������<see>
        ///         <cref>PagedResult</cref>
        ///     </see>
        ///     ���󣬸ù��췽��ֻ�����������
        /// </summary>
        /// <param name="totalCount">������Ϣ����</param>
        /// <param name="totalPages">����ҳ��</param>
        /// <param name="pageIndex">��С��1��ҳ��</param>
        /// <param name="pageSize">��ҳ��ʾ����������</param>
        /// <param name="results">�����ҳ���ݼ��ķ���IList����</param>
        public PagedResult(Int32 totalCount, Int32 totalPages, Int32 pageIndex, Int32 pageSize, IList<T> results)
        {
            TotalCount = totalCount;
            TotalPages = totalPages;
            PageIndex = pageIndex;
            PageSize = pageSize;
            Results = results;
        }

        #endregion

        #region Properties

        /// <summary>
        /// ��ȡ������������Ϣ����
        /// </summary>
        [DataMember]
        public int TotalCount { get; set; }

        /// <summary>
        /// ��ȡ����������ҳ��
        /// </summary>
        [DataMember]
        public Int32 TotalPages { get; set; }

        /// <summary>
        /// ��ȡ������ҳ�룬ҳ�벻С��1
        /// </summary>
        [DataMember]
        public Int32 PageIndex { get; set; }

        /// <summary>
        /// ��ȡ�����÷�ҳ��ʾ����������
        /// </summary>
        [DataMember]
        public Int32 PageSize { get; set; }

        /// <summary>
        /// ��ȡ�����ñ����ҳ���ݼ��ķ���<see cref="System.Collections.IList"/>����
        /// </summary>
        [DataMember]
        public IList<T> Results { get; set; }

        #endregion

    }
}