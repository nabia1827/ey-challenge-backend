using AutoMapper;
using Challenge.Application.DTO;
using Challenge.Application.Interface;
using Challenge.Domain.Entity;
using Challenge.Domain.Interface;
using Challenge.Transversal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Application.Main
{
    public class CountryApplication : ICountryApplication
    {
        private readonly ICountryDomain _domain;
        private readonly IMapper _mapper;

        public CountryApplication(ICountryDomain domain, IMapper iMapper)
        {
            _domain = domain;
            _mapper = iMapper;

        }

        public async Task<Response<IEnumerable<CountryDto>>> ListCountries()
        {
            var response = new Response<IEnumerable<CountryDto>>();
            try
            {
                var items = await _domain.ListCountries();

                response.Data = _mapper.Map<IEnumerable<CountryDto>>(items);

                if (response.Data != null)
                {
                    response.IsSuccess = true;
                    response.Message = "Successful";
                }

            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return response;
        }
    }
}
