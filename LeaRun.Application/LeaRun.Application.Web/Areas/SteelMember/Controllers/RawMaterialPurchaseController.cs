using LeaRun.Application.Entity.SteelMember;
using LeaRun.Application.Busines.SteelMember;
using LeaRun.Util;
using LeaRun.Util.WebControl;
using System.Collections.Generic;
using System.Web.Mvc;
using LeaRun.Application.Web.Areas.SteelMember.Models;
using System.Linq;
using LeaRun.Util.Extension;
using System;
using LeaRun.Application.Code;

namespace LeaRun.Application.Web.Areas.SteelMember.Controllers
{
    /// <summary>
    /// �� �� 6.1
    /// �� �ڣ�2017-07-08 11:58
    /// �� ����ԭ���ϲɹ�����
    /// </summary>
    public class RawMaterialPurchaseController : MvcControllerBase
    {
        private RawMaterialPurchaseBLL rawmaterialpurchasebll = new RawMaterialPurchaseBLL();
        private RawMaterialAnalysisBLL rawmaterialanalysisbll = new RawMaterialAnalysisBLL();
        private RawMaterialLibraryBLL rawmateriallibrarybll = new RawMaterialLibraryBLL();
        private RawMaterialInventoryBLL rawmaterialinventorybll = new RawMaterialInventoryBLL();

      
        #region ��ͼ����
        /// <summary>
        /// �б�ҳ��
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ItemList()
        {
            return View();
        }
        /// <summary>
        /// ��ҳ��
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Form(string keyValue)
        {
            if (keyValue == "" || keyValue == null)
            {
                ViewBag.PurchaseNumbering = "CGDD" + DateTime.Now.ToString("yyyyMMddhhmmssff");
                ViewBag.CreateMan = OperatorProvider.Provider.Current().UserName;
            }
            return View();
        }

        [HttpGet]
        public ActionResult DetailForm()
        {
            return View();
        }
        public ActionResult EditForm() {
            return View();
        }
        public ActionResult Purchase()
        {
            return View();
        }
        #endregion

