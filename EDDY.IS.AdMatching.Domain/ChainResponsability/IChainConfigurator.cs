namespace EDDY.IS.AdMatching.Domain.ChainResponsability
{
    public interface IChainConfigurator<T>
    {
        IChainConfigurator<T> Add<TImplementation>() where TImplementation : T;
        void Configure();
    }
}
