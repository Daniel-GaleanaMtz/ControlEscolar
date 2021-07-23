using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SL_WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IServiceAlumno" in both code and config file together.
    [ServiceContract]
    public interface IServiceAlumno
    {
        [OperationContract]
        Result Add(ML.Alumno alumno);

        [OperationContract]
        Result Update(ML.Alumno alumno);

        [OperationContract]
        Result Delete(ML.Alumno alumno);

        [OperationContract]
        [ServiceKnownType(typeof(ML.Alumno))]
        Result GetAll(ML.Alumno alumno);

        [OperationContract]
        [ServiceKnownType(typeof(ML.Alumno))]
        Result GetById(ML.Alumno alumno);
    }

    public class Result
    {

        [DataMember]
        public bool Correct { get; set; }

        [DataMember]
        public object Object { get; set; }

        [DataMember]
        public List<object> Objects { get; set; }
        [DataMember]
        public string ErrorMessage { get; set; }
        [DataMember]
        public Exception Ex { get; set; }
    }
}
