using BeatStore.API.Interfaces.DTO.Responses;
using Microsoft.AspNetCore.Mvc;

namespace BeatStore.API.UseCases
{
    public abstract class ABaseUseCase
    {
        public ABaseResponse? OutputPort { get; set; }
    }
}
