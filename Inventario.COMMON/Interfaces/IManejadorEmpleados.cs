using Inventario.COMMON.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventario.COMMON.Interfaces
{
    public interface IManejadorEmpleados : IManejadorGenerico<Empleado>
    {
        List<Empleado> EMpleadoPorArea(string area);
    }
}
