using Domain.Interfaces.Structure;
using Domain.ValueObjects;
using System.Collections.Generic;

namespace Domain.Services
{
    public class DataTypeAppService
    {
        private IRepository<DataTypeDomain> _dataTypeRepository;

        internal DataTypeAppService(IRepository<DataTypeDomain> dataTypeRepository)
            => _dataTypeRepository = dataTypeRepository;

        public void Insert(DataTypeDomain dataType)
            => _dataTypeRepository.Insert(dataType, true);

        public void Update(DataTypeDomain dataType)
            => _dataTypeRepository.Update( dataType, true);

        public void Delete(long id)
            => _dataTypeRepository.Delete(id, true);

        public DataTypeDomain GetById(long id)
            => _dataTypeRepository.GetById(id);

        public IEnumerable<DataTypeDomain> GetAll()
            => _dataTypeRepository.GetAll();

    }
}
