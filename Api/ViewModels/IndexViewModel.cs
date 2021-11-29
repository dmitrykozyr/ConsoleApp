using Api.Models;
using System.Collections.Generic;

namespace Api.ViewModels
{
    public class IndexViewModel
    {
        // ViewModel объединяет данные из разных моделей или содержит в себе несколько полей большой модели
        // С помощью этой ViewModel можем передать в представление список компаний и список смартфонов
        public IEnumerable<Phone> Phones { get; set; }
        public IEnumerable<CompanyModel> Companies { get; set; }
    }
}
