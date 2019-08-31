using System;
using System.ComponentModel.DataAnnotations;

namespace Booking_clerk.Core
{
    public class Film
    {
        public int Id { get; set; }

        [Display(Name ="Название фильма")]
        [Required(ErrorMessage = "заполните поле")]
        public string Title { get; set; }
        [Display(Name = "Начало фильма")]
        [Required(ErrorMessage = "заполните поле")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy hh:mm}")]
        public DateTime Start { get; set; }

        [Display(Name = "Число мест")]
        [Required(ErrorMessage = "заполните поле")]
        public int NumPlaces { get; set; }
        [Display(Name = "Время начало продаж")]
        [Required(ErrorMessage = "заполните поле")]
        public DateTime OrderStart { get; set; }
        [Display(Name = "Время окончания продаж")]
        [Required(ErrorMessage = "заполните поле")]
        public DateTime OrderEnd { get; set; }

        [Display(Name = "Число свободных мест")]
        public int FreeNumPlaces { get; set; }

    }
}