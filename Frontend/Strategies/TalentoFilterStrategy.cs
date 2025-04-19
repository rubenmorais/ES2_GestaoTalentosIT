using Frontend.DTOClasses;
using System.Collections.Generic;

namespace Frontend.Strategies
{
    public interface ITalentoFilterStrategy
    {
        string FilterName { get; } // Nome amigável para a estratégia
        string Description { get; } // Descrição da estratégia
        IEnumerable<TalentoDTO> Filter(IEnumerable<TalentoDTO> talentos, object filterCriteria);
    }
}