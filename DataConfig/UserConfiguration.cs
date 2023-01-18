using eventful_api_master.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eventful_api_master.DataConfig
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.FirstName)
                .HasColumnType("varchar(20)")
                .IsRequired();

            builder.Property(x => x.LastName)
                .HasColumnType("varchar(20)")
                .IsRequired();

            builder.Property(x => x.Email)
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder.Property(x => x.Password)
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder.Property(x => x.Cpf)
                .HasColumnType("char(11)")
                .IsRequired();

            builder.Property(x => x.BirthDate)
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(x => x.Genre)
                .HasColumnType("tinyint")
                .IsRequired();

            builder.Property(x => x.AcceptedTerms)
                .HasColumnType("bit")
                .IsRequired();

            builder.Property(x => x.Active)
                .HasColumnType("bit")
                .IsRequired();

            builder.Property(x => x.CreationDate)
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(x => x.ChangeDate)
                .HasColumnType("datetime");

            builder.Property(x => x.CreationUser)
                .HasColumnType("int");

            builder.Property(x => x.ChangeUser)
                .HasColumnType("int");
        }
    }
}
