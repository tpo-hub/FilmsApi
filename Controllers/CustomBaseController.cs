using AutoMapper;
using FilmsApi.DTOs;
using FilmsApi.Helpers;
using FilmsApi.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmsApi.Controllers
{
    public class CustomBaseController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public CustomBaseController(ApplicationDbContext context, IMapper mapper )
        {
            this.context = context;
            this.mapper = mapper;
        }

        protected async Task<List<TDTO>> Get<TEntity, TDTO>() where TEntity : class
        {
            var entities = await context.Set<TEntity>().ToListAsync();
            var entityMap = mapper.Map<List<TDTO>>(entities);
            return entityMap;
        } 

        protected async Task<List<TDTO>> Get<TEntity, TDTO>(PaginationDTO paginationDTO) where TEntity : class
        {
            var queryable = context.Set<TEntity>().AsQueryable();
            return await Get<TEntity, TDTO>(paginationDTO, queryable);
        }    
        protected async Task<List<TDTO>> Get<TEntity, TDTO>(PaginationDTO paginationDTO, 
            IQueryable<TEntity> queryable) where TEntity : class
        {
            await HttpContext.InsertPaginationParams(queryable, paginationDTO.countRegistersForPage);
            var entityList = await queryable.Paginate(paginationDTO).ToListAsync();
            var entityMap = mapper.Map<List<TDTO>>(entityList);
            return entityMap;
        } 
 
        protected async Task<ActionResult<TDTO>> Get<TEntity, TDTO>(int id) where TEntity : class, IId
        {
            var entity = await context.Set<TEntity>().FirstOrDefaultAsync(x=> x.Id == id);

            if(entity == null)
            {
                return NotFound();
            }

            var entityMap = mapper.Map<TDTO>(entity);
            return entityMap;
        }  
        protected async Task<ActionResult> Post <TCreate, TEntity, TRead>(TCreate createDto, string route) 
            where TEntity : class, IId
        {

            var toAddEntity = mapper.Map<TEntity>(createDto);
            context.Add(toAddEntity);
            await context.SaveChangesAsync();
            var entityMap = mapper.Map<TRead>(toAddEntity);
            return new CreatedAtRouteResult(route , new { id = toAddEntity.Id }, entityMap);
        }
        protected async Task<ActionResult> Put<TCreate, TEntity>(TCreate createDto, int id) 
            where TEntity : class, IId
        {
            var entity = await context.Set<TEntity>().AnyAsync(x => x.Id == id);
            if (!entity)
            {
                return NotFound();
            }
            var toPutEntity = mapper.Map<TEntity>(createDto);
            toPutEntity.Id = id;
            context.Entry(toPutEntity).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        } 
        protected async Task<ActionResult> Delete<TEntity>(int id) 
            where TEntity : class, IId, new()
        {
            var entity = await context.Set<TEntity>().AnyAsync(x => x.Id == id);
            if (!entity)
            {
                return NotFound();
            }
            context.Remove(new TEntity { Id = id});

            await context.SaveChangesAsync();

            return NoContent();
        }
        protected async Task<ActionResult> Patch<TEntity, TDTO>(int id, JsonPatchDocument<TDTO> patchDocument) 
        where TDTO : class
        where TEntity: class, IId
        {
            if (patchDocument == null)
            {
                return BadRequest();
            }

            var entity = await context.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null)
            {
                return NotFound();
            }
            var entityMap = mapper.Map<TDTO>(entity);
            patchDocument.ApplyTo(entityMap, ModelState);
            var IsValid = TryValidateModel(entityMap);
            if (!IsValid)
            {
                return BadRequest(ModelState);
            }
            mapper.Map(entityMap, entity);
            await context.SaveChangesAsync();
            return NoContent();
        }

    }
}
