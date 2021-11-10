using Api.Models;
using System.Collections.Generic;

namespace Api.ViewModels
{
    public class IndexViewModel
    {
        // ViewModel объединяет данные из разных моделей
        // .. или наоборот содержит в себе лишь несколько полей большой модели
        // С помощью этой вью модели можем передать в представление сразу список компаний и список смартфонов
        public IEnumerable<Phone> Phones { get; set; }
        public IEnumerable<CompanyModel> Companies { get; set; }
    }
}
