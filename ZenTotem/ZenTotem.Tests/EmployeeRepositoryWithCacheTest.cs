using AutoFixture;
using FluentAssertions;
using Moq;
using NLog;
using System.Text.Json;
using Xunit;
using ZenTotem.BusinessLogicLayer;

namespace ZenTotem.Tests
{
    public class EmployeeRepositoryTest
    {

        private readonly Fixture _fixture = new Fixture();

        [Fact]
        public async Task JsonEmployeeTests_Get_Success_With_Cache()
        {
            // Arrange
            var employee = _fixture.Build<Employee>().Create();

            var storageMock = new Mock<IStorageRepository>();
            storageMock.Setup(repo => repo.Get<Employee>(employee.Id))
                .Returns(employee);

            var repository = new JsonEmployeeRepository(null, null, new Mock<ILogger>().Object, storageMock.Object);

            // Act
            var actualEmployee = repository.GetEmployee(employee.Id);

            // Assert
            actualEmployee.Should().NotBeNull();
            actualEmployee.Id.Should().Be(employee.Id);
            actualEmployee.LastName.Should().Be(employee.LastName);
            actualEmployee.SalaryPerHour.Should().Be(employee.SalaryPerHour);
            actualEmployee.FirstName.Should().Be(employee.FirstName);
        }

        [Fact]
        public async Task JsonEmployeeTests_Get_Success_With_Emty_Cache()
        {
            // Arrange
            var employee = _fixture.Build<Employee>().Create();
            var jsonEmployee = JsonSerializer.Serialize(employee);

            var storageMock = new Mock<IStorageRepository>();
            storageMock.Setup(x => x.Get<Employee>(It.IsAny<object>()))
                .Returns<Employee>(null);

            var fileManagerMock = new Mock<IFileManager>();
            fileManagerMock.Setup(x => x.Read(employee.Id))
                .Returns(jsonEmployee);

            var repository = new JsonEmployeeRepository(fileManagerMock.Object, null, new Mock<ILogger>().Object, storageMock.Object);

            // Act
            var actualEmployee = repository.GetEmployee(employee.Id);

            // Assert
            actualEmployee.Should().NotBeNull();
            actualEmployee.Id.Should().Be(employee.Id);
            actualEmployee.LastName.Should().Be(employee.LastName);
            actualEmployee.SalaryPerHour.Should().Be(employee.SalaryPerHour);
            actualEmployee.FirstName.Should().Be(employee.FirstName);
        }

        [Fact]
        public async Task JsonEmployeeTests_Get_NonExsistId()
        {

            // Arrange
            var storageMock = new Mock<IStorageRepository>();
            storageMock.Setup(x => x.Get<Employee>(It.IsAny<object>()))
                .Returns<Employee>(null);

            var fileManagerMock = new Mock<IFileManager>();
            fileManagerMock.Setup(x => x.Read(It.IsAny<int>()))
                .Returns(string.Empty);

            var repository = new JsonEmployeeRepository(fileManagerMock.Object, null, new Mock<ILogger>().Object, storageMock.Object);

            // act/assert
            Assert.Throws<Exception>(() => repository.GetEmployee(1));
        }
    }
}
