using Microsoft.AspNetCore.Mvc.Rendering;

namespace MyWebApplication.Models.ViewModels;

public class ProductVM
{
    public Product Product { get; set; }
    public IEnumerable<SelectListItem> CategorySelectList { get; set; }
}