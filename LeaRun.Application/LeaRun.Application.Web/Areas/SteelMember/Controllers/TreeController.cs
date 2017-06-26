using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using LeaRun.Application.Repository.SteelMember.IBLL;
using LeaRun.Data.Entity;
using LeaRun.Util.WebControl;
using LeaRun.Util;

namespace LeaRun.Application.Web.Areas.SteelMember.Controllers
{
    public class TreeController : MvcControllerBase
    {
        // GET: DocManagement/Tree
        //public Base_ModuleBll Sys_modulebll = new Base_ModuleBll();
        //public Base_ButtonBll Sys_buttonbll = new Base_ButtonBll();
        [Inject]
        public TreeIBLL TreeCurrent { get; set; }
        [Inject]
        public ProjectInfoIBLL ProjectInfoCurrent { get; set; }
        /// <summary>
        /// 文件夹表单视图
        /// </summary>
        /// <returns></returns>
        //[ManagerPermission(PermissionMode.Enforce)]

        public virtual ActionResult IsItem(string TreeId)
        {
            int treeid = Convert.ToInt32(TreeId);
            var entitytree = TreeCurrent.Find(f => f.TreeID == treeid).SingleOrDefault();
            return Json(entitytree);
        }
        public virtual ActionResult FolderForm()
        {
            return View();
        }

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 【模块管理】返回树JONS
        /// </summary>
        /// <returns></returns>
        public ActionResult TreeJson()
        {
            List<RMC_Tree> list = TreeCurrent.Find(f => f.TreeID > 0).ToList();
            List<TreeEntity> TreeList = new List<TreeEntity>();
            foreach (RMC_Tree item in list)
            {
                TreeEntity tree = new TreeEntity();
                bool hasChildren = false;
                List<RMC_Tree> childnode = list.FindAll(t => t.ParentID == item.TreeID);
                if (childnode.Count > 0)
                {
                    hasChildren = true;
                }
                tree.id = item.TreeID.ToString();
                tree.text = item.TreeName;
                tree.value = item.TreeID.ToString();
                tree.itemId = item.ItemID.ToString();
                tree.isexpand = item.State == 1 ? true : false;
                tree.complete = true;
                tree.hasChildren = hasChildren;
                tree.parentId = item.ParentID.ToString();
                tree.img = item.Icon != null ? "/Content/Images/Icon16/" + item.Icon : item.Icon;
                TreeList.Add(tree);
            }
            return Content(TreeList.TreeToJson());
        }
        /// <summary>
        /// 【模块管理】返回对象JSON
        /// </summary>
        /// <param name="KeyValue">主键值</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SetFormControl(string KeyValue)
        {
            int _KeyValue = Convert.ToInt32(KeyValue);
            RMC_Tree entity = TreeCurrent.Find(f => f.TreeID == _KeyValue).SingleOrDefault();
            string JsonData = entity.ToJson();

            JsonData = JsonData.Insert(1, "\"ParentName\":\"" + TreeCurrent.Find(f => f.TreeID == entity.ParentID).SingleOrDefault().TreeName + "\",");
            return Content(JsonData);
        }



        /// <summary>
        /// 【控制测量文档管理】返回文件夹对象JSON
        /// </summary>
        /// <param name="KeyValue">主键值</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        //[LoginAuthorize]
        public virtual ActionResult SetFolderForm(string KeyValue)
        {
            int folderid = Convert.ToInt32(KeyValue);
            RMC_Tree entity = TreeCurrent.Find(f => f.TreeID == folderid).SingleOrDefault();
            //string JsonData = entity.ToJson();
            ////自定义
            //JsonData = JsonData.Insert(1, Sys_FormAttributeBll.Instance.GetBuildForm(KeyValue));
            return Content(entity.ToJson());
        }
        /// <summary>
        /// 提交文件夹表单
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="KeyValue">主键值</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        //[LoginAuthorize]
        public virtual ActionResult SubmitFolderForm(RMC_Tree entity, string KeyValue, string TreeId, string ItemID, string ItemClassId)
        {
            try
            {
                string Message = KeyValue == "" ? "新增成功。" : "编辑成功。";
                if (!string.IsNullOrEmpty(KeyValue))
                {
                    int keyvalue = Convert.ToInt32(KeyValue);
                    RMC_Tree Oldentity = TreeCurrent.Find(t => t.TreeID == keyvalue).SingleOrDefault();//获取没更新之前实体对象
                    Oldentity.TreeName = entity.TreeName;//给旧实体重新赋值
                    Oldentity.ModifiedTime = DateTime.Now;
                    Oldentity.OverdueTime = entity.OverdueTime;
                    Oldentity.IsMenu = entity.IsMenu;
                    Oldentity.Enabled = entity.Enabled;
                    Oldentity.Description = entity.Description;
                    TreeCurrent.Modified(Oldentity);
                }
                else
                {
                    int treeid = Convert.ToInt32(TreeId);
                    RMC_Tree entitys = new RMC_Tree();
                    entitys.ParentID = treeid;
                    entitys.ItemID = Convert.ToInt32(ItemID);
                    entitys.IsItem = 0;
                    if (ItemClassId == "2")
                    {
                        entitys.ItemClass = Convert.ToInt32(ItemClassId);
                         
                    }

                    entitys.TreeName = entity.TreeName;
                    entitys.UploadTime = DateTime.Now;
                    entitys.ModifiedTime = DateTime.Now;
                    entitys.IsMenu = entity.IsMenu;
                    entitys.OverdueTime = entity.OverdueTime;
                    entitys.Enabled = 1;
                    entitys.IsReview = 0;
                    TreeCurrent.Add(entitys);

                }
                return Success(Message);
            }
            catch (Exception ex)
            {
                //this.WriteLog(-1, entity, null, KeyValue, "操作失败：" + ex.Message);
                throw new Exception(ex.ToString());
            }
        }

        /// <summary>
        /// 销毁文件树
        /// </summary>
        /// <param name="KeyValue"></param>
        /// <returns></returns>
        public ActionResult DeleteTree(string KeyValue)
        {
            int Ok = 0;
            try
            {
                List<int> ids = new List<int>();
                List<int> ids1 = new List<int>();
                int TreeId = Convert.ToInt32(KeyValue);

                var list = GetSonId(TreeId).ToList();
                list.Add(TreeCurrent.Find(p => p.TreeID == TreeId).Single());

                if (list.Count>0)
                {
                    foreach (var item in list)
                    {
                        ids.Add(item.TreeID);

                    }
                }

                Ok = TreeCurrent.Remove(ids);
               return Success("删除成功。");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        //获取树字节子节点(自循环)
        public IEnumerable<RMC_Tree> GetSonId(int p_id)
        {
            List<RMC_Tree> list = TreeCurrent.Find(p => p.ParentID == p_id).ToList();
            return list.Concat(list.SelectMany(t => GetSonId(t.TreeID)));
        }


        /// <summary>
        /// 标记删除文件树
        /// </summary>
        /// <param name="KeyValue"></param>
        /// <returns></returns>
        public ActionResult MarkTree(string KeyValue)
        {
            try
            {
                int TreeId = Convert.ToInt32(KeyValue);
                var file = TreeCurrent.Find(f => f.TreeID == TreeId).First();
                file.ModifiedTime = DateTime.Now;
                file.DeleteFlag = 1;
                TreeCurrent.Modified(file);
                return Success("删除成功。");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

    }
}