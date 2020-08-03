using Dapper;
using Market_Crud_Dapper.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Market_Crud_Dapper.Controllers
{
    public class MarketController : Controller
    {
        // GET: Market
        public SqlConnection dbConnection
        {
            get
            {
                var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MarketDbConnection"].ConnectionString);
                return connection;
            }
        }
        public ActionResult Index()
        {
            using (var db = dbConnection)
            {
                var dbProducts=db.Query<Product>("SELECT * FROM [dbo].[Product]").ToList();
                return View(dbProducts);
            }
        }
        [HttpGet]
        public ActionResult Edit(int id=0)
        {
            using (var db = dbConnection)
            {
                var dbProduct = db.Query<Product>("SELECT * FROM [dbo].[Product] WHERE [ID]=" + id).SingleOrDefault();
                if (dbProduct != null)
                {
                    return View(dbProduct);
                }
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(Product productModel)
        {
            //try
            //{
                Product product = new Product
                {
                    ProductName=productModel.ProductName,
                    ProductCode=productModel.ProductCode,
                    ProductDescription=productModel.ProductDescription,
                    ProductPrice=productModel.ProductPrice,
                    Category=productModel.Category,
                    Id=productModel.Id
                };
                using (var db = dbConnection)
                {

                    db.Query<Product>("UPDATE [dbo].[Product] SET [ProductName]=@ProductName," +
                                      "[ProductCode]=@ProductCode," +
                                      "[ProductDescription]=@ProductDescription," +
                                      "[ProductPrice]=@ProductPrice,"+
                                      "[Category]=@Category WHERE [Id]=@Id",product);
                }

                return RedirectToAction("Index");
            //}
            //catch
            //{
            //    return HttpNotFound();
            //}

        }
        public ActionResult Delete(int? id)
        {
            //try
            //{
                using (var db=dbConnection)
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
                    }

                    var dbProduct = db.Query<Product>("SELECT * FROM [dbo].[Product] WHERE [ID]=" + id).SingleOrDefault();
                    if (dbProduct == null)
                    {
                        return HttpNotFound();
                    }
                    else
                    {
                        db.Query<Product>("DELETE FROM [dbo].[Product] WHERE [Id] = " + dbProduct.Id);
                    }
                    //DELETE ACTIONRESULT'IN HTTPPOST PROTOKOLÜ DE YAPILACAK.

                    return RedirectToAction("Index");
                }

            //}
            //catch
            //{
            //    return HttpNotFound();
            //}
        }
        [HttpGet]
        public ActionResult Save()
        {
            return View(new Product());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Product productModel)
        {
            //try
            //{
               Product product = new Product
                {
                    ProductName=productModel.ProductName,
                    ProductCode=productModel.ProductCode,
                    ProductDescription=productModel.ProductDescription,
                    ProductPrice=productModel.ProductPrice,
                    Category=productModel.Category,
                };
                using (var db = dbConnection)
                {

                    db.Query<Product>("INSERT INTO [dbo].[Product]([ProductName],[ProductCode],[ProductDescription],[ProductPrice],[Category])" +
                                      " VALUES (@ProductName,@ProductCode,@ProductDescription,@ProductPrice,@Category)",product);
                }

                return RedirectToAction("Index");

            //}
            //catch
            //{
            //    return HttpNotFound();
            //}
        }
    }
}