﻿using BookFaceApp.Infrastructure.Data.Entities;
using System.ComponentModel.DataAnnotations;
using static BookFaceApp.Infrastructure.Data.DataConstants.PublicationConstants;

namespace BookFaceApp.Core.Models.Publication
{
    public class PublicationAddModel
    {
        [Required]
        [MaxLength(MaxPublicationTitle)]
        public string Title { get; set; } = null!;

        [Required]
        public string ImageUrl { get; set; } = null!;

        [Required]
        public int CategoryId { get; set; }

        public IEnumerable<Category> Categories { get; set; } = new List<Category>();
    }
}