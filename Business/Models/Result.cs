using Business.Interfaces;

namespace Business.Models;

public abstract class Result : IResult
{
    public bool Success { get; protected set; }

    public int StatusCode { get; protected set; }

    public string? ErrorMassage { get; protected set; }
}