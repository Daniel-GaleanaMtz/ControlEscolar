using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Materia
    {
        public static ML.Result GetById(ML.Materia materia)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.Control_EscolarEntities context = new DL_EF.Control_EscolarEntities())
                {
                    var query = context.MateriaGetById(materia.IdMateria).FirstOrDefault();

                    materia.IdMateria = query.IdMateria;
                    materia.Nombre = query.Nombre;
                    materia.Costo = query.Costo.Value;

                    result.Object = materia;
                    if (query != null)
                    {
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
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.Control_EscolarEntities context = new DL_EF.Control_EscolarEntities())
                {
                    var materias = context.MateriaGetAll().ToList();
                    result.Objects = new List<object>();
                    if (materias != null)
                    {
                        foreach (var objs in materias)
                        {
                            ML.Materia materia = new ML.Materia();

                            materia.IdMateria = objs.IdMateria;
                            materia.Nombre = objs.Nombre;
                            materia.Costo = objs.Costo.Value;

                            result.Objects.Add(materia);
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

        public static ML.Result AddEF(ML.Materia materia)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.Control_EscolarEntities context = new DL_EF.Control_EscolarEntities())
                {
                    var query = context.MateriaAdd(materia.Nombre, materia.Costo);
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
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result UpdateEF(ML.Materia materia)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.Control_EscolarEntities context = new DL_EF.Control_EscolarEntities())
                {
                    var updateResult = context.MateriaUpdate(materia.IdMateria, materia.Nombre, materia.Costo);

                    if (updateResult >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ha ocurrido un error al momento de actualizar";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result DeleteEF(ML.Materia materia)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.Control_EscolarEntities context = new DL_EF.Control_EscolarEntities())
                {
                    var query = context.MateriaDelete(materia.IdMateria);
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
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
    }
}
