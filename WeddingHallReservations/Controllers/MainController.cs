using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using WeddingHallReservations.Models;

namespace WeddingHallReservations.Controllers
{

    public class MainController : Controller
    {
        public readonly WHRContext dbObject;
        public readonly IWebHostEnvironment webHost;
        public static Dictionary<int,int> listOfSelectedProduct
            = new Dictionary<int, int>();
        public static double? amount = 0;
        public static int sId = 0;

        public MainController(WHRContext dbObject, IWebHostEnvironment webHost)
        {
            this.dbObject = dbObject;
            this.webHost = webHost;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("UserId") == null)
            {
                ViewBag.button = "Login";
            }
            else
            {
                ViewBag.button = "Other";

            }
            //fill connent for the home page 
            ViewBag.customer = dbObject.User.Count();
            ViewBag.service =dbObject.Service.Count();
            ViewBag.products = dbObject.Prodouct.Count();
            ViewBag.resrvations = dbObject.Reservaition.Count();

            var service=dbObject.Service.ToList();
            var category = dbObject.Catregory.ToList();

            var wService = from c in category
                           join
                           s in service on c.CategoeyId equals s.CategoryId
                           select new WService
                           {
                               Service=s,
                               Catregory=c
                           };
            return View(wService.OrderByDescending(x =>x.Service.ServiceId).Take(4));
        }


        public IActionResult Service()
        {
            if (HttpContext.Session.GetInt32("UserId") == null)
            {
                ViewBag.button = "Login";
            }
            else
            {
                ViewBag.button = "Other";

            }

            //dispaly all available services 

            var service = dbObject.Service.ToList();
            var category = dbObject.Catregory.ToList();

            var wService = from c in category
                           join
                           s in service on c.CategoeyId equals s.CategoryId
                           select new WService
                           {
                               Service = s,
                               Catregory = c
                           };
            return View(wService);
        }
        public IActionResult Search(int? categoryId = null, double? startPrice = null
          , string Address = null, string ServiceProvidedName = null)
        {
            if (HttpContext.Session.GetInt32("UserId") == null)
            {
                ViewBag.button = "Login";
            }
            else
            {
                ViewBag.button = "Other";

            }
            var service = dbObject.Service.ToList();
            var category = dbObject.Catregory.ToList();
            if (categoryId == null && startPrice == null
            && Address == null && ServiceProvidedName == null)
            {
                service = dbObject.Service.ToList();
            }
            else if (categoryId == null && startPrice == null
            && Address == null && ServiceProvidedName != null)
            {
                service = dbObject.Service.Where(x => x.ServiceProvided.Contains(ServiceProvidedName)).ToList();

            }
            else if (categoryId == null && startPrice == null
            && Address != null && ServiceProvidedName == null)
            {
                service = dbObject.Service.Where(x => x.Address.Equals(Address)).ToList();

            }
            else if (categoryId == null && startPrice == null && Address != null && ServiceProvidedName != null)
            {
                service = dbObject.Service.Where(x => x.Address.Equals(Address) && x.ServiceProvided.Contains(ServiceProvidedName)).ToList();

            }
            else if (categoryId == null && startPrice != null && Address == null && ServiceProvidedName == null)
            {
                service = dbObject.Service.Where(x => x.StartingPrice >= startPrice).ToList();

            }
            else if (categoryId == null && startPrice != null
            && Address == null && ServiceProvidedName != null)
            {
                service = dbObject.Service.Where(x => x.StartingPrice >= startPrice && x.ServiceProvided.Contains(ServiceProvidedName)).ToList();

            }
            else if (categoryId == null && startPrice != null
            && Address != null && ServiceProvidedName == null)
            {
                service = dbObject.Service.Where(x => x.StartingPrice >= startPrice && x.Address.Equals(Address)).ToList();

            }
            else if (categoryId == null && startPrice != null
            && Address != null && ServiceProvidedName != null)
            {
                service = dbObject.Service.Where(x => x.StartingPrice >= startPrice && x.Address.Equals(Address) && x.ServiceProvided.Contains(ServiceProvidedName)).ToList();

            }
            else if (categoryId != null && startPrice == null
            && Address == null && ServiceProvidedName == null)
            {

                service = dbObject.Service.Where(x => x.CategoryId == categoryId).ToList();

            }

            else if (categoryId != null && startPrice == null
            && Address == null && ServiceProvidedName != null)
            {
                service = dbObject.Service.Where(x => x.CategoryId == categoryId && x.ServiceProvided.Contains(ServiceProvidedName)).ToList();
            }

            else if (categoryId != null && startPrice == null
            && Address != null && ServiceProvidedName == null)
            {
                service = dbObject.Service.Where(x => x.CategoryId == categoryId && x.Address.Equals(Address)).ToList();
            }

            else if (categoryId != null && startPrice == null
            && Address != null && ServiceProvidedName != null)
            {
                service = dbObject.Service.Where(x => x.CategoryId == categoryId && x.Address.Equals(Address) && x.ServiceProvided.Contains(ServiceProvidedName)).ToList();
            }
            else if (categoryId != null && startPrice != null
            && Address == null && ServiceProvidedName == null)
            {
                service = dbObject.Service.Where(x => x.CategoryId == categoryId && x.StartingPrice >= startPrice).ToList();
            }
            else if (categoryId != null && startPrice != null
            && Address == null && ServiceProvidedName != null)
            {
                service = dbObject.Service.Where(x => x.CategoryId == categoryId && x.ServiceProvided.Contains(ServiceProvidedName) && x.StartingPrice >= startPrice).ToList();
            }


            else if (categoryId != null && startPrice != null
            && Address != null && ServiceProvidedName == null)
            {
                service = dbObject.Service.Where(x => x.CategoryId == categoryId && x.Address.Equals(Address) && x.StartingPrice >= startPrice).ToList();
            }

            else if (categoryId != null && startPrice != null
            && Address != null && ServiceProvidedName != null)
            {
                service = dbObject.Service.Where(x => x.CategoryId == categoryId && x.Address.Equals(Address) && x.StartingPrice >= startPrice
                && x.ServiceProvided.Contains(ServiceProvidedName)).ToList();
            }

            var wService = from c in category
                           join
                         s in service on c.CategoeyId equals s.CategoryId
                           select new WService
                           {
                               Service = s,
                               Catregory = c
                           };

            return View("Service", wService);
        }

