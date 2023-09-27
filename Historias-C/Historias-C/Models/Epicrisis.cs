﻿using System.ComponentModel.DataAnnotations;

namespace Historias_C.Models
{
    public class Epicrisis
    {

        [Range(10,20,ErrorMessage = ErrorMessages._reqStrMinMaxEpicrisis)]
        public int Id { get; set; }
        public Episodio Episodio { get; set; }
        public Medico Medico { get; set; }
        public DateTime fechaYHora { get; set; } = DateTime.Now;
        public Diagnostico Diagnostico { get; set; }


        public Epicrisis()
        {

        }

        public Epicrisis(int id, Episodio episodio, Medico medico, DateTime fechaYHora, Diagnostico diagnostico)
        {
            this.Id = id;
            this.Episodio = episodio;
            this.Medico = medico;
            this.fechaYHora = fechaYHora;
            this.Diagnostico = diagnostico;


        }


    }
}
