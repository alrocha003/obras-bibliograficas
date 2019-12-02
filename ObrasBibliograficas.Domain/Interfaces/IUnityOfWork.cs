using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ObrasBibliograficas.Domain.Interfaces
{
    public interface IUnityOfWork
    {
        Task Commit();
    }
}
