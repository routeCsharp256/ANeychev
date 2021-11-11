using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchandiseService.HttpClients.EmployeeService.Interfaces;
using OzonEdu.MerchandiseService.HttpModels;

namespace OzonEdu.MerchandiseService.HttpClients.Stubs
{
    public sealed class EmployeeHttpClientStub : IEmployeeHttpClient
    {
        private List<EmployeeItemResponse> _employees;

        public EmployeeHttpClientStub()
        {
            _employees = new List<EmployeeItemResponse>
            {
                new EmployeeItemResponse
                {
                    id = 1,
                    firstName = "Ivan",
                    lastName = "Sidorov",
                    middleName = "Vladimirovich",
                    birthDay = new DateTime(1978, 12, 12),
                    hiringDate = new DateTime(2021, 11, 11),
                    email = "i.sidorovich@mail.com"
                }
            };
        }

        public async Task<List<EmployeeItemResponse>> GetAllAsync(CancellationToken cancellationToken = default) =>
            await Task.FromResult(_employees);

    }
}