using Newtonsoft.Json;
using EcommerceWebsite.Models;
using EcommerceWebsite.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace EcommerceWebsite.Controllers
{
    public class AdminController : Controller
    {



        public GenericUnitOfWork _unitOfWork = new GenericUnitOfWork();

        [Authorize(Roles = "Admin")]
        public ActionResult Dashboard()
        {
            return View();
        }


        [Authorize(Roles = "Admin")]
        public ActionResult Product()
        {
            return View(_unitOfWork.GetRepositoryInstance<Product>().GetProduct());
        }

        [Authorize(Roles = "Admin")]
        public ActionResult ProductEdit(int productId)
        {
            return View(_unitOfWork.GetRepositoryInstance<Product>().GetFirstorDefault(productId));
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult ProductEdit(Product tbl, HttpPostedFileBase file)
        {
            byte[] imageBytes = null;
            if (file != null)
            {
                using (var binaryReader = new BinaryReader(file.InputStream))
                {
                    imageBytes = binaryReader.ReadBytes(file.ContentLength);
                }
            }
            tbl.Image = imageBytes ?? tbl.Image; // Preserve existing image if no new image is uploaded

            _unitOfWork.GetRepositoryInstance<Product>().Update(tbl);
            return RedirectToAction("Product");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult ProductAdd()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult ProductAdd(Product tbl, HttpPostedFileBase file)
        {
            byte[] imageBytes = null;
            if (file != null)
            {
                using (var binaryReader = new BinaryReader(file.InputStream))
                {
                    imageBytes = binaryReader.ReadBytes(file.ContentLength);
                }
            }
            tbl.Image = imageBytes;

            _unitOfWork.GetRepositoryInstance<Product>().Add(tbl);
            return RedirectToAction("Product");
        }

    }
}