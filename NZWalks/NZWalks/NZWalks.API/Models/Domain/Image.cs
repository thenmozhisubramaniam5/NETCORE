﻿using System.ComponentModel.DataAnnotations.Schema;

namespace NZWalks.API.Models.Domain
{
    public class Images
    {
        public Guid Id { get; set; }
        [NotMapped] public IFormFile File { get; set; } // File content. we are not saving to DB.

        public string FileName { get; set; }
        public string? FileDescription { get; set; }
        public string FileExtension { get; set; }
        public long FileSizeInBytes { get; set; }
        public string FilePath { get; set; }
    }
}