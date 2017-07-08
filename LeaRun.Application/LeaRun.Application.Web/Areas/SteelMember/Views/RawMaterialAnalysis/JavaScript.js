(function ($) {
    "use strict";

    var parameter = {
        keyValue: false,
        baseInfo: { F_FrmId: "", F_FrmKind: "", F_FrmName: "" },
        moduleInfo: {}
    };

    $.formJs = {
        type: 0,
        initialType: function (type, isEdit) {
            parameter.baseInfo.F_FrmKind = type;
            switch (type) {
                case 0://自定义表单
                    $('#custmerForm').show();
                    $('#custmerForm textarea').height($(window).height() - 350);
                    //上级
                    $("#F_ParentId").comboBoxTree({
                        url: "../../AuthorizeManage/Module/GetTreeJson?target=expand",
                        description: "==请选择==",
                        maxHeight: "195px",
                        allowSearch: true
                    });
                    //绑定表单选择
                    $("#F_FrmId").comboBoxTree({
                        maxHeight: "190px",
                        url: "../../FormManage/FormModule/GetTreeJson",
                        param: { "queryJson": '{"F_FrmType2": 2}' },
                        dataItemName: "FormSort",
                        allowSearch: true,
                        click: $.formJs.event.selectForm
                    });
                    if (!!isEdit) {
                        $("#custmerForm").SetWebControls(parameter.moduleInfo);
                        $("#F_FrmId").comboBoxTreeSetValue(parameter.baseInfo.F_FrmId);
                    }
                    break;
                case 1://属性扩展
                    $('#attrExForm').show();
                    //所属功能
                    $("#F_ObjectId").comboBoxTree({
                        url: "../../AuthorizeManage/Module/GetTreeJson",
                        description: "==请选择==",
                        maxHeight: "195px",
                        allowSearch: true,
                        click: function (item) {
                            if (item.F_Target == "iframe") {
                                $(".tip_container").remove();
                                parameter.baseInfo.F_ObjectId = item.id;
                                parameter.baseInfo.F_ObjectName = item.text;
                                parameter.baseInfo.F_ObjectFormId = "";
                                $("#gridTable").jqGrid('setGridParam', {
                                    postData: { moduleId: item.id },
                                    url: "../../AuthorizeManage/ModuleButton/GetTreeListJson"
                                }).trigger('reloadGrid');
                            }
                            else {
                                learun.dialogTop({ msg: "请选择功能页面", type: "error" });
                                return "false";
                            }
                        }
                    });
                    //绑定表单选择
                    $("#F_FrmId2").comboBoxTree({
                        maxHeight: "190px",
                        url: "../../FormManage/FormModule/GetTreeJson",
                        param: { "queryJson": '{"F_FrmType2": 2}' },
                        dataItemName: "FormSort",
                        allowSearch: true,
                        click: $.formJs.event.selectForm
                    });
                    var $gridTable = $("#gridTable");
                    $gridTable.jqGrid({
                        datatype: "json",
                        height: 212,
                        postData: { moduleId: parameter.baseInfo.F_ObjectId },
                        url: "../../AuthorizeManage/ModuleButton/GetTreeListJson",
                        autowidth: true,
                        unwritten: false,
                        colModel: [
                            { label: "主键", name: "F_ModuleButtonId", hidden: true },
                            { label: "名称", name: "F_FullName", width: 140, align: "left", sortable: false },
                            { label: "编号", name: "F_EnCode", width: 140, align: "left", sortable: false },
                            { label: "地址", name: "F_ActionAddress", width: 400, align: "left", sortable: false },
                        ],
                        multiselect: true,
                        caption: "功能按钮",
                        rowNum: "1000",
                        rownumbers: true,
                        shrinkToFit: false,
                        gridview: true,
                        hidegrid: false,
                        loadComplete: function (data) {
                            if (parameter.baseInfo.F_ObjectFormId != undefined && parameter.baseInfo.F_ObjectFormId != null && parameter.baseInfo.F_ObjectFormId != "") {
                                $.each(data.rows, function (i, item) {
                                    if (parameter.baseInfo.F_ObjectFormId.indexOf(item.F_ModuleButtonId) != -1) {
                                        $gridTable.jqGrid('setSelection', i + 1);
                                    }
                                });
                            }
                        }
                    });
                    if (!!isEdit) {
                        $("#F_FrmId2").comboBoxTreeSetValue(parameter.baseInfo.F_FrmId);
                        $("#F_ObjectId").comboBoxTreeSetValue(parameter.baseInfo.F_ObjectId);

                        parameter.baseInfo.F_ObjectName = $('#F_FrmId2 .ui-select-text').text();
                    }
                    break;
            }
            $('#createPanel').hide();
        },
        loadData: function () {
            parameter.keyValue = learun.request('keyValue');
            //获取表单
            if (!!parameter.keyValue) {
                //获取表单
                learun.setForm({
                    url: "../../FormManage/FormModule/GetRelationEntityJson",
                    param: { keyValue: parameter.keyValue },
                    success: function (data) {
                        parameter.baseInfo = data.baseInfo;
                        parameter.moduleInfo = data.moduleInfo;
                        $.formJs.initialType(data.baseInfo.F_FrmKind, true);
                    }
                });
            }
        },
        bindEvent: function () {
            $('#btn_finish').unbind();
            $('.selectIcon').unbind();
            $('.previewBtn').unbind();

            $('#btn_finish').on('click', $.formJs.event.save);
            $('.selectIcon').on('click', $.formJs.event.selectIcon);
            $('.previewBtn').on('click', $.formJs.event.preview);
        },
        event: {
            save: function () {
                if (parameter.baseInfo.F_FrmKind == 0) {//自定义表单
                    if (!$('#custmerForm').Validform()) {
                        return false;
                    }
                    parameter.moduleInfo = $("#custmerForm").GetWebControls();
                    parameter.moduleInfo["F_Target"] = "iframe";
                    parameter.moduleInfo["F_UrlAddress"] = "/FormManage/FormModule/CustmerFormIndex";
                }
                else {
                    if (!$('#attrExForm').Validform()) {
                        return false;
                    }
                    parameter.baseInfo.F_ObjectFormId = $("#gridTable").jqGridRowValue("F_ModuleButtonId");
                    parameter.baseInfo.F_ObjectName += '/' + $("#gridTable").jqGridRowValue("F_FullName");
                }
                learun.saveForm({
                    url: "../../FormManage/FormModule/SaveRelation?keyValue=" + (parameter.keyValue ? parameter.keyValue : ""),
                    param: { baseInfo: JSON.stringify(parameter.baseInfo), moduleInfo: JSON.stringify(parameter.moduleInfo) },
                    loading: "正在保存数据...",
                    success: function () {
                        $.currentIframe().$("#gridTable").trigger("reloadGrid");
                    }
                });
            },
            preview: function () {
                if (parameter.baseInfo.F_FrmId != "") {
                    dialogOpen({
                        id: "previewForm",
                        title: '预览表单【' + parameter.baseInfo.F_FrmName + "】",
                        url: '/FormManage/FormModule/FormPreview?keyValue=' + parameter.baseInfo.F_FrmId,
                        width: "1100px",
                        height: "700px",
                        btn: false
                    });
                }
                else {
                    learun.dialogTop({ msg: "请选择表单", type: "error" });
                }
            },
            selectIcon: function () {
                dialogOpen({
                    id: "SelectIcon",
                    title: '选取图标',
                    url: '/AuthorizeManage/Module/Icon?ControlId=F_Icon',
                    width: "1000px",
                    height: "600px",
                    btn: false
                });
            },
            selectForm: function (item) {
                if (item.Sort == "form") {
                    $(".tip_container").remove();
                    parameter.baseInfo.F_FrmId = item.id;
                    parameter.baseInfo.F_FrmVersion = item.version;
                }
                else {
                    learun.dialogTop({ msg: "请选择表单而不是表单分类", type: "error" });
                    return "false";
                }
            }
        }
    };

    $(function () {
        $.formJs.loadData();
        $.formJs.bindEvent();
    });
})(window.jQuery);