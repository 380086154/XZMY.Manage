using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XZMY.Manage.WindowsService
{
    public class LogDto
    {
        public LogDto()
        {
        }

        public LogDto(string fileName, string description, string creatorIPv4, string creatorHostName)
        {
            Id = Guid.NewGuid();
            FileName = fileName;
            TypeName = Type.正常;
            Description = description;
            CreatorIPv4 = creatorIPv4;
            CreatorHostName = creatorHostName;
            CreatedTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public Guid Id { get; set; }
        public string FileName { get; set; }
        public Type TypeName { get; set; }
        public string Description { get; set; }
        public string CreatorIPv4 { get; set; }
        public string CreatorHostName { get; set; }
        public string CreatedTime { get; set; }
    }

    public enum Type
    {
        正常 = 0,
        异常 = 1
    }
}
