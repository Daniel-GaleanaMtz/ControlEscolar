using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SL_WebAPI.Controllers
{
    public class MateriaController : ApiController
    {
        // GET api/materia
        [Route("api/materia")]
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            ML.Result result = BL.Materia.GetAll();
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return Content(HttpStatusCode.NotFound, result);
            }
        }

        // GET api/materia/5
        [Route("api/materia/{IdMateria}")]
        [HttpGet]
        public IHttpActionResult Get(int IdMateria)
        {
            ML.Materia materia = new ML.Materia();
            materia.IdMateria = IdMateria;
            ML.Result result = BL.Materia.GetById(materia);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return Content(HttpStatusCode.NotFound, result);
            }
        }

        // POST api/departamento
        [Route("api/materia")]
        [HttpPost]
        public IHttpActionResult Post([FromBody]ML.Materia materia)
        {
            ML.Result result = BL.Materia.AddEF(materia);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return Content(HttpStatusCode.NotFound, result);
            }
        }

        // PUT api/departamento/5
        [Route("api/materia/{IdMateria}")]
        [HttpPut]
        public IHttpActionResult Put(int IdMateria, [FromBody]ML.Materia materia)
        {
            materia.IdMateria = IdMateria;
            ML.Result result = BL.Materia.UpdateEF(materia);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return Content(HttpStatusCode.NotFound, result);
            }
        }

        // DELETE api/departamento/5
        [Route("api/materia/{IdMateria}")]
        [HttpDelete]
        public IHttpActionResult Delete(int IdMateria)
        {
            ML.Materia materia = new ML.Materia();
            materia.IdMateria = IdMateria;
            ML.Result result = BL.Materia.DeleteEF(materia);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return Content(HttpStatusCode.NotFound, result);
            }
        }
    }
}
