namespace WebApplication1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GameRound")]
    public partial class GameRound
    {
        public int ID { get; set; }

        public int Game { get; set; }

        public int Hole { get; set; }

        public int? Throws { get; set; }

        public virtual Game Game1 { get; set; }

        public virtual Hole Hole1 { get; set; }
    }
}
