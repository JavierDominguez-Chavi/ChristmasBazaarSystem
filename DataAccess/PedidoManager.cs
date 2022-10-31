using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;

namespace DataAccess
{
    public class PedidoManager
    {
        public void AddPedido(Pedido pedido)
        {
            using (var context = new ChristmasBazaarContext())
            {
                ObjectResult<Decimal?> result = 
                    context.sp_insert_pedido(pedido.fecha, pedido.total, pedido.nombre, pedido.direccion);
                int insertedIdPedido = (int)result.ElementAt(0);

                foreach (PedidosProducto pedidoProducto in pedido.PedidosProductos)
                {
                    if (pedidoProducto.cantidad > 0)
                    {
                        context.sp_insert_pedidosproductos
                            (insertedIdPedido, pedidoProducto.idProducto, pedidoProducto.cantidad);
                    }
                        
                }
            }
        }
    }
}
