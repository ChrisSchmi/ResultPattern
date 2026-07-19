using System;

namespace ResultPatternDemo.Common
{
    // ==========================================
    // Fixe, kleine Kategorie-Liste für generischen
    // Infrastruktur-Code (HTTP-Mapping, Logging, ...).
    // Bewusst NICHT pro Domäne erweiterbar.
    // ==========================================
    public enum ErrorCategory
    {
        None = 0,
        Validation,          // Request strukturell fehlerhaft -> 400
        UnprocessableEntity, // Request strukturell ok, verletzt aber eine fachliche Regel -> 422
        NotFound,
        Unauthorized,
        Forbidden,
        Conflict,
        Failure
    }

    public interface IBaseError
    {
        string Description { get; }
        ErrorCategory Category { get; }
    }

    /// <summary>
    /// Basisklasse für alle domänenspezifischen Fehlertypen.
    /// TErrorType ist das feingranulare, pro Domäne freie Enum
    /// (z.B. EmployeeErrorType). Die Zuordnung zu einer groben
    /// ErrorCategory erfolgt zentral über MapToCategory, damit
    /// sie nicht an jeder Aufrufstelle separat gepflegt werden muss.
    /// </summary>
    public abstract class BaseError<TErrorType> : IBaseError
        where TErrorType : Enum
    {
        public string Description { get; }
        public TErrorType ErrorType { get; }
        public ErrorCategory Category { get; }

        protected BaseError(string description, TErrorType errorType)
        {
            Description = description ?? throw new ArgumentNullException(nameof(description));
            ErrorType = errorType;
            Category = MapToCategory(errorType);

            if (Category == ErrorCategory.None)
                throw new InvalidOperationException(
                    $"MapToCategory darf für '{errorType}' nicht ErrorCategory.None liefern.");
        }

        protected abstract ErrorCategory MapToCategory(TErrorType errorType);
    }

    /// <summary>
    /// Nicht-generischer Anker, damit generischer Infrastruktur-Code
    /// (z.B. ResultExtensions.ToActionResult) mit JEDEM Result arbeiten
    /// kann, ohne TValue/TError zu kennen.
    /// </summary>
    public interface IBaseResult
    {
        bool IsSuccess { get; }
        IBaseError? Error { get; }
    }

    /// <summary>
    /// Result ohne Rückgabewert (z.B. Delete, Validate).
    /// </summary>
    public abstract class BaseResult<TError> : IBaseResult
        where TError : IBaseError
    {
        public bool IsSuccess { get; }
        public TError? Error { get; }

        IBaseError? IBaseResult.Error => Error;

        protected BaseResult()
        {
            IsSuccess = true;
            Error = default;
        }

        protected BaseResult(TError error)
        {
            IsSuccess = false;
            Error = error ?? throw new ArgumentNullException(nameof(error));
        }
    }

    /// <summary>
    /// Result mit Rückgabewert (z.B. GetById).
    /// </summary>
    public abstract class BaseResult<TValue, TError> : BaseResult<TError>
        where TError : IBaseError
    {
        public TValue? Value { get; }

        protected BaseResult(TValue value) : base() => Value = value;
        protected BaseResult(TError error) : base(error) { }
    }
}
