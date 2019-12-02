using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ObrasBibliograficas.Domain.Authors;
using ObrasBibliograficas.Domain.DTOS;
using ObrasBibliograficas.Domain.Interfaces;
using ObrasBibliograficas.Infra.Utils;

namespace ObrasBibliograficas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private AuthorStore _authorStore { get; set; }
        private IRepository<Author> _authorRepository { get; set; }

        public AuthorController(AuthorStore store, IRepository<Author> repository)
        {
            _authorStore = store;
            _authorRepository = repository;
        }

        [HttpGet()]
        public ActionResult Get()
        {
            var authors = _authorRepository.GetAll();
            var viewModel = authors.Select(a => new AuthorDTO
            {
                Id = a.Id,
                Name = (a.Name.IndexOf(" ") < 0)
                        ? a.Name.ToUpper()
                        : a.Name.ToAuthorName()
            });

            return Ok(viewModel);

        }



        [HttpGet("GetAuthor")]

        public ActionResult GetAuthor(Guid id)
        {
            Author author = _authorRepository.GetById(id);

            if (author == null)
                return BadRequest("Author invalid");

            return Ok(new AuthorDTO
            {
                Id = author.Id,
                Name = (author.Name.IndexOf(" ") < 0) ? author.Name.ToUpper() : author.Name.ToAuthorName()
            });

        }


        [HttpPost("CreateOrEdit")]
        public async Task<ActionResult> CreateOrEdit(Author dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                _authorStore.Store(dto);
                return Ok("Author Created");
            }

            catch (Exception ex)
            {
                return BadRequest("Couldn't create author");
            }
        }
    }
}
