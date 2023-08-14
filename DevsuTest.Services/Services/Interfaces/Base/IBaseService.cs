namespace DevsuTest.Application.Services.Interfaces.Base
{
    public interface IBaseService<TDto> where TDto : class
    {
        Task<TDto> GetById(int entityId);
        Task<IEnumerable<TDto>> GetAll();
        Task<TDto> Create(TDto entityDto);
        Task Delete(int entityId);
    }
}
