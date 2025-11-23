using System;
using System.Collections.Generic;

namespace SistemaAcademico.Persistence.Models;

public partial class DetalleFactura
{
    public int Id { get; set; }

    public int IdFactura { get; set; }

    public string Concepto { get; set; } = null!;

    public decimal Monto { get; set; }

    public decimal Subtotal { get; set; }

    public decimal Descuento { get; set; }

    public decimal MontoTotal { get; set; }

    public virtual Factura IdFacturaNavigation { get; set; } = null!;
}
