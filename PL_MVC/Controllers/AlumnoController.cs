using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL_MVC.Controllers
{
    public class AlumnoController : Controller
    {
        //
        // GET: /Alumno/
        public ActionResult GetAll()
        {
            ML.Alumno alumno = new ML.Alumno();
            ML.Result result = new ML.Result();
            ServiceReferenceAlumno.ServiceAlumnoClient service = new ServiceReferenceAlumno.ServiceAlumnoClient();
            var SL_result = service.GetAll(alumno);
            if (SL_result.Correct)
            {

            }
            var x = SL_result.Objects;
            result.Objects = SL_result.Objects.ToList();
            alumno.Alumnos = result.Objects;
            return View(alumno);
        }

        [HttpGet]
        public ActionResult Delete(int IdAlumno)
        {
            ML.Alumno alumno = new ML.Alumno();
            alumno.IdAlumno = IdAlumno;
            ServiceReferenceAlumno.ServiceAlumnoClient service = new ServiceReferenceAlumno.ServiceAlumnoClient();
            service.Delete(alumno);
            return RedirectToAction("GetAll");
        }

        [HttpGet]
        public ActionResult Form(int? IdAlumno)
        {
            ML.Alumno alumno = new ML.Alumno();
            alumno.IdAlumno = IdAlumno.GetValueOrDefault();

            if (IdAlumno == null)
            {
                return View(alumno);
            }
            else
            {
                ML.Result result = BL.Alumno.AlumnoGetById(alumno);

                if (result.Correct)
                {
                    alumno.IdAlumno = ((ML.Alumno)result.Object).IdAlumno;
                    alumno.Nombre = ((ML.Alumno)result.Object).Nombre;
                    alumno.ApellidoPaterno = ((ML.Alumno)result.Object).ApellidoPaterno;
                    alumno.ApellidoMaterno = ((ML.Alumno)result.Object).ApellidoMaterno;
                    return View(alumno);
                }
                else
                {
                    ViewBag.Message = result.ErrorMessage;
                    return PartialView("Modal");
                }
            }
        }

        [HttpPost] 
        public ActionResult Form(ML.Alumno alumno)
        {
            ML.Result result = new ML.Result();

            if (alumno.IdAlumno == 0)
            {
                ServiceReferenceAlumno.ServiceAlumnoClient service = new ServiceReferenceAlumno.ServiceAlumnoClient();
                var result_SL = service.Add(alumno);
                result.Correct = result_SL.Correct;
                result.ErrorMessage = result_SL.ErrorMessage;
                ViewBag.Message = "El alumno se agregó correctamente ";
                //return PartialView("Modal");
            }
            else
            {
                ServiceReferenceAlumno.ServiceAlumnoClient service = new ServiceReferenceAlumno.ServiceAlumnoClient();
                service.Update(alumno);
                var result_SL = service.Update(alumno);
                result.Correct = result_SL.Correct;
                result.ErrorMessage = result_SL.ErrorMessage;                
                ViewBag.Message = "El alumno se actualizó correctamente ";
                //return PartialView("Modal");
            }

            if (!result.Correct)
            {
                ViewBag.Message = "No se pudo agregar correctamente al alumno " + result.ErrorMessage;
            }
            return PartialView("Modal");
        }
	}
}