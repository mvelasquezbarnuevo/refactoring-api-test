using System.Collections.Generic;

namespace Sat.Recruitment.Api.Abstractions
{
    public interface IDataStore<out T>
    {
        IEnumerable<T> GetData();
    }
}