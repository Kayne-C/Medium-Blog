using BLL.Repositories.InterfaceRepositories;
using ENTITIES.Entity.Abstract;
using ENTITIES.Entity.Concrete;
using Medium.Managers;
using Medium.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Medium.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IGenericRepository<Article> _genericRepository;

        public ArticleController(IWebHostEnvironment webHostEnvironment, IGenericRepository<Article> genericRepository)
        {
            _webHostEnvironment = webHostEnvironment;
            _genericRepository = genericRepository;
        }
        public IActionResult List()
        {
            var list = _genericRepository.Where(x => x.Status != Status.Passive && x.AuthorId == int.Parse(HttpContext.Session.GetString("userId")));
            return View(list);
        }
        public IActionResult Create(string yonlen)
        {
            ViewBag.yonlen = yonlen;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ArticleCreateViewModel model, string yonlen)
        {
            if (ModelState.IsValid)
            {
                var article = new Article()
                {
                    Title = model.Title,
                    Content = model.Content,
                    AuthorId = int.Parse(HttpContext.Session.GetString("userId")),
                    ArticlePicture = model.ArticlePicture.GetUniqueNameAndSavePhotoToDisk(_webHostEnvironment)
                };
                _genericRepository.Add(article);
                TempData["message"] = "Article Created!";
                if (string.IsNullOrEmpty(yonlen))
                {
                    return RedirectToAction("List");
                }
                return Redirect(yonlen);
            }
            else return View();
        }
        public IActionResult Delete(int id)
        {
            Article article = _genericRepository.Find(id);
            if (article != null)
            {
                _genericRepository.Delete(article);
                TempData["messagedelete"] = "Article Deleted!";
            }
            else
            {
                TempData["messagedeletefail"] = "Article couldn't be deleted!";
            }
            return RedirectToAction("List", "Article");
        }
    }
}
