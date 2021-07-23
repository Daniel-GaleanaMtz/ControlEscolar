using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL_MVC.Controllers
{
    public class AlumnoMateriaController : Controller
    {
        //
        // GET: /AlumnoMateria/
        public ActionResult AlumnoGetAll()
        {
            ML.Result result = BL.Alumno.AlumnoGetAll();
            ML.Alumno alumno = new ML.Alumno();
            alumno.Alumnos = result.Objects;
            return View(alumno);
        }

        public ActionResult AlumnoMateriaGetAsignadasByIdAlumno(int IdAlumno)
        {
            ML.Result result = new ML.Result();
            ML.AlumnoMateria alumnoMateria = new ML.AlumnoMateria();
            alumnoMateria.Alumno = new ML.Alumno();
            alumnoMateria.Alumno.IdAlumno = IdAlumno;
            result = BL.Alumno.AlumnoGetById(alumnoMateria.Alumno);
            if (result.Object != null)
            {
                ML.Alumno alumnoItem = new ML.Alumno();
                alumnoItem.IdAlumno = ((ML.Alumno)result.Object).IdAlumno;
                alumnoItem.Nombre = ((ML.Alumno)result.Object).Nombre;
                alumnoItem.ApellidoPaterno = ((ML.Alumno)result.Object).ApellidoPaterno;
                alumnoItem.ApellidoMaterno = ((ML.Alumno)result.Object).ApellidoMaterno;
            }
            //alumnoMateria.Materia = new ML.Materia();
            alumnoMateria.Alumno = (ML.Alumno)result.Object;
            result = BL.AlumnoMateria.AlumnoMateriaGetAsignadasByIdAlumno(alumnoMateria);
            alumnoMateria.AlumnoMaterias = result.Objects;
            return View(alumnoMateria);
        }

        public ActionResult AlumnoMateriaGetNotAsignadasByIdAlumno(int IdAlumno)
        {
            ML.Result result = new ML.Result();
            ML.AlumnoMateria alumnoMateria = new ML.AlumnoMateria();
            alumnoMateria.Alumno = new ML.Alumno();
            alumnoMateria.Alumno.IdAlumno = IdAlumno;
            result = BL.Alumno.AlumnoGetById(alumnoMateria.Alumno);
            if (result.Object != null)
            {
                ML.Alumno alumnoItem = new ML.Alumno();
                alumnoItem.IdAlumno = ((ML.Alumno)result.Object).IdAlumno;
                alumnoItem.Nombre = ((ML.Alumno)result.Object).Nombre;
                alumnoItem.ApellidoPaterno = ((ML.Alumno)result.Object).ApellidoPaterno;
                alumnoItem.ApellidoMaterno = ((ML.Alumno)result.Object).ApellidoMaterno;
            }
            alumnoMateria.Alumno = (ML.Alumno)result.Object;
            //alumnoMateria.Materia = new ML.Materia();
            result = BL.AlumnoMateria.AlumnoMateriaGetNotAsignadasByIdAlumno(alumnoMateria);
            alumnoMateria.AlumnoMaterias = result.Objects;
            return View(alumnoMateria);

            //ML.AlumnoMateria alumnoMateria = new ML.AlumnoMateria();
            //alumnoMateria.Alumno = new ML.Alumno();
            //alumnoMateria.Alumno.IdAlumno = IdAlumno;
            //alumnoMateria.Materia = new ML.Materia();
            //ML.Result result = BL.AlumnoMateria.AlumnoMateriaGetNotAsignadasByIdAlumno(alumnoMateria);
            //alumnoMateria.AlumnoMaterias = result.Objects;
            //return View(alumnoMateria);
        }

        [HttpPost]
        public ActionResult AlumnoMateriaGetNotAsignadasByIdAlumno(ML.AlumnoMateria alumnoMateria, int IdAlumno)
        {
            if (alumnoMateria.AlumnoMaterias != null)
            {
                foreach (string IdMateria in alumnoMateria.AlumnoMaterias)
                {
                    ML.AlumnoMateria alumnoMateriaItem = new ML.AlumnoMateria();

                    alumnoMateriaItem.Alumno = new ML.Alumno();
                    alumnoMateriaItem.Alumno.IdAlumno = IdAlumno;

                    alumnoMateriaItem.Materia = new ML.Materia();
                    alumnoMateriaItem.Materia.IdMateria = int.Parse(IdMateria);
                    ML.Result result = BL.AlumnoMateria.Add(alumnoMateriaItem);
                }
            }
            return RedirectToAction("AlumnoMateriaGetAsignadasByIdAlumno", new { IdAlumno });
        }

        public ActionResult Delete(int IdAlumnoMateria, int IdAlumno)
        {
            ML.AlumnoMateria alumnoMateria = new ML.AlumnoMateria();
            alumnoMateria.Alumno = new ML.Alumno();
            alumnoMateria.Alumno.IdAlumno = IdAlumno;
            alumnoMateria.IdAlumnoMateria = IdAlumnoMateria;
            ML.Result result = BL.AlumnoMateria.AlumnoMateriaDelete(alumnoMateria);
            if (result.Correct)
            {
                return RedirectToAction("AlumnoMateriaGetAsignadasByIdAlumno", new { IdAlumno });
            }
            else
            {
                result.ErrorMessage = "Hubo un problema";
            }
            return RedirectToAction("AlumnoMateriaGetAsignadasByIdAlumno?", new { IdAlumno });        
        }
	}
}