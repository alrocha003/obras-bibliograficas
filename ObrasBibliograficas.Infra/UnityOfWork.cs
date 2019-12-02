using ObrasBibliograficas.Domain.Interfaces;
using ObrasBibliograficas.Infra.Context;
using System;
using System.Threading.Tasks;

namespace ObrasBibliograficas.Infra
{
    public class UnityOfWork : IUnityOfWork
    {
        private readonly AppDbContext _context;

        public UnityOfWork(AppDbContext context) => _context = context;


        public async Task Commit() => await _context.SaveChangesAsync();
    }
}
