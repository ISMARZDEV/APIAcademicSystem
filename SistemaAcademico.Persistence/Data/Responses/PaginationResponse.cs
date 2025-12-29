using System;
using System.Collections.Generic;

namespace SistemaAcademico.Persistence.Data.Responses;

public class PaginationResponse<T>
{
    public ICollection<T> Items { get; set; } = new List<T>();
    public int Page { get; set; }
    public int ItemsPerPage { get; set; }
    public int TotalPages { get; set; }
}
