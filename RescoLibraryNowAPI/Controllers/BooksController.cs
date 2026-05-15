using RescoLibraryNowAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Reflection.Metadata.BlobBuilder;

namespace RescoLibraryNowAPI.Controllers
{
    [Route("api/v1/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private static List<Books> books = new List<Books>
        {
            new Books { Id = 1, Title = "The Awakening", Author = "Kate Chopin", Genre = "Feminist literature", Available = true, PublishedYear = 1899 },
            new Books { Id = 2, Title = "Broken Promises", Author = "Anne Willow", Genre = "Contemporary, Fiction, New Adult, Young Adult", Available = true, PublishedYear = 2018 },

        };
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(new { status = "success", data = books, message = "Books retrived." });
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var book = books.FirstOrDefault(b => b.Id == id);
            if (book == null)
                return NotFound(new { status = "error", data = (object?)null, message = "Books not found" });
            return Ok(new { status = "success", data = book, message = "Book retrived." });
        }
        [HttpPost]
        public IActionResult Create([FromBody] Books newBooks)
        {
            newBooks.Id = books.Count + 1;
            books.Add(newBooks);
            return CreatedAtAction(nameof(GetById), new { id = newBooks.Id },
                new { status = "success", data = newBooks, message = "Book created." });
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Books updateBook)
        {
            var book = books.FirstOrDefault(b => b.Id == id);
            if (book == null)
                return NotFound(new { status = "error", data = (object?)null, message = "Book not found." });

            book.Title = updateBook.Title;
            book.Author = updateBook.Author;
            book.Genre = updateBook.Genre;
            book.Available = updateBook.Available;
            book.PublishedYear = updateBook.PublishedYear;

            return Ok(new { status = "success", data = book, message = "Book updated." });

        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var book = books.FirstOrDefault(book => book.Id == id);
            if (book == null)
                return NotFound(new { status = "error", data = (object?)null, message = "Book not found." });

            books.Remove(book);
            return Ok(new { status = "success", data = (object?)null, message = "Books deleted." });
        }
    }
}
