using System;

namespace T2M.Common.Utils.Models
{
    /// <summary>
    /// 
    /// </summary>
    public interface IActorInfomationReader
    {
        ///// <summary>
        ///// ��ȡ�������¼�������Id��
        ///// </summary>
        //Guid CreatorId { get; set; }

        ///// <summary>
        ///// ��ȡ�������¼�������������
        ///// </summary>
        //String CreatorName { get; set; }

        /// <summary>
        /// ��ȡ�������¼�����ʱ�䡣
        /// </summary>
        DateTime CreatedTime { get; set; }

        ///// <summary>
        ///// ��ȡ�������¼�����޸���Id��
        ///// </summary>
        //Guid ModifierId { get; set; }

        ///// <summary>
        ///// ��ȡ�������¼�����޸���������
        ///// </summary>
        //String ModifierName { get; set; }

        ///// <summary>
        ///// ��ȡ�������¼�����޸�ʱ�䡣
        ///// </summary>
        //DateTime ModifiedTime { get; set; }
    }


}