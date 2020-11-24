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
        public Company AddCompany(Company company)
        {
            if (companies.Any(it => it.Name == company.Name))
            {
                BadRequest();
                return null;
            }

            companies.Add(company);
            return company;
        }

        [HttpGet("companies")]
        public IEnumerable<Company> GetAllCompanies()
        {
            return companies;
        }
    }
}
