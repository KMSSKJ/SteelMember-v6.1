using LeaRun.Application.Entity.SteelMember;
using LeaRun.Application.Busines.SteelMember;
using LeaRun.Util;
using LeaRun.Util.WebControl;
using System.Web.Mvc;
using LeaRun.Application.Code;
using LeaRun.Util.Extension;
using System.Collections.Generic;
using System.Linq;
using LeaRun.Application.Web.Areas.SteelMember.Models;
using System;
using ThoughtWorks.QRCode.Codec;
using System.Drawing;
using System.IO;
using ThoughtWorks.QRCode.Codec.Data;
using LeaRun.Application.Busines.SystemManage;

namespace LeaRun.Application.Web.Areas.SteelMember.Controllers
{
    public class ProductionProcessController : MvcControllerBase
    {
        //
        // GET: /SteelMember/ProductionProcess/
        #region 视图功能
        /// <summary>
        /// 列表页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 列表页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index1()
        {
            return View();
        }

        /// <summary>
        /// 表单页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Form()
        {
            return View();
        }
        /// <summary>
        /// 领取订单页面
        /// </summary>
        /// <returns></returns>
        public ActionResult ReceiveOrderIndex()
        {
            return View();
        }
        /// <summary>
        /// 领取材料页面
        /// </summary>
        /// <returns></returns>
        public ActionResult ReceiveRawMaterialIndex()
        {
            ViewBag.Numbering = "CLLYD" + DateTime.Now.ToString("yyyyMMddhhmmssff");
            //ViewBag.CollarNumbering= "CLCKD" + DateTime.Now.ToString("yyyyMMddhhmmssff");
            ViewBag.CreateTime = DateTime.Now;
            ViewBag.CreateMan = OperatorProvider.Provider.Current().UserName;
            return View();
        }
        /// <summary>
        /// 生产填报
        /// </summary>
        /// <returns></returns>
        public ActionResult ProductionNumberForm()
        {
            return View();
        }
        /// <summary>
        /// 自检填报
        /// </summary>
        /// <returns></returns>
        public ActionResult SelfDetectForm()
        {
            return View();
        }
        /// <summary>
        /// 监理质检填报
        /// </summary>
        /// <returns></returns>
        public ActionResult QualityInspectionForm()
        {
            return View();
        }

        /// <summary>
        /// 拒绝生产原因填报
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult NotConfirmOrder(string keyValue)
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult MemberSummary()
        {
            return View();
        }

        #endregion

        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public ActionResult GetListJson(string queryJson)
        {
            var data = ""; //memberprocessbll.GetList(queryJson);
            return ToJsonResult(data);
        }
        /// <summary>
        /// 获取实体 
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = "";//memberprocessbll.GetEntity(keyValue);
            return ToJsonResult(data);
        }



