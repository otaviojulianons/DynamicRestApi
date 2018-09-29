using Domain.Entities.LanguageAggregate;
using Domain.Interfaces.Infrastructure;
using Domain.Interfaces.Structure;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Domain.Services
{
    public class LanguageService 
    {
        private IRepository<LanguageDomain> _languagesRepository;
        private IRepository<LanguageDataTypeDomain> _languagesDataTypesRepository;

        private IDynamicService _dynamicService;

        public LanguageService(
            IRepository<LanguageDomain> languagesRepository,
            IRepository<LanguageDataTypeDomain> languagesDataTypesRepository,
            IDynamicService dynamicService
            )
        {
            _dynamicService = dynamicService;
            _languagesRepository = languagesRepository;
            _languagesDataTypesRepository = languagesDataTypesRepository;
        }

        public LanguageDomain GetById(long id)
        {
            return _languagesRepository.QueryById(id)
                    .Include(x => x.DataTypes).FirstOrDefault();
        }

        public AttributeTypeLanguage GetTypeLanguage(LanguageDomain language, long idDataType, bool nullable)
        {
            var dataType = language.DataTypes.FirstOrDefault(x => x.DataTypeId == idDataType);
            if (dataType == null)
                return null;
            return new AttributeTypeLanguage()
            {
                Format = dataType.Format,
                Type = nullable ? dataType.NameNullable ?? dataType.Name : dataType.Name
            };
        }


    }
}
