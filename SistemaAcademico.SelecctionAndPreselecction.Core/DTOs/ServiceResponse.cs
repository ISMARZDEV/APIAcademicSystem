namespace SistemaAcademico.SelecctionAndPreselecction.Core.DTOs;

public class ServiceResponse
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;

    public static ServiceResponse Ok(string message = "") => new() { Success = true, Message = message };
    public static ServiceResponse Error(string message) => new() { Success = false, Message = message };
}
