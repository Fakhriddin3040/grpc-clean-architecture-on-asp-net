using AuthMicroservice.Domain.Interfaces.Entities;

namespace AuthMicroservice.Domain.Interfaces.Services
{
	public interface IService<TEntity, TCreateDTO> where TEntity : IGuid
	{
		IQueryable<TEntity> GetAll();
		TEntity GetDetail(Guid id);
		bool Create(TCreateDTO entity);
		bool Update(Guid id, TEntity entity);
		bool Delete(Guid id);
		
	}
}