        #region ��ȡ����
        /// <summary>
        /// ��ȡ��������
        /// </summary>
        /// <returns></returns>
        public ActionResult DetailRawMaterialPurchase(string rawMaterialPurchaseId)
        {
            var entityrawMaterialPurchaseId = rawmaterialpurchasebll.GetEntity(rawMaterialPurchaseId);
            //entityrawMaterialPurchaseId.CreateTime; 
            var data = ToJsonResult(entityrawMaterialPurchaseId);
            return data;
        }
        /// <summary>
        /// ��ȡ������children����
        /// </summary>
        /// <returns></returns>
        public ActionResult DetailRawMaterialPurchaseInfo(string rawMaterialPurchaseId)
        {
            List<Text> list = new List<Text>();
            var listrawmaterialpurchaseInfo = rawmaterialpurchasebll.GetList(p => p.RawMaterialPurchaseId == rawMaterialPurchaseId);
            if (listrawmaterialpurchaseInfo.Count > 0)
            {
                for (int i = 0; i < listrawmaterialpurchaseInfo.Count; i++)
                {
                    var SuggestQuantity = listrawmaterialpurchaseInfo[i].PurchaseQuantity;
                    var entityrawmaterialanalysis = rawmaterialanalysisbll.GetEntity(listrawmaterialpurchaseInfo[i].RawMaterialAnalysisId);
                    var Entitymateriallibrary = rawmateriallibrarybll.GetEntity(entityrawmaterialanalysis.RawMaterialId);
                    Text projectdemand = new Text();
                    projectdemand.PurchaseQuantity =entityrawmaterialanalysis.RawMaterialDosage.ToString();//��������

                    projectdemand.InfoId=listrawmaterialpurchaseInfo[i].InfoId;
                    projectdemand.SuggestQuantity = SuggestQuantity;
                    projectdemand.RawMaterialAnalysisId = entityrawmaterialanalysis.Id;   //����ID                                                                      // // projectdemand.RawMaterialAnalysisId = arrayAnalysisId[i];
                    projectdemand.RawMaterialPurchaseId = rawMaterialPurchaseId;
                    projectdemand.RawMaterialName = Entitymateriallibrary.Category;
                    projectdemand.RawMaterialModel = Entitymateriallibrary.RawMaterialModel;
                    projectdemand.UnitName = Entitymateriallibrary.Unit; 
                    projectdemand.Description = entityrawmaterialanalysis.Description;
                    projectdemand.Price = listrawmaterialpurchaseInfo[i].Price==null?0:listrawmaterialpurchaseInfo[i].Price;
                    //projectdemand.   
                    projectdemand.RawMaterialSupplier = listrawmaterialpurchaseInfo[i].RawMaterialSupplier;
                    list.Add(projectdemand);
                }
                return ToJsonResult(list);

            }
            return ToJsonResult(list);
        }
        /// <summary>
        /// ��ȡ�����������ͨ���Ĳ���  IsPassed=1��ʾͨ��
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public ActionResult GetListByIsPassed(string category)
        {
            var list = rawmaterialanalysisbll.GetList(p => p.IsPassed == 1 && p.Category == category);
            var data = new List<RawMaterialLibraryModel>();

            if (list.Count > 0)
            {
                foreach (var item in list)
                {
                    var _data = new RawMaterialLibraryEntityBill();
                    var rawMaterialDosage = item.RawMaterialDosage;
                    var model = rawmateriallibrarybll.GetEntity(item.RawMaterialId);
                    _data.RawMaterialId = model.RawMaterialId;
                    _data.RawMaterialDosage = rawMaterialDosage.ToString();
                    _data.AnalysisId = item.Id;
                    _data.RawMaterialName = model.RawMaterialName;
                    _data.RawMaterialModel = model.RawMaterialModel;
                    _data.UnitName = model.Unit;
                    _data.Description = model.Description;
                    data.Add(_data);
                }
            }
            return ToJsonResult(data);
        }
        public ActionResult ListMember(string AnalysisId, string RawMaterialId, string RawMaterialName,
            string RawMaterialModel, string RawMaterialStandard, string RawMaterialDosage, string UnitName, string Description)
        {
           // var inventory = 0; //�����
            var listmember = new List<Text>();
            if (AnalysisId != null && AnalysisId != "")
            {
                string[] arrayAnalysisId = AnalysisId.Split(',');
                string[] arrayRawMaterialId = RawMaterialId.Split(',');
                string[] arrayRawMaterialName = RawMaterialName.Split(',');
                string[] arrayRawMaterialModel = RawMaterialModel.Split(',');
                string[] arrayRawMaterialStandard = RawMaterialStandard.Split(',');
                string[] arrayRawMaterialDosage = RawMaterialDosage.Split(',');
                string[] arrayUnitName = UnitName.Split(',');
                string[] arrayDescription = Description.Split(',');
                if (arrayAnalysisId.Count() > 0)
                {
                    if (arrayAnalysisId != null)
                        for (int i = 0; i < arrayAnalysisId.Length; i++)
                        {
                            var sb = arrayRawMaterialId[i];
                            //var rawmaterialinventory=rawmaterialinventorybll.GetEntity(arrayRawMaterialId[i].ToString());
                            var rawmaterialinventory = rawmaterialinventorybll.GetEntityByRawMaterialId(arrayRawMaterialId[i].ToString());
                            var inventory = Convert.ToInt32(rawmaterialinventory.Quantity); ; //�����
                            Text projectdemand = new Text();

                            projectdemand.PurchaseQuantity = arrayRawMaterialDosage[i];

                            projectdemand.Inventory = inventory;

                            projectdemand.SuggestQuantity = int.Parse(arrayRawMaterialDosage[i]) - inventory;//����ɹ���=������-�����
                            projectdemand.RawMaterialAnalysisId = arrayAnalysisId[i];
                            projectdemand.RawMaterialPurchaseId = arrayRawMaterialId[i];
                            projectdemand.RawMaterialName = arrayRawMaterialName[i];
                            projectdemand.RawMaterialModel = arrayRawMaterialModel[i];
                            projectdemand.RawMaterialStandard = arrayRawMaterialStandard[i];
                            projectdemand.UnitName = arrayUnitName[i];
                            projectdemand.Description = arrayDescription[i];
                            listmember.Add(projectdemand);
                        }
                }
            }
            else
            {
                return Json(listmember);
            }
            return Json(listmember);
        }
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination">��ҳ����</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�Json</returns>
        [HttpGet]
        public ActionResult GetPageListJson(Pagination pagination, string queryJson)
        {
           var watch = CommonHelper.TimerStart();
            var data = rawmaterialpurchasebll.GetPageList(pagination, queryJson);
            var jsonData = new
            {
                rows = data,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
                costtime = CommonHelper.TimerEnd(watch)
            };
            return ToJsonResult(jsonData);

        }
        /// <summary>
        /// ��ȡʵ�� 
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns>���ض���Json</returns>
        [HttpGet]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = rawmaterialpurchasebll.GetEntity(keyValue);
            var childData = rawmaterialpurchasebll.GetDetails(keyValue).ToList();
            List<RawMaterialPurchaseModel> list = new List<RawMaterialPurchaseModel>();
            if (childData.Count > 0)
            {
                foreach (var item in childData)
                {
                    RawMaterialPurchaseModel rawMaterialPurchaseModel = new RawMaterialPurchaseModel();
                    var purchaseQuantity = item.PurchaseQuantity;//ʵ�ʹ�����
                    var rawMaterialAnalysisId = item.RawMaterialAnalysisId;
                    var entityrawmaterialanalysis = rawmaterialanalysisbll.GetEntity(item.RawMaterialAnalysisId);
                    var entityrawmateriallibrary = rawmateriallibrarybll.GetEntity(entityrawmaterialanalysis.RawMaterialId);
                    var entityrawmaterialinventory = rawmaterialinventorybll.GetList(null).ToList().Find(f=>f.RawMaterialId== entityrawmaterialanalysis.RawMaterialId);

                    rawMaterialPurchaseModel.RawMaterialPurchaseId = item.RawMaterialPurchaseId;
                    rawMaterialPurchaseModel.RawMaterialAnalysisId = item.RawMaterialAnalysisId;
                    rawMaterialPurchaseModel.PurchaseQuantity = item.PurchaseQuantity;
                    rawMaterialPurchaseModel.RawMaterialModel = entityrawmateriallibrary.RawMaterialModel;
                    rawMaterialPurchaseModel.UnitName = entityrawmateriallibrary.Unit;
                    rawMaterialPurchaseModel.Description = entityrawmaterialanalysis.Description;
                    rawMaterialPurchaseModel.RawMaterialName = entityrawmateriallibrary.RawMaterialName;
                    rawMaterialPurchaseModel.Price = item.Price.ToString();
                    rawMaterialPurchaseModel.RawMaterialSupplier = item.RawMaterialSupplier;
                    rawMaterialPurchaseModel.Inventory = entityrawmaterialinventory.Quantity.ToString();
                    rawMaterialPurchaseModel.SuggestQuantity = entityrawmaterialanalysis.RawMaterialDosage.ToString();
                    list.Add(rawMaterialPurchaseModel);
                }
            }

