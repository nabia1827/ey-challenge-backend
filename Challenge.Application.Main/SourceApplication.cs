using AutoMapper;
using Challenge.Application.DTO;
using Challenge.Application.Interface;
using Challenge.Domain.Entity;
using Challenge.Domain.Interface;
using Challenge.Infrastructure.Interface;
using Challenge.Transversal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Application.Main
{
    public class SourceApplication : ISourceApplication
    {
        private readonly ISourceDomain _domain;
        private readonly IMapper _mapper;

        public SourceApplication(ISourceDomain domain, IMapper iMapper)
        {
            _domain = domain;
            _mapper = iMapper;
            
        }

        public async Task<Response<IEnumerable<SourceDto>>> ListSources()
        {
            var response = new Response<IEnumerable<SourceDto>>();
            try
            {
                var items = await _domain.ListSources();

                response.Data = _mapper.Map<IEnumerable<SourceDto>>(items);

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
