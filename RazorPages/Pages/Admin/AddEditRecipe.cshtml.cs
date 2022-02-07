using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using TopsyTurvyCakes.Models;

namespace RazorPages.Pages.Admin
{
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
            Recipe.Id = Id.GetValueOrDefault();
            await _recipesService.SaveAsync(Recipe);

            // После сохранения рецепта произойдет редирект на страницу с рецептом
            return RedirectToPage("/Recipe", new { id = Recipe.Id });
        }

        public async Task<IActionResult> OnPostDelete()
        {
            await _recipesService.DeleteAsync(Id.Value);
            return RedirectToPage("/Index");
        }
    }
}
