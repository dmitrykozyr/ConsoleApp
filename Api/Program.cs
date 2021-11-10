using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Api
{
    /*
        Entity Framework ��������� ���������������� �� �� � �������� � ������� ���������� �� ���� ���������
        ���� �� ���������� ������ ��������� ���������, �� �� �������������� - ���������
    
        EF ������������ ��� ��������� ������� �������������� � ����� ������:
        - DB first: EF ������� ������, ���������� ������ ��
        - Model first: ������� ��������� ������ ��, �� ������� ������� ������� �������� ��
        - Code first: ������� ��������� ����� ������ ������, ������� ����� ��������� � ��, ����� EF ���������� �� � �������

        � Entity Framework ���� ��� ������� �������� ������:
        - eager loading - ������ ��������
          ���������� ��������� �� �������� ����� ������ ����� ����� Include
          var players = db.Players.Include(p => p.Team).ToList()

        - explicit loading - ����� ��������
          ��� �������� ������ � �������� ��������� ����� Load()
          var p = db.Players.FirstOrDefault()
          db.Entry(p).Reference("Team").Load()

        - lazy loading - ������� ��������
          ������ ������������ ��� ������ ��������� � �������������� ��-��, � �� ����� �� ������������
          ������, ������������ ������� ��������, ������ ���� ����������, � �������� ����� ������������ public � virtual
          � ���� ������ �� ����������� ������������ Include � Load
     */
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
