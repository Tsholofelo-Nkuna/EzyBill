using EzyBill.BLL.Interfaces;
using EzyBill.BLL.Models.DataTranserObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using System.Text.Json;

namespace EzyBill.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize(AuthenticationSchemes = "gitOuth")]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly ICustomerService _customerService;
        public CustomerController(ILogger<CustomerController> logger, ICustomerService customerService)
        {
            _logger = logger;
            _customerService = customerService;
        }

        [HttpPost("[action]")]
        public PageReponseDto<CustomerDto> GetCustomers(PagingPageQueryDto<CustomerDto> pageQuery){
            try
            {
                var results = this._customerService
                    .GetCustomers(pageQuery, out var count);
                return new PageReponseDto<CustomerDto> { Data = results, Message = Message.Success, Ok = true,TotalRecordCount = count };
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, $"failed input: {JsonSerializer.Serialize(pageQuery)}");
                return new(){Data = Enumerable.Empty<CustomerDto>(), Ok = false, Message = Message.ContactAdminError };
            }
        }

        [HttpPost("[action]")]
        public ResponseDto<bool> Add(List<CustomerDto> customers)
        {
            try
            {
                var isSuccess = this._customerService.Add(customers);
              
                return new ResponseDto<bool> { Data = isSuccess, Ok = isSuccess, Message = isSuccess? Message.SaveSuccess : Message.PotentialSaveError};
            }
            catch(Exception ex)
            {
                this._logger.LogError(ex, $"failed input: {JsonSerializer.Serialize(customers)}");
                return new ResponseDto<bool> { Data = false, Errors = new List<string> { Message.ContactAdminError } };
            }
        }

        [HttpPost("[action]")]
        public ResponseDto<bool> Update(List<CustomerDto> customers)
        {
            try
            {
                var updated = this._customerService.Update(customers);
                return new ResponseDto<bool> { Data = updated, Message = Message.SaveSuccess, Ok = true };
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, $"failed input: {JsonSerializer.Serialize(customers)}");
                return new ResponseDto<bool> { Data = false , Errors = new List<string> { Message.ContactAdminError } };

            }
        }

        [HttpPost("[action]")]
        public ResponseDto<bool> Delete(IEnumerable<Guid> ids)
        {
            try
            {
                var deleted = this._customerService.Delete(ids);
                return new ResponseDto<bool> { Data = deleted, Message = Message.Success, Ok = true };
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, $"failed input: {JsonSerializer.Serialize(ids)}");
                return new ResponseDto<bool> { Data = false, Errors = new List<string> { Message.ContactAdminError } };

            }
        }
    }
}
