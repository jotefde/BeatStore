using Microsoft.AspNetCore.Mvc;

namespace BeatStore.API.Interfaces.DTO.Responses
{
    public interface IBaseResponse
    {
        ActionResult GetResult();
    }
}
