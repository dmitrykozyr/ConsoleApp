using Microsoft.AspNetCore.Mvc;
using MVC.Data;
using MVC.Models;

namespace MVC.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public CategoryController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> categories = _dbContext.Categories.ToList();
            return View(categories);
        }

        // GET Открываем страницу создания категории
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // Защита от злоумышленников
        public IActionResult Create(Category category)
        {
            // Кастомная валидация
            if (category.Name == category.DisplayOrder.ToString())
            {
                // Первый аргумент - свойство модели, под которым будет отображаться сообщение об ошибке
                ModelState.AddModelError("DisplayOrder", "'DisplayOrder' and 'Name' can't be equal");
            }

            // Программа смотрит на атрибуты валидации в модели Category
            // Поле Name там указано как обязательное
            if (!ModelState.IsValid)
            {
                return View(category);
            }

            _dbContext.Add(category);
            _dbContext.SaveChanges();

            // Выводим это сообщение в Index.cshtml, ищем TempData по ключу 'success'
            // Сообщение исчезнет после перезагрузки страницы
            TempData["success"] = "Category created successfully";

            return RedirectToAction("Index"); // Переходим на страницу Index текущего контроллера
            // Если нужно перейти на страницу другого контроллера, указываем его вторым параметром RedirectToAction("", "")
        }

        // GET Открываем страницу изменения категории
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            //var category = _dbContext.Categories.FirstOrDefault(c => c.Id == id);
            var category = _dbContext.Categories.Find(id); // Find ищет по Primary Key

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("DisplayOrder", "'DisplayOrder' and 'Name' can't be equal");
            }

            if (!ModelState.IsValid)
            {
                return View(category);
            }

            _dbContext.Update(category);

            _dbContext.SaveChanges();

            TempData["success"] = "Category updated successfully";

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var category = _dbContext.Categories.Find(id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var category = _dbContext.Categories.Find(id);

            if (category == null)
            {
                return NotFound();
            }

            _dbContext.Categories.Remove(category);

            _dbContext.SaveChanges();

            TempData["success"] = "Category deleted successfully";

            return RedirectToAction("Index");
        }
    }
}