        /// <summary>
        /// 获取实体 
        /// </summary>
        /// <param name="OrderId">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult GridListJsonRawMaterial(string OrderId)
        {
            var RawMaterial = new List<RawMaterialNumberModel>();
            var data = new MemberProductionOrderEntity();
            if (OrderId != null)
            {
                string[] array = OrderId.Split(',');
                if (array.Count() > 0)
                {
                    foreach (var item0 in array)
                    {
                        data = memberproductionorderbll.GetEntity(item0);
                        var orderinfo = memberproductionorderinfobll.GetList(f => f.OrderId == item0);
                        if (orderinfo.Count > 0)
                        {
                            foreach (var item in orderinfo)
                            {
                                var membermaterial = membermaterialbll.GetList(item.MemberId);
                                if (membermaterial.Count() > 0)
                                {
                                    foreach (var item1 in membermaterial)
                                    {
                                        var rawmaterial = rawmateriallibrarybll.GetEntity(item1.RawMaterialId);

                                        if (rawmaterial != null)
                                        {
                                            //var RawMaterialNumber = new RawMaterialNumberModel()
                                            //{
                                            //    Category = dataitemdetailbll.GetEntity(rawmaterial.Category).ItemName,
                                            //    RawMaterialId = rawmaterial.RawMaterialId,
                                            //    InventoryId = rawmaterialinventorybll.GetEntity(f => f.RawMaterialId == rawmaterial.RawMaterialId).InventoryId,
                                            //    //RawMaterialAnalysisId = item,
                                            //    RawMaterialModel = rawmaterial.RawMaterialModel,
                                            //    RawMaterialName = rawmaterial.RawMaterialName,
                                            //    Description = rawmaterial.Description,
                                            //    UnitId = dataitemdetailbll.GetEntity(rawmaterial.Unit).ItemName
                                            //};
                                            var RawMaterialNumber = new RawMaterialNumberModel();

                                            RawMaterialNumber.Category = dataitemdetailbll.GetEntity(rawmaterial.Category).ItemName;
                                            RawMaterialNumber.RawMaterialId = rawmaterial.RawMaterialId;
                                            RawMaterialNumber.InventoryId = rawmaterialinventorybll.GetEntity(f => f.RawMaterialId == rawmaterial.RawMaterialId).InventoryId;
                                            RawMaterialNumber.RawMaterialModel = rawmaterial.RawMaterialModel;
                                            RawMaterialNumber.RawMaterialName = rawmaterial.RawMaterialName;
                                            RawMaterialNumber.Description = rawmaterial.Description;
                                            RawMaterialNumber.UnitId = dataitemdetailbll.GetEntity(rawmaterial.Unit).ItemName;
                                            

                                            if (RawMaterial.Count() > 0)
                                            {
                                                var a = RawMaterial.Where(w => w.RawMaterialId == RawMaterialNumber.RawMaterialId).SingleOrDefault();
                                                if (a != null)
                                                {
                                                    RawMaterial.Where(r => r.RawMaterialId == a.RawMaterialId).Single().RawMaterialNumber += item1.RawMaterialNumber * item.ProductionQuantity;
                                                }
                                                else
                                                {
                                                    RawMaterialNumber.RawMaterialNumber = item1.RawMaterialNumber * item.ProductionQuantity;
                                                    RawMaterial.Add(RawMaterialNumber);
                                                }

                                            }
                                            else
                                            {
                                                RawMaterialNumber.RawMaterialNumber = item1.RawMaterialNumber * item.ProductionQuantity;
                                                RawMaterial.Add(RawMaterialNumber);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            var jsonData = new
            {
                entity = data,
                childEntity = RawMaterial
            };

            return ToJsonResult(jsonData);
        }

        [HttpGet]
        public ActionResult GetPageListSummaryJson(Pagination pagination, string queryJson)
        {
            var watch = CommonHelper.TimerStart();
            var MemberList = new List<MemberDemandModel>();
            var data0 = memberproductionorderbll.GetPageList(pagination, queryJson).OrderByDescending(O => O.Priority).ToList();
            if (data0.Count() > 0)
            {
                foreach (var item in data0)
                {
                    var data = memberproductionorderbll.GetDetails(item.OrderId).ToList();
                    for (int i = 0; i < data.Count(); i++)
                    {
                        var MemberLibrary = memberlibrarybll.GetEntity(data[i].MemberId);
                        var memberproductionorderinfo = memberproductionorderinfobll.GetEntity(data[i].InfoId);
                        var Member = new MemberDemandModel()
                        {
                            MemberId = data[i].MemberId,
                            MemberNumber = data[i].ProductionQuantity,
                            QualityInspectionNumber = data[i].QualityInspectionNumber,
                            ProductionNumber = data[i].ProductionQuantity,
                            MemberName = MemberLibrary.MemberName,
                            Category = dataitemdetailbll.GetEntity(MemberLibrary.Category).ItemName,
                            MemberNumbering = MemberLibrary.MemberNumbering,
                            UnitId = dataitemdetailbll.GetEntity(MemberLibrary.UnitId).ItemName,
                            CreateTime = item.CreateTime
                        };

                        if (MemberList.Count() > 0)
                        {
                            var a = MemberList.Where(w => w.MemberId == data[i].MemberId).SingleOrDefault();
                            if (a != null)
                            {
                                a.QualityInspectionNumber += data[i].QualityInspectionNumber;
                                a.ProductionNumber += data[i].ProductionQuantity;
                            }
                            else
                            {
                                MemberList.Add(Member);
                            }
                        }
                        else
                        {
                            MemberList.Add(Member);
                        }

                    }
                }
            }


            var queryParam = queryJson.ToJObject();
            if (!queryParam["Category"].IsEmpty())
            {
                string Category = queryParam["Category"].ToString();
                MemberList = MemberList.FindAll(t => t.Category == Category);
            }

            var jsonData = new
            {
                rows = MemberList,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
                costtime = CommonHelper.TimerEnd(watch)
            };
            return ToJsonResult(jsonData);
        }

        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        //[HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult RemoveForm(string keyValue)
        {
            //memberprocessbll.RemoveForm(keyValue);
            return Success("删除成功。");
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(string keyValue, MemberProcessEntity entity)
        {
            //memberprocessbll.SaveForm(keyValue, entity);
            return Success("操作成功。");
        }

        /// <summary>
        /// 确认生产订单
        /// </summary>
        /// <param name="keyValues">要审核的数据的主键些0(默认)未提交；1提交</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult ConfirmOrder(string keyValues)
        {
            string[] ids = new string[] { };
            if (!string.IsNullOrEmpty(keyValues))
            {
                ids = keyValues.Split(',');
            }
            if (!ids.IsEmpty())
            {
                List<MemberProductionOrderEntity> list = new List<MemberProductionOrderEntity>();
                foreach (var item in ids)
                {
                    var model = memberproductionorderbll.GetEntity(item.Trim());
                    if (model != null)
                    {
                        model.EstimatedFinishTime = DateTime.Parse(model.CreateTime.ToString()).AddDays(Convert.ToInt32(ids.Count()));//起始时间加通话时长
                        model.IsConfirm = 1;
                        list.Add(model);
                    }
                }
                if (list.Count > 0)
                {
                    memberproductionorderbll.UpdataList(list);
                }
            }
            return Success("操作成功。");
        }

        /// <summary>
        /// 拒绝生产订单
        /// </summary>
        /// <param name="Entity"></param>
        /// <param name="keyValue">要审核的数据的主键些0(默认)未提交；1提交</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult NotConfirmOrder(MemberProductionOrderEntity Entity, string keyValue)
        {
            var entity = new MemberProductionOrderEntity()
            {
                IsSubmit = 0,
                //IsPassed=0,
                IsConfirm = 3,
            };
            memberproductionorderbll.SaveForm(keyValue, entity);
            return Success("操作成功。");
        }

        /// <summary>
        /// 领取材料
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult ReceiveRawMaterial(string keyValue)
        {
            //var entity = strEntity.ToObject<MemberProductionOrderEntity>();
            var entity = memberproductionorderbll.GetEntity(keyValue);
            entity.IsReceiveRawMaterial = 1;
            entity.ProductionStatus = 1;
            memberproductionorderbll.SaveForm(keyValue, entity);
            return Success("操作成功。");
        }

        /// <summary>
        /// 生产信息提交
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="strChildEntitys"></param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult ProductionedOrderNumber(string keyValue, string strChildEntitys)
        {
            List<MemberProductionOrderInfoEntity> childEntitys = strChildEntitys.ToList<MemberProductionOrderInfoEntity>();
            int a = 0, b = 0;
            if (childEntitys.Count > 0)
            {
                foreach (var item in childEntitys)
                {
                    var ProductionedOrderInfo = memberproductionorderinfobll.GetEntity(item.InfoId);
                    a += Convert.ToInt32(item.ProductionQuantity);
                    b += Convert.ToInt32(item.ProductionedQuantity) + Convert.ToInt32(ProductionedOrderInfo.QualifiedQuantity);
                    ProductionedOrderInfo.ProductionedQuantity = ProductionedOrderInfo.ProductionedQuantity.ToDecimal() + item.ProductionedQuantity.ToDecimal();
                    ProductionedOrderInfo.Description = item.Description;
                    memberproductionorderinfobll.SaveForm(item.InfoId, ProductionedOrderInfo);
                }
                var ProductionedOrder = memberproductionorderbll.GetEntity(keyValue);
                if (a == b)
                {
                    ProductionedOrder.ProductionStatus = 2;
                }
                else
                {
                    ProductionedOrder.ProductionStatus = 1;
                }
                memberproductionorderbll.SaveForm(keyValue, ProductionedOrder);
            }

            return Success("操作成功。");
        }

        /// <summary>
        /// 自检信息提交
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="strChildEntitys"></param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult SelfDetectNumber(string keyValue, string strChildEntitys)
        {
            List<MemberProductionOrderInfoEntity> childEntitys = strChildEntitys.ToList<MemberProductionOrderInfoEntity>();
            int a = 0, b = 0;
            if (childEntitys.Count > 0)
            {
                foreach (var item in childEntitys)
                {
                    var ProductionedOrderInfo = memberproductionorderinfobll.GetEntity(item.InfoId);
                    a += Convert.ToInt32(ProductionedOrderInfo.ProductionQuantity);
                    b += Convert.ToInt32(item.SelfDetectNumber);
                   
                    ProductionedOrderInfo.SelfDetectNumber = item.SelfDetectNumber;
                    //ProductionedOrderInfo.QualifiedQuantity = ProductionedOrderInfo.QualifiedQuantity.ToDecimal() + item.SelfDetectNumber.ToDecimal();
                    ProductionedOrderInfo.SelfDetectRemarks = item.SelfDetectRemarks;
                    memberproductionorderinfobll.SaveForm(item.InfoId, ProductionedOrderInfo);
                }
                var ProductionedOrder = memberproductionorderbll.GetEntity(keyValue);
                if (a == b)
                {
                    ProductionedOrder.SelfDetectStatus = 2;

                }
                else
                {
                    ProductionedOrder.SelfDetectStatus = 1;
                }
                memberproductionorderbll.SaveForm(keyValue, ProductionedOrder);
            }

            return Success("操作成功。");
        }
        /// <summary>
        /// 监理检测信息提交
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="strChildEntitys"></param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult QualityInspectionNumber(string keyValue, string strChildEntitys)
        {
            List<MemberProductionOrderInfoEntity> childEntitys = strChildEntitys.ToList<MemberProductionOrderInfoEntity>();
            var ProductionedOrder = memberproductionorderbll.GetEntity(keyValue);

            decimal a = 0, b = 0;
            if (childEntitys.Count > 0)
            {
                foreach (var item in childEntitys)
                {
                    var ProductionedOrderInfo = memberproductionorderinfobll.GetEntity(item.InfoId);
                    a += item.QualityInspectionNumber.ToDecimal();
                    b += ProductionedOrderInfo.ProductionQuantity.ToDecimal();
                   
                    ProductionedOrderInfo.QualityInspectionNumber = item.QualityInspectionNumber;
                    ProductionedOrderInfo.QualifiedQuantity = ProductionedOrderInfo.QualifiedQuantity.ToDecimal() + item.QualityInspectionNumber.ToDecimal();
                    ProductionedOrderInfo.QualityInspectionRemarks = item.QualityInspectionRemarks;
                    memberproductionorderinfobll.SaveForm(item.InfoId, ProductionedOrderInfo);

                    //修改需求中已生产合格量
                    var denmand = memberdemandbll.GetEntity(f => f.MemberId == item.MemberId && f.SubProjectId == ProductionedOrder.Category);
                    denmand.ProductionNumber = denmand.ProductionNumber.ToDecimal() + item.QualityInspectionNumber.ToDecimal();
                    memberdemandbll.SaveForm(denmand.MemberDemandId, denmand);
                    //end
                }

                if (a == b)
                {
                    ProductionedOrder.QualityInspectionStatus = 2;

                }
                else
                {
                    ProductionedOrder.QualityInspectionStatus = 1;
                }
                memberproductionorderbll.SaveForm(keyValue, ProductionedOrder);
            }

            return Success("操作成功。");
        }

        /// <summary>
        /// 构件打包
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult Package(string keyValue)
        {
            var ProductionedOrder = memberproductionorderbll.GetEntity(keyValue);
            if (ProductionedOrder.IsPackage == 1)
            {
                return Success("该订单已打包");
            }
            else
            {
                ProductionedOrder.IsPackage = 1;
            }
            memberproductionorderbll.SaveForm(keyValue, ProductionedOrder);
            return Success("操作成功。");
        }
        #endregion

        #region 二维码解析，生成，打印

        /// <summary>
        /// 表单
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        public ActionResult QRCodeForm()
        {
            return View();
        }

        public ActionResult SetQRCodeForm(string KeyValue, string OrderId, QRCodeModel EntityModel)
        {
            try
            {
                var Member = memberlibrarybll.GetEntity(KeyValue);
                var Ordor = memberproductionorderbll.GetEntity(OrderId);
                //var MemberDemend = memberdemandbll.Find(f => f.MemberId == Member.MemberID).SingleOrDefault();
                EntityModel.MemberName = Member.MemberName;
                EntityModel.MemberNumbering = Member.MemberNumbering;
                EntityModel.ProjectName = subprojectbll.GetEntity(Ordor.Category).FullName;
                return Content(EntityModel.ToJson());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        //生成二维码方法一
        private void CreateCode_Simple(string nr)
        {
            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            qrCodeEncoder.QRCodeScale = 4;
            qrCodeEncoder.QRCodeVersion = 8;
            qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
            //System.Drawing.Image image = qrCodeEncoder.Encode("4408810820 深圳－广州 小江");
            System.Drawing.Image image = qrCodeEncoder.Encode(nr);
            string filename = DateTime.Now.ToString("yyyymmddhhmmssfff").ToString() + ".jpg";
            string filepath = Server.MapPath(@"~\Upload") + "\\" + filename;
            System.IO.FileStream fs = new System.IO.FileStream(filepath, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Write);
            image.Save(fs, System.Drawing.Imaging.ImageFormat.Jpeg);

            fs.Close();
            image.Dispose();
            //二维码解码
            var codeDecoder = CodeDecoder(filepath);
        }

        /// <summary>
        /// 生成二维码
        /// </summary>
        /// <param name="strData">要生成的文字或者数字，支持中文。如： "4408810820 深圳－广州" 或者：4444444444</param>
        /// <param name="qrEncoding">三种尺寸：BYTE ，ALPHA_NUMERIC，NUMERIC</param>
        /// <param name="level">大小：L M Q H</param>
        /// <param name="version">版本：如 8</param>
        /// <param name="scale">比例：如 4</param>
        /// <returns></returns>
        public ActionResult CreateCode_Choose(string strData, string qrEncoding, string level, int version, int scale)
        {
            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            string encoding = qrEncoding;
            switch (encoding)
            {
                case "Byte":
                    qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
                    break;
                case "AlphaNumeric":
                    qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.ALPHA_NUMERIC;
                    break;
                case "Numeric":
                    qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.NUMERIC;
                    break;
                default:
                    qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
                    break;
            }

            qrCodeEncoder.QRCodeScale = scale;
            qrCodeEncoder.QRCodeVersion = version;
            switch (level)
            {
                case "L":
                    qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.L;
                    break;
                case "M":
                    qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
                    break;
                case "Q":
                    qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.Q;
                    break;
                default:
                    qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.H;
                    break;
            }
            //文字生成图片
            Image image = qrCodeEncoder.Encode(strData);
            string filename = DateTime.Now.ToString("yyyymmddhhmmssfff").ToString() + ".jpg";
            string virtualPath = this.Server.MapPath("~") + "Resource/Document/NetworkDisk/QRCode";
            string filepath = virtualPath + "/" + filename;
            //string filepath = Server.MapPath(@"~\Upload") + "\\" + filename;
            if (Directory.Exists(virtualPath))
            {
                Directory.Delete(virtualPath, true);//pdf路径
                Directory.CreateDirectory(virtualPath);
            }
            else
            {
                Directory.CreateDirectory(virtualPath);//如果文件夹不存在，则创建
            }
            //如果文件夹不存在，则创建
            //if (!Directory.Exists(filepath))
            //    Directory.CreateDirectory(filepath);
            System.IO.FileStream fs = new System.IO.FileStream(filepath, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Write);
            image.Save(fs, System.Drawing.Imaging.ImageFormat.Jpeg);
            fs.Close();
            image.Dispose();
            return Content("../../Resource/Document/NetworkDisk/QRCode/" + filename);
        }

        /// <summary>
        /// 二维码解码
        /// </summary>
        /// <param name="filePath">图片路径</param>
        /// <returns></returns>
        public string CodeDecoder(string filePath)
        {
            if (!System.IO.File.Exists(filePath))
                return null;
            Bitmap myBitmap = new Bitmap(Image.FromFile(filePath));
            QRCodeDecoder decoder = new QRCodeDecoder();
            string decodedString = decoder.decode(new QRCodeBitmapImage(myBitmap));
            return decodedString;
        }

        /// <summary>
        /// 打印当前页
        /// </summary>
        /// <returns></returns>
        public ActionResult PrintPage()
        {
            return View();
        }

        #endregion
    }
}
