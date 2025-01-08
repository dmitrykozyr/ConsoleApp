using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MVVM.Services;
using MVVM.Stores;
using MVVM.ViewModels;
using System;

namespace MVVM.HostBuilders;

public static class AddViewModelsHostBuilderExtensions
{
    public static IHostBuilder AddViewModels(this IHostBuilder hostBuilder)
    {
        hostBuilder.ConfigureServices(services =>
        {
            services.AddTransient(s => CreateReservationListingViewModel(s));
            services.AddSingleton<Func<ReservationListingViewModel>>(s => () => s.GetRequiredService<ReservationListingViewModel>());
            services.AddSingleton<NavigationService<ReservationListingViewModel>>();

            services.AddTransient<MakeReservationViewModel>();
            services.AddSingleton<Func<MakeReservationViewModel>>(s => () => s.GetRequiredService<MakeReservationViewModel>());
            services.AddSingleton<NavigationService<MakeReservationViewModel>>();

            services.AddSingleton<MainViewModel>();
        });

        return hostBuilder;
    }

    private static ReservationListingViewModel CreateReservationListingViewModel(IServiceProvider services)
    {
        return ReservationListingViewModel.LoadViewModel(
            services.GetRequiredService<HotelStore>(),
            services.GetRequiredService<NavigationService<MakeReservationViewModel>>());
    }
}
