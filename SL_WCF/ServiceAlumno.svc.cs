using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SL_WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ServiceAlumno" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ServiceAlumno.svc or ServiceAlumno.svc.cs at the Solution Explorer and start debugging.
    public class ServiceAlumno : IServiceAlumno
    {
        public Result Add(ML.Alumno alumno)
        {

            ML.Result result = BL.Alumno.AlumnoAdd(alumno);

            return new Result { Correct = result.Correct, ErrorMessage = result.ErrorMessage, Ex = result.Ex };
        }

        public Result Update(ML.Alumno alumno)
        {
            ML.Result result = BL.Alumno.AlumnoUpdate(alumno);

            return new Result { Correct = result.Correct, ErrorMessage = result.ErrorMessage, Ex = result.Ex };
        }

        public Result Delete(ML.Alumno alumno)
        {

            ML.Result result = BL.Alumno.AlumnoDelete(alumno);

            return new Result { Correct = result.Correct, ErrorMessage = result.ErrorMessage, Ex = result.Ex };
        }

        public Result GetAll(ML.Alumno alumno)
        {

            ML.Result result = BL.Alumno.AlumnoGetAll();

            return new Result { Correct = result.Correct, ErrorMessage = result.ErrorMessage, Ex = result.Ex, Objects = result.Objects };
        }

        public Result GetById(ML.Alumno alumno)
        {

            ML.Result result = BL.Alumno.AlumnoGetById(alumno);

            return new Result { Correct = result.Correct, ErrorMessage = result.ErrorMessage, Ex = result.Ex, Object = result.Object };
        }
    }
}
