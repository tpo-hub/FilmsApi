using AutoMapper;
using FilmsApi.DTOs;
using FilmsApi.Helpers;
using FilmsApi.Models;
using FilmsApi.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FilmsApi.Controllers
{
    [ApiController]
    [Route("api/Actors")]
    public class ActorsController : CustomBaseController
    {

        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IStockerFile stockerFile;
        private readonly string container = "actors";

        public ActorsController(ApplicationDbContext context, IMapper mapper, IStockerFile stockerFile)
            :base(context, mapper)
                 
        {
            this.context = context;
            this.mapper = mapper;
            this.stockerFile = stockerFile;
        }

        [HttpGet]
        public async Task<ActionResult<List<ActorDTO>>> Actors([FromQuery] PaginationDTO paginationDTO )
        {
            return await Get<Actor, ActorDTO>(paginationDTO);  
        }

        [HttpGet("{id}", Name = "GetActor")]
        public async Task<ActionResult<ActorDTO>> Actor(int id)
        {

            return await Get<Actor, ActorDTO>(id);
        }

        [HttpPost]
        public async Task<ActionResult<ActorDTO>> Post([FromForm] ActorPostDTO actorPost)
        {

            var actor = await context.Actors.FirstOrDefaultAsync(x => x.Name == actorPost.Name);

            if (actor != null)
            {
                return NotFound();
            }

            var toAddActor = mapper.Map<Actor>(actorPost);

            if(actorPost.Photho != null )
            {
                using (var memoryStream = new MemoryStream())
                {
                    await actorPost.Photho.CopyToAsync(memoryStream);
                    var content = memoryStream.ToArray();
                    var extension = Path.GetExtension(actorPost.Photho.FileName);

                    toAddActor.Photho = await stockerFile.StockFile(content, extension, container,
                       actorPost.Photho.ContentType);
                    
                };
            }

            context.Add(toAddActor);

            await context.SaveChangesAsync();

            var actorMap = mapper.Map<ActorDTO>(toAddActor);

            return new CreatedAtRouteResult("GetActor", new { id = actorMap.Id }, actorMap);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ActorDTO>> Put(int id, [FromForm] ActorPostDTO actorPut)
        {

            var actor = await context.Actors.FirstOrDefaultAsync(x => x.Id == id);

            if (actor == null)
            {
                return NotFound();
            }

            actor = mapper.Map(actorPut, actor);

            if (actorPut.Photho != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await actorPut.Photho.CopyToAsync(memoryStream);
                    var content = memoryStream.ToArray();
                    var extension = Path.GetExtension(actorPut.Photho.FileName);

                    actor.Photho = await stockerFile.EditFile(content, extension, container,
                    actor.Photho,actorPut.Photho.ContentType);                   
                };
            }
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            return await Delete<Actor>(id);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> Patch(int id, [FromBody] JsonPatchDocument<ActorPatchDTO> patchDocument)
        {
            return await Patch<Actor, ActorPatchDTO>(id, patchDocument);
        }

    }
}
