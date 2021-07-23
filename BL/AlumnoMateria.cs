using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class AlumnoMateria
    {
        public static ML.Result AlumnoMateriaGetAsignadasByIdAlumno(ML.AlumnoMateria AlumnoMateriaItem)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.Control_EscolarEntities context = new DL_EF.Control_EscolarEntities())
                {
                    var query = context.AlumnoMateriaGetAsignadasByIdAlumno(AlumnoMateriaItem.Alumno.IdAlumno).ToList();
                    result.Objects = new List<object>();

                    if (query != null)
                    {
                        foreach (var objs in query)
                        {
                            ML.AlumnoMateria AlumnoMateria = new ML.AlumnoMateria();
                   
                            AlumnoMateria.IdAlumnoMateria = objs.IdAlumnoMateria;
                            AlumnoMateria.Alumno = new ML.Alumno();
                            AlumnoMateria.Materia = new ML.Materia();
                            AlumnoMateria.Alumno.IdAlumno = objs.IdAlumno.Value;
                            AlumnoMateria.Materia.IdMateria = objs.IdMateria.Value;                           
                            AlumnoMateria.Materia.Nombre = objs.MateriaNombre;
                            AlumnoMateria.Materia.Costo = objs.Costo.Value;                          
                            AlumnoMateria.Alumno.Nombre = objs.AlumnoNombre;
                            AlumnoMateria.Alumno.ApellidoPaterno = objs.ApellidoPaterno;
                            AlumnoMateria.Alumno.ApellidoMaterno = objs.ApellidoMaterno;

                            result.Objects.Add(AlumnoMateria);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Error: No existen datos en la base de datos";
                    }              
                }
            }
            catch (Exception Ex)
            {
                result.Correct = false;
                result.ErrorMessage = Ex.Message;
            }
            return result;
        }

        public static ML.Result AlumnoMateriaDelete(ML.AlumnoMateria alumnoMateria)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.Control_EscolarEntities context = new DL_EF.Control_EscolarEntities())
                {
                    var query = context.AlumnoMateriaDelete(alumnoMateria.IdAlumnoMateria);
                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ha ocurrido un error al momento de eliminar";
                    }
                }
            }
            catch (Exception Ex)
            {
                result.Correct = false;
                result.ErrorMessage = Ex.Message;
            }
            return result;
        }

        public static ML.Result AlumnoMateriaGetNotAsignadasByIdAlumno(ML.AlumnoMateria AlumnoMateriaItem)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.Control_EscolarEntities context = new DL_EF.Control_EscolarEntities())
                {
                    var query = context.AlumnoMateriaGetNotAsignadasByIdAlumno(AlumnoMateriaItem.Alumno.IdAlumno).ToList();
                    result.Objects = new List<object>();

                    if (query != null)
                    {
                        foreach (var objs in query)
                        {
                            ML.AlumnoMateria AlumnoMateria = new ML.AlumnoMateria();
                            AlumnoMateria.Materia = new ML.Materia();

                            AlumnoMateria.Materia.IdMateria = objs.IdMateria;
                            AlumnoMateria.Materia.Nombre = objs.Nombre;
                            AlumnoMateria.Materia.Costo = objs.Costo.Value;

                            result.Objects.Add(AlumnoMateria);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Error: No existen datos en la base de datos";
                    }
                }
            }
            catch (Exception Ex)
            {
                result.Correct = false;
                result.ErrorMessage = Ex.Message;
            }
            return result;
        }

        public static ML.Result Add(ML.AlumnoMateria alumnoMateria)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.Control_EscolarEntities context = new DL_EF.Control_EscolarEntities())
                {
                    var query = context.AlumnoMateriaAdd(alumnoMateria.Alumno.IdAlumno, alumnoMateria.Materia.IdMateria);
                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ha ocurrido un error al momento de insertar";
                    }
                }
            }
            catch (Exception Ex)
            {
                result.Correct = false;
                result.ErrorMessage = Ex.Message;
            }
            return result;
        }
    }
}
