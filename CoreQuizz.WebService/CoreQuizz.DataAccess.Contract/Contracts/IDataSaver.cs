using System.Collections.Generic;

namespace CoreQuizz.DataAccess.Contract.Contracts
{
    public interface IDataSaver<T>
    {
        void Save(IList<T> data);
        IList<T> Read();
    }
}