namespace Hunter.DataAccess.Interface
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork Create();
    }
}