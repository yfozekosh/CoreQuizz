namespace CoreQuizz.BAL.Contracts
{
    internal interface IChainElement
    {
        object Run(object args);
    }

    internal interface IChainElement<in T1, out T2> : IChainElement
    {
        T2 Run(T1 args);
    }
}