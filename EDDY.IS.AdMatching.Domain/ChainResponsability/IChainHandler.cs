namespace EDDY.IS.AdMatching.Domain.ChainResponsability
{
    public interface IChainHandler<TEntity>
    {
        IChainHandler<TEntity> Next { get; }
        Task Handle(TEntity foo);
    }
}
