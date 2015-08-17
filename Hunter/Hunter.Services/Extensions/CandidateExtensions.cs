﻿using System.Linq;
using Hunter.DataAccess.Entities;
using Hunter.DataAccess.Entities.Enums;
using Hunter.Services.Dto;
using System;
using System.Collections.Generic;
using Hunter.Services.Extensions;

namespace Hunter.Services
{
    public static class CandidateExtensions
    {
        public static CandidateDto ToCandidateDto(this Candidate candidate)
        {
            double experiance = candidate.YearsOfExperience ?? 0;
            var dto = new CandidateDto()
            {
                Id = candidate.Id,
                FirstName = candidate.FirstName,
                LastName = candidate.LastName,
                Email = candidate.Email,
                CurrentPosition = candidate.CurrentPosition,
                Company = candidate.Company,
                Location = candidate.Location,
                Skype = candidate.Skype,
                Phone = candidate.Phone,
                Linkedin = candidate.Linkedin,
                Salary = candidate.Salary,
                YearsOfExperience = Math.Round(experiance + candidate.CalculateYearsOfExperiance(), 1),
                ResumeId = candidate.ResumeId != null ? (int)candidate.ResumeId : 0,
                AddedByProfileId = candidate.AddedByProfileId,
                AddedBy = candidate.AddedByProfileId.HasValue ? candidate.UserProfile.UserLogin : "",
                AddDate = candidate.AddDate,
                Cards = candidate.Card.Select(x => x.ToCardDto()).ToList(),
                PoolNames = candidate.Pool.Select(x => x.Name).ToList(),
                Resolution = (int) candidate.Resolution,
                ShortListed = candidate.Shortlisted,
                Origin = (int) candidate.Origin,
                DateOfBirth = candidate.DateOfBirth,
                PhotoUrl = "api/fileupload/pictures/"+candidate.Id
            };
            return dto;
        }

        public static void ToCandidateModel(this CandidateDto dto, Candidate candidate)
        {
            candidate.Id = dto.Id;
            candidate.FirstName = dto.FirstName;
            candidate.LastName = dto.LastName;
            candidate.Email = dto.Email;
            candidate.CurrentPosition = dto.CurrentPosition;
            candidate.Company = dto.Company;
            candidate.Location = dto.Location;
            candidate.Skype = dto.Skype;
            candidate.Phone = dto.Phone;
            candidate.Linkedin = dto.Linkedin;
            candidate.Salary = dto.Salary;
            candidate.YearsOfExperience = dto.YearsOfExperience;
            candidate.ResumeId = dto.ResumeId == 0? (int?)null: dto.ResumeId;
            candidate.AddedByProfileId = dto.AddedByProfileId;
            //Card = dto.Cards.ToList();
            //Pool = new List<Pool>();
            candidate.Resolution = (Resolution)dto.Resolution;
            candidate.Shortlisted = dto.ShortListed;
            candidate.Origin = (Origin)dto.Origin;
            candidate.DateOfBirth = dto.DateOfBirth;
            candidate.EditDate = DateTime.Now;
        }

        public static IEnumerable<CandidateLongListDto> ToDto(this IEnumerable<Card> cards)
        {
            return cards.Select(c => new CandidateLongListDto()
            {
                Id = c.CandidateId,
                FirstName = c.Candidate != null ? c.Candidate.FirstName : "",
                LastName = c.Candidate != null ? c.Candidate.LastName : "",
                Email = c.Candidate != null ? c.Candidate.Email : "",
                Company = c.Candidate != null ? c.Candidate.Company : "",
                Location = c.Candidate != null ? c.Candidate.Location : "",
                Salary = c.Candidate != null ? c.Candidate.Salary : 0,
                YearsOfExperience = c.Candidate != null ? c.Candidate.YearsOfExperience : 0,
                Stage = c.Stage,
                Resolution = c.Candidate != null ? c.Candidate.Resolution.ToString() : "",
                UserAddsCard = c.UserProfile != null ? c.UserProfile.Alias : "",
                AddDate = c.Added,
                PhotoUrl = "api/fileupload/pictures/" + c.Id
            });
        }

        public static CandidateLongListDetailsDto ToCandidateLongListDetailsDto(this Candidate candidate)
        {
            var card = candidate.Card.FirstOrDefault(c => c.Id == candidate.Id);
            
            return new CandidateLongListDetailsDto
            {
                Id = candidate.Id,
                FirstName = candidate.FirstName,
                LastName = candidate.LastName,
                PhotoUrl = "api/fileupload/pictures/" + candidate.Id,
                Salary = candidate.Salary,
                Resolution = Enum.GetName(typeof (Resolution), candidate.Resolution),
                Stage = card != null ? Enum.GetName(typeof(Stage), card.Stage) : "-",
                TestComment = card != null ? (card.Test.FirstOrDefault(t => t.Id == card.Id) != null ? card.Test.FirstOrDefault(t => t.Id == card.Id).Comment : "No test comment") : "No test comment",
                SpecialNotes = card != null ? (card.SpecialNote.FirstOrDefault(n => n.Id == card.Id) != null ? card.SpecialNote.FirstOrDefault(n => n.Id == card.Id).Text : "No special note") : "No special note"
            };
        }

        public static IQueryable<CandidateDto> ToCandidateDtoForQuery(this IQueryable<Candidate> candidate)
        {
            return candidate.Select(c =>


                new CandidateDto()
                {
                    Id = c.Id,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    Email = c.Email,
                    CurrentPosition = c.CurrentPosition,
                    Company = c.Company,
                    Location = c.Location,
                    Skype = c.Skype,
                    Phone = c.Phone,
                    Linkedin = c.Linkedin,
                    Salary = c.Salary,
                    YearsOfExperience =  c.YearsOfExperience,
                    ResumeId = c.ResumeId,
                    AddedByProfileId = c.AddedByProfileId,
                    AddedBy = c.AddedByProfileId.HasValue ? c.UserProfile.UserLogin : "",
                    AddDate = c.AddDate,
                    Cards = c.Card.Select(x =>
                    new CardDto
                    {
                        Id = x.Id,
                        VacancyId = x.VacancyId,
                        CandidateId = x.CandidateId,
                        Added = x.Added,
                        AddedByProfileId = x.AddedByProfileId,
                        Stage = x.Stage
                    }
                    ),
                    PoolNames = c.Pool.Select(x => x.Name),
                    Resolution = (int) c.Resolution,
                    ShortListed = c.Shortlisted,
                    Origin = (int) c.Origin,
                    DateOfBirth = c.DateOfBirth,
                    PhotoUrl = "api/fileupload/pictures/" + c.Id
                }
                );
        }

    }
}