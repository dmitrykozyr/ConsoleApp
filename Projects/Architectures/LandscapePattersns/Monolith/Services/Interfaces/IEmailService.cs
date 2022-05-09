using Monolith.Domain;

namespace Monolith.Services.Interfaces
{
    public interface IEmailService
    {
        void SendBookingConfirmationMail(Booking booking, Tour tour);
    }
}
