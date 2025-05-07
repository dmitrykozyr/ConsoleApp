using MVVM.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVVM.Stores;

public class HotelStore
{
    private readonly Hotel _hotel;

    private Lazy<Task> _initializeLazy;

    private readonly List<Reservation> _reservations;

    public IEnumerable<Reservation> Reservations => _reservations;

    public event Action<Reservation> ReservationMade;

    public HotelStore(Hotel hotel)
    {
        _hotel = hotel;
        _initializeLazy = new Lazy<Task>(Initialize);
        _reservations = new List<Reservation>();
    }

    public async Task Load()
    {
        try
        {
            // После первого обращения, вызовется строчка выше
            // _initializeLazy = new Lazy<Task>(Initialize)
            // и вызовется метод Initialize()
            await _initializeLazy.Value;
        }
        catch (Exception)
        {
            _initializeLazy = new Lazy<Task>(Initialize);
            throw;
        }
    }

    public async Task MakeReservation(Reservation reservation)
    {
        await _hotel.MakeReservation(reservation);

        _reservations.Add(reservation);

        OnReservationMade(reservation);
    }

    private void OnReservationMade(Reservation reservation)
    {
        ReservationMade?.Invoke(reservation);
    }

    private async Task Initialize()
    {
        IEnumerable<Reservation> reservations = await _hotel.GetAllReservations();

        _reservations.Clear();

        _reservations.AddRange(reservations);

        // Тестирование обработчика ошибок
        //throw new Exception();
    }
}
