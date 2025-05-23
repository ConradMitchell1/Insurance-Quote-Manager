using Castle.Core.Logging;
using Insurance_Quote_Manager.Models.Enums;
using Insurance_Quote_Manager.Interfaces;
using Insurance_Quote_Manager.Models.DTO;
using Insurance_Quote_Manager.Services;
using Moq;
using Microsoft.Extensions.Logging;
using Insurance_Quote_Manager.Models;

namespace InsuranceQuoteManager.Tests
{
    public class ServiceTests
    {
        private readonly Mock<IQuoteCalculationContext> _mockCalcContet = new();
        private readonly Mock<IQuoteRepository> _mockRepository = new();
        private readonly Mock<ILogger<QuoteService>> _mockLogger = new();
        
        [Fact]
        public async Task CreateQuoteAsync_Valid_CallsAddAndSave()
        {
            var request = (CreateQuoteRequest)DefaultRequest(true, isSmoker: true, hasChronicIllness: true);

            var service = NewQuoteService();
            
            await service.CreateQuoteAsync(request);

            _mockCalcContet.Verify(c => c.ApplyCalculation(It.IsAny<QuoteModel>(), request), Times.Once);
            _mockRepository.Verify(r => r.AddAsync(It.IsAny<QuoteModel>()), Times.Once);
            _mockRepository.Verify(r => r.SaveAsync(), Times.Once);
        }
        [Fact]
        public async Task CreateQuoteAsync_Invalid_ThrowsArgumentException()
        {
            var request = (CreateQuoteRequest)DefaultRequest(true, clientAge: 17, email: "fakeemailexample.com");

            var service = NewQuoteService();

            var exception = await Assert.ThrowsAsync<ArgumentException>(() =>
            service.CreateQuoteAsync(request));

            Assert.Contains("ClientAge must be at least 18", exception.Message);
            Assert.Contains("Invalid email format", exception.Message);

            _mockCalcContet.Verify(c => c.ApplyCalculation(It.IsAny<QuoteModel>(), request), Times.Never);
            _mockRepository.Verify(r => r.AddAsync(It.IsAny<QuoteModel>()), Times.Never);
            _mockRepository.Verify(r => r.SaveAsync(), Times.Never);


        }
        [Fact]
        public async Task DeleteQuoteAsync_ValidId_CallsDeleteAndSave()
        {
            var testQuote = new QuoteModel
            {
                Id = 1,
                ClientName = "Jane Doe",
                ClientAge = 22,
                Email = "Fakeemail@example.com",
                PolicyType = PolicyType.Life,
                Status = QuoteStatus.Pending,
                IsSmoker = true,
                HasChronicIllness = false
            };

            _mockRepository.Setup(r => r.GetByIdAsync(testQuote.Id))
                   .ReturnsAsync(testQuote);

            var service = NewQuoteService();

            await service.DeleteQuoteAsync(testQuote.Id);
            _mockRepository.Verify(r => r.GetByIdAsync(testQuote.Id), Times.Once);
            _mockRepository.Verify(r => r.DeleteAsync(testQuote), Times.Once);
            _mockRepository.Verify(r => r.SaveAsync(), Times.Once);
        }
        [Fact]
        public async Task DeleteQuoteAsync_InvalidId_ThrowArgumentException()
        {
            int invalidId = 999;
            _mockRepository.Setup(r => r.GetByIdAsync(invalidId))
                           .ReturnsAsync((QuoteModel?)null);
            var service = NewQuoteService();

            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() =>
                service.DeleteQuoteAsync(invalidId)
            );
            Assert.Contains($"Quote with ID: {invalidId} not found", exception.Message);

            _mockRepository.Verify(r => r.DeleteAsync(It.IsAny<QuoteModel>()), Times.Never);
            _mockRepository.Verify(r => r.SaveAsync(), Times.Never);

        }
        [Fact]
        public async Task UpdateQuoteAsync_ValidUpdate_CallsUpdateAndSave()
        {
            var request = (UpdateQuoteRequest)DefaultRequest(false);

            var existingQuote = new QuoteModel();

            _mockRepository.Setup(r => r.GetByIdAsync(request.Id))
                   .ReturnsAsync(existingQuote);

            var service = NewQuoteService();


            await service.UpdateQuoteAsync(request);
            _mockRepository.Verify(r => r.GetByIdAsync(request.Id), Times.Once);
            _mockCalcContet.Verify(c => c.ApplyCalculation(existingQuote, request), Times.Once);
            _mockRepository.Verify(r => r.SaveAsync(), Times.Once);
        }
        [Fact]
        public async Task UpdateQuoteAsync_InvalidUpdate_ThrowArgumentException()
        {
            var request = (UpdateQuoteRequest)DefaultRequest(false, clientAge: 101, clientName:"");
            var existingQuote = new QuoteModel();

            _mockRepository.Setup(r => r.GetByIdAsync(request.Id))
                   .ReturnsAsync(existingQuote);
            var service = NewQuoteService();

            var exception = await Assert.ThrowsAsync<ArgumentException>(() =>
            service.UpdateQuoteAsync(request));

            Assert.Contains("ClientAge must be at least 18 and below 100", exception.Message);
            Assert.Contains("ClientName is required", exception.Message);

            _mockRepository.Verify(r => r.GetByIdAsync(request.Id), Times.Never);
            _mockCalcContet.Verify(c => c.ApplyCalculation(existingQuote, request), Times.Never);
            _mockRepository.Verify(r => r.SaveAsync(), Times.Never);

        }

        public IQuoteRequest DefaultRequest(bool isCreate, string clientName = "Jane", int clientAge = 20, string email = "fakeemail@example.com", PolicyType policyType = PolicyType.Life, QuoteStatus quoteStatus = QuoteStatus.Pending, bool isSmoker = false, bool hasChronicIllness = false)
        {
            if (isCreate)
            {
                return new CreateQuoteRequest
                {
                    ClientName = clientName,
                    ClientAge = clientAge,
                    Email = email,
                    ExpiryDate = new DateTime(2027, 10, 10),
                    PolicyType = policyType,
                    QuoteStatus = quoteStatus,
                    IsSmoker = isSmoker,
                    HasChronicIllness = hasChronicIllness
                };
            }
            else
            {
                return new UpdateQuoteRequest
                {
                    Id = 1,
                    ClientName = clientName,
                    ClientAge = clientAge,
                    Email = email,
                    PolicyType = policyType,
                    QuoteStatus = quoteStatus,
                    CoverageDurationMonths = 12,
                    IsSmoker= isSmoker,
                    HasChronicIllness= hasChronicIllness
                };
            }
        }

        public QuoteService NewQuoteService()
        {
            return new QuoteService(
                _mockRepository.Object,
                _mockCalcContet.Object,
                _mockLogger.Object);
        }
    }
}
