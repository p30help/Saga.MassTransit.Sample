using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SagaMachine.StateMachine;

namespace SagaMachine.DbConfiguration
{
    public class OrderStateMap : SagaClassMap<OrderStateData>
    {
        protected override void Configure(EntityTypeBuilder<OrderStateData> entity, ModelBuilder model)
        {
            entity.Property(x => x.CurrentState).HasMaxLength(64);
            entity.Property(x => x.OrderCreationDateTime);
        }
    }
}
