/*
 * 文件上传模块
 * 作者：郭明
 * 日期：2016年10月9日
 */
define(["module-services-apiUtil"], function (apiUtil) {
 

    //获取文件类型，根据文件扩展
    var getFileTypeByExt = function (obj) {
        var picExts = ".png|.jpg|.gif|.bmp|.jpeg";
        var photoExt = obj.value.substr(obj.value.lastIndexOf(".") + 1).toLowerCase();//获得文件后缀名
        var pos = picExts.indexOf(photoExt);
        if (pos < 0) {
            return "File";
        }
        else {
            return "Image";
        }

    }

    //获取文件类型（根据文件类型）
    var getFileTypeByContentType = function (file) {
        var type = file.type.substr(0, 5);

        //图片
        if (type == "image") {
            return "Image"
        }
            //音频
        else if (type == "audio") {
            return "Audio"
        }
            //音频
        else if (type == "video") {
            return "Video"
        }
            //文件
        else
            return "File";

    }

    /*
     * 上传文件
     * params:
     *      @file 需要上传的文件
     *      @fileType 文件类型
     *      @okCallback   上传成功回调函数
     *      @failCallback 上传失败回调函数
     */
    var uploadFile = function (file, fileType, okCallback, failCallback) {



        xhr = new XMLHttpRequest();
        xhr.open("post", apiUtil.webStoreUrl + "/Upload", true);
        xhr.onerror = function (err) {
            console.error("upload error")

            if (failCallback)
                failCallback(err)

        }
        xhr.ontimeout = function (e) { console.error("upload timeout") };
        xhr.upload.onprogress = function (e) { console.debug("upload progress") };
        xhr.onload = function () {

            if (this.readyState === 4 && this.status === 200) {

                if (this.responseText) {
                    var response = eval("(" + this.responseText + ")");

                    if (response.Status == 0) {

                        console.log("upload success")

                        if (okCallback)
                            okCallback(response)

                    }
                    else {
                        console.log("upload fail")

                        if (failCallback)
                            failCallback(response)
                    }
                }
            }
            else {
                if (failCallback)
                    failCallback()
            }
        }

        var fd = new FormData();
        fd.append('mypic', file);
        xhr.send(fd);
    }

    /*
    * 触发文件上传
    * params:
    *   @file 需要上传的文件
    *   @preHandlerCallback 预处理回调函数
    */
    var onFileUpload = function (file, preHandlerCallback) {
        if (!window.File || !window.FileList || !window.FileReader) {
            alert("您的浏览器不支持File Api");
            return;
        }

        //预览图片
        var reader = new FileReader();

        reader.onload = (function (file) {


            //获取文件大小
            var fileSize = file.size;

            var fileType = getFileTypeByContentType(file);

            console.debug("upload file", file);

            console.info('fileSize: ' + fileSize);

            console.info('fileType: ' + fileType);

            return function (e) {

                if (preHandlerCallback) {

                    preHandlerCallback({
                        file: file,
                        fileType: fileType,
                        reader: this
                    }, function (okCallback, failCallback) {

                        uploadFile(file, fileType, okCallback, failCallback);

                    });

                }
                else {
                    console.error("not Implement onUpload Function");
                }
            };

        })(file);

        //预览图片
        reader.readAsDataURL(file);

    }


    return {
        uploadFile: uploadFile,
        onFileUpload: onFileUpload,
    };

})