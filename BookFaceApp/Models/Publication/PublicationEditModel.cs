﻿using System.ComponentModel.DataAnnotations;
using static BookFaceApp.Data.DataConstants.PublicationConstants;

namespace BookFaceApp.Models.Publication
{
    public class PublicationEditModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(MaxPublicationTitle)]
        public string Title { get; set; } = null!;

        public string? ImageUrl { get; set; }

        public string UserId { get; set; } = null!;
    }
}
