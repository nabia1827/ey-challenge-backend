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
    public class SupplierApplication : ISupplierApplication
    {
        private readonly ISupplierDomain _domain;
        private readonly IMapper _mapper;

        public SupplierApplication(ISupplierDomain domain, IMapper iMapper)
        {
            _domain = domain;
            _mapper = iMapper;

        }

        public async Task<Response<bool>> DeleteSupplier(int supplierId)
        {
            var response = new Response<bool>();
            try
            {
                var r = await _domain.DeleteSupplier(supplierId);

                response.Data = r;

                if (response.Data == true)
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

        public async Task<Response<SupplierDto>> GetSupplier(int supplierId)
        {

            var response = new Response<SupplierDto>();
            try
            {
                var items = await _domain.GetSupplier(supplierId);

                response.Data = _mapper.Map<SupplierDto>(items);

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

        public async Task<ResponsePaginated<IEnumerable<SupplierDto>>> ListSuppliers(int pagina, int cantidadRegistros, string legalName, string tradeName, string taxIdentNumber, int countryId, string initDate, string endDate)
        {
            var response = new ResponsePaginated<IEnumerable<SupplierDto>>();
            try
            {
                var suppliers = await _domain.ListSuppliers(pagina, cantidadRegistros,
                    legalName, tradeName, taxIdentNumber, countryId, initDate, endDate);
                response.Data = _mapper.Map<IEnumerable<SupplierDto>>(suppliers.Items);
                if (response.Data != null)
                {
                    response.PageIndex = pagina;
                    response.PageSize = cantidadRegistros;
                    response.Count = suppliers.TotalSize;
                    response.PageCount = (suppliers.TotalSize + cantidadRegistros - 1) / cantidadRegistros;
                    response.IsSuccess = true;
                    response.Message = "Successful";
                }
                else
                {
                    response.IsSuccess = true;
                    response.Message = "No data";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<Response<bool>> UpsertSupplier(int supplierId, string legalName, 
            string tradeName, string taxIdentNumber, string phoneNumber, 
            string email, string website, string address, int countryId, 
            float revenue)
        {
            var response = new Response<bool>();
            try
            {
                var r = await _domain.UpsertSupplier(supplierId, legalName,
                    tradeName, taxIdentNumber, phoneNumber, email, website, 
                    address, countryId, revenue);

                response.Data = r;

                if (response.Data == true)
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
