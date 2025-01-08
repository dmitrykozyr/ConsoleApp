using Monolith.Domain;
using Monolith.Services.Interfaces;

namespace Monolith.Services;

public class EmailService : IEmailService
{
    public void SendBookingConfirmationMail(Booking booking, Tour tour)
    {
        File.AppendAllText("AppData\\mails.txt", $"{DateTime.Now.ToString("O")} Sent mail to {booking.Email} ({booking.Name}) for the '{tour.Name}' tour." + Environment.NewLine);
    }
}
