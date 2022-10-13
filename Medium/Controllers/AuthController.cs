using BLL.Repositories.InterfaceRepositories;
using ENTITIES.Entity.Concrete;
using MAP.EntityTypeConfigration.Concrete;
using Medium.DTOs;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;

namespace Medium.Controllers
{
    public class AuthController : Controller
    {
        private readonly IGenericRepository<User> _genericRepository;
        private IWebHostEnvironment _webHostEnvironment;

        public AuthController(IGenericRepository<User> genericRepository, IWebHostEnvironment webHostEnvironment)
        {
            _genericRepository = genericRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult Login (string yonlen)
        {
            ViewBag.yonlen = yonlen;
            return View();
        }
        [HttpPost]
        public IActionResult Login(User userDto, string yonlen)
        {
            if (ModelState.ErrorCount < 4)
            {
                User response = _genericRepository.GetDefault(x => x.Username == userDto.Username && x.Password == userDto.Password);
                if (response == null)
                {
                    ModelState.AddModelError("", "Girmiş olduğunuz bilgilerle eşleşen bir kullanıcı bulunamadı");
                    return View();
                }
                HttpContext.Session.SetString("userId", response.Id.ToString());
                HttpContext.Session.SetString("username", response.Username);
            
            if (string.IsNullOrEmpty(yonlen))
            {
                return RedirectToAction("List", "Article");
            }
            return Redirect(yonlen);
            }
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(User userDto)
        {
            if(ModelState.IsValid)
            {
                User userCheck = _genericRepository.Where(x => x.Username == userDto.Username).FirstOrDefault();

                if (userCheck != null) return View();

                _genericRepository.Add(userDto);
                return RedirectToAction("Login");
            }
            return View();
        }
        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("userId");
            HttpContext.Session.Remove("username");
            HttpContext.Session.Clear();
            return RedirectToAction("Login","Auth");
        }
        public IActionResult Profile(string username)
        {
            User user = _genericRepository.Where(x => x.Username == username).FirstOrDefault();
            if (user == null)
            {
                ModelState.AddModelError(String.Empty, "The user can not be found!");
                return RedirectToAction("Index","Home");
            }
            EditProfileDTo dto = new EditProfileDTo()
            {
                Username = user.Username,
                Password = user.Password,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Phone = user.Phone,
                Website = user.Website,
                AboutMe = user.AboutMe,
                Image = user.Image              
            };
            return View(dto);
        }
        [HttpPost]
        public IActionResult Profile(EditProfileDTo model)
        {
            if (ModelState.IsValid)
            {
                string imageName = "noimage.png";
                if (model.UploadImage != null)
                {
                    string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "images");

                    if (!string.Equals(model.Image, "noimage.png"))
                    {
                        string oldPath = Path.Combine(uploadDir, model.Image);

                        if (System.IO.File.Exists(oldPath))
                            System.IO.File.Delete(oldPath);
                    }

                    imageName = $"{Guid.NewGuid()}_{model.UploadImage.FileName}";
                    string filePath = Path.Combine(uploadDir, imageName);
                    FileStream fileStream = new FileStream(filePath, FileMode.Create);
                    model.UploadImage.CopyTo(fileStream);
                    fileStream.Close();
                }
                User user = new User();
                if (imageName != "noimage.png")
                {
                    user.Image = imageName;
                }

                _genericRepository.Update(user);
                TempData["Success"] = "The user has been updated..!";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["Error"] = "The user has not been edited..!";
                return View(model);
            }
        }
    }
}
