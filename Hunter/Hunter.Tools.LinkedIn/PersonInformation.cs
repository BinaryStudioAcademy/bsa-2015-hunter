using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;

namespace Hunter.Tools.LinkedIn
{
    public class PersonInformation
    {
        public string Name { get; set; }
        public string Headline { get; set; }
        public string Location { get; set; }
        public string Industry { get; set; }
        public IEnumerable<string> Summary { get; set; }
        public string Skills { get; set; }
        public string Img { get; set; }
        public string ExperienceTime { get; set; }
        public IEnumerable<string> Experience { get; set; }
        public IEnumerable<string> Courses { get; set; }
        public IEnumerable<string> Projects { get; set; }
        public IEnumerable<string> Certifications { get; set; }
        public IEnumerable<string> Languages { get; set; }
        public IEnumerable<string> Education { get; set; }

        private string Url { get; set; }
        private HtmlDocument Document { get; set; }

        public PersonInformation(string url)
        {
            var web = new HtmlWeb();
            Url = url;
            Document = web.Load(Url);
            Parsing();
        }




        private void Parsing()
        {
            ExperienceTime = GetExperience();

            Img = GetImg();

            //var name = Document.DocumentNode.SelectNodes("//span[@class='full-name']").Nodes();
            Name = SmallInfo("//span[@class='full-name']");
            //var location = Document.DocumentNode.SelectNodes("//span[@class='locality']").Nodes();
            Location = SmallInfo("//span[@class='locality']");
            //var industry = Document.DocumentNode.SelectNodes("//dd[@class='industry']").Nodes();
            Industry = SmallInfo("//dd[@class='industry']");
            //var headline = Document.DocumentNode.SelectNodes("//div[@id='headline']").Nodes();
            Headline = SmallInfo("//div[@id='headline']");
            var summary = Document.DocumentNode.SelectNodes("//p[@class='description']");
            Summary = Save(summary);

            Skills = GetSkills();

            Experience = MoreInfo("experience");

            Courses = MoreInfo("courses");

            Projects = MoreInfo("projects");

            Certifications = MoreInfo("certifications");

            Languages = MoreInfo("languages");

            Education = MoreInfo("education");

            string[] values = new string[] {"experience", "courses","projects","certifications", "languages", "education","interests",
            "patents","publications","honors","test-scores","organizations","volunteering"};
        }

        private IEnumerable<string> Save(IEnumerable<HtmlNode> obj)
        {
            try
            {
                return obj.ToList().Select(x => x.InnerText.Replace("&#8211;", "-").Replace("&#39;", "'").Replace("&amp;", "&"));
            }
            catch (Exception)
            {
                return null;
            }

        }
        private string SmallInfo(string xPath)
        {
            try
            {
                var obj = Document.DocumentNode.SelectNodes(xPath).Nodes();
                return obj.ToList().Select(x => x.InnerText).FirstOrDefault();
            }
            catch (Exception)
            {
                return null;
            }

        }
        private IEnumerable<string> MoreInfo(string value)
        {
            try
            {
                string selector = String.Format("//div[@id='background-{0}']", value);
                var items = Document.DocumentNode.SelectNodes(selector).Nodes().Where(x => x.Name != "script").Where(x => x.DescendantNodes().Count() != 1);
                var inf = Save(items);
                if (inf.Count() == 2 && inf.LastOrDefault().Length == 0 || inf.Count() == 0)
                    return null;
                return inf;
            }
            catch (Exception)
            {
                return null;
            }
        }
        private string GetImg()
        {
            //get img link if exist
            try
            {
                return Document.DocumentNode.SelectNodes("//img[@id='bg-blur-profile-picture']").FirstOrDefault().GetAttributeValue("src", "null");
            }
            catch (Exception)
            {
                return null;
            }
        }
        private string GetSkills()
        {
            //get all skills into one string, if exist
            try
            {
                var skills = Document.DocumentNode.SelectNodes("//span[@class='skill-pill']");
                var skillsList = String.Empty;
                Save(skills).ToList().ForEach(x => skillsList += String.Format("{0}, ", x));
                skillsList = skillsList.Remove(skillsList.LastIndexOf(","));
                return skillsList;
            }
            catch (Exception)
            {
                return null;
            }

        }
        private string GetExperience()
        {
            //get all dates
            IEnumerable<string> allDates;
            try
            {
                allDates = Document.DocumentNode.SelectNodes("//span[@class='experience-date-locale']").Select(x => x.InnerText);
            }
            catch (Exception)
            {
                return null;
            }
            //get begin and finish work dates
            var firstDatesString = allDates.FirstOrDefault();
            var lastDatesString = allDates.LastOrDefault();


            var firstIndex = allDates.FirstOrDefault().IndexOf(";");
            var lastIndex = allDates.FirstOrDefault().IndexOf("(");
            firstDatesString = firstDatesString.Substring(firstIndex + 1, lastIndex - firstIndex - 1);
            DateTime date1;
            if (firstDatesString.ToLower().Contains("present"))
            {
                date1 = DateTime.Now;
            }
            else
            {
                date1 = Convert.ToDateTime(firstDatesString);
            }
            lastIndex = lastDatesString.IndexOf("&");
            lastDatesString = lastDatesString.Substring(0, lastIndex);
            DateTime date2;
            try
            {
                date2 = Convert.ToDateTime(lastDatesString);
            }
            catch (Exception)
            {
                date2 = new DateTime(int.Parse(lastDatesString), 1, 1);
            }

            //count period of time
            var countYear = (date1.Year - date2.Year);
            var countMonth = (date1.Month - date2.Month + 1);
            if (countMonth < 0)
            {
                countMonth += 12;
                countYear--;
            }

            //put period in normal format
            var yearOutput = String.Empty;
            var monthOutput = String.Empty;
            if (countYear == 1)
            {
                yearOutput = "1 year";
            }
            else if (countYear > 1)
            {
                yearOutput = String.Format("{0} years", countYear);
            }
            if (countMonth == 1)
            {
                monthOutput = "1 month";
            }
            else if (countMonth > 1)
            {
                monthOutput = String.Format("{0} months", countMonth);
            }


            return String.Format("{0} {1}", yearOutput, monthOutput);
        }
    }
}
