using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ObrasBibliograficas.Domain.Authors;
using ObrasBibliograficas.Domain.Interfaces;
using ObrasBibliograficas.Infra;
using ObrasBibliograficas.Infra.Repositories;

namespace ObrasBibliograficas.DI
{
    public class Bootstrap
    {
        public static void Configure(IServiceCollection dependencies, string connection)
        {
            dependencies.AddDbContext<DbContext>(options => options.UseSqlServer(connection));

            dependencies.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            dependencies.AddTransient(typeof(AuthorStore));
            dependencies.AddTransient(typeof(IUnityOfWork), typeof(UnityOfWork));
        }
    }
}
