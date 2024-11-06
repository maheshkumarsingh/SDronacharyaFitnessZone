using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Core.Domain.Entities;

namespace WebApp.Core.DTOs
{
    public class PhotoResponseDTO
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public bool IsMain { get; set; }
    }


    public static class PhotoExtension
    {
        public static PhotoResponseDTO ToPhotoResponseDTO(this Photo photo)
        {
            return new PhotoResponseDTO
            {
                Id = photo.Id,
                Url = photo.Url,
                IsMain = photo.IsMain,
            };
        }
    }
}
