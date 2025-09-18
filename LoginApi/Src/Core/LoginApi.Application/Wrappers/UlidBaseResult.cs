using System.Collections.Generic;
using System.Linq;

namespace HyBrForex.Application.Wrappers;

public class UlidBaseResult
{
    public bool Success { get; set; }
    public List<Error> Errors { get; set; }

    public static UlidBaseResult Ok()
    {
        return new UlidBaseResult { Success = true };
    }

    public static UlidBaseResult Failure()
    {
        return new UlidBaseResult { Success = false };
    }

    public static UlidBaseResult Failure(Error error)
    {
        return new UlidBaseResult { Success = false, Errors = [error] };
    }

    public static UlidBaseResult Failure(IEnumerable<Error> errors)
    {
        return new UlidBaseResult { Success = false, Errors = errors.ToList() };
    }

    public static implicit operator UlidBaseResult(Error error)
    {
        return new UlidBaseResult { Success = false, Errors = [error] };
    }

    public static implicit operator UlidBaseResult(List<Error> errors)
    {
        return new UlidBaseResult { Success = false, Errors = errors };
    }

    public UlidBaseResult AddError(Error error)
    {
        Errors ??= [];
        Errors.Add(error);
        Success = false;
        return this;
    }
}

public class UlidBaseResult<TData> : UlidBaseResult
{
    public TData Data { get; set; }

    public static UlidBaseResult<TData> Ok(TData data)
    {
        return new UlidBaseResult<TData> { Success = true, Data = data };
    }

    public new static UlidBaseResult<TData> Failure()
    {
        return new UlidBaseResult<TData> { Success = false };
    }

    public new static UlidBaseResult<TData> Failure(Error error)
    {
        return new UlidBaseResult<TData> { Success = false, Errors = [error] };
    }

    public new static UlidBaseResult<TData> Failure(IEnumerable<Error> errors)
    {
        return new UlidBaseResult<TData> { Success = false, Errors = errors.ToList() };
    }

    public static implicit operator UlidBaseResult<TData>(TData data)
    {
        return new UlidBaseResult<TData> { Success = true, Data = data };
    }

    public static implicit operator UlidBaseResult<TData>(Error error)
    {
        return new UlidBaseResult<TData> { Success = false, Errors = [error] };
    }

    public static implicit operator UlidBaseResult<TData>(List<Error> errors)
    {
        return new UlidBaseResult<TData> { Success = false, Errors = errors };
    }
}