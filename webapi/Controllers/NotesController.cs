using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi.Data;
using webapi.Models.Entities;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace webapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class NotesController : Controller
    {

        private readonly NoteDbContext noteDbContext;
        public NotesController(NoteDbContext noteDbContext)
        {
            this.noteDbContext = noteDbContext;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllNotes()
        {
            return Ok(await noteDbContext.Notes.ToListAsync());
        }


        [HttpGet]
        [Route("{id:Guid}")]
        [ActionName("GetNotebyId")]

        public async Task<IActionResult> GetNotebyId([FromRoute] Guid id)
        {
            await noteDbContext.Notes.FirstOrDefaultAsync(x => x.Id == id);


            var note = await noteDbContext.Notes.FindAsync(id);

            if (note == null)
            {
                return NotFound();
            }

            return Ok(note);
        }


        [HttpPost]


        public async Task<IActionResult> AddNote(Note note)
        {
            note.Id = Guid.NewGuid();
            await noteDbContext.Notes.AddAsync(note);
            await noteDbContext.SaveChangesAsync();


            return CreatedAtAction(nameof(GetNotebyId), new { id = note.Id }, note);
        }


        [HttpPut]
        [Route("{id:Guid}")]

        public async Task<IActionResult> updateNote([FromRoute] Guid id, [FromBody] Note updatedNote)
        {
            var exisitingnode = await noteDbContext.Notes.FindAsync(id);

            if (exisitingnode == null) { return NotFound(); }

            exisitingnode.Title = updatedNote.Title;
            exisitingnode.Description = updatedNote.Description;
            exisitingnode.IsVisible = updatedNote.IsVisible;

            await noteDbContext.SaveChangesAsync();

            return Ok(exisitingnode);

        }

        [HttpDelete]
        [Route("{id:Guid}")]

        public async Task<IActionResult> DeleteNote([FromRoute] Guid id)
        {
            var exisitingnode = await noteDbContext.Notes.FindAsync(id);

            if (exisitingnode == null) { return NotFound(); }

            noteDbContext.Notes.Remove(exisitingnode);

            await noteDbContext.SaveChangesAsync();

            return Ok();

        }

    }
}

