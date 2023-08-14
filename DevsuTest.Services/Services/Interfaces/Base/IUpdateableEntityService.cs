using Microsoft.AspNetCore.JsonPatch;

namespace DevsuTest.Application.Services.Interfaces.Base
{
    public interface IUpdateableEntityService<TDto> : IBaseService<TDto> where TDto : class
    {
        Task<TDto> Update(int entityId, TDto entityDto);
        Task<TDto> Patch(int entityId, JsonPatchDocument<TDto> patch);
    }
}
