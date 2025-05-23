﻿using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Motos.Domain.Entities
{
    [Table("TB_PATIO")]
    public class Patio
    {
        [Key]
        [Column(TypeName = "VARCHAR2(255)")]
        public string IdPatio { get; set; }
        [Column(TypeName = "VARCHAR2(255)")]
        public string IdFilial { get; set; }
        [Column(TypeName = "VARCHAR2(255)")]
        public bool EstruturaPatioCriada { get; set; }
        public ICollection<Moto>? Motos { get; set; } = new Collection<Moto>();
    }
}
