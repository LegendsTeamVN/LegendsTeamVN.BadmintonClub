using LegendsTeamVN.Core.Domain.Entities;

namespace LegendsTeamVN.BadmintonClub.Domain.Entities;

public class Notification : SoftDeletableEntity<Guid>
{
    public Guid UserId { get; private set; }
    public string Title { get; private set; } = default!;
    public string Message { get; private set; } = default!;
    public bool IsRead { get; private set; }

    protected Notification() { }

    public Notification(Guid userId, string title, string message)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        Title = title;
        Message = message;
        IsRead = false;
    }

    public void MarkAsRead()
    {
        IsRead = true;
    }
}
