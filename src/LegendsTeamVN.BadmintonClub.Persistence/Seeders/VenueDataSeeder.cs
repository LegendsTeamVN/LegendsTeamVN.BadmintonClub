using LegendsTeamVN.BadmintonClub.Domain.Entities;
using LegendsTeamVN.Core.Application.Data;
using Microsoft.EntityFrameworkCore;

namespace LegendsTeamVN.BadmintonClub.Persistence.Seeders;

internal sealed class VenueDataSeeder(BadmintonDbContext dbContext) : IDataSeeder
{
    public async Task SeedAsync(CancellationToken cancellationToken = default)
    {
        if (await dbContext.Set<Venue>().AnyAsync(cancellationToken))
        {
            return;
        }

        var venues = new List<Venue>
        {
            new Venue(
                "Sân cầu lông AZ",
                "123 Nguyễn Trãi, Thanh Xuân, Hà Nội",
                20.9947m,
                105.8072m,
                new TimeOnly(8, 0),
                new TimeOnly(22, 0),
                "Sân cầu lông 5 sao, không gian rộng rãi, có điều hòa."
            ),

            new Venue(
                "Sân cầu lông Nguyễn Du",
                "42 Nguyễn Du, Hai Bà Trưng, Hà Nội",
                20.9981m,
                105.8498m,
                new TimeOnly(6, 0),
                new TimeOnly(23, 0),
                "Sân cầu lông trung tâm, thuận tiện di chuyển, phù hợp tập luyện và thi đấu."
            ),

            new Venue(
                "Sân cầu lông Hoàng Gia",
                "88 Trần Duy Hưng, Cầu Giấy, Hà Nội",
                21.0087m,
                105.8039m,
                new TimeOnly(7, 0),
                new TimeOnly(21, 0),
                "Sân cầu lông hiện đại với nhiều sân thi đấu và khu vực nghỉ ngơi."
            ),

            new Venue(
                "Sân cầu lông Cầu Giấy",
                "15 Dịch Vọng, Cầu Giấy, Hà Nội",
                21.0358m,
                105.7948m,
                new TimeOnly(6, 0),
                new TimeOnly(22, 0),
                "Cụm sân cầu lông rộng rãi, hệ thống chiếu sáng tốt, phù hợp cho các trận giao hữu."
            ),

            new Venue(
                "Sân cầu lông Hoàng Mai",
                "56 Tam Trinh, Hoàng Mai, Hà Nội",
                20.9825m,
                105.8612m,
                new TimeOnly(7, 0),
                new TimeOnly(23, 0),
                "Sân cầu lông có không gian thoáng, nhiều khung giờ hoạt động và thuận tiện đặt sân."
            )
        };

        await dbContext.Set<Venue>()
            .AddRangeAsync(venues, cancellationToken);

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}