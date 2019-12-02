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
        private IRepository<Author> _repository { get; set; }

        public AuthorController(AuthorStore store, IRepository<Author> repository)
        {
            _store = store;
            _repository = repository;
        }

        [HttpGet()]
        public ActionResult Get()
        {
            var authors = _repository.GetAll();
            var data = authors.Select(a => new AuthorDTO
            {
                Id = a.Id,
                Name = (a.Name.IndexOf(" ") > 0) ? a.Name.ToAuthorName() : a.Name.ToUpper()
            });

            return Ok(data);

        }



        [HttpGet("{id}")]

        public ActionResult GetAuthor(Guid id)
        {
            Author author = _repository.GetById(id);

            if (author == null)
                return BadRequest("Autor inválido");

            return Ok(new AuthorDTO
            {
                Id = author.Id,
                Name = (author.Name.IndexOf(" ") > 0) ? author.Name.ToAuthorName() : author.Name.ToUpper()
            });

        }


        [HttpPost]
        public async Task<ActionResult> CreateOrEdit(Author author)
        {
            if (!ModelState.IsValid) 
                return BadRequest(ModelState);

            try
            {
                _store.Store(dto);
                return Ok("Autor cadastrado");
            }
            catch (Exception ex)
            {
                return BadRequest("Não foi possível cadastrar o autor");
            }
        }
    }
}
