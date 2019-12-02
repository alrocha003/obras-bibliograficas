using ObrasBibliograficas.Domain.DTOS;
using ObrasBibliograficas.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ObrasBibliograficas.Domain.Authors
{
    public class AuthorStore
    {
        private readonly IRepository<Author> _authorRepository;

        public AuthorStore(IRepository<Author> authorRepository)
        {
            _authorRepository = authorRepository;
        }


        public void Store(Author author) => _authorRepository.Save(author);

    }
}
