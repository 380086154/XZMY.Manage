using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("BackupToEmail")]
[assembly: AssemblyDescription(

    "2017年8月7日 12:24:09       \r\n\t新增显示压缩文件总数。\r\n\r\n" +
    "2017年8月7日 12:00:50       \r\n\t新增自动判断盘符功能，因为开发机和部署电脑的安装目录不一致。\r\n\r\n" +
    "2017年8月2日 15:10:36       \r\n\t优化代码，新增删除因为异常发生而留下的文件夹，同时在邮件内容中新增版本号信息。\r\n\r\n" +
    "2017年7月24日 23:03:56      \r\n\t调整系统数据备份频率后，明显发现邮件多了起来，时间久了很有可能被邮件服务器拦截。所以将备份文件压缩发送备份文件，一次发送。\r\n\r\n" +
    "2017年7月15日 18:48:39      \r\n\t在华创店系统奔溃后：修正删除逻辑按照时间正序删除，每天只发送一次log文件，新增每小时检查一次备份文件并发送。\r\n\r\n" +
    "\r\n\r\n" +
    "\t该工具（服务）主要用于备份数据\r\n\r\n" +

    "\r\n\t用户须知：您必须同意以下所有条款才能使用本软件或任何本软件未来的更新。如果您不同意以下任一条款，请不要使用本软件或其任何更新。使用本软件即表明您同意以下所有条款。" +
    "\r\n\t免责声明：本软件为免费软件，允许用户随意传播，但不允许修改其源代码，作者不保证软件或其功能能够满足您的所有要求，也不保证软件操作不会发生任何中断、错误或所有缺陷均可得到更正。在任何情况下，对于使用该软件或无法使用该软件所造成的直接或间接损害，作者均不承担任何责任。"
)]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("BackupToEmail")]
[assembly: AssemblyCopyright("Copyright ©  2017")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("d4706bb4-550d-48b1-9920-9b3b22db653c")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.2.*")]
[assembly: AssemblyFileVersion("1.0.0.0")]
