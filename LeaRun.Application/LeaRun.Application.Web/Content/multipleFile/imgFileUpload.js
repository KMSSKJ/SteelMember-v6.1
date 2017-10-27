
/// -------------------------
/// 图片文件上传
/// created by chenbing 2016-05-16
/// -------------------------

var imgFileUpload = (function () {

    var $This = [];
    var ImgType = "";
    var URL = "";

    // 构造方法
    function imgFileUpload($this, imgType, url) {
        $This = $this;
        ImgType = imgType;
        URL = url;

        _bindUploadEvent();
    };

    // 注册上传控件事件
    function _bindUploadEvent() {

        $.each($This, function (i, item) {
            $(item).find("i.button").click(function () {
                $(item).find("input").click();
            });
        });
    }

    // 上传文件控件change
    var fileUploadChange = function (fileControl) {

        var file = fileControl.files[0];

        var reader = new FileReader();
        reader.onload = function (evt) {
            var $par = $(fileControl).parent();
            // 执行上传
            _uploadImage($par);
        }
        reader.readAsDataURL(file);
    }

    // 上传文件
    function _uploadImage($box) {
        var files = $box.find("[type=file]");

        $(files).each(function (index, item) {
            if (item.files.length) {
                $.ajaxFileUpload({
                    url: URL,
                    secureuri: false,
                    fileUpload: item,
                    dataType: 'json',
                    data: { "type": ImgType },
                    success: function (data, status) {
                        var str = $(data).text();
                        var result = JSON.parse(str);
                        if (result.FullFileName != "" && result.FullFileName != null && result.FullFileName != undefined) {
                            var html = "";
                            //$.each(result, function (i, dat) {
                            html += "<i class=\"list\"><img src=\"" + result.ImgUrl + "\" sname=\"" + result.FullFileName + "\" /><i onclick=\"imgFileUpload.deletedImg(this)\">删除</i></i>";
                            //});
                            $box.find("div.imgShow").append(html);
                        } else {
                            alert(result);
                        }
                    }
                });
            }
        });
    }

    // 图片删除事件
    function imgDeleted($i) {
        $($i).parent().remove();
    }

    // 获取已上传图片名称组合串
    function getImgNameStr() {
        debugger
        var result = "";

        var $img = $("div.houseImgUpload i.list img");
        $.each($img, function (i, item) {
            if (i == $img.length - 1) {
                result += $(item).attr("sname");
            } else {
                result += $(item).attr("sname") + ",";
            }
        });

        return result;
    }


    return {
        init: function ($this, imgType, url) {
            imgFileUpload($this, imgType, url);
        },
        fileUploadChange: function ($controller) {
            fileUploadChange($controller);
        },
        deletedImg: function ($i) {
            imgDeleted($i);
        },
        getImgNameStr: function () {
            return getImgNameStr();
        }
    };

})();

