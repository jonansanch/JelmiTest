using DataAcces.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAcces.Config
{
    public class UserConfig
    {
        public UserConfig(EntityTypeBuilder<User> entityBuilder)
        {
            entityBuilder.Property(x => x.UserId).HasDefaultValueSql("NEWID()");
            entityBuilder.Property(x => x.FirstName).IsRequired().HasMaxLength(50).HasDefaultValueSql("0");
            entityBuilder.Property(x => x.SecondName).HasMaxLength(50);
            entityBuilder.Property(x => x.Surname).IsRequired().HasMaxLength(50);
            entityBuilder.Property(x => x.SecondSurname).HasMaxLength(50);
            entityBuilder.Property(x => x.Birthdate).IsRequired();
            entityBuilder.Property(x => x.Salary).IsRequired().HasDefaultValueSql("0");
            entityBuilder.Property(x => x.CreatedOn).IsRequired();
            entityBuilder.Property(x => x.ModifiedOn).IsRequired();
        }
    }
}