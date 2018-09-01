using Domain;
using Domain.Helpers;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories;
using System.Linq;

namespace Services
{
    public class LanguageService
    {
        private ContextRepository<LanguageDomain> _languagesRepository;
        private ContextRepository<LanguageDataTypeDomain> _languagesDataTypesRepository;

        private DynamicService _dynamicService;

        public LanguageService(
            ContextRepository<LanguageDomain> languagesRepository,
            ContextRepository<LanguageDataTypeDomain> languagesDataTypesRepository,
            DynamicService dynamicService
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
