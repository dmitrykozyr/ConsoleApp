using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using TopsyTurvyCakes.Models;

namespace RazorPages.Pages.Admin
{
    [Authorize] // Включаем аутентификацию
    public class AddEditRecipeModel : PageModel
    {
        [FromRoute]
        public long? Id { get; set; }

        public bool IsNewRecipe
        {
            get { return Id == null; }
        }

        [BindProperty]
        public Recipe Recipe { get; set; }

        [BindProperty]
        public IFormFile Image { get; set; }

        private readonly IRecipesService _recipesService;

        public AddEditRecipeModel(IRecipesService recipesService)
        {
            _recipesService = recipesService;
        }

        public async Task OnGetAsync()
        {
            Recipe = await _recipesService.FindAsync(Id.GetValueOrDefault()) ?? new Recipe();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var recipe = await _recipesService.FindAsync(Id.GetValueOrDefault()) ?? new Recipe();
            await _recipesService.SaveAsync(recipe);

            recipe.Name         = Recipe.Name;
            recipe.Description  = Recipe.Description;
            recipe.Ingredients  = Recipe.Ingredients;
            recipe.Directions   = Recipe.Directions;

            if (Image != null)
            {
                using (var stream = new System.IO.MemoryStream())
                {
                    await Image.CopyToAsync(stream);
                    recipe.Image = stream.ToArray();
                    recipe.ImageContentType = Image.ContentType;
                }
            }

            // После сохранения рецепта произойдет редирект на страницу с рецептом
            return RedirectToPage("/Recipe", new { id = recipe.Id });
        }

        public async Task<IActionResult> OnPostDelete()
        {
            await _recipesService.DeleteAsync(Id.Value);
            return RedirectToPage("/Index");
        }
    }
}
