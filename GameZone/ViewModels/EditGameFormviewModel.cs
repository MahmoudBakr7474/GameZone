using GameZone.Attributes;

namespace GameZone.ViewModels
{
    public class EditGameFormviewModel : GameFormViewModel
    {
        public int Id { get; set; }
        public string? CurrentCover { get; set; }

        [allowedExtensions(FileSettings.AllowedExtensions),
            MaxFileSize(FileSettings.MaxFileSizeInBytes)]
        public IFormFile? Cover { get; set; } = default!;

    }
}