        [HttpPost]
        public IActionResult SearchPost(int? categoryId=null,double? startPrice = null
            , string Address = null, string ServiceProvidedName = null)
        {
            if (HttpContext.Session.GetInt32("UserId") == null)
            {
                ViewBag.button = "Login";
            }
            else
            {
                ViewBag.button = "Other";

            }
            var service = dbObject.Service.ToList();
            var category = dbObject.Catregory.ToList();
            if (categoryId==null && startPrice ==null
            && Address.Equals(null) && ServiceProvidedName == null){
                 service = dbObject.Service.ToList();
            }
            else if (categoryId == null && startPrice == null
            && Address == null && ServiceProvidedName != null)
            {
                service = dbObject.Service.Where(x=>x.ServiceProvided.Contains(ServiceProvidedName)).ToList();
              
            }
            else if (categoryId == null && startPrice == null
            && Address != null && ServiceProvidedName == null)
            {
                service = dbObject.Service.Where(x => x.Address.Equals(Address)).ToList();
                
            }
            else if (categoryId == null && startPrice == null&& Address != null && ServiceProvidedName != null)
            {
                service = dbObject.Service.Where(x => x.Address.Equals(Address) && x.ServiceProvided.Contains(ServiceProvidedName)).ToList();
                
            }
            else if (categoryId == null && startPrice != null && Address == null && ServiceProvidedName == null)
            {
                service = dbObject.Service.Where(x => x.StartingPrice>= startPrice).ToList();
               
            }
            else if (categoryId == null && startPrice != null
            && Address == null && ServiceProvidedName != null)
            {
                service = dbObject.Service.Where(x => x.StartingPrice >= startPrice && x.ServiceProvided.Contains(ServiceProvidedName)).ToList();
               
            }
            else if (categoryId == null && startPrice != null
            && Address != null && ServiceProvidedName == null)
            {
                service = dbObject.Service.Where(x => x.StartingPrice >= startPrice && x.Address.Equals(Address)).ToList();
              
            }
            else if (categoryId == null && startPrice != null
            && Address != null && ServiceProvidedName != null)
            {
                service = dbObject.Service.Where(x => x.StartingPrice >= startPrice && x.Address.Equals(Address) &&  x.ServiceProvided.Contains(ServiceProvidedName)).ToList();
               
            }
            else if (categoryId != null && startPrice == null
             && Address == null && ServiceProvidedName == null)
            {
               
                service = dbObject.Service.Where(x=>x.CategoryId==categoryId).ToList();
                
            }
            
            else if (categoryId != null && startPrice == null
            && Address == null && ServiceProvidedName != null)
            {
                service = dbObject.Service.Where(x => x.CategoryId == categoryId && x.ServiceProvided.Contains(ServiceProvidedName)).ToList();
            }

            else if (categoryId != null && startPrice == null
            && Address != null && ServiceProvidedName == null)
            {
                service = dbObject.Service.Where(x => x.CategoryId == categoryId && x.Address.Equals(Address)).ToList();
            }

            else if (categoryId != null && startPrice == null
            && Address != null && ServiceProvidedName != null)
            {
                service = dbObject.Service.Where(x => x.CategoryId == categoryId && x.Address.Equals(Address) && x.ServiceProvided.Contains(ServiceProvidedName) ).ToList();
            }
            else if (categoryId != null && startPrice != null
            && Address == null && ServiceProvidedName == null)
            {
                service = dbObject.Service.Where(x => x.CategoryId == categoryId && x.StartingPrice >= startPrice).ToList();
            }
            else if (categoryId != null && startPrice != null
            && Address == null && ServiceProvidedName != null)
            {
                service = dbObject.Service.Where(x => x.CategoryId == categoryId && x.ServiceProvided.Contains(ServiceProvidedName) && x.StartingPrice >= startPrice).ToList();
            }


            else if (categoryId != null && startPrice != null
            && Address != null && ServiceProvidedName == null)
            {
                service = dbObject.Service.Where(x => x.CategoryId == categoryId && x.Address.Equals(Address) && x.StartingPrice >= startPrice).ToList();
            }

            else if (categoryId != null && startPrice != null
            && Address != null && ServiceProvidedName != null)
            {
                service = dbObject.Service.Where(x => x.CategoryId == categoryId && x.Address.Equals(Address) && x.StartingPrice >= startPrice
                && x.ServiceProvided.Contains(ServiceProvidedName)).ToList();
            }
            var wService = from c in category
                           join
                         s in service on c.CategoeyId equals s.CategoryId
                           select new WService
                           {
                               Service = s,
                               Catregory = c
                           };

            return View("Service", wService);
        }

