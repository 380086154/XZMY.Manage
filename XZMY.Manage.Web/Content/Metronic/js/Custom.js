/*
用于将form表单的控件对象序列化为对象，方便列表查询时不用手动拼接条件
调用例子：$("#form1").serializeObject();
*/
$.fn.serializeObject = function () {
    var o = {};
    var a = this.serializeArray();
    $.each(a, function () {
        if (o[this.name]) {
            if (!o[this.name].push) {
                o[this.name] = [o[this.name]];
            }
            o[this.name].push(this.value || '');
        } else {
            o[this.name] = this.value || '';
        }
    });
    return o;
};

// 对Date的扩展，将 Date 转化为指定格式的String
// 月(M)、日(d)、小时(h)、分(m)、秒(s)、季度(q) 可以用 1-2 个占位符， 
// 年(y)可以用 1-4 个占位符，毫秒(S)只能用 1 个占位符(是 1-3 位的数字) 
// 例子： 
// (new Date()).Format("yyyy-MM-dd hh:mm:ss.S") ==> 2006-07-02 08:09:04.423 
// (new Date()).Format("yyyy-M-d h:m:s.S")      ==> 2006-7-2 8:9:4.18 
Date.prototype.Format = function (fmt) { //author: meizz 
    var o = {
        "M+": this.getMonth() + 1, //月份 
        "d+": this.getDate(), //日 
        "h+": this.getHours(), //小时 
        "m+": this.getMinutes(), //分 
        "s+": this.getSeconds(), //秒 
        "q+": Math.floor((this.getMonth() + 3) / 3), //季度 
        "S": this.getMilliseconds() //毫秒 
    };
    if (/(y+)/.test(fmt)) fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o)
        if (new RegExp("(" + k + ")").test(fmt)) fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
    return fmt;
};

// select 控件绑定数据
/*
*data:数据源
*id：目标元素
*selectValue:默认选中的option的Value
*/
var appendOption = function (data, id, selectValue) {
    $.each(data, function (i, item) {
        if (selectValue && item.Id == selectValue) {
            $('<option selected></option>').val(item.Id).text(item.Name).appendTo($(id));
        }
        else {
            $('<option></option>').val(item.Id).text(item.Name).appendTo($(id));
        }
    });
};

//bootstrap-table 显示行号
var RowNumber = function (value, row, index) {
    return index + 1;
};

//bootstrap-table 显示分店名称
var showBranchName = function (value, row, index) {

    if (value === '3389ca9f-57ec-44f1-a818-61370d61f553')
        return '华创';
    if (value === '949d7d00-7c85-4080-9ee3-9e65ccae575d')
        return '渝西';
    return '测试';
};

//bootstrap-table 日期格式化
var yyyy_mm_dd = function (value, row, index) {

    var reg = /\/Date\(([-]?\d+)\)\//gi;

    if (reg.test(value)) {
        var msec = value.toString().replace(reg, "$1");
        value = new Date(parseInt(msec)).Format('yyyy-MM-dd');
    }
    return value;
};

//bootstrap-table 日期格式化
var hh_mm_ss = function (value, row, index) {
    var reg = /\/Date\(([-]?\d+)\)\//gi;

    if (reg.test(value)) {
        var msec = value.toString().replace(reg, "$1");
        value = new Date(parseInt(msec)).Format('hh:mm:ss');
    }
    return value;
};

//bootstrap-table 显示数据截断
var cutOff = function (value, row, index) {
    var str = value;

    if (value.length > 10) {
        str = value.substr(0, 10) + '...';
    }
    return str;
};

//限制只能输入 数字
var inputInt = function (obj) {//限制只能输入Int
    obj.value = obj.value.replace(/[^\d]/g, '');

    //obj.val(obj.val().replace(/[^\d]/g, ''));
};

//限制只能输入 数字，可带小数
var inputFloat = function (obj) {//可输入Float，保留一位小数
    obj.value = obj.value.replace(/[^\d.]/g, "");  //清除“数字”和"."以外的字符
    obj.value = obj.value.replace(/^\./g, "");  //验证第一个字符是数字而不是"."
    obj.value = obj.value.replace(/\.{2,}/g, "."); //只保留第一个"."清除多余的"."
    obj.value = obj.value.replace(".", "$#$").replace(/\./g, "").replace("$#$", ".");

    if (obj.value.indexOf(".") == -1) return;

    var strValue = obj.value;//保留1位小数点
    var length = strValue.substring(obj.value.indexOf("."), obj.value.length).length;
    if (length > 3) obj.value = obj.value.substring(-1, obj.value.indexOf(".") + 3);
};

//bootstrap toast 浮动提示
var i = -1,
    toastCount = 0,
    $toastlast,
    getMessage = function () {
        var msgs = ['Hello, some notification sample goes here',
            'Did you like this one ? :)',
            'Totally Awesome!!!',
            'Yeah, this is the Metronic!',
            'Explore the power of App. '
            //'Explore the power of App. '
        ];
        i++;
        if (i === msgs.length) {
            i = 0;
        }

        return msgs[i];
    };

//显示浮动提示框
var showToast = function (type, title, message) {

    var toastType = ['success', 'info', 'warning', 'error'];
    //toastr[success]("Gnome & Growl type non-blocking notifications", "Toastr Notifications");
    if (type > 3) type = 1;
    if (!message) message = getMessage();

    toastr[toastType[type]](message, title);

    //toastr.options = {
    //    closeButton: true,
    //    debug: true,
    //    positionClass: 'toast-top-right',
    //    onclick: null
    //};

    //toastr.options = {
    //    "closeButton": true,
    //    "debug": false,
    //    "positionClass": "toast-top-right",
    //    "onclick": null,
    //    "showDuration": "1000",
    //    "hideDuration": "1000",
    //    "timeOut": "5000",
    //    "extendedTimeOut": "1000",
    //    "showEasing": "swing",
    //    "hideEasing": "linear",
    //    "showMethod": "fadeIn",
    //    "hideMethod": "fadeOut"
    //};
};


var postQueryParams = function (params) {

    //console.log(JSON.stringify(params));
    // {"limit":10,"offset":0,"order":"asc","your_param1":1,"your_param2":2}
    return params; // body data
};