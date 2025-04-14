using WebAPI.DTOClasses;
using WebAPI.Interfaces;
using DbLayer.Models;

namespace WebAPI.Services
{
    public class TalentoService
    {
        private readonly ITalentoRepository _repository;

        public TalentoService(ITalentoRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public List<TalentoDTO> GetAllTalentos()
        {
            return _repository.GetAll();
        }

        public TalentoDTO? GetTalentoPorId(int id)
        {
            return _repository.GetById(id);
        }

        public Talento CriarTalento(CreateTalentoDTO dto)
        {
            return _repository.Create(dto);
        }

        public TalentoDTO UpdateTalento(int id, UpdateTalentoDTO dto)
        {
            return _repository.Update(id, dto);
        }

        public void DeleteTalento(int id)
        {
            _repository.Delete(id);
        }
    }
}