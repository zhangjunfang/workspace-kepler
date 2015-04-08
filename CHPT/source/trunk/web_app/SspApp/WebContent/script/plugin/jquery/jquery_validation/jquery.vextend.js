/**
 * @description jquery-validation扩展验证方法
 *
 */
var validate_extend = {
    //电话号码
    phonenumber: function(value, element){
        var reg = "^(([0\\+]\\d{2,3}-)?(0\\d{2,3})-)?(\\d{7,8})(-(\\d{3,}))?$";
        var r = value.match(new RegExp(reg));
        if (r == null)
            return this.optional(element) || false;
        return this.optional(element) || true;
    },
    //电话号码带分机号
    phonenumberlong: function(value, element, param){
        var reg = "^(([0\+]\\d{2,3}-)?(0\\d{2,3})-)(\\d{7,8})(-(\\d{3,}))?$";
        var r = value.match(new RegExp(reg));
        if (r == null)
            return this.optional(element) || false;
        return this.optional(element) || true;
    },
    //邮编
    zipcode: function(value, element){
        var reg = "^\\d{6}$";
        var r = value.match(new RegExp(reg));
        if (r == null)
            return this.optional(element) || false;
        return this.optional(element) || true;
    },
    //固定电话--手机号
    mobilePhoneNum: function(value, element){
        return this.optional(element) || /(^(\d{3,4}-)?\d{7,8})$|(13[0-9]{9})/.test(value);
    },
    //手机号
    mobile: function(value, element){
        var reg = "^(13|15|18|16)[0-9]{9}$";
        var r = value.match(new RegExp(reg));
        if (r == null)
            return this.optional(element) || false;
        return this.optional(element) || true;
    },
    //email
    emailExtend: function(value, element){
        var reg = "^\\w+((-\\w+)|(\\.\\w+))*\\@[A-Za-z0-9]+((\\.|-)[A-Za-z0-9]+)*\\.[A-Za-z0-9]+$";
        var r = value.match(new RegExp(reg));
        if (r == null)
            return this.optional(element) || false;
        return this.optional(element) || true;
    },
    //判断身份证
    isidcardno: function(num, element){
    	var intStrLen = num.length;
    	var par1 = /^\d+(\.\d+)?$/;
    	var par3 = /^[A-Z0-9]$/;
        var idNumber = num;
        if (intStrLen != 18) {
            return this.optional(element) || false;
        } else {
        	var cardsub = num.substring(0, 17);
    		var cardsubl = num.substring(17, 18);
        	if (par1.test(cardsub)) {
        		return this.optional(element) || par3.test(cardsubl);
        	} else {
        		return this.optional(element) || false;
        	}
        }
        return this.optional(element) || true;
    },
    //特殊字符验证
    specialchars: function(value, element, param){
        return this.optional(element) || /^[\u4e00-\u9fa5\w]+$/.test(value);
    },
    //html关键字符验证
    htmlSpecialhars: function(value, element, param){
    	var reg = "[<|>]";
        var r = value.match(new RegExp(reg));
        if (r) return this.optional(element) || false;
        return this.optional(element) || true;

        
    }
};

// 字符验证
jQuery.validator.addMethod("specialchars", validate_extend.specialchars, "不能含特殊符号");
// 电话号码
jQuery.validator.addMethod("phonenumber", validate_extend.phonenumber, "无效电话号码");
// 电话邮编
jQuery.validator.addMethod("zipcode", validate_extend.zipcode, "无效邮编");
// 手机号码
jQuery.validator.addMethod("mobile", validate_extend.mobile, "无效手机号");
// 身份证号
jQuery.validator.addMethod("isidcardno", validate_extend.isidcardno, "无效身份证号");
//电子邮件地址
jQuery.validator.addMethod("emailExtend", validate_extend.emailExtend, "无效邮箱");

// 身份证号
jQuery.validator.addMethod("mobilePhoneNum", validate_extend.mobilePhoneNum, "无效联系电话");

//html关键字符验证
jQuery.validator.addMethod("htmlSpecialhars", validate_extend.htmlSpecialhars, "不能含特殊符号");

// 验证汉字
jQuery.validator.addMethod("noChinese", function(value, element){
    var noChinese = /^[^\u4E00-\u9FA5\uF900-\uFA2D]+$/;///^-?\d+(\.\d{1,2})?$/;///[\u4E00-\u9FA5\uF900-\uFA2D]/
    return this.optional(element) || (noChinese.test(value));
}, "不能包含汉字");
// 验证非负整数
jQuery.validator.addMethod("intNumber", function(value, element){
    var intNumber = /^\d+$/;
    return this.optional(element) || (intNumber.test(value));
}, "请输入正确数字");
// 不能含特殊符号，需要支持输入：“.”（点号）和“-”（短横杠）；
jQuery.validator.addMethod("specialcharsForTerminal", function(value, element){
    var specialcharsForTerminal = /^[\u4e00-\u9fa5\w\.\-]+$/;
    return this.optional(element) || (specialcharsForTerminal.test(value));
}, "不含特殊符号除(. -)");
// 验证字母或者数字组合
jQuery.validator.addMethod("letterUOrNumber", function(value, element) {
	var letterUOrNumber = /^[A-Z0-9]+$/;///^-?\d+(\.\d{1,2})?$/;///[\u4E00-\u9FA5\uF900-\uFA2D]/
	return this.optional(element) || (letterUOrNumber.test(value));},"请输入大写字母或数字的组合");