using System.Collections.Generic;

namespace Eleks.Yurii.Fozekosh.CoreQuizz.DataAccess.Contracts
{
    public interface IDataSaver<T>
    {
        void Save(IList<T> data);
        IList<T> Read();
    }
}