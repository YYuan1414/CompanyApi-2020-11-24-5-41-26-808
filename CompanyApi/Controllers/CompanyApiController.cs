﻿using System;
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
        public Company GetAllCompanies(int index)
        {
            if (index > companies.Count)
            {
                BadRequest();
                return null;
            }

            return companies[index - 1];
        }

        [HttpGet("companies/pageSize")]
        public List<Company> GetAllCompanies(int pageIndex, int pageSize)
        {
            if (companies.Count == 0)
            {
                return null;
            }

            var startNumber = (pageIndex - 1) * pageSize;
            if (pageIndex > Math.Ceiling((double)companies.Count / pageSize))
            {
                return null;
            }

            var count = companies.Count > pageSize * pageIndex ? pageSize : companies.Count - startNumber;
            var companiesTable = companies.ToList().GetRange(startNumber, count);
            return companiesTable;
        }
    }
}
