using System.Collections.Generic;
using Hunter.Tools.LinkedIn;
using NUnit.Framework;

namespace Hunter.Tests.Tools
{
    [TestFixture]
    class LinkedInAccessTests
    {
        [Test]
        public void Should_give_correct_name_When_set_url()
        {
            // Arrange
            var info = new PersonInformation("https://ua.linkedin.com/pub/myroslav-dmytrus/b7/a02/436");
            // Act
            var name = info.Name;

            // Assert
            Assert.AreEqual("Myroslav Dmytrus", name);
        }

        [Test]
        public void Should_give_correct_time_on_work_name_When_no_history_of_work_on_page()
        {
            // Arrange
            var info = new PersonInformation("https://ua.linkedin.com/pub/myroslav-dmytrus/b7/a02/436");
            // Act
            var time = info.ExperienceTime;

            // Assert
            Assert.AreEqual(null, time);
        }

        [Test]
        public void Should_give_null_on_every_property_When_bad_url()
        {
            // Arrange
            var info = new PersonInformation("https://www.linkedin.com/profile/view?id=417544170");
            // Act

            // Assert
            Assert.AreEqual(null, info.Name);
            Assert.AreEqual(null, info.Headline);
            Assert.AreEqual(null, info.Industry);
        }

        [Test]
        public void Should_give_correct_education_When_set_url()
        {
            // Arrange
            var info = new PersonInformation("https://ua.linkedin.com/pub/myroslav-dmytrus/b7/a02/436");
            // Act
            var edu = info.Education;
            List<string> checkEdu = new List<string>()
            {
                "National Forestry University of UkrainianMaster's degree, IT2013 - 2015",
                "National Forestry University of UkraineBachelor's degree, Computer science2009 - 2013"
            };

            // Assert
            Assert.AreEqual(checkEdu, edu);
        }

        [Test]
        public void Should_give_correct_location_When_set_url()
        {
            // Arrange
            var info = new PersonInformation("https://ua.linkedin.com/pub/myroslav-dmytrus/b7/a02/436");
            // Act
            var loc = info.Location;
            var checkLoc = "Ukraine";

            // Assert
            Assert.AreEqual(checkLoc, loc);
        }


        [Test]
        public void Should_give_correct_img_url_When_set_url()
        {
            // Arrange
            var info = new PersonInformation("https://ua.linkedin.com/pub/myroslav-dmytrus/b7/a02/436");
            // Act
            var img = info.Img;
            var checkImg =
                "https://media.licdn.com/mpr/mpr/shrinknp_400_400/AAEAAQAAAAAAAAN4AAAAJDQwMTI2ODUzLWI2MWItNDZkYS05YWNhLTJjMDZjOWFkMTZlNA.jpg";


            // Assert
            Assert.AreEqual(checkImg, img);
        }

        [Test]
        public void Should_give_correct_skills_When_set_url()
        {
            // Arrange
            var info = new PersonInformation("https://ua.linkedin.com/pub/myroslav-dmytrus/b7/a02/436");
            // Act
            var skills = info.Skills;
            var checkSkill = "C#, .NET, ASP.NET MVC, SQL, JavaScript, HTML, Microsoft Office";

            // Assert
            Assert.AreEqual(checkSkill, skills);
        }
        //"UkrainianNative or bilingual proficiencyEnglishProfessional working proficiencyRussianFull professional proficiency"

        [Test]
        public void Should_give_correct_languages_When_set_url()
        {
            // Arrange
            var info = new PersonInformation("https://ua.linkedin.com/pub/myroslav-dmytrus/b7/a02/436");
            // Act
            var lang = info.Languages;
            var checkLeng = new List<string>()
            {
            "UkrainianNative or bilingual proficiencyEnglishProfessional working proficiencyRussianFull professional proficiency"
            };
            // Assert
            Assert.AreEqual(checkLeng, lang);
        }

        [Test]
        public void Should_give_correct_time_expirience_When_set_url()
        {
            // Arrange
            var info = new PersonInformation("https://ua.linkedin.com/pub/dmitriy-beseda/a4/742/497/en");
            // Act
            var time = info.ExperienceTime;
            var checkTime = " 11 months";

            // Assert
            Assert.AreEqual(checkTime, time);
        }

        [Test]
        public void Should_give_correct_expirience_When_set_url()
        {
            // Arrange
            var info = new PersonInformation("https://ua.linkedin.com/pub/dmitriy-beseda/a4/742/497/en");
            // Act
            var work = info.Experience;
            List<string> checkWork = new List<string>()
            {
                    "Web DeveloperBinary StudioOctober 2014 - Present (11 months)Украина"
            };

            // Assert
            Assert.AreEqual(checkWork, work);
        }
    }
}
