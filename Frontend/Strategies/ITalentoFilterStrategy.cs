using Frontend.DTOClasses;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Frontend.Strategies
{
    public class NameFilterStrategy : ITalentoFilterStrategy
    {
        public string FilterName => "Nome";
        public string Description => "Filtra talentos pelo nome";

        public IEnumerable<TalentoDTO> Filter(IEnumerable<TalentoDTO> talentos, object filterCriteria)
        {
            if (filterCriteria == null || !(filterCriteria is string searchTerm) || string.IsNullOrWhiteSpace(searchTerm))
                return talentos;

            return talentos.Where(t => t.Nome.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
        }
    }
    
    public class CountryFilterStrategy : ITalentoFilterStrategy
    {
        public string FilterName => "País";
        public string Description => "Filtra talentos pelo país";

        public IEnumerable<TalentoDTO> Filter(IEnumerable<TalentoDTO> talentos, object filterCriteria)
        {
            if (filterCriteria == null || !(filterCriteria is string country) || string.IsNullOrWhiteSpace(country))
                return talentos;

            return talentos.Where(t => t.Pais.Equals(country, StringComparison.OrdinalIgnoreCase));
        }
    }
    
    public class PriceRangeFilterStrategy : ITalentoFilterStrategy
    {
        public string FilterName => "Faixa de Preço";
        public string Description => "Filtra talentos por faixa de preço";

        public IEnumerable<TalentoDTO> Filter(IEnumerable<TalentoDTO> talentos, object filterCriteria)
        {
            if (filterCriteria == null || !(filterCriteria is PriceRange priceRange))
                return talentos;

            return talentos.Where(t => t.PrecoPorHora >= priceRange.Min && 
                                       (priceRange.Max == 0 || t.PrecoPorHora <= priceRange.Max));
        }
    }
    
    public class PriceRange
    {
        public decimal Min { get; set; }
        public decimal Max { get; set; } // 0 significa sem limite máximo
    }

    public class HabilidadeFilterStrategy : ITalentoFilterStrategy
    {
        public string FilterName => "Habilidade";
        public string Description => "Filtra talentos por habilidades";

        public IEnumerable<TalentoDTO> Filter(IEnumerable<TalentoDTO> talentos, object filterCriteria)
        {
            if (filterCriteria == null || !(filterCriteria is List<int> selectedHabilidadeIds) || !selectedHabilidadeIds.Any())
                return talentos;

            return talentos.Where(t => 
                t.Habilidades != null && 
                t.Habilidades.Any(h => selectedHabilidadeIds.Contains(h.HabilidadeId)));
        }
    }
}