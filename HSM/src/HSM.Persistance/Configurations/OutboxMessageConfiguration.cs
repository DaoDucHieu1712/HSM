using HSM.Persistance.Constants;
using HSM.Persistance.Outbox;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HSM.Persistance.Configurations
{
    internal class OutboxMessageConfiguration : IEntityTypeConfiguration<OutboxMessage>
    {
        public void Configure(EntityTypeBuilder<OutboxMessage> builder)
        {
            builder.ToTable(TableNames.OutboxMessages);
            builder.HasKey(x => x.Id);
        }
    }
}
