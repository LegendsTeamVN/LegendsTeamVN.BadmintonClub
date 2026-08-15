using LegendsTeamVN.BadmintonClub.Application.Features.Courts.Events;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace LegendsTeamVN.BadmintonClub.Infrastructure.Messaging.Consumers;

public class CourtRentedIntegrationEventConsumer : IConsumer<CourtRentedIntegrationEvent>
{
    private readonly ILogger<CourtRentedIntegrationEventConsumer> _logger;

    public CourtRentedIntegrationEventConsumer(ILogger<CourtRentedIntegrationEventConsumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<CourtRentedIntegrationEvent> context)
    {
        var message = context.Message;
        
        // Log ra để giả lập việc xử lý (VD: trừ tiền, gửi email báo thuê sân...)
        _logger.LogInformation(">>> [RABBITMQ] Nhận được sự kiện thuê sân: Sân {CourtId} được thuê bởi {RenterName} lúc {RentedAt}",
            message.CourtId, message.RenterName, message.RentedAt);
        
        return Task.CompletedTask;
    }
}
