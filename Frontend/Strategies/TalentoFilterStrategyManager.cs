using System.Collections.Generic;
using System.Linq;

namespace Frontend.Strategies
{
    public class TalentoFilterStrategyManager
    {
        private readonly List<ITalentoFilterStrategy> _availableStrategies;
        
        public TalentoFilterStrategyManager()
        {
            _availableStrategies = new List<ITalentoFilterStrategy>
            {
                new NameFilterStrategy(),
                new CountryFilterStrategy(),
                new PriceRangeFilterStrategy(),
                new HabilidadeFilterStrategy()
            };
        }
        
        public IEnumerable<ITalentoFilterStrategy> GetAllStrategies() => _availableStrategies;
        
        public ITalentoFilterStrategy GetStrategyByName(string name)
        {
            return _availableStrategies.FirstOrDefault(s => 
                s.FilterName.Equals(name, System.StringComparison.OrdinalIgnoreCase));
        }
        
        // Facilmente extensível para adicionar novas estratégias
        public void RegisterStrategy(ITalentoFilterStrategy strategy)
        {
            if (!_availableStrategies.Any(s => s.GetType() == strategy.GetType()))
            {
                _availableStrategies.Add(strategy);
            }
        }
    }
}