        public IActionResult Details(int serviceId)
        {

            if (HttpContext.Session.GetInt32("UserId") == null)
            {
                ViewBag.button = "Login";
                return RedirectToAction("Login");
            }
            else
            {

                ViewBag.button = "Other";

                if (serviceId == 0)
            {
                serviceId = sId;
            }
            else
            {
                sId = serviceId;
            }
            
            var service = dbObject.Service.Where(x=>x.ServiceId== serviceId).ToList();
            var category = dbObject.Catregory.ToList();
            var wServiceDetails = from c in category join s in service on c.CategoeyId equals s.CategoryId
                           select new WService{Service = s,Catregory = c,  };
            WServiceDetails obj = new WServiceDetails();
            obj.Service = wServiceDetails.ElementAt(0).Service;
            obj.Catregory = wServiceDetails.ElementAt(0).Catregory;
            obj.ProdouctInfo = new List<ProductInfo>();
            List<Prodouct> products = dbObject.Prodouct.Where(x => x.ServiceId.Equals(serviceId)).ToList();
            foreach(Prodouct i in products) {
                Prodouct p = i;
                List<Media> media = dbObject.Media.Where(x => x.ProductId == i.ProductId).ToList();
                ProductInfo info=new ProductInfo();
                info.Prodouct = p;
                info.Medias = media;
                obj.ProdouctInfo.Add(info);
            }

            ViewBag.MyCartList = listOfSelectedProduct;
           
            ViewBag.totalPrice = amount;
            return View(obj);
            }
        }

     
        [HttpPost]
        public IActionResult Payment(string visa,string cvv2, DateTime date,
            DateTime dateTimeFrom, DateTime dateTimeTo
            , string note, string phone)
        {
            if (HttpContext.Session.GetInt32("UserId") == null)
            {
                ViewBag.button = "Login";
            }
            else
            {
                ViewBag.button = "Other";

            }
            //Payment Action and Make Reservaitions
            var pay = dbObject.Payment.Where(x => x.Visa == visa && x.Cvv2 == cvv2
            && x.ExpireDate == date && x.Balance >= amount).SingleOrDefault();
            if (pay != null)
            {
                //res
                pay.Balance -= amount;
                dbObject.Update(pay);

                var checkIfThereAResr = dbObject.Reservaition.Where(x => x.ServiceId == sId &&
                   x.ReservaitionDateFrom == dateTimeFrom).FirstOrDefault();
                if (checkIfThereAResr == null)
                {


                    Reservaition reservaition = new Reservaition();
                    reservaition.ResrvitionId = dbObject.Reservaition.OrderByDescending(x => x.ResrvitionId).FirstOrDefault().ResrvitionId + 1;
                    reservaition.TotalPrice = amount;
                    reservaition.ReservaitionDateFrom = dateTimeFrom;
                    reservaition.ReservaitionDateTo = dateTimeTo;
                    reservaition.CreatedBy = HttpContext.Session.GetInt32("UserId");
                    reservaition.Notes = note;
                    reservaition.CreatedDateTime = DateTime.Now;
                    reservaition.ServiceId = sId;
                    dbObject.Add(reservaition);
                    dbObject.SaveChanges();

                    int resId = dbObject.Reservaition.OrderByDescending(x => x.ResrvitionId)
                        .FirstOrDefault().ResrvitionId;
                    foreach (var key in listOfSelectedProduct.Keys)
                    {
                        ReservaitionProducts reservaitionProducts = new ReservaitionProducts();
                        reservaitionProducts.ResrvitionId = resId;
                        reservaitionProducts.ProductId = (int)key;
                        reservaitionProducts.Quantity = (int)listOfSelectedProduct[key];

                        dbObject.Add(reservaitionProducts);
                        dbObject.SaveChanges();

                    }

                    sId = 0;
                    listOfSelectedProduct = new Dictionary<int, int>();

                    return RedirectToAction("History");
                }
                else
                {
                    ViewBag.ErrorMessaage = "Wrong Reservaition Time Please Change The Starting Time";
                    ViewBag.MyCartList = listOfSelectedProduct;
                    ViewBag.totalPrice = amount;
                    return RedirectToAction("Details", sId);
                }

            }
            else
            {
                ViewBag.ErrorMessaage = "Wrong Payment Information Please Try Again";
                ViewBag.MyCartList = listOfSelectedProduct;
                ViewBag.totalPrice = amount;
                return RedirectToAction("Details", sId);
                //return NotFound();
            }
            //return View();
        }

