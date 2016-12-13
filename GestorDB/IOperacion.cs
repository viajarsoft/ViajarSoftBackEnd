using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace GestorDB
{
    public interface IOperacion
    {
        void Dispose();
        void Ejecutar(string procedimientoAlmacenado, List<SqlParameter> parametros);
        DataTable EjecutarConDatosEnTabla(string procedimientoAlmacenado, List<SqlParameter> parametros);
        object EjecutarConValor(string procedimientoAlmacenado, List<SqlParameter> parametros);
    }
}
