using Domain.Shared.ValueObjects;

namespace Domain.Entities.Permissions.ObjectValues;

public record TypePermission(string Value) : BaseValueObject(Value) { }
