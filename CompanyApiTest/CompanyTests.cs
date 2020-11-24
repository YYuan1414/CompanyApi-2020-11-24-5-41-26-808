using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CompanyApi;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using Xunit;

namespace CompanyApiTest
{
    public class CompanyTests
    {
        [Fact]
        public async Task Should_Return_New_Company_When_Add_Exsiting_Company_Test()
        {
            //given
            TestServer server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            HttpClient client = server.CreateClient();
            Company existingCompany = new Company("SSS");
            string request = JsonConvert.SerializeObject(existingCompany);
            StringContent requestBody = new StringContent(request, Encoding.UTF8, "application/json");

            //when
            var response = await client.PostAsync("CompanyApi/companies", requestBody);

            //then
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Company actrualcCompany = JsonConvert.DeserializeObject<Company>(responseString);
            Assert.Equal(existingCompany, actrualcCompany);
        }

        [Fact]
        public async Task Should_Return_All_Companies_When_Get_Companies_Test()
        {
            //given
            TestServer server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            HttpClient client = server.CreateClient();
            Company existingCompany = new Company("SSS");
            Company existingCompany1 = new Company("WWW");
            string request = JsonConvert.SerializeObject(existingCompany);
            string request1 = JsonConvert.SerializeObject(existingCompany1);
            StringContent requestBody = new StringContent(request, Encoding.UTF8, "application/json");
            StringContent requestBody1 = new StringContent(request1, Encoding.UTF8, "application/json");

            //when
            await client.PostAsync("CompanyApi/companies", requestBody);
            await client.PostAsync("CompanyApi/companies", requestBody1);
            var response = await client.GetAsync("CompanyApi/companies");

            //then
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            List<Company> actrualcCompany = JsonConvert.DeserializeObject<List<Company>>(responseString);
            Assert.Equal(new List<Company>() { existingCompany, existingCompany1 }, actrualcCompany);
        }

        [Fact]
        public async Task Should_Return_The_Company_When_Get_Company_Test()
        {
            //given
            TestServer server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            HttpClient client = server.CreateClient();
            Company existingCompany = new Company("SSS");
            Company existingCompany1 = new Company("WWW");
            string request = JsonConvert.SerializeObject(existingCompany);
            string request1 = JsonConvert.SerializeObject(existingCompany1);
            StringContent requestBody = new StringContent(request, Encoding.UTF8, "application/json");
            StringContent requestBody1 = new StringContent(request1, Encoding.UTF8, "application/json");

            //when
            await client.PostAsync("CompanyApi/companies", requestBody);
            await client.PostAsync("CompanyApi/companies", requestBody1);
            var response = await client.GetAsync("CompanyApi/companies?index=2");

            //then
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Company actrualcCompany = JsonConvert.DeserializeObject<Company>(responseString);
            Assert.Equal(existingCompany1, actrualcCompany);
        }
    }
}
