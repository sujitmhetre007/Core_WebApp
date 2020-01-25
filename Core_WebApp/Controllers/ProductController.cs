using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core_WebApp.Models;
using Core_WebApp.Services;

namespace Core_WebApp.Controllers
{
    /// <summary>
    /// Controller is a base class for ASP.NET Core MVC Controller for follwoing
    /// IActionFilter , the interface that will be used to define functionality
    /// to be added in Http Request wheb controller starts executing e.g. Exceptions
    /// IFilterMetadata, used by Filter to work with Model Metadata for Data Validation
    /// IAsyncActionFilter, the Async Filters, IDsiposable
    /// Controller have all methods as HttpGet by default to accept the request and execute
    /// To accept POSt request method must be decoarated with HttpPost attribute
    /// Each HttpGet and HttpPost request is called 'ActionMethod' and its return 
    /// IActionresult (ViewResult/PartialViewResult/JsonResult/FileResult)
    /// </summary>
    public class ProductController : Controller
    {
        // inject the Product Repository
        private IRepository<Product, int> repository;
        public ProductController(IRepository<Product, int> repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Return the List of Model Data, in our case it will be Product
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            var res = await repository.GetAsync();
            return View(res); // this will return Index View as the name of method is 'Index'
        }

        /// <summary>
        /// Return an empty Product Object to create new Product 
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            var res = new Product();
            return View(res);
        }
        /// <summary>
        /// This will accept the Product Object to create a new Product 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async  Task<IActionResult> Create(Product Product)
        {
            // validate the received Product
            if (ModelState.IsValid)
            {
                var res = await repository.CreateAsync(Product);
                return RedirectToAction("Index"); //redirect to Index action method
            }
            return View(Product); // stay on Create View with errors
        }

        /// <summary>
        /// Return an empty Product Object to create new Product 
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Edit(int id)
        {
            var res = await repository.GetAsync(id);
            return View(res); // return Edit view with data to be edited
        }
        /// <summary>
        /// This will accept the Product Object to create a new Product 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Product Product)
        {
            // validate the received Product
            if (ModelState.IsValid)   
            {
                var res = await repository.UpdateAsync(id,Product);
                return RedirectToAction("Index"); //redirect to Index action method
            }
            return View(Product); // stay on edit View with errors
        }

        public async Task<IActionResult> Delete(int id)
        {
            var res = await repository.DeleteAsync(id);
            if (res)
            {
                return RedirectToAction("Index"); // with success in delete
            }
            return RedirectToAction("Index"); // with fail in delete
        }

    }
}