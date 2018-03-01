namespace WebApplication1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

    public partial class SelectLocation
    {
        public List<GolfCourse> Courses;
        public SelectList Location { get; set; }
        public string SelectedLocation { get; set; }

    }
}
