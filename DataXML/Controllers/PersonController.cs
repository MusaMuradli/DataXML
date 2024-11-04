using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.IO.Pipes;

namespace DataXML.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PersonController : ControllerBase
{
    private readonly ICrudService _crudService;
    private static List<Person> People;

    public PersonController(IWebHostEnvironment env, ICrudService crudService)
    {
        _crudService = crudService;
        People = _crudService.Load();
    }

    [HttpGet]
    public ActionResult<List<Person>> Get()
    {
        return Ok(People);
    }

    [HttpGet("{id}")]
    public ActionResult<Person> GetById(int id)
    {
        var person = People.FirstOrDefault(p => p.Id == id);
        if (person == null)
            return NotFound("Person not found.");

        return Ok(person);
    }

    [HttpPost]
    public IActionResult Post([FromBody] Person person)
    {
        if (People.Any(p => p.Id == person.Id))
            return BadRequest("Person with this ID already exists.");

        People.Add(person);
        _crudService.Save(People);
        return CreatedAtAction(nameof(GetById), new { id = person.Id }, person);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] Person updatedPerson)
    {
        var person = People.FirstOrDefault(p => p.Id == id);
        if (person == null)
            return NotFound("Person not found.");

        person.FirstName = updatedPerson.FirstName;
        person.LastName = updatedPerson.LastName;
        person.Age = updatedPerson.Age;
        _crudService.Save(People); 

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var person = People.FirstOrDefault(p => p.Id == id);
        if (person == null)
            return NotFound("Person not found.");

        People.Remove(person);
        _crudService.Save(People); 
        return NoContent();
    }
}
