using AccountingAssistantBackend.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingAssistantBackend.Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.
                Entity<User>()
                .HasData(
                        new User() { Id = 1, Name = "Jorge Mesa Campo"} ,
                        new User() { Id = 2, Name = "Guillermo Mesa Campo" }
                );

            modelBuilder.
               Entity<SessionChat>()
               .HasData(
                       new SessionChat() { 
                           Id = 1, 
                           Title = "Accounts receivable are found in the current asset section of a balance sheet.",
                           UserId = 1,
                           CreatedAt = DateTime.Now
                       },
                      new SessionChat()
                      {
                          Id = 2,
                          Title = "Record revenue when it is collected.",
                          UserId = 1,
                          CreatedAt = DateTime.Now
                      }
               );

            modelBuilder.
               Entity<ChatMessage>()
               .HasData(
                       new ChatMessage() 
                       { 
                           Id = 1,
                           Content = "Accounts receivable are found in the current asset section of a balance sheet.",
                           SessionChatId = 1,
                           IsFromAssistant = false,
                           CreatedAt = DateTime.Now

                       },
                       new ChatMessage()
                       {
                           Id = 2,
                           Content = "Yes, that's correct. Accounts receivable are typically listed" +
                                      "in the current asset section of a company's balance sheet",
                           SessionChatId = 1,
                           IsFromAssistant = true,
                           CreatedAt = DateTime.Now
                       },
                        new ChatMessage()
                        {
                            Id = 3,
                            Content = "Record revenue when it is collected.",
                            SessionChatId = 2,
                            IsFromAssistant = false,
                            CreatedAt = DateTime.Now

                        },
                       new ChatMessage()
                       {
                           Id = 4,
                           Content = "The recognition of revenue in accounting is generally guided by the revenue" +
                                      "recognition principle, which states that revenue should be recognized when it is" +
                                      "earned and realized or realizable.",
                           SessionChatId = 2,
                           IsFromAssistant = true,
                           CreatedAt = DateTime.Now
                       }
               );
        }

        public DbSet<User> Customers { get; set; }

        public DbSet<SessionChat> SessionsChat { get; set; }

        public DbSet<ChatMessage> ChatMessages { get; set; }
    }
}
