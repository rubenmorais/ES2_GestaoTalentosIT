using WebAPI.DTOClasses;
using System.Collections.Generic;

namespace WebAPI.Interfaces
{
    public interface IRelatorioRepository
    {
        List<RelatorioPrecoMedioDTO> GetRelatorioPrecoMedio(
            decimal? minPrecoHora = null,
            decimal? maxPrecoHora = null
            );
    }
} 