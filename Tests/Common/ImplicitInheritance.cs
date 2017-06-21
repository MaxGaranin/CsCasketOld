using System.Collections;
using System.Collections.Generic;

namespace Tests45.Common
{
    public interface IElementParametersRepository
    {
        void BatchDelete(IEnumerable ids);
    }

    public class BaseLiteDbRepository
    {
        public void BatchDelete(IEnumerable ids)
        {
        }
    }

    public class ElementParametersRepository : BaseLiteDbRepository, IElementParametersRepository
    {
        
    }

    public class ElementParameters
    {
        public int Id { get; set; }

        public double Value { get; set; }
    }

    public class Work
    {
        private readonly IElementParametersRepository _elementParametersRepository;

        public Work(IElementParametersRepository elementParametersRepository)
        {
            _elementParametersRepository = elementParametersRepository;
        }

        public void Run()
        {
            _elementParametersRepository.BatchDelete(new List<int>());
        }
    }
}