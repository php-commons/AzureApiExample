using System.Diagnostics.Contracts;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PHP.AzureApiExample.Api.ViewModels;
using PHP.AzureApiExample.Domain;

namespace PHP.AzureApiExample.Api.Controllers
{
    /// <summary>
    /// Represents a collection of person resources. 
    /// </summary>
    [Route("api/[controller]")]
    public class PersonsController : Controller
    {
        private readonly IPersonRepository _personRepository;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="personRepository">Injected</param>
        public PersonsController(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        /// <summary>
        /// Get a person by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public IActionResult Get(int id)
        {
            Contract.Requires(id > 0);
            if (id <= 0)
                return BadRequest("Id must be an integer greater than zero");

            var person = _personRepository.GetById(id);

            if (person == null)
                return NotFound();

            return Ok(person);
        }

        /// <summary>
        /// Add a new person.
        /// </summary>>
        /// <param name="person"></param>
        [HttpPost]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public IActionResult Post([FromBody] PersonJsonViewModel person)      
        {
            Contract.Requires(person != null);           
            if (person == null)
                return BadRequest("Invalid person");

            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState.GetJsonValidationErrors());
            }

            var id  = _personRepository.AddPerson(person.ToDomainObject());
            return CreatedAtAction("Get", new { id }, id);            
        }

        /// <summary>
        /// Update a person's first and last name.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="person"></param>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Person), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public IActionResult Put(int id, PersonJsonViewModel person)
        {
            Contract.Requires(id > 0);
            Contract.Requires(person == null);
            if (id <= 0)
                return BadRequest("Id must be an integer greater than zero");      
            if (person == null)
                return BadRequest("Invalid person");

            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState.GetJsonValidationErrors());
            }

            var updatedPerson = _personRepository.UpdatePerson(id, person.ToDomainObject());

            if (updatedPerson != null)
            {
                return Ok(person);
            }

            return NotFound(id);
        }

        /// <summary>
        /// Delete a person given an idea.
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(void),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            Contract.Requires(id > 0);
            if (id <= 0)
                return BadRequest("Id must be an integer greater than zero");

            var wasFound = _personRepository.TryDelete(id);
            if (wasFound)
                return Ok();

            return NotFound(id);
        }
    }
}
