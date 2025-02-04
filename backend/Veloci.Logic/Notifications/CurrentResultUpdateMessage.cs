using MediatR;
using Veloci.Data.Domain;

namespace Veloci.Logic.Notifications;

public record CurrentResultUpdateMessage(List<TrackTimeDelta> Deltas) : INotification;
