using System;
using System.Collections.Generic;

namespace SistemaAcademico.Persistence.Models;

public partial class Factura
{
    public int IdFactura { get; set; }

    public DateOnly Fecha { get; set; }

    public DateOnly FechaVencimiento { get; set; }

    public string NumeroComprobante { get; set; } = null!;

    public string Estatus { get; set; } = null!;

    public decimal Subtotal { get; set; }

    public decimal Descuento { get; set; }

    public decimal MontoTotal { get; set; }

    public int IdCuentaPorPagar { get; set; }

    public virtual ICollection<DetalleFactura> DetalleFacturas { get; set; } = new List<DetalleFactura>();

    public virtual CuentaPorPagar IdCuentaPorPagarNavigation { get; set; } = null!;
}
