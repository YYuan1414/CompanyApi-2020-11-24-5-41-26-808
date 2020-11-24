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
            ExistingCompany existingCompany = new ExistingCompany("SSS");
            string request = JsonConvert.SerializeObject(existingCompany);
            StringContent requestBody = new StringContent(request, Encoding.UTF8, "application/json");

            //when
            var response = await client.PostAsync("CompanyApi/companies", requestBody);

            //then
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Company actrualcCompany = JsonConvert.DeserializeObject<Company>(responseString);
            Assert.Equal(existingCompany.Name, actrualcCompany.Name);
        }
    }
}
