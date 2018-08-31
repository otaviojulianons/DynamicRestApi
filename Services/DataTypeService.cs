using Domain;
using Repository.Repositories;
using System.Collections.Generic;

namespace Services
{
    public class DataTypeService
    {
        private ContextRepository<DataTypeDomain> _dataTypeRepository;

        public DataTypeService(ContextRepository<DataTypeDomain> dataTypeRepository)
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
