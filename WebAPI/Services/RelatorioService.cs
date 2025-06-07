using WebAPI.DTOClasses;
using WebAPI.Interfaces;
using System;
using System.Collections.Generic;

namespace WebAPI.Services
{
    public class RelatorioService
    {
        private readonly IRelatorioRepository _relatorioRepository;

        public RelatorioService(IRelatorioRepository relatorioRepository)
        {
            _relatorioRepository = relatorioRepository ?? throw new ArgumentNullException(nameof(relatorioRepository));
        }

        public List<RelatorioPrecoMedioDTO> GetRelatorioPrecoMedio()
        {
            try
            {
                return _relatorioRepository.GetRelatorioPrecoMedio();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao gerar relatório: {ex.Message}");
            }
        }
    }
} 