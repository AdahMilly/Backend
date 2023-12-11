using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TraineeDemo.Models;

namespace TraineeDemo.Controllers
{
    [Route("api/[controller]")] //routing
    [ApiController] // anotation for class is a controller
    public class TraineeController : ControllerBase
    {
        private ResponseDto _response;
        private readonly IMapper _mapper;


        public TraineeController(IMapper mapper)
        {
            _response = new ResponseDto(); // creating the instance for ourselves 
            _mapper = mapper;// an instance is created for us and we just save it inside a variable 
        }

        private static List<Trainee> trainees = new List<Trainee>()
        {
            new Trainee()
            {
                Name="Gordon",
                Email="gordon@gmail.com",
                PhoneNumber="012345678"
            }
        };

        //Get all trainees

        [HttpGet] // api/trainee
        public ActionResult<ResponseDto> getAllTrainees()
        {
            _response.Result = trainees;
            //_response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        }

        //Get one Trainee
        [HttpGet("{guid}")] //api/trainee/guid
        public ActionResult<ResponseDto> getTrainee(Guid guid)
        {
            var trainee = trainees.Find(x => x.Id == guid);
            if (trainee == null)
            {
                //not found
                _response.Result = null;
                _response.Message = "Trainee not Found";
                _response.StatusCode = HttpStatusCode.NotFound;
                return NotFound(_response);
            }
            _response.Result = trainee;
            return Ok(_response);
        }

        [HttpPost] // api/trainee
        public ActionResult<ResponseDto> addTrainee(AddTraineeDto newTrainee)
        {

            //trainees is of type List<Trainee>  and newTrainee is of type AddTraineeDto

            //manual way
            //var newTraine = new Trainee()
            //{
            //    Name = newTrainee.Name,
            //    Email = newTrainee.Email,
            //    PhoneNumber = newTrainee.PhoneNumber,
            //    //Id=Guid.NewGuid()
            //};
            var newTraine = _mapper.Map<Trainee>(newTrainee);
            _response.Result = "Trainee Added Successfully";
            _response.StatusCode = HttpStatusCode.Created;
            trainees.Add(newTraine);

            return Created($"api/trainee/{newTraine.Id}", _response);


        }

        [HttpPatch("{id}")]
        public ActionResult<ResponseDto> updateTrainee(Guid id, AddTraineeDto UptTrainee)
        {
            var trainee = trainees.Find(x => x.Id == id);
            if (trainee == null)
            {
                //not found
                _response.Result = null;
                _response.Message = "Trainee not Found";
                _response.StatusCode = HttpStatusCode.NotFound;
                return NotFound(_response);
            }
            //update
            //trainee.PhoneNumber=UptTrainee.PhoneNumber;
            //trainee.Email=UptTrainee.Email;
            //trainee.Name=UptTrainee.Name;   

            //Spread opertor
            _mapper.Map(UptTrainee, trainee);

            _response.Result = trainee;
            return Ok(_response);
        }

        [HttpDelete("{id}")]
        public ActionResult<ResponseDto> deleteTrainee(Guid id)
        {
            var trainee = trainees.Find(x => x.Id == id);
            if (trainee == null)
            {
                //not found
                _response.Result = null;
                _response.Message = "Trainee not Found";
                _response.StatusCode = HttpStatusCode.NotFound;
                return NotFound(_response);
            }
            //delete
            trainees.Remove(trainee);
            _response.StatusCode = HttpStatusCode.NoContent;
            _response.Result = "Trainee Removed Successfully";
            return NoContent();
        }
    }
}
