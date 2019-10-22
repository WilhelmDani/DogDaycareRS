using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DogDayCareRS.MVC.DATA.EF;
using Microsoft.AspNet.Identity;

namespace DogDayCareRS.MVC.UI.Controllers
{
    [Authorize]
    public class OwnerAssetsController : Controller
    {
        private DogDaycareRSEntities db = new DogDaycareRSEntities();

        // GET: OwnerAssets
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var ownerAssets = User.IsInRole("Client") ? db.OwnerAssets.Where(o => o.UserID == userId) : db.OwnerAssets;
            return View(ownerAssets.ToList());
        }

        // GET: OwnerAssets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OwnerAsset ownerAsset = db.OwnerAssets.Find(id);
            if (ownerAsset == null)
            {
                return HttpNotFound();
            }
            return View(ownerAsset);
        }

        // GET: OwnerAssets/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OwnerAssets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OwnerAssetID,AssetName,UserID,AssetPhoto,SpecialNotes,IsActive,DateAdded")] OwnerAsset ownerAsset, HttpPostedFileBase fupImage)
        {
                 if (ownerAsset.UserID == null) {
               ownerAsset.UserID = User.Identity.GetUserId();
            }

            //File upload for create
            if (ModelState.IsValid)
            {


                string imgName = "noImage.png";
                if (fupImage != null)
                {
                    imgName = fupImage.FileName;
                    string ext = imgName.Substring(imgName.LastIndexOf('.'));
                    string[] goodExts = { "jpeg", ".jpg", ".gif", ".png" };

                    if (goodExts.Contains(ext.ToLower()) && (fupImage.ContentLength <= 4194304))
                    {
                        //imgName = Guid.NewGuid() + ext;
                        fupImage.SaveAs(Server.MapPath("~/Content/Dogimages/" + imgName));
                    }
                    else
                    {
                        imgName = "noImage.png";
                    }
                }
                ownerAsset.AssetPhoto = imgName;
                db.Entry(ownerAsset).State = EntityState.Modified;
                db.OwnerAssets.Add(ownerAsset);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ownerAsset);
        }

        // GET: OwnerAssets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OwnerAsset ownerAsset = db.OwnerAssets.Find(id);
            if (ownerAsset == null)
            {
                return HttpNotFound();
            }
            return View(ownerAsset);
        }

        // POST: OwnerAssets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OwnerAssetID,AssetName,UserID,AssetPhoto,SpecialNotes,IsActive,DateAdded")] OwnerAsset ownerAsset, HttpPostedFileBase fupImage)
        {
            if (ownerAsset.UserID == null) {
               ownerAsset.UserID = User.Identity.GetUserId();
            }

            if (ModelState.IsValid)
            {
                //File upload for Edit
                if (fupImage != null)
                {
                    string imgName = fupImage.FileName;
                    imgName = fupImage.FileName;
                    string ext = imgName.Substring(imgName.LastIndexOf('.'));
                    string[] goodExts = { ".jpeg", ".jpg", ".gif", ".png" };

                    if (goodExts.Contains(ext.ToLower()) && (fupImage.ContentLength <= 4194304))
                    {
                        fupImage.SaveAs(Server.MapPath("~/Content/dogImages/" + imgName));

                        if (ownerAsset.AssetPhoto != null && ownerAsset.AssetPhoto != "noImage.png")
                        {
                            System.IO.File.Delete(Server.MapPath("~/Content/dogImages/" + ownerAsset.AssetPhoto));
                        }
                        ownerAsset.AssetPhoto = imgName;
                    }
                    else
                    {
                        imgName = "noImage.png";
                        throw new ApplicationException("Incorrect file type. 1) Please use one of the following: (png, jpg, jpeg, or gif). " +
                            " 2) File size may exceed the 4MB limit. Please reduce the file size and try again.");
                                   }

                }



                db.Entry(ownerAsset).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ownerAsset);
        }

        // GET: OwnerAssets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OwnerAsset ownerAsset = db.OwnerAssets.Find(id);
            if (ownerAsset == null)
            {
                return HttpNotFound();
            }
            return View(ownerAsset);
        }

        // POST: OwnerAssets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
           
            OwnerAsset ownerAsset = db.OwnerAssets.Find(id);
            if (ownerAsset.AssetPhoto != null && ownerAsset.AssetPhoto != "noImage.png")
            {
                System.IO.File.Delete(Server.MapPath("~/Content/dogImages/" + ownerAsset.AssetPhoto));
            }
            db.OwnerAssets.Remove(ownerAsset);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
