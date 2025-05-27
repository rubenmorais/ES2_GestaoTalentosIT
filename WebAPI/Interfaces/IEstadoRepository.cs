using WebAPI.DTOClasses;
using System.Collections.Generic;

namespace WebAPI.Interfaces
{
    public interface IEstadoRepository
    {
        List<EstadoDTO> GetAllEstados();
    }
}