            var jsonData = new
            {
                entity = data,
                childEntity = list
            };
            return ToJsonResult(jsonData);
        }
        /// <summary>
        /// ��ȡ�ӱ���ϸ��Ϣ 
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns>���ض���Json</returns>
        [HttpGet]
        public ActionResult GetDetailsJson(string keyValue)
        {
            List<RawMaterialPurchaseModel> list = new List<RawMaterialPurchaseModel>();
            var data = rawmaterialpurchasebll.GetList(p => p.RawMaterialPurchaseId == keyValue);
            if (data.Count > 0)
            {
                foreach (var item in data)
                {
                    RawMaterialPurchaseModel rawMaterialPurchaseModel = new RawMaterialPurchaseModel();
                    var purchaseQuantity = item.PurchaseQuantity;//ʵ�ʹ�����
                    var rawMaterialAnalysisId = item.RawMaterialAnalysisId;
                    var entityrawmaterialanalysis = rawmaterialanalysisbll.GetEntity(item.RawMaterialAnalysisId);
                    var entityrawmateriallibrary = rawmateriallibrarybll.GetEntity(entityrawmaterialanalysis.RawMaterialId);
                    //var entityrawmaterialinventory = rawmaterialinventorybll.GetEntity(entityrawmaterialanalysis.RawMaterialId);

                    rawMaterialPurchaseModel.RawMaterialPurchaseId = item.RawMaterialPurchaseId;
                    rawMaterialPurchaseModel.RawMaterialAnalysisId = item.RawMaterialAnalysisId;
                    rawMaterialPurchaseModel.PurchaseQuantity = item.PurchaseQuantity;
                    rawMaterialPurchaseModel.RawMaterialModel = entityrawmateriallibrary.RawMaterialModel;
                    rawMaterialPurchaseModel.UnitName = entityrawmateriallibrary.Unit;
                    rawMaterialPurchaseModel.Description = entityrawmaterialanalysis.Description;
                    rawMaterialPurchaseModel.RawMaterialName = entityrawmateriallibrary.RawMaterialName;
                    rawMaterialPurchaseModel.RawMaterialPurchaseModelPrice = item.Price;
                    rawMaterialPurchaseModel.RawMaterialSupplier = item.RawMaterialSupplier;
                    //rawMaterialPurchaseModel.Inventory = entityrawmaterialinventory.Quantity.ToString();
                    //rawMaterialPurchaseModel.SuggestQuantity = entityrawmaterialanalysis.RawMaterialDosage.ToString();
                    list.Add(rawMaterialPurchaseModel);
                }
            }
            
            return ToJsonResult(list);

        }

