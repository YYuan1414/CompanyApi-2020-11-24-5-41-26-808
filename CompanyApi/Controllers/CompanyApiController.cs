using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CompanyApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CompanyApiController : ControllerBase
    {
        private static IList<Company> companies = new List<Company>();
        [HttpPost("companies")]
        public Company AddCompany(ExistingCompany existingCompany)
        {
            Company company = new Company(existingCompany.Name);
            if (companies.Any(it => it.Name == company.Name))
            {
                BadRequest();
                return null;
            }

            companies.Add(company);
            return company;
        }
    }
}
