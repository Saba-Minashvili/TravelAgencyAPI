using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasMany(user => user.MyBookings)
                .WithOne()
                .HasForeignKey(book => book.HostUserId);
            builder.HasMany(user => user.MyGuests)
                .WithOne()
                .HasForeignKey(guest => guest.GuestUserId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(user => user.Apartment)
                .WithOne(apartment => apartment.User)
                .HasForeignKey<Apartment>(apartment => apartment.HostUserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
