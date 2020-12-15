using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AudioBooks.Data;
using AudioBooks.Domain;

namespace AudioBooks.Api.SeedData
{
    public class SeedTestAudioBookData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var dbContext = new AudioBookContext(serviceProvider.GetRequiredService<DbContextOptions<AudioBookContext>>()))
            {
                if (dbContext.AudioBooks.Any())
                {
                    return;   // DB has been already seeded
                }

                PopulateCategoryData(dbContext);
                PopulateTestData(dbContext);
                //Category thrillerCategory = new Category { CategoryName = "Thriller" };
                //Category thrillerCategory = new Category { CategoryName = "Thriller" };
            }
        }

        private static void PopulateCategoryData(AudioBookContext dbContext)
        {
            dbContext.Categories.Add(new Category { CategoryName = "Adventure" });
            dbContext.Categories.Add(new Category { CategoryName = "Romance" });
            dbContext.Categories.Add(new Category { CategoryName = "Sci Fi" });
            dbContext.Categories.Add(new Category { CategoryName = "Thriller" });
            dbContext.SaveChanges();
        }

        public static void PopulateTestData(AudioBookContext dbContext)
        {
            var adventureCategoryId = dbContext.Categories.FirstOrDefault(c => c.CategoryName.ToLower() == "adventure")?.Id;
            var thrillerCategoryId = dbContext.Categories.FirstOrDefault(c => c.CategoryName.ToLower() == "thriller")?.Id;
            Publisher penguinPublisher = new Publisher { PublisherName = "Penguin" };
            Author leeChildAuthor = new Author { AuthorName = "Lee Child",RecordOwnerId= "8414619f-2189-4344-a57e-62aadb3b4e4f" };

            var audioBook1 = new AudioBook()
            {
                Audio = new Audio
                {
                    AudibleLink = @"https://www.audible.com.au/pd/Killing-Floor-Audiobook/B076HVM1Q1?ref=a_cat_Myste_c10_lProduct_1_7&pf_rd_p=9978c3e4-1b90-4006-977d-6f886970fda3&pf_rd_r=JYKT2AJ85NMJA9CMQ8K7",
                    CoverImage = "KillingFloor-LeeChild.jpg",
                    DurationInMinutes = 939
                },
                Author = leeChildAuthor,
                Description = @"Although the Jack Reacher audiobooks can be listened to in any order, Killing Floor presents Reacher for the first time, as the tough ex-military cop of no fixed abode: a righter of wrongs, the perfect action hero.Jack Reacher jumps off a bus and walks 14 miles down a country road into Margrave, Georgia. An arbitrary decision he's about to regret........ ",
                Name = "Killing Floor",
                PublishedDate = new DateTime(1998, 01, 01),
                Publisher = penguinPublisher,
                QuickSummary = @"Jack Reacher jumps off a bus and walks 14 miles down a country road into Margrave, Georgia. An arbitrary decision he's about to regret."
            };
            if (thrillerCategoryId.HasValue)
            { audioBook1.AudioBookCategories.Add(new AudioBookCategory { CategoryId = thrillerCategoryId.Value }); }
            dbContext.AudioBooks.Add(audioBook1);


            var audioBook2 = new AudioBook()
            {
                Audio = new Audio
                {
                    AudibleLink = @"https://www.audible.com.au/pd/Die-Trying-Audiobook/B076HXFV96?plink=HiO2daeLfFlVOicX&ref=a_pd_Killin_c5_adblp13npsbx_1_1&pf_rd_p=1229e305-edc4-4736-93e3-49cda4a07ddd&pf_rd_r=Q9EQ1QECXRJN39RZT9XJ",
                    CoverImage = "DieTrying-LeeChild.jpg",
                    DurationInMinutes = 955
                },
                Author = leeChildAuthor,
                Description = @"Jack Reacher, alone, strolling nowhere. A Chicago street in bright sunshine. A young woman, struggling on crutches. Reacher offers her a steadying arm. And turns to see a handgun aimed at his stomach. Chained in a dark van racing across America, Reacher doesn't know why they've been kidnapped. The woman claims to be FBI. She's certainly tough enough. But at their remote destination, will raw courage be enough to overcome the hopeless odds?........ ",
                Name = "Die Trying",
                PublishedDate = new DateTime(2000, 01, 01),
                Publisher = penguinPublisher,
                QuickSummary = @"A Chicago street in bright sunshine. A young woman, struggling on crutches. Reacher offers her a steadying arm.."
            };
            if (adventureCategoryId.HasValue)
            { audioBook2.AudioBookCategories.Add(new AudioBookCategory { CategoryId = adventureCategoryId.Value }); }
            if (thrillerCategoryId.HasValue)
            { audioBook2.AudioBookCategories.Add(new AudioBookCategory { CategoryId = thrillerCategoryId.Value }); }

            dbContext.AudioBooks.Add(audioBook2);

            var audioBook3 = new AudioBook()
            {
                Audio = new Audio
                {
                    AudibleLink = @"https://www.audible.com.au/pd/Tripwire-Audiobook/B0774WV4JF?plink=i72on785m605VNc9&ref=a_pd_Die-Tr_c5_adblp13npsbx_1_1&pf_rd_p=1229e305-edc4-4736-93e3-49cda4a07ddd&pf_rd_r=X557KW8HPF6D1ZK473TG",
                    CoverImage = "Tripwire-LeeChild.jpg",
                    DurationInMinutes = 946
                },
                Author = leeChildAuthor,
                Description = @"JFor Jack Reacher being invisible has become a habit.He spends his days digging swimming pools by hand and his nights as the bouncer in the local strip club in the Florida Keys.He doesn't want to be found.But someone has sent a private detective to seek him out. Then Reacher finds the guy beaten to death with his fingertips sliced off. It's time to head north and work out who is trying to find him and why. Although the Jack Reacher novels can be listened to in any order, Tripwire is the third in the series......... ",
                Name = "Die Trying",
                PublishedDate = new DateTime(2013, 01, 01),
                Publisher = penguinPublisher,
                QuickSummary = @"He spends his days digging swimming pools by hand and his nights as the bouncer in the local strip club in the Florida Keys."
            };
            if (thrillerCategoryId.HasValue)
            { audioBook3.AudioBookCategories.Add(new AudioBookCategory { CategoryId = thrillerCategoryId.Value }); }

            dbContext.AudioBooks.Add(audioBook3);

            dbContext.SaveChanges();
        }
    }
}
