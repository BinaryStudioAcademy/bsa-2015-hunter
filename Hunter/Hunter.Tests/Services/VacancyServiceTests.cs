﻿using System.Collections.Generic;
using System.Linq;
using Hunter.Common.Interfaces;
using Hunter.DataAccess.Entities;
using Hunter.DataAccess.Interface;
using Hunter.DataAccess.Interface.Base;
using Hunter.Services;
using Hunter.Services.Interfaces;
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
            var cardsList = new List<Card> { new Card { Id = 1, VacancyId = 1 }, new Card { Id = 2, VacancyId = 1} };

            _vacancyRepository.Get(Arg.Any<int>()).Returns(new Vacancy { Id = 1, Name = "Vacancy name", Card = cardsList });

            _candidateRepository.All().Returns(new List<Candidate>()
            {
                new Candidate { Id = 1, FirstName = "Name 1", Card = cardsList },
                new Candidate { Id = 2, FirstName = "Name 2", Card = new List<Card> { cardsList[0] } },
                new Candidate { Id = 3, FirstName = "Name 3", Card = new List<Card> { cardsList[1] } },
                new Candidate { Id = 3, FirstName = "Name 3", Card = new List<Card> { new Card { Id = 3, VacancyId = 2} } }
            });
        }

        [Test]
        public void Long_list_for_vacancy_Should_correctly_returns_When_long_list_is_getted_by_vacancy_id()
        {
            // Arrange

            // Act
            var result = _vacancyService.GetLongList(1);

            // Assert
            Assert.True(result.CandidateLongListDto.Count() == 3); 
        }
    }
}
