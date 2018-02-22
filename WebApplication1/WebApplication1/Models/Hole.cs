namespace WebApplication1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Hole")]
    public partial class Hole
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Hole()
        {
            GameRound = new HashSet<GameRound>();
        }

        public int ID { get; set; }

        public int GolfCourse { get; set; }

        public int Par { get; set; }

        public int Number { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GameRound> GameRound { get; set; }

        public virtual GolfCourse GolfCourse1 { get; set; }
    }
}
