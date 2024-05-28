using Microsoft.AspNetCore.Mvc;
using TI_Devops_2024_DemoAspMvc.BLL.Interfaces;
using TI_Devops_2024_DemoAspMvc.Domain.Entities;
using TI_Devops_2024_DemoAspMvc.Mappers;
using TI_Devops_2024_DemoAspMvc.Models;

namespace TI_Devops_2024_DemoAspMvc.Controllers
{
    public class BookController : Controller
    {

        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        public IActionResult Index()
        {
            List<BookShortDTO> books = _bookService.GetAll()
                                            .Select(b => b.ToShortDTO())
                                            .ToList();

            return View(books);
        }

        public IActionResult Add()
        {
            return View(new BookFormDTO());
        }

        [HttpPost]
        public IActionResult Add([FromForm] BookFormDTO form)
        {
            //TODO REMOVE THIS
            form.AuthorId = 1;
            if(!ModelState.IsValid)
            {
                return View(form);
            }
            _bookService.Create(form.ToEntity());
            return RedirectToAction("Index");
        }

        public IActionResult Details(string isbn)
        {
            BookDetailsDTO book = _bookService.GetFullByISBN(isbn).ToDetailsDTO();

            return View(book);
        }
    }
}
