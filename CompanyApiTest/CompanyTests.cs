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

        [Fact]
        public async Task Should_Return_2_Companies_On_P1_And_1_Company_On_P2_When_Get_given_3_Companies_Test()
        {
            //given
            TestServer server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            HttpClient client = server.CreateClient();
            Company existingCompany = new Company("SSS");
            Company existingCompany1 = new Company("WWW");
            Company existingCompany2 = new Company("NNN");
            string request = JsonConvert.SerializeObject(existingCompany);
            string request1 = JsonConvert.SerializeObject(existingCompany1);
            string request2 = JsonConvert.SerializeObject(existingCompany2);
            StringContent requestBody = new StringContent(request, Encoding.UTF8, "application/json");
            StringContent requestBody1 = new StringContent(request1, Encoding.UTF8, "application/json");
            StringContent requestBody2 = new StringContent(request2, Encoding.UTF8, "application/json");

            //when
            await client.PostAsync("CompanyApi/companies", requestBody);
            await client.PostAsync("CompanyApi/companies", requestBody1);
            await client.PostAsync("CompanyApi/companies", requestBody2);
            var response = await client.GetAsync("CompanyApi/companies/pageSize?pageIndex=1&pageSize=2");
            var response1 = await client.GetAsync("CompanyApi/companies/pageSize?pageIndex=2&pageSize=2");
            var response2 = await client.GetAsync("CompanyApi/companies/pageSize?pageIndex=3&pageSize=2");

            //then
            response.EnsureSuccessStatusCode();
            response1.EnsureSuccessStatusCode();
            response2.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var responseString1 = await response1.Content.ReadAsStringAsync();
            var responseString2 = await response2.Content.ReadAsStringAsync();
            List<Company> actrualcCompany = JsonConvert.DeserializeObject<List<Company>>(responseString);
            List<Company> actrualcCompany1 = JsonConvert.DeserializeObject<List<Company>>(responseString1);
            List<Company> actrualcCompany2 = JsonConvert.DeserializeObject<List<Company>>(responseString2);
            Assert.Equal(new List<Company>() { existingCompany, existingCompany1 }, actrualcCompany);
            Assert.Equal(new List<Company>() { existingCompany2 }, actrualcCompany1);
            Assert.Equal(null, actrualcCompany2);
        }
    }
}
