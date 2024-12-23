using MailSend.DB.Entity;
using Microsoft.EntityFrameworkCore;

namespace MailSend.DB
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions) : base(dbContextOptions) { }
        public  DbSet<EmailTemplate> EmailTemplates {  get; set; }


    }
}
