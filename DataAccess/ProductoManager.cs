using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ProductoManager
    {
        public int GetExistenciaById(int idProducto)
        {
            int existencia = 0;
            using (var context = new ChristmasBazaarContext())
            {
                existencia = (from producto in context.Productos
                              where producto.idProducto == idProducto
                              select producto)
                              .ToList()
                              .ElementAt(0)
                              .existencia;
            }
            return existencia;
        }

        public double GetPrecioById(int idProducto)
        {
            double precioEndontrado =0;
            using (var context = new ChristmasBazaarContext())
            {
                precioEndontrado = (from producto in context.Productos
                                    where producto.idProducto == idProducto
                                    select producto)
                                      .ToList()
                                      .ElementAt(0)
                                      .precio;
            }
            return precioEndontrado;
        }
    }
}
