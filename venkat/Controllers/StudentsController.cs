using Microsoft.AspNetCore.Mvc;
using venkat.Models;
using venkat.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace venkat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService studentService;
        public StudentsController(IStudentService service) { studentService = service; }
        
        // GET: api/<StudentsController>
        [HttpGet]
        public ActionResult<List<Student>> Get() { return studentService.Get(); }
        
        // GET api/<StudentsController>/5
        [HttpGet("{id}")]
        public ActionResult<Student> Get(string id)
        {
            Student student = studentService.Get(id);
            return student;
            // ?? NotFound($"Student with Id = {id} not found")
        }

        // POST api/<StudentsController>
        [HttpPost]
        public ActionResult<Student> Post([FromBody] Student student)
        {
            studentService.Create(student);
            return CreatedAtAction(nameof(Get), new { id = student.Id }, student);
        }
        
        // PUT api/<StudentsController>/5
        [HttpPut("{id}")]
        public ActionResult<Student> Put(string id, [FromBody] Student student)
        {
            var existingStudent=studentService.Get(id);
            if (existingStudent == null) { return NotFound($"Student with Id = {id} not found"); }
            else { studentService.Update(id, existingStudent); return NoContent(); }
        }
        
        // DELETE api/<StudentsController>/5
        [HttpDelete("{id}")]
        public ActionResult<Student> Delete(string id)
        {
            var existingStudent = studentService.Get(id);
            if (existingStudent == null) { return NotFound($"Student with Id = {id} not found"); }
            else { studentService.Remove(id); return Ok($"Student with Id = {id} deleted"); }
        }
    }
}