        public IActionResult Redirect()
        {
            if (HttpContext.Session.GetInt32("UserId") == null)
            {
                ViewBag.button = "Login";
            }
            else
            {
                ViewBag.button = "Other";

            }
            return RedirectToAction("Index");
        }

        public IActionResult History()
        {

            if(HttpContext.Session.GetInt32("UserId") == null)
            {
                ViewBag.button = "Login";
                return RedirectToAction("Login");
            }
            else
            {
                ViewBag.button = "Other";
                //List My Previous Reservaitions 
                var user = dbObject.User.Where(x => x.UserId == HttpContext.Session.GetInt32("UserId")).ToList();
                var reserviation = dbObject.Reservaition.Where(x => x.CreatedBy == HttpContext.Session.GetInt32("UserId")).ToList();
                var services = dbObject.Service.ToList();

                var myReserviations = from u in user
                                      join
                                      r in reserviation on u.UserId equals r.CreatedBy
                                      join s in services on r.ServiceId equals s.ServiceId

                                      select new ReserviationHistory
                                      {
                                          Service = s,
                                          User = u,
                                          Reservaition = r
                                      };
                return View(myReserviations);
            }
        }


        public IActionResult ManageListOfProdcts(int productId,int bit,int quantity)
        {
            if (HttpContext.Session.GetInt32("UserId") == null)
            {
                ViewBag.button = "Login";
            }
            else
            {
                ViewBag.button = "Other";

            }
            //bit equal 1 that means add to products
            if (bit == 10)
            {
                if (listOfSelectedProduct.ContainsKey(productId))
                {
                    listOfSelectedProduct[productId] = listOfSelectedProduct[productId]+1;
                }
                else
                {
                    listOfSelectedProduct.Add(productId, quantity);
                }
              
            }
            else
            {
                listOfSelectedProduct[productId] = listOfSelectedProduct[productId] - 1;
            }
           
            


            for (int index = 0; index < listOfSelectedProduct.Count; index++)
            {
                var item = listOfSelectedProduct.ElementAt(index);
                double? itemPrice = dbObject.Prodouct.Where(x => x.ProductId == item.Key).Single().PricePerOne;
                int itemQuantity = item.Value;
                double? finalprice = itemPrice * itemQuantity;
                amount = finalprice;
            }
            ViewBag.MyCartList = listOfSelectedProduct;
            ViewBag.totalPrice = amount;
            return RedirectToAction("Details", sId);
        }

        public IActionResult Login()
        {
            //Login View
            return View();
        }
        [HttpPost]
        public IActionResult Login(string email,string password)
        {
            //Login Operation
            var user = dbObject.User.Where(x => x.Email == email
            && x.Password==password).SingleOrDefault();
            if (user == null)
            {
                return View();
            }
            else
            {
                HttpContext.Session.SetInt32("UserId", user.UserId);
                return RedirectToAction("History");
            }
               
        }

        public IActionResult Register()
        {
            //Register View
            return View();
        }
        [HttpPost]
        public IActionResult Register(string name , string email , string phone
            ,string password)
        {
            var user = dbObject.User.Where(x => x.Email == email).SingleOrDefault();
            if (user == null){ 
                User person = new User();
                person.Email = email;
                person.Name = name;
                person.Password = password;
                person.Phone = phone;
                dbObject.Add(person);
                dbObject.SaveChanges();
                return RedirectToAction("Login");
            }
            else{
                return RedirectToAction("Register");
            }
          
        }

        public IActionResult Logout()
        {
            sId = 0;
            listOfSelectedProduct = new Dictionary<int, int>();
            HttpContext.Session.Remove("UserId");
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }


}