        #region �ύ����
        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult RemoveForm(string keyValue)
        {
            rawmaterialpurchasebll.RemoveForm(keyValue);
            return Success("ɾ���ɹ���");

        }
        /// <summary>
        /// ��������������޸ģ�
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="strEntity">ʵ�����</param>
        /// <param name="strChildEntitys">�ӱ����</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(string keyValue, string strEntity, string strChildEntitys)
        {
            var entity = strEntity.ToObject<RawMaterialPurchaseEntity>();
            entity.CreateTime = System.DateTime.Now;
            entity.IsSubmit = entity.IsSubmit == null ? 0 : entity.IsSubmit;
            entity.IsPassed = entity.IsPassed == null ? 0 : entity.IsPassed;
            entity.IsPurchase = entity.IsPurchase == null ? 0 : entity.IsPurchase;
            entity.IsWarehousing = entity.IsWarehousing == null ? 0 : entity.IsWarehousing;
            entity.CreateMan = entity.CreateMan == null ? "���Թ���Ա" : entity.CreateMan;
            List<RawMaterialPurchaseInfoEntity> childEntitys = strChildEntitys.ToList<RawMaterialPurchaseInfoEntity>();
            rawmaterialpurchasebll.SaveForm(keyValue, entity, childEntitys);
            return Success("�����ɹ���");
        }

        /// <summary>
        /// �����Ѳɹ�
        /// </summary>  ��ȡ�����������ͨ���Ĳ���  IsPassed=1��ʾͨ��
        /// <param name="keyValue">����ֵ</param>
        /// <param name="strEntity">ʵ�����</param>
        /// <param name="strChildEntitys">�ӱ����</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult PurchaseSaveForm(string keyValue, string strEntity, string strChildEntitys)
        {
            //Session
            var entity = strEntity.ToObject<RawMaterialPurchaseEntity>();
            entity.IsPurchase = 1;
            entity.IsPassed = 1;
            entity.IsSubmit = 1;
            entity.IsWarehousing = entity.IsWarehousing == null ? 0 : entity.IsWarehousing;
            //entity.IsWarehousing = 1;
            List<RawMaterialPurchaseInfoEntity> childEntitys = strChildEntitys.ToList<RawMaterialPurchaseInfoEntity>();
            rawmaterialpurchasebll.SaveForm(keyValue, entity, childEntitys);
            return Success("�����ɹ���");
        }
        /// <summary>
        /// �ύ���
        /// </summary>
        /// <param name="keyValues">Ҫ��˵����ݵ�����Щ0(Ĭ��)δ�ύ��1�ύ</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult SubmitReview(string keyValues)
        {
            string[] ids = new string[] { };
            if (!string.IsNullOrEmpty(keyValues))
            {
                ids = keyValues.Split(',');
            }
            if (!ids.IsEmpty())
            {
                List<RawMaterialPurchaseEntity> list = new List<RawMaterialPurchaseEntity>();
                foreach (var item in ids)
                {
                    var model = rawmaterialpurchasebll.GetEntity(item.Trim());
                    if (model != null)
                    {
                        model.IsSubmit = 1;
                        list.Add(model);
                    }
                }
                if (list.Count > 0)
                {
                    rawmaterialpurchasebll.UpdataList(list);
                }
            }
            return Success("�����ɹ���");
        }
        /// <summary>
        /// ��˴���
        /// </summary>
        /// <param name="keyValues">Ҫ��˵����ݵ�����Щ</param>
        /// <param name="type">1ͨ����2ʧ��</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult ReviewOperation(string keyValues, int type)
        {
            string[] ids = new string[] { };
            if (!string.IsNullOrEmpty(keyValues))
            {
                ids = keyValues.Split(',');
            }
            if (!ids.IsEmpty())
            {
                List<RawMaterialPurchaseEntity> list = new List<RawMaterialPurchaseEntity>();
                foreach (var item in ids)
                {
                    var model = rawmaterialpurchasebll.GetEntity(item.Trim());
                    if (model != null)
                    {
                        model.IsPassed = type;
                        model.ReviewTime = System.DateTime.Now;
                        model.ReviewMan = "���Թ���Ա";
                        list.Add(model);
                    }
                }
                if (list.Count > 0)
                {
                    rawmaterialpurchasebll.UpdataList(list);
                }
            }
            return Success("�����ɹ���");
        }

