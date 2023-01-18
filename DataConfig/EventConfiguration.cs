using eventful_api_master.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eventful_api_master.DataConfig
{
    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.ToTable("Event");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Title)
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder.Property(x => x.Description)
                .HasColumnType("varchar(MAX)")
                .IsRequired();

            builder.Property(x => x.Banner)
                .HasColumnType("varchar(MAX)")
                .IsRequired();

            builder.Property(x => x.Datetime)
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(x => x.Cep)
                .HasColumnType("char(8)")
                .IsRequired();

            builder.Property(x => x.City)
                .HasColumnType("varchar(50)")
                .IsRequired();

            builder.Property(x => x.Uf)
                .HasColumnType("char(2)")
                .IsRequired();

            builder.Property(x => x.Address)
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder.Property(x => x.Number)
                .HasColumnType("varchar(5)")
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
