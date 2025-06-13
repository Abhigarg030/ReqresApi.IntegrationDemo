using Moq;

using ReqresApi.Application.Services;
using ReqresApi.Client.Models;
using ReqresApi.Client.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqresApi.Tests
{
    public class ExternalUserServiceTests
    {
        [Fact]
        public async Task GetUserByIdAsync_ReturnsUser_WhenFound()
        {
            var mockClient = new Mock<ReqresApiClient>(new HttpClient());
            mockClient.Setup(x => x.GetUserByIdAsync(2))
                .ReturnsAsync(new UserDto
                {
                    Id = 2,
                    Email = "janet.weaver@reqres.in",
                    First_Name = "Janet",
                    Last_Name = "Weaver"
                });

            var service = new ExternalUserService(mockClient.Object);
            var result = await service.GetUserByIdAsync(2);

            Assert.NotNull(result);
            Assert.Equal("Janet Weaver", result.FullName);
        }
    }
}
