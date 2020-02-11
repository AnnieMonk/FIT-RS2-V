using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eProdaja.WebAPI.Mapper
{
    public class Mapper : Profile
    {
         public Mapper()
        {
            CreateMap<Database.Korisnici, Model.Korisnici>();
            CreateMap<Database.VrsteProizvoda, Model.VrsteProizvoda>();
            CreateMap<Database.JediniceMjere, Model.JediniceMjere>();
            CreateMap<Database.Proizvodi, Model.Proizvodi>();
            CreateMap<Database.Uloge, Model.Uloge>();
            CreateMap<Database.Korisnici, Model.Requests.KorisniciInsertRequest>().ReverseMap();
            CreateMap<Database.Proizvodi, Model.Requests.ProizvodSearchRequest>().ReverseMap();
            CreateMap<Database.Proizvodi, Model.Requests.ProizvodUpsertRequest>().ReverseMap();
        }
    }
}
