using System;
using System.Collections.Generic;
using System.Data.Entity;
using Hunter.Common.Concrete;
using Hunter.DataAccess.Entities.Migrations;

namespace Hunter.DataAccess.Entities
{
    internal class HunterDbInitializer : 
                    //System.Data.Entity.DropCreateDatabaseIfModelChanges<HunterDbContext>
                    System.Data.Entity.CreateDatabaseIfNotExists<HunterDbContext>
                    //DropCreateDatabaseAlways<HunterDbContext>
                    //MigrateDatabaseToLatestVersion<HunterDbContext, Configuration>
    {
        protected override void Seed(HunterDbContext context)
        {
            SeedWithData(context);
        }

        private static byte[] ToByteArray(string sqlVarBinary)
        {
            List<byte> byteList = new List<byte>();

            string hexPart = sqlVarBinary.Substring(2);

            for (int i = 0; i < hexPart.Length / 2; i++)
            {
                string hexNumber = hexPart.Substring(i * 2, 2);
                byteList.Add((byte)Convert.ToInt32(hexNumber, 16));
            }

            return byteList.ToArray();
        }

        public static void SeedWithData(HunterDbContext context)
        {
            try
            {
                #region USER ROLES
                var recruiterRole = new UserRole() { Name = "Recruiter" };
                var technicalRole = new UserRole() { Name = "Technical Specialist" };
                var adminRole = new UserRole() { Name = "Admin" };

                var userRoles = new List<UserRole> { recruiterRole, technicalRole, adminRole };
                userRoles.ForEach(userRole => context.UserRole.Add(userRole));
                context.SaveChanges();
                #endregion

                #region USERS
                var recruiter1 = new User() { Login = "recruiter@local.com", RoleId = 1, PasswordHash = "AL1osj2Akgj8ezBwxcAnj+sn8jQxNU5xJfpkLQl9PKswryRZhwldGQOqFOntmG0zDQ==" };
                var recruiter2 = new User() { Login = "recruiter2", UserName = "Heaven Hayden", RoleId = 1, PasswordHash = "4272d77a88d418148b10545a86f2d094c75e747aedd224294054db7b9bd4b930e03406cd5ef60b5905d5272dd93f318f55a55da7ca087550e783a2dd3829d866" };
                var recruiter3 = new User() { Login = "recruiter3", UserName = "Chantel Sherley", RoleId = 1, PasswordHash = "e27f3ead50aa12b2a50b1ddf7ad619aeb69f37ca22ad50e834962c6f384f2ba5587d451cf1a2d8cdffe1f12ba9479d7bc62b2b85b28e0a6895c20cfba16903f8" };
                var techspec1 = new User() { Login = "techspec@local.com", RoleId = 2, PasswordHash = "AL2W1658IHoHGMrIYZn00YgBhIQ9tN00wAkdLDrHLDtpfQq7bkjfwkxTyshYa5G4tQ==" };
                var techspec2 = new User() { Login = "techspec1", UserName = "Colin Tobias", RoleId = 2, PasswordHash = "7ab8123c7ff047f3e556a5a2b3e2a8502944b54f4f62089635b3850ded96b3f700026af0684ba794bcafec70433ccce5a10368fdb2dcf1ac5658c37b422e6a04" };
                var techspec3 = new User() { Login = "techspec1", UserName = "Maddox Fulton", RoleId = 2, PasswordHash = "fd088e4d0ead7cc5d1c2cfe385c3e9b763aeba92ac77ef2307fbb59604e14a896f706ac9d7f6788c9a99f7ee6175a0f46004eab324a5ef2ec93284276767c7a0" };
                var admin = new User() { Login = "admin@local.com", RoleId = 3, PasswordHash = "AO+POAp4pSwtxgaOu74yRWoYALimtAjpWgsy1746KKw+NkKG+aFUB8UWLS89Jc98VQ==" };

                var users = new List<User> { recruiter1, recruiter2, recruiter3, techspec1, techspec2, techspec3, admin };
                users.ForEach(user => context.User.Add(user));
                context.SaveChanges();
                #endregion

                #region USER Profiles
                var uProfile1 = new UserProfile() { Alias = "UF", UserLogin = "ulyana@bs.com", Position = "HR Manager", Added = new DateTime(2015, 07, 01, 10, 10, 10, DateTimeKind.Utc), };
                var uProfile2 = new UserProfile() { Alias = "KP", UserLogin = "kate@bs.com", Position = "HR Manager", Added = new DateTime(2015, 07, 02, 10, 10, 10, DateTimeKind.Utc), };
                var uProfile3 = new UserProfile() { Alias = "IS", UserLogin = "irina@bs.com", Position = "Event Manager", Added = new DateTime(2015, 07, 03, 10, 10, 10, DateTimeKind.Utc), };

                var uProfiles = new List<UserProfile> { uProfile1, uProfile2, uProfile3 };
                uProfiles.ForEach(pr => context.UserProfile.Add(pr));
                context.SaveChanges();
                #endregion

                #region RESUMES
                var resume = new Resume() { FileName = "Resume1", FileStream = new byte[0] };

                context.Resume.Add(resume);
                context.SaveChanges();
                #endregion

                #region CANDIDATES
                var candidate1 = new Candidate() { FirstName = "Hollis", LastName = "Sefton", Email = "sefton@outlook.com", CurrentPosition = "Chief Tactics Planner", Company = "3D Me!", Location = "Winter Park", Skype = "sefton1052", Phone = "202-555-0160", Salary = "64K", YearsOfExperience = 12, ResumeId = 1 };
                var candidate2 = new Candidate() { FirstName = "Porter", LastName = "Wystan", Email = "pwystan@yahoo.com", CurrentPosition = "Central Configuration Specialist", Company = "Acacia Group", Location = "Athens", Skype = "porter_9", Phone = "202-555-0141", Salary = "110K", YearsOfExperience = 7.8, ResumeId = 1 };
                var candidate3 = new Candidate() { FirstName = "Gabe", LastName = "Raven", Email = "graven@gmail.com", CurrentPosition = "International Tactics Developer", Company = "AirSync", Location = "West Fargo", Skype = "gabe_raven_001", Phone = "202-555-0167", Salary = "78K", YearsOfExperience = 9, ResumeId = 1 };
                var candidate4 = new Candidate() { FirstName = "Jack", LastName = "Sylvanus", Email = "jsylv@gmail.com", CurrentPosition = "Senior Brand Orchestrator", Company = "All Apps", Location = "Camp Hill", Skype = "jack_16", Phone = "202-555-0128", Salary = "94K", YearsOfExperience = 1.2, ResumeId = 1 };
                var candidate5 = new Candidate() { FirstName = "Lindsay", LastName = "Darryl", Email = "lindsay@outlook.com", CurrentPosition = "Lead Mobility Agent", Company = "Bitrex", Location = "Princeton", Skype = "0lindsay0", Phone = "202-555-0196", Salary = "67K", YearsOfExperience = 4, ResumeId = 1 };
                var candidate6 = new Candidate() { FirstName = "Jennie", LastName = "Charlie", Email = "jennie@gmail.com", CurrentPosition = "Lead Integration Analyst", Company = "Decratex", Location = "Oakland Gardens", Skype = "jen0140", Phone = "202-555-0198", Salary = "99K", YearsOfExperience = 3.10, ResumeId = 1 };
                var candidate7 = new Candidate() { FirstName = "Gracelyn", LastName = "Moriah", Email = "grace@yahoo.com", CurrentPosition = "Dynamic Quality Assistant", Company = "DigestIT", Location = "Stafford", Skype = "moriah_99", Phone = "613-555-0115", Salary = "82K", YearsOfExperience = 2.1, ResumeId = 1 };
                var candidate8 = new Candidate() { FirstName = "Elizabeth", LastName = "Rona", Email = "eliz@hotmail.com", CurrentPosition = "Dynamic Identity Supervisor", Company = "eCourt", Location = "Sterling", Skype = "lisy228", Phone = "01632 960277", Salary = "71K", YearsOfExperience = 5, ResumeId = 1 };
                var candidate9 = new Candidate() { FirstName = "Sunny", LastName = "Fawn", Email = "fawn@gmail.com", CurrentPosition = "Division Assistant", Company = "Micromax", Location = "Windermere", Skype = "fawn322", Phone = "613-555-0104", Salary = "89K", YearsOfExperience = 4.4, ResumeId = 1 };
                var candidate10 = new Candidate() { FirstName = "Christianne", LastName = "Diantha", Email = "diantha@outlook.com", CurrentPosition = "Senior Optimization Planner", Company = "ExoGame", Location = "Summerville", Skype = "diantha", Phone = "613-555-0160", Salary = "91K", YearsOfExperience = 15, ResumeId = 1 };
                var candidate11 = new Candidate() { FirstName = "Allie", LastName = "Merideth", Email = "merideth@hotmail.com", CurrentPosition = "Division Architect", Company = "Future Technology", Location = "Canton", Skype = "merideth99", Phone = "01632 960813", Salary = "115K", YearsOfExperience = 0.9, ResumeId = 1 };
                var candidate12 = new Candidate() { FirstName = "Kennedy", LastName = "Wardell", Email = "wardell@yahoo.com", CurrentPosition = "Product Functionality Consultant", Company = "Highway Cruise Lines", Location = "Pikesville", Skype = "ken1520", Phone = "613-555-0126", Salary = "80K", YearsOfExperience = 1.5, ResumeId = 1 };
                var candidate13 = new Candidate() { FirstName = "Gloria", LastName = "Delma", Email = "glor@outlook.com", CurrentPosition = "District Creative Director", Company = "Karmalogic", Location = "Bristol", Skype = "gloria33", Phone = "613-555-0115", Salary = "99K", YearsOfExperience = 4, ResumeId = 1 };
                var candidate14 = new Candidate() { FirstName = "Deanne", LastName = "Imogene", Email = "imogene@hotmail.com", CurrentPosition = "Global Creative Associate", Company = "Microluxe", Location = "Goose Creek", Skype = "deanne17", Phone = "01632 960437", Salary = "117K", YearsOfExperience = 2, ResumeId = 1 };
                var candidate15 = new Candidate() { FirstName = "Lon", LastName = "Abner", Email = "lonabner@gmail.com", CurrentPosition = "Senior Web Analyst", Company = "KCS Design", Location = "Abingdon", Skype = "lon78", Phone = "613-555-0167", Salary = "102K", YearsOfExperience = 3, ResumeId = 1 };

                var candidates = new List<Candidate>() { candidate1, candidate2, candidate3, candidate4, candidate5, candidate6, candidate7, candidate8, candidate9, candidate10, candidate11, candidate12, candidate13, candidate14, candidate15 };
                candidates.ForEach(candidate => context.Candidate.Add(candidate));
                context.SaveChanges();
                #endregion

                #region POOLS
                var jsPool = new Pool() { Name = "JavaScript" };
                var netPool = new Pool { Name = ".Net" };
                var phpPool = new Pool { Name = "PHP" };

                var pools = new List<Pool>() { jsPool, netPool, phpPool };
                pools.ForEach(pool => context.Pool.Add(pool));
                context.SaveChanges();
                #endregion

                #region VACANCIES
                var vacancy1 = new Vacancy()
                {
                    Name = "PHP Developer",
                    Status = 0,
                    UserId = 1,
                    StartDate = new DateTime(2015, 8, 2),
                    Pool = phpPool,
                    Description = @"Company Description:
                                Brandastic is a fast expanding design company in Orange County, California specializing in custom web platforms, online marketing, and more.

                                Job Description:
                                WHEN WAS THE LAST TIME YOU HAD FUN AT WORK ?
                                It's a great time to join our marketing agency team. Get all the benefits of working with Fortune 500 Clients in a small agency environment. Not only do we take pride in what we do, we have FUN. Your creativity and energy are what we need.

                                Perks:
                                Work a great Office in Newport Beach, Ca
                                Have freedom
                                A paid parking spot
                                Office has a kitchen and lounge area

                                Brandastic, Inc is looking for an experienced Magento Programmer to lead and develop various Magento projects.

                                PREFERRED:
                                Must have built a Magento or Wordpress website in the past year
                                3 + years object- oriented PHP experience
                                Know Bootstrap Framework
                                Live in or near Orange County, Ca and be able to
    
                                THE IDEAL CANDIDATE:
                                Knows Magento inside and out, from a development perspective
                                Is passionate about creating elegant code to solve specific client requirements / needs
                                Communicates openly and candidly, asking questions and providing input supported by a logical, analytical thought process
                                Interacts effectively with others and has strong communication and management skills for handling project scope, team and clients
                                Is a voracious learner who realizes that technology and customers have evolving wants, needs and capabilities, and who is not content to stand still and wait to be told what to do or learn...likes to take initiative
                                Possesses excellent time and task management skills(we're paid for our time, so it's critical that we always provide value to our customers within schedules that meet and agree to timelines and budgets set jointly with our clients)
                                Enjoys project-based work that creates new features and functionality for a specific set of goals for a client within the context of a specific project
                                Assumes accountability not only for their own work, but the collective success of the team
                                Is detail oriented, focused on completing assigned tasks fully in a self - directed manner
                                We are a growing business with lots of opportunity and believe that our team is our success."
                };

                var vacancy2 = new Vacancy()
                {
                    Name = "PHP Developer - Magento Lead",
                    Status = 0,
                    UserId = 1,
                    StartDate = new DateTime(2015, 8, 2),
                    Pool = phpPool,
                    Description = @"Company Description:
                               For two decades Guidance Solutions has stood at the forefront of eCommerce innovation. We are based in beautiful Marina del Rey, CA just steps from the beach where we develop customized eCommerce strategy for our clients. Using social commerce and the best of Web 2.0 we offer clients a range of services from Information Architecture, User Experience, and Web Design to Development, Hosting, eMarketing and Mobile App Development.
                               We believe in hiring the best so we can deliver the best. We are passionate about the work we do and believe it is possible to do great work and have fun at the same time. We have been voted one of the Best Places to Work, by the LA Business Journal. We offer a relaxed, casual environment where passion, commitment, and excellence reign.

                               Job Description:
                               Guidance Solutions is located in beautiful Marina del Rey, steps from the beach. 
                               We are a leader in the eCommerce industry, and work with high profile clients such as: Foot Locker, Behr Paint, and Burlington Coat Factory. We believe in being passionate about work, having fun at work, and delivering the best. We have been named one of the BEST PLACES TO WORK in LA. We enjoy great pay and benefits, a casual, relaxed work style and a friendly, collaborative environment. We are proud to be environmentally and socially conscious. If you are looking for the opportunity to stand out from the crowd come join us at the BEACH. COME PLAY WITH THE BEST As our php-Magento Developer/Architect you will develop web solutions and custom applications for our high-profile clients with rich media user experiences. Responsibilities:Develop overall technical strategy for external client projects from technical requirementsCommunicate with external clients on a regular basis regarding progress, challenges, timelines and end results of client projectsDirect and oversee more junior developers including off-shore developers in performing Magento technical programming which needs to be accomplishedConduct technical estimations for sales presentationsPresent technical strategy to clients and gather technical requirements as neededDevelop technical documentationCreate and update both design and functional documentsIdentify and troubleshoot issues as needed, documenting developmentPerform a mix of maintenance, enhancements and new development as requiredImplement project applications according to specificationsResearch technical issues and provide recommendations to enhance client web sitesWork independently and as part of a team to create cutting edge E-commerce & mobile sitesTest sites to ensure optimal usabilityJuggle multiple projects and shifting prioritiesMeet stipulated deadlines Qualifications:10+ years developing Front End and user interface code for Web sitesProficient in OOP8+ years of experience coding in DHTML, JavaScript, CSS, Ajax, jQuery8+ years of PHP 4/56 years hands-on experience with Magento6 years E-commerce experienceExperience with Zend FrameworkBasic to intermediate understanding of LinuxBasic to intermediate understanding of MS-SQL and/or MySQLAbility to address and quickly fix HTML, CSS and Scripting compatibility issues between different browsers and platformsGood understanding of Web analytics and SEO techniquesExperience extending or customizing existing products/frameworks such as Drupal, Magento, WordPress, or OS CommerceExperience using Version Control such as SVN or CVS (is a plus)B.S. in Computer Science or equivalent preferredProblem solving and time management skillsAbility to work in both an individual and team based environmentAbility to organize, prioritize and perform multiple job tasks simultaneouslyProven ability to work creatively and analytically in a problem-solving environment."
                };

                var vacancy3 = new Vacancy()
                {
                    Name = "Senior PHP Developer",
                    Status = 0,
                    UserId = 1,
                    StartDate = new DateTime(2015, 8, 3),
                    Pool = phpPool,
                    Description = @"Company Description:
                                A Santa Monica-based pre-IPO tech company; CallFire is a voice and text platform, with technologies that fit with today mobile lifestyle. We work with major brands, including Public Storage, Dominoes Pizza, AllState, Pepsi and many others. We are a tight knit group of professionals who are passionate about our mission.

                                Job Description:
                                CallFire is one of the fastest growing companies in the country. We are in the Inc 500 and voted as one of the best places to work in Los Angeles! Our team is looking for a Sr. PHP Engineer to help guide our software teams and work along side the CTO to keep the team moving forward. Learn more about CallFire on our website
                                This job is a critical role in the company, on a team that is responsible for making sure all systems work under high system load scenarios. If you have a passion for working on complex problems and creating a testable, well designed, and successful product, this is a perfect opportunity! We are a fun team that is growing faster than ever, you'll love being part of what we do and what we believe in.

                                Responsibilities:
                                Plan and organize technical design and architecture
                                Be a core part of implementing application improvements and features
                                Be a technical mentor for the team
                                Take ownership of production success
                                Understand and have a passion for scalable designs

                                Qualifications:
                                You have a genuine passion for Software
                                You are energized by productive and fast moving teams
                                Strong PHP Experience (Composer, Zend2)

                                Benefits:
                                Medical, dental, and vision insurance
                                Paid vacation
                                Life Insurance
                                401(k) and stock-option plans
                                Commuter-friendly policies"
                };

                var vacancy4 = new Vacancy()
                {
                    Name = "Inventive JavaScript / Node.js Leader",
                    Status = 0,
                    UserId = 1,
                    StartDate = new DateTime(2015, 8, 2),
                    Pool = jsPool,
                    Description = @"About You:
                                You're pretty much a master with Node.js (it's a long-term relationship but it still feels like you met yesterday).
                                You also love trying out new languages, frameworks, libraries, and leveraging them off, whenever necessary (a recent fling with Ionic is a plus!).
                                You have led project teams to success and can show us that.
                                You understand how to motivate junior developers—you enjoy teaching and mentoring (almost) as much as programming.
                                You're interested in creating MVPs and helping clients scale their ideas to profit / exit.
                                You're great with clients, whether it's the CTO or a company's executive assistant.
                                You have interesting accomplishments to tell us about, and hey, you might have even rejected offers from companies like Google, Facebook, or Twitter.
                                You're ready for a long-term fit with the flexibility to create your best work, again and again.
                                If you're not a native English speaker, people often mistake you as such.
                                
                                About Clevertech:
                                We have been around for 14 years and have over 80 developers and designers, scattered all over the world but as tightly knit as any traditional team.
                                Our project teams are small and workflows are fluid—the result? Everyone feels heard and has a chance to make an impact.
                                We are very much enthralled by Agile and Lean principles, successfully launching products in short windows of time. That’s possible due to the power of focus.
                                If you want to understand why it's so amazing to work for Clevertech, you can look at why.clevertech.biz. Some of the great things we do are visible incleverstack.io, cleverte.ch/bloomberg, visualcaptcha.net, and clevertech.biz.
                                We could tell you more, but it’s much more interesting to see what you find out.
                                
                                About the Job:
                                This is an full-time, ongoing, and 100% remote opportunity (8h/day, 40h/week). We require that you work at least 5 hours within New York’s 9am-5pm.
                                Collaborate with top developers and contribute to open source.
                                Advance your ambitions—we invest in making you better, so that we’re better together.
                                Work on quality projects and with cutting-edge technology.
                                Take time off when you need it. Embrace the flow. Control your schedule!"
                };

                var vacancy5 = new Vacancy()
                {
                    Name = "Software engineer (JavaScript, AngularJS)",
                    Status = 0,
                    UserId = 1,
                    StartDate = new DateTime(2015, 8, 2),
                    Pool = jsPool,
                    Description = @"We are looking for the best and brightest minds in Web Development. Someone who is truly passionate about creating beautiful innovative next generation UIs using bleeding edge technologies.
                                
                                As a member of a SCRUM team, you are asked to deliver the following:
                                Implement requirements from design mockups on the UI and integrate them with the backend services.
                                Facilitate innovative design and find solutions to challenging technical problems through information gathering, ideation and best practices.
                                Contribute to UI and UX solutions.
                                Collaborate effectively with team members and product owners.
                                
                                What do we expect from you?
                                There is no set route to become a Front-end Web Developer at TomTom but to be successful in this role, this is the kind of profile we have in mind:
                                1-3 years of relevant experience with advance web development and applications.
                                Strong technical knowledge in current web development technologies including Javascript, AngularJS, CSS3, and HTML5.
                                You have solid understanding of MVC and ability to write clean, concise and maintainable code.
                                Keywords: AngularJS, Grunt, Karma, Protractor, Jasmine, Bootstrap, SASS, MomentJS, Bower, D3, SVG, test automation, responsive design.
                                
                                What do we offer?
                                The chance to work in a fast moving, innovative and international environment, dealing with all kind of different countries and cultures.
                                Indefinite contract, 15% annual bonus, 25 annual leave days, pension scheme, life, medical and disability insurance, up to 50% discount on products.
                                Among others, an online learning platform (1500 courses & 11000 e-books) as well as live speeches from software development gurus, like Uncle Bob.
                                
                                Who are we?
                                TomTom empowers movement. Every day millions of people around the world depend on TomTom to make smarter decisions. We design and develop innovative products that make it easy for people to keep moving towards their goals.
                                Our map-based components include map content, online map-based services, real-time traffic, and navigation software. Our consumer products include PNDs, navigation apps, and GPS sports watches. Our main business products are custom in-dash navigation systems and a fleet management system, which is offered to fleet owners as an online service with integrated in-vehicle cellular devices.
                                Our business consists of four customer facing business units: Consumer, Automotive, Licensing and Telematics.
                                Founded in 1991 and headquartered in Amsterdam, we have 4,000 employees worldwide and sell our products in over 36 countries."
                };

                var vacancy6 = new Vacancy()
                {
                    Name = "Senior JavaScript Developer",
                    Status = 0,
                    StartDate = new DateTime(2015, 8, 3),
                    Pool = jsPool,
                    UserId = 1,
                    Description = @"Job Description
                                As Senior JavaScript Developer you will be working on our most challenging and important TV projects and products. You will have a core role in our team of experienced developers, designers, project managers.
                                Most TV platforms are web-based, so you will make heavy use of your skills in JavaScript, CSS and HTML. The central pillar of much of our software is our AppCore: a JavaScript library which allows us to easily write TV apps once and deploy them to many different TV platforms easily.
                                
                                Skills & Requirements
                                
                                Key responsibilities:
                                    Developing state-of-the-art coding solutions for TV’s, set-top boxes, gaming consoles, tablets, smartphones and any other device that is connected to your TV
                                    Participating in generating ideas and implementation for the core application architecture
                                    Leading development of core products in our portfolio, including team management
                                    Researching new, emerging technologies in the TV market to keep us on the cutting edge
                                    Improving the skills of your colleagues through training, guidance and assistance
                                Must haves for this job:
                                    Expert knowledge of JavaScript (plain/vanilla JS)
                                    Expert knowledge of HTML(5) and CSS(3)
                                    Knowledge of design patterns
                                    Track record of developing robust, user-friendly, scalable, and rich web applications
                                    Experience with version control systems (preferably. Git)
                                    Experience with third party API’s (e.g. Facebook, Google, Advertising)
                                    Experience with network sniffing (e.g. Wireshark, Charles Proxy, HTTP Scoop)
                                    Good grasp of the English language
                                Nice to haves:
                                    Experience with developing for Smart TV platforms
                                    Experience with CoffeeScript, Node.js, RequireJS or other JavaScript-based libraries/languages
                                    Experience with SASS, Less or other CSS preprocessor
                                    Experience with Streaming video (e.q. HLS, Smooth) and Video On Demand
                                    Experience with DRM (e.g. PlayReady, Verimatrix)
                                
                                We offer:
                                Fulltime position
                                Salary and benefits matching your experience
                                Offices in Amsterdam, Barcelona, Madrid, Buenos Aires and Los Angeles
                                A fun, informal atmosphere
                                From our offices in Amsterdam and across the world our team has been working on many cool projects over the last five years. Our clients include major Dutch, Spanish and international brands, such as Pathé, RTL, SBS, FOX Sports, RTVE, Nubeox and RTP.
                                We’re constantly working to be on the cutting edge of TV, an area of technology which is evolving at immense speed. This attitude has made us market leader in the Netherlands, and we’re looking to grow further.
                                We are currently expanding our team with people who are passionate about technology, curious about TV, eager to learn and who would like to work in an young, informal team which has a lot of fun while trying to conquer the world. If that sounds like you, we’d love to hear from you!"
                };

                var vacancy7 = new Vacancy()
                {
                    Name = "Web Developer (Javascript/C#/.NET)",
                    Status = 0,
                    UserId = 1,
                    StartDate = new DateTime(2015, 8, 2),
                    Pool = netPool,
                    Description = @"Job Description

                                Roles and Responsibilities: 

                                    Enhance, support and maintain existing applications and new application development for applications built using Javascript, .NET/C#, and SQL
                                    Work both independently and as part of a team. 
                                    Assist in collecting application requirements, application design, testing and documentation. 
                                    Support users regarding system access and use. 
                                    Meaningful input on new application design. 
                                    Develop solutions to date transfer, interchange and integration problems with other systems. 

                                Skills & Requirements

                                Desire Characteristics: 

                                    Ability to be a self starter in a dynamic and fast paced environment. 
                                    Eager to learn new technologies. 
                                    Work successfully in a team environment. 
                                    Excellent communication skills. 
                                    Excellent interpersonal skills. 

                                Job Requirements: 

                                    2+ years minimum of hands on development experience using .NET, C#, SQL, and Javascript are required. 
                                    HTML/HTML5/CSS Web UI design skills required. 
                                    Experience with JQuery and JSON required.
                                    Experience with ASP.NET Web Services is strongly preferred. 
                                    Experience with AngulaJS a plus
                                    Experience with 3rd part APIs, REST interfaces. 
                                    Experience with mobile programming (Xamarin/Android) is a plus.
                                    Knowledge wf debugging techniques. 
                                    General understanding of security practices and threats as it pertains to web development. 
                                    Familiar with design patterns and development practices. 
                                    Familiarity with relational database technologies and tools. Especially MS SQL. 

                                About Therapy Support, Inc

                                We are seeking an ASP .NET Web Developer to work on existing and new web based products. This position will require a highly technical individual to work on designing, coding, testing, debugging, and documenting existing web products and exciting opportunities in designing and developing new web applications. 

                                This is a growing regional company with 21 branches and 18 years in business. We lead the industry in our use of technology. The IT team is small and autonomous with direct line to Executive Leadership of the company. Nice benefit package."
                };

                var vacancy8 = new Vacancy()
                {
                    Name = ".Net Developer",
                    Status = 0,
                    UserId = 1,
                    StartDate = new DateTime(2015, 8, 2),
                    Pool = netPool,
                    Description = @"Job Description

                                We are an innovative software company who produce awesome casino and bingo gaming content in an agile manner and using best practice techniques and approaches.
                                We are looking to expand our .NET team in­line with other areas of our business. The current team is a good mix of graduate, mid, and senior level developers ­ we are specifically looking at those who have commercial exposure to .NET for this role. We also want people who are passionate about producing good quality code and keen to contribute positively to the productive and fun working environment we've established at Bede.
                                We have dedicated Frontend and QA teams leaving the Developer able to focus on their key skill set, and being able to work with others who are extremely capable in their own fields.
                                
                                Skills & Requirements

                                ​Essential

                                    2+ years of C# .NET development in a commercial capacity.
                                    MVC3/4, and Razor
                                    Entity Framework or similar ORM
                                    Dependency Injection (Ideally autofac)
                                    Unit Testing (Ideally Nunit)
                                    Version Control (Ideally Git)
                                    Creation and use of a RESTful interface (Web API)

                                Desirable

                                    Experience with Azure web services
                                    Any experience with the Orchard CMS
                                    Experience of working in a Continuous Deployment Setting
                                    Familiarity with TeamCity, or even Octopus Deploy
                                    Experience of working in an Agile process using SCRUM techniques.

                                About Bede Gaming

                                Bede Gaming delivers state of the art technology solution for online gaming partners.  Cross device support is a key differentiator from our competitors, and we're seeking to grow an even stronger .Net development team."
                };

                var vacancy9 = new Vacancy()
                {
                    Name = "Backend .Net Web Developer",
                    Status = 0,
                    UserId = 1,
                    StartDate = new DateTime(2015, 8, 2),
                    Pool = netPool,
                    Description = @"Job Description

                                Whether you’re fresh out of college or an experienced professional, if you love working with .Net, APIs and MongoDB, GAN Integrity Solutions (GAN) would like to hear from you.
                                GAN is an international consultancy and IT services company headquartered in Copenhagen, Denmark. We help shape how companies manage compliance and regulatory risks through innovative cloud-based solutions, delivered as Software as a Service (SaaS).
                                GAN is seeking a .Net Back-End Web Developer to bring innovation and module thinking to our solutions. The GAN SaaS solution’s front-end is a single-page web application built with AngularJS, Twitter Bootstrap and a chart framework on top of a REST-based HTTPS+JSON API, served by a WebAPI back-end hosted on Azure, MongoDB.
                                Extensive experience with C#, WebAPI and SOLID Development is a must for this position.

                                You will be a part of a young and dynamic team of ambitious professionals who take pride in developing high-quality, well-tested and innovative applications. Our way of working includes:

                                    Agile methods (e.g. SCRUM and Kanban)
                                    Version Control (TFS, SVN, Git, Mercurial, etc.)
                                    A focus on efficient, reusable code
                                    A focus on refactoring our code to keep it efficient and on meet tight deadlines
                                    We promote information exchange as to educate and share knowledge

                                Skills & Requirements

                                As a Back-End Web Developer, you:

                                    Will write code that is secure, robust, maintainable and can perform adequately under predicted load patterns
                                    Will work with C# (with .NET 4.0 or later)
                                    Will work with WebAPI/MVC or RESTful Web Services
                                    Will work with MongoDB
                                    Will unit-test your code using frameworks such as NUnit, XUnit or MS Test
                                    Have a minimum of 2-3 years of solid .Net development experience & knowledge
                                    Are experienced working with SOLID development principles and design patterns

                                It is a plus to have skills in:

                                    Working with cloud solutions (Azure)
                                    IoC containers (Autofac or similar)
                                    Test-first development, mocking frameworks
                                    Continuous deployment via Visual Studio online
                                    Domain-driven design"
                };


                var vacancies = new List<Vacancy>() { vacancy1, vacancy2, vacancy3, vacancy4, vacancy5, vacancy6, vacancy7, vacancy8, vacancy9 };
                vacancies.ForEach(vacancy => context.Vacancy.Add(vacancy));
                context.SaveChanges();
                #endregion
            }
            catch (Exception ex)
            {
                Logger.Instance.Log(ex);
                throw;
            }
        }
    }
}