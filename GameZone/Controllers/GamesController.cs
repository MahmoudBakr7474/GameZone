using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace GameZone.Controllers
{
    public class GamesController : Controller
    {
       
        private readonly ICategoriesService _categoriesService;
        private readonly IDeviceService _deviceServices;
        private readonly IGameServices _gameService;

        public GamesController(IDeviceService device, 
            ICategoriesService categoriesService, IGameServices gameService)
        {
            _deviceServices = device;
            _categoriesService = categoriesService;
            _gameService = gameService;
        }

        public IActionResult Index()
        {
            var games = _gameService.GetAll();
            return View(games);
        }

        public IActionResult Details(int id)
        {
            var game = _gameService.GetById(id);
            if (game is null)
            {
                return NotFound();

            }

            return View(game);
        }


        [HttpGet]
        public IActionResult Create() 
        {

            CreateGameFormViewModel viewModel = new()
            {
                Categories = _categoriesService.GetSelectList(),
                Devices = _deviceServices.GetSelectListItems(),

            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>  Create(CreateGameFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = _categoriesService.GetSelectList();
                model.Devices = _deviceServices.GetSelectListItems();
                return View(model);
            }
            await _gameService.Create(model);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int id) 
        {
            var game = _gameService.GetById(id);
            if (game is null)
                return NotFound();

            EditGameFormviewModel viewModel = new()
            {
                Id = id,
                Name = game.Name,
                Description = game.Description,
                CategoryID = game.CategoryID,
                SelectedDevices = game.Devices.Select(d => d.DeviceId).ToList(),
                Categories = _categoriesService.GetSelectList(),
                Devices = _deviceServices.GetSelectList(),
                CurrentCover = game.Cover,

            };
            return View(viewModel);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditGameFormviewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = _categoriesService.GetSelectList();
                model.Devices = _deviceServices.GetSelectListItems();
                return View(model);
            }
            var game = await _gameService.Update(model);
            if (game is null)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            
            var isDeleted = _gameService.Delete(id);

            return isDeleted ? Ok() : BadRequest();
        }
    }
}
