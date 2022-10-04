namespace Microservices.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
        IProductRepository ProductRepository { get; }
    }
}