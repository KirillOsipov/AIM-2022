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
    
    public partial class Оборудование
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Оборудование()
        {
            this.Производитель = "Россия";
            this.Лечение = new HashSet<Лечение>();
        }
    
        public int ИД_Оборудования { get; set; }
        public string Наименование { get; set; }
        public int Количество { get; set; }
        public int НомерПалаты { get; set; }
        public string Применение { get; set; }
        public string Производитель { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Лечение> Лечение { get; set; }
        public virtual Палаты Палаты { get; set; }
    }
}