        /// <summary>
        /// �ύ�ɹ�
        /// </summary>
        /// <param name="keyValues">Ҫ��˵����ݵ�����Щ0(Ĭ��)δ�ɹ���1�Ѳɹ�</param>
        /// <returns></returns>
        //[HttpPost]
        //[AjaxOnly]
        //public ActionResult SubmitIsPurchase(string keyValues)
        //{
        //    string[] ids = new string[] { };
        //    if (!string.IsNullOrEmpty(keyValues))
        //    {
        //        ids = keyValues.Split(',');
        //    }
        //    if (!ids.IsEmpty())
        //    {
        //        List<RawMaterialPurchaseEntity> list = new List<RawMaterialPurchaseEntity>();
        //        foreach (var item in ids)
        //        {
        //            var model = rawmaterialpurchasebll.GetEntity(item.Trim());
        //            if (model != null)
        //            {
        //                model.IsPurchase = 1;
        //                list.Add(model);
                        
        //            }
        //        }
        //        if (list.Count > 0)
        //        {
        //            rawmaterialpurchasebll.UpdataList(list);
        //            //���Ѳɹ��ļ���ԭ���Ͽ�
        //            for (var i = 0; i < ids.Length; i++) {
        //                try
        //                {
        //                    var model = rawmaterialpurchasebll.GetEntity(ids[i]);
        //                    //��ȡ���ɹ�����
        //                    if (model.RawMaterialPurchaseId != null) {
        //                        //����ȥ��������ȡ��ÿһ��
        //                        var listInfo = rawmaterialpurchasebll.GetList(p => p.RawMaterialPurchaseId == model.RawMaterialPurchaseId && model.IsPurchase==1);
        //                        //�õ��ɹ����µ�����
        //                        for (var j=0;j<listInfo.Count;j++)
        //                        {
        //                            RawMaterialInventoryEntity rawMaterialInventoryEntity = new RawMaterialInventoryEntity();
        //                           var quantity=listInfo[j].PurchaseQuantity;//�ɹ�������
        //                           var rawmaterialanalysis=rawmaterialanalysisbll.GetEntity(listInfo[j].RawMaterialAnalysisId);//ͨ������IDȡ�÷�������
        //                           var rawMaterialId = rawmaterialanalysis.RawMaterialId;//ȡ�ò���ID
        //                           var rawmateriallibrary=rawmateriallibrarybll.GetEntity(rawMaterialId);//���ݲ���IDȡ��ԭ����������Ϣ

        //                            rawMaterialInventoryEntity.RawMaterialId = rawMaterialId;
        //                            rawMaterialInventoryEntity.RawMaterialModel = rawmateriallibrary.RawMaterialModel;
        //                            rawMaterialInventoryEntity.RawMaterialStandard = rawmateriallibrary.RawMaterialStandard;
        //                            rawMaterialInventoryEntity.Quantity = quantity;
        //                            rawMaterialInventoryEntity.Unit = rawmateriallibrary.Unit;
        //                            rawMaterialInventoryEntity.Category = rawmateriallibrary.Category;
        //                            rawMaterialInventoryEntity.InventoryTime = System.DateTime.Now;  //���ʱ��

        //                            //�鿴ԭ���Ͽ����Ƿ��иò��ϣ��о�ֱ�Ӽӿ��rawMaterialId
        //                            var linventoryEntity = rawmaterialinventorybll.GetEntityByRawMaterialId(rawMaterialId);
        //                            if (linventoryEntity != null)
        //                            {
        //                                RawMaterialInventoryEntity Entity = new RawMaterialInventoryEntity();
        //                                var allquantity = linventoryEntity.Quantity + quantity;
        //                                Entity.Quantity = allquantity;
        //                                Entity.InventoryTime = System.DateTime.Now;
        //                                rawmaterialinventorybll.SaveForm(linventoryEntity.InventoryId, Entity);
        //                            }
        //                            else {
        //                                string keyvalue = null;
        //                                rawmaterialinventorybll.SaveForm(keyvalue, rawMaterialInventoryEntity);
        //                            }
                                   
                                    

        //                        }
        //                    }
        //                }
        //                catch (System.Exception e)
        //                {
        //                    throw (e);
        //                }
        //            }
                    
        //        }
        //    }
        //    return Success("�����ɹ���");
        //}
        #endregion
    }
}
    #endregion