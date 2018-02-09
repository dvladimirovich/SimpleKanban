using SimpleKanban.DB.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SimpleKanban.DB.Configurations
{
    class CardConfiguration : EntityTypeConfiguration<Card>
    {
        internal override void Map(EntityTypeBuilder<Card> builder)
        {
            builder.ToTable("Cards").HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.Title).IsRequired().HasMaxLength(512);
            builder.Property(p => p.Description).HasMaxLength(2048);
            builder.Property(p => p.Start).HasColumnName("date_start").IsRequired(); // DataType.Date is omitted
            builder.Property(p => p.End).HasColumnName("date_end");                  // DataType.Date is omitted
            builder.Property(p => p.Complete);
            
            // Setting StatusId as foreign key
            //builder.HasOne(p => p.Statuses)
            //       .WithMany(p => p.Cards)
            //       .HasForeignKey(p => p.StatusId)
            //       .HasConstraintName("FK_Statuses_Cards")
            //       .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
