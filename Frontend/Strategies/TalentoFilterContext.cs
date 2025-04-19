using Frontend.DTOClasses;
using System.Collections.Generic;

namespace Frontend.Strategies
{
    public class TalentoFilterContext
    {
        private ITalentoFilterStrategy _strategy;

        public TalentoFilterContext(ITalentoFilterStrategy strategy)
        {
            _strategy = strategy;
        }

        public void SetStrategy(ITalentoFilterStrategy strategy)
        {
            _strategy = strategy;
        }

        public IEnumerable<TalentoDTO> ExecuteFilter(IEnumerable<TalentoDTO> talentos, object filterCriteria)
        {
            return _strategy.Filter(talentos, filterCriteria);
        }
    }
}