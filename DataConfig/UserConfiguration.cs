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

            builder.Property(x => x.firstName)
                .HasColumnType("varchar(20)")
                .IsRequired();

            builder.Property(x => x.lastName)
                .HasColumnType("varchar(20)")
                .IsRequired();

            builder.Property(x => x.email)
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder.Property(x => x.password)
                .HasColumnType("varchar(32)")
                .IsRequired();

            builder.Property(x => x.cpf)
                .HasColumnType("char(11)")
                .IsRequired();

            builder.Property(x => x.birthdate)
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(x => x.genre)
                .HasColumnType("tinyint")
                .IsRequired();

            builder.Property(x => x.acceptedTerms)
                .HasColumnType("bit")
                .IsRequired();

            builder.Property(x => x.active)
                .HasColumnType("bit")
                .IsRequired();

            builder.Property(x => x.creationDate)
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(x => x.changeDate)
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(x => x.creationUser)
                .HasColumnType("int")
                .IsRequired();

            builder.Property(x => x.changeUser)
                .HasColumnType("int")
                .IsRequired();
        }
    }
}
