using System;

namespace Hunter.DataAccess.Entities
{
    public interface IEntity
    {
        Int32 Id { get; set; }
    }

    public interface ISoftDeleteEntity
    {
        bool IsDeleted { get; set; }
    }


    public abstract class BaseSoftDeleteEntity: IEntity, ISoftDeleteEntity
    {
        public int Id { get; set; }

        public bool IsDeleted { get; set; }
    }
}