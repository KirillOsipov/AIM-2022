//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OsipovCourseWork.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Гражданин
    {
        public int ИД { get; set; }
        public string НомерПаспорта { get; set; }
        public string Фамилия { get; set; }
        public string Имя { get; set; }
        public string Отчество { get; set; }
        public System.DateTime ДатаРождения { get; set; }
        public bool Пол { get; set; }
        public string Телефон { get; set; }
    
        public virtual МедицинскийПерсонал МедицинскийПерсонал { get; set; }
        public virtual Пациенты Пациенты { get; set; }
    }
}
