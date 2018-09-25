using Domain.Interfaces;
using Domain.ValueObjects;
using System.Collections.Generic;

namespace Domain.Services
{
    public class DataTypeService
    {
        private IRepository<DataTypeDomain> _dataTypeRepository;

        public DataTypeService(IRepository<DataTypeDomain> dataTypeRepository)
            => _dataTypeRepository = dataTypeRepository;

        public void Insert(DataTypeDomain dataType)
            => _dataTypeRepository.Insert(dataType, true);

        public void Update(long id,DataTypeDomain dataType)
            => _dataTypeRepository.Update(id, dataType, true);

        public void Delete(long id)
            => _dataTypeRepository.Delete(id, true);

        public DataTypeDomain GetById(long id)
            => _dataTypeRepository.GetById(id);

        public IEnumerable<DataTypeDomain> GetAll()
            => _dataTypeRepository.GetAll();

    }
}
