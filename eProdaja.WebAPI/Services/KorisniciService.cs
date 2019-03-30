﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using eProdaja.Model;
using eProdaja.Model.Requests;
using eProdaja.WebAPI.Database;
using eProdaja.WebAPI.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace eProdaja.WebAPI.Services
{
    public class KorisniciService : IKorisniciService
    {
        private readonly eProdajaContext _context;
        private readonly IMapper _mapper;
        public KorisniciService(eProdajaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<Model.Korisnici> Get(KorisniciSearchRequest request)
        {
            var query = _context.Korisnici.AsQueryable();

            if (!string.IsNullOrWhiteSpace(request?.Ime))
            {
                query = query.Where(x => x.Ime.StartsWith(request.Ime));
            }

            if (!string.IsNullOrWhiteSpace(request?.Prezime))
            {
                query = query.Where(x => x.Prezime.StartsWith(request.Prezime));
            }

            if(request?.IsUlogeLoadingEnabled == true)
            {
                query = query.Include(x => x.KorisniciUloge);
            }

            var list = query.ToList();

            return _mapper.Map<List<Model.Korisnici>>(list);
        }

        public Model.Korisnici GetById(int id)
        {
            var entity = _context.Korisnici.Find(id);

            return _mapper.Map<Model.Korisnici>(entity);
        }

        public Model.Korisnici Insert(KorisniciInsertRequest request)
        {
            var entity = _mapper.Map<Database.Korisnici>(request);

            if(request.Password != request.PasswordPotvrda)
            {
                throw new Exception("Passwordi se ne slažu");
            }

            entity.LozinkaHash = "test";
            entity.LozinkaSalt = "test";

            _context.Korisnici.Add(entity);
            _context.SaveChanges();

            return _mapper.Map<Model.Korisnici>(entity);
        }

        public Model.Korisnici Update(int id, KorisniciInsertRequest request)
        {
            var entity = _context.Korisnici.Find(id);
            _context.Korisnici.Attach(entity);
            _context.Korisnici.Update(entity);

            _mapper.Map(request, entity);

            _context.SaveChanges();

            return _mapper.Map<Model.Korisnici>(entity);
        }
    }
}
