using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


/// <summary>
/// 图片分类
/// </summary>
public enum ImageCategory
{
    /// <summary>
    /// 无
    /// </summary>
    None = 0,
    /// <summary>
    /// banner
    /// </summary>
    Banner = 1,
    /// <summary>
    /// 新闻
    /// </summary>
    Article = 2,
    /// <summary>
    /// 商品
    /// </summary>
    Product = 3,
}

/// <summary>
/// 生成缩略图类型
/// </summary>
public enum ThumbnailType
{
    /// <summary>
    /// 指定高宽缩放(可能变形)
    /// </summary>
    WidthHeight = 0,
    /// <summary>
    /// 指定宽，高按比例(不变形)
    /// </summary>
    Width = 1,
    /// <summary>
    /// 指定高，宽按比例(不变形)
    /// </summary>
    Height = 2,
    /// <summary>
    /// 指定高宽裁减(不变形)
    /// </summary>
    WidthHeightCut = 3,
    /// <summary>
    /// 指定高宽范围(不变形)
    /// </summary>
    WidthHeightAuto = 4,
}

/// <summary>
/// 水印类型
/// </summary>
public enum WatermarkType
{
    /// <summary>
    /// 文字
    /// </summary>
    Text = 0,
    /// <summary>
    /// 图片
    /// </summary>
    Image = 1
}

/// <summary>
/// 水印定位
/// </summary>
public enum WatermarkPosition
{
    /// <summary>
    /// 左上角
    /// </summary>
    TopLeft = 1,
    /// <summary>
    /// 中上
    /// </summary>
    TopCenter = 2,
    /// <summary>
    /// 右上角
    /// </summary>
    TopRight = 3,
    /// <summary>
    /// 左中
    /// </summary>
    CenterLeft = 4,
    /// <summary>
    /// 中心
    /// </summary>
    Center = 5,
    /// <summary>
    /// 右中
    /// </summary>
    CenterRigth = 6,
    /// <summary>
    /// 左下角
    /// </summary>
    BottomLeft = 7,
    /// <summary>
    /// 中下
    /// </summary>
    BottomCenter = 8,
    /// <summary>
    /// 右下角
    /// </summary>
    BottomRight = 9,
}
/// <summary>
/// 返回状态
/// </summary>
public enum ResultCode
{
    /// <summary>
    /// 失败0
    /// </summary>
    Fail = 0,
    /// <summary>
    /// 成功1
    /// </summary>
    Success = 1,


}
