namespace WebApplication1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Game")]
    public partial class Game
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Game()
        {
            GameRound = new HashSet<GameRound>();
        }

        public int ID { get; set; }

        public int Player { get; set; }

        public int GolfCourse { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Date { get; set; }

        [Column("Total Par")]
        public int? Total_Par { get; set; }

        public virtual GolfCourse GolfCourse1 { get; set; }

        public virtual Player Player1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GameRound> GameRound { get; set; }
    }
}
