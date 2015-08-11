using Hunter.DataAccess.Interface;
using Hunter.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hunter.Services.Interfaces;
using Hunter.Common.Interfaces;
using Hunter.DataAccess.Entities;
using Hunter.DataAccess.Interface.Base;
using NSubstitute;
using NUnit.Framework;

namespace Hunter.Tests.Services
{
    class VacancyServiceTests
    {
        private readonly IVacancyRepository _vacancyRepository;
        private readonly ICandidateRepository _candidateRepository;
        private readonly IVacancyService _vacancyService;
        private readonly ICandidateService _candidateService;

        public VacancyServiceTests()
        {
            _vacancyRepository = Substitute.For<IVacancyRepository>();
            _candidateRepository = Substitute.For<ICandidateRepository>();

            var unitOfWork = Substitute.For<IUnitOfWork>();
            var logger = Substitute.For<ILogger>();

            _vacancyService = new VacancyService(_vacancyRepository, _candidateRepository, logger, unitOfWork);
        }

        [SetUp]
        public void TestSetup()
        {
            _vacancyRepository.Get(Arg.Any<int>()).Returns(new Vacancy { Id = 1, Name = "Vacancy name", Card = new List<Card> { new Card { Id = 1 }, new Card { Id = 2 } } });

            _candidateRepository.All().Returns(new List<Candidate>()
            {
                new Candidate { Id = 1, FirstName = "Name 1", Card = new List<Card> { new Card { Id = 1} , new Card { Id = 2 } } },
                new Candidate { Id = 2, FirstName = "Name 2", Card = new List<Card> { new Card { Id = 1 } } },
                new Candidate { Id = 3, FirstName = "Name 3", Card = new List<Card> { new Card { Id = 3 } } }
            });
        }

        [Test]
        public void Long_list_for_vacancy_Should_correctly_returns_When_long_list_is_getted_by_vacancy_id()
        {
            // Arrange

            // Act
            var vacancy = _vacancyService.GetLongList(1);

            // Assert
            Assert.AreEqual(1, vacancy.Id);
        }
    }
}
